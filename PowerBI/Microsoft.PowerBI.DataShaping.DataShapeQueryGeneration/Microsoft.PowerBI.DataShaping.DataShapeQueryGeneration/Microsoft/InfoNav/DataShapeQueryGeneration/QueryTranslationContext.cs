using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C3 RID: 195
	[ImmutableObject(false)]
	internal sealed class QueryTranslationContext
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0001AEAC File Offset: 0x000190AC
		internal QueryTranslationContext(DataShapeGenerationInternalContext context, ResolvedQueryDefinition resolvedQuery, DataShapeBinding binding, ResolvedDataReduction resolvedDataReduction, IntermediateQueryTransformContext transformContext, int? legacyMaxRowCount, SemanticQueryDataShapeAnnotations annotations, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, IReadOnlyList<DataShapeBindingHiddenProjections> hiddenProjections, bool suppressModelGrouping, in QueryLetReferenceContext letContext)
		{
			this.SharedContext = context;
			this.QueryDefinition = resolvedQuery;
			this.DataShapeBinding = binding;
			this.ResolvedDataReduction = resolvedDataReduction;
			this.TransformContext = transformContext;
			this.LegacyMaxRowCount = legacyMaxRowCount;
			this.Annotations = annotations;
			this.Expressions = new DsqExpressionGenerator(transformContext.Resolver, sourceRefContext, parameterRefContext, context.ErrorContext, suppressModelGrouping);
			this.SourceRefContext = sourceRefContext;
			this.SuppressedJoinPredicatesByName = suppressedJoinPredicatesByName;
			this.HiddenProjections = hiddenProjections;
			this.LetContext = letContext;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001AF37 File Offset: 0x00019137
		internal DataShapeGenerationInternalContext SharedContext { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001AF3F File Offset: 0x0001913F
		internal DsqExpressionGenerator Expressions { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001AF47 File Offset: 0x00019147
		internal DataShapeBinding DataShapeBinding { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001AF4F File Offset: 0x0001914F
		internal ResolvedDataReduction ResolvedDataReduction { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001AF57 File Offset: 0x00019157
		internal ResolvedQueryDefinition QueryDefinition { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001AF5F File Offset: 0x0001915F
		internal IntermediateQueryTransformContext TransformContext { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001AF67 File Offset: 0x00019167
		internal SemanticQueryDataShapeAnnotations Annotations { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001AF6F File Offset: 0x0001916F
		public int? LegacyMaxRowCount { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001AF77 File Offset: 0x00019177
		internal QuerySourceExpressionReferenceContext SourceRefContext { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001AF7F File Offset: 0x0001917F
		internal IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> SuppressedJoinPredicatesByName { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001AF87 File Offset: 0x00019187
		internal IReadOnlyList<DataShapeBindingHiddenProjections> HiddenProjections { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001AF8F File Offset: 0x0001918F
		internal QueryLetReferenceContext LetContext { get; }
	}
}
