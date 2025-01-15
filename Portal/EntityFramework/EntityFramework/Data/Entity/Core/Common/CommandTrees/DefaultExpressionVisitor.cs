using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E7 RID: 1767
	public class DefaultExpressionVisitor : DbExpressionVisitor<DbExpression>
	{
		// Token: 0x060051A2 RID: 20898 RVA: 0x00123D6E File Offset: 0x00121F6E
		protected DefaultExpressionVisitor()
		{
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00123D81 File Offset: 0x00121F81
		protected virtual void OnExpressionReplaced(DbExpression oldExpression, DbExpression newExpression)
		{
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00123D83 File Offset: 0x00121F83
		protected virtual void OnVariableRebound(DbVariableReferenceExpression fromVarRef, DbVariableReferenceExpression toVarRef)
		{
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00123D85 File Offset: 0x00121F85
		protected virtual void OnEnterScope(IEnumerable<DbVariableReferenceExpression> scopeVariables)
		{
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x00123D87 File Offset: 0x00121F87
		protected virtual void OnExitScope()
		{
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00123D8C File Offset: 0x00121F8C
		protected virtual DbExpression VisitExpression(DbExpression expression)
		{
			DbExpression dbExpression = null;
			if (expression != null)
			{
				dbExpression = expression.Accept<DbExpression>(this);
			}
			return dbExpression;
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x00123DA7 File Offset: 0x00121FA7
		protected virtual IList<DbExpression> VisitExpressionList(IList<DbExpression> list)
		{
			return DefaultExpressionVisitor.VisitList<DbExpression>(list, new Func<DbExpression, DbExpression>(this.VisitExpression));
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x00123DBC File Offset: 0x00121FBC
		protected virtual DbExpressionBinding VisitExpressionBinding(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != dbExpression)
				{
					dbExpressionBinding = dbExpression.BindAs(binding.VariableName);
					this.RebindVariable(binding.Variable, dbExpressionBinding.Variable);
				}
			}
			return dbExpressionBinding;
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00123E04 File Offset: 0x00122004
		protected virtual IList<DbExpressionBinding> VisitExpressionBindingList(IList<DbExpressionBinding> list)
		{
			return DefaultExpressionVisitor.VisitList<DbExpressionBinding>(list, new Func<DbExpressionBinding, DbExpressionBinding>(this.VisitExpressionBinding));
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x00123E1C File Offset: 0x0012201C
		protected virtual DbGroupExpressionBinding VisitGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			DbGroupExpressionBinding dbGroupExpressionBinding = binding;
			if (binding != null)
			{
				DbExpression dbExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != dbExpression)
				{
					dbGroupExpressionBinding = dbExpression.GroupBindAs(binding.VariableName, binding.GroupVariableName);
					this.RebindVariable(binding.Variable, dbGroupExpressionBinding.Variable);
					this.RebindVariable(binding.GroupVariable, dbGroupExpressionBinding.GroupVariable);
				}
			}
			return dbGroupExpressionBinding;
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x00123E7C File Offset: 0x0012207C
		protected virtual DbSortClause VisitSortClause(DbSortClause clause)
		{
			DbSortClause dbSortClause = clause;
			if (clause != null)
			{
				DbExpression dbExpression = this.VisitExpression(clause.Expression);
				if (clause.Expression != dbExpression)
				{
					if (!string.IsNullOrEmpty(clause.Collation))
					{
						dbSortClause = (clause.Ascending ? dbExpression.ToSortClause(clause.Collation) : dbExpression.ToSortClauseDescending(clause.Collation));
					}
					else
					{
						dbSortClause = (clause.Ascending ? dbExpression.ToSortClause() : dbExpression.ToSortClauseDescending());
					}
				}
			}
			return dbSortClause;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x00123EEE File Offset: 0x001220EE
		protected virtual IList<DbSortClause> VisitSortOrder(IList<DbSortClause> sortOrder)
		{
			return DefaultExpressionVisitor.VisitList<DbSortClause>(sortOrder, new Func<DbSortClause, DbSortClause>(this.VisitSortClause));
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00123F04 File Offset: 0x00122104
		protected virtual DbAggregate VisitAggregate(DbAggregate aggregate)
		{
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate != null)
			{
				return this.VisitFunctionAggregate(dbFunctionAggregate);
			}
			DbGroupAggregate dbGroupAggregate = (DbGroupAggregate)aggregate;
			return this.VisitGroupAggregate(dbGroupAggregate);
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00123F34 File Offset: 0x00122134
		protected virtual DbFunctionAggregate VisitFunctionAggregate(DbFunctionAggregate aggregate)
		{
			DbFunctionAggregate dbFunctionAggregate = aggregate;
			if (aggregate != null)
			{
				EdmFunction edmFunction = this.VisitFunction(aggregate.Function);
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (aggregate.Function != edmFunction || aggregate.Arguments != list)
				{
					if (aggregate.Distinct)
					{
						dbFunctionAggregate = edmFunction.AggregateDistinct(list);
					}
					else
					{
						dbFunctionAggregate = edmFunction.Aggregate(list);
					}
				}
			}
			return dbFunctionAggregate;
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00123F90 File Offset: 0x00122190
		protected virtual DbGroupAggregate VisitGroupAggregate(DbGroupAggregate aggregate)
		{
			DbGroupAggregate dbGroupAggregate = aggregate;
			if (aggregate != null)
			{
				IList<DbExpression> list = this.VisitExpressionList(aggregate.Arguments);
				if (aggregate.Arguments != list)
				{
					dbGroupAggregate = DbExpressionBuilder.GroupAggregate(list[0]);
				}
			}
			return dbGroupAggregate;
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00123FC8 File Offset: 0x001221C8
		protected virtual DbLambda VisitLambda(DbLambda lambda)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			DbLambda dbLambda = lambda;
			IList<DbVariableReferenceExpression> list = DefaultExpressionVisitor.VisitList<DbVariableReferenceExpression>(lambda.Variables, delegate(DbVariableReferenceExpression varRef)
			{
				TypeUsage typeUsage = this.VisitTypeUsage(varRef.ResultType);
				if (varRef.ResultType != typeUsage)
				{
					return typeUsage.Variable(varRef.VariableName);
				}
				return varRef;
			});
			this.EnterScope(list.ToArray<DbVariableReferenceExpression>());
			DbExpression dbExpression = this.VisitExpression(lambda.Body);
			this.ExitScope();
			if (lambda.Variables != list || lambda.Body != dbExpression)
			{
				dbLambda = DbExpressionBuilder.Lambda(dbExpression, list);
			}
			return dbLambda;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x00124035 File Offset: 0x00122235
		protected virtual EdmType VisitType(EdmType type)
		{
			return type;
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x00124038 File Offset: 0x00122238
		protected virtual TypeUsage VisitTypeUsage(TypeUsage type)
		{
			return type;
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0012403B File Offset: 0x0012223B
		protected virtual EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			return entitySet;
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0012403E File Offset: 0x0012223E
		protected virtual EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			return functionMetadata;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00124041 File Offset: 0x00122241
		private void NotifyIfChanged(DbExpression originalExpression, DbExpression newExpression)
		{
			if (originalExpression != newExpression)
			{
				this.OnExpressionReplaced(originalExpression, newExpression);
			}
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00124050 File Offset: 0x00122250
		private static IList<TElement> VisitList<TElement>(IList<TElement> list, Func<TElement, TElement> map)
		{
			IList<TElement> list2 = list;
			if (list != null)
			{
				List<TElement> list3 = null;
				for (int i = 0; i < list.Count; i++)
				{
					TElement telement = map(list[i]);
					if (list3 == null && list[i] != telement)
					{
						list3 = new List<TElement>(list);
						list2 = list3;
					}
					if (list3 != null)
					{
						list3[i] = telement;
					}
				}
			}
			return list2;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x001240B0 File Offset: 0x001222B0
		private DbExpression VisitUnary(DbUnaryExpression expression, Func<DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			if (expression.Argument != dbExpression2)
			{
				dbExpression = callback(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x001240E8 File Offset: 0x001222E8
		private DbExpression VisitTypeUnary(DbUnaryExpression expression, TypeUsage type, Func<DbExpression, TypeUsage, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			TypeUsage typeUsage = this.VisitTypeUsage(type);
			if (expression.Argument != dbExpression2 || type != typeUsage)
			{
				dbExpression = callback(dbExpression2, typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0012412C File Offset: 0x0012232C
		private DbExpression VisitBinary(DbBinaryExpression expression, Func<DbExpression, DbExpression, DbExpression> callback)
		{
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Left);
			DbExpression dbExpression3 = this.VisitExpression(expression.Right);
			if (expression.Left != dbExpression2 || expression.Right != dbExpression3)
			{
				dbExpression = callback(dbExpression2, dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0012417C File Offset: 0x0012237C
		private DbRelatedEntityRef VisitRelatedEntityRef(DbRelatedEntityRef entityRef)
		{
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(entityRef.SourceEnd, entityRef.TargetEnd, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression = this.VisitExpression(entityRef.TargetEntityReference);
			if (entityRef.SourceEnd != relationshipEndMember || entityRef.TargetEnd != relationshipEndMember2 || entityRef.TargetEntityReference != dbExpression)
			{
				return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, relationshipEndMember2, dbExpression);
			}
			return entityRef;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x001241D4 File Offset: 0x001223D4
		private void VisitRelationshipEnds(RelationshipEndMember source, RelationshipEndMember target, out RelationshipEndMember newSource, out RelationshipEndMember newTarget)
		{
			RelationshipType relationshipType = (RelationshipType)this.VisitType(target.DeclaringType);
			newSource = relationshipType.RelationshipEndMembers[source.Name];
			newTarget = relationshipType.RelationshipEndMembers[target.Name];
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0012421C File Offset: 0x0012241C
		private DbExpression VisitTerminal(DbExpression expression, Func<TypeUsage, DbExpression> reconstructor)
		{
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			if (expression.ResultType != typeUsage)
			{
				dbExpression = reconstructor(typeUsage);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x00124254 File Offset: 0x00122454
		private void RebindVariable(DbVariableReferenceExpression from, DbVariableReferenceExpression to)
		{
			if (!from.VariableName.Equals(to.VariableName, StringComparison.Ordinal) || from.ResultType.EdmType != to.ResultType.EdmType || !from.ResultType.EdmEquals(to.ResultType))
			{
				this.varMappings[from] = to;
				this.OnVariableRebound(from, to);
			}
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x001242B8 File Offset: 0x001224B8
		private DbExpressionBinding VisitExpressionBindingEnterScope(DbExpressionBinding binding)
		{
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBinding(binding);
			this.OnEnterScope(new DbVariableReferenceExpression[] { dbExpressionBinding.Variable });
			return dbExpressionBinding;
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x001242E3 File Offset: 0x001224E3
		private void EnterScope(params DbVariableReferenceExpression[] scopeVars)
		{
			this.OnEnterScope(scopeVars);
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x001242EC File Offset: 0x001224EC
		private void ExitScope()
		{
			this.OnExitScope();
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x001242F4 File Offset: 0x001224F4
		public override DbExpression Visit(DbExpression expression)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00124318 File Offset: 0x00122518
		public override DbExpression Visit(DbConstantExpression expression)
		{
			Check.NotNull<DbConstantExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Constant(expression.GetValue()));
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0012435B File Offset: 0x0012255B
		public override DbExpression Visit(DbNullExpression expression)
		{
			Check.NotNull<DbNullExpression>(expression, "expression");
			return this.VisitTerminal(expression, new Func<TypeUsage, DbExpression>(DbExpressionBuilder.Null));
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0012437C File Offset: 0x0012257C
		public override DbExpression Visit(DbVariableReferenceExpression expression)
		{
			Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbVariableReferenceExpression dbVariableReferenceExpression;
			if (this.varMappings.TryGetValue(expression, out dbVariableReferenceExpression))
			{
				dbExpression = dbVariableReferenceExpression;
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x001243B4 File Offset: 0x001225B4
		public override DbExpression Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			return this.VisitTerminal(expression, (TypeUsage newType) => newType.Parameter(expression.ParameterName));
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x001243F8 File Offset: 0x001225F8
		public override DbExpression Visit(DbFunctionExpression expression)
		{
			Check.NotNull<DbFunctionExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			EdmFunction edmFunction = this.VisitFunction(expression.Function);
			if (expression.Arguments != list || expression.Function != edmFunction)
			{
				dbExpression = edmFunction.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00124450 File Offset: 0x00122650
		public override DbExpression Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			DbLambda dbLambda = this.VisitLambda(expression.Lambda);
			if (expression.Arguments != list || expression.Lambda != dbLambda)
			{
				dbExpression = dbLambda.Invoke(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x001244A8 File Offset: 0x001226A8
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Instance);
			if (expression.Instance != dbExpression2)
			{
				dbExpression = dbExpression2.Property(expression.Property.Name);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x001244F4 File Offset: 0x001226F4
		public override DbExpression Visit(DbComparisonExpression expression)
		{
			Check.NotNull<DbComparisonExpression>(expression, "expression");
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Equal));
				}
				if (expressionKind == DbExpressionKind.GreaterThan)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThan));
				}
				if (expressionKind == DbExpressionKind.GreaterThanOrEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.GreaterThanOrEqual));
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.LessThan)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThan));
				}
				if (expressionKind == DbExpressionKind.LessThanOrEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.LessThanOrEqual));
				}
				if (expressionKind == DbExpressionKind.NotEquals)
				{
					return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.NotEqual));
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x001245BC File Offset: 0x001227BC
		public override DbExpression Visit(DbLikeExpression expression)
		{
			Check.NotNull<DbLikeExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Pattern);
			DbExpression dbExpression4 = this.VisitExpression(expression.Escape);
			if (expression.Argument != dbExpression2 || expression.Pattern != dbExpression3 || expression.Escape != dbExpression4)
			{
				dbExpression = dbExpression2.Like(dbExpression3, dbExpression4);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0012462C File Offset: 0x0012282C
		public override DbExpression Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			DbExpression dbExpression3 = this.VisitExpression(expression.Limit);
			if (expression.Argument != dbExpression2 || expression.Limit != dbExpression3)
			{
				dbExpression = dbExpression2.Limit(dbExpression3);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x00124684 File Offset: 0x00122884
		public override DbExpression Visit(DbIsNullExpression expression)
		{
			Check.NotNull<DbIsNullExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.IsNull));
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x001246A8 File Offset: 0x001228A8
		public override DbExpression Visit(DbArithmeticExpression expression)
		{
			Check.NotNull<DbArithmeticExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			if (expression.Arguments != list)
			{
				DbExpressionKind expressionKind = expression.ExpressionKind;
				if (expressionKind <= DbExpressionKind.Multiply)
				{
					if (expressionKind == DbExpressionKind.Divide)
					{
						dbExpression = list[0].Divide(list[1]);
						goto IL_00E1;
					}
					switch (expressionKind)
					{
					case DbExpressionKind.Minus:
						dbExpression = list[0].Minus(list[1]);
						goto IL_00E1;
					case DbExpressionKind.Modulo:
						dbExpression = list[0].Modulo(list[1]);
						goto IL_00E1;
					case DbExpressionKind.Multiply:
						dbExpression = list[0].Multiply(list[1]);
						goto IL_00E1;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Plus)
					{
						dbExpression = list[0].Plus(list[1]);
						goto IL_00E1;
					}
					if (expressionKind == DbExpressionKind.UnaryMinus)
					{
						dbExpression = list[0].UnaryMinus();
						goto IL_00E1;
					}
				}
				throw new NotSupportedException();
			}
			IL_00E1:
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0012479F File Offset: 0x0012299F
		public override DbExpression Visit(DbAndExpression expression)
		{
			Check.NotNull<DbAndExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x001247C0 File Offset: 0x001229C0
		public override DbExpression Visit(DbOrExpression expression)
		{
			Check.NotNull<DbOrExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x001247E4 File Offset: 0x001229E4
		public override DbExpression Visit(DbInExpression expression)
		{
			Check.NotNull<DbInExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Item);
			IList<DbExpression> list = this.VisitExpressionList(expression.List);
			if (expression.Item != dbExpression2 || expression.List != list)
			{
				dbExpression = DbExpressionBuilder.CreateInExpression(dbExpression2, list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0012483C File Offset: 0x00122A3C
		public override DbExpression Visit(DbNotExpression expression)
		{
			Check.NotNull<DbNotExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Not));
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0012485D File Offset: 0x00122A5D
		public override DbExpression Visit(DbDistinctExpression expression)
		{
			Check.NotNull<DbDistinctExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Distinct));
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0012487E File Offset: 0x00122A7E
		public override DbExpression Visit(DbElementExpression expression)
		{
			Check.NotNull<DbElementExpression>(expression, "expression");
			return this.VisitUnary(expression, expression.IsSinglePropertyUnwrapped ? new Func<DbExpression, DbExpression>(DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty) : new Func<DbExpression, DbExpression>(DbExpressionBuilder.Element));
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x001248B5 File Offset: 0x00122AB5
		public override DbExpression Visit(DbIsEmptyExpression expression)
		{
			Check.NotNull<DbIsEmptyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.IsEmpty));
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x001248D6 File Offset: 0x00122AD6
		public override DbExpression Visit(DbUnionAllExpression expression)
		{
			Check.NotNull<DbUnionAllExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x001248F7 File Offset: 0x00122AF7
		public override DbExpression Visit(DbIntersectExpression expression)
		{
			Check.NotNull<DbIntersectExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x00124918 File Offset: 0x00122B18
		public override DbExpression Visit(DbExceptExpression expression)
		{
			Check.NotNull<DbExceptExpression>(expression, "expression");
			return this.VisitBinary(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x00124939 File Offset: 0x00122B39
		public override DbExpression Visit(DbTreatExpression expression)
		{
			Check.NotNull<DbTreatExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.TreatAs));
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x00124960 File Offset: 0x00122B60
		public override DbExpression Visit(DbIsOfExpression expression)
		{
			Check.NotNull<DbIsOfExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.IsOfOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOfOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.IsOf));
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x001249B6 File Offset: 0x00122BB6
		public override DbExpression Visit(DbCastExpression expression)
		{
			Check.NotNull<DbCastExpression>(expression, "expression");
			return this.VisitTypeUnary(expression, expression.ResultType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.CastTo));
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x001249E0 File Offset: 0x00122BE0
		public override DbExpression Visit(DbCaseExpression expression)
		{
			Check.NotNull<DbCaseExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpression> list = this.VisitExpressionList(expression.When);
			IList<DbExpression> list2 = this.VisitExpressionList(expression.Then);
			DbExpression dbExpression2 = this.VisitExpression(expression.Else);
			if (expression.When != list || expression.Then != list2 || expression.Else != dbExpression2)
			{
				dbExpression = DbExpressionBuilder.Case(list, list2, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00124A50 File Offset: 0x00122C50
		public override DbExpression Visit(DbOfTypeExpression expression)
		{
			Check.NotNull<DbOfTypeExpression>(expression, "expression");
			if (expression.ExpressionKind == DbExpressionKind.OfTypeOnly)
			{
				return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfTypeOnly));
			}
			return this.VisitTypeUnary(expression, expression.OfType, new Func<DbExpression, TypeUsage, DbExpression>(DbExpressionBuilder.OfType));
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x00124AA8 File Offset: 0x00122CA8
		public override DbExpression Visit(DbNewInstanceExpression expression)
		{
			Check.NotNull<DbNewInstanceExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			TypeUsage typeUsage = this.VisitTypeUsage(expression.ResultType);
			IList<DbExpression> list = this.VisitExpressionList(expression.Arguments);
			bool flag = expression.ResultType == typeUsage && expression.Arguments == list;
			if (expression.HasRelatedEntityReferences)
			{
				IList<DbRelatedEntityRef> list2 = DefaultExpressionVisitor.VisitList<DbRelatedEntityRef>(expression.RelatedEntityReferences, new Func<DbRelatedEntityRef, DbRelatedEntityRef>(this.VisitRelatedEntityRef));
				if (!flag || expression.RelatedEntityReferences != list2)
				{
					dbExpression = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression((EntityType)typeUsage.EdmType, list, list2);
				}
			}
			else if (!flag)
			{
				dbExpression = typeUsage.New(list.ToArray<DbExpression>());
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00124B50 File Offset: 0x00122D50
		public override DbExpression Visit(DbRefExpression expression)
		{
			Check.NotNull<DbRefExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(expression.ResultType).ElementType;
			DbExpression dbExpression2 = this.VisitExpression(expression.Argument);
			EntityType entityType2 = (EntityType)this.VisitType(entityType);
			EntitySet entitySet = (EntitySet)this.VisitEntitySet(expression.EntitySet);
			if (expression.Argument != dbExpression2 || entityType != entityType2 || expression.EntitySet != entitySet)
			{
				dbExpression = entitySet.RefFromKey(dbExpression2, entityType2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00124BD8 File Offset: 0x00122DD8
		public override DbExpression Visit(DbRelationshipNavigationExpression expression)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			this.VisitRelationshipEnds(expression.NavigateFrom, expression.NavigateTo, out relationshipEndMember, out relationshipEndMember2);
			DbExpression dbExpression2 = this.VisitExpression(expression.NavigationSource);
			if (expression.NavigateFrom != relationshipEndMember || expression.NavigateTo != relationshipEndMember2 || expression.NavigationSource != dbExpression2)
			{
				dbExpression = dbExpression2.Navigate(relationshipEndMember, relationshipEndMember2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00124C43 File Offset: 0x00122E43
		public override DbExpression Visit(DbDerefExpression expression)
		{
			Check.NotNull<DbDerefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.Deref));
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00124C64 File Offset: 0x00122E64
		public override DbExpression Visit(DbRefKeyExpression expression)
		{
			Check.NotNull<DbRefKeyExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetRefKey));
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x00124C85 File Offset: 0x00122E85
		public override DbExpression Visit(DbEntityRefExpression expression)
		{
			Check.NotNull<DbEntityRefExpression>(expression, "expression");
			return this.VisitUnary(expression, new Func<DbExpression, DbExpression>(DbExpressionBuilder.GetEntityRef));
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00124CA8 File Offset: 0x00122EA8
		public override DbExpression Visit(DbScanExpression expression)
		{
			Check.NotNull<DbScanExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			EntitySetBase entitySetBase = this.VisitEntitySet(expression.Target);
			if (expression.Target != entitySetBase)
			{
				dbExpression = entitySetBase.Scan();
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00124CEC File Offset: 0x00122EEC
		public override DbExpression Visit(DbFilterExpression expression)
		{
			Check.NotNull<DbFilterExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Predicate != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Filter(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00124D4C File Offset: 0x00122F4C
		public override DbExpression Visit(DbProjectExpression expression)
		{
			Check.NotNull<DbProjectExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Projection);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Projection != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Project(dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00124DAC File Offset: 0x00122FAC
		public override DbExpression Visit(DbCrossJoinExpression expression)
		{
			Check.NotNull<DbCrossJoinExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			IList<DbExpressionBinding> list = this.VisitExpressionBindingList(expression.Inputs);
			if (expression.Inputs != list)
			{
				dbExpression = DbExpressionBuilder.CrossJoin(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00124DF0 File Offset: 0x00122FF0
		public override DbExpression Visit(DbJoinExpression expression)
		{
			Check.NotNull<DbJoinExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBinding(expression.Left);
			DbExpressionBinding dbExpressionBinding2 = this.VisitExpressionBinding(expression.Right);
			this.EnterScope(new DbVariableReferenceExpression[] { dbExpressionBinding.Variable, dbExpressionBinding2.Variable });
			DbExpression dbExpression2 = this.VisitExpression(expression.JoinCondition);
			this.ExitScope();
			if (expression.Left != dbExpressionBinding || expression.Right != dbExpressionBinding2 || expression.JoinCondition != dbExpression2)
			{
				if (DbExpressionKind.InnerJoin == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.InnerJoin(dbExpressionBinding2, dbExpression2);
				}
				else if (DbExpressionKind.LeftOuterJoin == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.LeftOuterJoin(dbExpressionBinding2, dbExpression2);
				}
				else
				{
					dbExpression = dbExpressionBinding.FullOuterJoin(dbExpressionBinding2, dbExpression2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00124EB0 File Offset: 0x001230B0
		public override DbExpression Visit(DbApplyExpression expression)
		{
			Check.NotNull<DbApplyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpressionBinding dbExpressionBinding2 = this.VisitExpressionBinding(expression.Apply);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Apply != dbExpressionBinding2)
			{
				if (DbExpressionKind.CrossApply == expression.ExpressionKind)
				{
					dbExpression = dbExpressionBinding.CrossApply(dbExpressionBinding2);
				}
				else
				{
					dbExpression = dbExpressionBinding.OuterApply(dbExpressionBinding2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00124F24 File Offset: 0x00123124
		public override DbExpression Visit(DbGroupByExpression expression)
		{
			Check.NotNull<DbGroupByExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbGroupExpressionBinding dbGroupExpressionBinding = this.VisitGroupExpressionBinding(expression.Input);
			this.EnterScope(new DbVariableReferenceExpression[] { dbGroupExpressionBinding.Variable });
			IList<DbExpression> list = this.VisitExpressionList(expression.Keys);
			this.ExitScope();
			this.EnterScope(new DbVariableReferenceExpression[] { dbGroupExpressionBinding.GroupVariable });
			IList<DbAggregate> list2 = DefaultExpressionVisitor.VisitList<DbAggregate>(expression.Aggregates, new Func<DbAggregate, DbAggregate>(this.VisitAggregate));
			this.ExitScope();
			if (expression.Input != dbGroupExpressionBinding || expression.Keys != list || expression.Aggregates != list2)
			{
				RowType edmType = TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(expression.ResultType).TypeUsage);
				List<KeyValuePair<string, DbExpression>> list3 = (from p in edmType.Properties.Take(list.Count)
					select p.Name).Zip(list).ToList<KeyValuePair<string, DbExpression>>();
				List<KeyValuePair<string, DbAggregate>> list4 = (from p in edmType.Properties.Skip(list.Count)
					select p.Name).Zip(list2).ToList<KeyValuePair<string, DbAggregate>>();
				dbExpression = dbGroupExpressionBinding.GroupBy(list3, list4);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00125074 File Offset: 0x00123274
		public override DbExpression Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			DbExpression dbExpression2 = this.VisitExpression(expression.Count);
			if (expression.Input != dbExpressionBinding || expression.SortOrder != list || expression.Count != dbExpression2)
			{
				dbExpression = dbExpressionBinding.Skip(list, dbExpression2);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x001250EC File Offset: 0x001232EC
		public override DbExpression Visit(DbSortExpression expression)
		{
			Check.NotNull<DbSortExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<DbSortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.SortOrder != list)
			{
				dbExpression = dbExpressionBinding.Sort(list);
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0012514C File Offset: 0x0012334C
		public override DbExpression Visit(DbQuantifierExpression expression)
		{
			Check.NotNull<DbQuantifierExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpressionBinding dbExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			DbExpression dbExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (expression.Input != dbExpressionBinding || expression.Predicate != dbExpression2)
			{
				if (expression.ExpressionKind == DbExpressionKind.All)
				{
					dbExpression = dbExpressionBinding.All(dbExpression2);
				}
				else
				{
					dbExpression = dbExpressionBinding.Any(dbExpression2);
				}
			}
			this.NotifyIfChanged(expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x04001DD5 RID: 7637
		private readonly Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression> varMappings = new Dictionary<DbVariableReferenceExpression, DbVariableReferenceExpression>();
	}
}
