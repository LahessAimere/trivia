using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        Player player = new Player("noms");

        // Nombre de joueurs maximum, nombre de questions et nombre de joueurs minimum pour commencer le jeu
        private static readonly int MaxPlayers = 6;
        private static readonly int nbquestion = 50;
        private static readonly int nbMinPlayer = 2;

        private int nbOfPlayers = 0;

        // Crée des listes pour les questions dans chaque catégorie
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        public Game()
        {
            // Pour chaque question, ajouter une question à chaque liste
            for (var i = 0; i < nbquestion; i++)
            {
                _popQuestions.AddLast(CreateQuestion("Pop", i));
                _scienceQuestions.AddLast((CreateQuestion("science", i)));
                _sportsQuestions.AddLast((CreateQuestion("Sports", i)));
                _rockQuestions.AddLast(CreateQuestion("Rock", i));
            }
        }

        // Crée une question pour une catégorie donnée et un numéro de question donné
        private string CreateQuestion(string questionType, int questionNumber)
        {
            return questionType + " Question " + questionNumber;
        }


        // Ajoute un joueur à la liste des joueurs
        public bool Add(string playerName)
        {
            player.Name = playerName;
            nbOfPlayers++;
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + nbOfPlayers);
            return true;
        }

        // Effectue un lancé de dés pour le joueur
        public void Roll(int roll)
        {
            Console.WriteLine(player.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (player.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    player.IsGettingOutOfPenaltyBox = true;

                    Console.WriteLine(player.Name + " is getting out of the penalty box");
                    player.Place = player.Place + roll;
                    if (player.Place > 11) player.Place = player.Place - 12;

                    Console.WriteLine(player.Name
                            + "'s new location is "
                            + player.Place);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(player.Name + " is not getting out of the penalty box");
                    player.IsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                player.Place = player.Place + roll;
                if (player.Place > 11) player.Place = player.Place - 12;

                Console.WriteLine(player.Name
                        + "'s new location is "
                        + player.Place);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        //Permet de poser une question en fonction de la catégorie actuelle du joueur
        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {

            }

            switch (CurrentCategory())
            {
                case "Pop":
                    Console.WriteLine(_popQuestions.First());
                    _popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(_scienceQuestions.First());
                    _scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(_sportsQuestions.First());
                    _sportsQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(_rockQuestions.First());
                    _rockQuestions.RemoveFirst();
                    break;
                default:
                    Console.WriteLine("Unknown category.");
                    break;
            }
        }

        //Permet de déterminer la catégorie actuelle du joueur en fonction de sa position sur le plateau de jeu
        private string CurrentCategory()
        {
            switch (player.Place % 4)
            {
                case 0:
                    return "Pop";
                    break;

                case 1:
                    return "Science";
                    break;

                case 2:
                    return "Sports";
                    break;
     
            }
            return "Rock";
        }

        //Si le joueur est dans la case "Penalty Box", il vérifie s'il est autorisé à sortir. Si oui, il attribue une pièce d'or
        public bool WasCorrectlyAnswered()
        {
            if (player.InPenaltyBox)
            {
                if (player.InPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    player.Purse++;
                    Console.WriteLine(player.Name
                            + " now has "
                            + player.Purse
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    player = player.Nextplayer;

                    return winner;
                }
                else
                {
                    player = player.Nextplayer;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                player.Purse++;
                Console.WriteLine(player.Name
                        + " now has "
                        + player.Purse
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                player = player.Nextplayer;
                return winner;
            }
        }

        //Place le joueur dans la case "Penalty Box" et passe au joueur suivant
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(player.Name + " was sent to the penalty box");
            player.InPenaltyBox = true;

            player = player.Nextplayer;
            return true;
        }

        //Vérifie si le joueur a gagné en ayant accumulé six pièces d'or
        private bool DidPlayerWin()
        {
            return ! (player.Purse == 6);
        }
    }

}
