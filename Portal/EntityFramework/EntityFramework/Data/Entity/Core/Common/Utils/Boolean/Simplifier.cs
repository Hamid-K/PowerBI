using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061D RID: 1565
	internal class Simplifier<T_Identifier> : BasicVisitor<T_Identifier>
	{
		// Token: 0x06004BB7 RID: 19383 RVA: 0x0010AF76 File Offset: 0x00109176
		protected Simplifier()
		{
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0010AF80 File Offset: 0x00109180
		internal override BoolExpr<T_Identifier> VisitNot(NotExpr<T_Identifier> expression)
		{
			BoolExpr<T_Identifier> boolExpr = expression.Child.Accept<BoolExpr<T_Identifier>>(this);
			switch (boolExpr.ExprType)
			{
			case ExprType.Not:
				return ((NotExpr<T_Identifier>)boolExpr).Child;
			case ExprType.True:
				return FalseExpr<T_Identifier>.Value;
			case ExprType.False:
				return TrueExpr<T_Identifier>.Value;
			}
			return base.VisitNot(expression);
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0010AFDE File Offset: 0x001091DE
		internal override BoolExpr<T_Identifier> VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0010AFE7 File Offset: 0x001091E7
		internal override BoolExpr<T_Identifier> VisitOr(OrExpr<T_Identifier> expression)
		{
			return this.SimplifyTree(expression);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0010AFF0 File Offset: 0x001091F0
		private BoolExpr<T_Identifier> SimplifyTree(TreeExpr<T_Identifier> tree)
		{
			bool flag = tree.ExprType == ExprType.And;
			List<BoolExpr<T_Identifier>> list = new List<BoolExpr<T_Identifier>>(tree.Children.Count);
			foreach (BoolExpr<T_Identifier> boolExpr in tree.Children)
			{
				BoolExpr<T_Identifier> boolExpr2 = boolExpr.Accept<BoolExpr<T_Identifier>>(this);
				if (boolExpr2.ExprType == tree.ExprType)
				{
					list.AddRange(((TreeExpr<T_Identifier>)boolExpr2).Children);
				}
				else
				{
					list.Add(boolExpr2);
				}
			}
			Dictionary<BoolExpr<T_Identifier>, bool> dictionary = new Dictionary<BoolExpr<T_Identifier>, bool>(tree.Children.Count);
			List<BoolExpr<T_Identifier>> list2 = new List<BoolExpr<T_Identifier>>(tree.Children.Count);
			foreach (BoolExpr<T_Identifier> boolExpr3 in list)
			{
				switch (boolExpr3.ExprType)
				{
				case ExprType.Not:
					dictionary[((NotExpr<T_Identifier>)boolExpr3).Child] = true;
					continue;
				case ExprType.True:
					if (!flag)
					{
						return TrueExpr<T_Identifier>.Value;
					}
					continue;
				case ExprType.False:
					if (flag)
					{
						return FalseExpr<T_Identifier>.Value;
					}
					continue;
				}
				list2.Add(boolExpr3);
			}
			List<BoolExpr<T_Identifier>> list3 = new List<BoolExpr<T_Identifier>>();
			foreach (BoolExpr<T_Identifier> boolExpr4 in list2)
			{
				if (dictionary.ContainsKey(boolExpr4))
				{
					if (flag)
					{
						return FalseExpr<T_Identifier>.Value;
					}
					return TrueExpr<T_Identifier>.Value;
				}
				else
				{
					list3.Add(boolExpr4);
				}
			}
			foreach (BoolExpr<T_Identifier> boolExpr5 in dictionary.Keys)
			{
				list3.Add(boolExpr5.MakeNegated());
			}
			if (list3.Count == 0)
			{
				if (flag)
				{
					return TrueExpr<T_Identifier>.Value;
				}
				return FalseExpr<T_Identifier>.Value;
			}
			else
			{
				if (1 == list3.Count)
				{
					return list3[0];
				}
				TreeExpr<T_Identifier> treeExpr;
				if (flag)
				{
					treeExpr = new AndExpr<T_Identifier>(list3);
				}
				else
				{
					treeExpr = new OrExpr<T_Identifier>(list3);
				}
				return treeExpr;
			}
			BoolExpr<T_Identifier> boolExpr6;
			return boolExpr6;
		}

		// Token: 0x04001A77 RID: 6775
		internal static readonly Simplifier<T_Identifier> Instance = new Simplifier<T_Identifier>();
	}
}
