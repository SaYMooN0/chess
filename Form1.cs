using System.Drawing;

namespace chess
{
    public partial class Form1 : Form
    {
        bool[,] board = new bool[8, 8] {
            { true, false, true, false, true, false, true, false },
            { false, true, false, true, false, true, false, true},
            { true, false, true, false, true, false, true, false },
            { false, true, false, true, false, true, false, true },
            { true, false, true, false, true, false, true, false },
            { false, true, false, true, false, true, false, true },
            { true, false, true, false, true, false, true, false },
            { false, true, false, true, false, true, false, true }
        };
        Figure[,] bWithFigures = new Figure[8, 8] {
            { new Figure(Types.rook,false), new Figure(Types.knight,false), new Figure(Types.bishop,false), new Figure(Types.queen,false), new Figure(Types.king,false), new Figure(Types.bishop,false), new Figure(Types.knight,false), new Figure(Types.rook,false)},
            { new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false),new Figure(Types.pawn,false)},
            { new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure()},
            { new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure()},
            { new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure()},
            { new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure(),new Figure()},
            { new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true), new Figure(Types.pawn,true)},
            { new Figure(Types.rook,true), new Figure(Types.knight,true), new Figure(Types.bishop,true), new Figure(Types.queen,true), new Figure(Types.king,true), new Figure(Types.bishop,true), new Figure(Types.knight,true), new Figure(Types.rook,true)}
        };
        int cellSize;
        char[] a = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        enum Types { king, queen, rook, bishop, knight, pawn, nothing }
        class Figure
        {
            public Types type;
            public bool isWhite;
            public Figure(Types type, bool isWhite)
            {
                this.type = type;
                this.isWhite = isWhite;
            }
            public Figure()
            {
                this.type = Types.nothing;
            }
        }
        public Form1()
        {
            InitializeComponent();
            cellSize = this.ClientSize.Height / 8;
            this.Paint += Form1_Paint;
            Label label1 = new Label();

        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            printBackground();
            paintAll();
        }

        private void printBackground()
        {
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle rect = new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize);
                    SolidBrush b;
                    if (board[i, j] == true)
                        b = new SolidBrush(Color.Wheat);
                    else
                        b = new SolidBrush(Color.Sienna);
                    g.FillRectangle(b, rect);
                }
            }
            g.Dispose();

        }
        private void paintAll()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    paintFigure(i, j);
            }
        }
        private void paintFigure(int i, int j)
        {
            if (bWithFigures[i, j].type == Types.nothing)
                return;
            else
            {
                Graphics g = this.CreateGraphics();
                Bitmap myBitmap = new Bitmap (getPath(i, j));
                g.DrawImage(myBitmap, j* cellSize, i* cellSize);
                g.Dispose();
            }
        }
        private string getPath(int i, int j)
        {
            string path;
            if (bWithFigures[i, j].isWhite == true) path = "L";
            else path = "B";
            path += bWithFigures[i, j].type.ToString();
            path += ".bmp";
            return path;

        }
        private void figureMenu(Figure f, int x, int y)
        {
            Controls.Clear();
            Label label1 = new Label()
            {
                Text = $"Type: {f.type}\n",
                TabIndex = 1000,
                Height = 80,
                Width = 80,
                BackColor = Color.SandyBrown
            };
            if (f.isWhite)
                label1.Text += "Color: white\n";
            else
                label1.Text += "Color: black\n";
            label1.Text += $"\n Position={8-y/80},{a[(int)x/80]}";
            label1.Location = new Point((int)(x/80)*80, (int)(y / 80) * 80);
            Controls.Add(label1);

        }
        private void showNothing(int x, int y)
        {
            Controls.Clear();
            Label label1 = new Label()
            {
                Text = $"Position={8-y/80},{a[(int)x/80]}",
                TabIndex = 1000,
                Height = 20,
                Width = 80,
                Location = new Point((int)(x / 80) * 80, (int)(y / 80) * 80),
                BackColor = Color.SandyBrown
            };
            Controls.Add(label1);
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bWithFigures[(int)e.Y / 80, (int)e.X / 80].type != Types.nothing)
                figureMenu(bWithFigures[(int)e.Y / 80, (int)e.X / 80], e.X, e.Y);
            else
                showNothing(e.X,e.Y);
                
        }
    }
}