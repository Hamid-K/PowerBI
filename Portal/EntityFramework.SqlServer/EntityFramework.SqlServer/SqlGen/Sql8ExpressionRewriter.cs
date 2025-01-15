using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Utilities;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000034 RID: 52
	internal class Sql8ExpressionRewriter : DbExpressionRebinder
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00011F00 File Offset: 0x00010100
		internal static DbQueryCommandTree Rewrite(DbQueryCommandTree originalTree)
		{
			DbExpression dbExpression = new Sql8ExpressionRewriter(originalTree.MetadataWorkspace).VisitExpression(originalTree.Query);
			return new DbQueryCommandTree(originalTree.MetadataWorkspace, originalTree.DataSpace, dbExpression, false);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00011F37 File Offset: 0x00010137
		private Sql8ExpressionRewriter(MetadataWorkspace metadata)
			: base(metadata)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00011F40 File Offset: 0x00010140
		public override DbExpression Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Except);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00011F6E File Offset: 0x0001016E
		public override DbExpression Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.TransformIntersectOrExcept(this.VisitExpression(e.Left), this.VisitExpression(e.Right), DbExpressionKind.Intersect);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00011F9C File Offset: 0x0001019C
		public override DbExpression Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			DbExpression dbExpression = this.VisitExpressionBinding(e.Input).Sort(this.VisitSortOrder(e.SortOrder)).Limit(this.VisitExpression(e.Count));
			DbExpression dbExpression2 = this.VisitExpression(e.Input.Expression);
			IList<DbSortClause> list = this.VisitSortOrder(e.SortOrder);
			IList<DbPropertyExpression> list2 = new List<DbPropertyExpression>(e.SortOrder.Count);
			foreach (DbSortClause dbSortClause in list)
			{
				if (dbSortClause.Expression.ExpressionKind == DbExpressionKind.Property)
				{
					list2.Add((DbPropertyExpression)dbSortClause.Expression);
				}
			}
			return this.TransformIntersectOrExcept(dbExpression2, dbExpression, DbExpressionKind.Skip, list2, e.Input.VariableName).BindAs(e.Input.VariableName).Sort(list);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001209C File Offset: 0x0001029C
		private DbExpression TransformIntersectOrExcept(DbExpression left, DbExpression right, DbExpressionKind expressionKind)
		{
			return this.TransformIntersectOrExcept(left, right, expressionKind, null, null);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000120AC File Offset: 0x000102AC
		private DbExpression TransformIntersectOrExcept(DbExpression left, DbExpression right, DbExpressionKind expressionKind, IList<DbPropertyExpression> sortExpressionsOverLeft, string sortExpressionsBindingVariableName)
		{
			bool flag = expressionKind == DbExpressionKind.Except || expressionKind == DbExpressionKind.Skip;
			bool flag2 = expressionKind == DbExpressionKind.Except || expressionKind == DbExpressionKind.Intersect;
			DbExpressionBinding dbExpressionBinding = left.Bind();
			DbExpressionBinding dbExpressionBinding2 = right.Bind();
			IList<DbPropertyExpression> list = new List<DbPropertyExpression>();
			IList<DbPropertyExpression> list2 = new List<DbPropertyExpression>();
			this.FlattenProperties(dbExpressionBinding.Variable, list);
			this.FlattenProperties(dbExpressionBinding2.Variable, list2);
			if (expressionKind == DbExpressionKind.Skip && Sql8ExpressionRewriter.RemoveNonSortProperties(list, list2, sortExpressionsOverLeft, dbExpressionBinding.VariableName, sortExpressionsBindingVariableName))
			{
				dbExpressionBinding2 = Sql8ExpressionRewriter.CapWithProject(dbExpressionBinding2, list2);
			}
			DbExpression dbExpression = null;
			for (int i = 0; i < list.Count; i++)
			{
				DbExpression dbExpression2 = list[i].Equal(list2[i]);
				DbExpression dbExpression3 = list[i].IsNull();
				DbExpression dbExpression4 = list2[i].IsNull();
				DbExpression dbExpression5 = dbExpression3.And(dbExpression4);
				DbExpression dbExpression6 = dbExpression2.Or(dbExpression5);
				if (i == 0)
				{
					dbExpression = dbExpression6;
				}
				else
				{
					dbExpression = dbExpression.And(dbExpression6);
				}
			}
			DbExpression dbExpression7 = dbExpressionBinding2.Any(dbExpression);
			DbExpression dbExpression8;
			if (flag)
			{
				dbExpression8 = dbExpression7.Not();
			}
			else
			{
				dbExpression8 = dbExpression7;
			}
			DbExpression dbExpression9 = dbExpressionBinding.Filter(dbExpression8);
			if (flag2)
			{
				dbExpression9 = dbExpression9.Distinct();
			}
			return dbExpression9;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000121DC File Offset: 0x000103DC
		private void FlattenProperties(DbExpression input, IList<DbPropertyExpression> flattenedProperties)
		{
			foreach (EdmProperty edmProperty in input.ResultType.GetProperties())
			{
				DbPropertyExpression dbPropertyExpression = input.Property(edmProperty);
				if (BuiltInTypeKind.PrimitiveType == edmProperty.TypeUsage.EdmType.BuiltInTypeKind)
				{
					flattenedProperties.Add(dbPropertyExpression);
				}
				else
				{
					this.FlattenProperties(dbPropertyExpression, flattenedProperties);
				}
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00012254 File Offset: 0x00010454
		private static bool RemoveNonSortProperties(IList<DbPropertyExpression> list1, IList<DbPropertyExpression> list2, IList<DbPropertyExpression> sortList, string list1BindingVariableName, string sortExpressionsBindingVariableName)
		{
			bool flag = false;
			for (int i = list1.Count - 1; i >= 0; i--)
			{
				if (!Sql8ExpressionRewriter.HasMatchInList(list1[i], sortList, list1BindingVariableName, sortExpressionsBindingVariableName))
				{
					list1.RemoveAt(i);
					list2.RemoveAt(i);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001229C File Offset: 0x0001049C
		private static bool HasMatchInList(DbPropertyExpression expr, IList<DbPropertyExpression> list, string exprBindingVariableName, string listExpressionsBindingVariableName)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (Sql8ExpressionRewriter.AreMatching(expr, list[i], exprBindingVariableName, listExpressionsBindingVariableName))
				{
					list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000122D8 File Offset: 0x000104D8
		private static bool AreMatching(DbPropertyExpression expr1, DbPropertyExpression expr2, string expr1BindingVariableName, string expr2BindingVariableName)
		{
			if (expr1.Property.Name != expr2.Property.Name)
			{
				return false;
			}
			if (expr1.Instance.ExpressionKind != expr2.Instance.ExpressionKind)
			{
				return false;
			}
			if (expr1.Instance.ExpressionKind == DbExpressionKind.Property)
			{
				return Sql8ExpressionRewriter.AreMatching((DbPropertyExpression)expr1.Instance, (DbPropertyExpression)expr2.Instance, expr1BindingVariableName, expr2BindingVariableName);
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)expr1.Instance;
			DbVariableReferenceExpression dbVariableReferenceExpression2 = (DbVariableReferenceExpression)expr2.Instance;
			return string.Equals(dbVariableReferenceExpression.VariableName, expr1BindingVariableName, StringComparison.Ordinal) && string.Equals(dbVariableReferenceExpression2.VariableName, expr2BindingVariableName, StringComparison.Ordinal);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00012380 File Offset: 0x00010580
		private static DbExpressionBinding CapWithProject(DbExpressionBinding inputBinding, IList<DbPropertyExpression> flattenedProperties)
		{
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(flattenedProperties.Count);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(flattenedProperties.Count);
			foreach (DbPropertyExpression dbPropertyExpression in flattenedProperties)
			{
				string text = dbPropertyExpression.Property.Name;
				int num;
				if (dictionary.TryGetValue(text, out num))
				{
					string text2;
					do
					{
						num++;
						text2 = text + num.ToString(CultureInfo.InvariantCulture);
					}
					while (dictionary.ContainsKey(text2));
					dictionary[text] = num;
					text = text2;
				}
				dictionary[text] = 0;
				list.Add(new KeyValuePair<string, DbExpression>(text, dbPropertyExpression));
			}
			DbExpression dbExpression = DbExpressionBuilder.NewRow(list);
			DbExpressionBinding dbExpressionBinding = inputBinding.Project(dbExpression).Bind();
			flattenedProperties.Clear();
			RowType rowType = (RowType)dbExpression.ResultType.EdmType;
			foreach (KeyValuePair<string, DbExpression> keyValuePair in list)
			{
				EdmProperty edmProperty = rowType.Properties[keyValuePair.Key];
				flattenedProperties.Add(dbExpressionBinding.Variable.Property(edmProperty));
			}
			return dbExpressionBinding;
		}
	}
}
