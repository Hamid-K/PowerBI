using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006F RID: 111
	internal sealed class QueryPatternSelector : DataShapeVisitor
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x000140AE File Offset: 0x000122AE
		private QueryPatternSelector(DataShapeContext dsContext, QueryPatternSelectionContext patternSelectionContext)
		{
			this.m_dsContext = dsContext;
			this.m_patternSelectionContext = patternSelectionContext;
			this.m_reasons = new QueryPatternReasonCollection();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000140CF File Offset: 0x000122CF
		public static QueryPatternSelectionResult SelectPattern(DataShapeContext dsContext, QueryPatternSelectionContext patternSelectionContext)
		{
			return new QueryPatternSelector(dsContext, patternSelectionContext).SelectPattern();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000140DD File Offset: 0x000122DD
		private QueryPatternSelectionResult SelectPattern()
		{
			BatchQueryModelChecker.CheckDaxQueryBatchingSupport(this.m_patternSelectionContext.Schema, this.m_reasons);
			QueryPatternSelectorDataShapeAnalyzer.Analyze(this.m_dsContext, this.m_patternSelectionContext, this.m_reasons);
			this.CheckConflictingPatterns();
			return this.CreateResult();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001411C File Offset: 0x0001231C
		private void CheckConflictingPatterns()
		{
			if (this.m_reasons.HasSingleResultPatternReason && this.m_reasons.HasBatchPatternReason)
			{
				this.m_patternSelectionContext.ErrorContext.Register(TranslationMessages.ConflictingQueryPatternRequirements(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_dsContext.Id, "DataShape", QueryPatternReasonCollection.CreateReasonsString<QueryPatternReason>(this.m_reasons.SingleResultPatternReasons, ", "), QueryPatternReasonCollection.CreateReasonsString<QueryPatternReason>(this.m_reasons.BatchPatternReasons, ", ")));
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00014198 File Offset: 0x00012398
		private QueryPatternSelectionResult CreateResult()
		{
			if (this.m_reasons.HasBatchPatternReason && this.m_reasons.HasSingleResultPatternReason)
			{
				return new QueryPatternSelectionResult(QueryPatternKind.Unsupported, this.m_reasons.AllReasons);
			}
			if (this.m_reasons.HasSingleResultPatternReason)
			{
				return new QueryPatternSelectionResult(QueryPatternKind.Regular, this.m_reasons.SingleResultPatternReasons);
			}
			return new QueryPatternSelectionResult(QueryPatternKind.SuperDax, this.m_reasons.BatchPatternReasons);
		}

		// Token: 0x040002D4 RID: 724
		private readonly DataShapeContext m_dsContext;

		// Token: 0x040002D5 RID: 725
		private readonly QueryPatternSelectionContext m_patternSelectionContext;

		// Token: 0x040002D6 RID: 726
		private readonly QueryPatternReasonCollection m_reasons;
	}
}
