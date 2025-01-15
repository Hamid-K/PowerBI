using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000752 RID: 1874
	[Serializable]
	internal sealed class ChartDataPointInstanceInfo : InstanceInfo
	{
		// Token: 0x060067F0 RID: 26608 RVA: 0x00195224 File Offset: 0x00193424
		internal ChartDataPointInstanceInfo(ReportProcessing.ProcessingContext pc, Chart chart, ChartDataPoint dataPointDef, int dataPointIndex, ChartDataPointInstance owner)
		{
			this.m_dataPointIndex = dataPointIndex;
			int count = dataPointDef.DataValues.Count;
			this.m_dataValues = new object[count];
			bool flag = false;
			if (dataPointDef.Action != null)
			{
				flag = dataPointDef.Action.ResetObjectModelForDrillthroughContext(pc.ReportObjectModel, dataPointDef);
			}
			for (int i = 0; i < count; i++)
			{
				this.m_dataValues[i] = pc.ReportRuntime.EvaluateChartDataPointDataValueExpression(dataPointDef, dataPointDef.DataValues[i], chart.Name);
			}
			if (flag)
			{
				dataPointDef.Action.GetSelectedItemsForDrillthroughContext(pc.ReportObjectModel, dataPointDef);
			}
			if (dataPointDef.DataLabel != null)
			{
				this.m_dataLabelStyleAttributeValues = Chart.CreateStyle(pc, dataPointDef.DataLabel.StyleClass, chart.Name + ".DataLabel", owner.UniqueName);
				this.m_dataLabelValue = pc.ReportRuntime.EvaluateChartDataLabelValueExpression(dataPointDef, chart.Name, this.m_dataLabelStyleAttributeValues);
			}
			if (dataPointDef.Action != null)
			{
				this.m_action = ReportProcessing.RuntimeRICollection.CreateActionInstance(pc, dataPointDef, owner.UniqueName, chart.ObjectType, chart.Name + ".DataPoint");
			}
			this.m_styleAttributeValues = Chart.CreateStyle(pc, dataPointDef.StyleClass, chart.Name + ".DataPoint", owner.UniqueName);
			if (dataPointDef.MarkerStyleClass != null)
			{
				this.m_markerStyleAttributeValues = Chart.CreateStyle(pc, dataPointDef.MarkerStyleClass, chart.Name + ".DataPoint.Marker", owner.UniqueName);
			}
			if (dataPointDef.CustomProperties != null)
			{
				this.m_customPropertyInstances = dataPointDef.CustomProperties.EvaluateExpressions(chart.ObjectType, chart.Name, "DataPoint(" + (dataPointIndex + 1).ToString(CultureInfo.InvariantCulture) + ").", pc);
			}
			pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x001953FC File Offset: 0x001935FC
		internal ChartDataPointInstanceInfo()
		{
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x0019540B File Offset: 0x0019360B
		// (set) Token: 0x060067F3 RID: 26611 RVA: 0x00195413 File Offset: 0x00193613
		internal int DataPointIndex
		{
			get
			{
				return this.m_dataPointIndex;
			}
			set
			{
				this.m_dataPointIndex = value;
			}
		}

		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x0019541C File Offset: 0x0019361C
		// (set) Token: 0x060067F5 RID: 26613 RVA: 0x00195424 File Offset: 0x00193624
		internal object[] DataValues
		{
			get
			{
				return this.m_dataValues;
			}
			set
			{
				this.m_dataValues = value;
			}
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x0019542D File Offset: 0x0019362D
		// (set) Token: 0x060067F7 RID: 26615 RVA: 0x00195435 File Offset: 0x00193635
		internal string DataLabelValue
		{
			get
			{
				return this.m_dataLabelValue;
			}
			set
			{
				this.m_dataLabelValue = value;
			}
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x0019543E File Offset: 0x0019363E
		// (set) Token: 0x060067F9 RID: 26617 RVA: 0x00195446 File Offset: 0x00193646
		internal object[] DataLabelStyleAttributeValues
		{
			get
			{
				return this.m_dataLabelStyleAttributeValues;
			}
			set
			{
				this.m_dataLabelStyleAttributeValues = value;
			}
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x060067FA RID: 26618 RVA: 0x0019544F File Offset: 0x0019364F
		// (set) Token: 0x060067FB RID: 26619 RVA: 0x00195457 File Offset: 0x00193657
		internal ActionInstance Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x060067FC RID: 26620 RVA: 0x00195460 File Offset: 0x00193660
		// (set) Token: 0x060067FD RID: 26621 RVA: 0x00195468 File Offset: 0x00193668
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

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x00195471 File Offset: 0x00193671
		// (set) Token: 0x060067FF RID: 26623 RVA: 0x00195479 File Offset: 0x00193679
		internal object[] MarkerStyleAttributeValues
		{
			get
			{
				return this.m_markerStyleAttributeValues;
			}
			set
			{
				this.m_markerStyleAttributeValues = value;
			}
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x06006800 RID: 26624 RVA: 0x00195482 File Offset: 0x00193682
		// (set) Token: 0x06006801 RID: 26625 RVA: 0x0019548A File Offset: 0x0019368A
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

		// Token: 0x06006802 RID: 26626 RVA: 0x00195494 File Offset: 0x00193694
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.DataPointIndex, Token.Int32),
				new MemberInfo(MemberName.DataValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.DataLabelValue, Token.String),
				new MemberInfo(MemberName.DataLabelStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.Action, ObjectType.ActionInstance),
				new MemberInfo(MemberName.StyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.MarkerStyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003371 RID: 13169
		private int m_dataPointIndex = -1;

		// Token: 0x04003372 RID: 13170
		private object[] m_dataValues;

		// Token: 0x04003373 RID: 13171
		private string m_dataLabelValue;

		// Token: 0x04003374 RID: 13172
		private object[] m_dataLabelStyleAttributeValues;

		// Token: 0x04003375 RID: 13173
		private ActionInstance m_action;

		// Token: 0x04003376 RID: 13174
		private object[] m_styleAttributeValues;

		// Token: 0x04003377 RID: 13175
		private object[] m_markerStyleAttributeValues;

		// Token: 0x04003378 RID: 13176
		private DataValueInstanceList m_customPropertyInstances;
	}
}
