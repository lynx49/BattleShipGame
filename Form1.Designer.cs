namespace BattleShipGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.customizeShipsButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.quitGameButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customizeShipsPanel = new System.Windows.Forms.Panel();
            this.uForeColorBox = new System.Windows.Forms.Label();
            this.eForeColorBox = new System.Windows.Forms.Label();
            this.uChangeCharButton = new System.Windows.Forms.Button();
            this.uSelectForeColorButton = new System.Windows.Forms.Button();
            this.uCharBox = new System.Windows.Forms.TextBox();
            this.eChangeCharButton = new System.Windows.Forms.Button();
            this.eSelectForeColorButton = new System.Windows.Forms.Button();
            this.eCharBox = new System.Windows.Forms.TextBox();
            this.uForeColorLabel = new System.Windows.Forms.Label();
            this.uCharLabel = new System.Windows.Forms.Label();
            this.eForeColorLabel = new System.Windows.Forms.Label();
            this.eCharLabel = new System.Windows.Forms.Label();
            this.uShipLabel = new System.Windows.Forms.Label();
            this.eShipLabel = new System.Windows.Forms.Label();
            this.changeShipLabel = new System.Windows.Forms.Label();
            this.csHomeButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.rButtonMandarin = new System.Windows.Forms.RadioButton();
            this.sHomeButton = new System.Windows.Forms.Button();
            this.rButtonFrench = new System.Windows.Forms.RadioButton();
            this.rButtonSwedish = new System.Windows.Forms.RadioButton();
            this.rButtonEnglish = new System.Windows.Forms.RadioButton();
            this.yLanguageButton = new System.Windows.Forms.Button();
            this.languageLabel = new System.Windows.Forms.Label();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.enemyShipsLeftCountLabel = new System.Windows.Forms.Label();
            this.enemyShipsLeftLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yCoordTextBox = new System.Windows.Forms.TextBox();
            this.attackButton = new System.Windows.Forms.Button();
            this.xCoordTextBox = new System.Windows.Forms.TextBox();
            this.gamePanel2 = new System.Windows.Forms.Panel();
            this.eBattleFieldLabel = new System.Windows.Forms.Label();
            this.roundCountLabel = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.mainGamePanel = new System.Windows.Forms.Panel();
            this.helpButton = new System.Windows.Forms.Button();
            this.placementButton = new System.Windows.Forms.Button();
            this.userShipsLeftCountLabel = new System.Windows.Forms.Label();
            this.userShipsLeftLabel = new System.Windows.Forms.Label();
            this.surrenderButton = new System.Windows.Forms.Button();
            this.gamePanel3 = new System.Windows.Forms.Panel();
            this.uBattleFieldLabel = new System.Windows.Forms.Label();
            this.eForeColorDialog = new System.Windows.Forms.ColorDialog();
            this.uForeColorDialog = new System.Windows.Forms.ColorDialog();
            this.informationButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.customizeShipsPanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.gamePanel2.SuspendLayout();
            this.mainGamePanel.SuspendLayout();
            this.gamePanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // customizeShipsButton
            // 
            resources.ApplyResources(this.customizeShipsButton, "customizeShipsButton");
            this.customizeShipsButton.Name = "customizeShipsButton";
            this.customizeShipsButton.UseVisualStyleBackColor = true;
            this.customizeShipsButton.Click += new System.EventHandler(this.customizeShipsButton_Click);
            // 
            // settingsButton
            // 
            resources.ApplyResources(this.settingsButton, "settingsButton");
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // quitGameButton
            // 
            resources.ApplyResources(this.quitGameButton, "quitGameButton");
            this.quitGameButton.Name = "quitGameButton";
            this.quitGameButton.UseVisualStyleBackColor = true;
            this.quitGameButton.Click += new System.EventHandler(this.quitGameButton_Click);
            // 
            // startGameButton
            // 
            resources.ApplyResources(this.startGameButton, "startGameButton");
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.informationButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.startGameButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.quitGameButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.settingsButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customizeShipsButton, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // customizeShipsPanel
            // 
            this.customizeShipsPanel.Controls.Add(this.uForeColorBox);
            this.customizeShipsPanel.Controls.Add(this.eForeColorBox);
            this.customizeShipsPanel.Controls.Add(this.uChangeCharButton);
            this.customizeShipsPanel.Controls.Add(this.uSelectForeColorButton);
            this.customizeShipsPanel.Controls.Add(this.uCharBox);
            this.customizeShipsPanel.Controls.Add(this.eChangeCharButton);
            this.customizeShipsPanel.Controls.Add(this.eSelectForeColorButton);
            this.customizeShipsPanel.Controls.Add(this.eCharBox);
            this.customizeShipsPanel.Controls.Add(this.uForeColorLabel);
            this.customizeShipsPanel.Controls.Add(this.uCharLabel);
            this.customizeShipsPanel.Controls.Add(this.eForeColorLabel);
            this.customizeShipsPanel.Controls.Add(this.eCharLabel);
            this.customizeShipsPanel.Controls.Add(this.uShipLabel);
            this.customizeShipsPanel.Controls.Add(this.eShipLabel);
            this.customizeShipsPanel.Controls.Add(this.changeShipLabel);
            this.customizeShipsPanel.Controls.Add(this.csHomeButton);
            resources.ApplyResources(this.customizeShipsPanel, "customizeShipsPanel");
            this.customizeShipsPanel.Name = "customizeShipsPanel";
            // 
            // uForeColorBox
            // 
            this.uForeColorBox.BackColor = System.Drawing.Color.White;
            this.uForeColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.uForeColorBox, "uForeColorBox");
            this.uForeColorBox.Name = "uForeColorBox";
            // 
            // eForeColorBox
            // 
            this.eForeColorBox.BackColor = System.Drawing.Color.White;
            this.eForeColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.eForeColorBox, "eForeColorBox");
            this.eForeColorBox.Name = "eForeColorBox";
            // 
            // uChangeCharButton
            // 
            resources.ApplyResources(this.uChangeCharButton, "uChangeCharButton");
            this.uChangeCharButton.Name = "uChangeCharButton";
            this.uChangeCharButton.UseVisualStyleBackColor = true;
            this.uChangeCharButton.Click += new System.EventHandler(this.uChangeCharButton_Click);
            // 
            // uSelectForeColorButton
            // 
            resources.ApplyResources(this.uSelectForeColorButton, "uSelectForeColorButton");
            this.uSelectForeColorButton.Name = "uSelectForeColorButton";
            this.uSelectForeColorButton.UseVisualStyleBackColor = true;
            this.uSelectForeColorButton.Click += new System.EventHandler(this.uSelectForeColorButton_Click);
            // 
            // uCharBox
            // 
            resources.ApplyResources(this.uCharBox, "uCharBox");
            this.uCharBox.Name = "uCharBox";
            // 
            // eChangeCharButton
            // 
            resources.ApplyResources(this.eChangeCharButton, "eChangeCharButton");
            this.eChangeCharButton.Name = "eChangeCharButton";
            this.eChangeCharButton.UseVisualStyleBackColor = true;
            this.eChangeCharButton.Click += new System.EventHandler(this.eChangeCharButton_Click);
            // 
            // eSelectForeColorButton
            // 
            resources.ApplyResources(this.eSelectForeColorButton, "eSelectForeColorButton");
            this.eSelectForeColorButton.Name = "eSelectForeColorButton";
            this.eSelectForeColorButton.UseVisualStyleBackColor = true;
            this.eSelectForeColorButton.Click += new System.EventHandler(this.eSelectForeColorButton_Click);
            // 
            // eCharBox
            // 
            resources.ApplyResources(this.eCharBox, "eCharBox");
            this.eCharBox.Name = "eCharBox";
            // 
            // uForeColorLabel
            // 
            resources.ApplyResources(this.uForeColorLabel, "uForeColorLabel");
            this.uForeColorLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.uForeColorLabel.Name = "uForeColorLabel";
            // 
            // uCharLabel
            // 
            resources.ApplyResources(this.uCharLabel, "uCharLabel");
            this.uCharLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.uCharLabel.Name = "uCharLabel";
            // 
            // eForeColorLabel
            // 
            resources.ApplyResources(this.eForeColorLabel, "eForeColorLabel");
            this.eForeColorLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.eForeColorLabel.Name = "eForeColorLabel";
            // 
            // eCharLabel
            // 
            resources.ApplyResources(this.eCharLabel, "eCharLabel");
            this.eCharLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.eCharLabel.Name = "eCharLabel";
            // 
            // uShipLabel
            // 
            resources.ApplyResources(this.uShipLabel, "uShipLabel");
            this.uShipLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.uShipLabel.Name = "uShipLabel";
            // 
            // eShipLabel
            // 
            resources.ApplyResources(this.eShipLabel, "eShipLabel");
            this.eShipLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.eShipLabel.Name = "eShipLabel";
            // 
            // changeShipLabel
            // 
            resources.ApplyResources(this.changeShipLabel, "changeShipLabel");
            this.changeShipLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.changeShipLabel.Name = "changeShipLabel";
            // 
            // csHomeButton
            // 
            resources.ApplyResources(this.csHomeButton, "csHomeButton");
            this.csHomeButton.Name = "csHomeButton";
            this.csHomeButton.UseVisualStyleBackColor = true;
            this.csHomeButton.Click += new System.EventHandler(this.csHomeButton_Click);
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.rButtonMandarin);
            this.settingsPanel.Controls.Add(this.sHomeButton);
            this.settingsPanel.Controls.Add(this.rButtonFrench);
            this.settingsPanel.Controls.Add(this.rButtonSwedish);
            this.settingsPanel.Controls.Add(this.rButtonEnglish);
            this.settingsPanel.Controls.Add(this.yLanguageButton);
            this.settingsPanel.Controls.Add(this.languageLabel);
            resources.ApplyResources(this.settingsPanel, "settingsPanel");
            this.settingsPanel.Name = "settingsPanel";
            // 
            // rButtonMandarin
            // 
            resources.ApplyResources(this.rButtonMandarin, "rButtonMandarin");
            this.rButtonMandarin.BackColor = System.Drawing.Color.Gainsboro;
            this.rButtonMandarin.Name = "rButtonMandarin";
            this.rButtonMandarin.TabStop = true;
            this.rButtonMandarin.UseVisualStyleBackColor = false;
            // 
            // sHomeButton
            // 
            resources.ApplyResources(this.sHomeButton, "sHomeButton");
            this.sHomeButton.Name = "sHomeButton";
            this.sHomeButton.UseVisualStyleBackColor = true;
            this.sHomeButton.Click += new System.EventHandler(this.sHomeButton_Click);
            // 
            // rButtonFrench
            // 
            resources.ApplyResources(this.rButtonFrench, "rButtonFrench");
            this.rButtonFrench.BackColor = System.Drawing.Color.Gainsboro;
            this.rButtonFrench.Name = "rButtonFrench";
            this.rButtonFrench.TabStop = true;
            this.rButtonFrench.UseVisualStyleBackColor = false;
            // 
            // rButtonSwedish
            // 
            resources.ApplyResources(this.rButtonSwedish, "rButtonSwedish");
            this.rButtonSwedish.BackColor = System.Drawing.Color.Gainsboro;
            this.rButtonSwedish.Name = "rButtonSwedish";
            this.rButtonSwedish.TabStop = true;
            this.rButtonSwedish.UseVisualStyleBackColor = false;
            // 
            // rButtonEnglish
            // 
            resources.ApplyResources(this.rButtonEnglish, "rButtonEnglish");
            this.rButtonEnglish.BackColor = System.Drawing.Color.Gainsboro;
            this.rButtonEnglish.Name = "rButtonEnglish";
            this.rButtonEnglish.TabStop = true;
            this.rButtonEnglish.UseVisualStyleBackColor = false;
            // 
            // yLanguageButton
            // 
            resources.ApplyResources(this.yLanguageButton, "yLanguageButton");
            this.yLanguageButton.Name = "yLanguageButton";
            this.yLanguageButton.UseVisualStyleBackColor = true;
            this.yLanguageButton.Click += new System.EventHandler(this.yLanguageButton_Click);
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.languageLabel.Name = "languageLabel";
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.enemyShipsLeftCountLabel);
            this.gamePanel.Controls.Add(this.enemyShipsLeftLabel);
            this.gamePanel.Controls.Add(this.label2);
            this.gamePanel.Controls.Add(this.label1);
            this.gamePanel.Controls.Add(this.yCoordTextBox);
            this.gamePanel.Controls.Add(this.attackButton);
            this.gamePanel.Controls.Add(this.xCoordTextBox);
            resources.ApplyResources(this.gamePanel, "gamePanel");
            this.gamePanel.Name = "gamePanel";
            // 
            // enemyShipsLeftCountLabel
            // 
            resources.ApplyResources(this.enemyShipsLeftCountLabel, "enemyShipsLeftCountLabel");
            this.enemyShipsLeftCountLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.enemyShipsLeftCountLabel.Name = "enemyShipsLeftCountLabel";
            // 
            // enemyShipsLeftLabel
            // 
            resources.ApplyResources(this.enemyShipsLeftLabel, "enemyShipsLeftLabel");
            this.enemyShipsLeftLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.enemyShipsLeftLabel.Name = "enemyShipsLeftLabel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // yCoordTextBox
            // 
            resources.ApplyResources(this.yCoordTextBox, "yCoordTextBox");
            this.yCoordTextBox.Name = "yCoordTextBox";
            // 
            // attackButton
            // 
            resources.ApplyResources(this.attackButton, "attackButton");
            this.attackButton.Name = "attackButton";
            this.attackButton.UseVisualStyleBackColor = true;
            this.attackButton.Click += new System.EventHandler(this.attackButton_Click);
            // 
            // xCoordTextBox
            // 
            resources.ApplyResources(this.xCoordTextBox, "xCoordTextBox");
            this.xCoordTextBox.Name = "xCoordTextBox";
            // 
            // gamePanel2
            // 
            this.gamePanel2.Controls.Add(this.eBattleFieldLabel);
            resources.ApplyResources(this.gamePanel2, "gamePanel2");
            this.gamePanel2.Name = "gamePanel2";
            // 
            // eBattleFieldLabel
            // 
            resources.ApplyResources(this.eBattleFieldLabel, "eBattleFieldLabel");
            this.eBattleFieldLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.eBattleFieldLabel.Name = "eBattleFieldLabel";
            // 
            // roundCountLabel
            // 
            resources.ApplyResources(this.roundCountLabel, "roundCountLabel");
            this.roundCountLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.roundCountLabel.Name = "roundCountLabel";
            // 
            // roundLabel
            // 
            resources.ApplyResources(this.roundLabel, "roundLabel");
            this.roundLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.roundLabel.Name = "roundLabel";
            // 
            // mainGamePanel
            // 
            this.mainGamePanel.Controls.Add(this.helpButton);
            this.mainGamePanel.Controls.Add(this.placementButton);
            this.mainGamePanel.Controls.Add(this.userShipsLeftCountLabel);
            this.mainGamePanel.Controls.Add(this.userShipsLeftLabel);
            this.mainGamePanel.Controls.Add(this.roundCountLabel);
            this.mainGamePanel.Controls.Add(this.surrenderButton);
            this.mainGamePanel.Controls.Add(this.gamePanel3);
            this.mainGamePanel.Controls.Add(this.roundLabel);
            this.mainGamePanel.Controls.Add(this.gamePanel2);
            this.mainGamePanel.Controls.Add(this.gamePanel);
            resources.ApplyResources(this.mainGamePanel, "mainGamePanel");
            this.mainGamePanel.Name = "mainGamePanel";
            // 
            // helpButton
            // 
            resources.ApplyResources(this.helpButton, "helpButton");
            this.helpButton.Name = "helpButton";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // placementButton
            // 
            resources.ApplyResources(this.placementButton, "placementButton");
            this.placementButton.Name = "placementButton";
            this.placementButton.UseVisualStyleBackColor = true;
            this.placementButton.Click += new System.EventHandler(this.placementButton_Click);
            // 
            // userShipsLeftCountLabel
            // 
            resources.ApplyResources(this.userShipsLeftCountLabel, "userShipsLeftCountLabel");
            this.userShipsLeftCountLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.userShipsLeftCountLabel.Name = "userShipsLeftCountLabel";
            // 
            // userShipsLeftLabel
            // 
            resources.ApplyResources(this.userShipsLeftLabel, "userShipsLeftLabel");
            this.userShipsLeftLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.userShipsLeftLabel.Name = "userShipsLeftLabel";
            // 
            // surrenderButton
            // 
            resources.ApplyResources(this.surrenderButton, "surrenderButton");
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.UseVisualStyleBackColor = true;
            this.surrenderButton.Click += new System.EventHandler(this.surrenderButton_Click);
            // 
            // gamePanel3
            // 
            this.gamePanel3.Controls.Add(this.uBattleFieldLabel);
            resources.ApplyResources(this.gamePanel3, "gamePanel3");
            this.gamePanel3.Name = "gamePanel3";
            // 
            // uBattleFieldLabel
            // 
            resources.ApplyResources(this.uBattleFieldLabel, "uBattleFieldLabel");
            this.uBattleFieldLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.uBattleFieldLabel.Name = "uBattleFieldLabel";
            // 
            // informationButton
            // 
            resources.ApplyResources(this.informationButton, "informationButton");
            this.informationButton.Name = "informationButton";
            this.informationButton.UseVisualStyleBackColor = true;
            this.informationButton.Click += new System.EventHandler(this.informationButton_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.mainGamePanel);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.customizeShipsPanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.customizeShipsPanel.ResumeLayout(false);
            this.customizeShipsPanel.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            this.gamePanel2.ResumeLayout(false);
            this.gamePanel2.PerformLayout();
            this.mainGamePanel.ResumeLayout(false);
            this.mainGamePanel.PerformLayout();
            this.gamePanel3.ResumeLayout(false);
            this.gamePanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button customizeShipsButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button quitGameButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel customizeShipsPanel;
        private System.Windows.Forms.Button csHomeButton;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.RadioButton rButtonFrench;
        private System.Windows.Forms.RadioButton rButtonSwedish;
        private System.Windows.Forms.RadioButton rButtonEnglish;
        private System.Windows.Forms.Button yLanguageButton;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox yCoordTextBox;
        private System.Windows.Forms.Button attackButton;
        private System.Windows.Forms.TextBox xCoordTextBox;
        private System.Windows.Forms.Panel gamePanel2;
        private System.Windows.Forms.Label eBattleFieldLabel;
        private System.Windows.Forms.Label roundCountLabel;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.Button sHomeButton;
        private System.Windows.Forms.Panel mainGamePanel;
        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.Panel gamePanel3;
        private System.Windows.Forms.Label uBattleFieldLabel;
        private System.Windows.Forms.Label userShipsLeftCountLabel;
        private System.Windows.Forms.Label userShipsLeftLabel;
        private System.Windows.Forms.Label enemyShipsLeftCountLabel;
        private System.Windows.Forms.Label enemyShipsLeftLabel;
        private System.Windows.Forms.Button placementButton;
        private System.Windows.Forms.RadioButton rButtonMandarin;
        private System.Windows.Forms.Button uChangeCharButton;
        private System.Windows.Forms.Button uSelectForeColorButton;
        private System.Windows.Forms.TextBox uCharBox;
        private System.Windows.Forms.Button eChangeCharButton;
        private System.Windows.Forms.Button eSelectForeColorButton;
        private System.Windows.Forms.TextBox eCharBox;
        private System.Windows.Forms.Label uForeColorLabel;
        private System.Windows.Forms.Label uCharLabel;
        private System.Windows.Forms.Label eForeColorLabel;
        private System.Windows.Forms.Label eCharLabel;
        private System.Windows.Forms.Label uShipLabel;
        private System.Windows.Forms.Label eShipLabel;
        private System.Windows.Forms.Label changeShipLabel;
        private System.Windows.Forms.ColorDialog eForeColorDialog;
        private System.Windows.Forms.ColorDialog uForeColorDialog;
        private System.Windows.Forms.Label uForeColorBox;
        private System.Windows.Forms.Label eForeColorBox;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button informationButton;
    }
}

