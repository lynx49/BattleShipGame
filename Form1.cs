using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace BattleShipGame
{
    public partial class Form1 : Form
    {
        //technical gameboards
        int[,] enemyBattleField = new int[10, 10];
        int[,] userBattleField = new int[10, 10];
        //visual gameboards
        TextBox[,] enemyGameBoxList = new TextBox[10, 10];
        TextBox[,] userGameBoxList = new TextBox[10, 10];
        //other necessary variables
        int roundCounter = 1;
        Boolean assumeEmpty, enemyBoxClick;
        //userType: 1.single player, 2.host, 3.client
        int randomX, randomY, xOrYShip, shipCounter, shipSize, enemyShipsLeft, userShipsLeft, attackValueX, attackValueY, userType;
        Random random = new Random();
        Boolean gameBoxesNotCreated = true;
        Boolean[] languageList = { true, false, false, false };
        char[] selectedSprites = { ' ', 'X', '€', '¤', 'Ü', '@' };
        int enemyShipColor, userShipColor = 0;
        String placementText;
        TcpListener listener;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        //Quits game, closes window
        private void quitGameButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //shows a text box with instructions how to play the game
        private void informationButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < languageList.Length; i++)
            {
                if (languageList[i])
                {
                    switch (i)
                    {
                        case 0:
                            MessageBox.Show("'Start' button starts the battleship game." + Environment.NewLine +
                                "1. You must place 1-18 vessels on your board and confirm in order to start attacking, you add them by clicking your boxes" + Environment.NewLine +
                                "2. You attack the enemy by selecting one of their boxes and clicking 'strike', they strike back and you can attack again. This will continue until there are no ships left on either board" + Environment.NewLine +
                                "3. If the game is too hard you can cheat with 'reveal a ship', it's use is obvious, you will not lose a round in this version " + Environment.NewLine +
                                "4. If you and/or your enemy run out of ships, you will win/lose/draw and the game board will be reset" + Environment.NewLine +
                                "5. In multiplayer you wait for your opponent, you can play again once they have made their move" + Environment.NewLine +
                                "6. Multiplayer only available in English", "How to play");
                            break;

                        case 1:
                            MessageBox.Show("Instruktioner finns bara på engelska", "Ej tillgängligt");
                            break;

                        case 2:
                            MessageBox.Show("Seulment des instructions en anglais", "Pas disponible");
                            break;

                        case 3:
                            MessageBox.Show("你改變語言成英文", "沒有");
                            break;
                    }
                }
            }
        }
        //Opens ship customization panel and adds ship sprites to textboxes
        private void customizeShipsButton_Click(object sender, EventArgs e)
        {
            eForeColorBox.Text = selectedSprites[2].ToString();
            uForeColorBox.Text = selectedSprites[4].ToString();
            customizeShipsPanel.Visible = true;
        }
        //Opens settings panel
        private void settingsButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = true;
        }
        //Closes customise ships panel
        private void csHomeButton_Click(object sender, EventArgs e)
        {
            customizeShipsPanel.Visible = false;
        }
        //Closes settings panel
        private void sHomeButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = false;
        }

        //language confirm button, checks which language is selected when pressing the button
        private void yLanguageButton_Click(object sender, EventArgs e)
        {
            //creates a variable to check radio buttons, goes through the radio buttons until a match is found
            RadioButton radioBtn = this.settingsPanel.Controls.OfType<RadioButton>().Where(x => x.Checked).FirstOrDefault();
            
            //if a match is found / radio button is checked
            if(radioBtn != null)
            {
                switch (radioBtn.Name)
                {
                    //the four different interface languages
                    case "rButtonEnglish":
                        setEnglish();
                        break;

                    case "rButtonSwedish":
                        setSwedish();
                        break;

                    case "rButtonFrench":
                        setFrench();
                        break;

                    case "rButtonMandarin":
                        setMandarin();
                        break;
                }
            }
        }
        //changes all text to English
        public void setEnglish()
        {
            yLanguageButton.Text = "Confirm";
            languageLabel.Text = "Select language";
            startGameButton.Text = "Start";
            customizeShipsButton.Text = "Customize ships";
            settingsButton.Text = "Settings";
            quitGameButton.Text = "Quit";
            informationButton.Text = "How to play";
            changeShipLabel.Text = "Change ship appearance";
            eShipLabel.Text = "Enemy ship";
            eCharLabel.Text = "Shape";
            eChangeCharButton.Text = "Change";
            eForeColorLabel.Text = "Ship color";
            eSelectForeColorButton.Text = "Select";
            uShipLabel.Text = "Your ship";
            uCharLabel.Text = "Shape";
            uChangeCharButton.Text = "Change";
            uForeColorLabel.Text = "Ship color";
            uSelectForeColorButton.Text = "Select";
            attackButton.Text = "Strike!";
            roundLabel.Text = "Round:";
            eBattleFieldLabel.Text = "Enemy battlefield";
            uBattleFieldLabel.Text = "Your battlefield";
            enemyShipsLeftLabel.Text = "Ships left:";
            userShipsLeftLabel.Text = "Ships left:";
            placementButton.Text = "Confirm placements";
            surrenderButton.Text = "Surrender";
            helpButton.Text = "Reveal a ship";
            //sets which type of language is to be displayed in message boxes
            //resets all
            for (int i = 0; i < languageList.Length; i++)
            {
                languageList[i] = false;
            }
            //sets language 0 (English) to main language
            languageList[0] = true;
        }
        //changes all text to Swedish
        public void setSwedish()
        {
            yLanguageButton.Text = "Bekräfta";
            languageLabel.Text = "Välj språk";
            startGameButton.Text = "Starta";
            customizeShipsButton.Text = "Anpassa skepp";
            settingsButton.Text = "Inställningar";
            quitGameButton.Text = "Avsluta";
            informationButton.Text = "Instruktioner";
            changeShipLabel.Text = "Ändra utseende på skepp";
            eShipLabel.Text = "Fiendens skepp";
            eCharLabel.Text = "Form";
            eChangeCharButton.Text = "Ändra";
            eForeColorLabel.Text = "Skeppfärg";
            eSelectForeColorButton.Text = "Välj";
            uShipLabel.Text = "Ditt skepp";
            uCharLabel.Text = "Form";
            uChangeCharButton.Text = "Ändra";
            uForeColorLabel.Text = "Skeppfärg";
            uSelectForeColorButton.Text = "Välj";
            attackButton.Text = "Attackera!";
            roundLabel.Text = "Rond:  ";
            eBattleFieldLabel.Text = "Fiendens bräde";
            uBattleFieldLabel.Text = "Ditt bräde";
            enemyShipsLeftLabel.Text = "Skepp kvar:";
            userShipsLeftLabel.Text = "Skepp kvar:";
            placementButton.Text = "Bekräfta placeringarna";
            surrenderButton.Text = "Ge upp";
            helpButton.Text = "Avslöja ett skepp";
            //sets which type of language is to be displayed in message boxes
            for (int i = 0; i < languageList.Length; i++)
            {
                languageList[i] = false;
            }
            languageList[1] = true;
        }
        //changes all text to French
        public void setFrench()
        {
            yLanguageButton.Text = "Confirmer";
            languageLabel.Text = "Choisir la langue";
            startGameButton.Text = "Commencer";
            customizeShipsButton.Text = "Customiser les navires";
            settingsButton.Text = "Options";
            quitGameButton.Text = "Sortir";
            informationButton.Text = "Comment jouer";
            changeShipLabel.Text = "Customiser les navires";
            eShipLabel.Text = "Navire de l'ennemi";
            eCharLabel.Text = "Forme";
            eChangeCharButton.Text = "Changer";
            eForeColorLabel.Text = "Couleur du navire";
            eSelectForeColorButton.Text = "Choisir";
            uShipLabel.Text = "Ton navire";
            uCharLabel.Text = "Forme";
            uChangeCharButton.Text = "Changer";
            uForeColorLabel.Text = "Couleur du navire";
            uSelectForeColorButton.Text = "Choisir";
            attackButton.Text = "Attaquer!";
            roundLabel.Text = "Round:";
            eBattleFieldLabel.Text = "Planche d'ennemi";
            uBattleFieldLabel.Text = "Ta planche";
            enemyShipsLeftLabel.Text = "Les navires restants:";
            userShipsLeftLabel.Text = "Les navires restants:";
            placementButton.Text = "Confirmer les placements";
            surrenderButton.Text = "Capituler";
            helpButton.Text = "Révélér un navire";
            //sets which type of language is to be displayed in message boxes
            for (int i = 0; i < languageList.Length; i++)
            {
                languageList[i] = false;
            }
            languageList[2] = true;
        }
        //changes all text to Mandarin
        public void setMandarin()
        {
            yLanguageButton.Text = "對";
            languageLabel.Text = "對不起，我不會中文，選擇別語言";
            startGameButton.Text = "開始";
            customizeShipsButton.Text = "船舶";
            settingsButton.Text = "選擇別語言";
            quitGameButton.Text = "離開";
            informationButton.Text = "怎麽玩游戲";
            changeShipLabel.Text = "選擇船";
            eShipLabel.Text = "敵人的船";
            eCharLabel.Text = "字";
            eChangeCharButton.Text = "改變";
            eForeColorLabel.Text = "船的顔色";
            eSelectForeColorButton.Text = "選擇";
            uShipLabel.Text = "你的船";
            uCharLabel.Text = "字";
            uChangeCharButton.Text = "改變";
            uForeColorLabel.Text = "船的顔色";
            uSelectForeColorButton.Text = "選擇";
            attackButton.Text = "打";
            roundLabel.Text = "回合";
            eBattleFieldLabel.Text = "敵人";
            uBattleFieldLabel.Text = "你";
            enemyShipsLeftLabel.Text = "敵人船";
            userShipsLeftLabel.Text = "你船";
            placementButton.Text = "對";
            surrenderButton.Text = "離開游戲";
            helpButton.Text = "看一個船";
            //sets which type of language is to be displayed in message boxes
            for (int i = 0; i < languageList.Length; i++)
            {
                languageList[i] = false;
            }
            languageList[3] = true;
        }

        //User choses to change color for enemy, color dialog pops up
        private void eSelectForeColorButton_Click(object sender, EventArgs e)
        {
            //if color dialog is confirmed, sets the variable "enemyShipColor" to selected color
            //the textcolor is set to selected "enemyShipColor"
            if (eForeColorDialog.ShowDialog() == DialogResult.OK)
            {
                enemyShipColor = eForeColorDialog.Color.ToArgb();
                eForeColorBox.ForeColor = Color.FromArgb(enemyShipColor);
            }
        }
        //User choses to change color for themselves, color dialog pops up
        private void uSelectForeColorButton_Click(object sender, EventArgs e)
        {
            //if color dialog is confirmed, sets the variable "userShipColor" to selected color
            //the textcolor is set to selected "userShipColor"
            if (uForeColorDialog.ShowDialog() == DialogResult.OK)
            {
                userShipColor = uForeColorDialog.Color.ToArgb();
                uForeColorBox.ForeColor = Color.FromArgb(userShipColor);
            }
        }
        //changes char used to represent enemy ship
        private void eChangeCharButton_Click(object sender, EventArgs e)
        {
            //confirms there is a single character + not a space, changes enemy ship sprite
            if (eCharBox.Text.Length == 1 && eCharBox.Text != " ")
            {
                selectedSprites[2] = char.Parse(eCharBox.Text);
                eForeColorBox.Text = eCharBox.Text;
            }
            else
            {
                for (int i = 0; i < languageList.Length; i++)
                {
                    if (languageList[i])
                    {
                        switch (i)
                        {
                            case 0:
                                MessageBox.Show("Only enter one character, no spacebar", "Error");
                                break;

                            case 1:
                                MessageBox.Show("Skriv bara in ett tecken, inget mellanslag", "Felmeddelande");
                                break;

                            case 2:
                                MessageBox.Show("Ecrire seulment un charactère, pas d'espace", "Erreur");
                                break;

                            case 3:
                                MessageBox.Show("只用一個符號，沒有空格", "錯誤");
                                break;
                        }
                    }
                }
            }
        }
        //changes char used to represent user ship
        private void uChangeCharButton_Click(object sender, EventArgs e)
        {
            //confirms there is a single character + not a space, changes user ship sprite
            if (uCharBox.Text.Length == 1 && uCharBox.Text != " ")
            {
                selectedSprites[4] = char.Parse(uCharBox.Text);
                uForeColorBox.Text = uCharBox.Text;
            }
            else
            {
                for (int i = 0; i < languageList.Length; i++)
                {
                    if (languageList[i])
                    {
                        switch (i)
                        {
                            case 0:
                                MessageBox.Show("Only enter one character, no spacebar", "Error");
                                break;

                            case 1:
                                MessageBox.Show("Skriv bara in ett tecken, inget mellanslag", "Felmeddelande");
                                break;

                            case 2:
                                MessageBox.Show("Ecrire seulment un charactère, pas d'espace", "Erreur");
                                break;

                            case 3:
                                MessageBox.Show("只用一個符號，沒有空格", "錯誤");
                                break;
                        }
                    }
                }
            }
        }

        //shows a panel where player can choose to play multiplayer/singleplayer
        private void startGameButton_Click(object sender, EventArgs e)
        {
            playersPanel.Visible = true;
        }
        //starts the battleship game (singleplayer), hides unnecessary panels and shows the game panel + starts the game
        private void sPlayerButton_Click(object sender, EventArgs e)
        {
            startGameButton.Enabled = false;
            playersPanel.Visible = false;
            ipPanel.Visible = false;
            customizeShipsPanel.Visible = false;
            settingsPanel.Visible = false;
            mainGamePanel.Visible = true;

            randomizeShipPlacement();

            //checks if a gameboard has been created earlier
            if (gameBoxesNotCreated)
            {
                createEnemyGameBoxes();
                createUserGameBoxes();
                gameBoxesNotCreated = false;
            }
            else
            {
                translateEnemyBoxes();
            }

            //sets user type to single player
            userType = 1;

            placementButton.Enabled = true;
            surrenderButton.Enabled = true;
        }

        //Shows a panel where the user can choose to be host/client
        private void mPlayerButton_Click(object sender, EventArgs e)
        {
            ipPanel.Visible = true;
        }
        
        //Starts multiplayer game as host
        async private void mPCreateButton_Click(object sender, EventArgs e)
        {
            mPCreateButton.Enabled = false;
            mPJoinButton.Enabled = false;
            try
            {
                //sets proper bounds for which ports can be used
                if (int.Parse(PortBox.Text) < 1024 || int.Parse(PortBox.Text) > 65535)
                {
                    throw new ArgumentOutOfRangeException();
                }

                //listener for the client's stream
                listener = new TcpListener(IPAddress.Any, int.Parse(PortBox.Text));
                listener.Start();
                //client gets identified
                client = await listener.AcceptTcpClientAsync();

                //once client has connected, changes panels to the game and starts it
                if (client.Connected)
                {
                    //VÄNTAR PÅ SERVERSKAPARE
                    startGameButton.Enabled = false;
                    playersPanel.Visible = false;
                    ipPanel.Visible = false;
                    mPCreateButton.Enabled = true;
                    mPJoinButton.Enabled = true;
                    customizeShipsPanel.Visible = false;
                    settingsPanel.Visible = false;
                    mainGamePanel.Visible = true;

                    //checks if a gameboard has been created earlier
                    if (gameBoxesNotCreated)
                    {
                        createEnemyGameBoxes();
                        createUserGameBoxes();
                        gameBoxesNotCreated = false;
                    }
                    //user type: host
                    userType = 2;

                    placementButton.Enabled = true;
                    surrenderButton.Enabled = true;
                }
            }
            catch(System.FormatException)
            {
                //only use integers!
                MessageBox.Show("You can only use integers in the port box!", "Format exception");
                mPCreateButton.Enabled = true;
                mPJoinButton.Enabled = true;
            }
            catch(System.ArgumentOutOfRangeException) //+ tal under 1024
            {
                //accepted integers from 1024 to 65535
                MessageBox.Show("Accepted for port integers: 1024 - 65535!", "Numbers out of range");
                mPCreateButton.Enabled = true;
                mPJoinButton.Enabled = true;
            }
        }
        
        //Starts multiplayer game as client
        private void mPJoinButton_Click(object sender, EventArgs e)
        {
            try
            {
                //sets proper bounds for which ports can be used
                if (int.Parse(PortBox.Text) < 1024 || int.Parse(PortBox.Text) > 65535)
                {
                    //there are two equal throw arguments, this is because they can only be handled in the class
                    throw new ArgumentOutOfRangeException();
                }

                //Uses the info the user has given and connects (if no errors)
                IPAddress adress = IPAddress.Parse(IPBox.Text);
                client = new TcpClient();
                client.NoDelay = true;
                client.Connect(adress, int.Parse(PortBox.Text));

                //once client has connected, changes panels to the game and starts it
                if (client.Connected)
                {
                    //VÄNTAR PÅ SERVERSKAPARE
                    placementButton.Enabled = false;
                    surrenderButton.Enabled = false;
                    startGameButton.Enabled = false;
                    playersPanel.Visible = false;
                    ipPanel.Visible = false;
                    customizeShipsPanel.Visible = false;
                    settingsPanel.Visible = false;
                    mainGamePanel.Visible = true;

                    //checks if a gameboard has been created earlier
                    if (gameBoxesNotCreated)
                    {
                        createEnemyGameBoxes();
                        createUserGameBoxes();
                        gameBoxesNotCreated = false;
                    }
                    //user type: client
                    userType = 3;
                    //accepts host shipplacements and let's client play
                    mPPlaceShips();
                }
            }
            catch(FormatException)
            {
                //only use four integers between 0-255 with dots between them e.g: '123.243.236.128'!
                //only use integers!
                MessageBox.Show("You can only use integers in the port box and 4 integers (0-255) with dots between them for the IP-Adress!", "Format exception");
            }
            catch (System.ArgumentOutOfRangeException) //+ tal under 1024
            {
                //accepted integers from 1024 to 65535
                MessageBox.Show("Accepted for port integers: 1024 - 65535!", "Numbers out of range");
            }
            catch (System.Net.Sockets.SocketException)
            {
                //could not connect, make sure the server has been created and that you have the correct IP
                MessageBox.Show("Could not connect, check if server created/you used the correct endpoint", "Connection error");
            }
        }

        //sends message to the enemy that user has surrendered, surrenders
        private void surrenderButton_Click(object sender, EventArgs e)
        {
            if(userType != 1)
            {
                byte[] dataOut = Encoding.UTF8.GetBytes("surrender-gameEnd");
                client.GetStream().Write(dataOut, 0, dataOut.Length);
            }
            surrender();
        }

        //leaves the battleship game, back to main menu and resets board
        public void surrender()
        {
            //if player isn't in single player
            if (userType > 1)
            {
                if (client.Connected)
                {
                    //closes and resets server connection
                    client.GetStream().Close();
                    client.Close();
                }
                //host has to stop the listener too
                if (userType == 2)
                {
                    listener.Stop();
                }
                userType = 0;
            }

            gameBoardReset();
            attackButton.Enabled = false;
            helpButton.Enabled = false;
            mainGamePanel.Visible = false;
            startGameButton.Enabled = true;
        }

        //places random enemy ships
        public void randomizeShipPlacement()
        {
            shipCounter = 0;

            while (shipCounter < 6)
            {
                randomX = random.Next(7);
                randomY = random.Next(7);

                shipSize = shipCounter / 2 + 1;

                //whether the ships follow the x/y axis
                xOrYShip = random.Next(2);

                //y-axis
                if(xOrYShip == 1)
                {
                    //assumes that the spot where the ship will be placed is empty
                    assumeEmpty = true;
                    //checks if all placement boxes are empty (randomized position, with addition: if y = 6, the last vessel might be placed at y =7, 8 or 9
                    for (int i = randomY; i <= (randomY + shipSize); i++)
                    {
                        if(enemyBattleField[i, randomX] != 0)
                        {
                            assumeEmpty = false;
                        }
                    }
                    //if the positions are empty, places vessels and adds to the ship counter
                    if(assumeEmpty)
                    {
                        for(int i = randomY; i <= (randomY + shipSize); i++)
                        {
                            enemyBattleField[i, randomX] = 2;
                        }
                        shipCounter++;
                    }
                }
                //x-axis
                else
                {
                    //assumes that the spot where the ship will be placed is empty
                    assumeEmpty = true;

                    //checks if all placement boxes are empty
                    for (int j = randomX; j <= (randomX + shipSize); j++)
                    {
                        if (enemyBattleField[randomY, j] != 0)
                        {
                            assumeEmpty = false;
                        }
                    }

                    if (assumeEmpty)
                    {
                        for (int j = randomX; j <= (randomX + shipSize); j++)
                        {
                            enemyBattleField[randomY, j] = 2;
                        }
                        shipCounter++;
                    }
                }
            }
        }

        //visually creates the enemy gameboard, sets up textboxes
        public void createEnemyGameBoxes()
        {
            //positioning and counter for which textbox is being placed
            int gameBoxYPos = 0;
            int gameBoxCounter = 0;

            //goes from column to column then row to row and repeats
            for (int i = 0; i < 10; i++)
            {
                //moves the textboxes down one row
                gameBoxYPos += 27;
                for (int j = 0; j < 10; j++)
                {
                    //adds a textbox
                    gameBoxCounter++;
                    var box = new TextBox();
                    //starts at [0, 0]: box will receive the name gameBox1, then gameBox2 etc
                    enemyGameBoxList[i, j] = box;
                    box.Name = "gameBox" + (gameBoxCounter).ToString();
                    //manages all the settings
                    box.Font = new Font("Microsoft Sans Serif", 8);
                    //whether there is a ship at the coordinate of the textbox
                    if (enemyBattleField[i, j] == 2)
                    {
                        box.ForeColor = Color.FromArgb(enemyShipColor);
                    }
                    else
                    {
                        box.Text = selectedSprites[0].ToString();
                    }
                    //manages all the settings
                    box.Location = new Point(10 + (j * 27), 15 + gameBoxYPos);
                    box.Visible = true;
                    box.Size = new Size(20, 20);
                    //removed due to color not changing
                    //box.ReadOnly = true;
                    //finishes the creation of the box on to the gameboard
                    gamePanel2.Controls.Add(box);
                }
            }
            //creates methods for every enemy gamebox click
            enemyGameBoxList[0, 0].Click += new EventHandler(enemyGameBox1_Click);
            enemyGameBoxList[0, 1].Click += new EventHandler(enemyGameBox2_Click);
            enemyGameBoxList[0, 2].Click += new EventHandler(enemyGameBox3_Click);
            enemyGameBoxList[0, 3].Click += new EventHandler(enemyGameBox4_Click);
            enemyGameBoxList[0, 4].Click += new EventHandler(enemyGameBox5_Click);
            enemyGameBoxList[0, 5].Click += new EventHandler(enemyGameBox6_Click);
            enemyGameBoxList[0, 6].Click += new EventHandler(enemyGameBox7_Click);
            enemyGameBoxList[0, 7].Click += new EventHandler(enemyGameBox8_Click);
            enemyGameBoxList[0, 8].Click += new EventHandler(enemyGameBox9_Click);
            enemyGameBoxList[0, 9].Click += new EventHandler(enemyGameBox10_Click);
            enemyGameBoxList[1, 0].Click += new EventHandler(enemyGameBox11_Click);
            enemyGameBoxList[1, 1].Click += new EventHandler(enemyGameBox12_Click);
            enemyGameBoxList[1, 2].Click += new EventHandler(enemyGameBox13_Click);
            enemyGameBoxList[1, 3].Click += new EventHandler(enemyGameBox14_Click);
            enemyGameBoxList[1, 4].Click += new EventHandler(enemyGameBox15_Click);
            enemyGameBoxList[1, 5].Click += new EventHandler(enemyGameBox16_Click);
            enemyGameBoxList[1, 6].Click += new EventHandler(enemyGameBox17_Click);
            enemyGameBoxList[1, 7].Click += new EventHandler(enemyGameBox18_Click);
            enemyGameBoxList[1, 8].Click += new EventHandler(enemyGameBox19_Click);
            enemyGameBoxList[1, 9].Click += new EventHandler(enemyGameBox20_Click);
            enemyGameBoxList[2, 0].Click += new EventHandler(enemyGameBox21_Click);
            enemyGameBoxList[2, 1].Click += new EventHandler(enemyGameBox22_Click);
            enemyGameBoxList[2, 2].Click += new EventHandler(enemyGameBox23_Click);
            enemyGameBoxList[2, 3].Click += new EventHandler(enemyGameBox24_Click);
            enemyGameBoxList[2, 4].Click += new EventHandler(enemyGameBox25_Click);
            enemyGameBoxList[2, 5].Click += new EventHandler(enemyGameBox26_Click);
            enemyGameBoxList[2, 6].Click += new EventHandler(enemyGameBox27_Click);
            enemyGameBoxList[2, 7].Click += new EventHandler(enemyGameBox28_Click);
            enemyGameBoxList[2, 8].Click += new EventHandler(enemyGameBox29_Click);
            enemyGameBoxList[2, 9].Click += new EventHandler(enemyGameBox30_Click);
            enemyGameBoxList[3, 0].Click += new EventHandler(enemyGameBox31_Click);
            enemyGameBoxList[3, 1].Click += new EventHandler(enemyGameBox32_Click);
            enemyGameBoxList[3, 2].Click += new EventHandler(enemyGameBox33_Click);
            enemyGameBoxList[3, 3].Click += new EventHandler(enemyGameBox34_Click);
            enemyGameBoxList[3, 4].Click += new EventHandler(enemyGameBox35_Click);
            enemyGameBoxList[3, 5].Click += new EventHandler(enemyGameBox36_Click);
            enemyGameBoxList[3, 6].Click += new EventHandler(enemyGameBox37_Click);
            enemyGameBoxList[3, 7].Click += new EventHandler(enemyGameBox38_Click);
            enemyGameBoxList[3, 8].Click += new EventHandler(enemyGameBox39_Click);
            enemyGameBoxList[3, 9].Click += new EventHandler(enemyGameBox40_Click);
            enemyGameBoxList[4, 0].Click += new EventHandler(enemyGameBox41_Click);
            enemyGameBoxList[4, 1].Click += new EventHandler(enemyGameBox42_Click);
            enemyGameBoxList[4, 2].Click += new EventHandler(enemyGameBox43_Click);
            enemyGameBoxList[4, 3].Click += new EventHandler(enemyGameBox44_Click);
            enemyGameBoxList[4, 4].Click += new EventHandler(enemyGameBox45_Click);
            enemyGameBoxList[4, 5].Click += new EventHandler(enemyGameBox46_Click);
            enemyGameBoxList[4, 6].Click += new EventHandler(enemyGameBox47_Click);
            enemyGameBoxList[4, 7].Click += new EventHandler(enemyGameBox48_Click);
            enemyGameBoxList[4, 8].Click += new EventHandler(enemyGameBox49_Click);
            enemyGameBoxList[4, 9].Click += new EventHandler(enemyGameBox50_Click);
            enemyGameBoxList[5, 0].Click += new EventHandler(enemyGameBox51_Click);
            enemyGameBoxList[5, 1].Click += new EventHandler(enemyGameBox52_Click);
            enemyGameBoxList[5, 2].Click += new EventHandler(enemyGameBox53_Click);
            enemyGameBoxList[5, 3].Click += new EventHandler(enemyGameBox54_Click);
            enemyGameBoxList[5, 4].Click += new EventHandler(enemyGameBox55_Click);
            enemyGameBoxList[5, 5].Click += new EventHandler(enemyGameBox56_Click);
            enemyGameBoxList[5, 6].Click += new EventHandler(enemyGameBox57_Click);
            enemyGameBoxList[5, 7].Click += new EventHandler(enemyGameBox58_Click);
            enemyGameBoxList[5, 8].Click += new EventHandler(enemyGameBox59_Click);
            enemyGameBoxList[5, 9].Click += new EventHandler(enemyGameBox60_Click);
            enemyGameBoxList[6, 0].Click += new EventHandler(enemyGameBox61_Click);
            enemyGameBoxList[6, 1].Click += new EventHandler(enemyGameBox62_Click);
            enemyGameBoxList[6, 2].Click += new EventHandler(enemyGameBox63_Click);
            enemyGameBoxList[6, 3].Click += new EventHandler(enemyGameBox64_Click);
            enemyGameBoxList[6, 4].Click += new EventHandler(enemyGameBox65_Click);
            enemyGameBoxList[6, 5].Click += new EventHandler(enemyGameBox66_Click);
            enemyGameBoxList[6, 6].Click += new EventHandler(enemyGameBox67_Click);
            enemyGameBoxList[6, 7].Click += new EventHandler(enemyGameBox68_Click);
            enemyGameBoxList[6, 8].Click += new EventHandler(enemyGameBox69_Click);
            enemyGameBoxList[6, 9].Click += new EventHandler(enemyGameBox70_Click);
            enemyGameBoxList[7, 0].Click += new EventHandler(enemyGameBox71_Click);
            enemyGameBoxList[7, 1].Click += new EventHandler(enemyGameBox72_Click);
            enemyGameBoxList[7, 2].Click += new EventHandler(enemyGameBox73_Click);
            enemyGameBoxList[7, 3].Click += new EventHandler(enemyGameBox74_Click);
            enemyGameBoxList[7, 4].Click += new EventHandler(enemyGameBox75_Click);
            enemyGameBoxList[7, 5].Click += new EventHandler(enemyGameBox76_Click);
            enemyGameBoxList[7, 6].Click += new EventHandler(enemyGameBox77_Click);
            enemyGameBoxList[7, 7].Click += new EventHandler(enemyGameBox78_Click);
            enemyGameBoxList[7, 8].Click += new EventHandler(enemyGameBox79_Click);
            enemyGameBoxList[7, 9].Click += new EventHandler(enemyGameBox80_Click);
            enemyGameBoxList[8, 0].Click += new EventHandler(enemyGameBox81_Click);
            enemyGameBoxList[8, 1].Click += new EventHandler(enemyGameBox82_Click);
            enemyGameBoxList[8, 2].Click += new EventHandler(enemyGameBox83_Click);
            enemyGameBoxList[8, 3].Click += new EventHandler(enemyGameBox84_Click);
            enemyGameBoxList[8, 4].Click += new EventHandler(enemyGameBox85_Click);
            enemyGameBoxList[8, 5].Click += new EventHandler(enemyGameBox86_Click);
            enemyGameBoxList[8, 6].Click += new EventHandler(enemyGameBox87_Click);
            enemyGameBoxList[8, 7].Click += new EventHandler(enemyGameBox88_Click);
            enemyGameBoxList[8, 8].Click += new EventHandler(enemyGameBox89_Click);
            enemyGameBoxList[8, 9].Click += new EventHandler(enemyGameBox90_Click);
            enemyGameBoxList[9, 0].Click += new EventHandler(enemyGameBox91_Click);
            enemyGameBoxList[9, 1].Click += new EventHandler(enemyGameBox92_Click);
            enemyGameBoxList[9, 2].Click += new EventHandler(enemyGameBox93_Click);
            enemyGameBoxList[9, 3].Click += new EventHandler(enemyGameBox94_Click);
            enemyGameBoxList[9, 4].Click += new EventHandler(enemyGameBox95_Click);
            enemyGameBoxList[9, 5].Click += new EventHandler(enemyGameBox96_Click);
            enemyGameBoxList[9, 6].Click += new EventHandler(enemyGameBox97_Click);
            enemyGameBoxList[9, 7].Click += new EventHandler(enemyGameBox98_Click);
            enemyGameBoxList[9, 8].Click += new EventHandler(enemyGameBox99_Click);
            enemyGameBoxList[9, 9].Click += new EventHandler(enemyGameBox100_Click);
        }
        //visually creates the user gameboard, same as the description for createEnemyGameBoxes()
        public void createUserGameBoxes()
        {
            int gameBoxYPos = 0;
            int gameBoxCounter = 0;
            for (int i = 0; i < 10; i++)
            {
                gameBoxYPos += 27;
                for (int j = 0; j < 10; j++)
                {
                    gameBoxCounter++;
                    var box = new TextBox();
                    userGameBoxList[i, j] = box;
                    box.Name = "gameBox" + (gameBoxCounter).ToString();
                    box.Font = new Font("Microsoft Sans Serif", 8);
                    box.Text = selectedSprites[0].ToString();
                    box.Location = new Point(10 + (j * 27), 15 + gameBoxYPos);
                    box.Visible = true;
                    box.Size = new Size(20, 20);
                    //removed due to color not changing
                    //box.ReadOnly = true;
                    gamePanel3.Controls.Add(box);
                }
            }
            //creates methods for every user gamebox click
            userGameBoxList[0, 0].Click += new EventHandler(userGameBox1_Click);
            userGameBoxList[0, 1].Click += new EventHandler(userGameBox2_Click);
            userGameBoxList[0, 2].Click += new EventHandler(userGameBox3_Click);
            userGameBoxList[0, 3].Click += new EventHandler(userGameBox4_Click);
            userGameBoxList[0, 4].Click += new EventHandler(userGameBox5_Click);
            userGameBoxList[0, 5].Click += new EventHandler(userGameBox6_Click);
            userGameBoxList[0, 6].Click += new EventHandler(userGameBox7_Click);
            userGameBoxList[0, 7].Click += new EventHandler(userGameBox8_Click);
            userGameBoxList[0, 8].Click += new EventHandler(userGameBox9_Click);
            userGameBoxList[0, 9].Click += new EventHandler(userGameBox10_Click);
            userGameBoxList[1, 0].Click += new EventHandler(userGameBox11_Click);
            userGameBoxList[1, 1].Click += new EventHandler(userGameBox12_Click);
            userGameBoxList[1, 2].Click += new EventHandler(userGameBox13_Click);
            userGameBoxList[1, 3].Click += new EventHandler(userGameBox14_Click);
            userGameBoxList[1, 4].Click += new EventHandler(userGameBox15_Click);
            userGameBoxList[1, 5].Click += new EventHandler(userGameBox16_Click);
            userGameBoxList[1, 6].Click += new EventHandler(userGameBox17_Click);
            userGameBoxList[1, 7].Click += new EventHandler(userGameBox18_Click);
            userGameBoxList[1, 8].Click += new EventHandler(userGameBox19_Click);
            userGameBoxList[1, 9].Click += new EventHandler(userGameBox20_Click);
            userGameBoxList[2, 0].Click += new EventHandler(userGameBox21_Click);
            userGameBoxList[2, 1].Click += new EventHandler(userGameBox22_Click);
            userGameBoxList[2, 2].Click += new EventHandler(userGameBox23_Click);
            userGameBoxList[2, 3].Click += new EventHandler(userGameBox24_Click);
            userGameBoxList[2, 4].Click += new EventHandler(userGameBox25_Click);
            userGameBoxList[2, 5].Click += new EventHandler(userGameBox26_Click);
            userGameBoxList[2, 6].Click += new EventHandler(userGameBox27_Click);
            userGameBoxList[2, 7].Click += new EventHandler(userGameBox28_Click);
            userGameBoxList[2, 8].Click += new EventHandler(userGameBox29_Click);
            userGameBoxList[2, 9].Click += new EventHandler(userGameBox30_Click);
            userGameBoxList[3, 0].Click += new EventHandler(userGameBox31_Click);
            userGameBoxList[3, 1].Click += new EventHandler(userGameBox32_Click);
            userGameBoxList[3, 2].Click += new EventHandler(userGameBox33_Click);
            userGameBoxList[3, 3].Click += new EventHandler(userGameBox34_Click);
            userGameBoxList[3, 4].Click += new EventHandler(userGameBox35_Click);
            userGameBoxList[3, 5].Click += new EventHandler(userGameBox36_Click);
            userGameBoxList[3, 6].Click += new EventHandler(userGameBox37_Click);
            userGameBoxList[3, 7].Click += new EventHandler(userGameBox38_Click);
            userGameBoxList[3, 8].Click += new EventHandler(userGameBox39_Click);
            userGameBoxList[3, 9].Click += new EventHandler(userGameBox40_Click);
            userGameBoxList[4, 0].Click += new EventHandler(userGameBox41_Click);
            userGameBoxList[4, 1].Click += new EventHandler(userGameBox42_Click);
            userGameBoxList[4, 2].Click += new EventHandler(userGameBox43_Click);
            userGameBoxList[4, 3].Click += new EventHandler(userGameBox44_Click);
            userGameBoxList[4, 4].Click += new EventHandler(userGameBox45_Click);
            userGameBoxList[4, 5].Click += new EventHandler(userGameBox46_Click);
            userGameBoxList[4, 6].Click += new EventHandler(userGameBox47_Click);
            userGameBoxList[4, 7].Click += new EventHandler(userGameBox48_Click);
            userGameBoxList[4, 8].Click += new EventHandler(userGameBox49_Click);
            userGameBoxList[4, 9].Click += new EventHandler(userGameBox50_Click);
            userGameBoxList[5, 0].Click += new EventHandler(userGameBox51_Click);
            userGameBoxList[5, 1].Click += new EventHandler(userGameBox52_Click);
            userGameBoxList[5, 2].Click += new EventHandler(userGameBox53_Click);
            userGameBoxList[5, 3].Click += new EventHandler(userGameBox54_Click);
            userGameBoxList[5, 4].Click += new EventHandler(userGameBox55_Click);
            userGameBoxList[5, 5].Click += new EventHandler(userGameBox56_Click);
            userGameBoxList[5, 6].Click += new EventHandler(userGameBox57_Click);
            userGameBoxList[5, 7].Click += new EventHandler(userGameBox58_Click);
            userGameBoxList[5, 8].Click += new EventHandler(userGameBox59_Click);
            userGameBoxList[5, 9].Click += new EventHandler(userGameBox60_Click);
            userGameBoxList[6, 0].Click += new EventHandler(userGameBox61_Click);
            userGameBoxList[6, 1].Click += new EventHandler(userGameBox62_Click);
            userGameBoxList[6, 2].Click += new EventHandler(userGameBox63_Click);
            userGameBoxList[6, 3].Click += new EventHandler(userGameBox64_Click);
            userGameBoxList[6, 4].Click += new EventHandler(userGameBox65_Click);
            userGameBoxList[6, 5].Click += new EventHandler(userGameBox66_Click);
            userGameBoxList[6, 6].Click += new EventHandler(userGameBox67_Click);
            userGameBoxList[6, 7].Click += new EventHandler(userGameBox68_Click);
            userGameBoxList[6, 8].Click += new EventHandler(userGameBox69_Click);
            userGameBoxList[6, 9].Click += new EventHandler(userGameBox70_Click);
            userGameBoxList[7, 0].Click += new EventHandler(userGameBox71_Click);
            userGameBoxList[7, 1].Click += new EventHandler(userGameBox72_Click);
            userGameBoxList[7, 2].Click += new EventHandler(userGameBox73_Click);
            userGameBoxList[7, 3].Click += new EventHandler(userGameBox74_Click);
            userGameBoxList[7, 4].Click += new EventHandler(userGameBox75_Click);
            userGameBoxList[7, 5].Click += new EventHandler(userGameBox76_Click);
            userGameBoxList[7, 6].Click += new EventHandler(userGameBox77_Click);
            userGameBoxList[7, 7].Click += new EventHandler(userGameBox78_Click);
            userGameBoxList[7, 8].Click += new EventHandler(userGameBox79_Click);
            userGameBoxList[7, 9].Click += new EventHandler(userGameBox80_Click);
            userGameBoxList[8, 0].Click += new EventHandler(userGameBox81_Click);
            userGameBoxList[8, 1].Click += new EventHandler(userGameBox82_Click);
            userGameBoxList[8, 2].Click += new EventHandler(userGameBox83_Click);
            userGameBoxList[8, 3].Click += new EventHandler(userGameBox84_Click);
            userGameBoxList[8, 4].Click += new EventHandler(userGameBox85_Click);
            userGameBoxList[8, 5].Click += new EventHandler(userGameBox86_Click);
            userGameBoxList[8, 6].Click += new EventHandler(userGameBox87_Click);
            userGameBoxList[8, 7].Click += new EventHandler(userGameBox88_Click);
            userGameBoxList[8, 8].Click += new EventHandler(userGameBox89_Click);
            userGameBoxList[8, 9].Click += new EventHandler(userGameBox90_Click);
            userGameBoxList[9, 0].Click += new EventHandler(userGameBox91_Click);
            userGameBoxList[9, 1].Click += new EventHandler(userGameBox92_Click);
            userGameBoxList[9, 2].Click += new EventHandler(userGameBox93_Click);
            userGameBoxList[9, 3].Click += new EventHandler(userGameBox94_Click);
            userGameBoxList[9, 4].Click += new EventHandler(userGameBox95_Click);
            userGameBoxList[9, 5].Click += new EventHandler(userGameBox96_Click);
            userGameBoxList[9, 6].Click += new EventHandler(userGameBox97_Click);
            userGameBoxList[9, 7].Click += new EventHandler(userGameBox98_Click);
            userGameBoxList[9, 8].Click += new EventHandler(userGameBox99_Click);
            userGameBoxList[9, 9].Click += new EventHandler(userGameBox100_Click);
        }

        //visually translates the randomized board to the game boxes
        public void translateEnemyBoxes()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (enemyBattleField[i, j] == 2)
                    {
                        enemyGameBoxList[i, j].ForeColor = Color.FromArgb(enemyShipColor);
                    }
                    /*
                    else
                    {
                        Users will be able to change all sprites in the future
                        enemyGameBoxList[i, j].Text = selectedSprites[0].ToString();
                    }
                    */
                }
            }
        }
        //removes characters, colors and ship placements of all boxes and resets round counter
        public void gameBoardReset()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    enemyGameBoxList[i, j].ForeColor = Color.FromArgb(0, 0, 0);
                    userGameBoxList[i, j].ForeColor = Color.FromArgb(0, 0, 0);
                    enemyGameBoxList[i, j].Text = selectedSprites[0].ToString();
                    userGameBoxList[i, j].Text = selectedSprites[0].ToString();
                    enemyBattleField[i, j] = 0;
                    userBattleField[i, j] = 0;
                }
            }
            roundCounter = 1;
            roundCountLabel.Text = roundCounter.ToString();
        }

        //after the user has chosen to attack
        private void attackButton_Click(object sender, EventArgs e)
        {
            //if an enemy box has been selected
            if(enemyBoxClick)
            {
                //single player: advance to next round + attack
                if(userType == 1)
                {
                    roundCounter++;
                    roundCountLabel.Text = roundCounter.ToString();

                    sPAttack();
                }
                //host: attack and wait for a counter attack
                else if(userType == 2)
                {
                    mPAttack();
                    u2GetAttackData();
                }
                //client: attack, count next round, count ships and check if anybody has won.
                else
                {
                    mPAttack();

                    roundCounter++;
                    roundCountLabel.Text = roundCounter.ToString();

                    //checks enemy's and user's boards to determine winner
                    countEnemyShips();
                    countUserShips();
                    if (client.Connected)
                    {
                        checkWinner(enemyShipsLeft, userShipsLeft);
                    }
                    //game/connection hasn't ended await counter attack
                    if (userType == 3)
                    {
                        u3GetAttackData();
                    }
                }
            }
            //if no enemy box has been chosen, an error depending on the language shows up
            else
            {
                for (int i = 0; i < languageList.Length; i++)
                {
                    if (languageList[i])
                    {
                        switch(i)
                        {
                            case 0:
                                MessageBox.Show("Select a box to attack", "Error");
                                break;

                            case 1:
                                MessageBox.Show("Välj en ruta att attackera", "Felmeddelande");
                                break;

                            case 2:
                                MessageBox.Show("Choisir une case à attaquer", "Erreur");
                                break;

                            case 3:
                                MessageBox.Show("選擇攻擊的正方形", "錯誤");
                                break;
                        }
                    }
                }
            }
        }

        //single player attack
        public void sPAttack()
        {
            //USER ATTACK, checks if user has attacked a ship or an empty box. If user attacks a box again it will count as them skipping the round.
            if (enemyBattleField[attackValueY, attackValueX] == 0)
            {
                //empty box to attacked box, no ship
                enemyGameBoxList[attackValueY, attackValueX].Text = selectedSprites[1].ToString();
                enemyBattleField[attackValueY, attackValueX] = 1;
            }
            else if (enemyBattleField[attackValueY, attackValueX] == 2)
            {
                //ship box to attacked ship box
                enemyGameBoxList[attackValueY, attackValueX].Text = selectedSprites[3].ToString();
                enemyBattleField[attackValueY, attackValueX] = 3;
            }

            //AI ATTACK, randomizes which box to attack. Has to attack a box that hasn't been attacked
            while (enemyBoxClick)
            {
                randomY = random.Next(10);
                randomX = random.Next(10);
                //empty box
                if (userBattleField[randomY, randomX] == 0)
                {
                    userGameBoxList[randomY, randomX].Text = selectedSprites[1].ToString();
                    userBattleField[randomY, randomX] = 1;

                    enemyBoxClick = false;
                    xCoordTextBox.Text = null;
                    yCoordTextBox.Text = null;

                    //checks enemy's and user's boards to determine winner
                    countUserShips();
                    checkWinner(enemyShipsLeft, userShipsLeft);
                }
                //box with ship
                else if (userBattleField[randomY, randomX] == 4)
                {
                    userGameBoxList[randomY, randomX].Text = selectedSprites[5].ToString();
                    userBattleField[randomY, randomX] = 5;

                    enemyBoxClick = false;

                    //checks enemy's and user's boards to determine winner
                    countEnemyShips();
                    countUserShips();
                    checkWinner(enemyShipsLeft, userShipsLeft);
                }
            }
        }

        //multiplayer attack
        public void mPAttack()
        {
            //USER ATTACK, checks if user has attacked a ship or an empty box. If user attacks a box again it will count as them skipping the round.
            if (enemyBattleField[attackValueY, attackValueX] == 0)
            {
                //empty box to attacked box, no ship
                enemyGameBoxList[attackValueY, attackValueX].Text = selectedSprites[1].ToString();
                enemyBattleField[attackValueY, attackValueX] = 1;
                //sends attack data to enemy (last digit is what value the box should get)
                //this should only require one byte in comparison to 4 for an integer, minimum load on the internet only the computer has to convert it
                try
                {
                    byte[] dataOut = Encoding.UTF8.GetBytes(attackValueY + "" + attackValueX + "1");
                    client.GetStream().Write(dataOut, 0, dataOut.Length);
                }
                catch(Exception)
                {
                    //network error, communication closed beforehand
                    MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                    surrender();
                }
            }
            else if (enemyBattleField[attackValueY, attackValueX] == 2)
            {
                //ship box to attacked ship box
                enemyGameBoxList[attackValueY, attackValueX].Text = selectedSprites[3].ToString();
                enemyBattleField[attackValueY, attackValueX] = 3;
                /*sends attack data to enemy (last digit is what value the box should get)
                this should only require one byte in comparison to 4 for an integer, minimum load on the internet and the computer only has to convert
                on the other person's board, their ship is sunk. thus last digit is 5.*/
                try
                {
                    byte[] dataOut = Encoding.UTF8.GetBytes(attackValueY + "" + attackValueX + "5");
                    client.GetStream().Write(dataOut, 0, dataOut.Length);
                }
                catch (Exception)
                {
                    //network error, communication closed beforehand
                    MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                    surrender();
                }
            }
            else
            {
                //sends "0", will count as nothing and attack counts as skipped
                try
                {
                    byte[] dataOut = Encoding.UTF8.GetBytes("0");
                    client.GetStream().Write(dataOut, 0, dataOut.Length);
                }
                catch (Exception)
                {
                    //network error, communication closed beforehand
                    MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                    surrender();
                }
            }
            //user's turn is over, resets attack values so they won't accidentally attack the same box
            attackButton.Enabled = false;
            surrenderButton.Enabled = false;
            enemyBoxClick = false;
            xCoordTextBox.Text = null;
            yCoordTextBox.Text = null;
        }

        //counts how many ships the enemy has left, outprints them on the enemy ship counter label
        public void countEnemyShips()
        {
            enemyShipsLeft = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (enemyBattleField[i, j] == 2)
                    {
                        enemyShipsLeft++;
                    }
                }
            }
            enemyShipsLeftCountLabel.Text = enemyShipsLeft.ToString();
        }
        //counts how many ships the user has left, outprints them on the user ship counter label
        public void countUserShips()
        {
            userShipsLeft = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (userBattleField[i, j] == 4)
                    {
                        userShipsLeft++;
                    }
                }
            }
            userShipsLeftCountLabel.Text = userShipsLeft.ToString();
        }

        //checks if the game has ended
        public void checkWinner(int enemyShipsLeft, int userShipsLeft)
        {
                //checks if both enemy and user have no ships left
                if (enemyShipsLeft == 0 && userShipsLeft == 0)
                {
                    //closes and resets server connection
                    client.GetStream().Close();
                    client.Close();
                    //listener needs to be closed for host
                    if (userType == 2)
                    {
                        listener.Stop();
                    }
                    //resets user type
                    userType = 0;
                    //displays message for draw
                    for (int i = 0; i < languageList.Length; i++)
                    {
                        if (languageList[i])
                        {
                            switch (i)
                            {
                                case 0:
                                    MessageBox.Show("The enemy and you have no ships left, you have tied", "Draw");
                                    break;

                                case 1:
                                    MessageBox.Show("Både fienden och du har inga skepp kvar, oavgjort", "Oavgjort");
                                    break;

                                case 2:
                                    MessageBox.Show("L'ennemi et toi n'ont pas des navires restants, c'est une égalité", "Égalité");
                                    break;

                                case 3:
                                    MessageBox.Show("沒有任何剩的船，你和敵人平局了", "平局");
                                    break;
                            }
                        }
                    }
                    //resets game and returns to menu
                    gameBoardReset();
                    attackButton.Enabled = false;
                    helpButton.Enabled = false;
                    mainGamePanel.Visible = false;
                    startGameButton.Enabled = true;
                }

                //checks if no enemy ships left
                else if (enemyShipsLeft == 0)
                {
                    //closes and resets server connection
                    client.GetStream().Close();
                    client.Close();
                    if (userType == 2)
                    {
                        listener.Stop();
                    }
                    userType = 0;
                    //displays message for victory
                    for (int i = 0; i < languageList.Length; i++)
                    {
                        if (languageList[i])
                        {
                            switch (i)
                            {
                                case 0:
                                    MessageBox.Show("The enemy has no ships left, you have won", "Victory");
                                    break;

                                case 1:
                                    MessageBox.Show("Fienden har inga skepp kvar, du har vunnit", "Seger");
                                    break;

                                case 2:
                                    MessageBox.Show("L'ennemi n'a pas des navires restants, vous avez gagné", "Victoire");
                                    break;

                                case 3:
                                    MessageBox.Show("敵人沒有剩的船，你勝利了", "勝利");
                                    break;
                            }
                        }
                    }
                    //resets game and returns to menu
                    gameBoardReset();
                    attackButton.Enabled = false;
                    helpButton.Enabled = false;
                    mainGamePanel.Visible = false;
                    startGameButton.Enabled = true;
                }

                //checks if no user ships left
                else if (userShipsLeft == 0)
                {
                    //closes and resets server connection
                    client.GetStream().Close();
                    client.Close();
                    if (userType == 2)
                    {
                        listener.Stop();
                    }
                    userType = 0;
                    //displays message for defeat
                    for (int i = 0; i < languageList.Length; i++)
                    {
                        if (languageList[i])
                        {
                            switch (i)
                            {
                                case 0:
                                    MessageBox.Show("You have no ships left, you have lost", "Defeat");
                                    break;

                                case 1:
                                    MessageBox.Show("Du har inga skepp kvar, du har förlorat", "Förlust");
                                    break;

                                case 2:
                                    MessageBox.Show("Vous n'avez pas des navires restants, vous avez perdu", "Défaite");
                                    break;

                                case 3:
                                    MessageBox.Show("你沒有剩的船，你失敗了", "失敗");
                                    break;
                            }
                        }
                    }
                    //resets game and returns to menu
                    gameBoardReset();
                    attackButton.Enabled = false;
                    helpButton.Enabled = false;
                    mainGamePanel.Visible = false;
                    startGameButton.Enabled = true;
                }
        }

        //when user confirms ship placements
        private void placementButton_Click(object sender, EventArgs e)
        {
            //IF SINGLEPLAYER
            if(userType == 1)
            {
                //checks enemy's and user's boards
                countEnemyShips();
                countUserShips();
            }
            //IF MULTIPLAYER
            else
            {
                countUserShips();
                //counts enemy ships later, as they need to be placed and sent first
            }

            //userboard must have 1 - 18 ships, disables this method and changes the color of the boxes who have user ships
            if (userShipsLeft >= 1 && userShipsLeft <= 18)
            {
                placementButton.Enabled = false;
                //multiplayer should not be able to surrender when it's not their turn
                if (userType != 1)
                {
                    surrenderButton.Enabled = false;
                }

                //Text that will contain information about ship placements, waits for all ships to be checked
                placementText = null;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (userBattleField[i, j] == 4)
                        {
                            userGameBoxList[i, j].ForeColor = Color.FromArgb(userShipColor);
                            if (placementText == null)
                            {
                                //due to an error, the first character disappears and information about the problem is insufficient for a good solution to the problem
                                //simplest correction: x always disappears and the rest is sent
                                placementText = "x" + i + "" + j + " ";
                            }
                            else
                            {
                                placementText = placementText + i + "" + j + " ";
                            }
                        }
                    }
                }
                //SINGLE PLAYER, can attack
                if (userType == 1)
                {
                    attackButton.Enabled = true;
                    helpButton.Enabled = true;
                }
                //MULTIPLAYER, put into waiting mode
                else
                {
                    placementButton.Enabled = false;
                    surrenderButton.Enabled = false;
                    //HOST
                    if (userType == 2)
                    {
                        //send ship placements to enemy and wait for them to send placements back
                        try
                        {
                            byte[] dataOut = Encoding.UTF8.GetBytes(placementText);
                            client.GetStream().Write(dataOut, 0, dataOut.Length);

                            mPPlaceShips();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                            surrender();
                        }
                    }
                    //CLIENT
                    else
                    {
                        try
                        {
                            //send ship placements to enemy
                            byte[] dataOut = Encoding.UTF8.GetBytes(placementText);
                            client.GetStream().Write(dataOut, 0, dataOut.Length);

                            //wait for enemy to attack a box, wait to get which box enemy has attacked
                            u3GetAttackData();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                            surrender();
                        }
                    }
                }
            }
            //if wrong amount of user ships, user gets an error message
            else if (userShipsLeft > 18)
            {
                for (int i = 0; i < languageList.Length; i++)
                {
                    if (languageList[i])
                    {
                        switch (i)
                        {
                            case 0:
                                MessageBox.Show("There are too many ships on your board, maximum: 18 ships", "Too many ships");
                                break;

                            case 1:
                                MessageBox.Show("Du har placerat för många skepp, max 18 skepp på ditt bräde", "För många skepp");
                                break;

                            case 2:
                                MessageBox.Show("Il y a trop de navires sur ta planche, maximum: 18 naivres", "Trop de navires");
                                break;

                            case 3:
                                MessageBox.Show("船需要比18少", "太多船了");
                                break;
                        }
                    }
                }
                //resets user's gameboard
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        userGameBoxList[i, j].Text = selectedSprites[0].ToString();
                        userBattleField[i, j] = 0;
                        userShipsLeftCountLabel.Text = "?";
                    }
                }
            }
            else
            {
                for (int i = 0; i < languageList.Length; i++)
                {
                    if (languageList[i])
                    {
                        switch (i)
                        {
                            case 0:
                                MessageBox.Show("There are no ships on your board, place at least one", "Too few ships");
                                break;

                            case 1:
                                MessageBox.Show("Det finns inga skepp på ditt spelbräde, placera åtminstone ett", "För lite skepp");
                                break;

                            case 2:
                                MessageBox.Show("Il n'y a pas des navires sur ta planche, poser au moins un", "Pas assez de navires");
                                break;

                            case 3:
                                MessageBox.Show("你需要船", "太少船了");
                                break;
                        }
                    }
                }
            }
        }

        //multiplayer method for ship placement
        async private void mPPlaceShips()
        {
            try
            {
                //receives data from enemy (either ship placements or if they have surrendered)
                byte[] dataIn = new byte[256];
                int totalBytes = await client.GetStream().ReadAsync(dataIn, 0, dataIn.Length);
                String textIn = Encoding.UTF8.GetString(dataIn, 0, totalBytes);
                Console.WriteLine(textIn);
                
                //if enemy hasn't surrendered, the data will be analyzed
                if (textIn != "surrender-gameEnd")
                {
                    //i needs to be < .length - 1 because it will check i+1
                    for (int i = 0; i < textIn.Length - 1; i++)
                    {
                        //if there are two digits in a row, proceeds to place enemy ship on the coordinates
                        if (Char.IsDigit(textIn[i]) && Char.IsDigit(textIn[i + 1]))
                        {
                            enemyBattleField[(textIn[i] - 48), (textIn[i + 1] - 48)] = 2;
                            Console.WriteLine("" + (textIn[i] - 48) + "" + (textIn[i + 1] - 48) + " ");
                        }
                    }

                    //fixes enemy color and ship count
                    translateEnemyBoxes();
                    countEnemyShips();

                    //once placements have been sent back, client can place ships, host can attack
                    if (userType == 2)
                    {
                        attackButton.Enabled = true;
                        surrenderButton.Enabled = true;
                    }
                    else
                    {
                        placementButton.Enabled = true;
                        surrenderButton.Enabled = true;
                    }
                }
                else
                {
                    //enemy has surrendered
                    MessageBox.Show("Your opponent has surrendered, leaving game", "Victory");
                    //ends game
                    surrender();
                }
            }
            catch (Exception)
            {
                //network error
                MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                surrender();
            }
        }

        //Host gets information about which box of theirs that has been attacked
        async public void u2GetAttackData()
        {
            try
            {
                //receives data from enemy (either attack on box or if they have surrendered)
                byte[] dataIn = new byte[256];
                int totalBytes = await client.GetStream().ReadAsync(dataIn, 0, dataIn.Length);
                String textIn = Encoding.UTF8.GetString(dataIn, 0, totalBytes);
                Console.WriteLine(textIn);

                //if a new box has been attacked, change box value and character
                if (textIn != "surrender-gameEnd")
                {
                    if (textIn != "0")
                    {
                        userGameBoxList[textIn[0] - 48, textIn[1] - 48].Text = selectedSprites[textIn[2] - 48].ToString();
                        userBattleField[textIn[0] - 48, textIn[1] - 48] = textIn[2] - 48;
                    }
                    else
                    {
                        //enemy has skipped a round
                    }

                    //next round starts (Host always begins)
                    roundCounter++;
                    roundCountLabel.Text = roundCounter.ToString();

                    //checks enemy's and user's boards to determine winner
                    countEnemyShips();
                    countUserShips();
                    if(client.Connected)
                    {
                        checkWinner(enemyShipsLeft, userShipsLeft);
                    }
                    //if game is still going, continue playing
                    if (userType == 2)
                    {
                        attackButton.Enabled = true;
                        surrenderButton.Enabled = true;
                    }
                }
                else
                {
                    //enemy has surrendered
                    MessageBox.Show("Your opponent has surrendered, leaving game", "Victory");
                    surrender();
                }
            }
            catch (Exception)
            {
                //network error
                MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                surrender();
            }
        }

        //Client gets information about which box of theirs that has been attacked
        async public void u3GetAttackData()
        {
            try
            {
                //receives data from enemy (either attack on box or if they have surrendered)
                byte[] dataIn = new byte[256];
                int totalBytes = await client.GetStream().ReadAsync(dataIn, 0, dataIn.Length);
                String textIn = Encoding.UTF8.GetString(dataIn, 0, totalBytes);
                Console.WriteLine(textIn);

                //if a new box has been attacked, change box value and character
                if (textIn != "surrender-gameEnd")
                {
                    if (textIn != "0")
                    {
                        userGameBoxList[textIn[0] - 48, textIn[1] - 48].Text = selectedSprites[textIn[2] - 48].ToString();
                        userBattleField[textIn[0] - 48, textIn[1] - 48] = textIn[2] - 48;
                    }
                    else
                    {
                        //enemy has skipped a round
                    }

                    //client's turn
                    attackButton.Enabled = true;
                    surrenderButton.Enabled = true;
                }
                else
                {
                    //enemy has surrendered
                    MessageBox.Show("Your opponent has surrendered, leaving game", "Victory");
                    surrender();
                }
            }
            catch (Exception)
            {
                //network error
                MessageBox.Show("Your connection with the oponent has encountered an error, leaving game", "Connection error");
                surrender();
            }
        }

        //lets the user reveal randomized enemy ship
        private void helpButton_Click(object sender, EventArgs e)
        {
            assumeEmpty = true;
            //while no ship is found, continue randomizing cordinates to search
            while (assumeEmpty)
            {
                randomX = random.Next(10);
                randomY = random.Next(10);

                //if an invisible ship is found, ship becomes visible, loop stops
                if (enemyBattleField[randomY, randomX] == 2 && enemyGameBoxList[randomY, randomX].Text != selectedSprites[2].ToString())
                {
                    enemyGameBoxList[randomY, randomX].Text = selectedSprites[2].ToString();
                    assumeEmpty = false;
                }
                //else: checks if there are other invisible ships
                else
                {
                    //assumes that there are no ships left to reveal, if there is any ships left: while loop continues
                    assumeEmpty = false;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (enemyBattleField[i, j] == 2 && enemyGameBoxList[i, j].Text != selectedSprites[2].ToString())
                            {
                                assumeEmpty = true;
                            }
                        }
                    }
                    //if no more ships can be found, no more help button, info message
                    if (!assumeEmpty)
                    {
                        helpButton.Enabled = false;

                        for (int i = 0; i < languageList.Length; i++)
                        {
                            if (languageList[i])
                            {
                                switch (i)
                                {
                                    case 0:
                                        MessageBox.Show("All enemy ships have been revealed", "Ability disabled");
                                        break;

                                    case 1:
                                        MessageBox.Show("Alla fiendeskepp har redan avslöjats", "Ej tillgängligt");
                                        break;

                                    case 2:
                                        MessageBox.Show("Tous les navires d'ennemi ont déjà été révélés", "Pas disponible");
                                        break;

                                    case 3:
                                        MessageBox.Show("敵人的船看得見", "沒有");
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        //enemy gameboxes' click controls, shows coordinates and sets location to attack
        private void enemyGameBox1_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox2_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox3_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox4_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox5_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox6_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox7_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox8_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox9_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox10_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 0;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "1";
        }
        private void enemyGameBox11_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox12_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox13_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox14_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox15_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox16_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox17_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox18_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox19_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox20_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 1;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "2";
        }
        private void enemyGameBox21_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox22_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox23_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox24_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox25_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox26_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox27_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox28_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox29_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox30_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 2;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "3";
        }
        private void enemyGameBox31_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox32_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox33_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox34_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox35_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox36_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox37_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox38_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox39_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox40_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 3;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "4";
        }
        private void enemyGameBox41_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox42_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox43_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox44_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox45_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox46_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox47_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox48_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox49_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox50_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 4;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "5";
        }
        private void enemyGameBox51_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox52_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox53_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox54_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox55_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox56_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox57_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox58_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox59_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox60_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 5;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "6";
        }
        private void enemyGameBox61_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox62_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox63_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox64_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox65_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox66_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox67_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox68_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox69_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox70_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 6;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "7";
        }
        private void enemyGameBox71_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox72_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox73_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox74_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox75_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox76_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox77_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox78_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox79_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox80_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 7;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "8";
        }
        private void enemyGameBox81_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox82_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox83_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox84_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox85_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox86_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox87_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox88_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox89_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox90_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 8;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "9";
        }
        private void enemyGameBox91_Click(object sender, EventArgs e)
        {
            attackValueX = 0;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "1";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox92_Click(object sender, EventArgs e)
        {
            attackValueX = 1;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "2";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox93_Click(object sender, EventArgs e)
        {
            attackValueX = 2;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "3";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox94_Click(object sender, EventArgs e)
        {
            attackValueX = 3;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "4";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox95_Click(object sender, EventArgs e)
        {
            attackValueX = 4;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "5";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox96_Click(object sender, EventArgs e)
        {
            attackValueX = 5;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "6";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox97_Click(object sender, EventArgs e)
        {
            attackValueX = 6;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "7";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox98_Click(object sender, EventArgs e)
        {
            attackValueX = 7;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "8";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox99_Click(object sender, EventArgs e)
        {
            attackValueX = 8;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "9";
            yCoordTextBox.Text = "10";
        }
        private void enemyGameBox100_Click(object sender, EventArgs e)
        {
            attackValueX = 9;
            attackValueY = 9;
            enemyBoxClick = true;
            xCoordTextBox.Text = "10";
            yCoordTextBox.Text = "10";
        }
        //user gameboxes' click controls, if placement button is enabled ships can be placed and removed from the board
        private void userGameBox1_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 0] != 4)
                {
                    userGameBoxList[0, 0].Text = selectedSprites[4].ToString();
                    userBattleField[0, 0] = 4;
                }
                else
                {
                    userGameBoxList[0, 0].Text = selectedSprites[0].ToString();
                    userBattleField[0, 0] = 0;
                }

            }       }
        private void userGameBox2_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 1] != 4)
                {
                    userGameBoxList[0, 1].Text = selectedSprites[4].ToString();
                    userBattleField[0, 1] = 4;
                }
                else
                {
                    userGameBoxList[0, 1].Text = " ";
                    userBattleField[0, 1] = 0;
                }
            }
        }
        private void userGameBox3_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 2] != 4)
                {
                    userGameBoxList[0, 2].Text = selectedSprites[4].ToString();
                    userBattleField[0, 2] = 4;
                }
                else
                {
                    userGameBoxList[0, 2].Text = " ";
                    userBattleField[0, 2] = 0;
                }
            }
        }
        private void userGameBox4_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 3] != 4)
                {
                    userGameBoxList[0, 3].Text = selectedSprites[4].ToString();
                    userBattleField[0, 3] = 4;
                }
                else
                {
                    userGameBoxList[0, 3].Text = " ";
                    userBattleField[0, 3] = 0;
                }
            }
        }
        private void userGameBox5_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 4] != 4)
                {
                    userGameBoxList[0, 4].Text = selectedSprites[4].ToString();
                    userBattleField[0, 4] = 4;
                }
                else
                {
                    userGameBoxList[0, 4].Text = " ";
                    userBattleField[0, 4] = 0;
                }
            }
        }
        private void userGameBox6_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 5] != 4)
                {
                    userGameBoxList[0, 5].Text = selectedSprites[4].ToString();
                    userBattleField[0, 5] = 4;
                }
                else
                {
                    userGameBoxList[0, 5].Text = " ";
                    userBattleField[0, 5] = 0;
                }
            }
        }
        private void userGameBox7_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 6] != 4)
                {
                    userGameBoxList[0, 6].Text = selectedSprites[4].ToString();
                    userBattleField[0, 6] = 4;
                }
                else
                {
                    userGameBoxList[0, 6].Text = " ";
                    userBattleField[0, 6] = 0;
                }
            }
        }
        private void userGameBox8_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 7] != 4)
                {
                    userGameBoxList[0, 7].Text = selectedSprites[4].ToString();
                    userBattleField[0, 7] = 4;
                }
                else
                {
                    userGameBoxList[0, 7].Text = " ";
                    userBattleField[0, 7] = 0;
                }
            }
        }
        private void userGameBox9_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 8] != 4)
                {
                    userGameBoxList[0, 8].Text = selectedSprites[4].ToString();
                    userBattleField[0, 8] = 4;
                }
                else
                {
                    userGameBoxList[0, 8].Text = " ";
                    userBattleField[0, 8] = 0;
                }
            }
        }
        private void userGameBox10_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[0, 9] != 4)
                {
                    userGameBoxList[0, 9].Text = selectedSprites[4].ToString();
                    userBattleField[0, 9] = 4;
                }
                else
                {
                    userGameBoxList[0, 9].Text = " ";
                    userBattleField[0, 9] = 0;
                }
            }
        }
        private void userGameBox11_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 0] != 4)
                {
                    userGameBoxList[1, 0].Text = selectedSprites[4].ToString();
                    userBattleField[1, 0] = 4;
                }
                else
                {
                    userGameBoxList[1, 0].Text = " ";
                    userBattleField[1, 0] = 0;
                }
            }
        }
        private void userGameBox12_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 1] != 4)
                {
                    userGameBoxList[1, 1].Text = selectedSprites[4].ToString();
                    userBattleField[1, 1] = 4;
                }
                else
                {
                    userGameBoxList[1, 1].Text = " ";
                    userBattleField[1, 1] = 0;
                }
            }
        }
        private void userGameBox13_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 2] != 4)
                {
                    userGameBoxList[1, 2].Text = selectedSprites[4].ToString();
                    userBattleField[1, 2] = 4;
                }
                else
                {
                    userGameBoxList[1, 2].Text = " ";
                    userBattleField[1, 2] = 0;
                }
            }
        }
        private void userGameBox14_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 3] != 4)
                {
                    userGameBoxList[1, 3].Text = selectedSprites[4].ToString();
                    userBattleField[1, 3] = 4;
                }
                else
                {
                    userGameBoxList[1, 3].Text = " ";
                    userBattleField[1, 3] = 0;
                }
            }
        }
        private void userGameBox15_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 4] != 4)
                {
                    userGameBoxList[1, 4].Text = selectedSprites[4].ToString();
                    userBattleField[1, 4] = 4;
                }
                else
                {
                    userGameBoxList[1, 4].Text = " ";
                    userBattleField[1, 4] = 0;
                }
            }
        }
        private void userGameBox16_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 5] != 4)
                {
                    userGameBoxList[1, 5].Text = selectedSprites[4].ToString();
                    userBattleField[1, 5] = 4;
                }
                else
                {
                    userGameBoxList[1, 5].Text = " ";
                    userBattleField[1, 5] = 0;
                }
            }
        }
        private void userGameBox17_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 6] != 4)
                {
                    userGameBoxList[1, 6].Text = selectedSprites[4].ToString();
                    userBattleField[1, 6] = 4;
                }
                else
                {
                    userGameBoxList[1, 6].Text = " ";
                    userBattleField[1, 6] = 0;
                }
            }
        }
        private void userGameBox18_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 7] != 4)
                {
                    userGameBoxList[1, 7].Text = selectedSprites[4].ToString();
                    userBattleField[1, 7] = 4;
                }
                else
                {
                    userGameBoxList[1, 7].Text = " ";
                    userBattleField[1, 7] = 0;
                }
            }
        }
        private void userGameBox19_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 8] != 4)
                {
                    userGameBoxList[1, 8].Text = selectedSprites[4].ToString();
                    userBattleField[1, 8] = 4;
                }
                else
                {
                    userGameBoxList[1, 8].Text = " ";
                    userBattleField[1, 8] = 0;
                }
            }
        }
        private void userGameBox20_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[1, 9] != 4)
                {
                    userGameBoxList[1, 9].Text = selectedSprites[4].ToString();
                    userBattleField[1, 9] = 4;
                }
                else
                {
                    userGameBoxList[1, 9].Text = " ";
                    userBattleField[1, 9] = 0;
                }
            }
        }
        private void userGameBox21_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 0] != 4)
                {
                    userGameBoxList[2, 0].Text = selectedSprites[4].ToString();
                    userBattleField[2, 0] = 4;
                }
                else
                {
                    userGameBoxList[2, 0].Text = " ";
                    userBattleField[2, 0] = 0;
                }
            }
        }
        private void userGameBox22_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 1] != 4)
                {
                    userGameBoxList[2, 1].Text = selectedSprites[4].ToString();
                    userBattleField[2, 1] = 4;
                }
                else
                {
                    userGameBoxList[2, 1].Text = " ";
                    userBattleField[2, 1] = 0;
                }
            }
        }
        private void userGameBox23_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 2] != 4)
                {
                    userGameBoxList[2, 2].Text = selectedSprites[4].ToString();
                    userBattleField[2, 2] = 4;
                }
                else
                {
                    userGameBoxList[2, 2].Text = " ";
                    userBattleField[2, 2] = 0;
                }
            }
        }
        private void userGameBox24_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 3] != 4)
                {
                    userGameBoxList[2, 3].Text = selectedSprites[4].ToString();
                    userBattleField[2, 3] = 4;
                }
                else
                {
                    userGameBoxList[2, 3].Text = " ";
                    userBattleField[2, 3] = 0;
                }
            }
        }
        private void userGameBox25_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 4] != 4)
                {
                    userGameBoxList[2, 4].Text = selectedSprites[4].ToString();
                    userBattleField[2, 4] = 4;
                }
                else
                {
                    userGameBoxList[2, 4].Text = " ";
                    userBattleField[2, 4] = 0;
                }
            }
        }
        private void userGameBox26_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 5] != 4)
                {
                    userGameBoxList[2, 5].Text = selectedSprites[4].ToString();
                    userBattleField[2, 5] = 4;
                }
                else
                {
                    userGameBoxList[2, 5].Text = " ";
                    userBattleField[2, 5] = 0;
                }
            }
        }
        private void userGameBox27_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 6] != 4)
                {
                    userGameBoxList[2, 6].Text = selectedSprites[4].ToString();
                    userBattleField[2, 6] = 4;
                }
                else
                {
                    userGameBoxList[2, 6].Text = " ";
                    userBattleField[2, 6] = 0;
                }
            }
        }
        private void userGameBox28_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 7] != 4)
                {
                    userGameBoxList[2, 7].Text = selectedSprites[4].ToString();
                    userBattleField[2, 7] = 4;
                }
                else
                {
                    userGameBoxList[2, 7].Text = " ";
                    userBattleField[2, 7] = 0;
                }
            }
        }
        private void userGameBox29_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 8] != 4)
                {
                    userGameBoxList[2, 8].Text = selectedSprites[4].ToString();
                    userBattleField[2, 8] = 4;
                }
                else
                {
                    userGameBoxList[2, 8].Text = " ";
                    userBattleField[2, 8] = 0;
                }
            }
        }
        private void userGameBox30_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[2, 9] != 4)
                {
                    userGameBoxList[2, 9].Text = selectedSprites[4].ToString();
                    userBattleField[2, 9] = 4;
                }
                else
                {
                    userGameBoxList[2, 9].Text = " ";
                    userBattleField[2, 9] = 0;
                }
            }
        }
        private void userGameBox31_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 0] != 4)
                {
                    userGameBoxList[3, 0].Text = selectedSprites[4].ToString();
                    userBattleField[3, 0] = 4;
                }
                else
                {
                    userGameBoxList[3, 0].Text = " ";
                    userBattleField[3, 0] = 0;
                }
            }
        }
        private void userGameBox32_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 1] != 4)
                {
                    userGameBoxList[3, 1].Text = selectedSprites[4].ToString();
                    userBattleField[3, 1] = 4;
                }
                else
                {
                    userGameBoxList[3, 1].Text = " ";
                    userBattleField[3, 1] = 0;
                }
            }
        }
        private void userGameBox33_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 2] != 4)
                {
                    userGameBoxList[3, 2].Text = selectedSprites[4].ToString();
                    userBattleField[3, 2] = 4;
                }
                else
                {
                    userGameBoxList[3, 2].Text = " ";
                    userBattleField[3, 2] = 0;
                }
            }
        }
        private void userGameBox34_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 3] != 4)
                {
                    userGameBoxList[3, 3].Text = selectedSprites[4].ToString();
                    userBattleField[3, 3] = 4;
                }
                else
                {
                    userGameBoxList[3, 3].Text = " ";
                    userBattleField[3, 3] = 0;
                }
            }
        }
        private void userGameBox35_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 4] != 4)
                {
                    userGameBoxList[3, 4].Text = selectedSprites[4].ToString();
                    userBattleField[3, 4] = 4;
                }
                else
                {
                    userGameBoxList[3, 4].Text = " ";
                    userBattleField[3, 4] = 0;
                }
            }
        }
        private void userGameBox36_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 5] != 4)
                {
                    userGameBoxList[3, 5].Text = selectedSprites[4].ToString();
                    userBattleField[3, 5] = 4;
                }
                else
                {
                    userGameBoxList[3, 5].Text = " ";
                    userBattleField[3, 5] = 0;
                }
            }
        }
        private void userGameBox37_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 6] != 4)
                {
                    userGameBoxList[3, 6].Text = selectedSprites[4].ToString();
                    userBattleField[3, 6] = 4;
                }
                else
                {
                    userGameBoxList[3, 6].Text = " ";
                    userBattleField[3, 6] = 0;
                }
            }
        }
        private void userGameBox38_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 7] != 4)
                {
                    userGameBoxList[3, 7].Text = selectedSprites[4].ToString();
                    userBattleField[3, 7] = 4;
                }
                else
                {
                    userGameBoxList[3, 7].Text = " ";
                    userBattleField[3, 7] = 0;
                }
            }
        }
        private void userGameBox39_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 8] != 4)
                {
                    userGameBoxList[3, 8].Text = selectedSprites[4].ToString();
                    userBattleField[3, 8] = 4;
                }
                else
                {
                    userGameBoxList[3, 8].Text = " ";
                    userBattleField[3, 8] = 0;
                }
            }
        }
        private void userGameBox40_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[3, 9] != 4)
                {
                    userGameBoxList[3, 9].Text = selectedSprites[4].ToString();
                    userBattleField[3, 9] = 4;
                }
                else
                {
                    userGameBoxList[3, 9].Text = " ";
                    userBattleField[3, 9] = 0;
                }
            }
        }
        private void userGameBox41_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 0] != 4)
                {
                    userGameBoxList[4, 0].Text = selectedSprites[4].ToString();
                    userBattleField[4, 0] = 4;
                }
                else
                {
                    userGameBoxList[4, 0].Text = " ";
                    userBattleField[4, 0] = 0;
                }
            }
        }
        private void userGameBox42_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 1] != 4)
                {
                    userGameBoxList[4, 1].Text = selectedSprites[4].ToString();
                    userBattleField[4, 1] = 4;
                }
                else
                {
                    userGameBoxList[4, 1].Text = " ";
                    userBattleField[4, 1] = 0;
                }
            }
        }
        private void userGameBox43_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 2] != 4)
                {
                    userGameBoxList[4, 2].Text = selectedSprites[4].ToString();
                    userBattleField[4, 2] = 4;
                }
                else
                {
                    userGameBoxList[4, 2].Text = " ";
                    userBattleField[4, 2] = 0;
                }
            }
        }
        private void userGameBox44_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 3] != 4)
                {
                    userGameBoxList[4, 3].Text = selectedSprites[4].ToString();
                    userBattleField[4, 3] = 4;
                }
                else
                {
                    userGameBoxList[4, 3].Text = " ";
                    userBattleField[4, 3] = 0;
                }
            }
        }
        private void userGameBox45_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 4] != 4)
                {
                    userGameBoxList[4, 4].Text = selectedSprites[4].ToString();
                    userBattleField[4, 4] = 4;
                }
                else
                {
                    userGameBoxList[4, 4].Text = " ";
                    userBattleField[4, 4] = 0;
                }
            }
        }
        private void userGameBox46_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 5] != 4)
                {
                    userGameBoxList[4, 5].Text = selectedSprites[4].ToString();
                    userBattleField[4, 5] = 4;
                }
                else
                {
                    userGameBoxList[4, 5].Text = " ";
                    userBattleField[4, 5] = 0;
                }
            }
        }
        private void userGameBox47_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 6] != 4)
                {
                    userGameBoxList[4, 6].Text = selectedSprites[4].ToString();
                    userBattleField[4, 6] = 4;
                }
                else
                {
                    userGameBoxList[4, 6].Text = " ";
                    userBattleField[4, 6] = 0;
                }
            }
        }
        private void userGameBox48_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 7] != 4)
                {
                    userGameBoxList[4, 7].Text = selectedSprites[4].ToString();
                    userBattleField[4, 7] = 4;
                }
                else
                {
                    userGameBoxList[4, 7].Text = " ";
                    userBattleField[4, 7] = 0;
                }
            }
        }
        private void userGameBox49_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 8] != 4)
                {
                    userGameBoxList[4, 8].Text = selectedSprites[4].ToString();
                    userBattleField[4, 8] = 4;
                }
                else
                {
                    userGameBoxList[4, 8].Text = " ";
                    userBattleField[4, 8] = 0;
                }
            }
        }
        private void userGameBox50_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[4, 9] != 4)
                {
                    userGameBoxList[4, 9].Text = selectedSprites[4].ToString();
                    userBattleField[4, 9] = 4;
                }
                else
                {
                    userGameBoxList[4, 9].Text = " ";
                    userBattleField[4, 9] = 0;
                }
            }
        }
        private void userGameBox51_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 0] != 4)
                {
                    userGameBoxList[5, 0].Text = selectedSprites[4].ToString();
                    userBattleField[5, 0] = 4;
                }
                else
                {
                    userGameBoxList[5, 0].Text = " ";
                    userBattleField[5, 0] = 0;
                }
            }
        }
        private void userGameBox52_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 1] != 4)
                {
                    userGameBoxList[5, 1].Text = selectedSprites[4].ToString();
                    userBattleField[5, 1] = 4;
                }
                else
                {
                    userGameBoxList[5, 1].Text = " ";
                    userBattleField[5, 1] = 0;
                }
            }
        }
        private void userGameBox53_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 2] != 4)
                {
                    userGameBoxList[5, 2].Text = selectedSprites[4].ToString();
                    userBattleField[5, 2] = 4;
                }
                else
                {
                    userGameBoxList[5, 2].Text = " ";
                    userBattleField[5, 2] = 0;
                }
            }
        }
        private void userGameBox54_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 3] != 4)
                {
                    userGameBoxList[5, 3].Text = selectedSprites[4].ToString();
                    userBattleField[5, 3] = 4;
                }
                else
                {
                    userGameBoxList[5, 3].Text = " ";
                    userBattleField[5, 3] = 0;
                }
            }
        }
        private void userGameBox55_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 4] != 4)
                {
                    userGameBoxList[5, 4].Text = selectedSprites[4].ToString();
                    userBattleField[5, 4] = 4;
                }
                else
                {
                    userGameBoxList[5, 4].Text = " ";
                    userBattleField[5, 4] = 0;
                }
            }
        }
        private void userGameBox56_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 5] != 4)
                {
                    userGameBoxList[5, 5].Text = selectedSprites[4].ToString();
                    userBattleField[5, 5] = 4;
                }
                else
                {
                    userGameBoxList[5, 5].Text = " ";
                    userBattleField[5, 5] = 0;
                }
            }
        }
        private void userGameBox57_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 6] != 4)
                {
                    userGameBoxList[5, 6].Text = selectedSprites[4].ToString();
                    userBattleField[5, 6] = 4;
                }
                else
                {
                    userGameBoxList[5, 6].Text = " ";
                    userBattleField[5, 6] = 0;
                }
            }
        }
        private void userGameBox58_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 7] != 4)
                {
                    userGameBoxList[5, 7].Text = selectedSprites[4].ToString();
                    userBattleField[5, 7] = 4;
                }
                else
                {
                    userGameBoxList[5, 7].Text = " ";
                    userBattleField[5, 7] = 0;
                }
            }
        }
        private void userGameBox59_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 8] != 4)
                {
                    userGameBoxList[5, 8].Text = selectedSprites[4].ToString();
                    userBattleField[5, 8] = 4;
                }
                else
                {
                    userGameBoxList[5, 8].Text = " ";
                    userBattleField[5, 8] = 0;
                }
            }
        }
        private void userGameBox60_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[5, 9] != 4)
                {
                    userGameBoxList[5, 9].Text = selectedSprites[4].ToString();
                    userBattleField[5, 9] = 4;
                }
                else
                {
                    userGameBoxList[5, 9].Text = " ";
                    userBattleField[5, 9] = 0;
                }
            }
        }
        private void userGameBox61_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 0] != 4)
                {
                    userGameBoxList[6, 0].Text = selectedSprites[4].ToString();
                    userBattleField[6, 0] = 4;
                }
                else
                {
                    userGameBoxList[6, 0].Text = " ";
                    userBattleField[6, 0] = 0;
                }
            }
        }
        private void userGameBox62_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 1] != 4)
                {
                    userGameBoxList[6, 1].Text = selectedSprites[4].ToString();
                    userBattleField[6, 1] = 4;
                }
                else
                {
                    userGameBoxList[6, 1].Text = " ";
                    userBattleField[6, 1] = 0;
                }
            }
        }
        private void userGameBox63_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 2] != 4)
                {
                    userGameBoxList[6, 2].Text = selectedSprites[4].ToString();
                    userBattleField[6, 2] = 4;
                }
                else
                {
                    userGameBoxList[6, 2].Text = " ";
                    userBattleField[6, 2] = 0;
                }
            }
        }
        private void userGameBox64_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 3] != 4)
                {
                    userGameBoxList[6, 3].Text = selectedSprites[4].ToString();
                    userBattleField[6, 3] = 4;
                }
                else
                {
                    userGameBoxList[6, 3].Text = " ";
                    userBattleField[6, 3] = 0;
                }
            }
        }
        private void userGameBox65_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 4] != 4)
                {
                    userGameBoxList[6, 4].Text = selectedSprites[4].ToString();
                    userBattleField[6, 4] = 4;
                }
                else
                {
                    userGameBoxList[6, 4].Text = " ";
                    userBattleField[6, 4] = 0;
                }
            }
        }
        private void userGameBox66_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 5] != 4)
                {
                    userGameBoxList[6, 5].Text = selectedSprites[4].ToString();
                    userBattleField[6, 5] = 4;
                }
                else
                {
                    userGameBoxList[6, 5].Text = " ";
                    userBattleField[6, 5] = 0;
                }
            }
        }
        private void userGameBox67_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 6] != 4)
                {
                    userGameBoxList[6, 6].Text = selectedSprites[4].ToString();
                    userBattleField[6, 6] = 4;
                }
                else
                {
                    userGameBoxList[6, 6].Text = " ";
                    userBattleField[6, 6] = 0;
                }
            }
        }
        private void userGameBox68_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 7] != 4)
                {
                    userGameBoxList[6, 7].Text = selectedSprites[4].ToString();
                    userBattleField[6, 7] = 4;
                }
                else
                {
                    userGameBoxList[6, 7].Text = " ";
                    userBattleField[6, 7] = 0;
                }
            }
        }
        private void userGameBox69_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 8] != 4)
                {
                    userGameBoxList[6, 8].Text = selectedSprites[4].ToString();
                    userBattleField[6, 8] = 4;
                }
                else
                {
                    userGameBoxList[6, 8].Text = " ";
                    userBattleField[6, 8] = 0;
                }
            }
        }
        private void userGameBox70_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[6, 9] != 4)
                {
                    userGameBoxList[6, 9].Text = selectedSprites[4].ToString();
                    userBattleField[6, 9] = 4;
                }
                else
                {
                    userGameBoxList[6, 9].Text = " ";
                    userBattleField[6, 9] = 0;
                }
            }
        }
        private void userGameBox71_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 0] != 4)
                {
                    userGameBoxList[7, 0].Text = selectedSprites[4].ToString();
                    userBattleField[7, 0] = 4;
                }
                else
                {
                    userGameBoxList[7, 0].Text = " ";
                    userBattleField[7, 0] = 0;
                }
            }
        }
        private void userGameBox72_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 1] != 4)
                {
                    userGameBoxList[7, 1].Text = selectedSprites[4].ToString();
                    userBattleField[7, 1] = 4;
                }
                else
                {
                    userGameBoxList[7, 1].Text = " ";
                    userBattleField[7, 1] = 0;
                }
            }
        }
        private void userGameBox73_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 2] != 4)
                {
                    userGameBoxList[7, 2].Text = selectedSprites[4].ToString();
                    userBattleField[7, 2] = 4;
                }
                else
                {
                    userGameBoxList[7, 2].Text = " ";
                    userBattleField[7, 2] = 0;
                }
            }
        }
        private void userGameBox74_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 3] != 4)
                {
                    userGameBoxList[7, 3].Text = selectedSprites[4].ToString();
                    userBattleField[7, 3] = 4;
                }
                else
                {
                    userGameBoxList[7, 3].Text = " ";
                    userBattleField[7, 3] = 0;
                }
            }
        }
        private void userGameBox75_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 4] != 4)
                {
                    userGameBoxList[7, 4].Text = selectedSprites[4].ToString();
                    userBattleField[7, 4] = 4;
                }
                else
                {
                    userGameBoxList[7, 4].Text = " ";
                    userBattleField[7, 4] = 0;
                }
            }
        }
        private void userGameBox76_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 5] != 4)
                {
                    userGameBoxList[7, 5].Text = selectedSprites[4].ToString();
                    userBattleField[7, 5] = 4;
                }
                else
                {
                    userGameBoxList[7, 5].Text = " ";
                    userBattleField[7, 5] = 0;
                }
            }
        }
        private void userGameBox77_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 6] != 4)
                {
                    userGameBoxList[7, 6].Text = selectedSprites[4].ToString();
                    userBattleField[7, 6] = 4;
                }
                else
                {
                    userGameBoxList[7, 6].Text = " ";
                    userBattleField[7, 6] = 0;
                }
            }
        }
        private void userGameBox78_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 7] != 4)
                {
                    userGameBoxList[7, 7].Text = selectedSprites[4].ToString();
                    userBattleField[7, 7] = 4;
                }
                else
                {
                    userGameBoxList[7, 7].Text = " ";
                    userBattleField[7, 7] = 0;
                }
            }
        }
        private void userGameBox79_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 8] != 4)
                {
                    userGameBoxList[7, 8].Text = selectedSprites[4].ToString();
                    userBattleField[7, 8] = 4;
                }
                else
                {
                    userGameBoxList[7, 8].Text = " ";
                    userBattleField[7, 8] = 0;
                }
            }
        }
        private void userGameBox80_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[7, 9] != 4)
                {
                    userGameBoxList[7, 9].Text = selectedSprites[4].ToString();
                    userBattleField[7, 9] = 4;
                }
                else
                {
                    userGameBoxList[7, 9].Text = " ";
                    userBattleField[7, 9] = 0;
                }
            }
        }
        private void userGameBox81_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 0] != 4)
                {
                    userGameBoxList[8, 0].Text = selectedSprites[4].ToString();
                    userBattleField[8, 0] = 4;
                }
                else
                {
                    userGameBoxList[8, 0].Text = " ";
                    userBattleField[8, 0] = 0;
                }
            }
        }
        private void userGameBox82_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 1] != 4)
                {
                    userGameBoxList[8, 1].Text = selectedSprites[4].ToString();
                    userBattleField[8, 1] = 4;
                }
                else
                {
                    userGameBoxList[8, 1].Text = " ";
                    userBattleField[8, 1] = 0;
                }
            }
        }
        private void userGameBox83_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 2] != 4)
                {
                    userGameBoxList[8, 2].Text = selectedSprites[4].ToString();
                    userBattleField[8, 2] = 4;
                }
                else
                {
                    userGameBoxList[8, 2].Text = " ";
                    userBattleField[8, 2] = 0;
                }
            }
        }
        private void userGameBox84_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 3] != 4)
                {
                    userGameBoxList[8, 3].Text = selectedSprites[4].ToString();
                    userBattleField[8, 3] = 4;
                }
                else
                {
                    userGameBoxList[8, 3].Text = " ";
                    userBattleField[8, 3] = 0;
                }
            }
        }
        private void userGameBox85_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 4] != 4)
                {
                    userGameBoxList[8, 4].Text = selectedSprites[4].ToString();
                    userBattleField[8, 4] = 4;
                }
                else
                {
                    userGameBoxList[8, 4].Text = " ";
                    userBattleField[8, 4] = 0;
                }
            }
        }
        private void userGameBox86_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 5] != 4)
                {
                    userGameBoxList[8, 5].Text = selectedSprites[4].ToString();
                    userBattleField[8, 5] = 4;
                }
                else
                {
                    userGameBoxList[8, 5].Text = " ";
                    userBattleField[8, 5] = 0;
                }
            }
        }
        private void userGameBox87_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 6] != 4)
                {
                    userGameBoxList[8, 6].Text = selectedSprites[4].ToString();
                    userBattleField[8, 6] = 4;
                }
                else
                {
                    userGameBoxList[8, 6].Text = " ";
                    userBattleField[8, 6] = 0;
                }
            }
        }
        private void userGameBox88_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 7] != 4)
                {
                    userGameBoxList[8, 7].Text = selectedSprites[4].ToString();
                    userBattleField[8, 7] = 4;
                }
                else
                {
                    userGameBoxList[8, 7].Text = " ";
                    userBattleField[8, 7] = 0;
                }
            }
        }
        private void userGameBox89_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 8] != 4)
                {
                    userGameBoxList[8, 8].Text = selectedSprites[4].ToString();
                    userBattleField[8, 8] = 4;
                }
                else
                {
                    userGameBoxList[8, 8].Text = " ";
                    userBattleField[8, 8] = 0;
                }
            }
        }
        private void userGameBox90_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[8, 9] != 4)
                {
                    userGameBoxList[8, 9].Text = selectedSprites[4].ToString();
                    userBattleField[8, 9] = 4;
                }
                else
                {
                    userGameBoxList[8, 9].Text = " ";
                    userBattleField[8, 9] = 0;
                }
            }
        }
        private void userGameBox91_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 0] != 4)
                {
                    userGameBoxList[9, 0].Text = selectedSprites[4].ToString();
                    userBattleField[9, 0] = 4;
                }
                else
                {
                    userGameBoxList[9, 0].Text = " ";
                    userBattleField[9, 0] = 0;
                }
            }
        }
        private void userGameBox92_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 1] != 4)
                {
                    userGameBoxList[9, 1].Text = selectedSprites[4].ToString();
                    userBattleField[9, 1] = 4;
                }
                else
                {
                    userGameBoxList[9, 1].Text = " ";
                    userBattleField[9, 1] = 0;
                }
            }
        }
        private void userGameBox93_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 2] != 4)
                {
                    userGameBoxList[9, 2].Text = selectedSprites[4].ToString();
                    userBattleField[9, 2] = 4;
                }
                else
                {
                    userGameBoxList[9, 2].Text = " ";
                    userBattleField[9, 2] = 0;
                }
            }
        }
        private void userGameBox94_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 3] != 4)
                {
                    userGameBoxList[9, 3].Text = selectedSprites[4].ToString();
                    userBattleField[9, 3] = 4;
                }
                else
                {
                    userGameBoxList[9, 3].Text = " ";
                    userBattleField[9, 3] = 0;
                }
            }
        }
        private void userGameBox95_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 4] != 4)
                {
                    userGameBoxList[9, 4].Text = selectedSprites[4].ToString();
                    userBattleField[9, 4] = 4;
                }
                else
                {
                    userGameBoxList[9, 4].Text = " ";
                    userBattleField[9, 4] = 0;
                }
            }
        }
        private void userGameBox96_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 5] != 4)
                {
                    userGameBoxList[9, 5].Text = selectedSprites[4].ToString();
                    userBattleField[9, 5] = 4;
                }
                else
                {
                    userGameBoxList[9, 5].Text = " ";
                    userBattleField[9, 5] = 0;
                }
            }
        }

        private void userGameBox97_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 6] != 4)
                {
                    userGameBoxList[9, 6].Text = selectedSprites[4].ToString();
                    userBattleField[9, 6] = 4;
                }
                else
                {
                    userGameBoxList[9, 6].Text = " ";
                    userBattleField[9, 6] = 0;
                }
            }
        }
        private void userGameBox98_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 7] != 4)
                {
                    userGameBoxList[9, 7].Text = selectedSprites[4].ToString();
                    userBattleField[9, 7] = 4;
                }
                else
                {
                    userGameBoxList[9, 7].Text = " ";
                    userBattleField[9, 7] = 0;
                }
            }
        }
        private void userGameBox99_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 8] != 4)
                {
                    userGameBoxList[9, 8].Text = selectedSprites[4].ToString();
                    userBattleField[9, 8] = 4;
                }
                else
                {
                    userGameBoxList[9, 8].Text = " ";
                    userBattleField[9, 8] = 0;
                }
            }
        }
        private void userGameBox100_Click(object sender, EventArgs e)
        {
            if (placementButton.Enabled)
            {
                if (userBattleField[9, 9] != 4)
                {
                    userGameBoxList[9, 9].Text = selectedSprites[4].ToString();
                    userBattleField[9, 9] = 4;
                }
                else
                {
                    userGameBoxList[9, 9].Text = " ";
                    userBattleField[9, 9] = 0;
                }
            }
        }
    }
}
