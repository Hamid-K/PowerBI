using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x02002037 RID: 8247
	internal abstract class SqlExpressionVisitor<TExpression, TBinding>
	{
		// Token: 0x17003098 RID: 12440
		// (get) Token: 0x0600C966 RID: 51558 RVA: 0x00285445 File Offset: 0x00283645
		protected SqlExpressionVisitor<TExpression, TBinding>.BindingContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x0600C967 RID: 51559 RVA: 0x0028544D File Offset: 0x0028364D
		protected virtual void BeginScalarExpression(SqlExpressionVisitor<TExpression, TBinding>.Table table)
		{
			this.context.Push(table);
		}

		// Token: 0x0600C968 RID: 51560 RVA: 0x0028545B File Offset: 0x0028365B
		protected virtual TExpression EndScalarExpression(TExpression expression)
		{
			this.context.Pop();
			return expression;
		}

		// Token: 0x0600C969 RID: 51561
		protected abstract TExpression NewBinaryExpression(BinaryExpressionType type, TExpression left, TExpression right);

		// Token: 0x0600C96A RID: 51562
		protected abstract TExpression NewBooleanBinaryExpression(BooleanBinaryExpressionType type, TExpression left, TExpression right);

		// Token: 0x0600C96B RID: 51563
		protected abstract TExpression NewBooleanComparisonExpression(BooleanComparisonType type, TExpression left, TExpression right);

		// Token: 0x0600C96C RID: 51564
		protected abstract TExpression NewBooleanIsNullExpression(bool isNot, TExpression expression);

		// Token: 0x0600C96D RID: 51565
		protected abstract TExpression NewBooleanNotExpression(TExpression expression);

		// Token: 0x0600C96E RID: 51566
		protected abstract TExpression NewBooleanParenthesisExpression(TExpression expression);

		// Token: 0x0600C96F RID: 51567
		protected abstract TExpression NewCastCall(TExpression dataType, TExpression expression);

		// Token: 0x0600C970 RID: 51568
		protected abstract TExpression NewCoalesceExpression(IList<TExpression> expressions);

		// Token: 0x0600C971 RID: 51569
		protected abstract TExpression NewColumnReferenceExpression(ColumnType type, MultiPartIdentifier identifier);

		// Token: 0x0600C972 RID: 51570
		protected abstract TExpression NewConvertCall(TExpression dataType, TExpression expression);

		// Token: 0x0600C973 RID: 51571
		protected abstract TExpression NewExpressionWithSortOrder(SortOrder sortOrder, TExpression expression);

		// Token: 0x0600C974 RID: 51572
		protected abstract TExpression NewFunctionCall(Identifier function, UniqueRowFilter uniqueRowFilter, IList<TExpression> parameters);

		// Token: 0x0600C975 RID: 51573
		protected abstract TExpression NewInPredicate(TExpression expression, IList<TExpression> values, TExpression subquery);

		// Token: 0x0600C976 RID: 51574
		protected abstract TExpression NewLeftFunctionCall(TExpression expression, TExpression count);

		// Token: 0x0600C977 RID: 51575
		protected abstract TExpression NewLiteralExpression(LiteralType type, string value);

		// Token: 0x0600C978 RID: 51576
		protected abstract TExpression NewParenthesisExpression(TExpression expression);

		// Token: 0x0600C979 RID: 51577
		protected abstract TExpression NewRightFunctionCall(TExpression expression, TExpression count);

		// Token: 0x0600C97A RID: 51578
		protected abstract TExpression NewScalarSubquery(SqlExpressionVisitor<TExpression, TBinding>.Table table, bool list);

		// Token: 0x0600C97B RID: 51579
		protected abstract TExpression NewSearchedCaseExpression(IList<Tuple<TExpression, TExpression>> whenExpressions, TExpression elseExpression);

		// Token: 0x0600C97C RID: 51580
		protected abstract TExpression NewSqlDataTypeReference(MultiPartIdentifier name, SqlDataTypeOption sqlDataTypeOption, IList<TExpression> parameters);

		// Token: 0x0600C97D RID: 51581
		protected abstract TExpression NewUnaryExpression(UnaryExpressionType type, TExpression expression);

		// Token: 0x0600C97E RID: 51582
		protected abstract TExpression NewVariableReferenceExpression(string name);

		// Token: 0x0600C97F RID: 51583
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewJoinParenthesisTableReference(SqlExpressionVisitor<TExpression, TBinding>.Table table);

		// Token: 0x0600C980 RID: 51584
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewNamedTableReference(Identifier alias, MultiPartIdentifier table);

		// Token: 0x0600C981 RID: 51585
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewQualifiedJoin(SqlExpressionVisitor<TExpression, TBinding>.Table firstTable, SqlExpressionVisitor<TExpression, TBinding>.Table secondTable, QualifiedJoinType joinType, TExpression searchCondition);

		// Token: 0x0600C982 RID: 51586
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewBinaryQueryExpression(SqlExpressionVisitor<TExpression, TBinding>.Table firstTable, SqlExpressionVisitor<TExpression, TBinding>.Table secondTable, BinaryQueryExpressionType binaryQueryExpressionType, bool binaryQueryAll);

		// Token: 0x0600C983 RID: 51587
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewQueryDerivedTable(Identifier alias, IList<Identifier> columns, SqlExpressionVisitor<TExpression, TBinding>.Table table);

		// Token: 0x0600C984 RID: 51588
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewQueryParenthesisExpression(SqlExpressionVisitor<TExpression, TBinding>.Table table);

		// Token: 0x0600C985 RID: 51589
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewQuerySpecification(SqlExpressionVisitor<TExpression, TBinding>.Table table);

		// Token: 0x0600C986 RID: 51590
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewVariableTableReference(Identifier alias, string name);

		// Token: 0x0600C987 RID: 51591
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewSchemaObjectFunctionTableReference(Identifier alias, IList<Identifier> columnAliases, MultiPartIdentifier function, IList<TExpression> parameters);

		// Token: 0x0600C988 RID: 51592
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewComputedColumnsClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, IList<Tuple<Identifier, TExpression>> computedColumns);

		// Token: 0x0600C989 RID: 51593
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewDistinctClause(SqlExpressionVisitor<TExpression, TBinding>.Table table);

		// Token: 0x0600C98A RID: 51594
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewFromClause(IList<SqlExpressionVisitor<TExpression, TBinding>.Table> tableRefs);

		// Token: 0x0600C98B RID: 51595
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewGroupByClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, IList<TExpression> groupingSpecs, IList<Tuple<Identifier, TExpression>> computedColumns);

		// Token: 0x0600C98C RID: 51596
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewOrderByClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, IList<TExpression> orderByElements);

		// Token: 0x0600C98D RID: 51597
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewSelectedColumnsClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, IList<MultiPartIdentifier> selection);

		// Token: 0x0600C98E RID: 51598
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewTopClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, TExpression expression);

		// Token: 0x0600C98F RID: 51599
		protected abstract SqlExpressionVisitor<TExpression, TBinding>.Table NewWhereClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, TExpression searchCondition);

		// Token: 0x0600C990 RID: 51600 RVA: 0x0028546C File Offset: 0x0028366C
		protected virtual TExpression VisitBinaryExpression(BinaryExpression binary)
		{
			TExpression texpression = this.VisitScalarExpression(binary.FirstExpression);
			TExpression texpression2 = this.VisitScalarExpression(binary.SecondExpression);
			return this.NewBinaryExpression(binary.BinaryExpressionType, texpression, texpression2);
		}

		// Token: 0x0600C991 RID: 51601 RVA: 0x002854A4 File Offset: 0x002836A4
		protected virtual TExpression VisitBooleanBinaryExpression(BooleanBinaryExpression binary)
		{
			TExpression texpression = this.VisitBooleanExpression(binary.FirstExpression);
			TExpression texpression2 = this.VisitBooleanExpression(binary.SecondExpression);
			return this.NewBooleanBinaryExpression(binary.BinaryExpressionType, texpression, texpression2);
		}

		// Token: 0x0600C992 RID: 51602 RVA: 0x002854DC File Offset: 0x002836DC
		protected virtual TExpression VisitBooleanComparisonExpression(BooleanComparisonExpression comparison)
		{
			TExpression texpression = this.VisitScalarExpression(comparison.FirstExpression);
			TExpression texpression2 = this.VisitScalarExpression(comparison.SecondExpression);
			return this.NewBooleanComparisonExpression(comparison.ComparisonType, texpression, texpression2);
		}

		// Token: 0x0600C993 RID: 51603 RVA: 0x00285514 File Offset: 0x00283714
		protected virtual TExpression VisitBooleanExpression(BooleanExpression expression)
		{
			BooleanBinaryExpression booleanBinaryExpression = expression as BooleanBinaryExpression;
			if (booleanBinaryExpression != null)
			{
				return this.VisitBooleanBinaryExpression(booleanBinaryExpression);
			}
			BooleanComparisonExpression booleanComparisonExpression = expression as BooleanComparisonExpression;
			if (booleanComparisonExpression != null)
			{
				return this.VisitBooleanComparisonExpression(booleanComparisonExpression);
			}
			BooleanIsNullExpression booleanIsNullExpression = expression as BooleanIsNullExpression;
			if (booleanIsNullExpression != null)
			{
				return this.VisitBooleanIsNullExpression(booleanIsNullExpression);
			}
			BooleanNotExpression booleanNotExpression = expression as BooleanNotExpression;
			if (booleanNotExpression != null)
			{
				return this.VisitBooleanNotExpression(booleanNotExpression);
			}
			BooleanParenthesisExpression booleanParenthesisExpression = expression as BooleanParenthesisExpression;
			if (booleanParenthesisExpression != null)
			{
				return this.VisitBooleanParenthesisExpression(booleanParenthesisExpression);
			}
			InPredicate inPredicate = expression as InPredicate;
			if (inPredicate != null)
			{
				return this.VisitInPredicate(inPredicate);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C994 RID: 51604 RVA: 0x00285598 File Offset: 0x00283798
		protected virtual TExpression VisitBooleanIsNullExpression(BooleanIsNullExpression isNull)
		{
			TExpression texpression = this.VisitScalarExpression(isNull.Expression);
			return this.NewBooleanIsNullExpression(isNull.IsNot, texpression);
		}

		// Token: 0x0600C995 RID: 51605 RVA: 0x002855C0 File Offset: 0x002837C0
		protected virtual TExpression VisitBooleanNotExpression(BooleanNotExpression not)
		{
			TExpression texpression = this.VisitBooleanExpression(not.Expression);
			return this.NewBooleanNotExpression(texpression);
		}

		// Token: 0x0600C996 RID: 51606 RVA: 0x002855E4 File Offset: 0x002837E4
		protected virtual TExpression VisitBooleanParenthesisExpression(BooleanParenthesisExpression parenthesis)
		{
			TExpression texpression = this.VisitBooleanExpression(parenthesis.Expression);
			return this.NewBooleanParenthesisExpression(texpression);
		}

		// Token: 0x0600C997 RID: 51607 RVA: 0x00285608 File Offset: 0x00283808
		protected virtual TExpression VisitCastCall(CastCall castCall)
		{
			TExpression texpression = this.VisitDataTypeReference(castCall.DataType);
			TExpression texpression2 = this.VisitScalarExpression(castCall.Parameter);
			return this.NewCastCall(texpression, texpression2);
		}

		// Token: 0x0600C998 RID: 51608 RVA: 0x00285638 File Offset: 0x00283838
		protected virtual TExpression VisitCoalesceExpression(CoalesceExpression coalesce)
		{
			List<TExpression> list = new List<TExpression>();
			for (int i = 0; i < coalesce.Expressions.Count; i++)
			{
				list.Add(this.VisitScalarExpression(coalesce.Expressions[i]));
			}
			return this.NewCoalesceExpression(list);
		}

		// Token: 0x0600C999 RID: 51609 RVA: 0x00285680 File Offset: 0x00283880
		protected virtual TExpression VisitColumnReferenceExpression(ColumnReferenceExpression columnRef)
		{
			return this.NewColumnReferenceExpression(columnRef.ColumnType, columnRef.MultiPartIdentifier);
		}

		// Token: 0x0600C99A RID: 51610 RVA: 0x00285694 File Offset: 0x00283894
		protected virtual TExpression VisitConvertCall(ConvertCall convertCall)
		{
			if (convertCall.Style != null)
			{
				throw new NotSupportedException();
			}
			TExpression texpression = this.VisitDataTypeReference(convertCall.DataType);
			TExpression texpression2 = this.VisitScalarExpression(convertCall.Parameter);
			return this.NewConvertCall(texpression, texpression2);
		}

		// Token: 0x0600C99B RID: 51611 RVA: 0x002856D4 File Offset: 0x002838D4
		protected virtual TExpression VisitDataTypeReference(DataTypeReference dataTypeRef)
		{
			SqlDataTypeReference sqlDataTypeReference = dataTypeRef as SqlDataTypeReference;
			if (sqlDataTypeReference != null)
			{
				return this.VisitSqlDataTypeReference(sqlDataTypeReference);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600C99C RID: 51612 RVA: 0x002856F8 File Offset: 0x002838F8
		protected virtual TExpression VisitExpressionGroupingSpecification(ExpressionGroupingSpecification expressionGroupingSpec)
		{
			return this.VisitScalarExpression(expressionGroupingSpec.Expression);
		}

		// Token: 0x0600C99D RID: 51613 RVA: 0x00285708 File Offset: 0x00283908
		protected virtual TExpression VisitExpressionWithSortOrder(SqlExpressionVisitor<TExpression, TBinding>.Table table, ExpressionWithSortOrder expressionWithSortOrder)
		{
			this.BeginScalarExpression(table);
			TExpression texpression = this.VisitScalarExpression(expressionWithSortOrder.Expression);
			texpression = this.EndScalarExpression(texpression);
			return this.NewExpressionWithSortOrder(expressionWithSortOrder.SortOrder, texpression);
		}

		// Token: 0x0600C99E RID: 51614 RVA: 0x00285740 File Offset: 0x00283940
		protected virtual TExpression VisitFunctionCall(FunctionCall functionCall)
		{
			if (functionCall.CallTarget != null || functionCall.OverClause != null)
			{
				throw new NotSupportedException();
			}
			List<TExpression> list = new List<TExpression>();
			for (int i = 0; i < functionCall.Parameters.Count; i++)
			{
				list.Add(this.VisitScalarExpression(functionCall.Parameters[i]));
			}
			return this.NewFunctionCall(functionCall.FunctionName, functionCall.UniqueRowFilter, list);
		}

		// Token: 0x0600C99F RID: 51615 RVA: 0x002857AC File Offset: 0x002839AC
		protected virtual TExpression VisitGroupingSpecification(GroupingSpecification groupingSpec)
		{
			ExpressionGroupingSpecification expressionGroupingSpecification = groupingSpec as ExpressionGroupingSpecification;
			if (expressionGroupingSpecification != null)
			{
				return this.VisitExpressionGroupingSpecification(expressionGroupingSpecification);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9A0 RID: 51616 RVA: 0x002857D0 File Offset: 0x002839D0
		protected virtual TExpression VisitInPredicate(InPredicate inPredicate)
		{
			if (inPredicate.NotDefined)
			{
				throw new NotSupportedException();
			}
			TExpression texpression = this.VisitScalarExpression(inPredicate.Expression);
			TExpression texpression2 = default(TExpression);
			if (inPredicate.Subquery != null)
			{
				bool flag = this.requireList;
				this.requireList = true;
				texpression2 = this.VisitScalarSubquery(inPredicate.Subquery);
				this.requireList = flag;
			}
			IList<TExpression> list = this.VisitInPredicateValues(inPredicate.Values);
			return this.NewInPredicate(texpression, list, texpression2);
		}

		// Token: 0x0600C9A1 RID: 51617 RVA: 0x00285840 File Offset: 0x00283A40
		protected virtual IList<TExpression> VisitInPredicateValues(IList<ScalarExpression> values)
		{
			TExpression[] array = new TExpression[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitScalarExpression(values[i]);
			}
			return array;
		}

		// Token: 0x0600C9A2 RID: 51618 RVA: 0x0028587C File Offset: 0x00283A7C
		protected virtual TExpression VisitLiteralExpression(Literal literal)
		{
			return this.NewLiteralExpression(literal.LiteralType, literal.Value);
		}

		// Token: 0x0600C9A3 RID: 51619 RVA: 0x00285890 File Offset: 0x00283A90
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitNamedTableReference(NamedTableReference namedTableRef)
		{
			return this.NewNamedTableReference(namedTableRef.Alias, namedTableRef.SchemaObject);
		}

		// Token: 0x0600C9A4 RID: 51620 RVA: 0x002858A4 File Offset: 0x00283AA4
		protected virtual TExpression VisitParenthesisExpression(ParenthesisExpression parenthesis)
		{
			TExpression texpression = this.VisitScalarExpression(parenthesis.Expression);
			return this.NewParenthesisExpression(texpression);
		}

		// Token: 0x0600C9A5 RID: 51621 RVA: 0x002858C8 File Offset: 0x00283AC8
		protected virtual TExpression VisitPrimaryExpression(PrimaryExpression primary)
		{
			CastCall castCall = primary as CastCall;
			if (castCall != null)
			{
				return this.VisitCastCall(castCall);
			}
			CoalesceExpression coalesceExpression = primary as CoalesceExpression;
			if (coalesceExpression != null)
			{
				return this.VisitCoalesceExpression(coalesceExpression);
			}
			ColumnReferenceExpression columnReferenceExpression = primary as ColumnReferenceExpression;
			if (columnReferenceExpression != null)
			{
				return this.VisitColumnReferenceExpression(columnReferenceExpression);
			}
			ConvertCall convertCall = primary as ConvertCall;
			if (convertCall != null)
			{
				return this.VisitConvertCall(convertCall);
			}
			FunctionCall functionCall = primary as FunctionCall;
			if (functionCall != null)
			{
				return this.VisitFunctionCall(functionCall);
			}
			ParenthesisExpression parenthesisExpression = primary as ParenthesisExpression;
			if (parenthesisExpression != null)
			{
				return this.VisitParenthesisExpression(parenthesisExpression);
			}
			SearchedCaseExpression searchedCaseExpression = primary as SearchedCaseExpression;
			if (searchedCaseExpression != null)
			{
				return this.VisitSearchedCaseExpression(searchedCaseExpression);
			}
			ValueExpression valueExpression = primary as ValueExpression;
			if (valueExpression != null)
			{
				return this.VisitValueExpression(valueExpression);
			}
			ScalarSubquery scalarSubquery = primary as ScalarSubquery;
			if (scalarSubquery != null)
			{
				return this.VisitScalarSubquery(scalarSubquery);
			}
			LeftFunctionCall leftFunctionCall = primary as LeftFunctionCall;
			if (leftFunctionCall != null)
			{
				return this.VisitLeftFunctionCall(leftFunctionCall);
			}
			RightFunctionCall rightFunctionCall = primary as RightFunctionCall;
			if (rightFunctionCall != null)
			{
				return this.VisitRightFunctionCall(rightFunctionCall);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9A6 RID: 51622 RVA: 0x002859B8 File Offset: 0x00283BB8
		protected virtual TExpression VisitLeftFunctionCall(LeftFunctionCall leftFunctionCall)
		{
			TExpression texpression = this.VisitScalarExpression(leftFunctionCall.Parameters[0]);
			TExpression texpression2 = this.VisitScalarExpression(leftFunctionCall.Parameters[1]);
			return this.NewLeftFunctionCall(texpression, texpression2);
		}

		// Token: 0x0600C9A7 RID: 51623 RVA: 0x002859F4 File Offset: 0x00283BF4
		protected virtual TExpression VisitRightFunctionCall(RightFunctionCall rightFunctionCall)
		{
			TExpression texpression = this.VisitScalarExpression(rightFunctionCall.Parameters[0]);
			TExpression texpression2 = this.VisitScalarExpression(rightFunctionCall.Parameters[1]);
			return this.NewRightFunctionCall(texpression, texpression2);
		}

		// Token: 0x0600C9A8 RID: 51624 RVA: 0x00285A30 File Offset: 0x00283C30
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitQueryDerivedTable(QueryDerivedTable queryTable)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitQueryExpression(queryTable.QueryExpression);
			return this.NewQueryDerivedTable(queryTable.Alias, queryTable.Columns, table);
		}

		// Token: 0x0600C9A9 RID: 51625 RVA: 0x00285A60 File Offset: 0x00283C60
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitQueryExpression(QueryExpression query)
		{
			QuerySpecification querySpecification = query as QuerySpecification;
			if (querySpecification != null)
			{
				return this.VisitQuerySpecification(querySpecification);
			}
			QueryParenthesisExpression queryParenthesisExpression = query as QueryParenthesisExpression;
			if (queryParenthesisExpression != null)
			{
				return this.VisitQueryParenthesisExpression(queryParenthesisExpression);
			}
			BinaryQueryExpression binaryQueryExpression = query as BinaryQueryExpression;
			if (binaryQueryExpression != null)
			{
				return this.VisitBinaryQueryExpression(binaryQueryExpression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9AA RID: 51626 RVA: 0x00285AA8 File Offset: 0x00283CA8
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitQueryParenthesisExpression(QueryParenthesisExpression parenthesis)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitQueryExpression(parenthesis.QueryExpression);
			return this.NewQueryParenthesisExpression(table);
		}

		// Token: 0x0600C9AB RID: 51627 RVA: 0x00285ACC File Offset: 0x00283CCC
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitBinaryQueryExpression(BinaryQueryExpression binaryQuery)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitQueryExpression(binaryQuery.FirstQueryExpression);
			SqlExpressionVisitor<TExpression, TBinding>.Table table2 = this.VisitQueryExpression(binaryQuery.SecondQueryExpression);
			return this.NewBinaryQueryExpression(table, table2, binaryQuery.BinaryQueryExpressionType, binaryQuery.All);
		}

		// Token: 0x0600C9AC RID: 51628 RVA: 0x00285B08 File Offset: 0x00283D08
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitFromClause(FromClause from)
		{
			List<SqlExpressionVisitor<TExpression, TBinding>.Table> list = null;
			if (from != null)
			{
				list = new List<SqlExpressionVisitor<TExpression, TBinding>.Table>();
				for (int i = 0; i < from.TableReferences.Count; i++)
				{
					SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitTableReference(from.TableReferences[i]);
					list.Add(table);
				}
			}
			return this.NewFromClause(list);
		}

		// Token: 0x0600C9AD RID: 51629 RVA: 0x00285B58 File Offset: 0x00283D58
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitWhereClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, WhereClause where)
		{
			if (where.Cursor != null)
			{
				throw new NotSupportedException();
			}
			if (where.SearchCondition != null)
			{
				this.BeginScalarExpression(table);
				TExpression texpression = this.VisitBooleanExpression(where.SearchCondition);
				texpression = this.EndScalarExpression(texpression);
				return this.NewWhereClause(table, texpression);
			}
			return null;
		}

		// Token: 0x0600C9AE RID: 51630 RVA: 0x00285BA4 File Offset: 0x00283DA4
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitGroupByClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, GroupByClause groupBy, List<Tuple<Identifier, TExpression>> computedColumns)
		{
			if (groupBy.GroupByOption != GroupByOption.None)
			{
				throw new NotSupportedException();
			}
			if (groupBy.All)
			{
				throw new NotSupportedException();
			}
			List<TExpression> list = new List<TExpression>();
			for (int i = 0; i < groupBy.GroupingSpecifications.Count; i++)
			{
				this.BeginScalarExpression(table);
				TExpression texpression = this.VisitGroupingSpecification(groupBy.GroupingSpecifications[i]);
				texpression = this.EndScalarExpression(texpression);
				list.Add(texpression);
			}
			return this.NewGroupByClause(table, list, computedColumns);
		}

		// Token: 0x0600C9AF RID: 51631 RVA: 0x000033E7 File Offset: 0x000015E7
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitHavingClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, HavingClause havingClause)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600C9B0 RID: 51632 RVA: 0x00285C1C File Offset: 0x00283E1C
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitJoinParenthesisTableReference(JoinParenthesisTableReference joinParenTableRef)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitTableReference(joinParenTableRef.Join);
			return this.NewJoinParenthesisTableReference(table);
		}

		// Token: 0x0600C9B1 RID: 51633 RVA: 0x00285C40 File Offset: 0x00283E40
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitJoinTableReference(JoinTableReference joinTableRef)
		{
			QualifiedJoin qualifiedJoin = joinTableRef as QualifiedJoin;
			if (qualifiedJoin != null)
			{
				return this.VisitQualifiedJoin(qualifiedJoin);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9B2 RID: 51634 RVA: 0x00285C64 File Offset: 0x00283E64
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitOrderByClause(SqlExpressionVisitor<TExpression, TBinding>.Table table, OrderByClause orderByClause)
		{
			List<TExpression> list = new List<TExpression>();
			for (int i = 0; i < orderByClause.OrderByElements.Count; i++)
			{
				TExpression texpression = this.VisitExpressionWithSortOrder(table, orderByClause.OrderByElements[i]);
				list.Add(texpression);
			}
			return this.NewOrderByClause(table, list);
		}

		// Token: 0x0600C9B3 RID: 51635 RVA: 0x00285CB0 File Offset: 0x00283EB0
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitQualifiedJoin(QualifiedJoin qualifiedJoin)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitTableReference(qualifiedJoin.FirstTableReference);
			SqlExpressionVisitor<TExpression, TBinding>.Table table2 = this.VisitTableReference(qualifiedJoin.SecondTableReference);
			SqlExpressionVisitor<TExpression, TBinding>.Table table3 = new SqlExpressionVisitor<TExpression, TBinding>.Table
			{
				Inputs = SqlExpressionVisitor<TExpression, TBinding>.ConcatenateTables(table.Inputs, table2.Inputs),
				Columns = SqlExpressionVisitor<TExpression, TBinding>.ConcatenateColumns(table.Columns, table2.Columns)
			};
			this.BeginScalarExpression(table3);
			TExpression texpression = this.VisitBooleanExpression(qualifiedJoin.SearchCondition);
			texpression = this.EndScalarExpression(texpression);
			return this.NewQualifiedJoin(table, table2, qualifiedJoin.QualifiedJoinType, texpression);
		}

		// Token: 0x0600C9B4 RID: 51636 RVA: 0x00285D38 File Offset: 0x00283F38
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitQuerySpecification(QuerySpecification querySpec)
		{
			if (querySpec.ForClause != null || querySpec.OffsetClause != null)
			{
				throw new NotSupportedException();
			}
			bool flag = this.requireList;
			this.requireList = false;
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitFromClause(querySpec.FromClause);
			if (querySpec.WhereClause != null)
			{
				table = this.VisitWhereClause(table, querySpec.WhereClause);
			}
			List<MultiPartIdentifier> list = new List<MultiPartIdentifier>();
			List<Tuple<Identifier, TExpression>> list2 = new List<Tuple<Identifier, TExpression>>();
			for (int i = 0; i < querySpec.SelectElements.Count; i++)
			{
				SelectStarExpression selectStarExpression = querySpec.SelectElements[i] as SelectStarExpression;
				SelectScalarExpression selectScalarExpression = querySpec.SelectElements[i] as SelectScalarExpression;
				if (selectStarExpression != null)
				{
					if (selectStarExpression.Qualifier != null)
					{
						for (int j = 0; j < table.Columns.Count; j++)
						{
							if (SqlExpressionVisitor<TExpression, TBinding>.StartsWith(table.Columns[j].Identifier, selectStarExpression.Qualifier))
							{
								list.Add(table.Columns[j].Identifier);
							}
						}
					}
				}
				else
				{
					if (selectScalarExpression == null)
					{
						throw new NotSupportedException();
					}
					ColumnReferenceExpression columnReferenceExpression = selectScalarExpression.Expression as ColumnReferenceExpression;
					MultiPartIdentifier multiPartIdentifier;
					if (selectScalarExpression.ColumnName != null)
					{
						multiPartIdentifier = SqlExpressionVisitor<TExpression, TBinding>.NewIdentifier(selectScalarExpression.ColumnName.Identifier);
					}
					else if (columnReferenceExpression != null)
					{
						multiPartIdentifier = columnReferenceExpression.MultiPartIdentifier;
					}
					else
					{
						multiPartIdentifier = SqlExpressionVisitor<TExpression, TBinding>.NewIdentifier(this.NewColumnIdentifier());
					}
					list.Add(multiPartIdentifier);
					if (selectScalarExpression.ColumnName != null || columnReferenceExpression == null)
					{
						this.BeginScalarExpression(table);
						TExpression texpression = this.VisitScalarExpression(selectScalarExpression.Expression);
						texpression = this.EndScalarExpression(texpression);
						list2.Add(Tuple.Create<Identifier, TExpression>(multiPartIdentifier.Identifiers.Single<Identifier>(), texpression));
					}
				}
			}
			if (querySpec.GroupByClause != null)
			{
				table = this.VisitGroupByClause(table, querySpec.GroupByClause, list2);
			}
			else
			{
				table = this.NewComputedColumnsClause(table, list2);
			}
			if (querySpec.HavingClause != null)
			{
				table = this.VisitHavingClause(table, querySpec.HavingClause);
			}
			if (querySpec.OrderByClause != null)
			{
				table = this.VisitOrderByClause(table, querySpec.OrderByClause);
			}
			table = this.NewSelectedColumnsClause(table, list);
			table = this.VisitDistinct(table, querySpec.UniqueRowFilter);
			if (querySpec.TopRowFilter != null)
			{
				table = this.VisitTop(table, querySpec.TopRowFilter);
			}
			this.requireList = flag;
			return this.NewQuerySpecification(table);
		}

		// Token: 0x0600C9B5 RID: 51637 RVA: 0x00285F7D File Offset: 0x0028417D
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitDistinct(SqlExpressionVisitor<TExpression, TBinding>.Table table, UniqueRowFilter filter)
		{
			if (filter == UniqueRowFilter.NotSpecified)
			{
				return table;
			}
			if (filter == UniqueRowFilter.Distinct)
			{
				return this.NewDistinctClause(table);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9B6 RID: 51638 RVA: 0x00285F98 File Offset: 0x00284198
		protected virtual TExpression VisitScalarSubquery(ScalarSubquery scalarSubquery)
		{
			SqlExpressionVisitor<TExpression, TBinding>.Table table = this.VisitQueryExpression(scalarSubquery.QueryExpression);
			return this.NewScalarSubquery(table, this.requireList);
		}

		// Token: 0x0600C9B7 RID: 51639 RVA: 0x00285FC0 File Offset: 0x002841C0
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitTop(SqlExpressionVisitor<TExpression, TBinding>.Table table, TopRowFilter filter)
		{
			if (filter.Percent || filter.WithTies)
			{
				throw new NotSupportedException();
			}
			this.BeginScalarExpression(table);
			TExpression texpression = this.VisitScalarExpression(filter.Expression);
			texpression = this.EndScalarExpression(texpression);
			return this.NewTopClause(table, texpression);
		}

		// Token: 0x0600C9B8 RID: 51640 RVA: 0x00286008 File Offset: 0x00284208
		protected virtual TExpression VisitScalarExpression(ScalarExpression scalar)
		{
			BinaryExpression binaryExpression = scalar as BinaryExpression;
			if (binaryExpression != null)
			{
				return this.VisitBinaryExpression(binaryExpression);
			}
			PrimaryExpression primaryExpression = scalar as PrimaryExpression;
			if (primaryExpression != null)
			{
				return this.VisitPrimaryExpression(primaryExpression);
			}
			UnaryExpression unaryExpression = scalar as UnaryExpression;
			if (unaryExpression != null)
			{
				return this.VisitUnaryExpression(unaryExpression);
			}
			ValueExpression valueExpression = scalar as ValueExpression;
			if (valueExpression != null)
			{
				return this.VisitValueExpression(valueExpression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9B9 RID: 51641 RVA: 0x00286064 File Offset: 0x00284264
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitSchemaObjectFunctionTableReference(SchemaObjectFunctionTableReference schemaObjectFunctionTableRef)
		{
			List<TExpression> list = new List<TExpression>();
			for (int i = 0; i < schemaObjectFunctionTableRef.Parameters.Count; i++)
			{
				list.Add(this.VisitScalarExpression(schemaObjectFunctionTableRef.Parameters[i]));
			}
			return this.NewSchemaObjectFunctionTableReference(schemaObjectFunctionTableRef.Alias, schemaObjectFunctionTableRef.Columns, schemaObjectFunctionTableRef.SchemaObject, list);
		}

		// Token: 0x0600C9BA RID: 51642 RVA: 0x002860C0 File Offset: 0x002842C0
		protected virtual TExpression VisitSearchedCaseExpression(SearchedCaseExpression searchedCase)
		{
			List<Tuple<TExpression, TExpression>> list = new List<Tuple<TExpression, TExpression>>();
			for (int i = 0; i < searchedCase.WhenClauses.Count; i++)
			{
				TExpression texpression = this.VisitBooleanExpression(searchedCase.WhenClauses[i].WhenExpression);
				TExpression texpression2 = this.VisitScalarExpression(searchedCase.WhenClauses[i].ThenExpression);
				list.Add(Tuple.Create<TExpression, TExpression>(texpression, texpression2));
			}
			TExpression texpression3 = this.VisitScalarExpression(searchedCase.ElseExpression);
			return this.NewSearchedCaseExpression(list, texpression3);
		}

		// Token: 0x0600C9BB RID: 51643 RVA: 0x00286140 File Offset: 0x00284340
		protected virtual TExpression VisitSqlDataTypeReference(SqlDataTypeReference sqlDataTypeRef)
		{
			TExpression[] array = new TExpression[sqlDataTypeRef.Parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitLiteralExpression(sqlDataTypeRef.Parameters[i]);
			}
			return this.NewSqlDataTypeReference(sqlDataTypeRef.Name, sqlDataTypeRef.SqlDataTypeOption, array);
		}

		// Token: 0x0600C9BC RID: 51644 RVA: 0x00286198 File Offset: 0x00284398
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitTableReference(TableReference tableRef)
		{
			JoinParenthesisTableReference joinParenthesisTableReference = tableRef as JoinParenthesisTableReference;
			if (joinParenthesisTableReference != null)
			{
				return this.VisitJoinParenthesisTableReference(joinParenthesisTableReference);
			}
			JoinTableReference joinTableReference = tableRef as JoinTableReference;
			if (joinTableReference != null)
			{
				return this.VisitJoinTableReference(joinTableReference);
			}
			NamedTableReference namedTableReference = tableRef as NamedTableReference;
			if (namedTableReference != null)
			{
				return this.VisitNamedTableReference(namedTableReference);
			}
			QueryDerivedTable queryDerivedTable = tableRef as QueryDerivedTable;
			if (queryDerivedTable != null)
			{
				return this.VisitQueryDerivedTable(queryDerivedTable);
			}
			VariableTableReference variableTableReference = tableRef as VariableTableReference;
			if (variableTableReference != null)
			{
				return this.VisitVariableTableReference(variableTableReference);
			}
			SchemaObjectFunctionTableReference schemaObjectFunctionTableReference = tableRef as SchemaObjectFunctionTableReference;
			if (schemaObjectFunctionTableReference != null)
			{
				return this.VisitSchemaObjectFunctionTableReference(schemaObjectFunctionTableReference);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9BD RID: 51645 RVA: 0x0028621C File Offset: 0x0028441C
		protected virtual TExpression VisitUnaryExpression(UnaryExpression unary)
		{
			TExpression texpression = this.VisitScalarExpression(unary.Expression);
			return this.NewUnaryExpression(unary.UnaryExpressionType, texpression);
		}

		// Token: 0x0600C9BE RID: 51646 RVA: 0x00286244 File Offset: 0x00284444
		protected virtual TExpression VisitValueExpression(ValueExpression value)
		{
			Literal literal = value as Literal;
			if (literal != null)
			{
				return this.VisitLiteralExpression(literal);
			}
			VariableReference variableReference = value as VariableReference;
			if (variableReference != null)
			{
				return this.VisitVariableReferenceExpression(variableReference);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600C9BF RID: 51647 RVA: 0x0028627A File Offset: 0x0028447A
		protected virtual TExpression VisitVariableReferenceExpression(VariableReference variableReference)
		{
			return this.NewVariableReferenceExpression(variableReference.Name);
		}

		// Token: 0x0600C9C0 RID: 51648 RVA: 0x00286288 File Offset: 0x00284488
		protected virtual SqlExpressionVisitor<TExpression, TBinding>.Table VisitVariableTableReference(VariableTableReference variableTableRef)
		{
			return this.NewVariableTableReference(variableTableRef.Alias, variableTableRef.Variable.Name);
		}

		// Token: 0x0600C9C1 RID: 51649
		protected abstract Identifier NewColumnIdentifier();

		// Token: 0x0600C9C2 RID: 51650
		protected abstract Identifier NewTableIdentifier();

		// Token: 0x0600C9C3 RID: 51651 RVA: 0x002862A1 File Offset: 0x002844A1
		protected static MultiPartIdentifier NewIdentifier(Identifier identifier)
		{
			return new MultiPartIdentifier
			{
				Identifiers = { identifier }
			};
		}

		// Token: 0x0600C9C4 RID: 51652 RVA: 0x002862B4 File Offset: 0x002844B4
		protected static MultiPartIdentifier NewIdentifier(MultiPartIdentifier baseIdentifier, Identifier relativeIdentifier)
		{
			MultiPartIdentifier multiPartIdentifier = new MultiPartIdentifier();
			for (int i = 0; i < baseIdentifier.Count; i++)
			{
				multiPartIdentifier.Identifiers.Add(baseIdentifier[i]);
			}
			multiPartIdentifier.Identifiers.Add(relativeIdentifier);
			return multiPartIdentifier;
		}

		// Token: 0x0600C9C5 RID: 51653 RVA: 0x002862F8 File Offset: 0x002844F8
		protected static bool StartsWith(MultiPartIdentifier identifier, MultiPartIdentifier start)
		{
			if (start.Count <= identifier.Count)
			{
				for (int i = 0; i < start.Count; i++)
				{
					if (start[i].Value != identifier[i].Value)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600C9C6 RID: 51654 RVA: 0x00286348 File Offset: 0x00284548
		protected static bool EndsWith(MultiPartIdentifier identifier, MultiPartIdentifier end)
		{
			if (end.Count <= identifier.Count)
			{
				for (int i = 0; i < end.Count; i++)
				{
					if (end[end.Count - i - 1].Value != identifier[identifier.Count - i - 1].Value)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600C9C7 RID: 51655 RVA: 0x002863AA File Offset: 0x002845AA
		protected static IList<SqlExpressionVisitor<TExpression, TBinding>.Table> ConcatenateTables(IList<SqlExpressionVisitor<TExpression, TBinding>.Table> left, IList<SqlExpressionVisitor<TExpression, TBinding>.Table> right)
		{
			return left.Concat(right).ToArray<SqlExpressionVisitor<TExpression, TBinding>.Table>();
		}

		// Token: 0x0600C9C8 RID: 51656 RVA: 0x002863B8 File Offset: 0x002845B8
		protected static IList<SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding> ConcatenateColumns(IList<SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding> left, IList<SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding> right)
		{
			return left.Concat(right).ToArray<SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding>();
		}

		// Token: 0x040066BC RID: 26300
		private readonly SqlExpressionVisitor<TExpression, TBinding>.BindingContext context = new SqlExpressionVisitor<TExpression, TBinding>.BindingContext();

		// Token: 0x040066BD RID: 26301
		private bool requireList;

		// Token: 0x02002038 RID: 8248
		protected struct ColumnBinding
		{
			// Token: 0x17003099 RID: 12441
			// (get) Token: 0x0600C9CA RID: 51658 RVA: 0x002863D9 File Offset: 0x002845D9
			// (set) Token: 0x0600C9CB RID: 51659 RVA: 0x002863E1 File Offset: 0x002845E1
			public MultiPartIdentifier Identifier { get; set; }

			// Token: 0x1700309A RID: 12442
			// (get) Token: 0x0600C9CC RID: 51660 RVA: 0x002863EA File Offset: 0x002845EA
			// (set) Token: 0x0600C9CD RID: 51661 RVA: 0x002863F2 File Offset: 0x002845F2
			public TBinding Value { get; set; }
		}

		// Token: 0x02002039 RID: 8249
		protected class Table
		{
			// Token: 0x1700309B RID: 12443
			// (get) Token: 0x0600C9CE RID: 51662 RVA: 0x002863FB File Offset: 0x002845FB
			// (set) Token: 0x0600C9CF RID: 51663 RVA: 0x00286403 File Offset: 0x00284603
			public IList<SqlExpressionVisitor<TExpression, TBinding>.Table> Inputs { get; set; }

			// Token: 0x1700309C RID: 12444
			// (get) Token: 0x0600C9D0 RID: 51664 RVA: 0x0028640C File Offset: 0x0028460C
			// (set) Token: 0x0600C9D1 RID: 51665 RVA: 0x00286414 File Offset: 0x00284614
			public IList<SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding> Columns { get; set; }

			// Token: 0x1700309D RID: 12445
			// (get) Token: 0x0600C9D2 RID: 51666 RVA: 0x0028641D File Offset: 0x0028461D
			// (set) Token: 0x0600C9D3 RID: 51667 RVA: 0x00286425 File Offset: 0x00284625
			public TExpression Expression { get; set; }
		}

		// Token: 0x0200203A RID: 8250
		protected class BindingContext
		{
			// Token: 0x1700309E RID: 12446
			// (get) Token: 0x0600C9D5 RID: 51669 RVA: 0x0028642E File Offset: 0x0028462E
			public SqlExpressionVisitor<TExpression, TBinding>.Table Table
			{
				get
				{
					return this.tables[this.tables.Count - 1];
				}
			}

			// Token: 0x0600C9D6 RID: 51670 RVA: 0x00286448 File Offset: 0x00284648
			public void Push(SqlExpressionVisitor<TExpression, TBinding>.Table table)
			{
				this.tables.Add(table);
			}

			// Token: 0x0600C9D7 RID: 51671 RVA: 0x00286456 File Offset: 0x00284656
			public void Pop()
			{
				this.tables.RemoveAt(this.tables.Count - 1);
			}

			// Token: 0x0600C9D8 RID: 51672 RVA: 0x00286470 File Offset: 0x00284670
			public SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding GetBinding(MultiPartIdentifier identifier)
			{
				SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding columnBinding;
				if (!this.TryGetBinding(identifier, out columnBinding))
				{
					throw new KeyNotFoundException();
				}
				return columnBinding;
			}

			// Token: 0x0600C9D9 RID: 51673 RVA: 0x00286490 File Offset: 0x00284690
			public bool TryGetBinding(MultiPartIdentifier identifier, out SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding binding)
			{
				for (int i = this.tables.Count - 1; i >= 0; i--)
				{
					for (int j = 0; j < this.tables[i].Columns.Count; j++)
					{
						if (SqlExpressionVisitor<TExpression, TBinding>.EndsWith(this.tables[i].Columns[j].Identifier, identifier))
						{
							binding = this.tables[i].Columns[j];
							return true;
						}
					}
				}
				binding = default(SqlExpressionVisitor<TExpression, TBinding>.ColumnBinding);
				return false;
			}

			// Token: 0x040066C3 RID: 26307
			private readonly List<SqlExpressionVisitor<TExpression, TBinding>.Table> tables = new List<SqlExpressionVisitor<TExpression, TBinding>.Table>();
		}

		// Token: 0x0200203B RID: 8251
		protected sealed class MultiPartIdentifierEqualityComparer : IEqualityComparer<MultiPartIdentifier>
		{
			// Token: 0x0600C9DB RID: 51675 RVA: 0x000020FD File Offset: 0x000002FD
			private MultiPartIdentifierEqualityComparer()
			{
			}

			// Token: 0x0600C9DC RID: 51676 RVA: 0x00286538 File Offset: 0x00284738
			public int GetHashCode(MultiPartIdentifier obj)
			{
				int num = 0;
				for (int i = 0; i < obj.Count; i++)
				{
					num = num * 31 + obj[i].Value.GetHashCode();
				}
				return num;
			}

			// Token: 0x0600C9DD RID: 51677 RVA: 0x00286570 File Offset: 0x00284770
			public bool Equals(MultiPartIdentifier x, MultiPartIdentifier y)
			{
				if (x.Count != y.Count)
				{
					return false;
				}
				for (int i = 0; i < x.Count; i++)
				{
					if (x[i].Value != y[i].Value)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040066C4 RID: 26308
			public static readonly IEqualityComparer<MultiPartIdentifier> Instance = new SqlExpressionVisitor<TExpression, TBinding>.MultiPartIdentifierEqualityComparer();
		}
	}
}
