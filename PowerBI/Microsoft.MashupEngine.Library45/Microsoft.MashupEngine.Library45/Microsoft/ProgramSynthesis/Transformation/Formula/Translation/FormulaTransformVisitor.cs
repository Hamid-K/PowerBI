using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001801 RID: 6145
	internal class FormulaTransformVisitor : IFormulaVisitor<FormulaExpression>
	{
		// Token: 0x0600CA13 RID: 51731 RVA: 0x002B3644 File Offset: 0x002B1844
		private FormulaTransformVisitor(Func<FormulaExpression, FormulaTransformNodeInfo, FormulaExpression> transform)
		{
			this._transform = transform;
		}

		// Token: 0x0600CA14 RID: 51732 RVA: 0x002B365E File Offset: 0x002B185E
		public static FormulaExpression Transform(FormulaExpression expression, Func<FormulaExpression, FormulaTransformNodeInfo, FormulaExpression> transform)
		{
			return new FormulaTransformVisitor(transform).Visit(expression);
		}

		// Token: 0x0600CA15 RID: 51733 RVA: 0x002B366C File Offset: 0x002B186C
		public static FormulaExpression Transform(FormulaExpression expression, Func<FormulaExpression, FormulaExpression> transform)
		{
			return new FormulaTransformVisitor((FormulaExpression node, FormulaTransformNodeInfo _) => transform(node)).Visit(expression);
		}

		// Token: 0x0600CA16 RID: 51734 RVA: 0x002B3690 File Offset: 0x002B1890
		public FormulaExpression Visit(FormulaExpression node)
		{
			if (node == null)
			{
				return null;
			}
			FormulaTransformNodeInfo formulaTransformNodeInfo = new FormulaTransformNodeInfo();
			formulaTransformNodeInfo.Ancestors = this._ancestorInfo.ToList<FormulaTransformNodeInfo>();
			int num = this._depth;
			this._depth = num + 1;
			formulaTransformNodeInfo.Depth = num;
			num = this._order;
			this._order = num + 1;
			formulaTransformNodeInfo.Order = num;
			formulaTransformNodeInfo.Node = node;
			FormulaTransformNodeInfo formulaTransformNodeInfo2 = formulaTransformNodeInfo;
			this._ancestorInfo.Add(formulaTransformNodeInfo2);
			FormulaExpression formulaExpression = this._transform(node.AcceptClone(this), formulaTransformNodeInfo2);
			this._ancestorInfo.RemoveAt(this._ancestorInfo.Count - 1);
			this._depth--;
			return formulaExpression;
		}

		// Token: 0x04004F57 RID: 20311
		private readonly List<FormulaTransformNodeInfo> _ancestorInfo = new List<FormulaTransformNodeInfo>();

		// Token: 0x04004F58 RID: 20312
		private int _depth;

		// Token: 0x04004F59 RID: 20313
		private int _order;

		// Token: 0x04004F5A RID: 20314
		private readonly Func<FormulaExpression, FormulaTransformNodeInfo, FormulaExpression> _transform;
	}
}
