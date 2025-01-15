using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F7A RID: 3962
	internal abstract class AdobeAnalyticsQueryCompiler<TDescription> where TDescription : class
	{
		// Token: 0x0600685C RID: 26716 RVA: 0x00166CDB File Offset: 0x00164EDB
		public AdobeAnalyticsQueryCompiler(AdobeAnalyticsCube cube, IList<ParameterArguments> arguments)
		{
			this.cube = cube;
			this.arguments = arguments;
		}

		// Token: 0x0600685D RID: 26717
		protected abstract TDescription CreateDescription(AdobeAnalyticsReportBuilder builder);

		// Token: 0x0600685E RID: 26718
		protected abstract bool TryApplyDateRangeParameters(AdobeAnalyticsReportBuilder builder, IEnumerable<ParameterArguments> dateParameters);

		// Token: 0x0600685F RID: 26719 RVA: 0x00166CF4 File Offset: 0x00164EF4
		public TDescription Compile(QueryCubeExpression expression)
		{
			TDescription tdescription;
			if (!this.TryCompile(expression, out tdescription))
			{
				throw new InvalidOperationException("CubeExpression could not be folded");
			}
			return tdescription;
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x00166D18 File Offset: 0x00164F18
		public bool CanCompile(QueryCubeExpression expression)
		{
			TDescription tdescription;
			return this.TryCompile(expression, out tdescription);
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x00166D30 File Offset: 0x00164F30
		protected bool TryCompileSort(IList<CubeSortOrder> sortOrders, IList<IdentifierCubeExpression> dimensions, out string sortBy)
		{
			if (sortOrders.Count == 1 && dimensions.Count == 1 && sortOrders[0].Expression.Kind == CubeExpressionKind.Identifier)
			{
				IdentifierCubeExpression sortOrderId = (IdentifierCubeExpression)sortOrders[0].Expression;
				if (!sortOrders[0].Ascending && this.cube.Measures.Any((AdobeAnalyticsMeasure m) => m.Id == sortOrderId.Identifier))
				{
					sortBy = sortOrderId.Identifier;
					return true;
				}
			}
			sortBy = null;
			return false;
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x00166DC0 File Offset: 0x00164FC0
		protected bool TryCompileExpression(CubeExpression expression, out AdobeAnalyticsExpression adobeExpression)
		{
			switch (expression.Kind)
			{
			case CubeExpressionKind.Constant:
				return this.TryCompileConstant((ConstantCubeExpression)expression, out adobeExpression);
			case CubeExpressionKind.Identifier:
				return this.TryCompileIdentifier((IdentifierCubeExpression)expression, out adobeExpression);
			case CubeExpressionKind.Binary:
				return this.TryCompileBinaryFilter((BinaryCubeExpression)expression, out adobeExpression);
			case CubeExpressionKind.Invocation:
				return this.TryCompileInvocationFilter((InvocationCubeExpression)expression, out adobeExpression);
			}
			adobeExpression = null;
			return false;
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x00166E2C File Offset: 0x0016502C
		protected bool TryCompileIdentifier(IdentifierCubeExpression expression, out AdobeAnalyticsExpression adobeIdentifer)
		{
			adobeIdentifer = new AdobeAnalyticsIdentifierExpression(expression.Identifier);
			return true;
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x00166E3C File Offset: 0x0016503C
		protected bool TryCompileConstant(ConstantCubeExpression expression, out AdobeAnalyticsExpression adobeIdentifer)
		{
			if (expression.Value.IsText)
			{
				adobeIdentifer = new AdobeAnalyticsConstantExpression(expression.Value.AsString);
				return true;
			}
			adobeIdentifer = null;
			return false;
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x00166E64 File Offset: 0x00165064
		protected bool TryCompileBinaryFilter(BinaryCubeExpression expression, out AdobeAnalyticsExpression adobeFilter)
		{
			AdobeAnalyticsExpression adobeAnalyticsExpression;
			AdobeAnalyticsExpression adobeAnalyticsExpression2;
			if (this.TryCompileExpression(expression.Left, out adobeAnalyticsExpression) && this.TryCompileExpression(expression.Right, out adobeAnalyticsExpression2))
			{
				switch (expression.Operator)
				{
				case BinaryOperator2.Equals:
				{
					string text;
					string text2;
					if ((adobeAnalyticsExpression.TryGetIdentifier(out text) && adobeAnalyticsExpression2.TryGetConstant(out text2)) || (adobeAnalyticsExpression2.TryGetIdentifier(out text) && adobeAnalyticsExpression.TryGetConstant(out text2)))
					{
						adobeFilter = AdobeAnalyticsFilterExpression.New(AdobeAnalyticsDimensionFilterKind.Select, text, text2);
						return true;
					}
					break;
				}
				case BinaryOperator2.And:
					if (adobeAnalyticsExpression.Kind == AdobeAnalyticsExpressionKind.Filter && adobeAnalyticsExpression2.Kind == AdobeAnalyticsExpressionKind.Filter)
					{
						return ((AdobeAnalyticsFilterExpression)adobeAnalyticsExpression).TryMergeAnd((AdobeAnalyticsFilterExpression)adobeAnalyticsExpression2, out adobeFilter);
					}
					break;
				case BinaryOperator2.Or:
					if (adobeAnalyticsExpression.Kind == AdobeAnalyticsExpressionKind.Filter && adobeAnalyticsExpression2.Kind == AdobeAnalyticsExpressionKind.Filter)
					{
						return ((AdobeAnalyticsFilterExpression)adobeAnalyticsExpression).TryMergeOr((AdobeAnalyticsFilterExpression)adobeAnalyticsExpression2, out adobeFilter);
					}
					break;
				}
			}
			adobeFilter = null;
			return false;
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x00166F3C File Offset: 0x0016513C
		protected bool TryCompileInvocationFilter(InvocationCubeExpression expression, out AdobeAnalyticsExpression adobeFilter)
		{
			IdentifierCubeExpression identifierExpression;
			ConstantCubeExpression constantCubeExpression;
			ConstantCubeExpression constantCubeExpression2;
			if (expression.Function.TryGetConstant(out constantCubeExpression) && expression.Arguments.Count == 2 && expression.Arguments[0].TryGetIdentifier(out identifierExpression) && expression.Arguments[1].TryGetConstant(out constantCubeExpression2) && this.cube.Dimensions.Any((AdobeAnalyticsDimension d) => d.Id == identifierExpression.Identifier) && constantCubeExpression.Value.IsFunction && constantCubeExpression2.Value.IsText)
			{
				string text = null;
				if (constantCubeExpression.Value.Equals(Library.Text.StartsWith))
				{
					text = "^" + constantCubeExpression2.Value.AsString;
				}
				else if (constantCubeExpression.Equals(Library.Text.EndsWith))
				{
					text = constantCubeExpression2.Value.AsString + "$";
				}
				else if (constantCubeExpression.Equals(Library.Text.Contains))
				{
					text = constantCubeExpression2.Value.AsString;
				}
				if (text != null)
				{
					adobeFilter = AdobeAnalyticsFilterExpression.New(AdobeAnalyticsDimensionFilterKind.And, identifierExpression.Identifier, text);
					return true;
				}
			}
			adobeFilter = null;
			return false;
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x00167070 File Offset: 0x00165270
		private bool TryCompile(QueryCubeExpression query, out TDescription reportDescription)
		{
			if (query.From.Kind != CubeExpressionKind.Identifier)
			{
				reportDescription = default(TDescription);
				return false;
			}
			AdobeAnalyticsReportBuilder adobeAnalyticsReportBuilder = new AdobeAnalyticsReportBuilder();
			adobeAnalyticsReportBuilder.ReportSuiteId = ((IdentifierCubeExpression)query.From).Identifier;
			foreach (IdentifierCubeExpression identifierCubeExpression in query.Measures)
			{
				adobeAnalyticsReportBuilder.Measures.Add(identifierCubeExpression.Identifier);
			}
			foreach (IdentifierCubeExpression identifierCubeExpression2 in query.DimensionAttributes)
			{
				adobeAnalyticsReportBuilder.Dimensions.Add(identifierCubeExpression2.Identifier);
			}
			IEnumerable<ParameterArguments> enumerable = this.arguments.Where((ParameterArguments a) => a.Parameter.Identifier == "DateRange");
			if (!this.TryApplyDateRangeParameters(adobeAnalyticsReportBuilder, enumerable))
			{
				reportDescription = default(TDescription);
				return false;
			}
			enumerable = this.arguments.Where((ParameterArguments a) => a.Parameter.Identifier == "Segment");
			if (enumerable.Any<ParameterArguments>())
			{
				ParameterArguments parameterArguments = enumerable.First<ParameterArguments>();
				if (enumerable.Count<ParameterArguments>() == 1 && parameterArguments.Values.Length == 1 && parameterArguments.Values[0].IsList)
				{
					using (IEnumerator<IValueReference> enumerator2 = parameterArguments.Values[0].AsList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							IValueReference valueReference = enumerator2.Current;
							adobeAnalyticsReportBuilder.Segments.Add(valueReference.Value.AsString);
						}
						goto IL_01A3;
					}
				}
				reportDescription = default(TDescription);
				return false;
			}
			IL_01A3:
			using (IEnumerator<ParameterArguments> enumerator3 = this.arguments.Where((ParameterArguments a) => a.Parameter.Identifier == "Top").GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ParameterArguments top = enumerator3.Current;
					if (top.Values.Length != 2 || !top.Values[0].IsNumber || !top.Values[0].AsNumber.IsInteger32)
					{
						reportDescription = default(TDescription);
						return false;
					}
					int num = top.Values[0].AsNumber.ToInt32();
					if (top.Values[1].IsNull || string.IsNullOrEmpty(top.Values[1].AsString))
					{
						using (IEnumerator<string> enumerator4 = adobeAnalyticsReportBuilder.Dimensions.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								string text = enumerator4.Current;
								adobeAnalyticsReportBuilder.DimensionToTop[text] = num;
							}
							continue;
						}
					}
					if (!this.cube.Dimensions.Any((AdobeAnalyticsDimension d) => d.Id == top.Values[1].AsString))
					{
						reportDescription = default(TDescription);
						return false;
					}
					adobeAnalyticsReportBuilder.DimensionToTop[top.Values[1].AsString] = num;
				}
			}
			string text2 = null;
			if (query.Sort != null && query.Sort.Count > 0 && !this.TryCompileSort(query.Sort, query.DimensionAttributes, out text2))
			{
				reportDescription = default(TDescription);
				return false;
			}
			adobeAnalyticsReportBuilder.SortBy = text2;
			if (query.Filter != null)
			{
				AdobeAnalyticsExpression adobeAnalyticsExpression = null;
				if (!this.TryCompileExpression(query.Filter, out adobeAnalyticsExpression) || adobeAnalyticsExpression.Kind != AdobeAnalyticsExpressionKind.Filter)
				{
					reportDescription = default(TDescription);
					return false;
				}
				adobeAnalyticsReportBuilder.Filter = (AdobeAnalyticsFilterExpression)adobeAnalyticsExpression;
			}
			if (!query.RowRange.IsAll)
			{
				reportDescription = default(TDescription);
				return false;
			}
			reportDescription = this.CreateDescription(adobeAnalyticsReportBuilder);
			return true;
		}

		// Token: 0x0400397A RID: 14714
		protected IList<ParameterArguments> arguments;

		// Token: 0x0400397B RID: 14715
		protected AdobeAnalyticsCube cube;
	}
}
