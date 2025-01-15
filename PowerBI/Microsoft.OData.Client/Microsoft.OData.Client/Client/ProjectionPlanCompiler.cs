using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.Materialization;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000060 RID: 96
	internal class ProjectionPlanCompiler : ALinqExpressionVisitor
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000B6D4 File Offset: 0x000098D4
		private ProjectionPlanCompiler(Dictionary<Expression, Expression> normalizerRewrites)
		{
			this.annotations = new Dictionary<Expression, ProjectionPlanCompiler.ExpressionAnnotation>(ReferenceEqualityComparer<Expression>.Instance);
			this.materializerExpression = Expression.Parameter(typeof(object), "mat");
			this.normalizerRewrites = normalizerRewrites;
			this.pathBuilder = new ProjectionPathBuilder();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B724 File Offset: 0x00009924
		internal static ProjectionPlan CompilePlan(LambdaExpression projection, Dictionary<Expression, Expression> normalizerRewrites)
		{
			ProjectionPlanCompiler projectionPlanCompiler = new ProjectionPlanCompiler(normalizerRewrites);
			Expression expression = projectionPlanCompiler.Visit(projection);
			return new ProjectionPlan
			{
				Plan = (Func<object, object, Type, object>)((LambdaExpression)expression).Compile(),
				ProjectedType = projection.Body.Type
			};
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B770 File Offset: 0x00009970
		internal override Expression VisitBinary(BinaryExpression b)
		{
			Expression expressionBeforeNormalization = this.GetExpressionBeforeNormalization(b);
			if (expressionBeforeNormalization == b)
			{
				return base.VisitBinary(b);
			}
			return this.Visit(expressionBeforeNormalization);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B798 File Offset: 0x00009998
		internal override Expression VisitConditional(ConditionalExpression conditional)
		{
			Expression expressionBeforeNormalization = this.GetExpressionBeforeNormalization(conditional);
			if (expressionBeforeNormalization != conditional)
			{
				return this.Visit(expressionBeforeNormalization);
			}
			ResourceBinder.PatternRules.MatchNullCheckResult matchNullCheckResult = ResourceBinder.PatternRules.MatchNullCheck(this.pathBuilder.LambdaParameterInScope, conditional);
			if (!matchNullCheckResult.Match || !ClientTypeUtil.TypeOrElementTypeIsEntity(ResourceBinder.StripConvertToAssignable(matchNullCheckResult.TestToNullExpression).Type))
			{
				Expression expression = null;
				if (matchNullCheckResult.Match)
				{
					Expression expression2 = this.Visit(matchNullCheckResult.TestToNullExpression);
					if (expression2.NodeType == ExpressionType.Convert)
					{
						expression2 = ((UnaryExpression)expression2).Operand;
					}
					expression = Expression.MakeBinary(ExpressionType.Equal, expression2, Expression.Constant(null));
				}
				if (expression == null)
				{
					expression = this.Visit(conditional.Test);
				}
				Expression expression3 = this.Visit(conditional.IfTrue);
				Expression expression4 = this.Visit(conditional.IfFalse);
				if (expression != conditional.Test || expression3 != conditional.IfTrue || expression4 != conditional.IfFalse)
				{
					return Expression.Condition(expression, expression3, expression4, expression3.Type.IsAssignableFrom(expression4.Type) ? expression3.Type : expression4.Type);
				}
			}
			return this.RebindConditionalNullCheck(conditional, matchNullCheckResult);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B8AC File Offset: 0x00009AAC
		internal override Expression VisitUnary(UnaryExpression u)
		{
			Expression expressionBeforeNormalization = this.GetExpressionBeforeNormalization(u);
			Expression expression;
			if (expressionBeforeNormalization == u)
			{
				expression = base.VisitUnary(u);
				UnaryExpression unaryExpression = expression as UnaryExpression;
				ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
				if (unaryExpression != null && this.annotations.TryGetValue(unaryExpression.Operand, out expressionAnnotation))
				{
					this.annotations[expression] = expressionAnnotation;
				}
			}
			else
			{
				expression = this.Visit(expressionBeforeNormalization);
			}
			return expression;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B904 File Offset: 0x00009B04
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression expression = m.Expression;
			Expression expression2;
			if (PrimitiveType.IsKnownNullableType(expression.Type))
			{
				expression2 = base.VisitMemberAccess(m);
			}
			else
			{
				Expression expression3 = this.Visit(expression);
				ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
				if (this.annotations.TryGetValue(expression3, out expressionAnnotation))
				{
					expression2 = this.RebindMemberAccess(m, expressionAnnotation);
				}
				else
				{
					expression2 = Expression.MakeMemberAccess(expression3, m.Member);
				}
			}
			return expression2;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B960 File Offset: 0x00009B60
		internal override Expression VisitParameter(ParameterExpression p)
		{
			ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
			Expression expression;
			if (this.annotations.TryGetValue(p, out expressionAnnotation))
			{
				expression = this.RebindParameter(p, expressionAnnotation);
			}
			else
			{
				expression = base.VisitParameter(p);
			}
			return expression;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B994 File Offset: 0x00009B94
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			this.pathBuilder.EnterMemberInit(init);
			Expression expression;
			if (this.pathBuilder.CurrentIsEntity && init.Bindings.Count > 0)
			{
				expression = this.RebindEntityMemberInit(init);
			}
			else
			{
				expression = base.VisitMemberInit(init);
			}
			this.pathBuilder.LeaveMemberInit();
			return expression;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expressionBeforeNormalization = this.GetExpressionBeforeNormalization(m);
			if (expressionBeforeNormalization != m)
			{
				return this.Visit(expressionBeforeNormalization);
			}
			Expression expression;
			if (this.pathBuilder.CurrentIsEntity)
			{
				if (m.Method.Name == "Select")
				{
					expression = this.RebindMethodCallForMemberSelect(m);
				}
				else if (m.Method.Name == "ToList")
				{
					expression = this.RebindMethodCallForMemberToList(m);
				}
				else
				{
					expression = base.VisitMethodCall(m);
				}
			}
			else if (ProjectionAnalyzer.IsMethodCallAllowedEntitySequence(m))
			{
				expression = this.RebindMethodCallForNewSequence(m);
			}
			else
			{
				expression = base.VisitMethodCall(m);
			}
			return expression;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BA80 File Offset: 0x00009C80
		internal override Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			if (exp.NodeType != ExpressionType.New)
			{
				return base.Visit(exp);
			}
			NewExpression newExpression = (NewExpression)exp;
			if (ResourceBinder.PatternRules.MatchNewDataServiceCollectionOfT(newExpression))
			{
				return this.RebindNewExpressionForDataServiceCollectionOfT(newExpression);
			}
			return this.VisitNew(newExpression);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		internal override Expression VisitLambda(LambdaExpression lambda)
		{
			Expression expression2;
			if (!this.topLevelProjectionFound || (lambda.Parameters.Count == 1 && ClientTypeUtil.TypeOrElementTypeIsEntity(lambda.Parameters[0].Type)))
			{
				this.topLevelProjectionFound = true;
				ParameterExpression parameterExpression = Expression.Parameter(typeof(Type), "type" + this.identifierId);
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "entry" + this.identifierId);
				this.identifierId++;
				this.pathBuilder.EnterLambdaScope(lambda, parameterExpression2, parameterExpression);
				ProjectionPath projectionPath = new ProjectionPath(lambda.Parameters[0], parameterExpression, parameterExpression2);
				ProjectionPathSegment projectionPathSegment = new ProjectionPathSegment(projectionPath, null, null);
				projectionPath.Add(projectionPathSegment);
				this.annotations[lambda.Parameters[0]] = new ProjectionPlanCompiler.ExpressionAnnotation
				{
					Segment = projectionPathSegment
				};
				Expression expression = this.Visit(lambda.Body);
				if (expression.Type.IsValueType())
				{
					expression = Expression.Convert(expression, typeof(object));
				}
				expression2 = Expression.Lambda<Func<object, object, Type, object>>(expression, new ParameterExpression[] { this.materializerExpression, parameterExpression2, parameterExpression });
				this.pathBuilder.LeaveLambdaScope();
			}
			else
			{
				expression2 = base.VisitLambda(lambda);
			}
			return expression2;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BC20 File Offset: 0x00009E20
		private static Expression CallMaterializer(string methodName, params Expression[] arguments)
		{
			return ProjectionPlanCompiler.CallMaterializerWithType(methodName, null, arguments);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000BC2C File Offset: 0x00009E2C
		private static Expression CallMaterializerWithType(string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			MethodInfo methodInfo = typeof(ODataEntityMaterializerInvoker).GetMethod(methodName, false, true);
			if (typeArguments != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(typeArguments);
			}
			return ProjectionPlanCompiler.dynamicProxyMethodGenerator.GetCallWrapper(methodInfo, arguments);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000BC63 File Offset: 0x00009E63
		private static Expression RebindConstructor(ConstructorInfo info, params Expression[] arguments)
		{
			return ProjectionPlanCompiler.dynamicProxyMethodGenerator.GetCallWrapper(info, arguments);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BC74 File Offset: 0x00009E74
		private Expression CallCheckValueForPathIsNull(Expression entry, Expression entryType, ProjectionPath path)
		{
			Expression expression = ProjectionPlanCompiler.CallMaterializer("ProjectionCheckValueForPathIsNull", new Expression[]
			{
				entry,
				entryType,
				Expression.Constant(path, typeof(object))
			});
			this.annotations.Add(expression, new ProjectionPlanCompiler.ExpressionAnnotation
			{
				Segment = path[path.Count - 1]
			});
			return expression;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		private Expression CallValueForPath(Expression entry, Expression entryType, ProjectionPath path)
		{
			Expression expression = ProjectionPlanCompiler.CallMaterializer("ProjectionValueForPath", new Expression[]
			{
				this.materializerExpression,
				entry,
				entryType,
				Expression.Constant(path, typeof(object))
			});
			this.annotations.Add(expression, new ProjectionPlanCompiler.ExpressionAnnotation
			{
				Segment = path[path.Count - 1]
			});
			return expression;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BD3C File Offset: 0x00009F3C
		private Expression CallValueForPathWithType(Expression entry, Expression entryType, ProjectionPath path, Type type)
		{
			Expression expression = this.CallValueForPath(entry, entryType, path);
			Expression expression2 = Expression.Convert(expression, type);
			this.annotations.Add(expression2, new ProjectionPlanCompiler.ExpressionAnnotation
			{
				Segment = path[path.Count - 1]
			});
			return expression2;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000BD84 File Offset: 0x00009F84
		private Expression RebindConditionalNullCheck(ConditionalExpression conditional, ResourceBinder.PatternRules.MatchNullCheckResult nullCheck)
		{
			Expression expression = this.Visit(nullCheck.TestToNullExpression);
			Expression expression2 = this.Visit(nullCheck.AssignExpression);
			ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
			if (!this.annotations.TryGetValue(expression, out expressionAnnotation))
			{
				return base.VisitConditional(conditional);
			}
			ProjectionPathSegment segment = expressionAnnotation.Segment;
			Expression expression3 = this.CallCheckValueForPathIsNull(segment.StartPath.RootEntry, segment.StartPath.ExpectedRootType, segment.StartPath);
			Expression expression4 = expression3;
			Expression expression5 = Expression.Constant(null, expression2.Type);
			Expression expression6 = expression2;
			return Expression.Condition(expression4, expression5, expression6);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BE14 File Offset: 0x0000A014
		private Expression RebindEntityMemberInit(MemberInitExpression init)
		{
			Expression[] array;
			if (!this.pathBuilder.HasRewrites)
			{
				MemberAssignmentAnalysis memberAssignmentAnalysis = MemberAssignmentAnalysis.Analyze(this.pathBuilder.LambdaParameterInScope, ((MemberAssignment)init.Bindings[0]).Expression);
				array = memberAssignmentAnalysis.GetExpressionsToTargetEntity();
			}
			else
			{
				array = MemberAssignmentAnalysis.EmptyExpressionArray;
			}
			Expression parameterEntryInScope = this.pathBuilder.ParameterEntryInScope;
			List<string> list = new List<string>();
			List<Func<object, object, Type, object>> list2 = new List<Func<object, object, Type, object>>();
			Type type = init.NewExpression.Type;
			Expression expression = Expression.Constant(type, typeof(Type));
			string[] array2 = (from e in array.Skip(1)
				select ((MemberExpression)e).Member.Name).ToArray<string>();
			Expression expression2;
			Expression expression3;
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			if (array.Length <= 1)
			{
				expression2 = this.pathBuilder.ParameterEntryInScope;
				expression3 = this.pathBuilder.ExpectedParamTypeInScope;
				parameterExpression = (ParameterExpression)this.pathBuilder.ParameterEntryInScope;
				parameterExpression2 = (ParameterExpression)this.pathBuilder.ExpectedParamTypeInScope;
			}
			else
			{
				expression2 = this.GetDeepestEntry(array);
				expression3 = expression;
				Type typeFromHandle = typeof(object);
				object obj = "subentry";
				int num = this.identifierId;
				this.identifierId = num + 1;
				parameterExpression = Expression.Parameter(typeFromHandle, obj + num);
				parameterExpression2 = (ParameterExpression)this.pathBuilder.ExpectedParamTypeInScope;
				ProjectionPath projectionPath = new ProjectionPath((ParameterExpression)this.pathBuilder.LambdaParameterInScope, this.pathBuilder.ExpectedParamTypeInScope, this.pathBuilder.ParameterEntryInScope, array.Skip(1));
				this.annotations.Add(expression2, new ProjectionPlanCompiler.ExpressionAnnotation
				{
					Segment = projectionPath[projectionPath.Count - 1]
				});
				this.annotations.Add(parameterExpression, new ProjectionPlanCompiler.ExpressionAnnotation
				{
					Segment = projectionPath[projectionPath.Count - 1]
				});
				this.pathBuilder.RegisterRewrite(this.pathBuilder.LambdaParameterInScope, array2, parameterExpression);
			}
			for (int i = 0; i < init.Bindings.Count; i++)
			{
				MemberAssignment memberAssignment = (MemberAssignment)init.Bindings[i];
				string serverDefinedName = ClientTypeUtil.GetServerDefinedName(memberAssignment.Member);
				list.Add(serverDefinedName);
				LambdaExpression lambdaExpression;
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(ClientTypeUtil.GetMemberType(memberAssignment.Member)) && memberAssignment.Expression.NodeType == ExpressionType.MemberInit)
				{
					Expression expression4 = ProjectionPlanCompiler.CallMaterializer("ProjectionGetEntry", new Expression[]
					{
						parameterEntryInScope,
						Expression.Constant(memberAssignment.Member.Name, typeof(string))
					});
					Type typeFromHandle2 = typeof(object);
					object obj2 = "subentry";
					int num = this.identifierId;
					this.identifierId = num + 1;
					ParameterExpression parameterExpression3 = Expression.Parameter(typeFromHandle2, obj2 + num);
					ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
					ProjectionPath projectionPath2;
					if (this.annotations.TryGetValue(this.pathBuilder.ParameterEntryInScope, out expressionAnnotation))
					{
						projectionPath2 = new ProjectionPath((ParameterExpression)this.pathBuilder.LambdaParameterInScope, this.pathBuilder.ExpectedParamTypeInScope, parameterEntryInScope);
						projectionPath2.AddRange(expressionAnnotation.Segment.StartPath);
					}
					else
					{
						projectionPath2 = new ProjectionPath((ParameterExpression)this.pathBuilder.LambdaParameterInScope, this.pathBuilder.ExpectedParamTypeInScope, parameterEntryInScope, array.Skip(1));
					}
					Type reflectedType = memberAssignment.Member.ReflectedType;
					ProjectionPathSegment projectionPathSegment = new ProjectionPathSegment(projectionPath2, memberAssignment.Member.Name, reflectedType);
					projectionPath2.Add(projectionPathSegment);
					string[] array3 = (from m in projectionPath2
						where m.Member != null
						select m.Member).ToArray<string>();
					this.annotations.Add(parameterExpression3, new ProjectionPlanCompiler.ExpressionAnnotation
					{
						Segment = projectionPathSegment
					});
					this.pathBuilder.RegisterRewrite(this.pathBuilder.LambdaParameterInScope, array3, parameterExpression3);
					Expression expression5 = this.Visit(memberAssignment.Expression);
					this.pathBuilder.RevokeRewrite(this.pathBuilder.LambdaParameterInScope, array3);
					this.annotations.Remove(parameterExpression3);
					expression5 = Expression.Convert(expression5, typeof(object));
					ParameterExpression[] array4 = new ParameterExpression[] { this.materializerExpression, parameterExpression3, parameterExpression2 };
					lambdaExpression = Expression.Lambda(expression5, array4);
					Expression[] array5 = new Expression[] { this.materializerExpression, expression4, parameterExpression2 };
					ParameterExpression[] array6 = new ParameterExpression[]
					{
						this.materializerExpression,
						(ParameterExpression)parameterEntryInScope,
						parameterExpression2
					};
					lambdaExpression = Expression.Lambda(Expression.Invoke(lambdaExpression, array5), array6);
				}
				else
				{
					Expression expression6 = this.Visit(memberAssignment.Expression);
					expression6 = Expression.Convert(expression6, typeof(object));
					ParameterExpression[] array7 = new ParameterExpression[] { this.materializerExpression, parameterExpression, parameterExpression2 };
					lambdaExpression = Expression.Lambda(expression6, array7);
				}
				list2.Add((Func<object, object, Type, object>)lambdaExpression.Compile());
			}
			for (int j = 1; j < array.Length; j++)
			{
				this.pathBuilder.RevokeRewrite(this.pathBuilder.LambdaParameterInScope, array2);
				this.annotations.Remove(expression2);
				this.annotations.Remove(parameterExpression);
			}
			Expression expression7 = ProjectionPlanCompiler.CallMaterializer("ProjectionInitializeEntity", new Expression[]
			{
				this.materializerExpression,
				expression2,
				expression3,
				expression,
				Expression.Constant(list.ToArray()),
				Expression.Constant(list2.ToArray())
			});
			return Expression.Convert(expression7, type);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private Expression GetDeepestEntry(Expression[] path)
		{
			Expression expression = null;
			int num = 1;
			do
			{
				expression = ProjectionPlanCompiler.CallMaterializer("ProjectionGetEntry", new Expression[]
				{
					expression ?? this.pathBuilder.ParameterEntryInScope,
					Expression.Constant(((MemberExpression)path[num]).Member.Name, typeof(string))
				});
				num++;
			}
			while (num < path.Length);
			return expression;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C424 File Offset: 0x0000A624
		private Expression GetExpressionBeforeNormalization(Expression expression)
		{
			Expression expression2;
			if (this.normalizerRewrites != null && this.normalizerRewrites.TryGetValue(expression, out expression2))
			{
				expression = expression2;
			}
			return expression;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C450 File Offset: 0x0000A650
		private Expression RebindParameter(Expression expression, ProjectionPlanCompiler.ExpressionAnnotation annotation)
		{
			Expression expression2 = this.CallValueForPathWithType(annotation.Segment.StartPath.RootEntry, annotation.Segment.StartPath.ExpectedRootType, annotation.Segment.StartPath, expression.Type);
			ProjectionPath projectionPath = new ProjectionPath(annotation.Segment.StartPath.Root, annotation.Segment.StartPath.ExpectedRootType, annotation.Segment.StartPath.RootEntry);
			ProjectionPathSegment projectionPathSegment = new ProjectionPathSegment(projectionPath, null, null);
			projectionPath.Add(projectionPathSegment);
			this.annotations[expression] = new ProjectionPlanCompiler.ExpressionAnnotation
			{
				Segment = projectionPathSegment
			};
			return expression2;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		private Expression RebindMemberAccess(MemberExpression m, ProjectionPlanCompiler.ExpressionAnnotation baseAnnotation)
		{
			Expression expression = m.Expression;
			Expression expression2 = this.pathBuilder.GetRewrite(expression);
			if (expression2 != null)
			{
				Expression expression3 = Expression.Constant(expression.Type, typeof(Type));
				ProjectionPath projectionPath = new ProjectionPath(expression2 as ParameterExpression, expression3, expression2);
				ProjectionPathSegment projectionPathSegment = new ProjectionPathSegment(projectionPath, m);
				projectionPath.Add(projectionPathSegment);
				expression2 = this.CallValueForPathWithType(expression2, expression3, projectionPath, m.Type);
			}
			else
			{
				ProjectionPathSegment projectionPathSegment2 = new ProjectionPathSegment(baseAnnotation.Segment.StartPath, m);
				baseAnnotation.Segment.StartPath.Add(projectionPathSegment2);
				expression2 = this.CallValueForPathWithType(baseAnnotation.Segment.StartPath.RootEntry, baseAnnotation.Segment.StartPath.ExpectedRootType, baseAnnotation.Segment.StartPath, m.Type);
			}
			return expression2;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		private Expression RebindNewExpressionForDataServiceCollectionOfT(NewExpression nex)
		{
			NewExpression newExpression = this.VisitNew(nex);
			Expression expression = null;
			ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation = null;
			if (newExpression != null)
			{
				ConstructorInfo constructorInfo = nex.Type.GetInstanceConstructors(false).First((ConstructorInfo c) => c.GetParameters().Length == 7 && c.GetParameters()[0].ParameterType == typeof(object));
				Type type = typeof(IEnumerable<>).MakeGenericType(new Type[] { nex.Type.GetGenericArguments()[0] });
				if (newExpression.Arguments.Count == 1 && newExpression.Constructor == nex.Type.GetInstanceConstructor(true, new Type[] { type }) && this.annotations.TryGetValue(newExpression.Arguments[0], out expressionAnnotation))
				{
					expression = ProjectionPlanCompiler.RebindConstructor(constructorInfo, new Expression[]
					{
						this.materializerExpression,
						Expression.Constant(null, typeof(DataServiceContext)),
						newExpression.Arguments[0],
						Expression.Constant(TrackingMode.AutoChangeTracking, typeof(TrackingMode)),
						Expression.Constant(null, typeof(string)),
						Expression.Constant(null, typeof(Func<EntityChangedParams, bool>)),
						Expression.Constant(null, typeof(Func<EntityCollectionChangedParams, bool>))
					});
				}
				else if (newExpression.Arguments.Count == 2 && this.annotations.TryGetValue(newExpression.Arguments[0], out expressionAnnotation))
				{
					expression = ProjectionPlanCompiler.RebindConstructor(constructorInfo, new Expression[]
					{
						this.materializerExpression,
						Expression.Constant(null, typeof(DataServiceContext)),
						newExpression.Arguments[0],
						newExpression.Arguments[1],
						Expression.Constant(null, typeof(string)),
						Expression.Constant(null, typeof(Func<EntityChangedParams, bool>)),
						Expression.Constant(null, typeof(Func<EntityCollectionChangedParams, bool>))
					});
				}
				else if (newExpression.Arguments.Count == 5 && this.annotations.TryGetValue(newExpression.Arguments[0], out expressionAnnotation))
				{
					expression = ProjectionPlanCompiler.RebindConstructor(constructorInfo, new Expression[]
					{
						this.materializerExpression,
						Expression.Constant(null, typeof(DataServiceContext)),
						newExpression.Arguments[0],
						newExpression.Arguments[1],
						newExpression.Arguments[2],
						newExpression.Arguments[3],
						newExpression.Arguments[4]
					});
				}
				else if (newExpression.Arguments.Count == 6 && typeof(DataServiceContext).IsAssignableFrom(newExpression.Arguments[0].Type) && this.annotations.TryGetValue(newExpression.Arguments[1], out expressionAnnotation))
				{
					expression = ProjectionPlanCompiler.RebindConstructor(constructorInfo, new Expression[]
					{
						this.materializerExpression,
						newExpression.Arguments[0],
						newExpression.Arguments[1],
						newExpression.Arguments[2],
						newExpression.Arguments[3],
						newExpression.Arguments[4],
						newExpression.Arguments[5]
					});
				}
			}
			if (expressionAnnotation != null)
			{
				this.annotations.Add(newExpression, expressionAnnotation);
			}
			return expression;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C948 File Offset: 0x0000AB48
		private Expression RebindMethodCallForMemberSelect(MethodCallExpression call)
		{
			Expression expression = null;
			Expression expression2 = this.Visit(call.Arguments[0]);
			ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
			this.annotations.TryGetValue(expression2, out expressionAnnotation);
			if (expressionAnnotation != null)
			{
				LambdaExpression lambdaExpression = call.Arguments[1] as LambdaExpression;
				ParameterExpression parameterExpression = lambdaExpression.Parameters.Last<ParameterExpression>();
				Expression expression3 = this.Visit(call.Arguments[1]);
				if (ClientTypeUtil.TypeOrElementTypeIsEntity(parameterExpression.Type))
				{
					Type type = call.Method.ReturnType.GetGenericArguments()[0];
					expression = ProjectionPlanCompiler.CallMaterializer("ProjectionSelect", new Expression[]
					{
						this.materializerExpression,
						this.pathBuilder.ParameterEntryInScope,
						this.pathBuilder.ExpectedParamTypeInScope,
						Expression.Constant(type, typeof(Type)),
						Expression.Constant(expressionAnnotation.Segment.StartPath, typeof(object)),
						expression3
					});
					this.annotations.Add(expression, expressionAnnotation);
					expression = ProjectionPlanCompiler.CallMaterializerWithType("EnumerateAsElementType", new Type[] { type }, new Expression[] { expression });
					this.annotations.Add(expression, expressionAnnotation);
				}
				else
				{
					expression = Expression.Call(call.Method, expression2, expression3);
					this.annotations.Add(expression, expressionAnnotation);
				}
			}
			if (expression == null)
			{
				expression = base.VisitMethodCall(call);
			}
			return expression;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		private Expression RebindMethodCallForMemberToList(MethodCallExpression call)
		{
			Expression expression = this.Visit(call.Arguments[0]);
			ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
			if (this.annotations.TryGetValue(expression, out expressionAnnotation))
			{
				expression = this.TypedEnumerableToList(expression, call.Type);
				this.annotations.Add(expression, expressionAnnotation);
			}
			return expression;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		private Expression RebindMethodCallForNewSequence(MethodCallExpression call)
		{
			Expression expression = null;
			if (call.Method.Name == "Select")
			{
				Expression expression2 = this.Visit(call.Arguments[0]);
				ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation;
				this.annotations.TryGetValue(expression2, out expressionAnnotation);
				if (expressionAnnotation != null)
				{
					LambdaExpression lambdaExpression = call.Arguments[1] as LambdaExpression;
					ParameterExpression parameterExpression = lambdaExpression.Parameters.Last<ParameterExpression>();
					Expression expression3 = this.Visit(call.Arguments[1]);
					if (ClientTypeUtil.TypeOrElementTypeIsEntity(parameterExpression.Type))
					{
						Type type = call.Method.ReturnType.GetGenericArguments()[0];
						expression = ProjectionPlanCompiler.CallMaterializer("ProjectionSelect", new Expression[]
						{
							this.materializerExpression,
							this.pathBuilder.ParameterEntryInScope,
							this.pathBuilder.ExpectedParamTypeInScope,
							Expression.Constant(type, typeof(Type)),
							Expression.Constant(expressionAnnotation.Segment.StartPath, typeof(object)),
							expression3
						});
						this.annotations.Add(expression, expressionAnnotation);
						expression = ProjectionPlanCompiler.CallMaterializerWithType("EnumerateAsElementType", new Type[] { type }, new Expression[] { expression });
						this.annotations.Add(expression, expressionAnnotation);
					}
					else
					{
						expression = Expression.Call(call.Method, expression2, expression3);
						this.annotations.Add(expression, expressionAnnotation);
					}
				}
			}
			else
			{
				Expression expression4 = this.Visit(call.Arguments[0]);
				ProjectionPlanCompiler.ExpressionAnnotation expressionAnnotation2;
				if (this.annotations.TryGetValue(expression4, out expressionAnnotation2))
				{
					expression = this.TypedEnumerableToList(expression4, call.Type);
					this.annotations.Add(expression, expressionAnnotation2);
				}
			}
			if (expression == null)
			{
				expression = base.VisitMethodCall(call);
			}
			return expression;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		private Expression TypedEnumerableToList(Expression source, Type targetType)
		{
			Type type = source.Type.GetGenericArguments()[0];
			Type type2 = targetType.GetGenericArguments()[0];
			return ProjectionPlanCompiler.CallMaterializerWithType("ListAsElementType", new Type[] { type, type2 }, new Expression[] { this.materializerExpression, source });
		}

		// Token: 0x04000105 RID: 261
		private static readonly DynamicProxyMethodGenerator dynamicProxyMethodGenerator = new DynamicProxyMethodGenerator();

		// Token: 0x04000106 RID: 262
		private readonly Dictionary<Expression, ProjectionPlanCompiler.ExpressionAnnotation> annotations;

		// Token: 0x04000107 RID: 263
		private readonly ParameterExpression materializerExpression;

		// Token: 0x04000108 RID: 264
		private readonly Dictionary<Expression, Expression> normalizerRewrites;

		// Token: 0x04000109 RID: 265
		private int identifierId;

		// Token: 0x0400010A RID: 266
		private ProjectionPathBuilder pathBuilder;

		// Token: 0x0400010B RID: 267
		private bool topLevelProjectionFound;

		// Token: 0x0200016A RID: 362
		internal class ExpressionAnnotation
		{
			// Token: 0x17000355 RID: 853
			// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002EAC1 File Offset: 0x0002CCC1
			// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0002EAC9 File Offset: 0x0002CCC9
			internal ProjectionPathSegment Segment { get; set; }
		}
	}
}
