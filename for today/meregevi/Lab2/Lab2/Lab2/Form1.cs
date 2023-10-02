using Lab2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Lab2
{
    public partial class Form1 : Form
    {
        const int mapSize = 8;
        const int cellSize = 60;
        int ScoreBlack = 0;
        int ScoreWhite = 0;
        int ScoreMatches = 0;
        public bool tournament = false;

        int currentPlayer;
        int player = 2;
        List<PictureBox> simpleSteps = new List<PictureBox>();

        int countEatSteps = 0;
        PictureBox prevButton;
        PictureBox pressedButton;
        bool isContinue = false;

        bool isMoving;

        int[,] map = new int[mapSize, mapSize];

        PictureBox[,] pictures = new PictureBox[mapSize, mapSize];

        Image whiteFigure;
        Image blueFigure;


        public Form1()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit); 
            InitializeComponent();
            OnServer.Value = false;
            whiteFigure = Resources.white;
            blueFigure = Resources.Blue;


            this.Text = "Checkers";

            Init();
        }
        public void Tournament(bool tournament)
        {
            if (tournament)
            {
                if (ScoreWhite == 1)
                {
                    tournament = false;
                    MessageBox.Show($"End of Tournament\n" +
                        $"White         Score:{ScoreWhite}\n" +
                        $"Black         Score:{ScoreBlack}");
                }
                if (ScoreBlack == 1)
                {
                    tournament = false;
                    MessageBox.Show($"End of Tournament\n" +
                        $"Black         Score:{ScoreBlack}\n" +
                        $"White         Score:{ScoreWhite}");
                }
            }
        }
        public void RefreshScore()
        {
            lbScoreWhite.Text = ScoreWhite.ToString();
            lbScoreBlack.Text = ScoreBlack.ToString();
            lbScoreMatches.Text = ScoreMatches.ToString() + " matches";
        }
        public void Init()
        {

            currentPlayer = 1;
            isMoving = false;
            prevButton = null;

            map = new int[mapSize, mapSize] {
                { 0,1,0,1,0,1,0,1 },
                { 1,0,1,0,1,0,1,0 },
                { 0,1,0,1,0,1,0,1 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 2,0,2,0,2,0,2,0 },
                { 0,2,0,2,0,2,0,2 },
                { 2,0,2,0,2,0,2,0 }
            };

            CreateMap();
        }
        public void ResetAllGame()
        {            
            ScoreBlack = 0;
            ScoreWhite = 0;
            ScoreMatches = 0;

            RefreshScore();
            this.Controls.Clear();
            InitializeComponent();
            Init();
        }


        public void ResetGame()
        {
            bool player1 = false;
            bool player2 = false;


            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == 1)
                        player1 = true;
       
                    if (map[i, j] == 2)
                        player2 = true;
                }
            }
            if (!player1)
            {
                ScoreBlack++;
                MessageBox.Show("Black win");
            }
            if (!player2)
            {
                ScoreWhite++;
                MessageBox.Show("White win");
            }
            if (!player1 || !player2)
            {
                ScoreMatches++;               
                this.Controls.Clear();               
                InitializeComponent();
                MessageBox.Show("End of game");
                Init();
                RefreshScore();
                Tournament(tournament);
            }
        }
        PictureBox picture;
        public void CreateMap()
        {
            //this.Width = (mapSize + 1) * cellSize;
            //this.Height = (mapSize + 1) * cellSize;






            this.ClientSize = new System.Drawing.Size(949, 480);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                     picture = new PictureBox();

                    picture.Location = new Point(j * cellSize, i * cellSize);

                    picture.Size = new Size(cellSize, cellSize);
                    picture.Click += new EventHandler(OnFigurePress);
                   
                    if (map[i, j] == 1)
                    {
                        picture.Image = whiteFigure;
                        picture.SizeMode = PictureBoxSizeMode.CenterImage;

                    }

                    else if (map[i, j] == 2)
                    {
                        picture.Image = blueFigure;
                        picture.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                    picture.BackColor = GetPrevButtonColor(picture);
                    picture.ForeColor = Color.Red;
                    picture.SizeMode = PictureBoxSizeMode.CenterImage;
                    pictures[i, j] = picture;

                    this.Controls.Add(picture);
                }
            }
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;

            ResetGame();
        }

        public Color GetPrevButtonColor(PictureBox prevButton)
        {
            if ((prevButton.Location.Y / cellSize % 2) != 0)
            {
                if ((prevButton.Location.X / cellSize % 2) == 0)
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
            if ((prevButton.Location.Y / cellSize) % 2 == 0)
            {
                if ((prevButton.Location.X / cellSize) % 2 != 0)
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
            return Color.FromArgb(128, 0, 0);
        }
        string str = "";
        bool flag = false;
        public void OnFigurePress(object sender, EventArgs e)
        {
            if (flag)
            {
                if (prevButton != null)
                    prevButton.BackColor = GetPrevButtonColor(prevButton);

                pressedButton = sender as PictureBox;

                if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] != 0 && (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == currentPlayer && player == currentPlayer))
                {
                    CloseSteps();
                    pressedButton.BackColor = Color.Red;
                    DeactivateAllButtons();
                    pressedButton.Enabled = true;
                    countEatSteps = 0;
                    if (pressedButton.Text == "D")
                        ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                    else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);

                    if (isMoving)
                    {
                        CloseSteps();
                        pressedButton.BackColor = GetPrevButtonColor(pressedButton);
                        ShowPossibleSteps();
                        isMoving = false;
                    }
                    else
                        isMoving = true;



                }
                else
                {
                    if (isMoving)
                    {
                        isContinue = false;
                        if (Math.Abs(pressedButton.Location.X / cellSize - prevButton.Location.X / cellSize) > 1)
                        {
                            isContinue = true;
                            DeleteEaten(pressedButton, prevButton);

                        }

                        //string [] a = str.Split(' ');
                        //if (a[0] != "Delet")
                        str = "mov" + " ";
                        str += Convert.ToString(pressedButton.Location.Y / cellSize) + " ";
                        str += Convert.ToString(pressedButton.Location.X / cellSize) + " ";
                        str += Convert.ToString(prevButton.Location.Y / cellSize) + " ";
                        str += Convert.ToString(prevButton.Location.X / cellSize) + " ";
                      
                        int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                        map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];

                        map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                        pressedButton.Image = prevButton.Image;
                        prevButton.Image = null;
                        pressedButton.Text = prevButton.Text;
                        prevButton.Text = "";
                        SwitchButtonToCheat(pressedButton);
                        countEatSteps = 0;
                        isMoving = false;
                        CloseSteps();
                        DeactivateAllButtons();
                        if (pressedButton.Text == "D")
                            ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                        else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
                        if (countEatSteps == 0 || !isContinue)
                        {
                            CloseSteps();
                            SwitchPlayer();
                            ShowPossibleSteps();
                            isContinue = false;
                        }
                        else if (isContinue)
                        {
                            pressedButton.BackColor = Color.Red;
                            pressedButton.Enabled = true;
                            isMoving = true;
                        }
                        str += Convert.ToString(currentPlayer);
                        SendMessageMap(str);
                        str = "";
                    }

                }

                prevButton = pressedButton;
            }
        }
        private void SendMessageMap(string txtMessage)
        {
           
                swSender.WriteLine(txtMessage); ;
                swSender.Flush();
            
        }

        public void ShowPossibleSteps()
        {
            bool isOneStep = true;
            bool isEatStep = false;
            DeactivateAllButtons();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == currentPlayer)
                    {
                        if (pictures[i, j].Text == "D")
                            isOneStep = false;
                        else isOneStep = true;
                        if (IsButtonHasEatStep(i, j, isOneStep, new int[2] { 0, 0 }))
                        {
                            isEatStep = true;
                            pictures[i, j].Enabled = true;
                        }
                    }
                }
            }
            if (!isEatStep)
                ActivateAllButtons();
        }

        public void SwitchButtonToCheat(PictureBox picture)
        {
            if (map[picture.Location.Y / cellSize, picture.Location.X / cellSize] == 1 && picture.Location.Y / cellSize == mapSize - 1)
            {
                picture.Text = "D";
                picture.Image = Resources.whiteDamka;

            }
            if (map[picture.Location.Y / cellSize, picture.Location.X / cellSize] == 2 && picture.Location.Y / cellSize == 0)
            {
                picture.Text = "D";
                picture.Image = Resources.blueDamka;

            }
        }

        public void DeleteEaten(PictureBox endButton, PictureBox startButton)
        {
            int count = Math.Abs(endButton.Location.Y / cellSize - startButton.Location.Y / cellSize);
            int startIndexX = endButton.Location.Y / cellSize - startButton.Location.Y / cellSize;
            int startIndexY = endButton.Location.X / cellSize - startButton.Location.X / cellSize;
            startIndexX = startIndexX < 0 ? -1 : 1;
            startIndexY = startIndexY < 0 ? -1 : 1;
            int currCount = 0;
            int i = startButton.Location.Y / cellSize + startIndexX;
            int j = startButton.Location.X / cellSize + startIndexY;
            while (currCount < count - 1)
            {
                map[i, j] = 0;
                pictures[i, j].Image = null;
                pictures[i, j].Text = "";
                i += startIndexX;
                j += startIndexY;
                currCount++;
            }

        }

        public void ShowSteps(int iCurrFigure, int jCurrFigure, bool isOnestep = true)
        {
            simpleSteps.Clear();
            ShowDiagonal(iCurrFigure, jCurrFigure, isOnestep);
            if (countEatSteps > 0)
                CloseSimpleSteps(simpleSteps);
        }

        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)
        {
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

        public bool DeterminePath(int ti, int tj)
        {

            if (map[ti, tj] == 0 && !isContinue)
            {
                pictures[ti, tj].BackColor = Color.Yellow;
                pictures[ti, tj].Enabled = true;
                simpleSteps.Add(pictures[ti, tj]);
            }
            else
            {

                if (map[ti, tj] != currentPlayer)
                {
                    if (pressedButton.Text == "D")
                        ShowProceduralEat(ti, tj, false);
                    else ShowProceduralEat(ti, tj);
                }

                return false;
            }
            return true;
        }

        public void CloseSimpleSteps(List<PictureBox> simpleSteps)
        {
            if (simpleSteps.Count > 0)
            {
                for (int i = 0; i < simpleSteps.Count; i++)
                {
                    simpleSteps[i].BackColor = GetPrevButtonColor(simpleSteps[i]);
                    simpleSteps[i].Enabled = false;
                }
            }
        }
        public void ShowProceduralEat(int i, int j, bool isOneStep = true)
        {
            int dirX = i - pressedButton.Location.Y / cellSize;
            int dirY = j - pressedButton.Location.X / cellSize;
            dirX = dirX < 0 ? -1 : 1;
            dirY = dirY < 0 ? -1 : 1;
            int il = i;
            int jl = j;
            bool isEmpty = true;
            while (IsInsideBorders(il, jl))
            {
                if (map[il, jl] != 0 && map[il, jl] != currentPlayer)
                {
                    isEmpty = false;
                    break;
                }
                il += dirX;
                jl += dirY;

                if (isOneStep)
                    break;
            }
            if (isEmpty)
                return;
            List<PictureBox> toClose = new List<PictureBox>();
            bool closeSimple = false;
            int ik = il + dirX;
            int jk = jl + dirY;
            while (IsInsideBorders(ik, jk))
            {
                if (map[ik, jk] == 0)
                {
                    if (IsButtonHasEatStep(ik, jk, isOneStep, new int[2] { dirX, dirY }))
                    {
                        closeSimple = true;
                    }
                    else
                    {
                        toClose.Add(pictures[ik, jk]);
                    }
                    pictures[ik, jk].BackColor = Color.Yellow;
                    pictures[ik, jk].Enabled = true;
                    countEatSteps++;
                }
                else break;
                if (isOneStep)
                    break;
                jk += dirY;
                ik += dirX;
            }
            if (closeSimple && toClose.Count > 0)
            {
                CloseSimpleSteps(toClose);
            }

        }

        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)
        {
            bool eatStep = false;
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (dir[0] == 1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j + 1))
                            eatStep = false;
                        else if (map[i - 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            eatStep = false;
                        else if (map[i - 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j - 1))
                            eatStep = false;
                        else if (map[i + 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j + 1))
                            eatStep = false;
                        else if (map[i + 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
            return eatStep;
        }

        public void CloseSteps()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].BackColor = GetPrevButtonColor(pictures[i, j]);
                }
            }
        }

        public bool IsInsideBorders(int ti, int tj)
        {
            if (ti >= mapSize || tj >= mapSize || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public void ActivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].Enabled = true;
                }
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    pictures[i, j].Enabled = false;
                }
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private string UserName = "Unknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;
   private delegate void UpdateLogCallback(string strMessage);
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;

        public int port { get; set; }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Connected == false)
            {
                InitializeConnection();
            }
            else
            {
                CloseConnection("Disconnected at user's request.");
            }
        }


        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void InitializeConnection()
        {

            ipAddr = IPAddress.Parse(txtIp.Text);



            port = Convert.ToInt32(Port.Text);
            tcpServer = new TcpClient();
            tcpServer.Connect(ipAddr, port);

            Connected = true;
            UserName = txtUser.Text;

            txtIp.Enabled = false;
            txtUser.Enabled = false;
            btnConnect.Text = "Disconnect";

            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(txtUser.Text);
            swSender.Flush();

            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();
        }

        private void ReceiveMessages()
        {
            srReceiver = new StreamReader(tcpServer.GetStream());
            string ConResponse = srReceiver.ReadLine();
            if (ConResponse[0] == '1')
            {
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else 
            {
                string Reason = "Not Connected: ";
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                return;
            }
            while (Connected)
            {
                try
                {
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            }
        }
        private void UpdateLog(string strMessage)
        {
            string[] str = strMessage.Split(' ');
            if (str[0] != "mov" && str[0] != "Delet")
            {
                txtLog.AppendText(strMessage + "\r\n");
                flag = true;
            }
            else
            {
           
                    prevButton = pictures[int.Parse(str[3]), int.Parse(str[4])];

                    Mov(pictures[int.Parse(str[1]), int.Parse(str[2])], str);
      
                
            }
     
        }

        public void Mov(object sender, string[] str)
        {
            if (prevButton != null)
                prevButton.BackColor = GetPrevButtonColor(prevButton);

            pressedButton = sender as PictureBox;

          
                    isContinue = false;
                    if (Math.Abs(pressedButton.Location.X / cellSize - prevButton.Location.X / cellSize) > 1)
                    {
                        isContinue = true;
                        DeleteEaten(pressedButton, prevButton);
                    }


                    int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                    map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];

                    map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                    pressedButton.Image = prevButton.Image;
                    prevButton.Image = null;
                    pressedButton.Text = prevButton.Text;
                    prevButton.Text = "";
                    SwitchButtonToCheat(pressedButton);
                    countEatSteps = 0;
                    isMoving = false;
                    CloseSteps();
                    DeactivateAllButtons();
            if (pressedButton.Text == "D")
                ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
            else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
            if (countEatSteps == 0 || !isContinue)
            {
                CloseSteps();
                SwitchPlayer();
                ShowPossibleSteps();
                isContinue = false;
            }
            else if (isContinue)
            {
                pressedButton.BackColor = Color.Red;
                pressedButton.Enabled = true;
                isMoving = true;
            }
        }
        private void CloseConnection(string Reason)
        {
            txtLog.AppendText(Reason + "Interrupted\r\n");
            txtIp.Enabled = true;
            txtUser.Enabled = true;
            btnConnect.Text = "Connect";
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }


        private void OnServer_OnValueChange(object sender, EventArgs e)
        {
            if (OnServer.Value == true)
            {
                player = 1;
                IPAddress ipAddr = IPAddress.Parse(txtIp.Text);
                port = Convert.ToInt32(Port.Text);
                Server mainServer = new Server(ipAddr, port);               
                Server.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                mainServer.StartListening();
            }
        }


        private delegate void UpdateStatusCallback(string strMessage);


        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }


        private void UpdateStatus(string strMessage)
        {
            txtLog.AppendText("\r");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ResetAllGame();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ResetAllGame();
            tournament = true;
        }
    }

}
