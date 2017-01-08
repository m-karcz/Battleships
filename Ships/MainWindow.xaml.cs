using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Ships
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            preparedBoard.DrawShips(this.humanCanvas);
            humanCanvas.MouseDown += MyBoardCanvas_MouseDown_Putting;
            humanCanvas.PreviewMouseMove += MyBoardCanvas_PreviewMouseMove;
            humanCanvas.MouseWheel += MyBoardCanvas_MouseWheel;
            clearButton.Click += ClearButton_Click;
            acceptButton.Click += AcceptButton_Click;
        }
        /// <summary>
        /// Runs game after preparations
        /// </summary>
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            clearButton.Visibility = Visibility.Hidden;
            acceptButton.Visibility = Visibility.Hidden;
            hints.Visibility = Visibility.Hidden;
            pcBoard.Visibility = Visibility.Visible;
            yourBoard.Content = "Your board";
            pcPlayer = new PCPlayer(preparedBoard);
            human = new Human(new PCBoard());
            human.Draw(humanCanvas);
            pcPlayer.Draw(pcCanvas);
            boardState = BoardState.Play;
            humanCanvas.MouseDown -= MyBoardCanvas_MouseDown_Putting;
            humanCanvas.MouseDown += MyBoardCanvas_MouseDown_MakingMove;
        }
        /// <summary>
        /// Action to make move in game
        /// </summary>
        private void MyBoardCanvas_MouseDown_MakingMove(object sender, MouseButtonEventArgs e)
        {
            if (boardState == BoardState.Play)
            {
                var hit = getBetterPoint(e, humanCanvas);
                if (human.IsHidden(hit))
                {
                    if (!human.MakeMove(hit))
                    {
                        human.Draw(humanCanvas);
                        command.Content = "PC turn";
                        pcPlayer.MakeMove();
                        pcPlayer.Draw(pcCanvas);
                        if (pcPlayer.IsWon())
                        {
                            boardState = BoardState.End;
                            command.Content = "Enemy won!";
                            return;
                        }
                    }else
                    {
                        human.Draw(humanCanvas);
                    }
                }
                if (human.IsWon())
                {
                    boardState = BoardState.End;
                    command.Content = "You won!";
                    return;
                }else
                {
                    command.Content = "Your turn";
                }
            }
        }
        /// <summary>
        /// Clears board in edit mode
        /// </summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            preparedBoard.Clear();
            preparedBoard.DrawShips(humanCanvas);
            acceptButton.IsEnabled = false;
        }
        /// <summary>
        /// Action to change size of putting ship
        /// </summary>
        private void MyBoardCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                preparedBoard.IncreaseSize();
            }
            else
            {
                preparedBoard.DecreaseSize();
            }
        }
        /// <summary>
        /// Helper function to convert WPF point to useful point for board
        /// </summary>
        private void MyBoardCanvas_MouseDown_Putting(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                preparedBoard.PutNewShips(getBetterPoint(e.GetPosition(humanCanvas)));
                preparedBoard.DrawShips(humanCanvas);
                if (preparedBoard.isReady())
                {
                    acceptButton.IsEnabled = true;
                }
            }
            else
            {
                preparedBoard.RightClick();

            }
        }
        private System.Drawing.Point getBetterPoint(System.Windows.Point worsePoint) => new System.Drawing.Point((int)worsePoint.X / 20, (int)worsePoint.Y / 20);
        /// <summary>
        /// Helper function to convert WPF point to useful point for board
        /// </summary>
        private System.Drawing.Point getBetterPoint(MouseButtonEventArgs e, UIElement element) => getBetterPoint(e.GetPosition(element));
        /// <summary>
        /// Action to show ships under cursor in edit mode
        /// </summary>
        private void MyBoardCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (boardState != BoardState.PuttingShips)
            {
                humanCanvas.PreviewMouseMove -= MyBoardCanvas_PreviewMouseMove;
                return;
            }
            var point = getBetterPoint(e.GetPosition(humanCanvas));
            if (point != cursorPosition)
            {
                cursorPosition = point;
                preparedBoard.MovePutting(cursorPosition);
                preparedBoard.DrawShips(this.humanCanvas);
            }

        }
        /// <summary>
        /// Remembers last point on board under cursor
        /// </summary>
        private System.Drawing.Point cursorPosition = new System.Drawing.Point(0, 0);
        /// <summary>
        /// Template of board given to PC player after accepting
        /// </summary>
        private PreparingBoard preparedBoard = new PreparingBoard();
        /// <summary>
        /// Enum used as simple state machine to determine state of board and game
        /// </summary>
        private BoardState boardState = BoardState.PuttingShips;
        /// <summary>
        /// Computer player
        /// </summary>
        private PCPlayer pcPlayer;
        /// <summary>
        /// Human player
        /// </summary>
        private Human human;
    }
}
