using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C9 RID: 201
	internal abstract class ResolvedSemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		protected virtual void Visit(ResolvedQueryDefinition query)
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001BBD2 File Offset: 0x00019DD2
		protected virtual void Visit(DataShapeBinding binding)
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001BBD4 File Offset: 0x00019DD4
		protected virtual void Visit(ResolvedQueryExpression expression)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001BBD6 File Offset: 0x00019DD6
		protected virtual void Visit(ResolvedSemanticQueryDataShapeCommand command)
		{
			this.Visit(command.QueryDataShape);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001BBE4 File Offset: 0x00019DE4
		protected virtual void Visit(ResolvedSemanticQueryDataShape queryDataShape)
		{
			this.Visit(queryDataShape.Query);
			this.Visit(queryDataShape.Binding);
		}
	}
}
