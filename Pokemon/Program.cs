﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<Pokemon> roster = new List<Pokemon>();
            //Charmander Moves
            Move fire_blast = new Move("Fire Blast");
            Move emnber = new Move("Ember");
            //Squirtle Moves
            Move bubble = new Move("Bubble");
            Move bite = new Move("Bite");
            //Bulabasaur Moves
            Move cut = new Move("Cut");
            Move mega_drain = new Move("Mega Drain");
            Move razor_leaf = new Move("Razor Leaf");
            //Make movelists for the pokemons
            List<Move> charmanderMoveSet = new List<Move> {fire_blast,emnber};
            List<Move> squirtleMoveSet = new List<Move> { bubble, bite};
            List<Move> bulbasaurMoveSet = new List<Move> { cut, mega_drain,razor_leaf };
            //Initialize the different pokemons
            Pokemon charmander = new Pokemon("Charmander", 3, 52, 43, 39, Elements.Fire, charmanderMoveSet);
            Pokemon squirtle = new Pokemon("Squirtle", 2, 48, 65, 44, Elements.Water, squirtleMoveSet);
            Pokemon bulbasaur = new Pokemon("Bulbasaur", 3, 49, 49, 45, Elements.Grass, bulbasaurMoveSet);
            //Add pokemons to the rooster
            roster.Add(charmander);
            roster.Add(squirtle);
            roster.Add(bulbasaur);

            // INITIALIZE YOUR THREE POKEMONS HERE ** DONE
            Console.WriteLine("Welcome to the world of Pokemon!\nThe available commands are list/fight/heal/quit");

            while (true)
            {
                Console.WriteLine("\nPlease enter a command");
                switch (Console.ReadLine())
                {
                    case "list":
                        //PRINT THE POKEMONS IN THE ROSTER HERE ** DONE
                        Console.WriteLine("\nThe roster of pokemon includes the following: ");
                        foreach (Pokemon pokemon in roster)
                        {
                            Console.WriteLine(pokemon.Name + " : " + pokemon.Hp + " HP");
                        }
                        break;

                    case "fight":
                        //PRINT INSTRUCTIONS AND POSSIBLE POKEMONS (SEE SLIDES FOR EXAMPLE OF EXECUTION) ** DONE
                        Console.WriteLine("\nWrite you pokemon first, then a space before the pokemon you wish to battle. Example: 'charmander squirtle'. Same pokemon not possible.");
                        Console.Write("Choose who should fight:\n" );
                        for (int i = 0; i < roster.Count; i++)
                        {
                            Console.WriteLine("- " + roster[i].Name);
                        }

                        //READ INPUT, REMEMBER IT SHOULD BE TWO POKEMON ** DONE
                        //BE SURE TO CHECK THE POKEMON NAMES THE USER WROTE ARE VALID (IN THE ROSTER) AND IF THEY ARE IN FACT 2! ** DONE
                        Pokemon player = null;
                        Pokemon enemy = null;

                        bool enemyChosen = false;
                        bool playerChosen = false;
                        do
                        {
                            string input = Console.ReadLine();
                            string[] splittetInput = input.Split(' ');
                            if (splittetInput.Length == 2)
                            {
                                for (int i = 0; i < roster.Count; i++)
                                {
                                    if (splittetInput[0].Equals(roster[i].Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        player = roster[i];
                                        playerChosen = true;
                                    }
                                    if (splittetInput[1].Equals(roster[i].Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        enemy = roster[i];
                                        enemyChosen = true;
                                    }
                                }
                            }
                            if (!enemyChosen || !playerChosen || splittetInput.Length > 2 || splittetInput.Length < 2)
                            {
                                Console.WriteLine("Please enter 2 valid pokemons (remember to spellcheck): ");
                            }
                        }
                        while (!enemyChosen && !playerChosen);

                        //if everything is fine and we have 2 pokemons let's make them fight
                        if (player != null && enemy != null && player != enemy)
                        {
                            Console.WriteLine("\nYour have choosen: "+player.Name);
                            Console.WriteLine("A wild " + enemy.Name + " appears!");
                            Console.Write(player.Name + " I choose you! ");

                            //BEGIN FIGHT LOOP
                            while (player.Hp > 0 && enemy.Hp > 0)
                            {
                                //PRINT POSSIBLE MOVES ** DONE
                                Console.WriteLine("\nWhat move should we use?: ");
                                foreach (Move moves in player.Moves)
                                {
                                    Console.WriteLine("- " + moves.Name);
                                }

                                //GET USER ANSWER, BE SURE TO CHECK IF IT'S A VALID MOVE, OTHERWISE ASK AGAIN ** DONE
                                Move playerMove = null;
                                bool moveChosen = false;
                                do
                                {
                                    string whatToUse = Console.ReadLine();

                                    for (int i = 0; i < player.Moves.Count; i++)
                                    {
                                        if (whatToUse.Equals(player.Moves[i].Name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            playerMove = player.Moves[i];
                                            moveChosen = true;
                                        } 
                                    }
                                    if (!moveChosen) Console.WriteLine("Please choose a valid move!!");
                                } 
                                while (!moveChosen);

                                //CALCULATE AND APPLY DAMAGE ** DONE
                                Console.WriteLine("\nEnemy hp before: "+enemy.Hp);
                                int damage = player.Attack(enemy);

                                //print the move and damage
                                Console.WriteLine(player.Name + " uses " + playerMove.Name + ". " + enemy.Name + " loses " + damage + " HP" + "\nEnemy hp after attack: " + enemy.Hp);

                                //if the enemy is not dead yet, it attacks
                                if (enemy.Hp > 0)
                                {
                                    //CHOOSE A RANDOM MOVE BETWEEN THE ENEMY MOVES AND USE IT TO ATTACK THE PLAYER ** DONE
                                    Random rand = new Random();
                                    /*the C# random is a bit different than the Unity random
                                     * you can ask for a number between [0,X) (X not included) by writing
                                     * rand.Next(X) 
                                     * where X is a number 
                                     */
                                    int enemyMove = rand.Next(0,enemy.Moves.Count);
                                    Console.WriteLine("Player hp before: " + player.Hp);
                                    int enemyDamage = enemy.Attack(player);

                                    //print the move and damage
                                    Console.WriteLine(enemy.Name + " uses " + enemy.Moves[enemyMove].Name + ". " + player.Name + " loses " + enemyDamage + " HP"+ "\nPlayer hp after attack: " + player.Hp);
                                }
                            }
                            //The loop is over, so either we won or lost
                            if (enemy.Hp <= 0)
                            {
                                Console.WriteLine(enemy.Name + " faints, you won!");
                            }
                            else
                            {
                                Console.WriteLine(player.Name + " faints, you lost...");
                            }
                        }
                        //otherwise let's print an error message
                        else
                        {
                            Console.WriteLine("Invalid pokemons");
                        }
                        break;

                    case "heal":
                        
                        //RESTORE ALL POKEMONS IN THE ROSTER ** DONE
                    foreach (Pokemon pokemon in roster)
                        {
                            pokemon.Restore();
                        }
                        Console.WriteLine("\nAll pokemons have been healed");
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
