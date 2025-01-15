using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073B RID: 1851
	[Serializable]
	internal class MatrixHeadingInstanceInfo : InstanceInfo
	{
		// Token: 0x060066E3 RID: 26339 RVA: 0x0019273C File Offset: 0x0019093C
		internal MatrixHeadingInstanceInfo(ReportProcessing.ProcessingContext pc, int headingCellIndex, MatrixHeading matrixHeadingDef, MatrixHeadingInstance owner, bool isSubtotal, int reportItemDefIndex, VariantList groupExpressionValues, out NonComputedUniqueNames nonComputedUniqueNames)
		{
			ReportItemCollection reportItemCollection;
			if (isSubtotal)
			{
				reportItemCollection = matrixHeadingDef.Subtotal.ReportItems;
			}
			else
			{
				reportItemCollection = matrixHeadingDef.ReportItems;
				if (matrixHeadingDef.OwcGroupExpression)
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
			}
			if (0 < reportItemCollection.Count && !reportItemCollection.IsReportItemComputed(reportItemDefIndex))
			{
				this.m_contentUniqueNames = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, reportItemCollection[reportItemDefIndex]);
			}
			nonComputedUniqueNames = this.m_contentUniqueNames;
			this.m_headingCellIndex = headingCellIndex;
			if (!isSubtotal && pc.ShowHideType != Report.ShowHideTypes.None)
			{
				this.m_startHidden = pc.ProcessReceiver(owner.UniqueName, matrixHeadingDef.Visibility, matrixHeadingDef.ExprHost, matrixHeadingDef.DataRegionDef.ObjectType, matrixHeadingDef.DataRegionDef.Name);
			}
			if (matrixHeadingDef.Grouping != null && matrixHeadingDef.Grouping.GroupLabel != null)
			{
				this.m_label = pc.NavigationInfo.RegisterLabel(pc.ReportRuntime.EvaluateGroupingLabelExpression(matrixHeadingDef.Grouping, matrixHeadingDef.DataRegionDef.ObjectType, matrixHeadingDef.DataRegionDef.Name));
			}
			if (matrixHeadingDef.Grouping != null && matrixHeadingDef.Grouping.CustomProperties != null)
			{
				this.m_customPropertyInstances = matrixHeadingDef.Grouping.CustomProperties.EvaluateExpressions(matrixHeadingDef.DataRegionDef.ObjectType, matrixHeadingDef.DataRegionDef.Name, matrixHeadingDef.Grouping.Name + ".", pc);
			}
			matrixHeadingDef.StartHidden = this.m_startHidden;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x001928C9 File Offset: 0x00190AC9
		internal MatrixHeadingInstanceInfo()
		{
		}

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x060066E5 RID: 26341 RVA: 0x001928D8 File Offset: 0x00190AD8
		// (set) Token: 0x060066E6 RID: 26342 RVA: 0x001928E0 File Offset: 0x00190AE0
		internal NonComputedUniqueNames ContentUniqueNames
		{
			get
			{
				return this.m_contentUniqueNames;
			}
			set
			{
				this.m_contentUniqueNames = value;
			}
		}

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x001928E9 File Offset: 0x00190AE9
		// (set) Token: 0x060066E8 RID: 26344 RVA: 0x001928F1 File Offset: 0x00190AF1
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x001928FA File Offset: 0x00190AFA
		// (set) Token: 0x060066EA RID: 26346 RVA: 0x00192902 File Offset: 0x00190B02
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

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x060066EB RID: 26347 RVA: 0x0019290B File Offset: 0x00190B0B
		// (set) Token: 0x060066EC RID: 26348 RVA: 0x00192913 File Offset: 0x00190B13
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

		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x060066ED RID: 26349 RVA: 0x0019291C File Offset: 0x00190B1C
		// (set) Token: 0x060066EE RID: 26350 RVA: 0x00192924 File Offset: 0x00190B24
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

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x060066EF RID: 26351 RVA: 0x0019292D File Offset: 0x00190B2D
		// (set) Token: 0x060066F0 RID: 26352 RVA: 0x00192935 File Offset: 0x00190B35
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

		// Token: 0x17002466 RID: 9318
		// (get) Token: 0x060066F1 RID: 26353 RVA: 0x0019293E File Offset: 0x00190B3E
		// (set) Token: 0x060066F2 RID: 26354 RVA: 0x00192946 File Offset: 0x00190B46
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

		// Token: 0x060066F3 RID: 26355 RVA: 0x00192950 File Offset: 0x00190B50
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ContentUniqueNames, ObjectType.NonComputedUniqueNames),
				new MemberInfo(MemberName.StartHidden, Token.Boolean),
				new MemberInfo(MemberName.HeadingCellIndex, Token.Int32),
				new MemberInfo(MemberName.HeadingSpan, Token.Int32),
				new MemberInfo(MemberName.GroupExpressionValue, ObjectType.Variant),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x04003321 RID: 13089
		private NonComputedUniqueNames m_contentUniqueNames;

		// Token: 0x04003322 RID: 13090
		private bool m_startHidden;

		// Token: 0x04003323 RID: 13091
		private int m_headingCellIndex;

		// Token: 0x04003324 RID: 13092
		private int m_headingSpan = 1;

		// Token: 0x04003325 RID: 13093
		private object m_groupExpressionValue;

		// Token: 0x04003326 RID: 13094
		private string m_label;

		// Token: 0x04003327 RID: 13095
		private DataValueInstanceList m_customPropertyInstances;
	}
}
