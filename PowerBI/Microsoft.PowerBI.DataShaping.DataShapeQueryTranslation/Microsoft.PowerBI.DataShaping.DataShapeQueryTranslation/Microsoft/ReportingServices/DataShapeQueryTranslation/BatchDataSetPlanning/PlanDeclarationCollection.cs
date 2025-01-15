using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000195 RID: 405
	internal sealed class PlanDeclarationCollection
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x000388D0 File Offset: 0x00036AD0
		internal PlanDeclarationCollection(ExpressionTable expressionTable)
		{
			this.m_declarations = new List<IPlanNamedItem>();
			PlanOperationDeclarationEqualityComparer planOperationDeclarationEqualityComparer = new PlanOperationDeclarationEqualityComparer(new FilterConditionComparer(new ExpressionComparerByNode(expressionTable, ExpressionNode.Comparer)));
			this.m_declaredTables = new Dictionary<PlanOperation, PlanOperationDeclarationReference>(planOperationDeclarationEqualityComparer);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00038910 File Offset: 0x00036B10
		public PlanOperationDeclarationReference DeclareTable(string name, PlanOperation table, bool canExpandToMultiTables = false, bool isFragmentOfExistingDeclaration = false, string namingContextId = null, bool useGlobalNamingContext = false)
		{
			PlanOperationDeclarationReference planOperationDeclarationReference;
			if (this.m_declaredTables.TryGetValue(table, out planOperationDeclarationReference))
			{
				return planOperationDeclarationReference;
			}
			PlanNamedTable planNamedTable = new PlanNamedTable(name, table, canExpandToMultiTables, false, isFragmentOfExistingDeclaration, namingContextId, useGlobalNamingContext);
			this.m_declarations.Add(planNamedTable);
			planOperationDeclarationReference = new PlanOperationDeclarationReference(name, canExpandToMultiTables);
			this.m_declaredTables.Add(table, planOperationDeclarationReference);
			return planOperationDeclarationReference;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00038964 File Offset: 0x00036B64
		public BatchScalarDeclarationReferenceExpressionNode DeclareScalar(string name, PlanExpression expression)
		{
			PlanNamedScalar planNamedScalar = new PlanNamedScalar(name, expression);
			this.m_declarations.Add(planNamedScalar);
			return new BatchScalarDeclarationReferenceExpressionNode(name);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003898C File Offset: 0x00036B8C
		public void DeclareEntity(string name, PlanOperation table, PlanVisualShape visualShape, IReadOnlyList<Calculation> additionalColumns, IReadOnlyList<Calculation> subtotalsOverAdditionalColumns)
		{
			PlanNamedEntity planNamedEntity = new PlanNamedEntity(name, table, visualShape, additionalColumns, subtotalsOverAdditionalColumns);
			this.m_declarations.Add(planNamedEntity);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000389B2 File Offset: 0x00036BB2
		public IReadOnlyList<IPlanNamedItem> GetDeclarations()
		{
			return this.m_declarations;
		}

		// Token: 0x040006CA RID: 1738
		private readonly List<IPlanNamedItem> m_declarations;

		// Token: 0x040006CB RID: 1739
		private readonly Dictionary<PlanOperation, PlanOperationDeclarationReference> m_declaredTables;
	}
}
