using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	internal sealed class AxisInstance
	{
		// Token: 0x06006803 RID: 26627 RVA: 0x00195558 File Offset: 0x00193758
		internal AxisInstance(ReportProcessing.ProcessingContext pc, Chart chart, Axis axisDef, Axis.Mode mode)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			string text = mode.ToString();
			if (axisDef.Title != null)
			{
				this.m_title = new ChartTitleInstance(pc, chart, axisDef.Title, text);
			}
			this.m_styleAttributeValues = Chart.CreateStyle(pc, axisDef.StyleClass, chart.Name + "." + text, this.m_uniqueName);
			if (axisDef.MajorGridLines != null)
			{
				this.m_majorGridLinesStyleAttributeValues = Chart.CreateStyle(pc, axisDef.MajorGridLines.StyleClass, chart.Name + "." + text + ".MajorGridLines", this.m_uniqueName);
			}
			if (axisDef.MinorGridLines != null)
			{
				this.m_minorGridLinesStyleAttributeValues = Chart.CreateStyle(pc, axisDef.MinorGridLines.StyleClass, chart.Name + "." + text + ".MinorGridLines", this.m_uniqueName);
			}
			if (axisDef.Min != null && ExpressionInfo.Types.Constant != axisDef.Min.Type)
			{
				this.m_minValue = pc.ReportRuntime.EvaluateChartAxisValueExpression(axisDef.ExprHost, axisDef.Min, chart.Name, text + ".Min", Axis.ExpressionType.Min);
			}
			if (axisDef.Max != null && ExpressionInfo.Types.Constant != axisDef.Max.Type)
			{
				this.m_maxValue = pc.ReportRuntime.EvaluateChartAxisValueExpression(axisDef.ExprHost, axisDef.Max, chart.Name, text + ".Max", Axis.ExpressionType.Max);
			}
			if (axisDef.CrossAt != null && ExpressionInfo.Types.Constant != axisDef.CrossAt.Type)
			{
				this.m_crossAtValue = pc.ReportRuntime.EvaluateChartAxisValueExpression(axisDef.ExprHost, axisDef.CrossAt, chart.Name, text + ".CrossAt", Axis.ExpressionType.CrossAt);
			}
			if (axisDef.MajorInterval != null && ExpressionInfo.Types.Constant != axisDef.MajorInterval.Type)
			{
				this.m_majorIntervalValue = pc.ReportRuntime.EvaluateChartAxisValueExpression(axisDef.ExprHost, axisDef.MajorInterval, chart.Name, text + ".MajorInterval", Axis.ExpressionType.MajorInterval);
			}
			if (axisDef.MinorInterval != null && ExpressionInfo.Types.Constant != axisDef.MinorInterval.Type)
			{
				this.m_minorIntervalValue = pc.ReportRuntime.EvaluateChartAxisValueExpression(axisDef.ExprHost, axisDef.MinorInterval, chart.Name, text + ".MinorInterval", Axis.ExpressionType.MinorInterval);
			}
			if (axisDef.CustomProperties != null)
			{
				this.m_customPropertyInstances = axisDef.CustomProperties.EvaluateExpressions(chart.ObjectType, chart.Name, text + ".", pc);
			}
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x001957CA File Offset: 0x001939CA
		internal AxisInstance()
		{
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x06006805 RID: 26629 RVA: 0x001957D2 File Offset: 0x001939D2
		// (set) Token: 0x06006806 RID: 26630 RVA: 0x001957DA File Offset: 0x001939DA
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x06006807 RID: 26631 RVA: 0x001957E3 File Offset: 0x001939E3
		// (set) Token: 0x06006808 RID: 26632 RVA: 0x001957EB File Offset: 0x001939EB
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

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x06006809 RID: 26633 RVA: 0x001957F4 File Offset: 0x001939F4
		// (set) Token: 0x0600680A RID: 26634 RVA: 0x001957FC File Offset: 0x001939FC
		internal object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x0600680B RID: 26635 RVA: 0x00195805 File Offset: 0x00193A05
		// (set) Token: 0x0600680C RID: 26636 RVA: 0x0019580D File Offset: 0x00193A0D
		internal object[] MajorGridLinesStyleAttributeValues
		{
			get
			{
				return this.m_majorGridLinesStyleAttributeValues;
			}
			set
			{
				this.m_majorGridLinesStyleAttributeValues = value;
			}
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x0600680D RID: 26637 RVA: 0x00195816 File Offset: 0x00193A16
		// (set) Token: 0x0600680E RID: 26638 RVA: 0x0019581E File Offset: 0x00193A1E
		internal object[] MinorGridLinesStyleAttributeValues
		{
			get
			{
				return this.m_minorGridLinesStyleAttributeValues;
			}
			set
			{
				this.m_minorGridLinesStyleAttributeValues = value;
			}
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x0600680F RID: 26639 RVA: 0x00195827 File Offset: 0x00193A27
		// (set) Token: 0x06006810 RID: 26640 RVA: 0x0019582F File Offset: 0x00193A2F
		internal object MinValue
		{
			get
			{
				return this.m_minValue;
			}
			set
			{
				this.m_minValue = value;
			}
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x06006811 RID: 26641 RVA: 0x00195838 File Offset: 0x00193A38
		// (set) Token: 0x06006812 RID: 26642 RVA: 0x00195840 File Offset: 0x00193A40
		internal object MaxValue
		{
			get
			{
				return this.m_maxValue;
			}
			set
			{
				this.m_maxValue = value;
			}
		}

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x06006813 RID: 26643 RVA: 0x00195849 File Offset: 0x00193A49
		// (set) Token: 0x06006814 RID: 26644 RVA: 0x00195851 File Offset: 0x00193A51
		internal object CrossAtValue
		{
			get
			{
				return this.m_crossAtValue;
			}
			set
			{
				this.m_crossAtValue = value;
			}
		}

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x06006815 RID: 26645 RVA: 0x0019585A File Offset: 0x00193A5A
		// (set) Token: 0x06006816 RID: 26646 RVA: 0x00195862 File Offset: 0x00193A62
		internal object MajorIntervalValue
		{
			get
			{
				return this.m_majorIntervalValue;
			}
			set
			{
				this.m_majorIntervalValue = value;
			}
		}

		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x06006817 RID: 26647 RVA: 0x0019586B File Offset: 0x00193A6B
		// (set) Token: 0x06006818 RID: 26648 RVA: 0x00195873 File Offset: 0x00193A73
		internal object MinorIntervalValue
		{
			get
			{
				return this.m_minorIntervalValue;
			}
			set
			{
				this.m_minorIntervalValue = value;
			}
		}

		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06006819 RID: 26649 RVA: 0x0019587C File Offset: 0x00193A7C
		// (set) Token: 0x0600681A RID: 26650 RVA: 0x00195884 File Offset: 0x00193A84
		internal DataValueInstanceList CustomPropertyInstances
		{
			get
			{
				return this.m_customPropertyInstances;
			}
			set
			{
				this.m_customPropertyInstances = value;
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x00195890 File Offset: 0x00193A90
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.Title, ObjectType.ChartTitleInstance),
				new MemberInfo(MemberName.StyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.MajorGridLinesStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.MinorGridLinesStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.Min, ObjectType.Variant),
				new MemberInfo(MemberName.Max, ObjectType.Variant),
				new MemberInfo(MemberName.CrossAt, ObjectType.Variant),
				new MemberInfo(MemberName.MajorInterval, ObjectType.Variant),
				new MemberInfo(MemberName.MinorInterval, ObjectType.Variant),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003379 RID: 13177
		private int m_uniqueName;

		// Token: 0x0400337A RID: 13178
		private ChartTitleInstance m_title;

		// Token: 0x0400337B RID: 13179
		private object[] m_styleAttributeValues;

		// Token: 0x0400337C RID: 13180
		private object[] m_majorGridLinesStyleAttributeValues;

		// Token: 0x0400337D RID: 13181
		private object[] m_minorGridLinesStyleAttributeValues;

		// Token: 0x0400337E RID: 13182
		private object m_minValue;

		// Token: 0x0400337F RID: 13183
		private object m_maxValue;

		// Token: 0x04003380 RID: 13184
		private object m_crossAtValue;

		// Token: 0x04003381 RID: 13185
		private object m_majorIntervalValue;

		// Token: 0x04003382 RID: 13186
		private object m_minorIntervalValue;

		// Token: 0x04003383 RID: 13187
		private DataValueInstanceList m_customPropertyInstances;
	}
}
