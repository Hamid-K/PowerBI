using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200008B RID: 139
	internal sealed class ResolvedQueryExpressionReferenceRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x00013FF8 File Offset: 0x000121F8
		internal void AddUpdatedTable(ResolvedQueryTransformTable table)
		{
			if (this._updatedTables == null)
			{
				this._updatedTables = new Dictionary<string, ResolvedQueryTransformTable>(QueryNameComparer.Instance);
			}
			this._updatedTables.Add(table.Name, table);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014024 File Offset: 0x00012224
		internal void RemoveTransforms()
		{
			this._updatedTables = null;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001402D File Offset: 0x0001222D
		internal void AddUpdatedExpressionSource(ResolvedExpressionSource expressionSource)
		{
			if (this._updatedExpressionSources == null)
			{
				this._updatedExpressionSources = new Dictionary<string, ResolvedExpressionSource>(QueryNameComparer.Instance);
			}
			this._updatedExpressionSources.Add(expressionSource.Name, expressionSource);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00014059 File Offset: 0x00012259
		internal void AddUpdatedParameterDeclaration(ResolvedQueryParameterDeclaration declaration)
		{
			if (this._updatedParameterDeclarations == null)
			{
				this._updatedParameterDeclarations = new Dictionary<string, ResolvedQueryParameterDeclaration>(QueryNameComparer.Instance);
			}
			this._updatedParameterDeclarations.Add(declaration.Name, declaration);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014085 File Offset: 0x00012285
		internal void AddUpdatedLetBinding(ResolvedQueryLetBinding binding)
		{
			if (this._updatedLetBindings == null)
			{
				this._updatedLetBindings = new Dictionary<string, ResolvedQueryLetBinding>(QueryNameComparer.Instance);
			}
			this._updatedLetBindings.Add(binding.Name, binding);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000140B4 File Offset: 0x000122B4
		internal void RemoveLetBindings(IReadOnlyList<ResolvedQueryLetBinding> bindings)
		{
			if (this._updatedLetBindings == null || bindings == null)
			{
				return;
			}
			foreach (ResolvedQueryLetBinding resolvedQueryLetBinding in bindings)
			{
				this._updatedLetBindings.Remove(resolvedQueryLetBinding.Name);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014114 File Offset: 0x00012314
		public override ResolvedQueryExpression Visit(ResolvedQueryTransformTableColumnExpression expression)
		{
			ResolvedQueryTransformTable resolvedQueryTransformTable;
			if (this._updatedTables == null || !this._updatedTables.TryGetValue(expression.Table.Name, out resolvedQueryTransformTable))
			{
				return expression;
			}
			ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn;
			if (!resolvedQueryTransformTable.TryGetColumn(expression.Column.Name, out resolvedQueryTransformTableColumn))
			{
				throw new InvalidOperationException("Cannot find required column in rewritten table");
			}
			return resolvedQueryTransformTable.TransformTableColumn(resolvedQueryTransformTableColumn);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001416C File Offset: 0x0001236C
		public override ResolvedQueryExpression Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			ResolvedExpressionSource resolvedExpressionSource;
			if (this._updatedExpressionSources != null && this._updatedExpressionSources.TryGetValue(expression.SourceName, out resolvedExpressionSource))
			{
				return resolvedExpressionSource.ExpressionSourceRef();
			}
			return expression;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000141A0 File Offset: 0x000123A0
		public override ResolvedQueryExpression Visit(ResolvedQueryParameterRefExpression expression)
		{
			ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration;
			if (this._updatedParameterDeclarations != null && this._updatedParameterDeclarations.TryGetValue(expression.Declaration.Name, out resolvedQueryParameterDeclaration))
			{
				return resolvedQueryParameterDeclaration.ParameterRef();
			}
			return expression;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000141D8 File Offset: 0x000123D8
		public override ResolvedQueryExpression Visit(ResolvedQueryLetRefExpression expression)
		{
			ResolvedQueryLetBinding resolvedQueryLetBinding;
			if (this._updatedLetBindings != null && this._updatedLetBindings.TryGetValue(expression.Binding.Name, out resolvedQueryLetBinding))
			{
				return resolvedQueryLetBinding.LetRef();
			}
			return expression;
		}

		// Token: 0x040002F6 RID: 758
		private Dictionary<string, ResolvedQueryTransformTable> _updatedTables;

		// Token: 0x040002F7 RID: 759
		private Dictionary<string, ResolvedExpressionSource> _updatedExpressionSources;

		// Token: 0x040002F8 RID: 760
		private Dictionary<string, ResolvedQueryLetBinding> _updatedLetBindings;

		// Token: 0x040002F9 RID: 761
		private Dictionary<string, ResolvedQueryParameterDeclaration> _updatedParameterDeclarations;
	}
}
