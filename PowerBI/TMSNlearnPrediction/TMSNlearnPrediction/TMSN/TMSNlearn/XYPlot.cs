using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004AC RID: 1196
	public class XYPlot
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x0008C5A8 File Offset: 0x0008A7A8
		public void InitializeChart(bool addLegend = true, bool legendOnBottom = false, bool isLogaritmic = false, bool legendOnLeft = false)
		{
			this.chart = new Chart();
			this.chartArea = new ChartArea();
			if (!this.PlainStyle)
			{
				this.AddChartStyle();
			}
			this.chartArea.Name = "Default";
			this.chartArea.ShadowColor = Color.Transparent;
			this.chartArea.AxisY.Minimum = this.MinY;
			this.chartArea.AxisY.Maximum = this.MaxY;
			this.chartArea.AxisY.Title = this.LegendY;
			this.chartArea.AxisY.IsLogarithmic = isLogaritmic;
			this.chartArea.AxisX.Minimum = this.MinX;
			this.chartArea.AxisX.Maximum = this.MaxX;
			this.chartArea.AxisX.Title = this.LegendX;
			this.chartArea.AxisX.IsLogarithmic = false;
			if (this.fontSize != 0)
			{
				Font font = new Font(this.chartArea.AxisX.TitleFont.FontFamily, (float)(this.fontSize + 3), FontStyle.Bold);
				this.chartArea.AxisX.TitleFont = font;
				this.chartArea.AxisY.TitleFont = font;
				this.chartArea.AxisX.LabelAutoFitMaxFontSize = this.fontSize + 3;
				this.chartArea.AxisY.LabelAutoFitMaxFontSize = this.fontSize + 3;
			}
			this.chart.ChartAreas.Add(this.chartArea);
			this.chart.Size = new Size(this.ResolutionX, this.ResolutionY);
			if (addLegend)
			{
				Legend legend = new Legend();
				legend.DockedToChartArea = "Default";
				legend.Name = "Default";
				if (legendOnBottom)
				{
					legend.Docking = Docking.Bottom;
					legend.Alignment = StringAlignment.Far;
					legend.LegendStyle = LegendStyle.Column;
				}
				if (legendOnLeft)
				{
					legend.Docking = Docking.Left;
					legend.Alignment = StringAlignment.Near;
					legend.LegendStyle = LegendStyle.Column;
				}
				if (this.fontSize != 0)
				{
					Font font2 = new Font(legend.Font.FontFamily, (float)(this.fontSize - 2));
					legend.Font = font2;
				}
				this.chart.Legends.Add(legend);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0008C7E0 File Offset: 0x0008A9E0
		protected virtual void AddChartStyle()
		{
			this.chart.BackColor = Color.FromArgb(243, 223, 193);
			this.chart.BackGradientStyle = GradientStyle.TopBottom;
			this.chart.BorderlineColor = Color.FromArgb(181, 64, 1);
			this.chart.BorderlineDashStyle = ChartDashStyle.Solid;
			this.chart.BorderlineWidth = 3;
			this.chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
			this.chartArea.Area3DStyle.Inclination = 40;
			this.chartArea.Area3DStyle.IsClustered = true;
			this.chartArea.Area3DStyle.IsRightAngleAxes = false;
			this.chartArea.Area3DStyle.LightStyle = LightStyle.Realistic;
			this.chartArea.Area3DStyle.Perspective = 9;
			this.chartArea.Area3DStyle.Rotation = 25;
			this.chartArea.Area3DStyle.WallWidth = 3;
			this.chartArea.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
			this.chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
			this.chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
			this.chartArea.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
			this.chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
			this.chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
			this.chartArea.BackColor = Color.OldLace;
			this.chartArea.BackGradientStyle = GradientStyle.TopBottom;
			this.chartArea.BackSecondaryColor = Color.White;
			this.chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
			this.chartArea.BorderDashStyle = ChartDashStyle.Solid;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0008C9EC File Offset: 0x0008ABEC
		protected virtual Color GetNextColor()
		{
			return this.plotColors[this.currentPlot++ % this.plotColors.Length];
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008CA24 File Offset: 0x0008AC24
		protected virtual Series CreateSeries(string seriesName, Color color, ChartDashStyle style = ChartDashStyle.Solid)
		{
			return new Series
			{
				BorderColor = Color.FromArgb(180, 26, 59, 105),
				BorderWidth = 2,
				ChartArea = "Default",
				ChartType = SeriesChartType.FastLine,
				Color = color,
				BorderDashStyle = style,
				Legend = "Default",
				Name = seriesName,
				ShadowColor = Color.Black,
				ShadowOffset = 2,
				XValueType = ChartValueType.Double,
				YValueType = ChartValueType.Double,
				IsVisibleInLegend = true
			};
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
		protected virtual Series CreateMarkerSeries(string seriesName)
		{
			return new Series
			{
				BorderWidth = 1,
				ChartArea = "Default",
				ChartType = SeriesChartType.FastPoint,
				Color = Color.FromArgb(128, 128, 128, 255),
				Name = seriesName,
				XValueType = ChartValueType.Double,
				YValueType = ChartValueType.Double,
				IsVisibleInLegend = true,
				MarkerStyle = MarkerStyle.Cross,
				MarkerSize = 7,
				MarkerBorderWidth = 1,
				MarkerBorderColor = Color.FromArgb(128, 0, 0, 255)
			};
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008CB44 File Offset: 0x0008AD44
		public void AddCurveXY(IEnumerable<XYPlot.XYPoint> points, string seriesName, ChartDashStyle style = ChartDashStyle.Solid)
		{
			if (points.Count<XYPlot.XYPoint>() == 0)
			{
				return;
			}
			Series series = this.CreateSeries(seriesName, this.GetNextColor(), style);
			foreach (XYPlot.XYPoint xypoint in points)
			{
				if (FloatUtils.IsFinite(xypoint.x) && FloatUtils.IsFinite(xypoint.y))
				{
					series.Points.AddXY(xypoint.x, xypoint.y);
				}
			}
			this.chart.Series.Add(series);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008CBE4 File Offset: 0x0008ADE4
		public void AddCurveY(IEnumerable<double> points, string seriesName)
		{
			if (points.Count<double>() == 0)
			{
				return;
			}
			Series series = this.CreateSeries(seriesName, this.GetNextColor(), ChartDashStyle.Solid);
			int num = 0;
			foreach (double num2 in points)
			{
				double num3 = num2;
				if (FloatUtils.IsFinite(num3))
				{
					series.Points.AddXY((double)num++, num3);
				}
			}
			this.chart.Series.Add(series);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0008CC6C File Offset: 0x0008AE6C
		public void AddMarkerXY(IEnumerable<XYPlot.XYPoint> points, string seriesName)
		{
			if (points.Count<XYPlot.XYPoint>() == 0)
			{
				return;
			}
			Series series = this.CreateMarkerSeries(seriesName);
			foreach (XYPlot.XYPoint xypoint in points)
			{
				if (FloatUtils.IsFinite(xypoint.x) && FloatUtils.IsFinite(xypoint.y))
				{
					series.Points.AddXY(xypoint.x, xypoint.y);
				}
			}
			this.chart.Series.Add(series);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008CD08 File Offset: 0x0008AF08
		public void AddCurveFromFile(string filename, string xColumnName, string yColumnName, string seriesName)
		{
			Series series = this.CreateSeries(seriesName, this.GetNextColor(), ChartDashStyle.Solid);
			bool flag = false;
			using (TsvReader tsvReader = new TsvReader(ZStreamReader.Open(filename, Encoding.UTF8)))
			{
				int num = 0;
				while (!tsvReader.Eof())
				{
					string text = tsvReader[yColumnName];
					string text2 = tsvReader[xColumnName];
					if (!string.IsNullOrEmpty(text))
					{
						double num2 = double.Parse(text, CultureInfo.InvariantCulture);
						double num3 = (double)(++num);
						if (!string.IsNullOrEmpty(text2))
						{
							num3 = double.Parse(text2, CultureInfo.InvariantCulture);
						}
						flag = true;
						series.Points.AddXY(num3, num2);
						tsvReader.NextRow();
					}
				}
			}
			if (flag)
			{
				this.chart.Series.Add(series);
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		public void Save(string imageFilename)
		{
			using (Stream stream = ZStreamOut.Open(imageFilename))
			{
				if (imageFilename.ToLower().EndsWith(".emf"))
				{
					this.chart.SaveImage(stream, ChartImageFormat.Emf);
				}
				else
				{
					this.chart.SaveImage(stream, ChartImageFormat.Jpeg);
				}
			}
		}

		// Token: 0x04000EDB RID: 3803
		public int ResolutionX = 640;

		// Token: 0x04000EDC RID: 3804
		public int ResolutionY = 480;

		// Token: 0x04000EDD RID: 3805
		public double MinX = double.NaN;

		// Token: 0x04000EDE RID: 3806
		public double MaxX = double.NaN;

		// Token: 0x04000EDF RID: 3807
		public double MinY = double.NaN;

		// Token: 0x04000EE0 RID: 3808
		public double MaxY = double.NaN;

		// Token: 0x04000EE1 RID: 3809
		public string LegendX = "LegendX";

		// Token: 0x04000EE2 RID: 3810
		public string LegendY = "LegendY";

		// Token: 0x04000EE3 RID: 3811
		public bool PlainStyle;

		// Token: 0x04000EE4 RID: 3812
		public int fontSize = 8;

		// Token: 0x04000EE5 RID: 3813
		protected Color[] plotColors = new Color[]
		{
			Color.FromArgb(240, 0, 0, 0),
			Color.FromArgb(240, 255, 0, 0),
			Color.FromArgb(240, 0, 255, 0),
			Color.FromArgb(240, 0, 0, 255),
			Color.FromArgb(240, 255, 255, 0),
			Color.FromArgb(240, 255, 0, 255),
			Color.FromArgb(240, 0, 255, 255),
			Color.FromArgb(240, 128, 128, 128),
			Color.FromArgb(240, 128, 0, 0),
			Color.FromArgb(240, 0, 128, 0),
			Color.FromArgb(240, 0, 0, 128),
			Color.FromArgb(240, 128, 128, 0),
			Color.FromArgb(240, 128, 0, 128),
			Color.FromArgb(240, 0, 128, 128)
		};

		// Token: 0x04000EE6 RID: 3814
		protected int currentPlot;

		// Token: 0x04000EE7 RID: 3815
		protected ChartArea chartArea;

		// Token: 0x04000EE8 RID: 3816
		protected Chart chart;

		// Token: 0x020004AD RID: 1197
		public struct XYPoint
		{
			// Token: 0x04000EE9 RID: 3817
			public double x;

			// Token: 0x04000EEA RID: 3818
			public double y;
		}
	}
}
