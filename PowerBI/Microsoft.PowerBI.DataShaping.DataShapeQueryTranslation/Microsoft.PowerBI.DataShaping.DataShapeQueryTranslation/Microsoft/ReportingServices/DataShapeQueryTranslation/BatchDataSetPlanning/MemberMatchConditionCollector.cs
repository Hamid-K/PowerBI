using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000190 RID: 400
	internal sealed class MemberMatchConditionCollector : DataShapeVisitor
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x000385B8 File Offset: 0x000367B8
		private MemberMatchConditionCollector(DataShapeAnnotations annotations, string outputTableName)
		{
			this.m_annotations = annotations;
			this.m_outputTableName = outputTableName;
			this.m_matchConditions = new IntermediateMemberMatchConditions();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000385DC File Offset: 0x000367DC
		public static IntermediateMemberMatchConditions CollectMatchConditions(DataShapeAnnotations annotations, DataHierarchy hierarchy, string outputTableName)
		{
			MemberMatchConditionCollector memberMatchConditionCollector = new MemberMatchConditionCollector(annotations, outputTableName);
			memberMatchConditionCollector.Visit(hierarchy);
			if (memberMatchConditionCollector.m_matchConditions.Count != 0)
			{
				return memberMatchConditionCollector.m_matchConditions;
			}
			return null;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00038610 File Offset: 0x00036810
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.ContextOnly)
			{
				return;
			}
			BatchSubtotalAnnotation batchSubtotalAnnotation2;
			if (dataMember.IsDynamic)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (this.m_annotations.TryGetBatchSubtotalAnnotation(dataMember, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput())
				{
					this.m_matchConditions.Add(dataMember, new IntermediateMatchCondition(batchSubtotalAnnotation.SubtotalIndicatorColumnName, this.m_outputTableName, false));
					return;
				}
			}
			else if (this.m_annotations.TryGetBatchSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation2))
			{
				this.m_matchConditions.Add(dataMember, new IntermediateMatchCondition(batchSubtotalAnnotation2.SubtotalIndicatorColumnName, this.m_outputTableName, true));
			}
		}

		// Token: 0x040006C2 RID: 1730
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040006C3 RID: 1731
		private readonly string m_outputTableName;

		// Token: 0x040006C4 RID: 1732
		private readonly IntermediateMemberMatchConditions m_matchConditions;
	}
}
