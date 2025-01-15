using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000768 RID: 1896
	[Serializable]
	internal sealed class CustomReportItemHeadingInstance
	{
		// Token: 0x06006935 RID: 26933 RVA: 0x0019990C File Offset: 0x00197B0C
		internal CustomReportItemHeadingInstance(ReportProcessing.ProcessingContext pc, int headingCellIndex, CustomReportItemHeading headingDef, VariantList groupExpressionValues, int recursiveLevel)
		{
			if (headingDef.InnerHeadings != null)
			{
				this.m_subHeadingInstances = new CustomReportItemHeadingInstanceList();
			}
			this.m_headingDef = headingDef;
			this.m_headingCellIndex = headingCellIndex;
			if (groupExpressionValues != null)
			{
				this.m_groupExpressionValues = new VariantList(groupExpressionValues.Count);
				for (int i = 0; i < groupExpressionValues.Count; i++)
				{
					if (groupExpressionValues[i] == null || DBNull.Value == groupExpressionValues[i])
					{
						this.m_groupExpressionValues.Add(null);
					}
					else
					{
						this.m_groupExpressionValues.Add(groupExpressionValues[i]);
					}
				}
			}
			if (headingDef.Grouping != null && headingDef.Grouping.GroupLabel != null)
			{
				this.m_label = pc.NavigationInfo.RegisterLabel(pc.ReportRuntime.EvaluateGroupingLabelExpression(headingDef.Grouping, headingDef.DataRegionDef.ObjectType, headingDef.DataRegionDef.Name));
			}
			if (headingDef.CustomProperties != null)
			{
				this.m_customPropertyInstances = headingDef.CustomProperties.EvaluateExpressions(headingDef.DataRegionDef.ObjectType, headingDef.DataRegionDef.Name, "DataGrouping.", pc);
			}
			this.m_recursiveLevel = recursiveLevel;
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x00199A3C File Offset: 0x00197C3C
		internal CustomReportItemHeadingInstance()
		{
		}

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x06006937 RID: 26935 RVA: 0x00199A52 File Offset: 0x00197C52
		// (set) Token: 0x06006938 RID: 26936 RVA: 0x00199A5A File Offset: 0x00197C5A
		internal CustomReportItemHeadingInstanceList SubHeadingInstances
		{
			get
			{
				return this.m_subHeadingInstances;
			}
			set
			{
				this.m_subHeadingInstances = value;
			}
		}

		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x06006939 RID: 26937 RVA: 0x00199A63 File Offset: 0x00197C63
		// (set) Token: 0x0600693A RID: 26938 RVA: 0x00199A6B File Offset: 0x00197C6B
		internal CustomReportItemHeading HeadingDefinition
		{
			get
			{
				return this.m_headingDef;
			}
			set
			{
				this.m_headingDef = value;
			}
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x0600693B RID: 26939 RVA: 0x00199A74 File Offset: 0x00197C74
		// (set) Token: 0x0600693C RID: 26940 RVA: 0x00199A7C File Offset: 0x00197C7C
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

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x00199A85 File Offset: 0x00197C85
		// (set) Token: 0x0600693E RID: 26942 RVA: 0x00199A8D File Offset: 0x00197C8D
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

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x00199A96 File Offset: 0x00197C96
		// (set) Token: 0x06006940 RID: 26944 RVA: 0x00199A9E File Offset: 0x00197C9E
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

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x00199AA7 File Offset: 0x00197CA7
		// (set) Token: 0x06006942 RID: 26946 RVA: 0x00199AAF File Offset: 0x00197CAF
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x00199AB8 File Offset: 0x00197CB8
		// (set) Token: 0x06006944 RID: 26948 RVA: 0x00199AC0 File Offset: 0x00197CC0
		internal VariantList GroupExpressionValues
		{
			get
			{
				return this.m_groupExpressionValues;
			}
			set
			{
				this.m_groupExpressionValues = value;
			}
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06006945 RID: 26949 RVA: 0x00199AC9 File Offset: 0x00197CC9
		internal int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x00199AD4 File Offset: 0x00197CD4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.SubHeadingInstances, ObjectType.CustomReportItemHeadingInstanceList),
				new MemberInfo(MemberName.HeadingDefinition, Token.Reference, ObjectType.CustomReportItemHeading),
				new MemberInfo(MemberName.HeadingCellIndex, Token.Int32),
				new MemberInfo(MemberName.HeadingSpan, Token.Int32),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.GroupExpressionValue, ObjectType.VariantList)
			});
		}

		// Token: 0x040033D4 RID: 13268
		private CustomReportItemHeadingInstanceList m_subHeadingInstances;

		// Token: 0x040033D5 RID: 13269
		[Reference]
		private CustomReportItemHeading m_headingDef;

		// Token: 0x040033D6 RID: 13270
		private int m_headingCellIndex;

		// Token: 0x040033D7 RID: 13271
		private int m_headingSpan = 1;

		// Token: 0x040033D8 RID: 13272
		private DataValueInstanceList m_customPropertyInstances;

		// Token: 0x040033D9 RID: 13273
		private string m_label;

		// Token: 0x040033DA RID: 13274
		private VariantList m_groupExpressionValues;

		// Token: 0x040033DB RID: 13275
		[NonSerialized]
		private int m_recursiveLevel = -1;
	}
}
