using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Four
{
    public partial class Form1 : Form
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private Button[,] buttons;
        private char[,] board;
        private char currentPlayer;
        public Form1()
        {
            InitializeComponent();
            InitializeBoard();
        }
        private void InitializeBoard()
        {
            buttons = new Button[Rows, Cols];
            board = new char[Rows, Cols];
            currentPlayer = 'X';

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    buttons[row, col] = new Button
                    {
                        Size = new System.Drawing.Size(50, 50),
                        Location = new System.Drawing.Point(col * 55, row * 55),
                        Tag = new Tuple<int, int>(row, col)
                    };
                    buttons[row, col].Click += button1_Click;
                    this.Controls.Add(buttons[row, col]);
                    board[row, col] = ' ';
                }
            }
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var (row, col) = (Tuple<int, int>)button.Tag;



            for (int r = Rows - 1; r >= 0; r--)
            {
                if (board[r, col] == ' ')
                {
                    board[r, col] = currentPlayer;
                    buttons[r, col].Text = currentPlayer.ToString();
                    buttons[r, col].Enabled = false;

                    if (CheckWin(r, col))
                    {
                        MessageBox.Show($"Player {currentPlayer} wins!");
                        ResetGame();
                        return;
                    }

                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    break;
                }
            }
        }
        private bool CheckWin(int row, int col)
        {
            // Проверка по горизонтали
            int count = 0;
            for (int c = 0; c < Cols; c++)
            {
                if (board[row, c] == currentPlayer)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Проверка по вертикали
            count = 0;
            for (int r = 0; r < Rows; r++)
            {
                if (board[r, col] == currentPlayer)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Проверка по диагонали (слева направо)
            count = 0;
            int startRow = row - Math.Min(row, col);
            int startCol = col - Math.Min(row, col);
            for (int r = startRow, c = startCol; r < Rows && c < Cols; r++, c++)
            {
                if (board[r, c] == currentPlayer)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }

            // Проверка по диагонали (справа налево)
            count = 0;
            startRow = row + Math.Min(Rows - 1 - row, col);
            startCol = col - Math.Min(Rows - 1 - row, col);
            for (int r = startRow, c = startCol; r >= 0 && c < Cols; r--, c++)
            {
                if (board[r, c] == currentPlayer)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }

            return false;
        }

        private void ResetGame()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    board[row, col] = ' ';
                    buttons[row, col].Text = "";
                    buttons[row, col].Enabled = true;
                }
            }
            currentPlayer = 'X';
        }
    }
}

