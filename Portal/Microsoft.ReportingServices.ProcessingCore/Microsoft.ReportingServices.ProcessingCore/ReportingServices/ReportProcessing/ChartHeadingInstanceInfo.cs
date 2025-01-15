using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074F RID: 1871
	[Serializable]
	internal class ChartHeadingInstanceInfo : InstanceInfo
	{
		// Token: 0x060067D1 RID: 26577 RVA: 0x00194E08 File Offset: 0x00193008
		internal ChartHeadingInstanceInfo(ReportProcessing.ProcessingContext pc, int headingCellIndex, ChartHeading chartHeadingDef, int labelIndex, VariantList groupExpressionValues)
		{
			this.m_headingCellIndex = headingCellIndex;
			if (chartHeadingDef.ChartGroupExpression)
			{
				if (groupExpressionValues == null || DBNull.Value == groupExpressionValues[0])
				{
					this.m_groupExpressionValue = null;
				}
				else
				{
					this.m_groupExpressionValue = groupExpressionValues[0];
				}
			}
			if (chartHeadingDef.Labels != null)
			{
				ExpressionInfo expressionInfo = chartHeadingDef.Labels[labelIndex];
				if (expressionInfo != null)
				{
					if (chartHeadingDef.Grouping != null)
					{
						this.m_headingLabel = pc.ReportRuntime.EvaluateChartDynamicHeadingLabelExpression(chartHeadingDef, expressionInfo, chartHeadingDef.DataRegionDef.Name);
					}
					else
					{
						this.m_headingLabel = pc.ReportRuntime.EvaluateChartStaticHeadingLabelExpression(chartHeadingDef, expressionInfo, chartHeadingDef.DataRegionDef.Name);
					}
				}
			}
			if (chartHeadingDef.Grouping == null)
			{
				this.m_staticGroupingIndex = labelIndex;
				return;
			}
			if (chartHeadingDef.Grouping.CustomProperties != null)
			{
				this.m_customPropertyInstances = chartHeadingDef.Grouping.CustomProperties.EvaluateExpressions(chartHeadingDef.DataRegionDef.ObjectType, chartHeadingDef.DataRegionDef.Name, chartHeadingDef.Grouping.Name + ".", pc);
			}
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x00194F21 File Offset: 0x00193121
		internal ChartHeadingInstanceInfo()
		{
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00194F37 File Offset: 0x00193137
		// (set) Token: 0x060067D4 RID: 26580 RVA: 0x00194F3F File Offset: 0x0019313F
		internal object HeadingLabel
		{
			get
			{
				return this.m_headingLabel;
			}
			set
			{
				this.m_headingLabel = value;
			}
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x060067D5 RID: 26581 RVA: 0x00194F48 File Offset: 0x00193148
		// (set) Token: 0x060067D6 RID: 26582 RVA: 0x00194F50 File Offset: 0x00193150
		internal int HeadingCellIndex
		{
			get
			{
				return this.m_headingCellIndex;
			}
			set
			{
				this.m_headingCellIndex = value;
			}
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x060067D7 RID: 26583 RVA: 0x00194F59 File Offset: 0x00193159
		// (set) Token: 0x060067D8 RID: 26584 RVA: 0x00194F61 File Offset: 0x00193161
		internal int HeadingSpan
		{
			get
			{
				return this.m_headingSpan;
			}
			set
			{
				this.m_headingSpan = value;
			}
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x00194F6A File Offset: 0x0019316A
		// (set) Token: 0x060067DA RID: 26586 RVA: 0x00194F72 File Offset: 0x00193172
		internal object GroupExpressionValue
		{
			get
			{
				return this.m_groupExpressionValue;
			}
			set
			{
				this.m_groupExpressionValue = value;
			}
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x060067DB RID: 26587 RVA: 0x00194F7B File Offset: 0x0019317B
		// (set) Token: 0x060067DC RID: 26588 RVA: 0x00194F83 File Offset: 0x00193183
		internal int StaticGroupingIndex
		{
			get
			{
				return this.m_staticGroupingIndex;
			}
			set
			{
				this.m_staticGroupingIndex = value;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x060067DD RID: 26589 RVA: 0x00194F8C File Offset: 0x0019318C
		// (set) Token: 0x060067DE RID: 26590 RVA: 0x00194F94 File Offset: 0x00193194
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

		// Token: 0x060067DF RID: 26591 RVA: 0x00194FA0 File Offset: 0x001931A0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.HeadingLabel, ObjectType.Variant),
				new MemberInfo(MemberName.HeadingCellIndex, Token.Int32),
				new MemberInfo(MemberName.HeadingSpan, Token.Int32),
				new MemberInfo(MemberName.GroupExpressionValue, ObjectType.Variant),
				new MemberInfo(MemberName.StaticGroupingIndex, Token.Int32),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003367 RID: 13159
		private object m_headingLabel;

		// Token: 0x04003368 RID: 13160
		private int m_headingCellIndex;

		// Token: 0x04003369 RID: 13161
		private int m_headingSpan = 1;

		// Token: 0x0400336A RID: 13162
		private object m_groupExpressionValue;

		// Token: 0x0400336B RID: 13163
		private int m_staticGroupingIndex = -1;

		// Token: 0x0400336C RID: 13164
		private DataValueInstanceList m_customPropertyInstances;
	}
}
