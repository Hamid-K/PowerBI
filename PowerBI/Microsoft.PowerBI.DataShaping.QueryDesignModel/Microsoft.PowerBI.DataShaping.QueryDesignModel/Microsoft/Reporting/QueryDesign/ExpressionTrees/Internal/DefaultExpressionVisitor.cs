using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200015A RID: 346
	internal class DefaultExpressionVisitor : QueryExpressionVisitor<QueryExpression>
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x00037EAB File Offset: 0x000360AB
		protected DefaultExpressionVisitor()
		{
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00037EC9 File Offset: 0x000360C9
		protected virtual void OnExpressionReplaced(QueryExpression oldExpression, QueryExpression newExpression)
		{
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00037ECB File Offset: 0x000360CB
		protected virtual void OnVariableRebound(QueryVariableReferenceExpression fromVarRef, QueryVariableReferenceExpression toVarRef)
		{
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00037ECD File Offset: 0x000360CD
		protected virtual void OnEnterScope(IEnumerable<QueryVariableReferenceExpression> scopeVariables)
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00037ECF File Offset: 0x000360CF
		protected virtual void OnExitScope()
		{
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00037ED4 File Offset: 0x000360D4
		protected virtual QueryExpression VisitExpression(QueryExpression expression)
		{
			QueryExpression queryExpression = null;
			if (expression != null)
			{
				queryExpression = expression.Accept<QueryExpression>(this);
			}
			return queryExpression;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00037EEF File Offset: 0x000360EF
		protected T VisitGenericExpression<T>(T expression) where T : QueryExpression
		{
			return (T)((object)this.VisitExpression(expression));
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00037F02 File Offset: 0x00036102
		protected IReadOnlyList<KeyValuePair<string, T>> VisitGenericExpressionList<T>(IReadOnlyList<KeyValuePair<string, T>> list) where T : QueryExpression
		{
			return this.VisitList<string, T>(list, new Func<T, T>(this.VisitGenericExpression<T>));
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00037F17 File Offset: 0x00036117
		protected IReadOnlyList<QueryExpression> VisitExpressionList(IReadOnlyList<QueryExpression> list)
		{
			return Microsoft.Reporting.Util.VisitReadOnlyList<QueryExpression>(list, new Func<QueryExpression, QueryExpression>(this.VisitExpression));
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00037F2C File Offset: 0x0003612C
		protected IReadOnlyList<IReadOnlyList<QueryExpression>> VisitExpressionList(IReadOnlyList<IReadOnlyList<QueryExpression>> list)
		{
			return Microsoft.Reporting.Util.VisitReadOnlyList<IReadOnlyList<QueryExpression>>(list, new Func<IReadOnlyList<QueryExpression>, IReadOnlyList<QueryExpression>>(this.VisitExpressionList));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00037F40 File Offset: 0x00036140
		protected IReadOnlyList<KeyValuePair<string, QueryExpression>> VisitExpressionList(IReadOnlyList<KeyValuePair<string, QueryExpression>> list)
		{
			return this.VisitList<string, QueryExpression>(list, new Func<QueryExpression, QueryExpression>(this.VisitExpression));
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00037F58 File Offset: 0x00036158
		protected virtual QueryExpressionBinding VisitExpressionBinding(QueryExpressionBinding binding)
		{
			QueryExpressionBinding queryExpressionBinding = binding;
			if (binding != null)
			{
				QueryExpression queryExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != queryExpression)
				{
					queryExpressionBinding = queryExpression.BindAs(binding.Variable.VariableName);
					this.RebindVariable(binding.Variable, queryExpressionBinding.Variable);
				}
			}
			return queryExpressionBinding;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00037FA5 File Offset: 0x000361A5
		protected virtual IReadOnlyList<QueryExpressionBinding> VisitExpressionBindingList(IReadOnlyList<QueryExpressionBinding> list)
		{
			return Microsoft.Reporting.Util.VisitReadOnlyList<QueryExpressionBinding>(list, new Func<QueryExpressionBinding, QueryExpressionBinding>(this.VisitExpressionBinding));
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00037FBC File Offset: 0x000361BC
		protected virtual QueryGroupExpressionBinding VisitGroupExpressionBinding(QueryGroupExpressionBinding binding)
		{
			QueryGroupExpressionBinding queryGroupExpressionBinding = binding;
			if (binding != null)
			{
				QueryExpression queryExpression = this.VisitExpression(binding.Expression);
				if (binding.Expression != queryExpression)
				{
					queryGroupExpressionBinding = queryExpression.GroupBindAs(binding.Variable.VariableName);
					this.RebindVariable(binding.Variable, queryGroupExpressionBinding.Variable);
				}
			}
			return queryGroupExpressionBinding;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00038009 File Offset: 0x00036209
		protected virtual QuerySortClause VisitSortClause(QuerySortClause clause)
		{
			return DefaultExpressionVisitor.VisitSortClause(clause, new Func<QueryExpression, QueryExpression>(this.VisitExpression));
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00038020 File Offset: 0x00036220
		internal static QuerySortClause VisitSortClause(QuerySortClause clause, Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			QuerySortClause querySortClause = clause;
			if (clause != null)
			{
				QueryExpression queryExpression = rewriteExpression(clause.Expression);
				if (clause.Expression != queryExpression)
				{
					querySortClause = queryExpression.ToSortClause(clause.Direction);
				}
			}
			return querySortClause;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00038056 File Offset: 0x00036256
		protected virtual IList<QuerySortClause> VisitSortOrder(IList<QuerySortClause> sortOrder)
		{
			return Microsoft.Reporting.Util.VisitList<QuerySortClause>(sortOrder, new Func<QuerySortClause, QuerySortClause>(this.VisitSortClause));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003806B File Offset: 0x0003626B
		protected virtual EdmType VisitType(EdmType type)
		{
			return type;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003806E File Offset: 0x0003626E
		protected virtual ConceptualResultType VisitConceptualResultType(ConceptualResultType type)
		{
			return type;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00038071 File Offset: 0x00036271
		protected virtual EntitySet VisitEntitySet(EntitySet entitySet)
		{
			return entitySet;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00038074 File Offset: 0x00036274
		protected virtual IConceptualEntity VisitEntity(IConceptualEntity entity)
		{
			return entity;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00038077 File Offset: 0x00036277
		protected virtual EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			return functionMetadata;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0003807A File Offset: 0x0003627A
		protected virtual EdmOperator VisitOperator(EdmOperator operatorMetadata)
		{
			return operatorMetadata;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0003807D File Offset: 0x0003627D
		private void NotifyIfChanged(QueryExpression originalExpression, QueryExpression newExpression)
		{
			if (originalExpression != newExpression)
			{
				this.OnExpressionReplaced(originalExpression, newExpression);
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0003808C File Offset: 0x0003628C
		private IReadOnlyList<KeyValuePair<TKey, TElement>> VisitList<TKey, TElement>(IReadOnlyList<KeyValuePair<TKey, TElement>> list, Func<TElement, TElement> map)
		{
			IReadOnlyList<KeyValuePair<TKey, TElement>> readOnlyList = list;
			if (list != null)
			{
				List<KeyValuePair<TKey, TElement>> list2 = null;
				for (int i = 0; i < list.Count; i++)
				{
					TElement telement = map(list[i].Value);
					if (list[i].Value != telement)
					{
						if (list2 == null)
						{
							list2 = new List<KeyValuePair<TKey, TElement>>(list);
							readOnlyList = list2;
						}
						list2[i] = new KeyValuePair<TKey, TElement>(list[i].Key, telement);
					}
				}
			}
			return readOnlyList;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00038110 File Offset: 0x00036310
		private QueryExpression VisitUnary(QueryUnaryExpression expression, Func<QueryExpression, QueryExpression> callback)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Argument);
			if (expression.Argument != queryExpression2)
			{
				queryExpression = callback(queryExpression2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00038148 File Offset: 0x00036348
		private QueryExpression VisitBinary(QueryBinaryExpression expression, Func<QueryExpression, QueryExpression, QueryExpression> callback)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Left);
			QueryExpression queryExpression3 = this.VisitExpression(expression.Right);
			if (expression.Left != queryExpression2 || expression.Right != queryExpression3)
			{
				queryExpression = callback(queryExpression2, queryExpression3);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00038198 File Offset: 0x00036398
		private T VisitTerminal<T>(T expression, Func<ConceptualResultType, T> reconstructor) where T : QueryExpression
		{
			T t = expression;
			ConceptualResultType conceptualResultType = this.VisitConceptualResultType(expression.ConceptualResultType);
			if (expression.ConceptualResultType != conceptualResultType)
			{
				t = reconstructor(conceptualResultType);
			}
			this.NotifyIfChanged(expression, t);
			return t;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000381E4 File Offset: 0x000363E4
		private void RebindVariable(QueryVariableReferenceExpression from, QueryVariableReferenceExpression to)
		{
			if (!from.VariableName.Equals(to.VariableName, EdmItem.IdentityComparison) || from.ConceptualResultType != to.ConceptualResultType || !from.ConceptualResultType.Equals(to.ConceptualResultType))
			{
				this.varMappings[from] = to;
				this.OnVariableRebound(from, to);
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00038240 File Offset: 0x00036440
		private QueryExpressionBinding VisitExpressionBindingEnterScope(QueryExpressionBinding binding)
		{
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBinding(binding);
			this.OnEnterScope(new QueryVariableReferenceExpression[] { queryExpressionBinding.Variable });
			return queryExpressionBinding;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0003826B File Offset: 0x0003646B
		private void EnterScope(params QueryVariableReferenceExpression[] scopeVars)
		{
			this.OnEnterScope(scopeVars);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00038274 File Offset: 0x00036474
		private void ExitScope()
		{
			this.OnExitScope();
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003827C File Offset: 0x0003647C
		protected internal override QueryExpression Visit(QueryExtensionExpression expression)
		{
			throw new NotSupportedException(DevErrors.DefaultExpressionVisitor.UnsupportedExpression(expression.GetType().FullName));
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00038294 File Offset: 0x00036494
		protected internal override QueryExpression Visit(QueryLiteralExpression expression)
		{
			return this.VisitTerminal<QueryLiteralExpression>(expression, (ConceptualResultType newConceptualType) => newConceptualType.Literal(expression.Value));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000382C6 File Offset: 0x000364C6
		protected internal override QueryExpression Visit(QueryNullExpression expression)
		{
			ArgumentValidation.CheckNotNull<QueryNullExpression>(expression, "expression");
			Func<ConceptualResultType, QueryNullExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<0>__Null) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<0>__Null = new Func<ConceptualResultType, QueryNullExpression>(QueryExpressionBuilder.Null));
			}
			return this.VisitTerminal<QueryNullExpression>(expression, func);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000382F8 File Offset: 0x000364F8
		protected internal override QueryExpression Visit(QueryTypeCastExpression expression)
		{
			ArgumentValidation.CheckNotNull<QueryTypeCastExpression>(expression, "expression");
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Input);
			if (expression.Input != queryExpression2)
			{
				queryExpression = queryExpression2.Cast(expression.ConceptualResultType);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00038340 File Offset: 0x00036540
		protected internal override QueryExpression Visit(QueryVariableReferenceExpression expression)
		{
			ArgumentValidation.CheckNotNull<QueryVariableReferenceExpression>(expression, "expression");
			QueryExpression queryExpression = expression;
			QueryVariableReferenceExpression queryVariableReferenceExpression;
			if (this.varMappings.TryGetValue(expression, out queryVariableReferenceExpression))
			{
				queryExpression = queryVariableReferenceExpression;
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00038378 File Offset: 0x00036578
		protected internal override QueryExpression Visit(QueryFormatExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = expression.Input.Accept<QueryExpression>(this);
			if (expression.Input != queryExpression2)
			{
				queryExpression = queryExpression2.Format(expression.FormatString, expression.Locale);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000383BC File Offset: 0x000365BC
		protected internal override QueryExpression Visit(QueryFunctionExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Arguments);
			EdmFunction edmFunction = this.VisitFunction(expression.Function);
			if (expression.Arguments != readOnlyList || expression.Function != edmFunction)
			{
				queryExpression = edmFunction.Invoke(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00038408 File Offset: 0x00036608
		protected internal override QueryExpression Visit(QueryOperatorExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Arguments);
			EdmOperator edmOperator = this.VisitOperator(expression.Operator);
			if (expression.Arguments != readOnlyList || expression.Operator != edmOperator)
			{
				queryExpression = edmOperator.InvokeOperator(readOnlyList, expression.UseBinaryEquivalent);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0003845C File Offset: 0x0003665C
		protected internal override QueryExpression Visit(QueryExtensionFunctionExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Arguments);
			if (expression.Arguments != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.InvokeExtensionFunction(expression.ConceptualResultType, expression.FunctionName, readOnlyList, expression.ResultColumnLineage);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000384A4 File Offset: 0x000366A4
		protected internal override QueryExpression Visit(QueryFieldExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Instance);
			if (expression.Instance != queryExpression2)
			{
				queryExpression = queryExpression2.Field(expression.Column.EdmName);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000384E4 File Offset: 0x000366E4
		protected internal override QueryExpression Visit(QueryFieldReferenceNameExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Table);
			if (expression.Table != queryExpression2)
			{
				queryExpression = queryExpression2.FieldReferenceName(expression.InternalFieldName);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00038520 File Offset: 0x00036720
		protected internal override QueryExpression Visit(QueryRelatedColumnExpression expression)
		{
			return this.VisitTerminal<QueryRelatedColumnExpression>(expression, (ConceptualResultType newConceptualType) => QueryExpressionBuilder.RelatedColumn(expression.Field, expression.Column));
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00038554 File Offset: 0x00036754
		protected internal override QueryExpression Visit(QueryScalarEntityReferenceExpression expression)
		{
			QueryExpression queryExpression = expression;
			EntitySet entitySet = this.VisitEntitySet(expression.Target);
			IConceptualEntity conceptualEntity = this.VisitEntity(expression.TargetEntity);
			if (expression.Target != entitySet || expression.TargetEntity != conceptualEntity)
			{
				queryExpression = entitySet.ScalarEntity(conceptualEntity);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000385A0 File Offset: 0x000367A0
		protected internal override QueryExpression Visit(QueryComparisonExpression expression)
		{
			switch (expression.ComparisonKind)
			{
			case QueryComparisonKind.Equals:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func;
				if ((func = DefaultExpressionVisitor.<>O.<1>__Equal) == null)
				{
					func = (DefaultExpressionVisitor.<>O.<1>__Equal = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.Equal));
				}
				return this.VisitBinary(expression, func);
			}
			case QueryComparisonKind.NotEquals:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func2;
				if ((func2 = DefaultExpressionVisitor.<>O.<2>__NotEqual) == null)
				{
					func2 = (DefaultExpressionVisitor.<>O.<2>__NotEqual = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.NotEqual));
				}
				return this.VisitBinary(expression, func2);
			}
			case QueryComparisonKind.GreaterThan:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func3;
				if ((func3 = DefaultExpressionVisitor.<>O.<3>__GreaterThan) == null)
				{
					func3 = (DefaultExpressionVisitor.<>O.<3>__GreaterThan = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.GreaterThan));
				}
				return this.VisitBinary(expression, func3);
			}
			case QueryComparisonKind.GreaterThanOrEquals:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func4;
				if ((func4 = DefaultExpressionVisitor.<>O.<4>__GreaterThanOrEqual) == null)
				{
					func4 = (DefaultExpressionVisitor.<>O.<4>__GreaterThanOrEqual = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.GreaterThanOrEqual));
				}
				return this.VisitBinary(expression, func4);
			}
			case QueryComparisonKind.LessThan:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func5;
				if ((func5 = DefaultExpressionVisitor.<>O.<5>__LessThan) == null)
				{
					func5 = (DefaultExpressionVisitor.<>O.<5>__LessThan = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.LessThan));
				}
				return this.VisitBinary(expression, func5);
			}
			case QueryComparisonKind.LessThanOrEquals:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func6;
				if ((func6 = DefaultExpressionVisitor.<>O.<6>__LessThanOrEqual) == null)
				{
					func6 = (DefaultExpressionVisitor.<>O.<6>__LessThanOrEqual = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.LessThanOrEqual));
				}
				return this.VisitBinary(expression, func6);
			}
			case QueryComparisonKind.EqualsIdentity:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func7;
				if ((func7 = DefaultExpressionVisitor.<>O.<7>__EqualIdentity) == null)
				{
					func7 = (DefaultExpressionVisitor.<>O.<7>__EqualIdentity = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.EqualIdentity));
				}
				return this.VisitBinary(expression, func7);
			}
			case QueryComparisonKind.NotEqualsIdentity:
			{
				Func<QueryExpression, QueryExpression, QueryExpression> func8;
				if ((func8 = DefaultExpressionVisitor.<>O.<8>__NotEqualIdentity) == null)
				{
					func8 = (DefaultExpressionVisitor.<>O.<8>__NotEqualIdentity = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryExpressionBuilder.NotEqualIdentity));
				}
				return this.VisitBinary(expression, func8);
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000386FC File Offset: 0x000368FC
		protected internal override QueryExpression Visit(QueryNewInstanceExpression expression)
		{
			QueryExpression queryExpression = expression;
			ConceptualResultType conceptualResultType = this.VisitConceptualResultType(expression.ConceptualResultType);
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(expression.Arguments);
			if (expression.ConceptualResultType != conceptualResultType || expression.Arguments != readOnlyList)
			{
				queryExpression = conceptualResultType.New(readOnlyList.Select((KeyValuePair<string, QueryExpression> a) => a.Value).ToArray<QueryExpression>());
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00038778 File Offset: 0x00036978
		protected internal override QueryExpression Visit(QueryNewTableExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(expression.Columns);
			if (expression.Columns != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.NewTable(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x000387AD File Offset: 0x000369AD
		protected internal override QueryExpression Visit(QueryNonVisualExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<9>__NonVisual) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<9>__NonVisual = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.NonVisual));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x000387D4 File Offset: 0x000369D4
		protected internal override QueryExpression Visit(QueryScanExpression expression)
		{
			QueryExpression queryExpression = expression;
			EntitySet entitySet = this.VisitEntitySet(expression.Target);
			IConceptualEntity conceptualEntity = this.VisitEntity(expression.TargetEntity);
			if (expression.Target != entitySet || expression.TargetEntity != conceptualEntity)
			{
				queryExpression = ((conceptualEntity != null) ? conceptualEntity.Scan(false) : null) ?? entitySet.Scan(false);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00038834 File Offset: 0x00036A34
		protected internal override QueryExpression Visit(QueryFilterExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression2 = this.VisitExpression(expression.Predicate);
			this.ExitScope();
			if (expression.Input != queryExpressionBinding || expression.Predicate != queryExpression2)
			{
				queryExpression = queryExpressionBinding.Filter(queryExpression2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00038888 File Offset: 0x00036A88
		protected internal override QueryExpression Visit(QueryProjectExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression2 = this.VisitExpression(expression.Projection);
			this.ExitScope();
			if (expression.Input != queryExpressionBinding || expression.Projection != queryExpression2)
			{
				queryExpression = queryExpressionBinding.Project(queryExpression2, expression.ProjectSubsetStrategy);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x000388E0 File Offset: 0x00036AE0
		protected internal override QueryExpression Visit(QueryEnsureUniqueUnqualifiedNamesExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Table);
			if (expression.Table != queryExpression2)
			{
				queryExpression = queryExpression2.EnsureUniqueUnqualifiedNames(expression.ForceRename);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0003891C File Offset: 0x00036B1C
		protected internal override QueryExpression Visit(QueryCrossJoinExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpressionBinding> readOnlyList = this.VisitExpressionBindingList(expression.Inputs);
			if (expression.Inputs != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.CrossJoin(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00038954 File Offset: 0x00036B54
		protected internal override QueryExpression Visit(QueryNaturalJoinExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBinding(expression.Left);
			QueryExpressionBinding queryExpressionBinding2 = this.VisitExpressionBinding(expression.Right);
			if (expression.Left != queryExpressionBinding || expression.Right != queryExpressionBinding2)
			{
				queryExpression = QueryExpressionBuilder.NaturalJoin(expression.JoinKind, queryExpressionBinding, queryExpressionBinding2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x000389A8 File Offset: 0x00036BA8
		protected internal override QueryExpression Visit(QueryImplicitJoinWithProjectionExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBinding(expression.PrimaryTable);
			IReadOnlyList<ImplicitJoinSecondaryTable> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<ImplicitJoinSecondaryTable>(expression.SecondaryTables, new Func<ImplicitJoinSecondaryTable, ImplicitJoinSecondaryTable>(this.VisitImplicitJoinSecondaryTable));
			if (expression.PrimaryTable != queryExpressionBinding || expression.SecondaryTables != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.ImplicitJoinWithProjection(queryExpressionBinding, readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00038A00 File Offset: 0x00036C00
		private ImplicitJoinSecondaryTable VisitImplicitJoinSecondaryTable(ImplicitJoinSecondaryTable secondaryTable)
		{
			ImplicitJoinSecondaryTable implicitJoinSecondaryTable = secondaryTable;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBinding(secondaryTable.Table);
			IReadOnlyList<KeyValuePair<string, QueryFieldExpression>> readOnlyList = this.VisitGenericExpressionList<QueryFieldExpression>(secondaryTable.KeyColumns);
			if (secondaryTable.Table != queryExpressionBinding || secondaryTable.KeyColumns != readOnlyList)
			{
				implicitJoinSecondaryTable = new ImplicitJoinSecondaryTable(queryExpressionBinding, readOnlyList);
			}
			return implicitJoinSecondaryTable;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00038A44 File Offset: 0x00036C44
		protected internal override QueryExpression Visit(QueryUnionAllExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Tables);
			if (expression.Tables != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.UnionAll(readOnlyList, TypeUnionBehavior.Upcast);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00038A7C File Offset: 0x00036C7C
		protected internal override QueryExpression Visit(QuerySubstituteWithIndexExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBinding(expression.Table);
			QueryExpressionBinding queryExpressionBinding2 = this.VisitExpressionBindingEnterScope(expression.IndexTable);
			IList<QuerySortClause> list = this.VisitSortOrder(expression.IndexTableSortOrder);
			this.ExitScope();
			if (expression.Table != queryExpressionBinding || expression.IndexTable != queryExpressionBinding2 || expression.IndexTableSortOrder != list)
			{
				queryExpression = queryExpressionBinding.SubstituteWithIndex(expression.IndexColumnName, queryExpressionBinding2, list);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00038AEC File Offset: 0x00036CEC
		protected internal override QueryExpression Visit(QueryAddMissingItemsExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Table);
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.ShowAllColumns);
			IList<IAddMissingItemsGroupItem> list = Microsoft.Reporting.Util.VisitList<IAddMissingItemsGroupItem>(expression.Groups, new Func<IAddMissingItemsGroupItem, IAddMissingItemsGroupItem>(this.VisitAddMissingItemsGroupItem));
			this.ExitScope();
			IList<QueryExpression> list2 = Microsoft.Reporting.Util.VisitList<QueryExpression>(expression.ContextTables, new Func<QueryExpression, QueryExpression>(this.VisitExpression));
			if (expression.Table != queryExpressionBinding || expression.ShowAllColumns != readOnlyList || expression.Groups != list || expression.ContextTables != list2)
			{
				queryExpression = QueryExpressionBuilder.AddMissingItems(readOnlyList, queryExpressionBinding, list, list2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00038B88 File Offset: 0x00036D88
		private IAddMissingItemsGroupItem VisitAddMissingItemsGroupItem(IAddMissingItemsGroupItem item)
		{
			AddMissingItemsRollup addMissingItemsRollup = item as AddMissingItemsRollup;
			if (addMissingItemsRollup != null)
			{
				return this.Visit(addMissingItemsRollup);
			}
			AddMissingItemsGroupWithSubtotal addMissingItemsGroupWithSubtotal = item as AddMissingItemsGroupWithSubtotal;
			if (addMissingItemsGroupWithSubtotal != null)
			{
				return this.Visit(addMissingItemsGroupWithSubtotal);
			}
			AddMissingItemsGroup addMissingItemsGroup = item as AddMissingItemsGroup;
			if (addMissingItemsGroup != null)
			{
				return this.Visit(addMissingItemsGroup);
			}
			throw new NotSupportedException(DevErrors.DefaultExpressionVisitor.UnsupportedAddMissingItemsGroupItem(item.GetType().FullName));
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00038BE0 File Offset: 0x00036DE0
		private AddMissingItemsRollup Visit(AddMissingItemsRollup item)
		{
			IList<AddMissingItemsGroupWithSubtotal> list = Microsoft.Reporting.Util.VisitList<AddMissingItemsGroupWithSubtotal>(item.Groups, new Func<AddMissingItemsGroupWithSubtotal, AddMissingItemsGroupWithSubtotal>(this.Visit));
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(item.ContextTables);
			if (list != item.Groups || readOnlyList != item.ContextTables)
			{
				return new AddMissingItemsRollup(list, readOnlyList);
			}
			return item;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00038C30 File Offset: 0x00036E30
		private AddMissingItemsGroupWithSubtotal Visit(AddMissingItemsGroupWithSubtotal item)
		{
			AddMissingItemsGroup addMissingItemsGroup = this.Visit(item.Group);
			QueryExpression queryExpression = this.VisitExpression(item.SubtotalIndicator);
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(item.ContextTables);
			if (addMissingItemsGroup != item.Group || queryExpression != item.SubtotalIndicator || readOnlyList != item.ContextTables)
			{
				return new AddMissingItemsGroupWithSubtotal(addMissingItemsGroup, queryExpression, readOnlyList);
			}
			return item;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00038C8C File Offset: 0x00036E8C
		private AddMissingItemsGroup Visit(AddMissingItemsGroup item)
		{
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(item.Keys);
			if (readOnlyList != item.Keys)
			{
				return new AddMissingItemsGroup(readOnlyList);
			}
			return item;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00038CB8 File Offset: 0x00036EB8
		protected internal override QueryExpression Visit(QueryGenerateExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpressionBinding> readOnlyList = this.VisitExpressionBindingList(expression.Inputs);
			this.ExitScope();
			if (expression.Inputs != readOnlyList)
			{
				QueryGenerateKind generateKind = expression.GenerateKind;
				if (generateKind != QueryGenerateKind.Generate)
				{
					if (generateKind != QueryGenerateKind.GenerateAll)
					{
						Microsoft.DataShaping.Contract.RetailFail("QueryGenerateExpression had GenerateKind other than Generate or GenerateAll?");
					}
					else
					{
						queryExpression = QueryExpressionBuilder.GenerateAll(readOnlyList);
					}
				}
				else
				{
					queryExpression = QueryExpressionBuilder.Generate(readOnlyList);
				}
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00038D18 File Offset: 0x00036F18
		protected internal override QueryExpression Visit(QueryBatchRootExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryParameterDeclarationExpression> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryParameterDeclarationExpression>(expression.QueryParameters, new Func<QueryParameterDeclarationExpression, QueryParameterDeclarationExpression>(this.VisitQueryParameterDeclaration));
			IReadOnlyList<QueryBaseDeclarationExpression> readOnlyList2 = Microsoft.Reporting.Util.VisitReadOnlyList<QueryBaseDeclarationExpression>(expression.Declarations, new Func<QueryBaseDeclarationExpression, QueryBaseDeclarationExpression>(this.VisitBaseDeclaration));
			IReadOnlyList<QueryExpression> readOnlyList3 = this.VisitExpressionList(expression.OutputTables);
			for (int i = 0; i < readOnlyList2.Count; i++)
			{
				if (readOnlyList2[i] is QueryVariableDeclarationExpression)
				{
					this.ExitScope();
				}
			}
			if (expression.QueryParameters != readOnlyList || expression.Declarations != readOnlyList2 || expression.OutputTables != readOnlyList3)
			{
				queryExpression = QueryExpressionBuilder.BatchRoot(readOnlyList, readOnlyList2, readOnlyList3);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00038DBB File Offset: 0x00036FBB
		private QueryParameterDeclarationExpression VisitQueryParameterDeclaration(QueryParameterDeclarationExpression declaration)
		{
			return (QueryParameterDeclarationExpression)this.VisitExpression(declaration);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00038DCC File Offset: 0x00036FCC
		private QueryBaseDeclarationExpression VisitBaseDeclaration(QueryBaseDeclarationExpression declaration)
		{
			QueryBaseDeclarationExpression queryBaseDeclarationExpression = (QueryBaseDeclarationExpression)this.VisitExpression(declaration);
			QueryVariableDeclarationExpression queryVariableDeclarationExpression = queryBaseDeclarationExpression as QueryVariableDeclarationExpression;
			if (queryVariableDeclarationExpression != null)
			{
				this.OnEnterScope(new QueryVariableReferenceExpression[] { queryVariableDeclarationExpression.Variable });
			}
			return queryBaseDeclarationExpression;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00038E08 File Offset: 0x00037008
		private QueryVariableDeclarationExpression VisitVariableDeclarationEnterScope(QueryVariableDeclarationExpression declaration)
		{
			QueryVariableDeclarationExpression queryVariableDeclarationExpression = (QueryVariableDeclarationExpression)declaration.Accept<QueryExpression>(this);
			this.OnEnterScope(new QueryVariableReferenceExpression[] { queryVariableDeclarationExpression.Variable });
			return queryVariableDeclarationExpression;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00038E38 File Offset: 0x00037038
		protected internal override QueryExpression Visit(QueryGroupByExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryGroupExpressionBinding queryGroupExpressionBinding = this.VisitGroupExpressionBinding(expression.Input);
			this.EnterScope(new QueryVariableReferenceExpression[] { queryGroupExpressionBinding.Variable });
			IList<IGroupItem> list = Microsoft.Reporting.Util.VisitList<IGroupItem>(expression.GroupItems, new Func<IGroupItem, IGroupItem>(this.VisitGroupItem));
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(expression.Aggregates);
			this.ExitScope();
			if (expression.Input != queryGroupExpressionBinding || expression.GroupItems != list || expression.Aggregates != readOnlyList)
			{
				queryExpression = queryGroupExpressionBinding.GroupBy(list, readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00038EC4 File Offset: 0x000370C4
		private NamedRollupGroupItem VisitNamedRollupGroupItem(NamedRollupGroupItem item)
		{
			CompositeKeyGroupItem compositeKeyGroupItem = this.VisitCompositeKeyGroupItem(item.GroupKeysItem);
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(item.ContextTables);
			if (item.GroupKeysItem != compositeKeyGroupItem || item.ContextTables != readOnlyList)
			{
				return new NamedRollupGroupItem(compositeKeyGroupItem, item.SubtotalIndicatorColumnName, readOnlyList);
			}
			return item;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00038F0C File Offset: 0x0003710C
		private IGroupItem VisitGroupItem(IGroupItem item)
		{
			CompositeKeyGroupItem compositeKeyGroupItem = item as CompositeKeyGroupItem;
			if (compositeKeyGroupItem != null)
			{
				return this.VisitCompositeKeyGroupItem(compositeKeyGroupItem);
			}
			RollupGroupItem rollupGroupItem = item as RollupGroupItem;
			if (rollupGroupItem != null)
			{
				IList<CompositeKeyGroupItem> list = Microsoft.Reporting.Util.VisitList<CompositeKeyGroupItem>(rollupGroupItem.GroupItems, new Func<CompositeKeyGroupItem, CompositeKeyGroupItem>(this.VisitCompositeKeyGroupItem));
				if (rollupGroupItem.GroupItems != list)
				{
					return new RollupGroupItem(list);
				}
				return rollupGroupItem;
			}
			else
			{
				NamedRollupGroupItem namedRollupGroupItem = item as NamedRollupGroupItem;
				if (namedRollupGroupItem != null)
				{
					return this.VisitNamedRollupGroupItem(namedRollupGroupItem);
				}
				RollupAddIsSubtotalGroupItem rollupAddIsSubtotalGroupItem = item as RollupAddIsSubtotalGroupItem;
				if (rollupAddIsSubtotalGroupItem == null)
				{
					throw new NotSupportedException(DevErrors.DefaultExpressionVisitor.UnsupportedGroupItem(item.GetType().FullName));
				}
				IReadOnlyList<NamedRollupGroupItem> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<NamedRollupGroupItem>(rollupAddIsSubtotalGroupItem.GroupItems, new Func<NamedRollupGroupItem, NamedRollupGroupItem>(this.VisitNamedRollupGroupItem));
				IReadOnlyList<QueryExpression> readOnlyList2 = this.VisitExpressionList(rollupAddIsSubtotalGroupItem.ContextTables);
				if (rollupAddIsSubtotalGroupItem.GroupItems != readOnlyList || rollupAddIsSubtotalGroupItem.ContextTables != readOnlyList2)
				{
					return new RollupAddIsSubtotalGroupItem(readOnlyList, readOnlyList2);
				}
				return rollupAddIsSubtotalGroupItem;
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00038FDC File Offset: 0x000371DC
		private QueryGroupAndJoinAdditionalColumn VisitQueryGroupAndJoinAdditionalColumn(QueryGroupAndJoinAdditionalColumn column)
		{
			QueryExpression queryExpression = this.VisitExpression(column.Expression);
			if (column.Expression != queryExpression)
			{
				return new QueryGroupAndJoinAdditionalColumn(column.Name, queryExpression, column.SuppressJoinPredicate);
			}
			return column;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00039014 File Offset: 0x00037214
		private CompositeKeyGroupItem VisitCompositeKeyGroupItem(CompositeKeyGroupItem item)
		{
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(item.Keys);
			if (item.Keys != readOnlyList)
			{
				return new CompositeKeyGroupItem(readOnlyList);
			}
			return item;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00039040 File Offset: 0x00037240
		protected internal override QueryExpression Visit(QueryGroupAndJoinExpression expression)
		{
			QueryExpression queryExpression = expression;
			IList<IGroupItem> list = Microsoft.Reporting.Util.VisitList<IGroupItem>(expression.GroupItems, new Func<IGroupItem, IGroupItem>(this.VisitGroupItem));
			IList<QueryGroupAndJoinAdditionalColumn> list2 = Microsoft.Reporting.Util.VisitList<QueryGroupAndJoinAdditionalColumn>(expression.AdditionalColumns, new Func<QueryGroupAndJoinAdditionalColumn, QueryGroupAndJoinAdditionalColumn>(this.VisitQueryGroupAndJoinAdditionalColumn));
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.ContextTables);
			if (expression.GroupItems != list || expression.AdditionalColumns != list2 || expression.ContextTables != readOnlyList)
			{
				queryExpression = QueryExpressionBuilder.GroupAndJoin(list, list2, readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000390BC File Offset: 0x000372BC
		protected internal override QueryExpression Visit(QueryCurrentGroupExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryVariableReferenceExpression queryVariableReferenceExpression = (QueryVariableReferenceExpression)this.VisitExpression(expression.Input);
			if (expression.Input != queryVariableReferenceExpression)
			{
				queryExpression = new QueryCurrentGroupExpression(expression.ConceptualResultType, queryVariableReferenceExpression);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000390FC File Offset: 0x000372FC
		protected internal override QueryExpression Visit(QuerySortExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			IList<QuerySortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			if (expression.Input != queryExpressionBinding || expression.SortOrder != list)
			{
				queryExpression = queryExpressionBinding.Sort(list);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00039150 File Offset: 0x00037350
		protected internal override QueryExpression Visit(QueryStartAtExpression expression)
		{
			QueryStartAtExpression queryStartAtExpression = expression;
			QuerySortExpression querySortExpression = ArgumentValidation.CheckAs<QuerySortExpression>(this.VisitExpression(expression.OrderBy), "orderBy");
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Values);
			if (expression.OrderBy != querySortExpression || expression.Values != readOnlyList)
			{
				queryStartAtExpression = querySortExpression.StartAt(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryStartAtExpression);
			return queryStartAtExpression;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000391A8 File Offset: 0x000373A8
		protected internal override QueryExpression Visit(QueryParameterReferenceExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryParameterReferenceExpression queryParameterReferenceExpression;
			if (this.parameterMappings.TryGetValue(expression, out queryParameterReferenceExpression))
			{
				queryExpression = queryParameterReferenceExpression;
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000391D4 File Offset: 0x000373D4
		protected internal override QueryExpression Visit(QueryParameterDeclarationExpression expression)
		{
			QueryParameterDeclarationExpression queryParameterDeclarationExpression = this.VisitTerminal<QueryParameterDeclarationExpression>(expression, (ConceptualResultType newResultType) => newResultType.DeclareParameterAs(expression.Name));
			if (expression != queryParameterDeclarationExpression)
			{
				this.RebindParameter(expression.Parameter, queryParameterDeclarationExpression.Parameter);
			}
			this.NotifyIfChanged(expression, queryParameterDeclarationExpression);
			return queryParameterDeclarationExpression;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00039235 File Offset: 0x00037435
		private void RebindParameter(QueryParameterReferenceExpression from, QueryParameterReferenceExpression to)
		{
			if (!QueryNamingContext.NameComparer.Equals(from.Name, to.Name) || from.ConceptualResultType != to.ConceptualResultType)
			{
				this.parameterMappings[from] = to;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003926C File Offset: 0x0003746C
		protected internal override QueryExpression Visit(QueryVariableDeclarationExpression expression)
		{
			QueryVariableDeclarationExpression queryVariableDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryVariableDeclarationExpression = queryExpression.DeclareVariableAs(expression.VariableName);
				this.RebindVariable(expression.Variable, queryVariableDeclarationExpression.Variable);
			}
			this.NotifyIfChanged(expression, queryVariableDeclarationExpression);
			return queryVariableDeclarationExpression;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000392BC File Offset: 0x000374BC
		protected internal override QueryExpression Visit(QueryMeasureDeclarationExpression expression)
		{
			QueryMeasureDeclarationExpression queryMeasureDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryMeasureDeclarationExpression = queryExpression.DeclareMeasureAs(expression.MeasureRef);
			}
			this.NotifyIfChanged(expression, queryMeasureDeclarationExpression);
			return queryMeasureDeclarationExpression;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000392F8 File Offset: 0x000374F8
		protected internal override QueryExpression Visit(QueryFieldDeclarationExpression expression)
		{
			QueryFieldDeclarationExpression queryFieldDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryFieldDeclarationExpression = queryExpression.DeclareFieldAs(expression.FieldRef);
			}
			this.NotifyIfChanged(expression, queryFieldDeclarationExpression);
			return queryFieldDeclarationExpression;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00039334 File Offset: 0x00037534
		protected internal override QueryExpression Visit(QueryTableDeclarationExpression expression)
		{
			QueryTableDeclarationExpression queryTableDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			QueryVisualShape queryVisualShape = this.VisitVisualShape(expression.VisualShape);
			IReadOnlyList<QueryFieldDeclarationExpression> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryFieldDeclarationExpression>(expression.AdditionalColumns, (QueryFieldDeclarationExpression c) => (QueryFieldDeclarationExpression)this.VisitExpression(c));
			if (expression.Expression != queryExpression || expression.VisualShape != queryVisualShape || expression.AdditionalColumns != readOnlyList)
			{
				queryTableDeclarationExpression = queryExpression.DeclareTableAs(queryVisualShape, expression.Entity, readOnlyList);
			}
			this.NotifyIfChanged(expression, queryTableDeclarationExpression);
			return queryTableDeclarationExpression;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000393A8 File Offset: 0x000375A8
		private QueryVisualShape VisitVisualShape(QueryVisualShape visualShape)
		{
			if (visualShape == null)
			{
				return null;
			}
			IReadOnlyList<QueryVisualAxis> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryVisualAxis>(visualShape.Axes, new Func<QueryVisualAxis, QueryVisualAxis>(this.VisitVisualAxis));
			if (visualShape.Axes != readOnlyList)
			{
				return new QueryVisualShape(readOnlyList, visualShape.IsDensifiedColumnName);
			}
			return visualShape;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000393EC File Offset: 0x000375EC
		private QueryVisualAxis VisitVisualAxis(QueryVisualAxis axis)
		{
			IReadOnlyList<QueryVisualAxisGroup> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryVisualAxisGroup>(axis.Groups, new Func<QueryVisualAxisGroup, QueryVisualAxisGroup>(this.VisitVisualAxisGroup));
			IReadOnlyList<QuerySortClause> readOnlyList2 = Microsoft.Reporting.Util.VisitReadOnlyList<QuerySortClause>(axis.OrderBy, new Func<QuerySortClause, QuerySortClause>(this.VisitSortClause));
			if (axis.Groups != readOnlyList || axis.OrderBy != readOnlyList2)
			{
				return new QueryVisualAxis(axis.Name, readOnlyList, readOnlyList2);
			}
			return axis;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003944C File Offset: 0x0003764C
		private QueryVisualAxisGroup VisitVisualAxisGroup(QueryVisualAxisGroup axisGroup)
		{
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(axisGroup.Keys);
			QueryExpression queryExpression = this.VisitExpression(axisGroup.SubtotalIndicator);
			if (axisGroup.Keys != readOnlyList || axisGroup.SubtotalIndicator != queryExpression)
			{
				return new QueryVisualAxisGroup(readOnlyList, queryExpression);
			}
			return axisGroup;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00039490 File Offset: 0x00037690
		protected internal override QueryExpression Visit(QueryDataSourceVariablesDeclarationExpression expression)
		{
			QueryDataSourceVariablesDeclarationExpression queryDataSourceVariablesDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryDataSourceVariablesDeclarationExpression = queryExpression.DeclareDataSourceVariables();
			}
			this.NotifyIfChanged(expression, queryDataSourceVariablesDeclarationExpression);
			return queryDataSourceVariablesDeclarationExpression;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x000394C8 File Offset: 0x000376C8
		protected internal override QueryExpression Visit(QueryMParameterDeclarationExpression expression)
		{
			QueryMParameterDeclarationExpression queryMParameterDeclarationExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryMParameterDeclarationExpression = expression.ParameterName.DeclareMParameter(queryExpression);
			}
			this.NotifyIfChanged(expression, queryMParameterDeclarationExpression);
			return queryMParameterDeclarationExpression;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00039503 File Offset: 0x00037703
		protected internal override QueryExpression Visit(QueryLimitExpression expression)
		{
			return this.VisitLimit(expression, expression.LimitKind);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00039514 File Offset: 0x00037714
		private QueryExpression VisitLimit(QueryLimitExpression expression, QueryLimitOperator limitKind)
		{
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression = this.VisitExpression(expression.Count);
			QueryExpression queryExpression2 = this.VisitExpression(expression.SkipCount);
			IList<QuerySortClause> list = this.VisitSortOrder(expression.SortOrder);
			this.ExitScope();
			if (expression.Input == queryExpressionBinding && expression.Count == queryExpression && expression.SkipCount == queryExpression2 && expression.SortOrder == list)
			{
				this.NotifyIfChanged(expression, expression);
				return expression;
			}
			switch (limitKind)
			{
			case QueryLimitOperator.TopN:
				return queryExpressionBinding.TopN(queryExpression, list);
			case QueryLimitOperator.Sample:
				return queryExpressionBinding.Sample(queryExpression, list);
			case QueryLimitOperator.TopNSkip:
				return queryExpressionBinding.TopNSkip(queryExpression, queryExpression2, list);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000395C8 File Offset: 0x000377C8
		protected internal override QueryExpression Visit(QueryLookupValueExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.ResultColumn);
			IReadOnlyList<QueryLookupTuple> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryLookupTuple>(expression.LookupTuples, new Func<QueryLookupTuple, QueryLookupTuple>(this.VisitQueryLookupTuple));
			if (expression.ResultColumn != queryExpression2 || expression.LookupTuples != readOnlyList)
			{
				queryExpression = queryExpression2.LookupValue(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00039620 File Offset: 0x00037820
		private QueryLookupTuple VisitQueryLookupTuple(QueryLookupTuple lookupTuple)
		{
			QueryLookupTuple queryLookupTuple = lookupTuple;
			QueryExpression queryExpression = this.VisitExpression(lookupTuple.SearchColumn);
			QueryExpression queryExpression2 = this.VisitExpression(lookupTuple.SearchValue);
			if (lookupTuple.SearchColumn != queryExpression || lookupTuple.SearchValue != queryExpression2)
			{
				queryLookupTuple = new QueryLookupTuple(queryExpression, queryExpression2);
			}
			return queryLookupTuple;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00039664 File Offset: 0x00037864
		protected internal override QueryExpression Visit(QueryConcatenateExpression expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Inputs);
			if (expression.Inputs != readOnlyList)
			{
				queryExpression = readOnlyList.Concatenate();
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003969C File Offset: 0x0003789C
		protected internal override QueryExpression Visit(QueryConcatenateXExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Table);
			QueryExpression queryExpression2 = expression.Expression.Accept<QueryExpression>(this);
			QueryExpression queryExpression3 = this.VisitExpression(expression.Delimiter);
			QuerySortClause querySortClause = this.VisitSortClause(expression.OrderBy);
			this.ExitScope();
			if (expression.Table != queryExpressionBinding || expression.Expression != queryExpression2 || expression.Delimiter != queryExpression3 || expression.OrderBy != querySortClause)
			{
				queryExpression = queryExpressionBinding.ConcatenateX(queryExpression2, queryExpression3, querySortClause);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00039720 File Offset: 0x00037920
		protected internal override QueryExpression Visit(QueryConvertToStringExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = expression.Input.Accept<QueryExpression>(this);
			if (expression.Input != queryExpression2)
			{
				queryExpression = queryExpression2.ConvertToString();
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00039758 File Offset: 0x00037958
		protected internal override QueryExpression Visit(QueryExpressionWithLocalVariables expression)
		{
			QueryExpression queryExpression = expression;
			IReadOnlyList<QueryVariableDeclarationExpression> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryVariableDeclarationExpression>(expression.Declarations, new Func<QueryVariableDeclarationExpression, QueryVariableDeclarationExpression>(this.VisitVariableDeclarationEnterScope));
			QueryExpression queryExpression2 = expression.Result.Accept<QueryExpression>(this);
			for (int i = 0; i < expression.Declarations.Count; i++)
			{
				this.ExitScope();
			}
			if (expression.Declarations != readOnlyList || expression.Result != queryExpression2)
			{
				queryExpression = QueryExpressionBuilder.CreateExpressionWithLocalVariables(readOnlyList, queryExpression2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000397CC File Offset: 0x000379CC
		protected internal override QueryExpression Visit(QueryDataTableExpression expression)
		{
			QueryDataTableExpression queryDataTableExpression = expression;
			IReadOnlyList<QueryTupleExpression> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QueryTupleExpression>(expression.Rows, new Func<QueryTupleExpression, QueryTupleExpression>(this.VisitQueryTuple));
			if (expression.Rows != readOnlyList)
			{
				queryDataTableExpression = new QueryDataTableExpression(expression.ConceptualResultType, readOnlyList, expression.ColumnNames);
			}
			return queryDataTableExpression;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00039810 File Offset: 0x00037A10
		protected internal override QueryExpression Visit(QueryTupleExpression expression)
		{
			QueryTupleExpression queryTupleExpression = expression;
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(expression.NamedColumns);
			if (expression.NamedColumns != readOnlyList)
			{
				queryTupleExpression = new QueryTupleExpression(expression.ConceptualResultType, readOnlyList.ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>());
			}
			return queryTupleExpression;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00039848 File Offset: 0x00037A48
		protected internal override QueryExpression Visit(QueryInExpression expression)
		{
			QueryInExpression queryInExpression = expression;
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Expressions);
			IReadOnlyList<IReadOnlyList<QueryExpression>> readOnlyList2 = this.VisitExpressionList(expression.Values);
			if (expression.Expressions != readOnlyList || expression.Values != readOnlyList2)
			{
				queryInExpression = readOnlyList.In(readOnlyList2, expression.IsStrict);
			}
			this.NotifyIfChanged(expression, queryInExpression);
			return queryInExpression;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0003989C File Offset: 0x00037A9C
		protected internal override QueryExpression Visit(QueryTypeSafeFloorExpression expression)
		{
			QueryTypeSafeFloorExpression queryTypeSafeFloorExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Expression);
			if (expression.Expression != queryExpression)
			{
				queryTypeSafeFloorExpression = new QueryTypeSafeFloorExpression(expression.ConceptualResultType, queryExpression, expression.Size, expression.TimeUnit);
			}
			this.NotifyIfChanged(expression, queryTypeSafeFloorExpression);
			return queryTypeSafeFloorExpression;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000398E4 File Offset: 0x00037AE4
		protected internal override QueryExpression Visit(QueryInTableExpression expression)
		{
			QueryInTableExpression queryInTableExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.LeftExpression);
			QueryExpression queryExpression2 = this.VisitExpression(expression.RightExpression);
			if (expression.LeftExpression != queryExpression || expression.RightExpression != queryExpression2)
			{
				queryInTableExpression = queryExpression.InTable(queryExpression2);
			}
			this.NotifyIfChanged(expression, queryInTableExpression);
			return queryInTableExpression;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00039930 File Offset: 0x00037B30
		internal QueryTupleExpression VisitQueryTuple(QueryTupleExpression row)
		{
			return (QueryTupleExpression)row.Accept<QueryExpression>(this);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00039940 File Offset: 0x00037B40
		protected internal override QueryExpression Visit(QueryTreatAsExpression expression)
		{
			QueryTreatAsExpression queryTreatAsExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.InputTable);
			IReadOnlyList<KeyValuePair<string, QueryExpression>> readOnlyList = this.VisitExpressionList(expression.Columns);
			if (expression.InputTable != queryExpression || expression.Columns != readOnlyList)
			{
				queryTreatAsExpression = queryExpression.TreatAs(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryTreatAsExpression);
			return queryTreatAsExpression;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0003998C File Offset: 0x00037B8C
		protected internal override QueryExpression Visit(QueryCalculateExpression expression)
		{
			QueryCalculateExpression queryCalculateExpression = expression;
			QueryExpression queryExpression = this.VisitExpression(expression.Argument);
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Filters);
			if (expression.Argument != queryExpression || expression.Filters != readOnlyList)
			{
				queryCalculateExpression = queryExpression.Calculate(readOnlyList);
			}
			this.NotifyIfChanged(expression, queryCalculateExpression);
			return queryCalculateExpression;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000399D8 File Offset: 0x00037BD8
		protected internal override QueryExpression Visit(QueryCountRowsExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<10>__CountRows) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<10>__CountRows = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.CountRows));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000399FC File Offset: 0x00037BFC
		protected internal override QueryExpression Visit(QueryDistinctExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<11>__Distinct) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<11>__Distinct = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.Distinct));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00039A20 File Offset: 0x00037C20
		protected internal override QueryExpression Visit(QueryIsAggregateExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<12>__IsAggregate) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<12>__IsAggregate = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.IsAggregate));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00039A44 File Offset: 0x00037C44
		protected internal override QueryExpression Visit(QueryIsNullExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<13>__IsNull) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<13>__IsNull = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.IsNull));
			}
			return this.VisitUnary(expression, func);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00039A68 File Offset: 0x00037C68
		protected internal override QueryExpression Visit(QueryIsEmptyExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<14>__IsEmpty) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<14>__IsEmpty = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.IsEmpty));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00039A8C File Offset: 0x00037C8C
		protected internal override QueryExpression Visit(QueryDateDiffExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.StartDate);
			QueryExpression queryExpression3 = this.VisitExpression(expression.EndDate);
			if (expression.StartDate != queryExpression2 || expression.EndDate != queryExpression3)
			{
				queryExpression = QueryExpressionBuilder.DateDiff(queryExpression2, queryExpression3, expression.TimeUnit);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00039AE0 File Offset: 0x00037CE0
		protected internal override QueryExpression Visit(QuerySampleAxisWithLocalMinMaxExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression2 = this.VisitExpression(expression.MaxTargetPointCount);
			QueryExpression queryExpression3 = this.VisitExpression(expression.Axis);
			IReadOnlyList<QueryExpression> readOnlyList = this.VisitExpressionList(expression.Measures);
			QueryExpression queryExpression4 = this.VisitExpression(expression.MinPointsResolution);
			IReadOnlyList<QueryExpression> readOnlyList2 = this.VisitExpressionList(expression.Series);
			QueryExpression queryExpression5 = this.VisitExpression(expression.MaxPointsResolution);
			QueryExpression queryExpression6 = this.VisitExpression(expression.MaxDynamicSeriesCount);
			this.ExitScope();
			if (expression.MaxTargetPointCount != queryExpression2 || expression.Input != queryExpressionBinding || expression.Axis != queryExpression3 || expression.Measures != readOnlyList || expression.MinPointsResolution != queryExpression4 || expression.Series != readOnlyList2 || expression.MaxPointsResolution != queryExpression5 || expression.MaxDynamicSeriesCount != queryExpression6)
			{
				queryExpression = QueryExpressionBuilder.SampleAxisWithLocalMinMax(queryExpression2, queryExpressionBinding, queryExpression3, readOnlyList, queryExpression4, readOnlyList2, expression.DynamicSeriesSelectionCriteria, expression.DynamicSeriesSelectionCriteriaOrder, queryExpression5, queryExpression6);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00039BD8 File Offset: 0x00037DD8
		protected internal override QueryExpression Visit(QuerySampleCartesianPointsByCoverExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression2 = this.VisitExpression(expression.MaxTargetPointCount);
			QueryExpression queryExpression3 = this.VisitExpression(expression.X);
			QueryExpression queryExpression4 = this.VisitExpression(expression.Y);
			QueryExpression queryExpression5 = this.VisitExpression(expression.Radius);
			QueryExpression queryExpression6 = this.VisitExpression(expression.MaxMinRatio);
			QueryExpression queryExpression7 = this.VisitExpression(expression.MaxBlankRatio);
			this.ExitScope();
			if (expression.MaxTargetPointCount != queryExpression2 || expression.Input != queryExpressionBinding || expression.X != queryExpression3 || expression.Y != queryExpression4 || expression.Radius != queryExpression5 || expression.MaxMinRatio != queryExpression6 || expression.MaxBlankRatio != queryExpression7)
			{
				queryExpression = QueryExpressionBuilder.SampleCartesianPointsByCover(queryExpression2, queryExpressionBinding, queryExpression3, queryExpression4, queryExpression5, queryExpression6, queryExpression7);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00039CAC File Offset: 0x00037EAC
		protected internal override QueryExpression Visit(QueryIsOnOrAfterExpression expression)
		{
			QueryExpression queryExpression = expression;
			IList<QueryIsOnOrAfterArgument> list = Microsoft.Reporting.Util.VisitList<QueryIsOnOrAfterArgument>(expression.Arguments, new Func<QueryIsOnOrAfterArgument, QueryIsOnOrAfterArgument>(this.VisitIsOnOrAfterArgument));
			if (expression.Arguments != list)
			{
				queryExpression = QueryExpressionBuilder.IsOnOrAfter(list);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00039CEC File Offset: 0x00037EEC
		protected internal override QueryExpression Visit(QueryIsAfterExpression expression)
		{
			QueryExpression queryExpression = expression;
			IList<QueryIsOnOrAfterArgument> list = Microsoft.Reporting.Util.VisitList<QueryIsOnOrAfterArgument>(expression.Arguments, new Func<QueryIsOnOrAfterArgument, QueryIsOnOrAfterArgument>(this.VisitIsOnOrAfterArgument));
			if (expression.Arguments != list)
			{
				queryExpression = QueryExpressionBuilder.IsAfter(list);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00039D2C File Offset: 0x00037F2C
		protected internal override QueryExpression Visit(QueryTopNPerLevelSampleExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpressionBinding queryExpressionBinding = this.VisitExpressionBindingEnterScope(expression.Input);
			QueryExpression queryExpression2 = this.VisitExpression(expression.Count);
			this.ExitScope();
			if (expression.Input != queryExpressionBinding || expression.Count != queryExpression2)
			{
				queryExpression = queryExpressionBinding.TopNPerLevel(expression.Levels, queryExpression2, expression.RestartIndicatorColumnName, expression.WindowExpansion);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00039D90 File Offset: 0x00037F90
		private QueryIsOnOrAfterArgument VisitIsOnOrAfterArgument(QueryIsOnOrAfterArgument arg)
		{
			QueryIsOnOrAfterArgument queryIsOnOrAfterArgument = arg;
			QueryExpression queryExpression = this.VisitExpression(arg.Left);
			QueryExpression queryExpression2 = this.VisitExpression(arg.Right);
			if (arg.Left != queryExpression || arg.Right != queryExpression2)
			{
				queryIsOnOrAfterArgument = new QueryIsOnOrAfterArgument(queryExpression, queryExpression2, arg.Direction);
			}
			return queryIsOnOrAfterArgument;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00039DDC File Offset: 0x00037FDC
		protected internal override QueryExpression Visit(QuerySwitchExpression expression)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Input);
			IReadOnlyList<QuerySwitchCase> readOnlyList = Microsoft.Reporting.Util.VisitReadOnlyList<QuerySwitchCase>(expression.Cases, new Func<QuerySwitchCase, QuerySwitchCase>(this.VisitQuerySwitchCase));
			QueryExpression queryExpression3 = this.VisitExpression(expression.DefaultResult);
			if (expression.Input != queryExpression2 || expression.Cases != readOnlyList || expression.DefaultResult != queryExpression3)
			{
				queryExpression = queryExpression2.Switch(readOnlyList, queryExpression3);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00039E4C File Offset: 0x0003804C
		private QuerySwitchCase VisitQuerySwitchCase(QuerySwitchCase queryCase)
		{
			QuerySwitchCase querySwitchCase = queryCase;
			QueryExpression queryExpression = this.VisitExpression(queryCase.Value);
			QueryExpression queryExpression2 = this.VisitExpression(queryCase.Result);
			if (queryCase.Value != queryExpression || queryCase.Result != queryExpression2)
			{
				querySwitchCase = new QuerySwitchCase(queryExpression, queryExpression2);
			}
			return querySwitchCase;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00039E90 File Offset: 0x00038090
		protected internal override QueryExpression Visit(QueryAllExpression expression)
		{
			QueryExpression queryExpression = expression;
			EntitySet entitySet = this.VisitEntitySet(expression.Target);
			IConceptualEntity conceptualEntity = this.VisitEntity(expression.TargetEntity);
			if (expression.Target != entitySet || expression.TargetEntity != conceptualEntity)
			{
				QueryAllKind allKind = expression.AllKind;
				if (allKind != QueryAllKind.All)
				{
					if (allKind != QueryAllKind.AllSelected)
					{
						throw new NotSupportedException();
					}
					if (expression.Target == null)
					{
						queryExpression = QueryExpressionBuilder.AllSelected();
					}
					else if (expression.Fields == null)
					{
						queryExpression = entitySet.AllSelected(conceptualEntity);
					}
					else
					{
						queryExpression = entitySet.AllSelected(expression.Fields, conceptualEntity, expression.Columns);
					}
				}
				else if (expression.Target == null)
				{
					queryExpression = QueryExpressionBuilder.All();
				}
				else if (expression.Fields == null)
				{
					queryExpression = entitySet.All(conceptualEntity);
				}
				else
				{
					queryExpression = entitySet.All(expression.Fields, conceptualEntity, expression.Columns);
				}
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00039F60 File Offset: 0x00038160
		protected internal override QueryExpression Visit(QueryMeasureExpression expression)
		{
			QueryExpression queryExpression = expression;
			EntitySet entitySet = this.VisitEntitySet(expression.Target);
			IConceptualEntity conceptualEntity = this.VisitEntity(expression.TargetEntity);
			if (expression.Target != entitySet || expression.TargetEntity != conceptualEntity)
			{
				queryExpression = entitySet.InvokeMeasure(expression.Measure.Name, conceptualEntity);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00039FB8 File Offset: 0x000381B8
		protected internal override QueryExpression Visit(QueryDaxTextExpression expression)
		{
			return this.VisitTerminal<QueryDaxTextExpression>(expression, (ConceptualResultType newConceptualType) => QueryExpressionBuilder.DaxText(newConceptualType, expression.Text));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00039FEA File Offset: 0x000381EA
		protected internal override QueryExpression Visit(QuerySingleValueExpression expression)
		{
			Func<QueryExpression, QueryExpression> func;
			if ((func = DefaultExpressionVisitor.<>O.<15>__SingleValue) == null)
			{
				func = (DefaultExpressionVisitor.<>O.<15>__SingleValue = new Func<QueryExpression, QueryExpression>(QueryExpressionBuilder.SingleValue));
			}
			return this.VisitUnaryExtension(expression, func);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0003A010 File Offset: 0x00038210
		private QueryExpression VisitUnaryExtension(QueryUnaryExtensionExpression expression, Func<QueryExpression, QueryExpression> callback)
		{
			QueryExpression queryExpression = expression;
			QueryExpression queryExpression2 = this.VisitExpression(expression.Argument);
			if (expression.Argument != queryExpression2)
			{
				queryExpression = callback(queryExpression2);
			}
			this.NotifyIfChanged(expression, queryExpression);
			return queryExpression;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0003A048 File Offset: 0x00038248
		protected internal override QueryExpression Visit(QueryEarlierExpression expression)
		{
			QueryEarlierExpression queryEarlierExpression = expression;
			QueryExpression queryExpression = expression.Column.Accept<QueryExpression>(this);
			if (expression.Column != queryExpression)
			{
				queryEarlierExpression = queryExpression.Earlier(expression.Number);
			}
			this.NotifyIfChanged(expression, queryEarlierExpression);
			return queryEarlierExpression;
		}

		// Token: 0x04000AFC RID: 2812
		private readonly Dictionary<QueryVariableReferenceExpression, QueryVariableReferenceExpression> varMappings = new Dictionary<QueryVariableReferenceExpression, QueryVariableReferenceExpression>();

		// Token: 0x04000AFD RID: 2813
		private readonly Dictionary<QueryParameterReferenceExpression, QueryParameterReferenceExpression> parameterMappings = new Dictionary<QueryParameterReferenceExpression, QueryParameterReferenceExpression>();

		// Token: 0x020003A9 RID: 937
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001346 RID: 4934
			public static Func<ConceptualResultType, QueryNullExpression> <0>__Null;

			// Token: 0x04001347 RID: 4935
			public static Func<QueryExpression, QueryExpression, QueryExpression> <1>__Equal;

			// Token: 0x04001348 RID: 4936
			public static Func<QueryExpression, QueryExpression, QueryExpression> <2>__NotEqual;

			// Token: 0x04001349 RID: 4937
			public static Func<QueryExpression, QueryExpression, QueryExpression> <3>__GreaterThan;

			// Token: 0x0400134A RID: 4938
			public static Func<QueryExpression, QueryExpression, QueryExpression> <4>__GreaterThanOrEqual;

			// Token: 0x0400134B RID: 4939
			public static Func<QueryExpression, QueryExpression, QueryExpression> <5>__LessThan;

			// Token: 0x0400134C RID: 4940
			public static Func<QueryExpression, QueryExpression, QueryExpression> <6>__LessThanOrEqual;

			// Token: 0x0400134D RID: 4941
			public static Func<QueryExpression, QueryExpression, QueryExpression> <7>__EqualIdentity;

			// Token: 0x0400134E RID: 4942
			public static Func<QueryExpression, QueryExpression, QueryExpression> <8>__NotEqualIdentity;

			// Token: 0x0400134F RID: 4943
			public static Func<QueryExpression, QueryExpression> <9>__NonVisual;

			// Token: 0x04001350 RID: 4944
			public static Func<QueryExpression, QueryExpression> <10>__CountRows;

			// Token: 0x04001351 RID: 4945
			public static Func<QueryExpression, QueryExpression> <11>__Distinct;

			// Token: 0x04001352 RID: 4946
			public static Func<QueryExpression, QueryExpression> <12>__IsAggregate;

			// Token: 0x04001353 RID: 4947
			public static Func<QueryExpression, QueryExpression> <13>__IsNull;

			// Token: 0x04001354 RID: 4948
			public static Func<QueryExpression, QueryExpression> <14>__IsEmpty;

			// Token: 0x04001355 RID: 4949
			public static Func<QueryExpression, QueryExpression> <15>__SingleValue;
		}
	}
}
