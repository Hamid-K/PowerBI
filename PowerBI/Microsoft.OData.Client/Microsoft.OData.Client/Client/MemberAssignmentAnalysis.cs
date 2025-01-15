using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x02000056 RID: 86
	internal class MemberAssignmentAnalysis : ALinqExpressionVisitor
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000A78C File Offset: 0x0000898C
		private MemberAssignmentAnalysis(Expression entity)
		{
			this.entity = entity;
			this.pathFromEntity = new List<Expression>();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000A7A6 File Offset: 0x000089A6
		internal Exception IncompatibleAssignmentsException
		{
			get
			{
				return this.incompatibleAssignmentsException;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000A7AE File Offset: 0x000089AE
		internal bool MultiplePathsFound
		{
			get
			{
				return this.multiplePathsFound;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A7B8 File Offset: 0x000089B8
		internal static MemberAssignmentAnalysis Analyze(Expression entityInScope, Expression assignmentExpression)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = new MemberAssignmentAnalysis(entityInScope);
			memberAssignmentAnalysis.Visit(assignmentExpression);
			return memberAssignmentAnalysis;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A7D8 File Offset: 0x000089D8
		internal Exception CheckCompatibleAssignments(Type targetType, ref MemberAssignmentAnalysis previous)
		{
			if (previous == null)
			{
				previous = this;
				return null;
			}
			Expression[] expressionsToTargetEntity = previous.GetExpressionsToTargetEntity();
			Expression[] expressionsToTargetEntity2 = this.GetExpressionsToTargetEntity();
			return MemberAssignmentAnalysis.CheckCompatibleAssignments(targetType, expressionsToTargetEntity, expressionsToTargetEntity2);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A805 File Offset: 0x00008A05
		internal override Expression Visit(Expression expression)
		{
			if (this.multiplePathsFound || this.incompatibleAssignmentsException != null)
			{
				return expression;
			}
			return base.Visit(expression);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A820 File Offset: 0x00008A20
		internal override Expression VisitConditional(ConditionalExpression c)
		{
			ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.entity, c);
			Expression expression;
			if (matchNullCheckResult.Match)
			{
				this.Visit(matchNullCheckResult.AssignExpression);
				expression = c;
			}
			else
			{
				expression = base.VisitConditional(c);
			}
			return expression;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A85C File Offset: 0x00008A5C
		internal override Expression VisitParameter(ParameterExpression p)
		{
			if (p == this.entity)
			{
				if (this.pathFromEntity.Count != 0)
				{
					this.multiplePathsFound = true;
				}
				else
				{
					this.pathFromEntity.Add(p);
				}
			}
			return p;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A88C File Offset: 0x00008A8C
		internal override NewExpression VisitNew(NewExpression nex)
		{
			if (nex.Members == null)
			{
				return base.VisitNew(nex);
			}
			MemberAssignmentAnalysis memberAssignmentAnalysis = null;
			foreach (Expression expression in nex.Arguments)
			{
				if (!this.CheckCompatibleAssigmentExpression(expression, nex.Type, ref memberAssignmentAnalysis))
				{
					break;
				}
			}
			return nex;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = null;
			foreach (MemberBinding memberBinding in init.Bindings)
			{
				MemberAssignment memberAssignment = memberBinding as MemberAssignment;
				if (memberAssignment != null && !this.CheckCompatibleAssigmentExpression(memberAssignment.Expression, init.Type, ref memberAssignmentAnalysis))
				{
					break;
				}
			}
			return init;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A968 File Offset: 0x00008B68
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = base.VisitMemberAccess(m);
			Type type;
			Expression expression2 = ResourceBinder.StripTo<Expression>(m.Expression, out type);
			if (this.pathFromEntity.Contains(expression2))
			{
				this.pathFromEntity.Add(m);
			}
			return expression;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A9A6 File Offset: 0x00008BA6
		internal override Expression VisitMethodCall(MethodCallExpression call)
		{
			if (ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.Select))
			{
				this.Visit(call.Arguments[0]);
				return call;
			}
			return base.VisitMethodCall(call);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		internal Expression[] GetExpressionsBeyondTargetEntity()
		{
			if (this.pathFromEntity.Count <= 1)
			{
				return MemberAssignmentAnalysis.EmptyExpressionArray;
			}
			return new Expression[] { this.pathFromEntity[this.pathFromEntity.Count - 1] };
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AA18 File Offset: 0x00008C18
		internal Expression[] GetExpressionsToTargetEntity()
		{
			return this.GetExpressionsToTargetEntity(true);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AA24 File Offset: 0x00008C24
		internal Expression[] GetExpressionsToTargetEntity(bool ignoreLastExpression)
		{
			int num = (ignoreLastExpression ? 1 : 0);
			if (this.pathFromEntity.Count <= num)
			{
				return MemberAssignmentAnalysis.EmptyExpressionArray;
			}
			Expression[] array = new Expression[this.pathFromEntity.Count - num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.pathFromEntity[i];
			}
			return array;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AA80 File Offset: 0x00008C80
		private static Exception CheckCompatibleAssignments(Type targetType, Expression[] previous, Expression[] candidate)
		{
			if (previous.Length != candidate.Length)
			{
				throw MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
			}
			for (int i = 0; i < previous.Length; i++)
			{
				Expression expression = previous[i];
				Expression expression2 = candidate[i];
				if (expression.NodeType != expression2.NodeType)
				{
					throw MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
				}
				if (expression != expression2)
				{
					if (expression.NodeType != ExpressionType.MemberAccess)
					{
						return MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
					}
					if (((MemberExpression)expression).Member.Name != ((MemberExpression)expression2).Member.Name)
					{
						return MemberAssignmentAnalysis.CheckCompatibleAssignmentsFail(targetType, previous, candidate);
					}
				}
			}
			return null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AB14 File Offset: 0x00008D14
		private static Exception CheckCompatibleAssignmentsFail(Type targetType, Expression[] previous, Expression[] candidate)
		{
			string text = Strings.ALinq_ProjectionMemberAssignmentMismatch(targetType.FullName, previous.LastOrDefault<Expression>(), candidate.LastOrDefault<Expression>());
			return new NotSupportedException(text);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AB40 File Offset: 0x00008D40
		private bool CheckCompatibleAssigmentExpression(Expression expressionToAssign, Type initType, ref MemberAssignmentAnalysis previousNested)
		{
			MemberAssignmentAnalysis memberAssignmentAnalysis = MemberAssignmentAnalysis.Analyze(this.entity, expressionToAssign);
			if (memberAssignmentAnalysis.MultiplePathsFound)
			{
				this.multiplePathsFound = true;
				return false;
			}
			Exception ex = memberAssignmentAnalysis.CheckCompatibleAssignments(initType, ref previousNested);
			if (ex != null)
			{
				this.incompatibleAssignmentsException = ex;
				return false;
			}
			if (this.pathFromEntity.Count == 0)
			{
				this.pathFromEntity.AddRange(memberAssignmentAnalysis.GetExpressionsToTargetEntity());
			}
			return true;
		}

		// Token: 0x040000E6 RID: 230
		internal static readonly Expression[] EmptyExpressionArray = new Expression[0];

		// Token: 0x040000E7 RID: 231
		private readonly Expression entity;

		// Token: 0x040000E8 RID: 232
		private Exception incompatibleAssignmentsException;

		// Token: 0x040000E9 RID: 233
		private bool multiplePathsFound;

		// Token: 0x040000EA RID: 234
		private List<Expression> pathFromEntity;
	}
}
