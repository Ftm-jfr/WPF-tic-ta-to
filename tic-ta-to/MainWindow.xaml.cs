using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tic_toc_to
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Tic_Tac_To : Window
    {    
        Player player1 = new Player() { _name = "Player 1", _symbol = 'X', _isWin = false, _isTurn = true };
        Player player2 = new Player() { _name = "Player 2", _symbol = 'O', _isWin = false, _isTurn = false };
        public Tic_Tac_To()
        {
            InitializeComponent();
            InitializeGameBoard();
            Turn.Text = "Player1 Turn";

        }

        public class Player
        {
            public string _name { get; set; }
            public char _symbol { get; set; }
            public bool _isWin { get; set; }
            public bool _isTurn { get; set; }

        }

        private void Start(object sender, RoutedEventArgs e)
        {
            Tic_Tac_To newWindow = new Tic_Tac_To();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
            player1 = new Player();
            player2= new Player();
        }
        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            Button b = (Button)sender;
            
            if(player1._isTurn && b.IsEnabled)
            {
                b.Content = player1._symbol;
                player1._isTurn = false;
                player2._isTurn = true;
                Turn.Text = "Player2 Turn";
                b.IsEnabled = false;
                if (IsTie())
                {
                    MessageBox.Show("Sorry,Game Tied");
                    Start(sender, e);
                }
                if (IsWin(player1))
                {
                    MessageBox.Show("Player1 Wins");
                    Start(sender, e);
                }
            }
            else if(player2._isTurn && b.IsEnabled)
            {
                b.Content = player2._symbol;
                player2._isTurn = false;
                player1._isTurn = true;
                Turn.Text = "Player1 Turn";
                b.IsEnabled = false;
                if (IsTie())
                {
                    MessageBox.Show("Sorry,Game Tied Up");
                    Start(sender, e);
                }
                if (IsWin(player2))
                {
                    MessageBox.Show("Player2 Wins");
                    Start(sender, e);
                }
            }
        }

        private Button[][] board = new Button[3][];
        private bool IsTie()
        {
            // Check if all buttons have been clickedfor
            for(int i = 0; i < 3; i++)
            {
                foreach (Button button in board[i])
                {
                    if (button.Content.ToString() != "X" && button.Content.ToString() != "O")
                    {
                        return false; // Game is not a tie
                    }
                }
            }

            return true; // Game is a tie
        }

        private void InitializeGameBoard()
        {
            board[0] = new Button[3] { b1, b2, b3 };
            board[1] = new Button[3] { b4, b5, b6};
            board[2] = new Button[3] { b7, b8, b9 };
        }

        private bool IsWin(Player player)
        {
            // Check rows
            foreach (var row in board)
            {
                if (row.All(b => b.Content.ToString() == player._symbol.ToString()))
                {
                    return true;
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                for(int j=0;j< 3; j++)
                {
                    if (board[j][i].Content.ToString() == player._symbol.ToString())
                    {
                        if(j==2)
                            return true;
                    }
                    else { break; }
                }
            }

            // Check diagonals
            if ((board[0][0].Content.ToString() == player._symbol.ToString() &&
                 board[1][1].Content.ToString() == player._symbol.ToString() &&
                 board[2][2].Content.ToString() == player._symbol.ToString()) 
                 ||
                (board[0][2].Content.ToString() == player._symbol.ToString() &&
                 board[1][1].Content.ToString() == player._symbol.ToString() &&
                 board[2][0].Content.ToString() == player._symbol.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
