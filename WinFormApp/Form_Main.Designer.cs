namespace WinFormApp
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.Panel_Client = new System.Windows.Forms.Panel();
            this.Panel_Power = new System.Windows.Forms.Panel();
            this.Label_AppName = new System.Windows.Forms.Label();
            this.Label_Note = new System.Windows.Forms.Label();
            this.Label_ReturnToZero = new System.Windows.Forms.Label();
            this.Panel_Input = new System.Windows.Forms.Panel();
            this.Label_Base = new System.Windows.Forms.Label();
            this.ContextMenuStrip_Base = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Base_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Base_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.Label_Pow = new System.Windows.Forms.Label();
            this.ContextMenuStrip_Pow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Pow_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Pow_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Output = new System.Windows.Forms.Panel();
            this.ContextMenuStrip_Output = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Output_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Label_Equal = new System.Windows.Forms.Label();
            this.Label_Val = new System.Windows.Forms.Label();
            this.Label_Exp = new System.Windows.Forms.Label();
            this.Label_Time = new System.Windows.Forms.Label();
            this.Panel_Main.SuspendLayout();
            this.Panel_Client.SuspendLayout();
            this.Panel_Power.SuspendLayout();
            this.Panel_Input.SuspendLayout();
            this.ContextMenuStrip_Base.SuspendLayout();
            this.ContextMenuStrip_Pow.SuspendLayout();
            this.Panel_Output.SuspendLayout();
            this.ContextMenuStrip_Output.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Main.Controls.Add(this.Panel_Client);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(450, 220);
            this.Panel_Main.TabIndex = 0;
            // 
            // Panel_Client
            // 
            this.Panel_Client.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Client.Controls.Add(this.Panel_Power);
            this.Panel_Client.Location = new System.Drawing.Point(0, 0);
            this.Panel_Client.Name = "Panel_Client";
            this.Panel_Client.Size = new System.Drawing.Size(450, 220);
            this.Panel_Client.TabIndex = 0;
            // 
            // Panel_Power
            // 
            this.Panel_Power.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Power.Controls.Add(this.Label_AppName);
            this.Panel_Power.Controls.Add(this.Label_Note);
            this.Panel_Power.Controls.Add(this.Label_ReturnToZero);
            this.Panel_Power.Controls.Add(this.Panel_Input);
            this.Panel_Power.Controls.Add(this.Panel_Output);
            this.Panel_Power.Controls.Add(this.Label_Time);
            this.Panel_Power.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Power.Location = new System.Drawing.Point(0, 0);
            this.Panel_Power.Name = "Panel_Power";
            this.Panel_Power.Size = new System.Drawing.Size(450, 220);
            this.Panel_Power.TabIndex = 0;
            this.Panel_Power.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Power_Paint);
            // 
            // Label_AppName
            // 
            this.Label_AppName.AutoSize = true;
            this.Label_AppName.BackColor = System.Drawing.Color.Transparent;
            this.Label_AppName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_AppName.ForeColor = System.Drawing.Color.White;
            this.Label_AppName.Location = new System.Drawing.Point(20, 25);
            this.Label_AppName.Name = "Label_AppName";
            this.Label_AppName.Size = new System.Drawing.Size(117, 28);
            this.Label_AppName.TabIndex = 0;
            this.Label_AppName.Text = "乘方计算器";
            this.Label_AppName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Note
            // 
            this.Label_Note.AutoSize = true;
            this.Label_Note.BackColor = System.Drawing.Color.Transparent;
            this.Label_Note.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label_Note.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Label_Note.ForeColor = System.Drawing.Color.White;
            this.Label_Note.Location = new System.Drawing.Point(22, 80);
            this.Label_Note.Name = "Label_Note";
            this.Label_Note.Size = new System.Drawing.Size(161, 17);
            this.Label_Note.TabIndex = 0;
            this.Label_Note.Text = "输入介于 ±1E15 之间的实数";
            this.Label_Note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_ReturnToZero
            // 
            this.Label_ReturnToZero.BackColor = System.Drawing.Color.Transparent;
            this.Label_ReturnToZero.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ReturnToZero.ForeColor = System.Drawing.Color.White;
            this.Label_ReturnToZero.Location = new System.Drawing.Point(395, 80);
            this.Label_ReturnToZero.Name = "Label_ReturnToZero";
            this.Label_ReturnToZero.Size = new System.Drawing.Size(30, 30);
            this.Label_ReturnToZero.TabIndex = 0;
            this.Label_ReturnToZero.Text = "C";
            this.Label_ReturnToZero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_Input
            // 
            this.Panel_Input.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Input.Controls.Add(this.Label_Base);
            this.Panel_Input.Controls.Add(this.Label_Pow);
            this.Panel_Input.Location = new System.Drawing.Point(25, 120);
            this.Panel_Input.Name = "Panel_Input";
            this.Panel_Input.Size = new System.Drawing.Size(200, 38);
            this.Panel_Input.TabIndex = 0;
            this.Panel_Input.LocationChanged += new System.EventHandler(this.Panel_Input_LocationChanged);
            this.Panel_Input.SizeChanged += new System.EventHandler(this.Panel_Input_SizeChanged);
            this.Panel_Input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseDown);
            this.Panel_Input.MouseLeave += new System.EventHandler(this.Panel_Input_MouseLeave);
            this.Panel_Input.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseMove);
            this.Panel_Input.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseUp);
            // 
            // Label_Base
            // 
            this.Label_Base.AutoSize = true;
            this.Label_Base.BackColor = System.Drawing.Color.Transparent;
            this.Label_Base.ContextMenuStrip = this.ContextMenuStrip_Base;
            this.Label_Base.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Base.ForeColor = System.Drawing.Color.White;
            this.Label_Base.Location = new System.Drawing.Point(0, 11);
            this.Label_Base.Name = "Label_Base";
            this.Label_Base.Size = new System.Drawing.Size(56, 27);
            this.Label_Base.TabIndex = 0;
            this.Label_Base.Text = "Base";
            this.Label_Base.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Label_Base.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Label_Base_KeyDown);
            this.Label_Base.LocationChanged += new System.EventHandler(this.Label_Base_LocationChanged);
            this.Label_Base.SizeChanged += new System.EventHandler(this.Label_Base_SizeChanged);
            this.Label_Base.TextChanged += new System.EventHandler(this.Label_Base_TextChanged);
            this.Label_Base.GotFocus += new System.EventHandler(this.Label_Base_GotFocus);
            this.Label_Base.LostFocus += new System.EventHandler(this.Label_Base_LostFocus);
            this.Label_Base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Base_MouseDown);
            this.Label_Base.MouseLeave += new System.EventHandler(this.Label_Base_MouseLeave);
            this.Label_Base.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Base_MouseMove);
            // 
            // ContextMenuStrip_Base
            // 
            this.ContextMenuStrip_Base.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip_Base.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Base_Copy,
            this.ToolStripMenuItem_Base_Paste});
            this.ContextMenuStrip_Base.Name = "ContextMenuStrip_ID";
            this.ContextMenuStrip_Base.Size = new System.Drawing.Size(117, 48);
            // 
            // ToolStripMenuItem_Base_Copy
            // 
            this.ToolStripMenuItem_Base_Copy.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Base_Copy.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Base_Copy.Name = "ToolStripMenuItem_Base_Copy";
            this.ToolStripMenuItem_Base_Copy.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Base_Copy.Text = "复制(C)";
            this.ToolStripMenuItem_Base_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Base_Copy_Click);
            // 
            // ToolStripMenuItem_Base_Paste
            // 
            this.ToolStripMenuItem_Base_Paste.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Base_Paste.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Base_Paste.Name = "ToolStripMenuItem_Base_Paste";
            this.ToolStripMenuItem_Base_Paste.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Base_Paste.Text = "粘贴(P)";
            this.ToolStripMenuItem_Base_Paste.Click += new System.EventHandler(this.ToolStripMenuItem_Base_Paste_Click);
            // 
            // Label_Pow
            // 
            this.Label_Pow.AutoSize = true;
            this.Label_Pow.BackColor = System.Drawing.Color.Transparent;
            this.Label_Pow.ContextMenuStrip = this.ContextMenuStrip_Pow;
            this.Label_Pow.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Pow.ForeColor = System.Drawing.Color.White;
            this.Label_Pow.Location = new System.Drawing.Point(56, 0);
            this.Label_Pow.Name = "Label_Pow";
            this.Label_Pow.Size = new System.Drawing.Size(43, 21);
            this.Label_Pow.TabIndex = 0;
            this.Label_Pow.Text = "Pow";
            this.Label_Pow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Label_Pow_KeyDown);
            this.Label_Pow.LocationChanged += new System.EventHandler(this.Label_Pow_LocationChanged);
            this.Label_Pow.SizeChanged += new System.EventHandler(this.Label_Pow_SizeChanged);
            this.Label_Pow.TextChanged += new System.EventHandler(this.Label_Pow_TextChanged);
            this.Label_Pow.GotFocus += new System.EventHandler(this.Label_Pow_GotFocus);
            this.Label_Pow.LostFocus += new System.EventHandler(this.Label_Pow_LostFocus);
            this.Label_Pow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Pow_MouseDown);
            this.Label_Pow.MouseLeave += new System.EventHandler(this.Label_Pow_MouseLeave);
            this.Label_Pow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Pow_MouseMove);
            // 
            // ContextMenuStrip_Pow
            // 
            this.ContextMenuStrip_Pow.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip_Pow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Pow_Copy,
            this.ToolStripMenuItem_Pow_Paste});
            this.ContextMenuStrip_Pow.Name = "ContextMenuStrip_ID";
            this.ContextMenuStrip_Pow.Size = new System.Drawing.Size(117, 48);
            // 
            // ToolStripMenuItem_Pow_Copy
            // 
            this.ToolStripMenuItem_Pow_Copy.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Pow_Copy.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Pow_Copy.Name = "ToolStripMenuItem_Pow_Copy";
            this.ToolStripMenuItem_Pow_Copy.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Pow_Copy.Text = "复制(C)";
            this.ToolStripMenuItem_Pow_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Pow_Copy_Click);
            // 
            // ToolStripMenuItem_Pow_Paste
            // 
            this.ToolStripMenuItem_Pow_Paste.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Pow_Paste.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Pow_Paste.Name = "ToolStripMenuItem_Pow_Paste";
            this.ToolStripMenuItem_Pow_Paste.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Pow_Paste.Text = "粘贴(P)";
            this.ToolStripMenuItem_Pow_Paste.Click += new System.EventHandler(this.ToolStripMenuItem_Pow_Paste_Click);
            // 
            // Panel_Output
            // 
            this.Panel_Output.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Output.ContextMenuStrip = this.ContextMenuStrip_Output;
            this.Panel_Output.Controls.Add(this.Label_Equal);
            this.Panel_Output.Controls.Add(this.Label_Val);
            this.Panel_Output.Controls.Add(this.Label_Exp);
            this.Panel_Output.Location = new System.Drawing.Point(225, 120);
            this.Panel_Output.Name = "Panel_Output";
            this.Panel_Output.Size = new System.Drawing.Size(200, 38);
            this.Panel_Output.TabIndex = 0;
            this.Panel_Output.LocationChanged += new System.EventHandler(this.Panel_Output_LocationChanged);
            this.Panel_Output.SizeChanged += new System.EventHandler(this.Panel_Output_SizeChanged);
            // 
            // ContextMenuStrip_Output
            // 
            this.ContextMenuStrip_Output.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip_Output.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Output_Copy});
            this.ContextMenuStrip_Output.Name = "ContextMenuStrip_ID";
            this.ContextMenuStrip_Output.Size = new System.Drawing.Size(117, 26);
            // 
            // ToolStripMenuItem_Output_Copy
            // 
            this.ToolStripMenuItem_Output_Copy.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Output_Copy.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Output_Copy.Name = "ToolStripMenuItem_Output_Copy";
            this.ToolStripMenuItem_Output_Copy.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Output_Copy.Text = "复制(C)";
            this.ToolStripMenuItem_Output_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Output_Copy_Click);
            // 
            // Label_Equal
            // 
            this.Label_Equal.AutoSize = true;
            this.Label_Equal.BackColor = System.Drawing.Color.Transparent;
            this.Label_Equal.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Equal.ForeColor = System.Drawing.Color.White;
            this.Label_Equal.Location = new System.Drawing.Point(0, 11);
            this.Label_Equal.Name = "Label_Equal";
            this.Label_Equal.Size = new System.Drawing.Size(27, 27);
            this.Label_Equal.TabIndex = 0;
            this.Label_Equal.Text = "=";
            // 
            // Label_Val
            // 
            this.Label_Val.AutoSize = true;
            this.Label_Val.BackColor = System.Drawing.Color.Transparent;
            this.Label_Val.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Val.ForeColor = System.Drawing.Color.White;
            this.Label_Val.Location = new System.Drawing.Point(27, 11);
            this.Label_Val.Name = "Label_Val";
            this.Label_Val.Size = new System.Drawing.Size(42, 27);
            this.Label_Val.TabIndex = 0;
            this.Label_Val.Text = "Val";
            this.Label_Val.LocationChanged += new System.EventHandler(this.Label_Val_LocationChanged);
            this.Label_Val.SizeChanged += new System.EventHandler(this.Label_Val_SizeChanged);
            // 
            // Label_Exp
            // 
            this.Label_Exp.AutoSize = true;
            this.Label_Exp.BackColor = System.Drawing.Color.Transparent;
            this.Label_Exp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Exp.ForeColor = System.Drawing.Color.White;
            this.Label_Exp.Location = new System.Drawing.Point(69, 0);
            this.Label_Exp.Name = "Label_Exp";
            this.Label_Exp.Size = new System.Drawing.Size(37, 21);
            this.Label_Exp.TabIndex = 0;
            this.Label_Exp.Text = "Exp";
            this.Label_Exp.LocationChanged += new System.EventHandler(this.Label_Exp_LocationChanged);
            this.Label_Exp.SizeChanged += new System.EventHandler(this.Label_Exp_SizeChanged);
            // 
            // Label_Time
            // 
            this.Label_Time.BackColor = System.Drawing.Color.Transparent;
            this.Label_Time.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label_Time.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Time.ForeColor = System.Drawing.Color.White;
            this.Label_Time.Location = new System.Drawing.Point(0, 200);
            this.Label_Time.Name = "Label_Time";
            this.Label_Time.Size = new System.Drawing.Size(450, 20);
            this.Label_Time.TabIndex = 0;
            this.Label_Time.Text = "用时";
            this.Label_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(450, 220);
            this.Controls.Add(this.Panel_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Panel_Main.ResumeLayout(false);
            this.Panel_Client.ResumeLayout(false);
            this.Panel_Power.ResumeLayout(false);
            this.Panel_Power.PerformLayout();
            this.Panel_Input.ResumeLayout(false);
            this.Panel_Input.PerformLayout();
            this.ContextMenuStrip_Base.ResumeLayout(false);
            this.ContextMenuStrip_Pow.ResumeLayout(false);
            this.Panel_Output.ResumeLayout(false);
            this.Panel_Output.PerformLayout();
            this.ContextMenuStrip_Output.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Client;
        private System.Windows.Forms.Panel Panel_Output;
        private System.Windows.Forms.Label Label_Equal;
        private System.Windows.Forms.Label Label_Exp;
        private System.Windows.Forms.Label Label_Val;
        private System.Windows.Forms.Panel Panel_Input;
        private System.Windows.Forms.Label Label_Note;
        private System.Windows.Forms.Label Label_AppName;
        private System.Windows.Forms.Label Label_Pow;
        private System.Windows.Forms.Label Label_Base;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Base;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Base_Copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Base_Paste;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Output;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Output_Copy;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Pow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pow_Copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Pow_Paste;
        private System.Windows.Forms.Panel Panel_Power;
        private System.Windows.Forms.Label Label_ReturnToZero;
        private System.Windows.Forms.Label Label_Time;
    }
}