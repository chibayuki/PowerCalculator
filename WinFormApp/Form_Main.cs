/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2018 chibayuki@foxmail.com

乘方计算器 (PowerCalculator)
Version 18.10.2.1600

This file is part of "乘方计算器" (PowerCalculator)

"乘方计算器" (PowerCalculator) is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WinFormApp
{
    public partial class Form_Main : Form
    {
        #region 版本信息

        private static readonly string ApplicationName = Application.ProductName; // 程序名。
        private static readonly string ApplicationEdition = "18"; // 程序版本。

        private static readonly Int32 MajorVersion = new Version(Application.ProductVersion).Major; // 主版本。
        private static readonly Int32 MinorVersion = new Version(Application.ProductVersion).Minor; // 副版本。
        private static readonly Int32 BuildNumber = new Version(Application.ProductVersion).Build; // 版本号。
        private static readonly Int32 BuildRevision = new Version(Application.ProductVersion).Revision; // 修订版本。
        private static readonly string LabString = "REL"; // 分支名。
        private static readonly string BuildTime = "181002-1600"; // 编译时间。

        #endregion

        #region 程序功能变量

        private struct SciN // 科学计数法。
        {
            public double Val; // 有效数字。
            public Int64 Exp; // 数量级。
            public bool IsReal; // 是（true）否为实数。
        }

        private double Base_Input = 0; // 输入的底数。
        private double Pow_Input = 0; // 输入的指数。

        private Color FocusedColor; // 指定日期标签获得焦点时的背景色。
        private Color PointedColor; // 指定日期标签被指向但未获得焦点时的背景色。
        private Color UnfocusedColor; // 指定日期标签失去焦点时的背景色。

        #endregion

        #region 窗体构造

        private Com.WinForm.FormManager Me;

        public Com.WinForm.FormManager FormManager
        {
            get
            {
                return Me;
            }
        }

        private void _Ctor(Com.WinForm.FormManager owner)
        {
            InitializeComponent();

            //

            if (owner != null)
            {
                Me = new Com.WinForm.FormManager(this, owner);
            }
            else
            {
                Me = new Com.WinForm.FormManager(this);
            }

            //

            FormDefine();
        }

        public Form_Main()
        {
            _Ctor(null);
        }

        public Form_Main(Com.WinForm.FormManager owner)
        {
            _Ctor(owner);
        }

        private void FormDefine()
        {
            Me.Caption = Application.ProductName;
            Me.FormStyle = Com.WinForm.FormStyle.Fixed;
            Me.EnableMaximize = false;
            Me.EnableFullScreen = false;
            Me.ClientSize = new Size(450, 220);
            Me.Theme = Com.WinForm.Theme.Colorful;
            Me.ThemeColor = Com.ColorManipulation.GetRandomColorX();

            Me.Loaded += LoadedEvents;
            Me.Closed += ClosedEvents;
            Me.SizeChanged += SizeChangedEvents;
            Me.ThemeChanged += ThemeColorChangedEvents;
            Me.ThemeColorChanged += ThemeColorChangedEvents;
        }

        #endregion

        #region 窗体事件

        private void LoadedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载后发生。
            //

            Me.OnThemeChanged();

            //

            ReturnToZero();
        }

        private void ClosedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体关闭后发生。
            //

            Calc_Stop();
        }

        private void SizeChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小更改时发生。
            //

            Panel_Client.Size = Panel_Main.Size;

            //

            Panel_Power.Refresh();
        }

        private void ThemeColorChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的主题色更改时发生。
            //

            FocusedColor = Me.RecommendColors.Background_INC.ToColor();
            PointedColor = Me.RecommendColors.Background.ToColor();
            UnfocusedColor = Color.Transparent;

            //

            Label_AppName.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_Note.ForeColor = Me.RecommendColors.Text_DEC.ToColor();

            //

            Label_ReturnToZero.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ReturnToZero.BackColor = Me.RecommendColors.Background_DEC.ToColor();

            Panel_Input.BackColor = Panel_Output.BackColor = Me.RecommendColors.Background_DEC.ToColor();

            Label_Base.ForeColor = Label_Pow.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_Equal.ForeColor = Label_Val.ForeColor = Label_Exp.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_Time.ForeColor = (Me.RecommendColors.Main.Lightness_LAB < 70 ? Color.White : Color.Black);
            Label_Time.BackColor = Me.RecommendColors.Main.ToColor();

            ContextMenuStrip_Base.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ToolStripMenuItem_Base_Copy.ForeColor = ToolStripMenuItem_Base_Paste.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            ContextMenuStrip_Pow.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ToolStripMenuItem_Pow_Copy.ForeColor = ToolStripMenuItem_Pow_Paste.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            ContextMenuStrip_Output.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ToolStripMenuItem_Output_Copy.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            //

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ReturnToZero, Label_ReturnToZero_Click, Me.RecommendColors.Background_DEC.ToColor(), PointedColor, FocusedColor);
        }

        #endregion

        #region 背景绘图

        private void Panel_Power_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Power 绘图。
            //

            Pen BorderLine = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
            Pen BorderLine_Shadow = new Pen(Me.RecommendColors.Border_DEC.AtAlpha(96).ToColor(), 1);

            e.Graphics.DrawRectangle(BorderLine_Shadow, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Label_ReturnToZero }, 3));
            e.Graphics.DrawRectangle(BorderLine, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Label_ReturnToZero }, 2));

            e.Graphics.DrawRectangle(BorderLine_Shadow, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Panel_Input, Panel_Output }, 3));
            e.Graphics.DrawRectangle(BorderLine, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Panel_Input, Panel_Output }, 2));
        }

        #endregion

        #region 乘方函数

        private SciN PowerStandard(double Base, Int64 Pow)
        {
            //
            // 标准乘方函数。底数为区间 (1, 10) 内的实数，指数为大于 1 的整数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();
            SN.IsReal = true;

            Int64 Pow_Exp = (Int64)Math.Floor(Math.Log10(Pow));
            double Pow_Val = Pow / Math.Pow(10, Pow_Exp);

            if (Pow_Val == 0)
            {
                Pow_Exp = 0;
            }
            else
            {
                if (Pow_Val < 1)
                {
                    Pow_Val *= 10;
                    Pow_Exp--;
                }
            }

            double PwrVal_Val = Math.Pow(Base, Pow_Val);
            Int64 PwrVal_Exp = 0;

            for (long i = 1; i <= Pow_Exp; i++)
            {
                if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                {
                    return new SciN();
                }

                //

                PwrVal_Val = Math.Pow(PwrVal_Val, 10);
                Int64 PwrVal_Val_Exp = (Int64)Math.Floor(Math.Log10(PwrVal_Val));
                PwrVal_Val /= Math.Pow(10, PwrVal_Val_Exp);
                PwrVal_Exp = PwrVal_Exp * 10 + PwrVal_Val_Exp;
            }

            SN.Val = PwrVal_Val;
            SN.Exp = PwrVal_Exp;

            while (SN.Val >= 10)
            {
                SN.Val /= 10;
                SN.Exp++;
            }

            if (SN.Val == 0)
            {
                SN.Exp = 0;
            }
            else
            {
                while (SN.Val < 1)
                {
                    SN.Val *= 10;
                    SN.Exp--;
                }
            }

            return SN;
        }

        private SciN PowerGeneral(double Base, double Pow)
        {
            //
            // 一般乘方函数。底数为正实数，指数为大于 1 的有理数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();
            SN.IsReal = true;

            if (Base == Math.Pow(10, (Int64)Math.Floor(Math.Log10(Base)))) // 底数为 10 的正整数次幂。
            {
                SN.Val = Math.Pow(Base, Pow - Math.Truncate(Pow));
                SN.Exp = (Int64)(Math.Floor(Math.Log10(Base)) * Math.Truncate(Pow));
            }
            else // 底数为其他正实数。
            {
                Int64 Base_Exp = (Int64)Math.Floor(Math.Log10(Base));
                double Base_Val = Base / Math.Pow(10, Base_Exp);

                if (Base_Val == 0)
                {
                    Base_Exp = 0;
                }
                else
                {
                    if (Base_Val < 1)
                    {
                        Base_Val *= 10;
                        Base_Exp--;
                    }
                }

                SciN SN_X = PowerStandard(Base_Val, (Int64)Pow);

                SN.Val = SN_X.Val * Math.Pow(Base, Pow - Math.Truncate(Pow));
                SN.Exp = SN_X.Exp + Base_Exp * (Int64)Pow;
            }

            while (SN.Val >= 10)
            {
                SN.Val /= 10;
                SN.Exp++;
            }

            if (SN.Val == 0)
            {
                SN.Exp = 0;
            }
            else
            {
                while (SN.Val < 1)
                {
                    SN.Val *= 10;
                    SN.Exp--;
                }
            }

            return SN;
        }

        private SciN PowerSpecial(double Base, double Pow)
        {
            //
            // 特殊乘方函数。底数为负实数，指数为不等于 -1，0，1 的有理数。
            //

            /*
            ================================
            【定义 1】（整有理数的奇偶性）
            定义整数 Z，正实数 R，整数 n，
            则 Z 的奇偶性， P+ = (+R) ^ Z 与 P- = (-R) ^ Z 所属的集合，
            它们之间满足如下对应关系：
            （1）Z = 2n：Z 为偶数，P+ 为正实数，P- 为正实数；
            （2）Z = 2n + 1：Z 为奇数，P+ 为正实数，P- 为负实数。
            ================================
            【定义 2】（非整有理数的奇偶性）
            定义非整有理数 Q，并将其表示为既约分数 p / q 的形式，
            定义正实数 R，整数 n，
            则 p 与 q 的奇偶性， Q 的奇偶性， 
            P+ = (+R) ^ Q 与 P- = (-R) ^ Q 所属的集合，
            它们之间满足如下对应关系：
            （1）p = 2n，q = 2n：Q 为奇数，P+ 为正实数，P- 为正实数；
            （2）p = 2n + 1，q = 2n：Q 为偶数，P+ 为正实数，P- 为正虚数；
            （3）p = 2n，q = 2n + 1：Q 为奇数，P+ 为正实数，P- 为正实数；
            （4）p = 2n + 1，q = 2n + 1：Q 为奇数，P+ 为正实数，P- 为负实数。
            ================================
            */

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();
            SN.IsReal = true;

            if (Pow == Math.Truncate(Pow)) // 指数为不等于 -1，0，1 的整有理数。
            {
                Int64 Pow_L = (Int64)Pow;

                if (Pow_L < -1) // 指数为小于 -1 的整有理数。
                {
                    SN = PowerGeneral(-Base, -Pow_L);

                    if (Pow_L % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    SN.Val = 1 / SN.Val;
                    SN.Exp = -SN.Exp;
                } // 指数为小于 -1 的整有理数。
                else if (Pow_L > 1) // 指数为大于 1 的整有理数。
                {
                    SN = PowerGeneral(-Base, Pow_L);

                    if (Pow_L % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }
                } // 指数为大于 1 的整有理数。
            } // 指数为不等于 -1，0，1 的整有理数。
            else // 指数为非整有理数。
            {
                double Rational_P = Pow, Rational_Q = 1; // 指数的既约分数形式的分子与分母。

                if (Rational_P.ToString().IndexOf(".") != -1)
                {
                    double Mag = Math.Pow(10, Rational_P.ToString().Length - Rational_P.ToString().IndexOf(".") - 1);

                    Rational_P *= Mag;
                    Rational_Q *= Mag;
                }

                while (Rational_P % 2 == 0 && Rational_Q % 2 == 0)
                {
                    Rational_P /= 2;
                    Rational_Q /= 2;
                }

                while (Rational_P % 5 == 0 && Rational_Q % 5 == 0)
                {
                    Rational_P /= 5;
                    Rational_Q /= 5;
                }

                //

                if (Pow < -1) // 指数为小于 -1 的非整有理数。
                {
                    SN = PowerGeneral(-Base, -Pow);

                    if (Rational_P % 2 != 0 && Rational_Q % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    if (Rational_P % 2 != 0 && Rational_Q % 2 == 0)
                    {
                        SN.IsReal = false;
                    }

                    if (SN.IsReal == false && Pow < 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    SN.Val = 1 / SN.Val;
                    SN.Exp = -SN.Exp;
                } // 指数为小于 -1 的非整有理数。
                else if (Pow > -1 && Pow < 0) // 指数为介于 -1 到 0 的非整有理数。
                {
                    SN.Val = Math.Pow(-Base, -Pow);
                    SN.Exp = 0;

                    if (Rational_P % 2 != 0 && Rational_Q % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    if (Rational_P % 2 != 0 && Rational_Q % 2 == 0)
                    {
                        SN.IsReal = false;
                    }

                    if (SN.IsReal == false && Pow < 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    SN.Val = 1 / SN.Val;
                    SN.Exp = -SN.Exp;
                } // 指数为介于 -1 到 0 的非整有理数。
                else if (Pow > 0 && Pow < 1) // 指数为介于 0 到 1 的非整有理数。
                {
                    SN.Val = Math.Pow(-Base, Pow);
                    SN.Exp = 0;

                    if (Rational_P % 2 != 0 && Rational_Q % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    if (Rational_P % 2 != 0 && Rational_Q % 2 == 0)
                    {
                        SN.IsReal = false;
                    }

                    if (SN.IsReal == false && Pow < 0)
                    {
                        SN.Val = -SN.Val;
                    }
                } // 指数为介于 0 到 1 的非整有理数。
                else if (Pow > 1) // 指数为大于 1 的非整有理数。
                {
                    SN = PowerGeneral(-Base, Pow);

                    if (Rational_P % 2 != 0 && Rational_Q % 2 != 0)
                    {
                        SN.Val = -SN.Val;
                    }

                    if (Rational_P % 2 != 0 && Rational_Q % 2 == 0)
                    {
                        SN.IsReal = false;
                    }

                    if (SN.IsReal == false && Pow < 0)
                    {
                        SN.Val = -SN.Val;
                    }
                } // 指数为大于 1 的非整有理数。
            } // 指数为非整有理数。

            // 科学记数法标准化处理：

            while (SN.Val <= -10 || SN.Val >= 10)
            {
                SN.Val /= 10;
                SN.Exp++;
            }

            if (SN.Val == 0)
            {
                SN.Exp = 0;
            }
            else
            {
                while (SN.Val > -1 && SN.Val < 1)
                {
                    SN.Val *= 10;
                    SN.Exp--;
                }
            }

            //

            return SN;
        }

        private struct PwrRslt // 乘方函数的计算结果。
        {
            public string ValStr; // 底数的字符串。
            public string ExpStr; // 指数的字符串。
        }

        private PwrRslt Power(double Base, double Pow)
        {
            //
            // 乘方函数。底数为实数，指数为有理数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new PwrRslt();
            }

            //

            PwrRslt PR = new PwrRslt();

            SciN SN = new SciN();
            SN.IsReal = true;

            if (Pow == 0) // 指数等于 0。
            {
                if (Base == 0) // 底数等于 0。
                {
                    PR.ValStr = "1";

                    goto RETURN;
                }
                else // 底数不等于 0。
                {
                    PR.ValStr = "1";

                    goto RETURN;
                }
            } // 指数等于 0。
            else if (Pow == -1) // 指数等于 -1。
            {
                if (Base == 0) // 底数等于 0。
                {
                    PR.ValStr = "正无穷大";

                    goto RETURN;
                }
                else // 底数不等于 0。
                {
                    SN.Val = 1 / Base;
                    SN.Exp = 0;

                    goto SCINSTD;
                }
            } // 指数等于 -1。
            else if (Pow == 1) // 指数等于 1。
            {
                if (Base == 0) // 底数等于 0。
                {
                    PR.ValStr = "0";

                    goto RETURN;
                }
                else // 底数不等于 0。
                {
                    SN.Val = Base;
                    SN.Exp = 0;

                    goto SCINSTD;
                }
            } // 指数等于 1。
            else if (Pow < -1) // 指数小于 -1。
            {
                if (Base == 0) // 底数等于 0。
                {
                    PR.ValStr = "正无穷大";

                    goto RETURN;
                }
                else if (Base < 0) // 底数小于 0。
                {
                    SN = PowerSpecial(Base, Pow);

                    goto SCINSTD;
                }
                else if (Base > 0) // 底数大于 0。
                {
                    SN = PowerGeneral(Base, -Pow);

                    SN.Val = 1 / SN.Val;
                    SN.Exp = -SN.Exp;

                    goto SCINSTD;
                }
            } // 指数小于 -1。
            else if (Pow > -1 && Pow < 1) // 指数介于 -1 到 1。
            {
                if (Base == 0) // 底数等于 0。
                {
                    if (Pow > 0) // 指数介于 0 到 1。
                    {
                        PR.ValStr = "0";

                        goto RETURN;
                    }
                    else if (Pow < 0) // 指数介于 -1 到 0。
                    {
                        PR.ValStr = "正无穷大";

                        goto RETURN;
                    }
                } // 底数等于 0。
                else if (Base == 1) // 底数等于 1。
                {
                    PR.ValStr = "1";

                    goto RETURN;
                }
                else if (Base < 0) // 底数小于 0。
                {
                    SN = PowerSpecial(Base, Pow);

                    goto SCINSTD;
                }
                else if (Base > 0) // 底数大于 0。
                {
                    SN.Val = Math.Pow(Base, Pow);
                    SN.Exp = 0;

                    goto SCINSTD;
                }
            } // 指数介于 -1 到 1。
            else if (Pow > 1) // 指数大于 1。
            {
                if (Base == 0) // 底数等于 0。
                {
                    PR.ValStr = "0";

                    goto RETURN;
                }
                else if (Base == 1) // 底数等于 1。
                {
                    PR.ValStr = "1";

                    goto RETURN;
                }
                else if (Base < 0) // 底数小于 0。
                {
                    SN = PowerSpecial(Base, Pow);

                    goto SCINSTD;
                }
                else if (Base > 0) // 底数大于 0。
                {
                    SN = PowerGeneral(Base, Pow);

                    goto SCINSTD;
                }
            } // 指数大于 1。

            SCINSTD:
            // 科学记数法标准化处理：

            while (SN.Val <= -10 || SN.Val >= 10)
            {
                SN.Val /= 10;
                SN.Exp++;
            }

            if (SN.Val == 0)
            {
                SN.Exp = 0;
            }
            else
            {
                while (SN.Val > -1 && SN.Val < 1)
                {
                    SN.Val *= 10;
                    SN.Exp--;
                }
            }

            //

            if (SN.Exp >= -4 && SN.Exp <= 14)
            {
                PR.ValStr = (SN.Val * Math.Pow(10, SN.Exp)) + (SN.IsReal == false ? " i" : string.Empty);
            }
            else
            {
                PR.ValStr = SN.Val + (SN.IsReal == false ? " i" : string.Empty) + " × 10";
                PR.ExpStr = SN.Exp.ToString();
            }

            //

            RETURN:

            return PR;
        }

        #endregion

        #region 后台计算

        private string Result_Val, Result_Exp, Result_Time, Result_Time_BefStr; // 计算结果字符串。

        private void RefreshResult()
        {
            //
            // 刷新计算结果。
            //

            Label_Val.Text = Result_Val;
            Label_Exp.Text = Result_Exp;
            Label_Time.Text = Result_Time;
        }

        // 后台计算异步线程。

        private BackgroundWorker BackgroundWorker_Calc = new BackgroundWorker(); // 后台计算异步线程。

        private DateTime LastWorkAsync = DateTime.Now; // 上次开始异步工作的时刻。
        private TimeSpan LastWorkAsyncToNow => DateTime.Now - LastWorkAsync; // 上次开始异步工作到现在的时间间隔。

        private void BackgroundWorker_Calc_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 后台计算执行异步工作。
            //

            LastWorkAsync = DateTime.Now;

            //

            Calc_WorkAsync();
        }

        private DateTime LastReportProgress = DateTime.Now; // 上次报告异步工作进度的时刻。
        private TimeSpan LastReportProgressToNow => DateTime.Now - LastReportProgress; // 上次报告异步工作进度到现在的时间间隔。

        private void BackgroundWorker_Calc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 后台计算异步工作进度改变。
            //

            LastReportProgress = DateTime.Now;

            //

            Calc_ReportProgress();
        }

        private void BackgroundWorker_Calc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 后台计算异步工作完成。
            //

            if (!e.Cancelled)
            {
                Calc_WorkDone();
            }
        }

        // 计算步骤周期与时间。

        private const Int32 CycSteps = 1; // 最大计算步骤数。

        private double[] CycCount = new double[CycSteps]; // 所有计算步骤分别需要的循环或递归的周期数。

        private Int32 _CycDone = 0; // 已完成的计算步骤数。
        private Int32 CycDone
        {
            get
            {
                return _CycDone;
            }

            set
            {
                _CycDone = Math.Max(0, Math.Min(value, CycSteps));
            }
        }

        private double CycCount_Total // 所有计算步骤需要的循环或递归的总周期数。
        {
            get
            {
                double _CycCount_Total = 0;

                for (int i = 0; i <= CycSteps - 1; i++)
                {
                    double V = (double.IsNaN(CycCount[i]) || double.IsInfinity(CycCount[i]) || CycCount[i] <= 0 ? 0 : CycCount[i]);

                    _CycCount_Total += V;
                }

                return Math.Max(1, _CycCount_Total);
            }
        }
        private double CycCount_Done // 已完成的计算步骤数包含的循环或递归的周期数。
        {
            get
            {
                double _CycCount_Done = 0;

                for (int i = 0; i <= CycSteps - 1; i++)
                {
                    if (CycDone >= i + 1)
                    {
                        double V = (double.IsNaN(CycCount[i]) || double.IsInfinity(CycCount[i]) || CycCount[i] <= 0 ? 0 : CycCount[i]);

                        _CycCount_Done += V;
                    }
                    else
                    {
                        break;
                    }
                }

                return Math.Max(1, _CycCount_Done);
            }
        }

        private void CycReset()
        {
            //
            // 将 CycCount 的所有元素和 CycDone 重置为 0。
            //

            for (int i = 0; i <= CycSteps - 1; i++)
            {
                CycCount[i] = 0;
            }

            CycDone = 0;
        }

        private void ReportRemainingTime(double Cyc)
        {
            //
            // 向后台计算异步线程报告剩余时间。Cyc：当前计算步骤已完成的循环或递归的周期数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.WorkerReportsProgress && BackgroundWorker_Calc.IsBusy)
            {
                double _Cyc = CycCount_Done + Cyc;

                TimeSpan TS = TimeSpan.FromMilliseconds(LastWorkAsyncToNow.TotalMilliseconds / _Cyc * (CycCount_Total - _Cyc));

                Result_Time = Result_Time_BefStr + (TS.TotalMilliseconds >= 1000 ? "还需大约 " + GetTimeStringFromTimeSpan(TS) : "即将完成");

                BackgroundWorker_Calc.ReportProgress(0);
            }
        }

        // 后台计算异步工作控制。

        private void Calc_Start()
        {
            //
            // 后台计算开始。
            //

            CycReset();

            Result_Val = Result_Exp = Result_Time = Result_Time_BefStr = string.Empty;

            //

            BackgroundWorker_Calc = new BackgroundWorker();

            BackgroundWorker_Calc.WorkerReportsProgress = true;
            BackgroundWorker_Calc.WorkerSupportsCancellation = true;
            BackgroundWorker_Calc.DoWork += BackgroundWorker_Calc_DoWork;
            BackgroundWorker_Calc.ProgressChanged += BackgroundWorker_Calc_ProgressChanged;
            BackgroundWorker_Calc.RunWorkerCompleted += BackgroundWorker_Calc_RunWorkerCompleted;

            if (!BackgroundWorker_Calc.IsBusy)
            {
                BackgroundWorker_Calc.RunWorkerAsync();
            }
        }

        private void Calc_Stop()
        {
            //
            // 后台计算停止。
            //

            if (BackgroundWorker_Calc != null)
            {
                BackgroundWorker_Calc.DoWork -= BackgroundWorker_Calc_DoWork;
                BackgroundWorker_Calc.ProgressChanged -= BackgroundWorker_Calc_ProgressChanged;
                BackgroundWorker_Calc.RunWorkerCompleted -= BackgroundWorker_Calc_RunWorkerCompleted;

                if (BackgroundWorker_Calc.IsBusy)
                {
                    BackgroundWorker_Calc.CancelAsync();
                }

                BackgroundWorker_Calc.Dispose();
            }
        }

        private void Calc_Restart()
        {
            //
            // 后台计算停止并重新开始。
            //

            Calc_Stop();
            Calc_Start();
        }

        private void Calc_WorkAsync()
        {
            //
            // 后台计算异步工作内容。
            //

            if (Validity_Base && Validity_Pow)
            {
                CycCount[0] = Math.Floor(Math.Log10(Math.Max(1, Math.Abs(Pow_Input))));

                Result_Val = "正在计算…";
                Result_Time = "正在计算…";
                Result_Time_BefStr = "正在计算，";

                BackgroundWorker_Calc.ReportProgress(0);

                //

                Stopwatch Sw = new Stopwatch();
                Sw.Restart();

                PwrRslt PR = Power(Base_Input, Pow_Input);

                Sw.Stop();

                Result_Val = PR.ValStr;
                Result_Exp = PR.ExpStr;
                Result_Time = "用时 " + GetTimeStringFromTimeSpan(Sw.Elapsed);
                Result_Time_BefStr = string.Empty;
            }
            else
            {
                Result_Val = "无效输入";
                Result_Exp = string.Empty;
                Result_Time = Result_Time_BefStr = string.Empty;
            }
        }

        private void Calc_ReportProgress()
        {
            //
            // 后台计算报告异步工作进度。
            //

            RefreshResult();
        }

        private void Calc_WorkDone()
        {
            //
            // 后台计算异步工作完成。
            //

            RefreshResult();
        }

        #endregion

        #region 输入输出

        // 归零。

        private void ReturnToZero()
        {
            //
            // 归零。
            //

            Calc_Stop();

            //

            Base_Input = 0;
            Label_Base.TextChanged -= Label_Base_TextChanged;
            Label_Base.Text = "0";
            Label_Base.TextChanged += Label_Base_TextChanged;
            Validity_Base = true;

            Pow_Input = 0;
            Label_Pow.TextChanged -= Label_Pow_TextChanged;
            Label_Pow.Text = "0";
            Label_Pow.TextChanged += Label_Pow_TextChanged;
            Validity_Pow = true;

            //

            Label_Val.Text = "1";
            Label_Exp.Text = string.Empty;
            Label_Time.Text = string.Empty;

            //

            Label_Base.Focus();
        }

        private void Label_ReturnToZero_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ReturnToZero。
            //

            ReturnToZero();
        }

        // 输入合法性。

        private bool Validity_Base = false; // 输入的底数的合法性。
        private bool Validity_Pow = false; // 输入的指数的合法性。

        // 输入区域容器。

        private void Panel_Input_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_Input。
            //

            Point PTC = Panel_Input.PointToClient(Cursor.Position);

            if ((PTC.Y < Label_Base.Top && PTC.X >= (Label_Base.Left + Label_Base.Right) / 2) || (PTC.Y >= Label_Pow.Bottom && PTC.X >= (Label_Pow.Left + Label_Pow.Right) / 2) || (PTC.Y < Label_Pow.Bottom && PTC.Y >= Label_Base.Top && PTC.X >= Label_Pow.Left))
            {
                if (!Label_Pow.Focused)
                {
                    Label_Pow.BackColor = PointedColor;
                }

                if (!Label_Base.Focused)
                {
                    Label_Base.BackColor = UnfocusedColor;
                }
            }
            else
            {
                if (!Label_Base.Focused)
                {
                    Label_Base.BackColor = PointedColor;
                }

                if (!Label_Pow.Focused)
                {
                    Label_Pow.BackColor = UnfocusedColor;
                }
            }
        }

        private void Panel_Input_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Panel_Input。
            //

            if (!Label_Base.Focused)
            {
                Label_Base.BackColor = UnfocusedColor;
            }

            if (!Label_Pow.Focused)
            {
                Label_Pow.BackColor = UnfocusedColor;
            }
        }

        private void Panel_Input_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_Input。
            //

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Point PTC = Panel_Input.PointToClient(Cursor.Position);

                if ((PTC.Y < Label_Base.Top && PTC.X >= (Label_Base.Left + Label_Base.Right) / 2) || (PTC.Y >= Label_Pow.Bottom && PTC.X >= (Label_Pow.Left + Label_Pow.Right) / 2) || (PTC.Y < Label_Pow.Bottom && PTC.Y >= Label_Base.Top && PTC.X >= Label_Pow.Left))
                {
                    Label_Pow.Focus();
                }
                else
                {
                    Label_Base.Focus();
                }
            }
        }

        private void Panel_Input_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_Input。
            //

            if (e.Button == MouseButtons.Right)
            {
                Point PTC = Panel_Input.PointToClient(Cursor.Position);

                if ((PTC.Y < Label_Base.Top && PTC.X >= (Label_Base.Left + Label_Base.Right) / 2) || (PTC.Y >= Label_Pow.Bottom && PTC.X >= (Label_Pow.Left + Label_Pow.Right) / 2) || (PTC.Y < Label_Pow.Bottom && PTC.Y >= Label_Base.Top && PTC.X >= Label_Pow.Left))
                {
                    ContextMenuStrip_Pow.Show(Cursor.Position);
                }
                else
                {
                    ContextMenuStrip_Base.Show(Cursor.Position);
                }
            }
        }

        private void Panel_Input_LocationChanged(object sender, EventArgs e)
        {
            //
            // Panel_Input 位置改变。
            //

            Panel_Output.Left = Panel_Input.Right;
        }

        private void Panel_Input_SizeChanged(object sender, EventArgs e)
        {
            //
            // Panel_Input 大小改变。
            //

            Panel_Output.Left = Panel_Input.Right;
        }

        // 输入值：底数。

        private void Label_Base_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Label_Base。
            //

            if (!Label_Base.Focused)
            {
                Label_Base.BackColor = PointedColor;
            }
        }

        private void Label_Base_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Label_Base。
            //

            if (!Label_Base.Focused)
            {
                Label_Base.BackColor = UnfocusedColor;
            }
        }

        private void Label_Base_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Base。
            //

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Label_Base.Focus();
            }
        }

        private void Label_Base_GotFocus(object sender, EventArgs e)
        {
            //
            // Label_Base 接收焦点。
            //

            Label_Base.BackColor = FocusedColor;
        }

        private void Label_Base_LostFocus(object sender, EventArgs e)
        {
            //
            // Label_Base 失去焦点。
            //

            Label_Base.BackColor = UnfocusedColor;
        }

        private void Label_Base_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // 在 Label_Base 按下键。
            //

            if (Label_Base.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.D0:
                    case Keys.NumPad0:
                        Label_Base.Text += "0";
                        break;

                    case Keys.D1:
                    case Keys.NumPad1:
                        Label_Base.Text += "1";
                        break;

                    case Keys.D2:
                    case Keys.NumPad2:
                        Label_Base.Text += "2";
                        break;

                    case Keys.D3:
                    case Keys.NumPad3:
                        Label_Base.Text += "3";
                        break;

                    case Keys.D4:
                    case Keys.NumPad4:
                        Label_Base.Text += "4";
                        break;

                    case Keys.D5:
                    case Keys.NumPad5:
                        Label_Base.Text += "5";
                        break;

                    case Keys.D6:
                    case Keys.NumPad6:
                        Label_Base.Text += "6";
                        break;

                    case Keys.D7:
                    case Keys.NumPad7:
                        Label_Base.Text += "7";
                        break;

                    case Keys.D8:
                    case Keys.NumPad8:
                        Label_Base.Text += "8";
                        break;

                    case Keys.D9:
                    case Keys.NumPad9:
                        Label_Base.Text += "9";
                        break;

                    case Keys.OemMinus:
                    case Keys.Subtract:
                        Label_Base.Text += "-";
                        break;

                    case Keys.OemPeriod:
                    case Keys.Decimal:
                        Label_Base.Text += ".";
                        break;

                    case Keys.Back:
                        Label_Base.Text = Label_Base.Text.Substring(0, Math.Max(0, Label_Base.Text.Length - 1));
                        break;

                    case Keys.Delete:
                        Label_Base.Text = "0";
                        break;

                    case Keys.Escape:
                        ReturnToZero();
                        break;

                    case Keys.Right:
                    case Keys.Up:
                    case Keys.PageDown:
                    case Keys.Space:
                        Label_Pow.Focus();
                        break;
                }
            }
        }

        private void Label_Base_TextChanged(object sender, EventArgs e)
        {
            //
            // Label_Base 文本改变。
            //

            string Str = Label_Base.Text;

            REJUDGE:

            Str = new Regex(@"[^\d\.\-]").Replace(Str, string.Empty);

            Int32 MCount = Regex.Matches(Str, @"-").Count; // "-" 出现在文本的次数。
            Int32 DIndex = Str.IndexOf("."); // "." 第一次出现在文本的位置。

            if (MCount % 2 == 0) // "-" 出现偶数次。
            {
                if (DIndex == -1) // "." 未出现。
                {
                    string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                    if (Text.Length > 0)
                    {
                        Str = Convert.ToDouble(Text).ToString();
                    }
                    else
                    {
                        Str = "0";
                    }

                    Validity_Base = true;
                } // "." 未出现。
                else // "." 出现。
                {
                    string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                    string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                    string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                    string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                    if (Part1.Length > 0)
                    {
                        if (Part2.Length > 0)
                        {
                            Str = Convert.ToDouble(Part1) + "." + Part2;

                            Validity_Base = true;
                        }
                        else if (Part1.Length < 15)
                        {
                            Str = Convert.ToDouble(Part1) + ".";

                            Validity_Base = false;
                        }
                        else
                        {
                            Str = Convert.ToDouble(Part1).ToString();

                            goto REJUDGE;
                        }
                    }
                    else
                    {
                        Str = "0";

                        goto REJUDGE;
                    }
                } // "." 出现。
            } // "-" 出现偶数次。
            else // "-" 出现奇数次。
            {
                if (DIndex == -1) // "." 未出现。
                {
                    string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                    if (Text.Length > 0)
                    {
                        Str = "-" + Convert.ToDouble(Text);

                        Validity_Base = true;
                    }
                    else
                    {
                        Str = "-";

                        Validity_Base = false;
                    }
                } // "." 未出现。
                else // "." 出现。
                {
                    string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                    string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                    string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                    string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                    if (Part1.Length > 0)
                    {
                        if (Part2.Length > 0)
                        {
                            Str = "-" + Convert.ToDouble(Part1) + "." + Part2;

                            Validity_Base = true;
                        }
                        else if (Part1.Length < 15)
                        {
                            Str = "-" + Convert.ToDouble(Part1) + ".";

                            Validity_Base = false;
                        }
                        else
                        {
                            Str = "-" + Convert.ToDouble(Part1);

                            goto REJUDGE;
                        }
                    }
                    else
                    {
                        Str = "-";

                        goto REJUDGE;
                    }
                } // "." 出现。
            } // "-" 出现奇数次。

            Label_Base.TextChanged -= Label_Base_TextChanged;
            Label_Base.Text = Str;
            Label_Base.TextChanged += Label_Base_TextChanged;

            //

            if (Validity_Base)
            {
                Base_Input = Convert.ToDouble(Label_Base.Text);
            }

            //

            Calc_Restart();
        }

        private void Label_Base_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Base 位置改变。
            //

            Label_Pow.Left = Label_Base.Right + 1;
        }

        private void Label_Base_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Base 大小改变。
            //

            Label_Pow.Left = Label_Base.Right + 1;
        }

        // 输入值：指数。

        private void Label_Pow_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Label_Pow。
            //

            if (!Label_Pow.Focused)
            {
                Label_Pow.BackColor = PointedColor;
            }
        }

        private void Label_Pow_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Label_Pow。
            //

            if (!Label_Pow.Focused)
            {
                Label_Pow.BackColor = UnfocusedColor;
            }
        }

        private void Label_Pow_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Pow。
            //

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Label_Pow.Focus();
            }
        }

        private void Label_Pow_GotFocus(object sender, EventArgs e)
        {
            //
            // Label_Pow 接收焦点。
            //

            Label_Pow.BackColor = FocusedColor;
        }

        private void Label_Pow_LostFocus(object sender, EventArgs e)
        {
            //
            // Label_Pow 失去焦点。
            //

            Label_Pow.BackColor = UnfocusedColor;
        }

        private void Label_Pow_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // 在 Label_Pow 按下键。
            //

            if (Label_Pow.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.D0:
                    case Keys.NumPad0:
                        Label_Pow.Text += "0";
                        break;

                    case Keys.D1:
                    case Keys.NumPad1:
                        Label_Pow.Text += "1";
                        break;

                    case Keys.D2:
                    case Keys.NumPad2:
                        Label_Pow.Text += "2";
                        break;

                    case Keys.D3:
                    case Keys.NumPad3:
                        Label_Pow.Text += "3";
                        break;

                    case Keys.D4:
                    case Keys.NumPad4:
                        Label_Pow.Text += "4";
                        break;

                    case Keys.D5:
                    case Keys.NumPad5:
                        Label_Pow.Text += "5";
                        break;

                    case Keys.D6:
                    case Keys.NumPad6:
                        Label_Pow.Text += "6";
                        break;

                    case Keys.D7:
                    case Keys.NumPad7:
                        Label_Pow.Text += "7";
                        break;

                    case Keys.D8:
                    case Keys.NumPad8:
                        Label_Pow.Text += "8";
                        break;

                    case Keys.D9:
                    case Keys.NumPad9:
                        Label_Pow.Text += "9";
                        break;

                    case Keys.OemMinus:
                    case Keys.Subtract:
                        Label_Pow.Text += "-";
                        break;

                    case Keys.OemPeriod:
                    case Keys.Decimal:
                        Label_Pow.Text += ".";
                        break;

                    case Keys.Back:
                        Label_Pow.Text = Label_Pow.Text.Substring(0, Math.Max(0, Label_Pow.Text.Length - 1));
                        break;

                    case Keys.Delete:
                        Label_Pow.Text = "0";
                        break;

                    case Keys.Escape:
                        ReturnToZero();
                        break;

                    case Keys.Left:
                    case Keys.Down:
                    case Keys.PageUp:
                    case Keys.Space:
                        Label_Base.Focus();
                        break;
                }
            }
        }

        private void Label_Pow_TextChanged(object sender, EventArgs e)
        {
            //
            // Label_Pow 文本改变。
            //

            string Str = Label_Pow.Text;

            REJUDGE:

            Str = new Regex(@"[^\d\.\-]").Replace(Str, string.Empty);

            Int32 MCount = Regex.Matches(Str, @"-").Count; // "-" 出现在文本的次数。
            Int32 DIndex = Str.IndexOf("."); // "." 第一次出现在文本的位置。

            if (MCount % 2 == 0) // "-" 出现偶数次。
            {
                if (DIndex == -1) // "." 未出现。
                {
                    string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                    if (Text.Length > 0)
                    {
                        Str = Convert.ToDouble(Text).ToString();
                    }
                    else
                    {
                        Str = "0";
                    }

                    Validity_Pow = true;
                } // "." 未出现。
                else // "." 出现。
                {
                    string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                    string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                    string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                    string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                    if (Part1.Length > 0)
                    {
                        if (Part2.Length > 0)
                        {
                            Str = Convert.ToDouble(Part1) + "." + Part2;

                            Validity_Pow = true;
                        }
                        else if (Part1.Length < 15)
                        {
                            Str = Convert.ToDouble(Part1) + ".";

                            Validity_Pow = false;
                        }
                        else
                        {
                            Str = Convert.ToDouble(Part1).ToString();

                            goto REJUDGE;
                        }
                    }
                    else
                    {
                        Str = "0";

                        goto REJUDGE;
                    }
                } // "." 出现。
            } // "-" 出现偶数次。
            else // "-" 出现奇数次。
            {
                if (DIndex == -1) // "." 未出现。
                {
                    string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                    if (Text.Length > 0)
                    {
                        Str = "-" + Convert.ToDouble(Text);

                        Validity_Pow = true;
                    }
                    else
                    {
                        Str = "-";

                        Validity_Pow = false;
                    }
                } // "." 未出现。
                else // "." 出现。
                {
                    string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                    string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                    string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                    string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                    if (Part1.Length > 0)
                    {
                        if (Part2.Length > 0)
                        {
                            Str = "-" + Convert.ToDouble(Part1) + "." + Part2;

                            Validity_Pow = true;
                        }
                        else if (Part1.Length < 15)
                        {
                            Str = "-" + Convert.ToDouble(Part1) + ".";

                            Validity_Pow = false;
                        }
                        else
                        {
                            Str = "-" + Convert.ToDouble(Part1);

                            goto REJUDGE;
                        }
                    }
                    else
                    {
                        Str = "-";

                        goto REJUDGE;
                    }
                } // "." 出现。
            } // "-" 出现奇数次。

            Label_Pow.TextChanged -= Label_Pow_TextChanged;
            Label_Pow.Text = Str;
            Label_Pow.TextChanged += Label_Pow_TextChanged;

            //

            if (Validity_Pow)
            {
                Pow_Input = Convert.ToDouble(Label_Pow.Text);
            }

            //

            Calc_Restart();
        }

        private void Label_Pow_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Pow 位置改变。
            //

            Panel_Input.Width = Math.Max(200, Label_Pow.Right);
        }

        private void Label_Pow_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Pow 大小改变。
            //

            Panel_Input.Width = Math.Max(200, Label_Pow.Right);
        }

        // 输出区域容器。

        private void ResizeForm()
        {
            //
            // 重置窗体大小。
            //

            Me.Width = Panel_Output.Right + Panel_Input.Left;
            Me.X = Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.X + Screen.PrimaryScreen.WorkingArea.Width - Me.Width, Me.X));
        }

        private void Panel_Output_LocationChanged(object sender, EventArgs e)
        {
            //
            // Panel_Output 位置改变。
            //

            Label_ReturnToZero.Left = Panel_Output.Right - Label_ReturnToZero.Width;

            //

            ResizeForm();
        }

        private void Panel_Output_SizeChanged(object sender, EventArgs e)
        {
            //
            // Panel_Output 大小改变。
            //

            Label_ReturnToZero.Left = Panel_Output.Right - Label_ReturnToZero.Width;

            //

            ResizeForm();
        }

        // 输出值标签。

        private void Label_Val_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Val 位置改变。
            //

            Label_Exp.Left = Label_Val.Right;
        }

        private void Label_Val_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Val 大小改变。
            //

            Label_Exp.Left = Label_Val.Right;
        }

        private void Label_Exp_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Exp 位置改变。
            //

            Panel_Output.Width = Math.Max(200, Label_Exp.Right);
        }

        private void Label_Exp_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Exp 大小改变。
            //

            Panel_Output.Width = Math.Max(200, Label_Exp.Right);
        }

        #endregion

        #region 菜单项

        // 输入。

        private void ToolStripMenuItem_Base_Copy_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Base_Copy。
            //

            if (Label_Base.Text != string.Empty)
            {
                Clipboard.SetDataObject(Label_Base.Text);
            }
        }

        private void ToolStripMenuItem_Base_Paste_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Base_Paste。
            //

            IDataObject Data = Clipboard.GetDataObject();

            if (Data.GetDataPresent(DataFormats.Text))
            {
                Label_Base.Text = (string)Data.GetData(DataFormats.Text);
            }
        }

        private void ToolStripMenuItem_Pow_Copy_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Pow。
            //

            if (Label_Pow.Text != string.Empty)
            {
                Clipboard.SetDataObject(Label_Pow.Text);
            }
        }

        private void ToolStripMenuItem_Pow_Paste_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Pow_Paste。
            //

            IDataObject Data = Clipboard.GetDataObject();

            if (Data.GetDataPresent(DataFormats.Text))
            {
                Label_Pow.Text = (string)Data.GetData(DataFormats.Text);
            }
        }

        // 输出。

        private void ToolStripMenuItem_Output_Copy_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Output_Copy。
            //

            if (Label_Val.Text != string.Empty)
            {
                if (Label_Exp.Text == string.Empty)
                {
                    Clipboard.SetDataObject(Label_Val.Text);
                }
                else
                {
                    Clipboard.SetDataObject(Label_Val.Text + " ^ " + Label_Exp.Text);
                }
            }
        }

        #endregion

        #region 公用函数与方法

        private string GetTimeStringFromTimeSpan(TimeSpan TS)
        {
            //
            // 获取时间间隔的字符串。TS：时间间隔。
            //

            try
            {
                return (TS.TotalHours >= 1 ? Math.Floor(TS.TotalHours) + " 小时 " + TS.Minutes + " 分 " + TS.Seconds + " 秒" : (TS.TotalMinutes >= 1 ? TS.Minutes + " 分 " + TS.Seconds + " 秒" : (TS.TotalSeconds >= 1 ? TS.Seconds + "." + TS.Milliseconds.ToString("D3").Substring(0, TS.Seconds >= 10 ? 1 : 2) + " 秒" : (TS.TotalMilliseconds >= 1 ? Math.Truncate(TS.TotalMilliseconds) + (TS.TotalMilliseconds < 100 ? "." + ((Int32)((TS.TotalMilliseconds - Math.Truncate(TS.TotalMilliseconds)) * 1000)).ToString("D3").Substring(0, TS.TotalMilliseconds >= 10 ? 1 : 2) : string.Empty) + " 毫秒" : (TS.TotalMilliseconds * 1000 >= 0.1 ? Math.Truncate(TS.TotalMilliseconds * 1000) + (TS.TotalMilliseconds * 1000 < 100 ? "." + ((Int32)((TS.TotalMilliseconds * 1000 - Math.Truncate(TS.TotalMilliseconds * 1000)) * 1000)).ToString("D3").Substring(0, 1) : string.Empty) + " 微秒" : "小于 0.1 微秒")))));
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

    }
}