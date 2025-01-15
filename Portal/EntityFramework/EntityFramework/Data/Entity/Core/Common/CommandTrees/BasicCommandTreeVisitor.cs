using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A1 RID: 1697
	public abstract class BasicCommandTreeVisitor : BasicExpressionVisitor
	{
		// Token: 0x06004F95 RID: 20373 RVA: 0x001209E7 File Offset: 0x0011EBE7
		protected virtual void VisitSetClause(DbSetClause setClause)
		{
			Check.NotNull<DbSetClause>(setClause, "setClause");
			this.VisitExpression(setClause.Property);
			this.VisitExpression(setClause.Value);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00120A0D File Offset: 0x0011EC0D
		protected virtual void VisitModificationClause(DbModificationClause modificationClause)
		{
			Check.NotNull<DbModificationClause>(modificationClause, "modificationClause");
			this.VisitSetClause((DbSetClause)modificationClause);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00120A28 File Offset: 0x0011EC28
		protected virtual void VisitModificationClauses(IList<DbModificationClause> modificationClauses)
		{
			Check.NotNull<IList<DbModificationClause>>(modificationClauses, "modificationClauses");
			for (int i = 0; i < modificationClauses.Count; i++)
			{
				this.VisitModificationClause(modificationClauses[i]);
			}
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00120A60 File Offset: 0x0011EC60
		public virtual void VisitCommandTree(DbCommandTree commandTree)
		{
			Check.NotNull<DbCommandTree>(commandTree, "commandTree");
			switch (commandTree.CommandTreeKind)
			{
			case DbCommandTreeKind.Query:
				this.VisitQueryCommandTree((DbQueryCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Update:
				this.VisitUpdateCommandTree((DbUpdateCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Insert:
				this.VisitInsertCommandTree((DbInsertCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Delete:
				this.VisitDeleteCommandTree((DbDeleteCommandTree)commandTree);
				return;
			case DbCommandTreeKind.Function:
				this.VisitFunctionCommandTree((DbFunctionCommandTree)commandTree);
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00120AE2 File Offset: 0x0011ECE2
		protected virtual void VisitDeleteCommandTree(DbDeleteCommandTree deleteTree)
		{
			Check.NotNull<DbDeleteCommandTree>(deleteTree, "deleteTree");
			this.VisitExpressionBindingPre(deleteTree.Target);
			this.VisitExpression(deleteTree.Predicate);
			this.VisitExpressionBindingPost(deleteTree.Target);
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00120B14 File Offset: 0x0011ED14
		protected virtual void VisitFunctionCommandTree(DbFunctionCommandTree functionTree)
		{
			Check.NotNull<DbFunctionCommandTree>(functionTree, "functionTree");
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00120B24 File Offset: 0x0011ED24
		protected virtual void VisitInsertCommandTree(DbInsertCommandTree insertTree)
		{
			Check.NotNull<DbInsertCommandTree>(insertTree, "insertTree");
			this.VisitExpressionBindingPre(insertTree.Target);
			this.VisitModificationClauses(insertTree.SetClauses);
			if (insertTree.Returning != null)
			{
				this.VisitExpression(insertTree.Returning);
			}
			this.VisitExpressionBindingPost(insertTree.Target);
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00120B75 File Offset: 0x0011ED75
		protected virtual void VisitQueryCommandTree(DbQueryCommandTree queryTree)
		{
			Check.NotNull<DbQueryCommandTree>(queryTree, "queryTree");
			this.VisitExpression(queryTree.Query);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00120B90 File Offset: 0x0011ED90
		protected virtual void VisitUpdateCommandTree(DbUpdateCommandTree updateTree)
		{
			Check.NotNull<DbUpdateCommandTree>(updateTree, "updateTree");
			this.VisitExpressionBindingPre(updateTree.Target);
			this.VisitModificationClauses(updateTree.SetClauses);
			this.VisitExpression(updateTree.Predicate);
			if (updateTree.Returning != null)
			{
				this.VisitExpression(updateTree.Returning);
			}
			this.VisitExpressionBindingPost(updateTree.Target);
		}
	}
}
