using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using tmplltest.Core.Context;
using tmplltest.Core.DataModels;
using tmplltest.Core.Impl;
using tmplltest.Core.Utilities;

namespace tmplltest
{
    class Program
    {
        static void  Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<IHttpResourceService, HttpResourceService>()
                .AddSingleton<IJokeService, JokeService>()
                .BuildServiceProvider();

            Console.WriteLine("Starting Up Joke Service");

            //do the actual work here
            var jokeService = services.GetService<IJokeService>();
            var jokes =  jokeService.GetJokes(count: 20).GetAwaiter().GetResult();
            if(jokes.Count < 1)
            {
                Console.WriteLine("Ughh!! No Jokes found.. Guess who the joke is on now eh ?");
            }
            else
            {
                List<JokeDataObject> _jokes = new List<JokeDataObject>();
                foreach(var joke in jokes)
                {
                    JokeDataObject dbJoke =  new JokeDataObject()
                    {
                        Joke = joke.Joke
                    };
                    _jokes.Add(dbJoke);
                }

                //this is bad practice to expose db context this way but for the sake of this test, lets go this route
                using JokeDbContext context = new JokeDbContext();
                context.Jokes.AddRangeAsync(_jokes);
                context.SaveChanges();
                Console.WriteLine("Ughh!! No Jokes found.. Guess who the joke is on now eh ?");
                Console.ReadKey();
            }
        }
    }
}
