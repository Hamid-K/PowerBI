using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A2 RID: 1698
	public abstract class BasicExpressionVisitor : DbExpressionVisitor
	{
		// Token: 0x06004F9F RID: 20383 RVA: 0x00120BF5 File Offset: 0x0011EDF5
		protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
		{
			Check.NotNull<DbUnaryExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00120C0F File Offset: 0x0011EE0F
		protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
		{
			Check.NotNull<DbBinaryExpression>(expression, "expression");
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00120C35 File Offset: 0x0011EE35
		protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
		{
			Check.NotNull<DbExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00120C4F File Offset: 0x0011EE4F
		protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
		{
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00120C51 File Offset: 0x0011EE51
		protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
		{
			Check.NotNull<DbGroupExpressionBinding>(binding, "binding");
			this.VisitExpression(binding.Expression);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00120C6B File Offset: 0x0011EE6B
		protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00120C6D File Offset: 0x0011EE6D
		protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
		{
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x00120C6F File Offset: 0x0011EE6F
		protected virtual void VisitLambdaPre(DbLambda lambda)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x00120C7D File Offset: 0x0011EE7D
		protected virtual void VisitLambdaPost(DbLambda lambda)
		{
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00120C7F File Offset: 0x0011EE7F
		public virtual void VisitExpression(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			expression.Accept(this);
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00120C94 File Offset: 0x0011EE94
		public virtual void VisitExpressionList(IList<DbExpression> expressionList)
		{
			Check.NotNull<IList<DbExpression>>(expressionList, "expressionList");
			for (int i = 0; i < expressionList.Count; i++)
			{
				this.VisitExpression(expressionList[i]);
			}
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x00120CCC File Offset: 0x0011EECC
		public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
		{
			Check.NotNull<IList<DbAggregate>>(aggregates, "aggregates");
			for (int i = 0; i < aggregates.Count; i++)
			{
				this.VisitAggregate(aggregates[i]);
			}
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x00120D03 File Offset: 0x0011EF03
		public virtual void VisitAggregate(DbAggregate aggregate)
		{
			Check.NotNull<DbAggregate>(aggregate, "aggregate");
			this.VisitExpressionList(aggregate.Arguments);
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x00120D20 File Offset: 0x0011EF20
		internal virtual void VisitRelatedEntityReferenceList(IList<DbRelatedEntityRef> relatedEntityReferences)
		{
			for (int i = 0; i < relatedEntityReferences.Count; i++)
			{
				this.VisitRelatedEntityReference(relatedEntityReferences[i]);
			}
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00120D4B File Offset: 0x0011EF4B
		internal virtual void VisitRelatedEntityReference(DbRelatedEntityRef relatedEntityRef)
		{
			this.VisitExpression(relatedEntityRef.TargetEntityReference);
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x00120D59 File Offset: 0x0011EF59
		public override void Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x00120D7C File Offset: 0x0011EF7C
		public override void Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x00120D8A File Offset: 0x0011EF8A
		public override void Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00120D98 File Offset: 0x0011EF98
		public override void Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00120DA6 File Offset: 0x0011EFA6
		public override void Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00120DB4 File Offset: 0x0011EFB4
		public override void Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00120DD0 File Offset: 0x0011EFD0
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			this.VisitLambdaPre(expression.Lambda);
			this.VisitExpression(expression.Lambda.Body);
			this.VisitLambdaPost(expression.Lambda);
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00120E1E File Offset: 0x0011F01E
		public override void Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			if (expression.Instance != null)
			{
				this.VisitExpression(expression.Instance);
			}
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00120E40 File Offset: 0x0011F040
		public override void Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00120E55 File Offset: 0x0011F055
		public override void Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Pattern);
			this.VisitExpression(expression.Escape);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x00120E87 File Offset: 0x0011F087
		public override void Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			this.VisitExpression(expression.Argument);
			this.VisitExpression(expression.Limit);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x00120EAD File Offset: 0x0011F0AD
		public override void Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00120EC2 File Offset: 0x0011F0C2
		public override void Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00120EDC File Offset: 0x0011F0DC
		public override void Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x00120EF1 File Offset: 0x0011F0F1
		public override void Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x00120F06 File Offset: 0x0011F106
		public override void Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			this.VisitExpression(expression.Item);
			this.VisitExpressionList(expression.List);
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x00120F2C File Offset: 0x0011F12C
		public override void Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00120F41 File Offset: 0x0011F141
		public override void Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x00120F56 File Offset: 0x0011F156
		public override void Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00120F6B File Offset: 0x0011F16B
		public override void Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00120F80 File Offset: 0x0011F180
		public override void Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00120F95 File Offset: 0x0011F195
		public override void Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x00120FAA File Offset: 0x0011F1AA
		public override void Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00120FBF File Offset: 0x0011F1BF
		public override void Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x00120FD4 File Offset: 0x0011F1D4
		public override void Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x00120FE9 File Offset: 0x0011F1E9
		public override void Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00120FFE File Offset: 0x0011F1FE
		public override void Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00121013 File Offset: 0x0011F213
		public override void Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			this.VisitExpressionList(expression.When);
			this.VisitExpressionList(expression.Then);
			this.VisitExpression(expression.Else);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x00121045 File Offset: 0x0011F245
		public override void Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			this.VisitExpressionList(expression.Arguments);
			if (expression.HasRelatedEntityReferences)
			{
				this.VisitRelatedEntityReferenceList(expression.RelatedEntityReferences);
			}
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x00121073 File Offset: 0x0011F273
		public override void Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x00121088 File Offset: 0x0011F288
		public override void Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			this.VisitExpression(expression.NavigationSource);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x001210A2 File Offset: 0x0011F2A2
		public override void Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x001210B7 File Offset: 0x0011F2B7
		public override void Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x001210CC File Offset: 0x0011F2CC
		public override void Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			this.VisitUnaryExpression(expression);
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x001210E1 File Offset: 0x0011F2E1
		public override void Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x001210EF File Offset: 0x0011F2EF
		public override void Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x00121121 File Offset: 0x0011F321
		public override void Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Projection);
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x00121154 File Offset: 0x0011F354
		public override void Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			foreach (DbExpressionBinding dbExpressionBinding in expression.Inputs)
			{
				this.VisitExpressionBindingPre(dbExpressionBinding);
			}
			foreach (DbExpressionBinding dbExpressionBinding2 in expression.Inputs)
			{
				this.VisitExpressionBindingPost(dbExpressionBinding2);
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x001211EC File Offset: 0x0011F3EC
		public override void Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Left);
			this.VisitExpressionBindingPre(expression.Right);
			this.VisitExpression(expression.JoinCondition);
			this.VisitExpressionBindingPost(expression.Left);
			this.VisitExpressionBindingPost(expression.Right);
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x00121241 File Offset: 0x0011F441
		public override void Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			if (expression.Apply != null)
			{
				this.VisitExpression(expression.Apply.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x00121280 File Offset: 0x0011F480
		public override void Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			this.VisitGroupExpressionBindingPre(expression.Input);
			this.VisitExpressionList(expression.Keys);
			this.VisitGroupExpressionBindingMid(expression.Input);
			this.VisitAggregateList(expression.Aggregates);
			this.VisitGroupExpressionBindingPost(expression.Input);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x001212D8 File Offset: 0x0011F4D8
		public override void Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			foreach (DbSortClause dbSortClause in expression.SortOrder)
			{
				this.VisitExpression(dbSortClause.Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
			this.VisitExpression(expression.Count);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0012135C File Offset: 0x0011F55C
		public override void Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			for (int i = 0; i < expression.SortOrder.Count; i++)
			{
				this.VisitExpression(expression.SortOrder[i].Expression);
			}
			this.VisitExpressionBindingPost(expression.Input);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x001213BA File Offset: 0x0011F5BA
		public override void Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			this.VisitExpressionBindingPre(expression.Input);
			this.VisitExpression(expression.Predicate);
			this.VisitExpressionBindingPost(expression.Input);
		}
	}
}
