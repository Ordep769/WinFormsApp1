using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        //random



        List<Color> colorList = new List<Color>() { Color.Blue, Color.Red, Color.Green, Color.Yellow, Color.Gray };
        Random rand = new Random();

        //variáveis
        //propriedades
        //2 botões
        private Button btnIniciar;
        private Button btnAlvo;

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerTrocaCor;

        //timer
        private Random random;


        //stopWatch
        private Stopwatch stopwatch;


        public Form1()
        {
            InitializeComponent();

            //determina o titulo da tela 
            this.Text = "Reflexo";
            //altura da tela
            this.Size = new Size(400, 400);
            //determina os botões da tela
            this.StartPosition = FormStartPosition.CenterParent;
            btnIniciar = new Button()
            {
                Text = "iniciar",
                Size = new Size(100, 50)
            };
            btnIniciar.Click += IniciarJogo;
            //adiciona o botão na tela
            this.Controls.Add(btnIniciar);


            //btnalvo
            btnAlvo = new Button()
            {
                Size = new Size(50, 50),
                BackColor = colorList[rand.Next(0, colorList.Count - 1)],
                Visible = false,
            };
            btnAlvo.Click += btnAlvoClick;
            //adiciona o botão na tela, mas culto
            this.Controls.Add(btnAlvo);



            //timer
            timer = new System.Windows.Forms.Timer();
            timer.Tick += MostrarBotaoAlvo;

            timerTrocaCor = new System.Windows.Forms.Timer();
            timerTrocaCor.Interval = 5000;
            timerTrocaCor.Tick += MostrarBotaoAlvo;

            // random
            random = new Random();
            stopwatch = new Stopwatch();
        }

        //iniciar o jogo
        private void IniciarJogo(object sender, EventArgs e)
        {
            //desabilita o botão
            btnIniciar.Enabled = false;
            IniciarNovaRodada();

        }


        private void IniciarNovaRodada()
        {
            //determina um timer aleatorio entre 1 e 3 segundos
            timer.Interval = random.Next(1000, 3000);
            timerTrocaCor.Start();
            timer.Start();
            stopwatch.Restart();
        }
        private void MostrarBotaoAlvo(object sender, EventArgs e)
        {

            // parar o tempo no clicar o botao
            timer.Stop();
            //gera os valores aleatorios para a posicao btn alvo
            int x = random.Next(50, this.ClientSize.Width - 70);
            int y = random.Next(50, this.ClientSize.Height - 70);
            btnAlvo.BackColor = colorList[rand.Next(0, colorList.Count - 1)];
            btnAlvo.Location = new Point(x, y); // define a posiçao do btn alvo
            btnAlvo.Visible = true; // exibe o botao alvo na tela
            stopwatch.Restart(); // reiniciar o cronometro



        }



        // Ao clicar no botão alvo
        private void btnAlvoClick(object sender, EventArgs e)
        {
            stopwatch.Stop();
            btnAlvo.Visible = false;
            timerTrocaCor.Stop();
            if (btnAlvo.BackColor == Color.Blue)
            {
                MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds}ms", "Você acertouuuu!");

                Task.Delay(500)
                    .ContinueWith(t => IniciarNovaRodada(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                MessageBox.Show("Botão cor errada, seu burro");

                Task.Delay(500)
                    .ContinueWith(t => IniciarNovaRodada(),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }


        }
    }
}
