using System;
using System.Threading;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074C RID: 1868
	[Serializable]
	internal sealed class ChartInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x060067A9 RID: 26537 RVA: 0x00194944 File Offset: 0x00192B44
		internal ChartInstanceInfo(ReportProcessing.ProcessingContext pc, Chart reportItemDef, ChartInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
			if (reportItemDef.Title != null)
			{
				this.m_title = new ChartTitleInstance(pc, reportItemDef, reportItemDef.Title, "Title");
			}
			if (reportItemDef.CategoryAxis != null)
			{
				this.m_categoryAxis = new AxisInstance(pc, reportItemDef, reportItemDef.CategoryAxis, Axis.Mode.CategoryAxis);
			}
			if (reportItemDef.ValueAxis != null)
			{
				this.m_valueAxis = new AxisInstance(pc, reportItemDef, reportItemDef.ValueAxis, Axis.Mode.ValueAxis);
			}
			if (reportItemDef.Legend != null)
			{
				this.m_legendStyleAttributeValues = Chart.CreateStyle(pc, reportItemDef.Legend.StyleClass, reportItemDef.Name + ".Legend", owner.UniqueName);
			}
			if (reportItemDef.PlotArea != null)
			{
				this.m_plotAreaStyleAttributeValues = Chart.CreateStyle(pc, reportItemDef.PlotArea.StyleClass, reportItemDef.Name + ".PlotArea", owner.UniqueName);
			}
			this.SaveChartCulture();
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x00194A46 File Offset: 0x00192C46
		internal ChartInstanceInfo(Chart reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x060067AB RID: 26539 RVA: 0x00194A4F File Offset: 0x00192C4F
		// (set) Token: 0x060067AC RID: 26540 RVA: 0x00194A57 File Offset: 0x00192C57
		internal AxisInstance CategoryAxis
		{
			get
			{
				return this.m_categoryAxis;
			}
			set
			{
				this.m_categoryAxis = value;
			}
		}

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x060067AD RID: 26541 RVA: 0x00194A60 File Offset: 0x00192C60
		// (set) Token: 0x060067AE RID: 26542 RVA: 0x00194A68 File Offset: 0x00192C68
		internal AxisInstance ValueAxis
		{
			get
			{
				return this.m_valueAxis;
			}
			set
			{
				this.m_valueAxis = value;
			}
		}

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x060067AF RID: 26543 RVA: 0x00194A71 File Offset: 0x00192C71
		// (set) Token: 0x060067B0 RID: 26544 RVA: 0x00194A79 File Offset: 0x00192C79
		internal ChartTitleInstance Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x00194A82 File Offset: 0x00192C82
		// (set) Token: 0x060067B2 RID: 26546 RVA: 0x00194A8A File Offset: 0x00192C8A
		internal object[] PlotAreaStyleAttributeValues
		{
			get
			{
				return this.m_plotAreaStyleAttributeValues;
			}
			set
			{
				this.m_plotAreaStyleAttributeValues = value;
			}
		}

		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x060067B3 RID: 26547 RVA: 0x00194A93 File Offset: 0x00192C93
		// (set) Token: 0x060067B4 RID: 26548 RVA: 0x00194A9B File Offset: 0x00192C9B
		internal object[] LegendStyleAttributeValues
		{
			get
			{
				return this.m_legendStyleAttributeValues;
			}
			set
			{
				this.m_legendStyleAttributeValues = value;
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x00194AA4 File Offset: 0x00192CA4
		// (set) Token: 0x060067B6 RID: 26550 RVA: 0x00194AAC File Offset: 0x00192CAC
		internal string CultureName
		{
			get
			{
				return this.m_cultureName;
			}
			set
			{
				this.m_cultureName = value;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x060067B7 RID: 26551 RVA: 0x00194AB5 File Offset: 0x00192CB5
		// (set) Token: 0x060067B8 RID: 26552 RVA: 0x00194ABD File Offset: 0x00192CBD
		internal string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00194AC8 File Offset: 0x00192CC8
		private void SaveChartCulture()
		{
			if (this.m_reportItemDef.StyleClass != null && this.m_reportItemDef.StyleClass.StyleAttributes != null)
			{
				AttributeInfo attributeInfo = this.m_reportItemDef.StyleClass.StyleAttributes["Language"];
				if (attributeInfo != null)
				{
					if (attributeInfo.IsExpression)
					{
						this.m_cultureName = (string)this.m_styleAttributeValues[attributeInfo.IntValue];
					}
					else
					{
						this.m_cultureName = attributeInfo.Value;
					}
				}
			}
			if (this.m_cultureName == null)
			{
				this.m_cultureName = Thread.CurrentThread.CurrentCulture.Name;
			}
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00194B60 File Offset: 0x00192D60
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.CategoryAxis, ObjectType.AxisInstance),
				new MemberInfo(MemberName.ValueAxis, ObjectType.AxisInstance),
				new MemberInfo(MemberName.Title, ObjectType.ChartTitleInstance),
				new MemberInfo(MemberName.PlotAreaStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.LegendStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.CultureName, Token.String),
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x04003359 RID: 13145
		private AxisInstance m_categoryAxis;

		// Token: 0x0400335A RID: 13146
		private AxisInstance m_valueAxis;

		// Token: 0x0400335B RID: 13147
		private ChartTitleInstance m_title;

		// Token: 0x0400335C RID: 13148
		private object[] m_plotAreaStyleAttributeValues;

		// Token: 0x0400335D RID: 13149
		private object[] m_legendStyleAttributeValues;

		// Token: 0x0400335E RID: 13150
		private string m_cultureName;

		// Token: 0x0400335F RID: 13151
		private string m_noRows;
	}
}
