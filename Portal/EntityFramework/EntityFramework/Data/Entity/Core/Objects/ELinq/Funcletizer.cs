using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000460 RID: 1120
	internal sealed class Funcletizer
	{
		// Token: 0x0600371E RID: 14110 RVA: 0x000B2D74 File Offset: 0x000B0F74
		private Funcletizer(Funcletizer.Mode mode, ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			this._mode = mode;
			this._rootContext = rootContext;
			this._rootContextParameter = rootContextParameter;
			this._compiledQueryParameters = compiledQueryParameters;
			if (this._rootContextParameter != null && this._rootContext != null)
			{
				this._rootContextExpression = Expression.Constant(this._rootContext);
			}
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000B2DD0 File Offset: 0x000B0FD0
		internal static Funcletizer CreateCompiledQueryEvaluationFuncletizer(ObjectContext rootContext, ParameterExpression rootContextParameter, ReadOnlyCollection<ParameterExpression> compiledQueryParameters)
		{
			return new Funcletizer(Funcletizer.Mode.CompiledQueryEvaluation, rootContext, rootContextParameter, compiledQueryParameters);
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000B2DDB File Offset: 0x000B0FDB
		internal static Funcletizer CreateCompiledQueryLockdownFuncletizer()
		{
			return new Funcletizer(Funcletizer.Mode.CompiledQueryLockdown, null, null, null);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000B2DE6 File Offset: 0x000B0FE6
		internal static Funcletizer CreateQueryFuncletizer(ObjectContext rootContext)
		{
			return new Funcletizer(Funcletizer.Mode.ConventionalQuery, rootContext, null, null);
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000B2DF1 File Offset: 0x000B0FF1
		internal ObjectContext RootContext
		{
			get
			{
				return this._rootContext;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000B2DF9 File Offset: 0x000B0FF9
		internal ParameterExpression RootContextParameter
		{
			get
			{
				return this._rootContextParameter;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000B2E01 File Offset: 0x000B1001
		internal ConstantExpression RootContextExpression
		{
			get
			{
				return this._rootContextExpression;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000B2E09 File Offset: 0x000B1009
		internal bool IsCompiledQuery
		{
			get
			{
				return this._mode == Funcletizer.Mode.CompiledQueryEvaluation || this._mode == Funcletizer.Mode.CompiledQueryLockdown;
			}
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000B2E20 File Offset: 0x000B1020
		internal Expression Funcletize(Expression expression, out Func<bool> recompileRequired)
		{
			expression = this.ReplaceRootContextParameter(expression);
			Func<Expression, bool> func;
			Func<Expression, bool> func2;
			if (this._mode == Funcletizer.Mode.CompiledQueryEvaluation)
			{
				func = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				func2 = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsCompiledQueryParameterVariable));
			}
			else if (this._mode == Funcletizer.Mode.CompiledQueryLockdown)
			{
				func = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
				func2 = (Expression exp) => false;
			}
			else
			{
				func = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsImmutable));
				func2 = Funcletizer.Nominate(expression, new Func<Expression, bool>(this.IsClosureExpression));
			}
			Funcletizer.FuncletizingVisitor funcletizingVisitor = new Funcletizer.FuncletizingVisitor(this, func, func2);
			Expression expression2 = funcletizingVisitor.Visit(expression);
			recompileRequired = funcletizingVisitor.GetRecompileRequiredFunction();
			return expression2;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000B2EE2 File Offset: 0x000B10E2
		private Expression ReplaceRootContextParameter(Expression expression)
		{
			if (this._rootContextExpression != null)
			{
				return EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
				{
					if (exp != this._rootContextParameter)
					{
						return baseVisit(exp);
					}
					return this._rootContextExpression;
				});
			}
			return expression;
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000B2F00 File Offset: 0x000B1100
		private static Func<Expression, bool> Nominate(Expression expression, Func<Expression, bool> localCriterion)
		{
			HashSet<Expression> candidates = new HashSet<Expression>();
			bool cannotBeNominated = false;
			Func<Expression, Func<Expression, Expression>, Expression> func = delegate(Expression exp, Func<Expression, Expression> baseVisit)
			{
				if (exp != null)
				{
					bool cannotBeNominated2 = cannotBeNominated;
					cannotBeNominated = false;
					baseVisit(exp);
					if (!cannotBeNominated)
					{
						if (localCriterion(exp))
						{
							candidates.Add(exp);
						}
						else
						{
							cannotBeNominated = true;
						}
					}
					cannotBeNominated = cannotBeNominated || cannotBeNominated2;
				}
				return exp;
			};
			EntityExpressionVisitor.Visit(expression, func);
			return new Func<Expression, bool>(candidates.Contains);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000B2F54 File Offset: 0x000B1154
		private bool IsImmutable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType <= ExpressionType.Convert)
			{
				if (nodeType == ExpressionType.Constant)
				{
					return true;
				}
				if (nodeType == ExpressionType.Convert)
				{
					return true;
				}
			}
			else
			{
				if (nodeType == ExpressionType.New)
				{
					PrimitiveType primitiveType;
					return ClrProviderManifest.Instance.TryGetPrimitiveType(TypeSystem.GetNonNullableType(expression.Type), out primitiveType);
				}
				if (nodeType == ExpressionType.NewArrayInit)
				{
					return typeof(byte[]) == expression.Type;
				}
			}
			return false;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000B2FC4 File Offset: 0x000B11C4
		private bool IsClosureExpression(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsImmutable(expression))
			{
				return true;
			}
			if (ExpressionType.MemberAccess == expression.NodeType)
			{
				MemberExpression memberExpression = (MemberExpression)expression;
				return memberExpression.Member.MemberType != MemberTypes.Property || ExpressionConverter.CanFuncletizePropertyInfo((PropertyInfo)memberExpression.Member);
			}
			return false;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000B3018 File Offset: 0x000B1218
		private bool IsCompiledQueryParameterVariable(Expression expression)
		{
			if (expression == null)
			{
				return false;
			}
			if (this.IsClosureExpression(expression))
			{
				return true;
			}
			if (ExpressionType.Parameter == expression.NodeType)
			{
				ParameterExpression parameterExpression = (ParameterExpression)expression;
				return this._compiledQueryParameters.Contains(parameterExpression);
			}
			return false;
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000B3054 File Offset: 0x000B1254
		private bool TryGetTypeUsageForTerminal(Expression expression, out TypeUsage typeUsage)
		{
			Type type = expression.Type;
			if (this._rootContext.Perspective.TryGetTypeByName(TypeSystem.GetNonNullableType(type).FullNameWithNesting(), false, out typeUsage) && TypeSemantics.IsScalarType(typeUsage))
			{
				if (expression.NodeType == ExpressionType.Convert)
				{
					type = ((UnaryExpression)expression).Operand.Type;
				}
				if (type.IsValueType && Nullable.GetUnderlyingType(type) == null && TypeSemantics.IsNullable(typeUsage))
				{
					typeUsage = typeUsage.ShallowCopy(new FacetValues
					{
						Nullable = new bool?(false)
					});
				}
				return true;
			}
			typeUsage = null;
			return false;
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000B30F0 File Offset: 0x000B12F0
		internal string GenerateParameterName()
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text = "{0}{1}";
			object[] array = new object[2];
			array[0] = "p__linq__";
			int num = 1;
			long parameterNumber = this._parameterNumber;
			this._parameterNumber = parameterNumber + 1L;
			array[num] = parameterNumber;
			return string.Format(invariantCulture, text, array);
		}

		// Token: 0x04001200 RID: 4608
		private readonly ParameterExpression _rootContextParameter;

		// Token: 0x04001201 RID: 4609
		private readonly ObjectContext _rootContext;

		// Token: 0x04001202 RID: 4610
		private readonly ConstantExpression _rootContextExpression;

		// Token: 0x04001203 RID: 4611
		private readonly ReadOnlyCollection<ParameterExpression> _compiledQueryParameters;

		// Token: 0x04001204 RID: 4612
		private readonly Funcletizer.Mode _mode;

		// Token: 0x04001205 RID: 4613
		private readonly HashSet<Expression> _linqExpressionStack = new HashSet<Expression>();

		// Token: 0x04001206 RID: 4614
		private const string s_parameterPrefix = "p__linq__";

		// Token: 0x04001207 RID: 4615
		private long _parameterNumber;

		// Token: 0x02000A9F RID: 2719
		private enum Mode
		{
			// Token: 0x04002B32 RID: 11058
			CompiledQueryLockdown,
			// Token: 0x04002B33 RID: 11059
			CompiledQueryEvaluation,
			// Token: 0x04002B34 RID: 11060
			ConventionalQuery
		}

		// Token: 0x02000AA0 RID: 2720
		private sealed class FuncletizingVisitor : EntityExpressionVisitor
		{
			// Token: 0x06006260 RID: 25184 RVA: 0x00155B17 File Offset: 0x00153D17
			internal FuncletizingVisitor(Funcletizer funcletizer, Func<Expression, bool> isClientConstant, Func<Expression, bool> isClientVariable)
			{
				this._funcletizer = funcletizer;
				this._isClientConstant = isClientConstant;
				this._isClientVariable = isClientVariable;
			}

			// Token: 0x06006261 RID: 25185 RVA: 0x00155B3F File Offset: 0x00153D3F
			internal Func<bool> GetRecompileRequiredFunction()
			{
				ReadOnlyCollection<Func<bool>> recompileRequiredDelegates = new ReadOnlyCollection<Func<bool>>(this._recompileRequiredDelegates);
				return () => recompileRequiredDelegates.Any((Func<bool> d) => d());
			}

			// Token: 0x06006262 RID: 25186 RVA: 0x00155B64 File Offset: 0x00153D64
			internal override Expression Visit(Expression exp)
			{
				if (exp != null)
				{
					if (!this._funcletizer._linqExpressionStack.Add(exp))
					{
						throw new InvalidOperationException(Strings.ELinq_CycleDetected);
					}
					try
					{
						if (this._isClientConstant(exp))
						{
							return this.InlineValue(exp, false);
						}
						if (!this._isClientVariable(exp))
						{
							return base.Visit(exp);
						}
						TypeUsage typeUsage;
						if (this._funcletizer.TryGetTypeUsageForTerminal(exp, out typeUsage))
						{
							return new QueryParameterExpression(typeUsage.Parameter(this._funcletizer.GenerateParameterName()), exp, this._funcletizer._compiledQueryParameters);
						}
						if (this._funcletizer.IsCompiledQuery)
						{
							throw Funcletizer.FuncletizingVisitor.InvalidCompiledQueryParameterException(exp);
						}
						return this.InlineValue(exp, true);
					}
					finally
					{
						this._funcletizer._linqExpressionStack.Remove(exp);
					}
				}
				return base.Visit(exp);
			}

			// Token: 0x06006263 RID: 25187 RVA: 0x00155C4C File Offset: 0x00153E4C
			private static NotSupportedException InvalidCompiledQueryParameterException(Expression expression)
			{
				ParameterExpression parameterExpression;
				if (expression.NodeType == ExpressionType.Parameter)
				{
					parameterExpression = (ParameterExpression)expression;
				}
				else
				{
					HashSet<ParameterExpression> parameters = new HashSet<ParameterExpression>();
					EntityExpressionVisitor.Visit(expression, delegate(Expression exp, Func<Expression, Expression> baseVisit)
					{
						if (exp != null && exp.NodeType == ExpressionType.Parameter)
						{
							parameters.Add((ParameterExpression)exp);
						}
						return baseVisit(exp);
					});
					if (parameters.Count != 1)
					{
						return new NotSupportedException(Strings.CompiledELinq_UnsupportedParameterTypes(expression.Type.FullName));
					}
					parameterExpression = parameters.Single<ParameterExpression>();
				}
				if (parameterExpression.Type.Equals(expression.Type))
				{
					return new NotSupportedException(Strings.CompiledELinq_UnsupportedNamedParameterType(parameterExpression.Name, parameterExpression.Type.FullName));
				}
				return new NotSupportedException(Strings.CompiledELinq_UnsupportedNamedParameterUseAsType(parameterExpression.Name, expression.Type.FullName));
			}

			// Token: 0x06006264 RID: 25188 RVA: 0x00155D0A File Offset: 0x00153F0A
			private static Func<object> CompileExpression(Expression expression)
			{
				return Expression.Lambda<Func<object>>(TypeSystem.EnsureType(expression, typeof(object)), new ParameterExpression[0]).Compile();
			}

			// Token: 0x06006265 RID: 25189 RVA: 0x00155D2C File Offset: 0x00153F2C
			private Expression InlineValue(Expression expression, bool recompileOnChange)
			{
				Func<object> func = null;
				object obj = null;
				if (expression.NodeType == ExpressionType.Constant)
				{
					obj = ((ConstantExpression)expression).Value;
				}
				else
				{
					bool flag = false;
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)expression;
						if (!recompileOnChange && unaryExpression.Operand.NodeType == ExpressionType.Constant && typeof(IQueryable).IsAssignableFrom(unaryExpression.Operand.Type))
						{
							obj = ((ConstantExpression)unaryExpression.Operand).Value;
							flag = true;
						}
					}
					if (!flag)
					{
						func = Funcletizer.FuncletizingVisitor.CompileExpression(expression);
						obj = func();
					}
				}
				ObjectQuery objectQuery = (obj as IQueryable).TryGetObjectQuery();
				Expression expression2;
				if (objectQuery != null)
				{
					expression2 = this.InlineObjectQuery(objectQuery, objectQuery.GetType());
				}
				else
				{
					LambdaExpression lambdaExpression = obj as LambdaExpression;
					if (lambdaExpression != null)
					{
						expression2 = this.InlineExpression(Expression.Quote(lambdaExpression));
					}
					else
					{
						expression2 = ((expression.NodeType == ExpressionType.Constant) ? expression : Expression.Constant(obj, expression.Type));
					}
				}
				if (recompileOnChange)
				{
					this.AddRecompileRequiredDelegates(func, obj);
				}
				return expression2;
			}

			// Token: 0x06006266 RID: 25190 RVA: 0x00155E24 File Offset: 0x00154024
			private void AddRecompileRequiredDelegates(Func<object> getValue, object value)
			{
				Funcletizer.FuncletizingVisitor.<>c__DisplayClass10_0 CS$<>8__locals1 = new Funcletizer.FuncletizingVisitor.<>c__DisplayClass10_0();
				CS$<>8__locals1.getValue = getValue;
				CS$<>8__locals1.value = value;
				CS$<>8__locals1.originalQuery = (CS$<>8__locals1.value as IQueryable).TryGetObjectQuery();
				if (CS$<>8__locals1.originalQuery == null)
				{
					if (CS$<>8__locals1.getValue != null)
					{
						this._recompileRequiredDelegates.Add(() => CS$<>8__locals1.value != CS$<>8__locals1.getValue());
					}
					return;
				}
				MergeOption? originalMergeOption = CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption;
				if (CS$<>8__locals1.getValue == null)
				{
					this._recompileRequiredDelegates.Add(delegate
					{
						MergeOption? userSpecifiedMergeOption = CS$<>8__locals1.originalQuery.QueryState.UserSpecifiedMergeOption;
						MergeOption? originalMergeOption3 = originalMergeOption;
						return !((userSpecifiedMergeOption.GetValueOrDefault() == originalMergeOption3.GetValueOrDefault()) & (userSpecifiedMergeOption != null == (originalMergeOption3 != null)));
					});
					return;
				}
				this._recompileRequiredDelegates.Add(delegate
				{
					ObjectQuery objectQuery = (CS$<>8__locals1.getValue() as IQueryable).TryGetObjectQuery();
					if (CS$<>8__locals1.originalQuery == objectQuery)
					{
						MergeOption? userSpecifiedMergeOption2 = objectQuery.QueryState.UserSpecifiedMergeOption;
						MergeOption? originalMergeOption2 = originalMergeOption;
						return !((userSpecifiedMergeOption2.GetValueOrDefault() == originalMergeOption2.GetValueOrDefault()) & (userSpecifiedMergeOption2 != null == (originalMergeOption2 != null)));
					}
					return true;
				});
			}

			// Token: 0x06006267 RID: 25191 RVA: 0x00155EE8 File Offset: 0x001540E8
			private Expression InlineObjectQuery(ObjectQuery inlineQuery, Type expressionType)
			{
				Expression expression;
				if (this._funcletizer._mode == Funcletizer.Mode.CompiledQueryLockdown)
				{
					expression = Expression.Constant(inlineQuery, expressionType);
				}
				else
				{
					if (this._funcletizer._rootContext != inlineQuery.QueryState.ObjectContext)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedDifferentContexts);
					}
					expression = inlineQuery.GetExpression();
					if (!(inlineQuery.QueryState is EntitySqlQueryState))
					{
						expression = this.InlineExpression(expression);
					}
					expression = TypeSystem.EnsureType(expression, expressionType);
				}
				return expression;
			}

			// Token: 0x06006268 RID: 25192 RVA: 0x00155F54 File Offset: 0x00154154
			private Expression InlineExpression(Expression exp)
			{
				Func<bool> func;
				exp = this._funcletizer.Funcletize(exp, out func);
				if (!this._funcletizer.IsCompiledQuery)
				{
					this._recompileRequiredDelegates.Add(func);
				}
				return exp;
			}

			// Token: 0x04002B35 RID: 11061
			private readonly Funcletizer _funcletizer;

			// Token: 0x04002B36 RID: 11062
			private readonly Func<Expression, bool> _isClientConstant;

			// Token: 0x04002B37 RID: 11063
			private readonly Func<Expression, bool> _isClientVariable;

			// Token: 0x04002B38 RID: 11064
			private readonly List<Func<bool>> _recompileRequiredDelegates = new List<Func<bool>>();
		}
	}
}
