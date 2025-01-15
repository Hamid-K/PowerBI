using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000BD RID: 189
	internal sealed class DataTransformInliner : DataShapeVisitor
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
		private DataTransformInliner(ExpressionTable expressionTable)
		{
			this.m_expressionTable = expressionTable;
			this.m_expressionTransform = new DataTransformInlinerExpressionTransform(expressionTable);
			this.m_outputExpressionTable = expressionTable.CopyTable();
			this.m_expressionsToRestore = new List<ExpressionRestorationInfo>();
			this.m_sourceColumns = new Dictionary<DataTransformTableColumn, DataTransformColumnInliningInfo>();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001F5F1 File Offset: 0x0001D7F1
		public static DataTransformInliningResult InlineTransforms(DataShape dataShape, ExpressionTable expressionTable, TranslationErrorContext errorContext, bool applyTransformsInQuery)
		{
			if (dataShape.Transforms.IsNullOrEmpty<DataTransform>() || applyTransformsInQuery)
			{
				return new DataTransformInliningResult(expressionTable.AsReadOnly());
			}
			DataTransformInliner dataTransformInliner = new DataTransformInliner(expressionTable);
			dataTransformInliner.Visit(dataShape);
			return dataTransformInliner.BuildResult();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001F620 File Offset: 0x0001D820
		private DataTransformInliningResult BuildResult()
		{
			return new DataTransformInliningResult(this.m_outputExpressionTable.AsReadOnly(), this.m_expressionsToRestore, this.m_sourceColumns);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001F63E File Offset: 0x0001D83E
		protected override void Visit(Calculation calculation)
		{
			this.VisitExpression(calculation, calculation.Value);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001F650 File Offset: 0x0001D850
		protected override void Enter(DataMember dataMember)
		{
			Group group = dataMember.Group;
			if (group == null)
			{
				return;
			}
			this.Visit<GroupKey>(dataMember, group.GroupKeys, new Action<IContextItem, GroupKey>(this.Visit));
			this.Visit<SortKey>(dataMember, group.SortKeys, new Action<IContextItem, SortKey>(this.Visit));
			if (group.ScopeIdDefinition != null)
			{
				this.Visit<ScopeValueDefinition>(dataMember, group.ScopeIdDefinition.Values, new Action<IContextItem, ScopeValueDefinition>(this.Visit));
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		private void Visit(IContextItem owner, GroupKey groupKey)
		{
			this.VisitExpression(owner, groupKey.Value);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001F6CF File Offset: 0x0001D8CF
		private void Visit(IContextItem owner, SortKey sortKey)
		{
			this.VisitExpression(owner, sortKey.Value);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001F6DE File Offset: 0x0001D8DE
		private void Visit(IContextItem owner, ScopeValueDefinition valueDefn)
		{
			this.VisitExpression(owner, valueDefn.Value);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		private void Visit<T>(IContextItem owner, IEnumerable<T> collection, Action<IContextItem, T> visitAction)
		{
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				visitAction(owner, t);
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001F740 File Offset: 0x0001D940
		private void VisitExpression(IContextItem owner, Expression expression)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(expression);
			DataTransformTableColumn dataTransformTableColumn;
			ExpressionNode expressionNode = this.m_expressionTransform.TraverseToSource(node, out dataTransformTableColumn);
			if (node != expressionNode)
			{
				ExpressionReference expressionReference = new ExpressionReference(owner, expression.ExpressionId.Value);
				this.m_expressionsToRestore.Add(new ExpressionRestorationInfo(expressionReference, node));
				DataTransformColumnInliningInfo dataTransformColumnInliningInfo;
				if (!this.m_sourceColumns.TryGetValue(dataTransformTableColumn, out dataTransformColumnInliningInfo))
				{
					dataTransformColumnInliningInfo = new DataTransformColumnInliningInfo();
					this.m_sourceColumns.Add(dataTransformTableColumn, dataTransformColumnInliningInfo);
				}
				dataTransformColumnInliningInfo.AddReferrer(expressionReference);
				this.m_outputExpressionTable.SetNode(expression, expressionNode);
			}
		}

		// Token: 0x04000404 RID: 1028
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000405 RID: 1029
		private readonly DataTransformInlinerExpressionTransform m_expressionTransform;

		// Token: 0x04000406 RID: 1030
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000407 RID: 1031
		private readonly List<ExpressionRestorationInfo> m_expressionsToRestore;

		// Token: 0x04000408 RID: 1032
		private readonly Dictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> m_sourceColumns;
	}
}
