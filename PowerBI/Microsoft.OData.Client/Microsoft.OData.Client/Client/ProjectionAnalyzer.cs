using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000093 RID: 147
	internal static class ProjectionAnalyzer
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000F860 File Offset: 0x0000DA60
		internal static bool Analyze(LambdaExpression le, ResourceExpression re, bool matchMembers, DataServiceContext context)
		{
			if (le.Body.NodeType == ExpressionType.Constant)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(le.Body.Type))
				{
					throw new NotSupportedException(Strings.ALinq_CannotCreateConstantEntity);
				}
				re.Projection = new ProjectionQueryOptionExpression(le.Body.Type, le, new List<string>());
				return true;
			}
			else
			{
				if (le.Body.NodeType == ExpressionType.MemberInit || le.Body.NodeType == ExpressionType.New)
				{
					ProjectionAnalyzer.AnalyzeResourceExpression(le, re, context);
					return true;
				}
				if (matchMembers)
				{
					Expression expression = ProjectionAnalyzer.SkipConverts(le.Body);
					if (expression.NodeType == ExpressionType.MemberAccess)
					{
						ProjectionAnalyzer.AnalyzeResourceExpression(le, re, context);
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000F904 File Offset: 0x0000DB04
		private static void Analyze(LambdaExpression e, SelectExpandPathBuilder pb, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(e.Body.Type);
			ParameterExpression parameterExpression = e.Parameters.Last<ParameterExpression>();
			bool flag2 = ClientTypeUtil.TypeOrElementTypeIsEntity(parameterExpression.Type);
			if (flag2)
			{
				pb.PushParamExpression(parameterExpression);
			}
			if (!flag)
			{
				ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(e.Body, pb, context);
			}
			else
			{
				ExpressionType nodeType = e.Body.NodeType;
				if (nodeType == ExpressionType.Constant)
				{
					throw new NotSupportedException(Strings.ALinq_CannotCreateConstantEntity);
				}
				if (nodeType != ExpressionType.MemberInit)
				{
					if (nodeType == ExpressionType.New)
					{
						throw new NotSupportedException(Strings.ALinq_CannotConstructKnownEntityTypes);
					}
					ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(e.Body, pb, context);
				}
				else
				{
					ProjectionAnalyzer.EntityProjectionAnalyzer.Analyze((MemberInitExpression)e.Body, pb, context);
				}
			}
			if (flag2)
			{
				pb.PopParamExpression();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000F9B3 File Offset: 0x0000DBB3
		internal static bool IsMethodCallAllowedEntitySequence(MethodCallExpression call)
		{
			return ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.ToList) || ReflectionUtil.IsSequenceMethod(call.Method, SequenceMethod.Select);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		internal static void CheckChainedSequence(MethodCallExpression call, Type type)
		{
			if (ReflectionUtil.IsSequenceSelectMethod(call.Method))
			{
				MethodCallExpression methodCallExpression = ResourceBinder.StripTo<MethodCallExpression>(call.Arguments[0]);
				if (methodCallExpression != null && ReflectionUtil.IsSequenceSelectMethod(methodCallExpression.Method))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(type, call.ToString()));
				}
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000FA28 File Offset: 0x0000DC28
		internal static bool IsCollectionProducingExpression(Expression e)
		{
			if (TypeSystem.FindIEnumerable(e.Type) != null)
			{
				Type elementType = TypeSystem.GetElementType(e.Type);
				Type dataServiceCollectionOfT = WebUtil.GetDataServiceCollectionOfT(new Type[] { elementType });
				if (typeof(List<>).MakeGenericType(new Type[] { elementType }).IsAssignableFrom(e.Type) || (dataServiceCollectionOfT != null && dataServiceCollectionOfT.IsAssignableFrom(e.Type)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		internal static bool IsDisallowedExpressionForMethodCall(Expression e, ClientEdmModel model)
		{
			MemberExpression memberExpression = e as MemberExpression;
			return (memberExpression == null || !ClientTypeUtil.TypeIsEntity(memberExpression.Expression.Type, model)) && ProjectionAnalyzer.IsCollectionProducingExpression(e);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		private static void Analyze(MemberInitExpression mie, SelectExpandPathBuilder pb, DataServiceContext context)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(mie.Type);
			if (flag)
			{
				ProjectionAnalyzer.EntityProjectionAnalyzer.Analyze(mie, pb, context);
				return;
			}
			ProjectionAnalyzer.NonEntityProjectionAnalyzer.Analyze(mie, pb, context);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000FB08 File Offset: 0x0000DD08
		private static void AnalyzeResourceExpression(LambdaExpression lambda, ResourceExpression resource, DataServiceContext context)
		{
			SelectExpandPathBuilder selectExpandPathBuilder = new SelectExpandPathBuilder();
			ProjectionAnalyzer.Analyze(lambda, selectExpandPathBuilder, context);
			resource.Projection = new ProjectionQueryOptionExpression(lambda.Body.Type, lambda, selectExpandPathBuilder.ProjectionPaths.ToList<string>());
			resource.ExpandPaths = selectExpandPathBuilder.ExpandPaths.Union(resource.ExpandPaths, StringComparer.Ordinal).ToList<string>();
			resource.RaiseUriVersion(selectExpandPathBuilder.UriVersion);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000FB74 File Offset: 0x0000DD74
		private static Expression SkipConverts(Expression expression)
		{
			Expression expression2 = expression;
			while (expression2.NodeType == ExpressionType.Convert || expression2.NodeType == ExpressionType.ConvertChecked)
			{
				expression2 = ((UnaryExpression)expression2).Operand;
			}
			return expression2;
		}

		// Token: 0x02000175 RID: 373
		private class EntityProjectionAnalyzer : ALinqExpressionVisitor
		{
			// Token: 0x06000D99 RID: 3481 RVA: 0x0002ECE7 File Offset: 0x0002CEE7
			private EntityProjectionAnalyzer(SelectExpandPathBuilder pb, Type type, DataServiceContext context)
			{
				this.builder = pb;
				this.type = type;
				this.context = context;
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x0002ED04 File Offset: 0x0002CF04
			internal static void Analyze(MemberInitExpression mie, SelectExpandPathBuilder pb, DataServiceContext context)
			{
				ProjectionAnalyzer.EntityProjectionAnalyzer entityProjectionAnalyzer = new ProjectionAnalyzer.EntityProjectionAnalyzer(pb, mie.Type, context);
				MemberAssignmentAnalysis memberAssignmentAnalysis = null;
				foreach (MemberBinding memberBinding in mie.Bindings)
				{
					MemberAssignment memberAssignment = memberBinding as MemberAssignment;
					entityProjectionAnalyzer.Visit(memberAssignment.Expression);
					if (memberAssignment != null)
					{
						MemberAssignmentAnalysis memberAssignmentAnalysis2 = MemberAssignmentAnalysis.Analyze(pb.ParamExpressionInScope, memberAssignment.Expression);
						if (memberAssignmentAnalysis2.IncompatibleAssignmentsException != null)
						{
							throw memberAssignmentAnalysis2.IncompatibleAssignmentsException;
						}
						Type memberType = ClientTypeUtil.GetMemberType(memberAssignment.Member);
						Expression[] expressionsBeyondTargetEntity = memberAssignmentAnalysis2.GetExpressionsBeyondTargetEntity();
						if (expressionsBeyondTargetEntity.Length == 0)
						{
							throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(memberType, memberAssignment.Expression));
						}
						MemberExpression memberExpression = expressionsBeyondTargetEntity[expressionsBeyondTargetEntity.Length - 1] as MemberExpression;
						memberAssignmentAnalysis2.CheckCompatibleAssignments(mie.Type, ref memberAssignmentAnalysis);
						if (memberExpression != null)
						{
							if (memberExpression.Member.Name != memberAssignment.Member.Name)
							{
								throw new NotSupportedException(Strings.ALinq_PropertyNamesMustMatchInProjections(memberExpression.Member.Name, memberAssignment.Member.Name));
							}
							bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(memberType);
							bool flag2 = ClientTypeUtil.TypeOrElementTypeIsEntity(memberExpression.Type);
							if (flag2 && !flag)
							{
								throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(memberType, memberAssignment.Expression));
							}
						}
					}
				}
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x0002EE78 File Offset: 0x0002D078
			internal override Expression VisitUnary(UnaryExpression u)
			{
				if (ResourceBinder.PatternRules.MatchConvertToAssignable(u) || (u.NodeType == ExpressionType.TypeAs && this.leafExpressionIsMemberAccess))
				{
					return base.VisitUnary(u);
				}
				if (u.NodeType == ExpressionType.Convert || u.NodeType == ExpressionType.ConvertChecked)
				{
					Type type = Nullable.GetUnderlyingType(u.Operand.Type) ?? u.Operand.Type;
					Type type2 = Nullable.GetUnderlyingType(u.Type) ?? u.Type;
					if (PrimitiveType.IsKnownType(type) && PrimitiveType.IsKnownType(type2))
					{
						return base.Visit(u.Operand);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, u.ToString()));
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitBinary(BinaryExpression b)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, b.ToString()));
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitTypeIs(TypeBinaryExpression b)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, b.ToString()));
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x0002EF3C File Offset: 0x0002D13C
			internal override Expression VisitConditional(ConditionalExpression c)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.builder.ParamExpressionInScope, c);
				if (matchNullCheckResult.Match)
				{
					this.Visit(matchNullCheckResult.AssignExpression);
					return c;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, c.ToString()));
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitConstant(ConstantExpression c)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, c.ToString()));
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x0002EF88 File Offset: 0x0002D188
			internal override Expression VisitMemberAccess(MemberExpression m)
			{
				this.leafExpressionIsMemberAccess = true;
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(m.Expression.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(m.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
				}
				PropertyInfo propertyInfo;
				Expression expression;
				if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(m, out propertyInfo, out expression))
				{
					Expression expression2 = base.VisitMemberAccess(m);
					Type type;
					ResourceBinder.StripTo<Expression>(m.Expression, out type);
					this.builder.AppendPropertyToPath(propertyInfo, type, this.context);
					this.leafExpressionIsMemberAccess = false;
					return expression2;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x0002F028 File Offset: 0x0002D228
			internal override Expression VisitMethodCall(MethodCallExpression m)
			{
				if ((m.Object != null && (ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(m.Object, this.context.Model) || !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Object.Type))) || m.Arguments.Any((Expression a) => ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(a, this.context.Model)) || (m.Object == null && !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Arguments[0].Type)))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				if (ProjectionAnalyzer.IsMethodCallAllowedEntitySequence(m))
				{
					ProjectionAnalyzer.CheckChainedSequence(m, this.type);
					return base.VisitMethodCall(m);
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, m.ToString()));
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitInvocation(InvocationExpression iv)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, iv.ToString()));
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x0002F0E9 File Offset: 0x0002D2E9
			internal override Expression VisitLambda(LambdaExpression lambda)
			{
				ProjectionAnalyzer.Analyze(lambda, this.builder, this.context);
				return lambda;
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitListInit(ListInitExpression init)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, init.ToString()));
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override Expression VisitNewArray(NewArrayExpression na)
			{
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, na.ToString()));
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x0002F0FE File Offset: 0x0002D2FE
			internal override Expression VisitMemberInit(MemberInitExpression init)
			{
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(init.Type))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, init.ToString()));
				}
				ProjectionAnalyzer.Analyze(init, this.builder, this.context);
				return init;
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0002F138 File Offset: 0x0002D338
			internal override NewExpression VisitNew(NewExpression nex)
			{
				if (ResourceBinder.PatternRules.MatchNewDataServiceCollectionOfT(nex))
				{
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type))
					{
						foreach (Expression expression in nex.Arguments)
						{
							if (expression.NodeType != ExpressionType.Constant)
							{
								base.Visit(expression);
							}
						}
						return nex;
					}
				}
				else if (ResourceBinder.PatternRules.MatchNewCollectionOfT(nex) && !ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type))
				{
					foreach (Expression expression2 in nex.Arguments)
					{
						if (expression2.NodeType != ExpressionType.Constant)
						{
							base.Visit(expression2);
						}
					}
					return nex;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjectionToEntity(this.type, nex.ToString()));
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0002F220 File Offset: 0x0002D420
			internal override Expression VisitParameter(ParameterExpression p)
			{
				if (p != this.builder.ParamExpressionInScope)
				{
					throw new NotSupportedException(Strings.ALinq_CanOnlyProjectTheLeaf);
				}
				this.builder.StartNewPath();
				return p;
			}

			// Token: 0x0400072E RID: 1838
			private readonly SelectExpandPathBuilder builder;

			// Token: 0x0400072F RID: 1839
			private readonly Type type;

			// Token: 0x04000730 RID: 1840
			private bool leafExpressionIsMemberAccess;

			// Token: 0x04000731 RID: 1841
			private readonly DataServiceContext context;
		}

		// Token: 0x02000176 RID: 374
		private class NonEntityProjectionAnalyzer : DataServiceALinqExpressionVisitor
		{
			// Token: 0x06000DAA RID: 3498 RVA: 0x0002F25A File Offset: 0x0002D45A
			private NonEntityProjectionAnalyzer(SelectExpandPathBuilder pb, Type type, DataServiceContext context)
			{
				this.builder = pb;
				this.type = type;
				this.context = context;
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0002F278 File Offset: 0x0002D478
			internal static void Analyze(Expression e, SelectExpandPathBuilder pb, DataServiceContext context)
			{
				ProjectionAnalyzer.NonEntityProjectionAnalyzer nonEntityProjectionAnalyzer = new ProjectionAnalyzer.NonEntityProjectionAnalyzer(pb, e.Type, context);
				MemberInitExpression memberInitExpression = e as MemberInitExpression;
				if (memberInitExpression != null)
				{
					using (IEnumerator<MemberBinding> enumerator = memberInitExpression.Bindings.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MemberBinding memberBinding = enumerator.Current;
							MemberAssignment memberAssignment = memberBinding as MemberAssignment;
							if (memberAssignment != null)
							{
								nonEntityProjectionAnalyzer.Visit(memberAssignment.Expression);
							}
						}
						return;
					}
				}
				nonEntityProjectionAnalyzer.Visit(e);
			}

			// Token: 0x06000DAC RID: 3500 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
			internal override Expression VisitUnary(UnaryExpression u)
			{
				if (!ResourceBinder.PatternRules.MatchConvertToAssignable(u))
				{
					if (u.NodeType == ExpressionType.TypeAs && this.leafExpressionIsMemberAccess)
					{
						return base.VisitUnary(u);
					}
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(u.Operand.Type))
					{
						throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, u.ToString()));
					}
				}
				return base.VisitUnary(u);
			}

			// Token: 0x06000DAD RID: 3501 RVA: 0x0002F358 File Offset: 0x0002D558
			internal override Expression VisitBinary(BinaryExpression b)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(b.Left.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(b.Right.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Left) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Right))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, b.ToString()));
				}
				return base.VisitBinary(b);
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x0002F3C1 File Offset: 0x0002D5C1
			internal override Expression VisitTypeIs(TypeBinaryExpression b)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(b.Expression.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(b.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, b.ToString()));
				}
				return base.VisitTypeIs(b);
			}

			// Token: 0x06000DAF RID: 3503 RVA: 0x0002F400 File Offset: 0x0002D600
			internal override Expression VisitConditional(ConditionalExpression c)
			{
				ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.builder.ParamExpressionInScope, c);
				if (matchNullCheckResult.Match)
				{
					this.Visit(matchNullCheckResult.AssignExpression);
					return c;
				}
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(c.Test.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(c.IfTrue.Type) || ClientTypeUtil.TypeOrElementTypeIsEntity(c.IfFalse.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(c.Test) || ProjectionAnalyzer.IsCollectionProducingExpression(c.IfTrue) || ProjectionAnalyzer.IsCollectionProducingExpression(c.IfFalse))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, c.ToString()));
				}
				return base.VisitConditional(c);
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x0002F4B4 File Offset: 0x0002D6B4
			internal override Expression VisitMemberAccess(MemberExpression m)
			{
				Type type = m.Expression.Type;
				this.leafExpressionIsMemberAccess = true;
				if (PrimitiveType.IsKnownNullableType(type))
				{
					this.leafExpressionIsMemberAccess = false;
					return base.VisitMemberAccess(m);
				}
				if (ProjectionAnalyzer.IsCollectionProducingExpression(m.Expression))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				PropertyInfo propertyInfo;
				Expression expression;
				if (ResourceBinder.PatternRules.MatchNonPrivateReadableProperty(m, out propertyInfo, out expression))
				{
					Expression expression2 = base.VisitMemberAccess(m);
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(type))
					{
						Type type2;
						ResourceBinder.StripTo<Expression>(m.Expression, out type2);
						this.builder.AppendPropertyToPath(propertyInfo, type2, this.context);
						this.leafExpressionIsMemberAccess = false;
					}
					return expression2;
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x0002F56C File Offset: 0x0002D76C
			internal override Expression VisitMethodCall(MethodCallExpression m)
			{
				if ((m.Object != null && ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(m.Object, this.context.Model)) || m.Arguments.Any((Expression a) => ProjectionAnalyzer.IsDisallowedExpressionForMethodCall(a, this.context.Model)))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
				}
				ProjectionAnalyzer.CheckChainedSequence(m, this.type);
				if (ProjectionAnalyzer.IsMethodCallAllowedEntitySequence(m))
				{
					return base.VisitMethodCall(m);
				}
				if (m.Object == null || !ClientTypeUtil.TypeOrElementTypeIsEntity(m.Object.Type))
				{
					if (!m.Arguments.Any((Expression a) => ClientTypeUtil.TypeOrElementTypeIsEntity(a.Type)))
					{
						return base.VisitMethodCall(m);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, m.ToString()));
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x0002F64C File Offset: 0x0002D84C
			internal override Expression VisitInvocation(InvocationExpression iv)
			{
				if (!ClientTypeUtil.TypeOrElementTypeIsEntity(iv.Expression.Type) && !ProjectionAnalyzer.IsCollectionProducingExpression(iv.Expression))
				{
					if (!iv.Arguments.Any((Expression a) => ClientTypeUtil.TypeOrElementTypeIsEntity(a.Type) || ProjectionAnalyzer.IsCollectionProducingExpression(a)))
					{
						return base.VisitInvocation(iv);
					}
				}
				throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, iv.ToString()));
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x0002F6C2 File Offset: 0x0002D8C2
			internal override Expression VisitLambda(LambdaExpression lambda)
			{
				ProjectionAnalyzer.Analyze(lambda, this.builder, this.context);
				return lambda;
			}

			// Token: 0x06000DB4 RID: 3508 RVA: 0x0002F6D7 File Offset: 0x0002D8D7
			internal override Expression VisitMemberInit(MemberInitExpression init)
			{
				ProjectionAnalyzer.Analyze(init, this.builder, this.context);
				return init;
			}

			// Token: 0x06000DB5 RID: 3509 RVA: 0x0002F6EC File Offset: 0x0002D8EC
			internal override NewExpression VisitNew(NewExpression nex)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(nex.Type) && !ResourceBinder.PatternRules.MatchNewDataServiceCollectionOfT(nex))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, nex.ToString()));
				}
				return base.VisitNew(nex);
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x0002F721 File Offset: 0x0002D921
			internal override Expression VisitParameter(ParameterExpression p)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(p.Type))
				{
					if (p != this.builder.ParamExpressionInScope)
					{
						throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, p.ToString()));
					}
					this.builder.StartNewPath();
				}
				return p;
			}

			// Token: 0x06000DB7 RID: 3511 RVA: 0x0002F761 File Offset: 0x0002D961
			internal override Expression VisitConstant(ConstantExpression c)
			{
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(c.Type))
				{
					throw new NotSupportedException(Strings.ALinq_ExpressionNotSupportedInProjection(this.type, c.ToString()));
				}
				return base.VisitConstant(c);
			}

			// Token: 0x04000732 RID: 1842
			private SelectExpandPathBuilder builder;

			// Token: 0x04000733 RID: 1843
			private Type type;

			// Token: 0x04000734 RID: 1844
			private bool leafExpressionIsMemberAccess;

			// Token: 0x04000735 RID: 1845
			private readonly DataServiceContext context;
		}
	}
}
