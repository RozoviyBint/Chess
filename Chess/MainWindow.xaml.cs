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
using ChessEngine.ViewModels;

namespace Chess
{

    public partial class MainWindow : Window
    {
        private readonly CurrentGame _currentGame = new CurrentGame();//Поля для чтения представляют такие поля класса или структуры,
                                                                      //значение которых нельзя изменить.
                                                                      //Таким полям можно присвоить значение либо при непосредственно
                                                                      //при их объявлении, либо в конструкторе.
                                                                      //В других местах программы присваивать значение таким полям нельзя,
                                                                      //можно только считывать их значение.
        public MainWindow()
        {
            InitializeComponent();
            CreatePromotionBox();
            DataContext = _currentGame;//DataContext является источником всех сущностей, отображаемых через соединение с базой данных.
                                       //Он отслеживает изменения, внесенные вами во все извлеченные сущности, и поддерживает «кэш удостоверений»,
                                       //который гарантирует, что сущности, извлеченные более одного раза, представлены с использованием одного и
                                       //того же экземпляра объекта.
        }

        private void ClickOnSquare(object sender, RoutedEventArgs e)//нажатие на клетку
        {
            Button b = sender as Button;//Object sender — это параметр под названием Sender, который содержит ссылку
                                        //на элемент управления/объект, вызвавший событие. Когда кнопка будет нажата,
                                        //будет запущен обработчик события btn_Click.

            int x = b.Name[1] - '0';//Возвращает или задает имя, обозначающее элемент.
            int y = b.Name[2] - '0';
            _currentGame.SelectSquare(x, y);
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            _currentGame.NewGame();
        }

        private void CreatePromotionBox()
        {
           PromotionBox.Items.Add("Queen");
            PromotionBox.Items.Add("Rook");
            PromotionBox.Items.Add("Bishop");
            PromotionBox.Items.Add("Knight");
        }

        private void HandlePromotion(object sender, RoutedEventArgs e)
        {
            _currentGame.ChangePromotionPiece(PromotionBox.SelectedItem.ToString());
        }
    }
}
