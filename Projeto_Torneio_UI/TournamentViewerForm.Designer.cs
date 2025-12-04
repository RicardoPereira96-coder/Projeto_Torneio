namespace Projeto_Torneio_UI
{
    partial class TournamentViewerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            headerLabel = new Label();
            tournamentName = new Label();
            roundLabel = new Label();
            roundDropDown = new ComboBox();
            unplayedOnlyCheckBox = new CheckBox();
            matchupListBox = new ListBox();
            teamOneName = new Label();
            teamOneScoreLabel = new Label();
            teamOneScoreValue = new TextBox();
            teamTwoScoreValue = new TextBox();
            teamTwoScoreLabel = new Label();
            teamTwoName = new Label();
            versusLabel = new Label();
            scoreButton = new Button();
            SuspendLayout();
            // 
            // headerLabel
            // 
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI Light", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            headerLabel.ForeColor = Color.RoyalBlue;
            headerLabel.Location = new Point(12, 9);
            headerLabel.Name = "headerLabel";
            headerLabel.Size = new Size(214, 50);
            headerLabel.TabIndex = 0;
            headerLabel.Text = "Tournament:";
            // 
            // tournamentName
            // 
            tournamentName.AutoSize = true;
            tournamentName.Font = new Font("Segoe UI Light", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tournamentName.ForeColor = Color.RoyalBlue;
            tournamentName.Location = new Point(213, 9);
            tournamentName.Name = "tournamentName";
            tournamentName.Size = new Size(150, 50);
            tournamentName.TabIndex = 1;
            tournamentName.Text = "<none>";
            // 
            // roundLabel
            // 
            roundLabel.AutoSize = true;
            roundLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundLabel.ForeColor = Color.RoyalBlue;
            roundLabel.Location = new Point(12, 59);
            roundLabel.Name = "roundLabel";
            roundLabel.Size = new Size(94, 37);
            roundLabel.TabIndex = 2;
            roundLabel.Text = "Round";
            // 
            // roundDropDown
            // 
            roundDropDown.FormattingEnabled = true;
            roundDropDown.Location = new Point(101, 58);
            roundDropDown.Name = "roundDropDown";
            roundDropDown.Size = new Size(197, 38);
            roundDropDown.TabIndex = 3;
            roundDropDown.SelectedIndexChanged += roundDropDown_SelectedIndexChanged;
            // 
            // unplayedOnlyCheckBox
            // 
            unplayedOnlyCheckBox.AutoSize = true;
            unplayedOnlyCheckBox.FlatStyle = FlatStyle.Flat;
            unplayedOnlyCheckBox.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unplayedOnlyCheckBox.ForeColor = Color.RoyalBlue;
            unplayedOnlyCheckBox.Location = new Point(101, 99);
            unplayedOnlyCheckBox.Name = "unplayedOnlyCheckBox";
            unplayedOnlyCheckBox.Size = new Size(209, 41);
            unplayedOnlyCheckBox.TabIndex = 4;
            unplayedOnlyCheckBox.Text = "Unplayed Only";
            unplayedOnlyCheckBox.UseVisualStyleBackColor = true;
            unplayedOnlyCheckBox.CheckedChanged += unplayedOnlyCheckBox_CheckedChanged;
            // 
            // matchupListBox
            // 
            matchupListBox.BorderStyle = BorderStyle.FixedSingle;
            matchupListBox.FormattingEnabled = true;
            matchupListBox.ItemHeight = 30;
            matchupListBox.Location = new Point(12, 146);
            matchupListBox.Name = "matchupListBox";
            matchupListBox.Size = new Size(286, 242);
            matchupListBox.TabIndex = 5;
            matchupListBox.SelectedIndexChanged += matchupListBox_SelectedIndexChanged;
            // 
            // teamOneName
            // 
            teamOneName.AutoSize = true;
            teamOneName.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamOneName.ForeColor = Color.RoyalBlue;
            teamOneName.Location = new Point(362, 146);
            teamOneName.Name = "teamOneName";
            teamOneName.Size = new Size(135, 37);
            teamOneName.TabIndex = 6;
            teamOneName.Text = "Team One";
            // 
            // teamOneScoreLabel
            // 
            teamOneScoreLabel.AutoSize = true;
            teamOneScoreLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamOneScoreLabel.ForeColor = Color.RoyalBlue;
            teamOneScoreLabel.Location = new Point(362, 183);
            teamOneScoreLabel.Name = "teamOneScoreLabel";
            teamOneScoreLabel.Size = new Size(82, 37);
            teamOneScoreLabel.TabIndex = 7;
            teamOneScoreLabel.Text = "Score";
            // 
            // teamOneScoreValue
            // 
            teamOneScoreValue.Location = new Point(441, 186);
            teamOneScoreValue.Name = "teamOneScoreValue";
            teamOneScoreValue.Size = new Size(141, 35);
            teamOneScoreValue.TabIndex = 8;
            teamOneScoreValue.TextChanged += teamOneScoreValue_TextChanged;
            // 
            // teamTwoScoreValue
            // 
            teamTwoScoreValue.Location = new Point(441, 338);
            teamTwoScoreValue.Name = "teamTwoScoreValue";
            teamTwoScoreValue.Size = new Size(141, 35);
            teamTwoScoreValue.TabIndex = 11;
            // 
            // teamTwoScoreLabel
            // 
            teamTwoScoreLabel.AutoSize = true;
            teamTwoScoreLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamTwoScoreLabel.ForeColor = Color.RoyalBlue;
            teamTwoScoreLabel.Location = new Point(362, 335);
            teamTwoScoreLabel.Name = "teamTwoScoreLabel";
            teamTwoScoreLabel.Size = new Size(82, 37);
            teamTwoScoreLabel.TabIndex = 10;
            teamTwoScoreLabel.Text = "Score";
            // 
            // teamTwoName
            // 
            teamTwoName.AutoSize = true;
            teamTwoName.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamTwoName.ForeColor = Color.RoyalBlue;
            teamTwoName.Location = new Point(362, 298);
            teamTwoName.Name = "teamTwoName";
            teamTwoName.Size = new Size(135, 37);
            teamTwoName.TabIndex = 9;
            teamTwoName.Text = "Team Two";
            // 
            // versusLabel
            // 
            versusLabel.AutoSize = true;
            versusLabel.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            versusLabel.ForeColor = Color.RoyalBlue;
            versusLabel.Location = new Point(449, 246);
            versusLabel.Name = "versusLabel";
            versusLabel.Size = new Size(48, 37);
            versusLabel.TabIndex = 12;
            versusLabel.Text = "VS";
            // 
            // scoreButton
            // 
            scoreButton.FlatAppearance.BorderColor = Color.Silver;
            scoreButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(102, 102, 102);
            scoreButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(242, 242, 242);
            scoreButton.FlatStyle = FlatStyle.Flat;
            scoreButton.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            scoreButton.ForeColor = Color.RoyalBlue;
            scoreButton.Location = new Point(594, 246);
            scoreButton.Name = "scoreButton";
            scoreButton.Size = new Size(106, 61);
            scoreButton.TabIndex = 13;
            scoreButton.Text = "Score";
            scoreButton.UseVisualStyleBackColor = true;
            scoreButton.Click += scoreButton_Click;
            // 
            // TournamentViewerForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(753, 417);
            Controls.Add(scoreButton);
            Controls.Add(versusLabel);
            Controls.Add(teamTwoScoreValue);
            Controls.Add(teamTwoScoreLabel);
            Controls.Add(teamTwoName);
            Controls.Add(teamOneScoreValue);
            Controls.Add(teamOneScoreLabel);
            Controls.Add(teamOneName);
            Controls.Add(matchupListBox);
            Controls.Add(unplayedOnlyCheckBox);
            Controls.Add(roundDropDown);
            Controls.Add(roundLabel);
            Controls.Add(tournamentName);
            Controls.Add(headerLabel);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 6, 5, 6);
            Name = "TournamentViewerForm";
            Text = "Tournament Viewer";
            Load += TournamentViewerForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label headerLabel;
        private Label tournamentName;
        private Label roundLabel;
        private ComboBox roundDropDown;
        private CheckBox unplayedOnlyCheckBox;
        private ListBox matchupListBox;
        private Label teamOneName;
        private Label teamOneScoreLabel;
        private TextBox teamOneScoreValue;
        private TextBox teamTwoScoreValue;
        private Label teamTwoScoreLabel;
        private Label teamTwoName;
        private Label versusLabel;
        private Button scoreButton;
    }
}
