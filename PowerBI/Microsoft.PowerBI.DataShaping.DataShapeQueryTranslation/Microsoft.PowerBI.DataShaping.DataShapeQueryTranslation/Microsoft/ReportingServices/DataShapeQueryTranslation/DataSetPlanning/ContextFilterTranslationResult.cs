using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F1 RID: 241
	internal sealed class ContextFilterTranslationResult
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x000250E3 File Offset: 0x000232E3
		internal ContextFilterTranslationResult(ScopeTree scopeTree, DataShape outputDataShape, Dictionary<IIdentifiable, List<Filter>> translatedFilterTable, ReadOnlyExpressionTable outputExpressionTable)
		{
			this.m_scopeTree = scopeTree;
			this.m_translatedFilterTable = translatedFilterTable;
			this.m_outputDataShape = outputDataShape;
			this.m_outputExpressionTable = outputExpressionTable;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00025108 File Offset: 0x00023308
		public ScopeTree ScopeTree
		{
			get
			{
				return this.m_scopeTree;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00025110 File Offset: 0x00023310
		public DataShape OutputDataShape
		{
			get
			{
				return this.m_outputDataShape;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00025118 File Offset: 0x00023318
		public Dictionary<IIdentifiable, List<Filter>> TranslatedFilterTable
		{
			get
			{
				return this.m_translatedFilterTable;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00025120 File Offset: 0x00023320
		public ReadOnlyExpressionTable OutputExpressionTable
		{
			get
			{
				return this.m_outputExpressionTable;
			}
		}

		// Token: 0x04000498 RID: 1176
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000499 RID: 1177
		private readonly DataShape m_outputDataShape;

		// Token: 0x0400049A RID: 1178
		private readonly Dictionary<IIdentifiable, List<Filter>> m_translatedFilterTable;

		// Token: 0x0400049B RID: 1179
		private readonly ReadOnlyExpressionTable m_outputExpressionTable;
	}
}
