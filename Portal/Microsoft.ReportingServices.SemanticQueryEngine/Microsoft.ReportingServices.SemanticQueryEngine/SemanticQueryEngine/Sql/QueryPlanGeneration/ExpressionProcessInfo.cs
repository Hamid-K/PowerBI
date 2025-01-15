using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000055 RID: 85
	internal sealed class ExpressionProcessInfo : IQPExpressionInfo
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x000112C0 File Offset: 0x0000F4C0
		internal static ExpressionProcessInfo CreateForAggregateArgument(ExpressionProcessInfo aggregateInfo)
		{
			aggregateInfo.CheckAggregateExpression(true);
			if (aggregateInfo.m_aggregateArgument == null)
			{
				throw SQEAssert.AssertFalseAndThrow("m_aggregateArgument is null for aggregate expression.", Array.Empty<object>());
			}
			if (aggregateInfo.PathPointIndex != aggregateInfo.Path.Length)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			IQueryEntity queryEntity = (aggregateInfo.m_aggregateArgument.Path.IsEmpty ? aggregateInfo.InvocationPointEntity : aggregateInfo.m_aggregateArgument.Path.LastItem.TargetEntity);
			return new ExpressionProcessInfo(aggregateInfo.FunctionArguments[0], null, aggregateInfo.m_aggregateArgument.Path, queryEntity, null, aggregateInfo.PathPointIndex, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011360 File Offset: 0x0000F560
		internal static ExpressionProcessInfo CreateForFunctionArgument(ExpressionProcessInfo functionInfo, Expression argument)
		{
			if (functionInfo.PathPointIndex != functionInfo.Path.Length)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			return new ExpressionProcessInfo(argument, null, new FilteredPath(argument.Path), functionInfo.InvocationPointEntity, null, 0, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000113A4 File Offset: 0x0000F5A4
		internal static IEnumerable<ExpressionProcessInfo> CreateForDistinctAggregation(FilteredPath nonUniqueAggregationPath)
		{
			if (nonUniqueAggregationPath.IsEmpty || nonUniqueAggregationPath[0].Cardinality != Cardinality.Many)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			bool dirOneToMany = true;
			bool skipEvalPointsOnReverseToManySegment = false;
			int num;
			for (int i = 1; i < nonUniqueAggregationPath.Length; i = num)
			{
				FilteredPathItem pathItem = nonUniqueAggregationPath[i];
				if (pathItem.Evaluate)
				{
					if (dirOneToMany)
					{
						yield return ExpressionProcessInfo.CreateInfoForDistinctAggregationKey(nonUniqueAggregationPath.GetSegment(0, i));
					}
					else if (!skipEvalPointsOnReverseToManySegment)
					{
						skipEvalPointsOnReverseToManySegment = true;
						yield return ExpressionProcessInfo.CreateInfoForDistinctAggregationKey(nonUniqueAggregationPath.GetSegment(0, i));
					}
				}
				if (pathItem.ExpressionPathItem.ReverseCardinality == Cardinality.Many)
				{
					if (dirOneToMany)
					{
						dirOneToMany = false;
					}
				}
				else if (pathItem.Cardinality == Cardinality.Many && !dirOneToMany)
				{
					dirOneToMany = true;
					skipEvalPointsOnReverseToManySegment = false;
				}
				pathItem = null;
				num = i + 1;
			}
			if (!skipEvalPointsOnReverseToManySegment)
			{
				yield return ExpressionProcessInfo.CreateInfoForDistinctAggregationKey(nonUniqueAggregationPath.GetSegment(0, nonUniqueAggregationPath.Length));
			}
			yield break;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000113B4 File Offset: 0x0000F5B4
		private static ExpressionProcessInfo CreateInfoForDistinctAggregationKey(FilteredPath nonUniqueAggregationPathSegment)
		{
			return new ExpressionProcessInfo(new Expression(new EntityRefNode(nonUniqueAggregationPathSegment[nonUniqueAggregationPathSegment.Length - 1].TargetEntity)), null, nonUniqueAggregationPathSegment, null, null, 1, AggregationContext.Scalar, false, true, false);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000113EC File Offset: 0x0000F5EC
		internal static ExpressionProcessInfo CreateForGroupingExpression(Grouping grouping, IQueryEntity currentEntity)
		{
			return new ExpressionProcessInfo(grouping.Expression, grouping.Expression, new FilteredPath(grouping.Expression.Path), currentEntity, null, 0, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011424 File Offset: 0x0000F624
		internal static ExpressionProcessInfo CreateForGroupingDetail(Grouping grouping, Expression expression, IQueryEntity currentEntity)
		{
			FilteredPath filteredPath = new FilteredPath(expression.Path);
			if (grouping.Expression.Path.Length > 0)
			{
				filteredPath.InsertRange(0, grouping.Expression.Path);
			}
			return new ExpressionProcessInfo(expression, grouping.Expression, filteredPath, currentEntity, null, 0, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011478 File Offset: 0x0000F678
		internal static ExpressionProcessInfo CreateForEntityFilter(Expression expression, IQueryEntity currentEntity)
		{
			return new ExpressionProcessInfo(expression, null, new FilteredPath(expression.Path), currentEntity, null, 0, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000114A0 File Offset: 0x0000F6A0
		internal static ExpressionProcessInfo CreateForMeasure(TotalAggregationPathItem totalAggregationPathItem, Expression expression)
		{
			if (expression.Path.Length > 0 || expression.IsSubtreeAnchored())
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			ExpressionProcessInfo.AggregateArgument aggregateArgument = null;
			if (ExpressionProcessInfo.CheckAggregateExpression(expression, false))
			{
				aggregateArgument = new ExpressionProcessInfo.AggregateArgument(expression.NodeAsFunction.Arguments[0], null, totalAggregationPathItem);
			}
			return new ExpressionProcessInfo(expression, null, new FilteredPath(expression.Path), null, aggregateArgument, 0, AggregationContext.Scalar, false, false, false);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011508 File Offset: 0x0000F708
		internal static ExpressionProcessInfo CreateForNestedQuery(ExpressionProcessInfo parentInfo, AggregationContext nestedAggContext)
		{
			return new ExpressionProcessInfo(parentInfo.m_expression, parentInfo.m_groupingExpression, parentInfo.m_expressionPath, parentInfo.Path[parentInfo.m_pathPointIndex].TargetEntity, parentInfo.m_aggregateArgument, parentInfo.m_pathPointIndex + 1, nestedAggContext, parentInfo.m_invokedAggregate, parentInfo.m_distinctKey, false);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011560 File Offset: 0x0000F760
		internal static ExpressionProcessInfo CreateForWrappedQuery(ExpressionProcessInfo wrapperQueryExpressionInfo, AggregationContext wrappedQueryAggContext)
		{
			return new ExpressionProcessInfo(wrapperQueryExpressionInfo.m_expression, wrapperQueryExpressionInfo.m_groupingExpression, wrapperQueryExpressionInfo.m_expressionPath, wrapperQueryExpressionInfo.m_currentEntity, wrapperQueryExpressionInfo.m_aggregateArgument, wrapperQueryExpressionInfo.m_pathPointIndex, wrappedQueryAggContext, wrapperQueryExpressionInfo.m_invokedAggregate, false, false);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000115A0 File Offset: 0x0000F7A0
		internal static ExpressionProcessInfo CreateForNullSelect(Expression selectAsNullExpression)
		{
			return new ExpressionProcessInfo(selectAsNullExpression, null, new FilteredPath(), null, null, 0, AggregationContext.Scalar, false, false, true);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000115C0 File Offset: 0x0000F7C0
		internal void SetOwner(SqlQuery owner)
		{
			if (this.m_owner != null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.m_owner = owner;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000115D8 File Offset: 0x0000F7D8
		internal void CreateAndSetNestedQueryKey()
		{
			int num = ((this.m_pathPointIndexBeforeAdjusting < 0) ? this.m_pathPointIndex : this.m_pathPointIndexBeforeAdjusting);
			this.m_nestedQueryKey = new NestedQueryKey(this, num);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001160A File Offset: 0x0000F80A
		internal void AdjustPathPointIndex(int newPathPointIndex)
		{
			if (this.m_pathPointIndexBeforeAdjusting < 0)
			{
				this.m_pathPointIndexBeforeAdjusting = this.m_pathPointIndex;
			}
			this.m_pathPointIndex = newPathPointIndex;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011628 File Offset: 0x0000F828
		internal bool CheckAggregateExpression(bool throwException)
		{
			return ExpressionProcessInfo.CheckAggregateExpression(this.m_expression, throwException);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011636 File Offset: 0x0000F836
		internal bool GetNonTransitiveProcessingRequired()
		{
			if (this.IsInvokedAggregate)
			{
				this.CheckAggregateExpression(true);
				if (this.Path.GetCardinalityContext() == CardinalityContext.NonUniqueSet)
				{
					return true;
				}
				if (ExpressionProcessInfo.IsNonTransitiveAggregate(this.m_expression.NodeAsFunction))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001166D File Offset: 0x0000F86D
		internal bool IsBlob(QueryPlanBuilder qpBuilder)
		{
			if (this.__isBlob == null)
			{
				this.__isBlob = new bool?(ExpressionProcessInfo.CheckBlob(this.m_expression, qpBuilder));
			}
			return this.__isBlob.Value;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0001169E File Offset: 0x0000F89E
		internal int SerialID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_serialID;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000116A8 File Offset: 0x0000F8A8
		internal IList<Expression> FunctionArguments
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_expression.NodeAsFunction == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				if (!this.CheckAggregateExpression(false))
				{
					return this.m_expression.NodeAsFunction.Arguments;
				}
				if (this.m_aggregateArgument == null)
				{
					throw SQEAssert.AssertFalseAndThrow("m_aggregateArgument is null for aggregate expression.", Array.Empty<object>());
				}
				return this.m_aggregateArgument.ArgumentExpression;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00011708 File Offset: 0x0000F908
		internal bool IsBooleanArgument(int argIndex)
		{
			int count = this.FunctionArguments.Count;
			if (argIndex < 0 && argIndex >= count)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("argIndex"));
			}
			FunctionName functionName = this.m_expression.NodeAsFunction.FunctionName;
			if (functionName - FunctionName.And > 2)
			{
				switch (functionName)
				{
				case FunctionName.If:
					return argIndex == 0;
				case FunctionName.Switch:
					return argIndex % 2 == 0;
				case FunctionName.Filter:
					return argIndex == 1;
				}
				return false;
			}
			return true;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00011780 File Offset: 0x0000F980
		internal FunctionNode NodeAsFunction
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.NodeAsFunction;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001178D File Offset: 0x0000F98D
		internal AttributeRefNode NodeAsAttributeRef
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.NodeAsAttributeRef;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001179A File Offset: 0x0000F99A
		internal EntityRefNode NodeAsEntityRef
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.NodeAsEntityRef;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000117A7 File Offset: 0x0000F9A7
		internal LiteralNode NodeAsLiteral
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.NodeAsLiteral;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000117B4 File Offset: 0x0000F9B4
		internal NullNode NodeAsNull
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.NodeAsNull;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000117C1 File Offset: 0x0000F9C1
		internal FilteredPath Path
		{
			[DebuggerStepThrough]
			get
			{
				return this.__activePath;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000117C9 File Offset: 0x0000F9C9
		internal int PathPointIndex
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathPointIndex;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000117D1 File Offset: 0x0000F9D1
		internal AggregationContext AggregationContext
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_aggContext;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000117D9 File Offset: 0x0000F9D9
		internal bool IsInvokedAggregate
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_invokedAggregate;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000117E1 File Offset: 0x0000F9E1
		internal bool IsDistinctKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_distinctKey;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000117E9 File Offset: 0x0000F9E9
		internal NestedQueryKey NestedQueryKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nestedQueryKey;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000117F1 File Offset: 0x0000F9F1
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000117F9 File Offset: 0x0000F9F9
		internal SqlNestedQuery NestedQuery
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nestedQuery;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_nestedQuery = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00011802 File Offset: 0x0000FA02
		internal Expression ObjKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001180A File Offset: 0x0000FA0A
		internal Expression GroupingExpressionObjKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_groupingExpression;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011814 File Offset: 0x0000FA14
		public override string ToString()
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("E={0} PPI={1} AC={2} IA{3} P={4} NQK={5} NQ={6}", new object[]
			{
				this.ExprToStr(),
				this.m_pathPointIndex,
				this.m_aggContext,
				this.m_invokedAggregate ? "+" : "-",
				this.Path,
				this.NestedQueryKeyToStr(this.m_nestedQueryKey),
				this.NestedQueryToStr()
			});
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00011802 File Offset: 0x0000FA02
		Expression IQPExpressionInfo.Expression
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001188E File Offset: 0x0000FA8E
		Expression IQPExpressionInfo.ObjKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.ObjKey;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00011896 File Offset: 0x0000FA96
		bool IQPExpressionInfo.IsInnerMost
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathPointIndex == this.Path.Length;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000118AB File Offset: 0x0000FAAB
		void IQPExpressionInfo.CheckAggregateExpression()
		{
			this.CheckAggregateExpression(true);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000118B5 File Offset: 0x0000FAB5
		Expression IQPExpressionInfo.AggregateArgument
		{
			[DebuggerStepThrough]
			get
			{
				this.CheckAggregateExpression(true);
				return this.FunctionArguments[0];
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000118CB File Offset: 0x0000FACB
		bool IQPExpressionInfo.MustAggregate
		{
			[DebuggerStepThrough]
			get
			{
				return this.MustAggregate;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000118D3 File Offset: 0x0000FAD3
		bool IQPExpressionInfo.MustAggregateDegenerate
		{
			[DebuggerStepThrough]
			get
			{
				return this.MustAggregateDegenerate;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000118DB File Offset: 0x0000FADB
		bool IQPExpressionInfo.IsAggregateInvocationPoint
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_invokedAggregate && this.m_pathPointIndex == 0 && !this.m_owner.IsWrapperQuery;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00011900 File Offset: 0x0000FB00
		bool IQPExpressionInfo.IsInnerMostAggregation
		{
			get
			{
				if (this.MustAggregate)
				{
					ExpressionProcessInfo expressionProcessInfo = this;
					while (expressionProcessInfo.NestedQuery != null)
					{
						expressionProcessInfo = expressionProcessInfo.NestedQuery.SelectExpressions[expressionProcessInfo.ObjKey];
						if (expressionProcessInfo.MustAggregate)
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00011944 File Offset: 0x0000FB44
		bool IQPExpressionInfo.Nullable
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression.GetResultType().Nullable;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00011964 File Offset: 0x0000FB64
		private ExpressionProcessInfo(Expression expression, Expression groupingExpression, FilteredPath expressionPath, IQueryEntity currentEntity, ExpressionProcessInfo.AggregateArgument aggregateArgument, int pathPointIndex, AggregationContext aggContext, bool invokedAggregate, bool distinctKey, bool selectAsNull)
		{
			if (expression == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("expression"));
			}
			if (expressionPath == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("expressionPath"));
			}
			this.m_expression = expression;
			this.m_groupingExpression = groupingExpression;
			this.m_expressionPath = expressionPath;
			this.m_currentEntity = currentEntity;
			bool flag = !selectAsNull && ExpressionProcessInfo.CheckAggregateExpression(expression, false);
			if (aggregateArgument == null && flag)
			{
				this.m_aggregateArgument = new ExpressionProcessInfo.AggregateArgument(expression.NodeAsFunction.Arguments[0], this.InvocationPointEntity, null);
			}
			else
			{
				this.m_aggregateArgument = aggregateArgument;
			}
			this.m_pathPointIndex = pathPointIndex;
			this.m_aggContext = aggContext;
			this.m_invokedAggregate = invokedAggregate;
			this.m_distinctKey = distinctKey;
			if (flag && !this.m_invokedAggregate && this.m_pathPointIndex >= this.m_expressionPath.Length)
			{
				this.InvokeAggregate();
			}
			this.SetupActivePath();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00011A60 File Offset: 0x0000FC60
		private void InvokeAggregate()
		{
			if (this.m_invokedAggregate)
			{
				throw SQEAssert.AssertFalseAndThrow("m_invokedAggregate is already true.", Array.Empty<object>());
			}
			this.m_invokedAggregate = true;
			this.m_pathPointIndex = 0;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00011A88 File Offset: 0x0000FC88
		private void SetupActivePath()
		{
			if (!this.m_invokedAggregate)
			{
				this.__activePath = this.m_expressionPath;
				return;
			}
			this.CheckAggregateExpression(true);
			if (this.m_aggregateArgument == null)
			{
				throw SQEAssert.AssertFalseAndThrow("m_aggregateArgument is null for aggregate expression.", Array.Empty<object>());
			}
			this.__activePath = this.m_aggregateArgument.Path;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011ADC File Offset: 0x0000FCDC
		private static bool CheckAggregateExpression(Expression expression, bool throwException)
		{
			FunctionNode nodeAsFunction = expression.NodeAsFunction;
			if (nodeAsFunction != null)
			{
				FunctionInfo functionInfo = nodeAsFunction.GetFunctionInfo();
				if (functionInfo.IsAggregate && functionInfo.Arguments.Count == 1 && nodeAsFunction.Arguments.Count == 1)
				{
					return true;
				}
			}
			if (throwException)
			{
				throw SQEAssert.AssertFalseAndThrow("Unknown or invalid aggregate expression: {0}.", new object[] { expression });
			}
			return false;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011B3C File Offset: 0x0000FD3C
		private static bool IsNonTransitiveAggregate(FunctionNode fNode)
		{
			FunctionInfo functionInfo = fNode.GetFunctionInfo();
			if (!functionInfo.IsAggregate)
			{
				return false;
			}
			FunctionName functionName = functionInfo.FunctionName;
			return functionName != FunctionName.Count && !functionInfo.IsTransitive;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011B74 File Offset: 0x0000FD74
		private static bool CheckBlob(Expression expression, QueryPlanBuilder qpBuilder)
		{
			DataType dataType = expression.GetResultType().DataType;
			if (dataType != DataType.String && dataType != DataType.Binary)
			{
				return false;
			}
			if (expression.NodeAsAttributeRef != null)
			{
				if (expression.NodeAsAttributeRef.Attribute == null || expression.NodeAsAttributeRef.Attribute.ModelAttribute == null || expression.NodeAsAttributeRef.Attribute.ModelAttribute.Binding == null)
				{
					throw SQEAssert.AssertFalseAndThrow("AttributeRefNode must point to a column-bound model attribute.", Array.Empty<object>());
				}
				ColumnBinding binding = expression.NodeAsAttributeRef.Attribute.ModelAttribute.Binding;
				return qpBuilder.IsBlob(binding);
			}
			else
			{
				if (expression.NodeAsFunction != null)
				{
					using (List<Expression>.Enumerator enumerator = expression.NodeAsFunction.Arguments.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (ExpressionProcessInfo.CheckBlob(enumerator.Current, qpBuilder))
							{
								return true;
							}
						}
					}
					return false;
				}
				if (expression.NodeAsEntityRef != null || expression.NodeAsLiteral != null || expression.NodeAsNull != null)
				{
					return false;
				}
				throw SQEAssert.AssertFalseAndThrow("Invalid expression node.", Array.Empty<object>());
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011C8C File Offset: 0x0000FE8C
		private string ExprToStr()
		{
			if (this.m_expression.NodeAsAttributeRef != null)
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("ARef({0})", new object[] { this.m_expression.NodeAsAttributeRef.ModelAttribute });
			}
			if (this.m_expression.NodeAsEntityRef != null)
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("ERef({0})", new object[] { this.m_expression.NodeAsEntityRef.ModelEntity });
			}
			if (this.m_expression.NodeAsFunction != null)
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Func({0})", new object[] { this.m_expression.NodeAsFunction.FunctionName });
			}
			if (this.m_expression.NodeAsLiteral != null)
			{
				return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("L[{0}]", new object[] { this.m_expression.NodeAsLiteral });
			}
			if (this.m_expression.NodeAsNull != null)
			{
				return "NULL";
			}
			return "unknown expression node";
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011D75 File Offset: 0x0000FF75
		private string NestedQueryKeyToStr(NestedQueryKey nestedQueryKey)
		{
			if (nestedQueryKey == null)
			{
				return null;
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("({0})", new object[] { nestedQueryKey.ToString() });
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00011D98 File Offset: 0x0000FF98
		private string NestedQueryToStr()
		{
			if (this.m_nestedQuery == null)
			{
				return null;
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("(PA{0} NQK={1})", new object[]
			{
				this.m_nestedQuery.PerformAggregation ? "+" : "-",
				(this.m_nestedQueryKey == this.m_nestedQuery.Key) ? "this.NQK" : this.NestedQueryKeyToStr(this.m_nestedQuery.Key)
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00011E09 File Offset: 0x00010009
		private bool MustAggregate
		{
			get
			{
				if (this.m_owner == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				return this.m_aggContext == AggregationContext.Aggregate || (this.m_aggContext == AggregationContext.Inner && this.m_owner.PerformAggregation) || this.MustAggregateDegenerate;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00011E40 File Offset: 0x00010040
		private bool MustAggregateDegenerate
		{
			get
			{
				if (this.m_pathPointIndex != this.Path.Length || !this.CheckAggregateExpression(false) || this.m_aggContext != AggregationContext.Scalar)
				{
					return false;
				}
				if (this.Path.GetCardinality() != Cardinality.One)
				{
					throw SQEAssert.AssertFalseAndThrow("Found degenerate aggregate on a ToMany path.", Array.Empty<object>());
				}
				return true;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011E91 File Offset: 0x00010091
		private IQueryEntity InvocationPointEntity
		{
			get
			{
				if (this.m_expressionPath.IsEmpty)
				{
					return this.m_currentEntity;
				}
				return this.m_expressionPath.LastItem.TargetEntity;
			}
		}

		// Token: 0x040001BB RID: 443
		private readonly Expression m_expression;

		// Token: 0x040001BC RID: 444
		private readonly Expression m_groupingExpression;

		// Token: 0x040001BD RID: 445
		private readonly FilteredPath m_expressionPath;

		// Token: 0x040001BE RID: 446
		private readonly IQueryEntity m_currentEntity;

		// Token: 0x040001BF RID: 447
		private ExpressionProcessInfo.AggregateArgument m_aggregateArgument;

		// Token: 0x040001C0 RID: 448
		private FilteredPath __activePath;

		// Token: 0x040001C1 RID: 449
		private int m_pathPointIndex;

		// Token: 0x040001C2 RID: 450
		private int m_pathPointIndexBeforeAdjusting = -1;

		// Token: 0x040001C3 RID: 451
		private readonly AggregationContext m_aggContext;

		// Token: 0x040001C4 RID: 452
		private bool m_invokedAggregate;

		// Token: 0x040001C5 RID: 453
		private readonly bool m_distinctKey;

		// Token: 0x040001C6 RID: 454
		private NestedQueryKey m_nestedQueryKey;

		// Token: 0x040001C7 RID: 455
		private SqlNestedQuery m_nestedQuery;

		// Token: 0x040001C8 RID: 456
		private SqlQuery m_owner;

		// Token: 0x040001C9 RID: 457
		private bool? __isBlob;

		// Token: 0x040001CA RID: 458
		private static int SerialIDCounter;

		// Token: 0x040001CB RID: 459
		private readonly int m_serialID = Interlocked.Increment(ref ExpressionProcessInfo.SerialIDCounter);

		// Token: 0x020000CC RID: 204
		private sealed class AggregateArgument
		{
			// Token: 0x0600073A RID: 1850 RVA: 0x0001BF9C File Offset: 0x0001A19C
			internal AggregateArgument(Expression aggregateArgument, IQueryEntity invocationPointEntity, TotalAggregationPathItem totalAggregationPathItem)
			{
				if (totalAggregationPathItem != null)
				{
					this.m_path.Add(new FilteredPathItem(totalAggregationPathItem));
				}
				bool flag = false;
				while (aggregateArgument.NodeAsFunction != null && aggregateArgument.NodeAsFunction.GetFunctionInfo().IsPassthrough)
				{
					if (aggregateArgument.Path.Length > 0)
					{
						this.m_path.AddRange(aggregateArgument.Path);
						if (flag)
						{
							this.m_path[this.m_path.Length - aggregateArgument.Path.Length].Evaluate = true;
							flag = false;
						}
					}
					else if (aggregateArgument.NodeAsFunction.FunctionName == FunctionName.Filter)
					{
						if (totalAggregationPathItem != null && this.m_path.Length == 1)
						{
							if (this.m_path[0].ExpressionPathItem != totalAggregationPathItem)
							{
								throw SQEAssert.AssertFalseAndThrow();
							}
						}
						else
						{
							this.m_path.Add(new FilteredPathItem(new SelfPathItem(this.m_path.IsEmpty ? invocationPointEntity : this.m_path.LastItem.TargetEntity)));
							if (this.m_path.LastItem.Cardinality != Cardinality.One || this.m_path.LastItem.ReverseCardinality != Cardinality.One)
							{
								throw SQEAssert.AssertFalseAndThrow();
							}
						}
					}
					FunctionName functionName = aggregateArgument.NodeAsFunction.FunctionName;
					if (functionName != FunctionName.Filter)
					{
						if (functionName != FunctionName.Evaluate)
						{
							throw SQEAssert.AssertFalseAndThrow("Unknown passthru function: {0}.", new object[] { aggregateArgument.NodeAsFunction.FunctionName });
						}
						flag = true;
					}
					else
					{
						this.m_path.LastItem.FilterCondition = aggregateArgument.NodeAsFunction.Arguments[1];
					}
					aggregateArgument = aggregateArgument.NodeAsFunction.Arguments[aggregateArgument.NodeAsFunction.GetFunctionInfo().PassthroughArgument.Value];
				}
				this.m_path.AddRange(aggregateArgument.Path);
				if (flag && !aggregateArgument.Path.IsEmpty)
				{
					this.m_path[this.m_path.Length - aggregateArgument.Path.Length].Evaluate = true;
				}
				this.m_path.CanonicalizeEvals();
				this.m_aggregateArgument = new Expression[] { aggregateArgument };
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001C1D3 File Offset: 0x0001A3D3
			internal FilteredPath Path
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_path;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001C1DB File Offset: 0x0001A3DB
			internal IList<Expression> ArgumentExpression
			{
				[DebuggerStepThrough]
				get
				{
					if (this.m_aggregateArgument == null || this.m_aggregateArgument.Count != 1)
					{
						throw SQEAssert.AssertFalseAndThrow();
					}
					return this.m_aggregateArgument;
				}
			}

			// Token: 0x0400039B RID: 923
			private readonly FilteredPath m_path = new FilteredPath();

			// Token: 0x0400039C RID: 924
			private readonly IList<Expression> m_aggregateArgument;
		}
	}
}
