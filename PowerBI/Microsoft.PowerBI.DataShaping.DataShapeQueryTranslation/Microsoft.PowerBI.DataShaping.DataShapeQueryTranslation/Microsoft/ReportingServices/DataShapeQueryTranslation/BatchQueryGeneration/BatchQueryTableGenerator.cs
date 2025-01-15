using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000149 RID: 329
	internal sealed class BatchQueryTableGenerator
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003142C File Offset: 0x0002F62C
		internal BatchQueryTableGenerator(BatchQueryGenerationContext context, GeneratedDeclarationCollection declarations, BatchQueryExpressionReferenceContext referenceContext)
		{
			this.m_context = context;
			this.m_declarations = declarations;
			this.m_referenceContext = referenceContext;
			this.m_reservedColumnNames = new List<string>();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00031454 File Offset: 0x0002F654
		public GeneratedTable Generate(PlanOperation table, IQueryExpressionGenerator expressionGenerator, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			return BatchQueryTableGeneratorImpl.Generate(this.m_context, expressionGenerator, this.m_declarations, this.m_referenceContext, this.m_reservedColumnNames, table, sharedNamingContext);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00031476 File Offset: 0x0002F676
		public IReadOnlyList<GeneratedTable> GenerateTables(PlanOperation table, IQueryExpressionGenerator expressionGenerator, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			return BatchQueryTableGeneratorImpl.GenerateTables(this.m_context, expressionGenerator, this.m_declarations, this.m_referenceContext, this.m_reservedColumnNames, table, sharedNamingContext);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00031498 File Offset: 0x0002F698
		public IEnumerable<GeneratedTable> GenerateTables(IEnumerable<PlanOperation> tables, IQueryExpressionGenerator expressionGenerator, BatchQueryGenerationNamingContext sharedNamingContext)
		{
			List<GeneratedTable> list = new List<GeneratedTable>();
			foreach (PlanOperation planOperation in tables)
			{
				list.AddRange(BatchQueryTableGeneratorImpl.GenerateTables(this.m_context, expressionGenerator, this.m_declarations, this.m_referenceContext, this.m_reservedColumnNames, planOperation, sharedNamingContext));
			}
			return list;
		}

		// Token: 0x0400061E RID: 1566
		private readonly BatchQueryGenerationContext m_context;

		// Token: 0x0400061F RID: 1567
		private readonly GeneratedDeclarationCollection m_declarations;

		// Token: 0x04000620 RID: 1568
		private readonly BatchQueryExpressionReferenceContext m_referenceContext;

		// Token: 0x04000621 RID: 1569
		private readonly IList<string> m_reservedColumnNames;
	}
}
