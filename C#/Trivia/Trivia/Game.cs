using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        // Crée une liste pour les joueurs
        private readonly List<Player> _player = new List<Player>();

        // Nombre de joueurs maximum, nombre de questions et nombre de joueurs minimum pour commencer le jeu
        private static readonly int nbplayer = 6;
        private static readonly int nbquestion = 50;
        private static readonly int nbMinPlayer = 2;

        // Crée des tableaux pour les places et les purses de chaque joueur
        private readonly int[] _places = new int[nbplayer];
        private readonly int[] _purses = new int[nbplayer];

        // Crée un tableau pour les joueurs qui on une penalité
        private readonly bool[] _inPenaltyBox = new bool[6];

        // Crée des listes pour les questions dans chaque catégorie
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        // Numéro du joueur et s'il y n'a plus de pénalité
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;



        public Game()
        {
            // Pour chaque question, ajouter une question à chaque liste
            for (var i = 0; i < nbquestion; i++)
            {
                _popQuestions.AddLast(CreateQuestion("Pop",i));
                _scienceQuestions.AddLast((CreateQuestion("science",i)));
                _sportsQuestions.AddLast((CreateQuestion ("Sports", i)));
                _rockQuestions.AddLast(CreateQuestion("Rock", i));
            }
            string s = "";
            Player p = new Player("nom");
        }

        // Crée une question pour une catégorie donnée et un numéro de question donné
        private string CreateQuestion(string questionType, int questionNumber)
        {
            return questionType + " Question " + questionNumber;
        }

        // Vérifie si le jeu est jouable en fonction du nombre de joueurs
        public bool IsPlayable()
        {
            return _player.Count >= nbMinPlayer;
        }

        // Ajoute un joueur à la liste des joueurs
        public bool Add(string playerName)
        {
            Player p = new Player(playerName);
            _player.Add(p);
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _player.Count);
            return true;
        }

        // Effectue un lancé de dés pour le joueur
        public void Roll(int roll)
        {
            Console.WriteLine(_player[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_player[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    Console.WriteLine(_player[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_player[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                Console.WriteLine(_player[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        //Permet de poser une question en fonction de la catégorie actuelle du joueur
        private void AskQuestion()
        {

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
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        //Si le joueur est dans la case "Penalty Box", il vérifie s'il est autorisé à sortir. Si oui, il attribue une pièce d'or
        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(_player[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _player.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _player.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_player[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _player.Count) _currentPlayer = 0;

                return winner;
            }
        }

        //Place le joueur dans la case "Penalty Box" et passe au joueur suivant
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_player[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _player.Count) _currentPlayer = 0;
            return true;
        }

        //Vérifie si le joueur a gagné en ayant accumulé six pièces d'or
        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
