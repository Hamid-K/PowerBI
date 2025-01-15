using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017FA RID: 6138
	internal class FormulaExtractVisitor : IFormulaVisitor<IEnumerable<FormulaExpressionDetail>>
	{
		// Token: 0x0600C9E5 RID: 51685 RVA: 0x002B31CD File Offset: 0x002B13CD
		private FormulaExtractVisitor(Func<FormulaExpression, FormulaExpressionDetail, bool> predicate)
		{
			this._predicate = predicate;
		}

		// Token: 0x0600C9E6 RID: 51686 RVA: 0x002B31F2 File Offset: 0x002B13F2
		public static IEnumerable<FormulaExpressionDetail> Extract(FormulaExpression expression)
		{
			return new FormulaExtractVisitor((FormulaExpression _, FormulaExpressionDetail _) => true).Visit(expression);
		}

		// Token: 0x0600C9E7 RID: 51687 RVA: 0x002B321E File Offset: 0x002B141E
		public static IEnumerable<FormulaExpressionDetail> Extract(IEnumerable<FormulaExpression> expressions)
		{
			return FormulaExtractVisitor.Extract(expressions, (FormulaExpression _) => true);
		}

		// Token: 0x0600C9E8 RID: 51688 RVA: 0x002B3245 File Offset: 0x002B1445
		public static IEnumerable<FormulaExpressionDetail> Extract(IEnumerable<FormulaExpression> expressions, Func<FormulaExpression, bool> predicate)
		{
			return new FormulaExtractVisitor((FormulaExpression node, FormulaExpressionDetail _) => predicate(node)).Visit(expressions);
		}

		// Token: 0x0600C9E9 RID: 51689 RVA: 0x002B3269 File Offset: 0x002B1469
		public static IEnumerable<FormulaExpressionDetail> Extract(IEnumerable<FormulaExpression> expressions, Func<FormulaExpression, FormulaExpressionDetail, bool> predicate)
		{
			return new FormulaExtractVisitor(predicate).Visit(expressions);
		}

		// Token: 0x0600C9EA RID: 51690 RVA: 0x002B3277 File Offset: 0x002B1477
		public static IEnumerable<FormulaExpressionDetail> Extract(FormulaExpression expression, Func<FormulaExpression, bool> predicate)
		{
			return new FormulaExtractVisitor((FormulaExpression node, FormulaExpressionDetail _) => predicate(node)).Visit(expression);
		}

		// Token: 0x0600C9EB RID: 51691 RVA: 0x002B329B File Offset: 0x002B149B
		public static IEnumerable<FormulaExpressionDetail> Extract(FormulaExpression expression, Func<FormulaExpression, FormulaExpressionDetail, bool> predicate)
		{
			return new FormulaExtractVisitor(predicate).Visit(expression);
		}

		// Token: 0x0600C9EC RID: 51692 RVA: 0x002B32A9 File Offset: 0x002B14A9
		public static int ExtractMaxDepth(FormulaExpression expression)
		{
			return FormulaExtractVisitor.ExtractMaxDepth(expression, (FormulaExpression _) => true);
		}

		// Token: 0x0600C9ED RID: 51693 RVA: 0x002B32D0 File Offset: 0x002B14D0
		public static int ExtractMaxDepth(FormulaExpression expression, Func<FormulaExpression, bool> predicate)
		{
			return FormulaExtractVisitor.Extract(expression, predicate).Max((FormulaExpressionDetail i) => i.Depth);
		}

		// Token: 0x0600C9EE RID: 51694 RVA: 0x002B32FD File Offset: 0x002B14FD
		public static IEnumerable<FormulaExpression> ExtractNodes(IEnumerable<FormulaExpression> expressions)
		{
			return FormulaExtractVisitor.ExtractNodes(expressions, (FormulaExpression _) => true);
		}

		// Token: 0x0600C9EF RID: 51695 RVA: 0x002B3324 File Offset: 0x002B1524
		public static IEnumerable<FormulaExpression> ExtractNodes(IEnumerable<FormulaExpression> expressions, Func<FormulaExpression, bool> predicate)
		{
			return from e in FormulaExtractVisitor.Extract(expressions, predicate)
				select e.Node;
		}

		// Token: 0x0600C9F0 RID: 51696 RVA: 0x002B3351 File Offset: 0x002B1551
		public static IEnumerable<FormulaExpression> ExtractNodes(FormulaExpression expression)
		{
			return from e in FormulaExtractVisitor.Extract(expression)
				select e.Node;
		}

		// Token: 0x0600C9F1 RID: 51697 RVA: 0x002B337D File Offset: 0x002B157D
		public static IEnumerable<FormulaExpression> ExtractNodes(FormulaExpression node, Func<FormulaExpression, bool> predicate)
		{
			return from e in FormulaExtractVisitor.Extract(node, predicate)
				select e.Node;
		}

		// Token: 0x0600C9F2 RID: 51698 RVA: 0x002B33AC File Offset: 0x002B15AC
		public IEnumerable<FormulaExpressionDetail> Visit(FormulaExpression node)
		{
			if (node == null)
			{
				return this._results;
			}
			FormulaExpressionDetail formulaExpressionDetail = new FormulaExpressionDetail();
			formulaExpressionDetail.Ancestors = this._ancestors.ToList<FormulaExpressionDetail>();
			formulaExpressionDetail.Depth = this._depth;
			int order = this._order;
			this._order = order + 1;
			formulaExpressionDetail.Order = order;
			formulaExpressionDetail.Node = node;
			FormulaExpressionDetail formulaExpressionDetail2 = formulaExpressionDetail;
			if (this._predicate(node, formulaExpressionDetail2))
			{
				this._results.Add(formulaExpressionDetail2);
			}
			IReadOnlyList<FormulaExpression> children = node.Children;
			bool flag = !(((children != null) ? new bool?(children.Any<FormulaExpression>()) : null) ?? false);
			if (flag)
			{
				return this._results;
			}
			this._depth++;
			this._ancestors.Add(formulaExpressionDetail2);
			node.AcceptChildren(this);
			this._ancestors.RemoveAt(this._ancestors.Count - 1);
			this._depth--;
			return this._results;
		}

		// Token: 0x0600C9F3 RID: 51699 RVA: 0x002B34B8 File Offset: 0x002B16B8
		public IEnumerable<FormulaExpressionDetail> Visit(IEnumerable<FormulaExpression> nodes)
		{
			foreach (FormulaExpression formulaExpression in nodes)
			{
				this.Visit(formulaExpression);
			}
			return this._results;
		}

		// Token: 0x04004F41 RID: 20289
		private readonly List<FormulaExpressionDetail> _ancestors = new List<FormulaExpressionDetail>();

		// Token: 0x04004F42 RID: 20290
		private int _depth;

		// Token: 0x04004F43 RID: 20291
		private int _order;

		// Token: 0x04004F44 RID: 20292
		private readonly Func<FormulaExpression, FormulaExpressionDetail, bool> _predicate;

		// Token: 0x04004F45 RID: 20293
		private readonly IList<FormulaExpressionDetail> _results = new List<FormulaExpressionDetail>();
	}
}
