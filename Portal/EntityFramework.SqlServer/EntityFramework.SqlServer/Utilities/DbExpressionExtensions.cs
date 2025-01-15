using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200001B RID: 27
	internal static class DbExpressionExtensions
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000EA5E File Offset: 0x0000CC5E
		public static IEnumerable<DbExpression> GetLeafNodes(this DbExpression root, DbExpressionKind kind, Func<DbExpression, IEnumerable<DbExpression>> getChildNodes)
		{
			Stack<DbExpression> nodes = new Stack<DbExpression>();
			nodes.Push(root);
			while (nodes.Count > 0)
			{
				DbExpression dbExpression = nodes.Pop();
				if (dbExpression.ExpressionKind != kind)
				{
					yield return dbExpression;
				}
				else
				{
					foreach (DbExpression dbExpression2 in getChildNodes(dbExpression).Reverse<DbExpression>())
					{
						nodes.Push(dbExpression2);
					}
				}
			}
			yield break;
		}
	}
}
