using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B27 RID: 2855
	internal class GoogleAnalyticsQueryCompilerV1 : IGoogleAnalyticsQueryCompiler
	{
		// Token: 0x06004F03 RID: 20227 RVA: 0x00105D64 File Offset: 0x00103F64
		public GoogleAnalyticsQueryCompilerV1(IGoogleAnalyticsCube cube)
		{
			this.cube = cube;
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x00105D74 File Offset: 0x00103F74
		public bool CanCompile(QueryCubeExpression expression)
		{
			GoogleAnalyticsQueryExpression googleAnalyticsQueryExpression;
			return this.TryCompile(expression, out googleAnalyticsQueryExpression);
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x00105D8C File Offset: 0x00103F8C
		public GoogleAnalyticsExpression Compile(QueryCubeExpression expression)
		{
			GoogleAnalyticsQueryExpression googleAnalyticsQueryExpression;
			if (!this.TryCompile(expression, out googleAnalyticsQueryExpression))
			{
				throw new InvalidOperationException("CubeExpression could not be folded");
			}
			return googleAnalyticsQueryExpression;
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x00105DB0 File Offset: 0x00103FB0
		private bool TryCompile(QueryCubeExpression expression, out GoogleAnalyticsQueryExpression ga)
		{
			expression = this.FlattenSubcubes(expression);
			return this.TryCompileQueryExpression(expression, out ga);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00105DC4 File Offset: 0x00103FC4
		private QueryCubeExpression FlattenSubcubes(QueryCubeExpression expression)
		{
			if (expression.From.Kind == CubeExpressionKind.Query)
			{
				QueryCubeExpression queryCubeExpression = this.FlattenSubcubes((QueryCubeExpression)expression.From);
				if ((queryCubeExpression.Filter == null || !queryCubeExpression.Filter.GetReferences().Any((IdentifierCubeExpression r) => !this.IsDimensionAttribute(r))) && queryCubeExpression.RowRange.IsAll)
				{
					CubeExpression cubeExpression;
					if (queryCubeExpression.Filter == null)
					{
						cubeExpression = expression.Filter;
					}
					else if (expression.Filter == null)
					{
						cubeExpression = queryCubeExpression.Filter;
					}
					else
					{
						cubeExpression = new BinaryCubeExpression(BinaryOperator2.And, expression.Filter, queryCubeExpression.Filter);
					}
					IList<CubeSortOrder> list = expression.Sort.Union(queryCubeExpression.Sort).ToArray<CubeSortOrder>();
					return new QueryCubeExpression(queryCubeExpression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, cubeExpression, list, expression.RowRange);
				}
			}
			return expression;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00105EA8 File Offset: 0x001040A8
		private bool TryCompileSort(CubeSortOrder sort, out GoogleAnalyticsSortOrder ga)
		{
			GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression;
			if (sort.Expression.Kind == CubeExpressionKind.Identifier && this.TryCompileIdentifierFilter((IdentifierCubeExpression)sort.Expression, out googleAnalyticsFilterExpression))
			{
				ga = new GoogleAnalyticsSortOrder((GoogleAnalyticsIdentifierExpression)googleAnalyticsFilterExpression, sort.Ascending);
				return true;
			}
			ga = null;
			return false;
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x00105EF4 File Offset: 0x001040F4
		private bool TryConvertBinaryOperator(BinaryOperator2 op, out GoogleAnalyticsBinaryOperator googleOp)
		{
			switch (op)
			{
			case BinaryOperator2.GreaterThan:
				googleOp = GoogleAnalyticsBinaryOperator.GreaterThan;
				return true;
			case BinaryOperator2.LessThan:
				googleOp = GoogleAnalyticsBinaryOperator.LessThan;
				return true;
			case BinaryOperator2.GreaterThanOrEquals:
				googleOp = GoogleAnalyticsBinaryOperator.GreaterThanOrEqual;
				return true;
			case BinaryOperator2.LessThanOrEquals:
				googleOp = GoogleAnalyticsBinaryOperator.LessThanOrEqual;
				return true;
			case BinaryOperator2.Equals:
				googleOp = GoogleAnalyticsBinaryOperator.Equal;
				return true;
			case BinaryOperator2.NotEquals:
				googleOp = GoogleAnalyticsBinaryOperator.NotEqual;
				return true;
			case BinaryOperator2.Or:
				googleOp = GoogleAnalyticsBinaryOperator.Or;
				return true;
			}
			googleOp = GoogleAnalyticsBinaryOperator.Or;
			return false;
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x00105F52 File Offset: 0x00104152
		private bool TypeCompatible(GoogleAnalyticsDataType identifierType, GoogleAnalyticsDataType valueType)
		{
			switch (identifierType)
			{
			case GoogleAnalyticsDataType.Currency:
			case GoogleAnalyticsDataType.Float:
				break;
			case GoogleAnalyticsDataType.Date:
				return valueType == identifierType || valueType == GoogleAnalyticsDataType.String;
			default:
				if (identifierType != GoogleAnalyticsDataType.Percent)
				{
					return valueType == identifierType;
				}
				break;
			}
			return valueType == identifierType || valueType == GoogleAnalyticsDataType.Float || valueType == GoogleAnalyticsDataType.Integer;
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00105F88 File Offset: 0x00104188
		private bool TryCompileBinaryFilter(BinaryCubeExpression expression, out GoogleAnalyticsFilterExpression ga)
		{
			GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression;
			GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression2;
			if (!this.TryCompileFilterExpression(expression.Left, out googleAnalyticsFilterExpression) || !this.TryCompileFilterExpression(expression.Right, out googleAnalyticsFilterExpression2))
			{
				ga = null;
				return false;
			}
			if (expression.Operator == BinaryOperator2.Or)
			{
				if (googleAnalyticsFilterExpression.Kind != GoogleAnalyticsExpressionKind.Binary || googleAnalyticsFilterExpression2.Kind != GoogleAnalyticsExpressionKind.Binary)
				{
					ga = null;
					return false;
				}
			}
			else if (googleAnalyticsFilterExpression.Kind == GoogleAnalyticsExpressionKind.Identifier && googleAnalyticsFilterExpression2.Kind == GoogleAnalyticsExpressionKind.Constant)
			{
				GoogleAnalyticsValueExpression googleAnalyticsValueExpression = (GoogleAnalyticsValueExpression)googleAnalyticsFilterExpression;
				GoogleAnalyticsValueExpression googleAnalyticsValueExpression2 = (GoogleAnalyticsValueExpression)googleAnalyticsFilterExpression2;
				if (!this.TypeCompatible(googleAnalyticsValueExpression.DataType, googleAnalyticsValueExpression2.DataType))
				{
					ga = null;
					return false;
				}
			}
			else
			{
				if (googleAnalyticsFilterExpression.Kind != GoogleAnalyticsExpressionKind.Constant || googleAnalyticsFilterExpression2.Kind != GoogleAnalyticsExpressionKind.Identifier)
				{
					ga = null;
					return false;
				}
				GoogleAnalyticsValueExpression googleAnalyticsValueExpression = (GoogleAnalyticsValueExpression)googleAnalyticsFilterExpression;
				GoogleAnalyticsValueExpression googleAnalyticsValueExpression2 = (GoogleAnalyticsValueExpression)googleAnalyticsFilterExpression2;
				if (!this.TypeCompatible(googleAnalyticsValueExpression2.DataType, googleAnalyticsValueExpression.DataType))
				{
					ga = null;
					return false;
				}
			}
			GoogleAnalyticsBinaryOperator googleAnalyticsBinaryOperator;
			if (!this.TryConvertBinaryOperator(expression.Operator, out googleAnalyticsBinaryOperator))
			{
				ga = null;
				return false;
			}
			GoogleAnalyticsCubeObjectKind googleAnalyticsCubeObjectKind;
			if (googleAnalyticsFilterExpression.ColumnKind == GoogleAnalyticsCubeObjectKind.Constant)
			{
				googleAnalyticsCubeObjectKind = googleAnalyticsFilterExpression2.ColumnKind;
			}
			else if (googleAnalyticsFilterExpression2.ColumnKind == GoogleAnalyticsCubeObjectKind.Constant)
			{
				googleAnalyticsCubeObjectKind = googleAnalyticsFilterExpression.ColumnKind;
			}
			else
			{
				if (googleAnalyticsFilterExpression.ColumnKind != googleAnalyticsFilterExpression2.ColumnKind)
				{
					ga = null;
					return false;
				}
				googleAnalyticsCubeObjectKind = googleAnalyticsFilterExpression.ColumnKind;
			}
			ga = new GoogleAnalyticsBinaryExpression(googleAnalyticsBinaryOperator, googleAnalyticsFilterExpression, googleAnalyticsFilterExpression2, googleAnalyticsCubeObjectKind);
			return true;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x001060BC File Offset: 0x001042BC
		private bool TryCompileIdentifierFilter(IdentifierCubeExpression expression, out GoogleAnalyticsFilterExpression ga)
		{
			GoogleAnalyticsCubeObject @object = this.cube.GetObject(expression.Identifier);
			ga = new GoogleAnalyticsIdentifierExpression(expression.Identifier, @object.Type, @object.Kind);
			return true;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x001060F8 File Offset: 0x001042F8
		private bool TryCompileConstantFilter(ConstantCubeExpression expression, out GoogleAnalyticsFilterExpression ga)
		{
			ValueKind kind = expression.Value.Kind;
			if (kind != ValueKind.Date)
			{
				if (kind != ValueKind.Number)
				{
					if (kind == ValueKind.Text)
					{
						ga = new GoogleAnalyticsStringConstantExpression(expression.Value.AsString);
						return true;
					}
				}
				else
				{
					NumberValue asNumber = expression.Value.AsNumber;
					if (asNumber.NumberKind == NumberKind.Double)
					{
						ga = new GoogleAnalyticsDoubleConstantExpression(asNumber.AsDouble);
						return true;
					}
					int num;
					if (asNumber.TryGetInt32(out num))
					{
						ga = new GoogleAnalyticsIntegerConstantExpression(num);
						return true;
					}
				}
				ga = null;
				return false;
			}
			ga = new GoogleAnalyticsDateConstantExpression(expression.Value.AsDate.AsClrDateTime);
			return true;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x00106188 File Offset: 0x00104388
		private bool TryCompileInvocationFilter(InvocationCubeExpression expression, out GoogleAnalyticsFilterExpression ga)
		{
			if (expression.Function.Kind == CubeExpressionKind.Constant && expression.Arguments.Count == 2 && expression.Arguments[0].Kind == CubeExpressionKind.Identifier && expression.Arguments[1].Kind == CubeExpressionKind.Constant)
			{
				ConstantCubeExpression constantCubeExpression = (ConstantCubeExpression)expression.Function;
				IdentifierCubeExpression identifierCubeExpression = (IdentifierCubeExpression)expression.Arguments[0];
				ConstantCubeExpression constantCubeExpression2 = (ConstantCubeExpression)expression.Arguments[1];
				GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression;
				if (constantCubeExpression.Value.IsFunction && constantCubeExpression.Value.AsFunction.Equals(Library.Text.StartsWith) && constantCubeExpression2.Value.IsText && this.TryCompileIdentifierFilter(identifierCubeExpression, out googleAnalyticsFilterExpression))
				{
					ga = new GoogleAnalyticsBinaryExpression(GoogleAnalyticsBinaryOperator.RegexMatch, googleAnalyticsFilterExpression, new GoogleAnalyticsStringConstantExpression(constantCubeExpression2.Value.AsString + ".*"), googleAnalyticsFilterExpression.ColumnKind);
					return true;
				}
			}
			ga = null;
			return false;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x00106280 File Offset: 0x00104480
		private bool TryCompileFilterExpression(CubeExpression expression, out GoogleAnalyticsFilterExpression ga)
		{
			switch (expression.Kind)
			{
			case CubeExpressionKind.Constant:
				return this.TryCompileConstantFilter((ConstantCubeExpression)expression, out ga);
			case CubeExpressionKind.Identifier:
				return this.TryCompileIdentifierFilter((IdentifierCubeExpression)expression, out ga);
			case CubeExpressionKind.Binary:
				return this.TryCompileBinaryFilter((BinaryCubeExpression)expression, out ga);
			case CubeExpressionKind.Invocation:
				return this.TryCompileInvocationFilter((InvocationCubeExpression)expression, out ga);
			}
			ga = null;
			return false;
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x001062EC File Offset: 0x001044EC
		private bool TryCompileQueryExpression(QueryCubeExpression query, out GoogleAnalyticsQueryExpression ga)
		{
			if (query.From.Kind != CubeExpressionKind.Identifier)
			{
				ga = null;
				return false;
			}
			if (((IdentifierCubeExpression)query.From).Identifier != this.cube.Name)
			{
				ga = null;
				return false;
			}
			List<GoogleAnalyticsExpression> list = new List<GoogleAnalyticsExpression>();
			foreach (IdentifierCubeExpression identifierCubeExpression in query.Measures)
			{
				GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression;
				if (!this.TryCompileIdentifierFilter(identifierCubeExpression, out googleAnalyticsFilterExpression))
				{
					ga = null;
					return false;
				}
				list.Add(googleAnalyticsFilterExpression);
			}
			List<GoogleAnalyticsExpression> list2 = new List<GoogleAnalyticsExpression>();
			foreach (IdentifierCubeExpression identifierCubeExpression2 in query.DimensionAttributes)
			{
				GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression2;
				if (!this.TryCompileIdentifierFilter(identifierCubeExpression2, out googleAnalyticsFilterExpression2))
				{
					ga = null;
					return false;
				}
				list2.Add(googleAnalyticsFilterExpression2);
			}
			List<GoogleAnalyticsExpression> list3 = new List<GoogleAnalyticsExpression>();
			if (query.Sort != null)
			{
				foreach (CubeSortOrder cubeSortOrder in query.Sort)
				{
					GoogleAnalyticsSortOrder googleAnalyticsSortOrder;
					if (!this.TryCompileSort(cubeSortOrder, out googleAnalyticsSortOrder))
					{
						ga = null;
						return false;
					}
					list3.Add(googleAnalyticsSortOrder);
				}
			}
			GoogleAnalyticsFilterExpression[] array = new GoogleAnalyticsFilterExpression[0];
			bool flag = false;
			string text = string.Empty;
			DateTime fixedNow = this.cube.FixedNow;
			bool flag2 = false;
			DateTime dateTime;
			DateTime dateTime2;
			if (query.Filter == null)
			{
				dateTime = this.cube.Created;
				dateTime2 = fixedNow;
			}
			else
			{
				if (GoogleAnalyticsQueryCompilerV1.ContainsDateTextComparison(query.Filter))
				{
					ga = null;
					return false;
				}
				if (list2.Count == 1 && ((GoogleAnalyticsIdentifierExpression)list2[0]).Identifier == "ga:date" && query.Filter.GetReferences().Any((IdentifierCubeExpression expression) => expression.Identifier != "ga:date" && this.cube.GetObject(expression.Identifier).Kind == GoogleAnalyticsCubeObjectKind.Dimension))
				{
					ga = null;
					return false;
				}
				dateTime = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(query.Filter, true);
				dateTime2 = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(query.Filter, false);
				dateTime = ((this.cube.Created > dateTime) ? this.cube.Created : dateTime);
				dateTime2 = ((fixedNow < dateTime2) ? fixedNow : dateTime2);
				flag2 = dateTime > this.cube.Created || dateTime2 < fixedNow;
				CubeExpression cubeExpression = GoogleAnalyticsQueryCompilerV1.RemoveSuperfluousDateFilters(query.Filter, dateTime, dateTime2);
				if (cubeExpression != null)
				{
					cubeExpression = GoogleAnalyticsQueryCompilerV1.ExpandDateFilters(cubeExpression, dateTime, dateTime2);
					if (GoogleAnalyticsQueryCompilerV1.TryGetNonFalseExpression(GoogleAnalyticsQueryCompilerV1.CompressConjunctions(cubeExpression), out cubeExpression))
					{
						IList<CubeExpression> conjunctiveNF = cubeExpression.GetConjunctiveNF();
						array = new GoogleAnalyticsFilterExpression[conjunctiveNF.Count];
						for (int i = 0; i < conjunctiveNF.Count; i++)
						{
							if (!this.TryCompileFilterExpression(conjunctiveNF[i], out array[i]))
							{
								ga = null;
								return false;
							}
						}
						text = GoogleAnalyticsQueryCompilerV1.CreateGoogleAnalyticsFilterExpressionQuery(array);
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					text = string.Empty;
				}
			}
			if (query.RowRange.SkipCount.IsInfinite || query.RowRange.SkipCount.Value >= 2147483647L)
			{
				ga = null;
				return false;
			}
			string text2 = GoogleAnalyticsQueryCompilerV1.CreateGoogleAnalyticsExpressionQueryList(list);
			string text3 = GoogleAnalyticsQueryCompilerV1.CreateGoogleAnalyticsExpressionQueryList(list2);
			flag2 = flag2 && list2.Count == 1 && ((GoogleAnalyticsIdentifierExpression)list2[0]).Identifier == "ga:date";
			string text4 = GoogleAnalyticsQueryCompilerV1.CreateGoogleAnalyticsExpressionQueryList(list3);
			if (text2.Length > GoogleAnalyticsQueryCompilerV1.ExpressionCharacterLimit || text3.Length > GoogleAnalyticsQueryCompilerV1.ExpressionCharacterLimit || text.Length > GoogleAnalyticsQueryCompilerV1.ExpressionCharacterLimit || text4.Length > GoogleAnalyticsQueryCompilerV1.ExpressionCharacterLimit)
			{
				ga = null;
				return false;
			}
			ga = new GoogleAnalyticsQueryExpressionV1(this.cube, list, list2, text, flag2, query.Filter, list3, query.RowRange, dateTime, dateTime2, flag);
			return true;
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x001066E8 File Offset: 0x001048E8
		private static bool ContainsDateTextComparison(CubeExpression expression)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				IdentifierCubeExpression identifierCubeExpression;
				ConstantCubeExpression constantCubeExpression;
				if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Identifier && binaryCubeExpression.Right.Kind == CubeExpressionKind.Constant)
				{
					identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Left;
					constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Right;
				}
				else
				{
					if (binaryCubeExpression.Left.Kind != CubeExpressionKind.Constant || binaryCubeExpression.Right.Kind != CubeExpressionKind.Identifier)
					{
						return GoogleAnalyticsQueryCompilerV1.ContainsDateTextComparison(binaryCubeExpression.Left) || GoogleAnalyticsQueryCompilerV1.ContainsDateTextComparison(binaryCubeExpression.Right);
					}
					identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Right;
					constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Left;
				}
				return identifierCubeExpression.Identifier == "ga:date" && constantCubeExpression.Value.IsText;
			}
			return false;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x001067B0 File Offset: 0x001049B0
		private static CubeExpression CompressConjunctions(CubeExpression filter)
		{
			if (filter.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)filter;
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					string text;
					string text2;
					if (GoogleAnalyticsQueryCompilerV1.TryGetSingleStringComparisonIdentifier(binaryCubeExpression.Left, out text) && GoogleAnalyticsQueryCompilerV1.TryGetSingleStringComparisonIdentifier(binaryCubeExpression.Right, out text2) && text == text2)
					{
						StringEqualityComparison[] array = GoogleAnalyticsQueryCompilerV1.CollectStringComparisonValues(binaryCubeExpression.Left);
						StringEqualityComparison[] array2 = GoogleAnalyticsQueryCompilerV1.CollectStringComparisonValues(binaryCubeExpression.Right);
						IList<StringEqualityComparison> list = GoogleAnalyticsQueryCompilerV1.StringComparisonValuesIntersect(array, array2);
						return GoogleAnalyticsQueryCompilerV1.CreateStringFilterExpression(text, list, 0);
					}
					return new BinaryCubeExpression(BinaryOperator2.And, GoogleAnalyticsQueryCompilerV1.CompressConjunctions(binaryCubeExpression.Left), GoogleAnalyticsQueryCompilerV1.CompressConjunctions(binaryCubeExpression.Right));
				}
				else if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					return new BinaryCubeExpression(BinaryOperator2.Or, GoogleAnalyticsQueryCompilerV1.CompressConjunctions(binaryCubeExpression.Left), GoogleAnalyticsQueryCompilerV1.CompressConjunctions(binaryCubeExpression.Right));
				}
			}
			return filter;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00106874 File Offset: 0x00104A74
		private static CubeExpression CreateStringFilterExpression(string identifier, IList<StringEqualityComparison> comparisons, int position)
		{
			int num = comparisons.Count - position;
			if (num == 0)
			{
				return new ConstantCubeExpression(LogicalValue.New(false));
			}
			CubeExpression cubeExpression;
			if (comparisons[position].Prefix)
			{
				cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.Text.StartsWith), new CubeExpression[]
				{
					new IdentifierCubeExpression(identifier),
					new ConstantCubeExpression(TextValue.New(comparisons[position].Value))
				});
			}
			else
			{
				cubeExpression = new BinaryCubeExpression(BinaryOperator2.Equals, new IdentifierCubeExpression(identifier), new ConstantCubeExpression(TextValue.New(comparisons[position].Value)));
			}
			if (num == 1)
			{
				return cubeExpression;
			}
			return new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression, GoogleAnalyticsQueryCompilerV1.CreateStringFilterExpression(identifier, comparisons, position + 1));
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0010691C File Offset: 0x00104B1C
		public static IList<StringEqualityComparison> StringComparisonValuesIntersect(StringEqualityComparison[] leftValues, StringEqualityComparison[] rightValues)
		{
			List<StringEqualityComparison> list = new List<StringEqualityComparison>();
			Array.Sort<StringEqualityComparison>(leftValues);
			Array.Sort<StringEqualityComparison>(rightValues);
			int num = 0;
			int num2 = 0;
			while (num2 < rightValues.Length && num < leftValues.Length)
			{
				if (leftValues[num].Prefix)
				{
					if (rightValues[num2].Value.StartsWith(leftValues[num].Value, StringComparison.Ordinal))
					{
						list.Add(rightValues[num2]);
						num2++;
						continue;
					}
				}
				else if (rightValues[num2].Prefix)
				{
					if (leftValues[num].Value.StartsWith(rightValues[num2].Value, StringComparison.Ordinal))
					{
						list.Add(leftValues[num]);
						num++;
						continue;
					}
				}
				else if (rightValues[num2].Value == leftValues[num].Value)
				{
					list.Add(leftValues[num]);
					num++;
					num2++;
					continue;
				}
				if (leftValues[num].CompareTo(rightValues[num2]) < 0 && num < leftValues.Length)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			return list;
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x00106A00 File Offset: 0x00104C00
		private static StringEqualityComparison[] CollectStringComparisonValues(CubeExpression expression)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					IList<StringEqualityComparison> list = GoogleAnalyticsQueryCompilerV1.CollectStringComparisonValues(binaryCubeExpression.Left);
					IList<StringEqualityComparison> list2 = GoogleAnalyticsQueryCompilerV1.CollectStringComparisonValues(binaryCubeExpression.Right);
					StringEqualityComparison[] array = new StringEqualityComparison[list.Count + list2.Count];
					list.CopyTo(array, 0);
					list2.CopyTo(array, list.Count);
					return array;
				}
				if (binaryCubeExpression.Operator == BinaryOperator2.Equals)
				{
					ConstantCubeExpression constantCubeExpression;
					if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Constant)
					{
						constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Left;
					}
					else
					{
						constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Right;
					}
					return new StringEqualityComparison[]
					{
						new StringEqualityComparison(constantCubeExpression.Value.AsString, false)
					};
				}
			}
			else if (expression.Kind == CubeExpressionKind.Invocation)
			{
				ConstantCubeExpression constantCubeExpression2 = (ConstantCubeExpression)((InvocationCubeExpression)expression).Arguments[1];
				return new StringEqualityComparison[]
				{
					new StringEqualityComparison(constantCubeExpression2.Value.AsString, true)
				};
			}
			return EmptyArray<StringEqualityComparison>.Instance;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00106B00 File Offset: 0x00104D00
		private static bool TryGetSingleStringComparisonIdentifier(CubeExpression expression, out string identifier)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					string text;
					string text2;
					if (GoogleAnalyticsQueryCompilerV1.TryGetSingleStringComparisonIdentifier(binaryCubeExpression.Left, out text) && GoogleAnalyticsQueryCompilerV1.TryGetSingleStringComparisonIdentifier(binaryCubeExpression.Right, out text2))
					{
						identifier = text;
						return text == text2;
					}
				}
				else if (binaryCubeExpression.Operator == BinaryOperator2.Equals)
				{
					if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Identifier && binaryCubeExpression.Right.Kind == CubeExpressionKind.Constant)
					{
						identifier = ((IdentifierCubeExpression)binaryCubeExpression.Left).Identifier;
						return ((ConstantCubeExpression)binaryCubeExpression.Right).Value.IsText;
					}
					if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Constant && binaryCubeExpression.Right.Kind == CubeExpressionKind.Identifier)
					{
						identifier = ((IdentifierCubeExpression)binaryCubeExpression.Right).Identifier;
						return ((ConstantCubeExpression)binaryCubeExpression.Left).Value.IsText;
					}
					identifier = null;
					return false;
				}
			}
			else if (expression.Kind == CubeExpressionKind.Invocation)
			{
				InvocationCubeExpression invocationCubeExpression = (InvocationCubeExpression)expression;
				if (invocationCubeExpression.Function.Kind == CubeExpressionKind.Constant && invocationCubeExpression.Arguments.Count == 2 && invocationCubeExpression.Arguments[0].Kind == CubeExpressionKind.Identifier && invocationCubeExpression.Arguments[1].Kind == CubeExpressionKind.Constant)
				{
					ConstantCubeExpression constantCubeExpression = (ConstantCubeExpression)invocationCubeExpression.Function;
					IdentifierCubeExpression identifierCubeExpression = (IdentifierCubeExpression)invocationCubeExpression.Arguments[0];
					if (constantCubeExpression.Value.IsFunction && constantCubeExpression.Value.AsFunction == Library.Text.StartsWith)
					{
						identifier = identifierCubeExpression.Identifier;
						return true;
					}
				}
			}
			identifier = null;
			return false;
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00106C98 File Offset: 0x00104E98
		private static bool IsGaDateComparison(BinaryCubeExpression binaryExpression, out DateTime constant, out BinaryOperator2 op)
		{
			if (binaryExpression.Left.Kind == CubeExpressionKind.Identifier && ((IdentifierCubeExpression)binaryExpression.Left).Identifier == "ga:date" && binaryExpression.Right.Kind == CubeExpressionKind.Constant)
			{
				constant = ((ConstantCubeExpression)binaryExpression.Right).Value.AsDate.AsClrDateTime;
				op = binaryExpression.Operator;
				return true;
			}
			if ((binaryExpression.Left.Kind == CubeExpressionKind.Constant && ((IdentifierCubeExpression)binaryExpression.Right).Identifier == "ga:date") || binaryExpression.Right.Kind == CubeExpressionKind.Identifier)
			{
				constant = ((ConstantCubeExpression)binaryExpression.Left).Value.AsDate.AsClrDateTime;
				op = GoogleAnalyticsQueryCompilerV1.InvertOperator(binaryExpression.Operator);
				return true;
			}
			constant = default(DateTime);
			op = BinaryOperator2.Add;
			return false;
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00106D78 File Offset: 0x00104F78
		private static CubeExpression RemoveSuperfluousDateFilters(CubeExpression cubeExpression, DateTime startDate, DateTime endDate)
		{
			if (cubeExpression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)cubeExpression;
				DateTime dateTime;
				BinaryOperator2 binaryOperator;
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					CubeExpression cubeExpression2 = GoogleAnalyticsQueryCompilerV1.RemoveSuperfluousDateFilters(binaryCubeExpression.Left, startDate, endDate);
					CubeExpression cubeExpression3 = GoogleAnalyticsQueryCompilerV1.RemoveSuperfluousDateFilters(binaryCubeExpression.Right, startDate, endDate);
					if (cubeExpression2 != null && cubeExpression3 != null)
					{
						return new BinaryCubeExpression(BinaryOperator2.And, cubeExpression2, cubeExpression3);
					}
					if (cubeExpression2 != null)
					{
						return cubeExpression2;
					}
					return cubeExpression3;
				}
				else if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					CubeExpression cubeExpression4 = GoogleAnalyticsQueryCompilerV1.RemoveSuperfluousDateFilters(binaryCubeExpression.Left, startDate, endDate);
					CubeExpression cubeExpression5 = GoogleAnalyticsQueryCompilerV1.RemoveSuperfluousDateFilters(binaryCubeExpression.Right, startDate, endDate);
					if (cubeExpression4 == null || cubeExpression5 == null)
					{
						return null;
					}
					return new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression4, cubeExpression5);
				}
				else if (GoogleAnalyticsQueryCompilerV1.IsGaDateComparison(binaryCubeExpression, out dateTime, out binaryOperator) && ((binaryOperator == BinaryOperator2.GreaterThanOrEquals && dateTime <= startDate) || (binaryOperator == BinaryOperator2.LessThanOrEquals && dateTime >= endDate) || (binaryOperator == BinaryOperator2.Equals && dateTime == startDate && dateTime == endDate)))
				{
					return null;
				}
			}
			return cubeExpression;
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00106E58 File Offset: 0x00105058
		private static CubeExpression ExpandDateFilters(CubeExpression cubeExpression, DateTime minimumDate, DateTime maximumDate)
		{
			if (cubeExpression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)cubeExpression;
				if (binaryCubeExpression.Operator == BinaryOperator2.Equals)
				{
					IdentifierCubeExpression identifierCubeExpression = null;
					ConstantCubeExpression constantCubeExpression = null;
					if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Identifier && binaryCubeExpression.Right.Kind == CubeExpressionKind.Constant)
					{
						identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Left;
						constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Right;
					}
					else if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Constant && binaryCubeExpression.Right.Kind == CubeExpressionKind.Identifier)
					{
						constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Left;
						identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Right;
					}
					if (identifierCubeExpression != null && identifierCubeExpression.Identifier == "ga:date")
					{
						return new BinaryCubeExpression(BinaryOperator2.Equals, new IdentifierCubeExpression(identifierCubeExpression.Identifier), new ConstantCubeExpression(TextValue.New(constantCubeExpression.Value.AsDate.AsClrDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture))));
					}
				}
				else
				{
					DateTime dateTime;
					if (GoogleAnalyticsQueryCompilerV1.TryGetDateComparisonBound(binaryCubeExpression, true, out dateTime))
					{
						return GoogleAnalyticsQueryCompilerV1.CreateDateRangeExpression(GoogleAnalyticsQueryCompilerV1.ExtractIdentifier(binaryCubeExpression), dateTime, maximumDate);
					}
					if (GoogleAnalyticsQueryCompilerV1.TryGetDateComparisonBound(binaryCubeExpression, false, out dateTime))
					{
						return GoogleAnalyticsQueryCompilerV1.CreateDateRangeExpression(GoogleAnalyticsQueryCompilerV1.ExtractIdentifier(binaryCubeExpression), minimumDate, dateTime);
					}
					if (binaryCubeExpression.Operator == BinaryOperator2.And)
					{
						return new BinaryCubeExpression(BinaryOperator2.And, GoogleAnalyticsQueryCompilerV1.ExpandDateFilters(binaryCubeExpression.Left, minimumDate, maximumDate), GoogleAnalyticsQueryCompilerV1.ExpandDateFilters(binaryCubeExpression.Right, minimumDate, maximumDate));
					}
					if (binaryCubeExpression.Operator == BinaryOperator2.Or)
					{
						return new BinaryCubeExpression(BinaryOperator2.Or, GoogleAnalyticsQueryCompilerV1.ExpandDateFilters(binaryCubeExpression.Left, minimumDate, maximumDate), GoogleAnalyticsQueryCompilerV1.ExpandDateFilters(binaryCubeExpression.Right, minimumDate, maximumDate));
					}
				}
			}
			return cubeExpression;
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00106FD8 File Offset: 0x001051D8
		private static CubeExpression CreateDateRangeExpression(string identifier, DateTime startDate, DateTime endDate)
		{
			if (startDate > endDate)
			{
				return new ConstantCubeExpression(LogicalValue.New(false));
			}
			CubeExpression cubeExpression;
			DateTime dateTime;
			if (startDate.Day == 1 && startDate.Month == 1 && startDate.AddYears(1) <= endDate)
			{
				cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.Text.StartsWith), new CubeExpression[]
				{
					new IdentifierCubeExpression(identifier),
					new ConstantCubeExpression(TextValue.New(startDate.ToString("yyyy", CultureInfo.InvariantCulture)))
				});
				dateTime = startDate.AddYears(1);
			}
			else if (startDate.Day == 1 && startDate.AddMonths(1) <= endDate)
			{
				cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.Text.StartsWith), new CubeExpression[]
				{
					new IdentifierCubeExpression(identifier),
					new ConstantCubeExpression(TextValue.New(startDate.ToString("yyyyMM", CultureInfo.InvariantCulture)))
				});
				dateTime = startDate.AddMonths(1);
			}
			else
			{
				cubeExpression = new BinaryCubeExpression(BinaryOperator2.Equals, new IdentifierCubeExpression(identifier), new ConstantCubeExpression(TextValue.New(startDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture))));
				dateTime = startDate.AddDays(1.0);
			}
			if (dateTime <= endDate)
			{
				return new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression, GoogleAnalyticsQueryCompilerV1.CreateDateRangeExpression(identifier, dateTime, endDate));
			}
			return cubeExpression;
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0010711F File Offset: 0x0010531F
		private static string ExtractIdentifier(BinaryCubeExpression binaryExpression)
		{
			return ((binaryExpression.Left.Kind == CubeExpressionKind.Identifier) ? ((IdentifierCubeExpression)binaryExpression.Left) : ((IdentifierCubeExpression)binaryExpression.Right)).Identifier;
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x0010714C File Offset: 0x0010534C
		private static string CreateGoogleAnalyticsExpressionQueryList(IList<GoogleAnalyticsExpression> expressions)
		{
			if (expressions.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(expressions[0].QueryString);
			for (int i = 1; i < expressions.Count; i++)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(expressions[i].QueryString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x001071B0 File Offset: 0x001053B0
		private static string CreateGoogleAnalyticsFilterExpressionQuery(IList<GoogleAnalyticsExpression> cnfExpression)
		{
			if (cnfExpression.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(cnfExpression[0].QueryString);
			for (int i = 1; i < cnfExpression.Count; i++)
			{
				stringBuilder.Append(";");
				stringBuilder.Append(cnfExpression[i].QueryString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00107213 File Offset: 0x00105413
		private static BinaryOperator2 InvertOperator(BinaryOperator2 op)
		{
			switch (op)
			{
			case BinaryOperator2.GreaterThan:
				return BinaryOperator2.LessThan;
			case BinaryOperator2.LessThan:
				return BinaryOperator2.GreaterThan;
			case BinaryOperator2.GreaterThanOrEquals:
				return BinaryOperator2.LessThanOrEquals;
			case BinaryOperator2.LessThanOrEquals:
				return BinaryOperator2.GreaterThanOrEquals;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0010723C File Offset: 0x0010543C
		private static DateTime ComputeDateBound(CubeExpression expression, bool lowerBound)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				DateTime dateTime;
				if (GoogleAnalyticsQueryCompilerV1.TryGetDateComparisonBound(binaryCubeExpression, lowerBound, out dateTime))
				{
					return dateTime;
				}
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					DateTime dateTime2 = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(binaryCubeExpression.Left, lowerBound);
					DateTime dateTime3 = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(binaryCubeExpression.Right, lowerBound);
					if (lowerBound == dateTime2 > dateTime3)
					{
						return dateTime2;
					}
					return dateTime3;
				}
				else if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					DateTime dateTime4 = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(binaryCubeExpression.Left, lowerBound);
					DateTime dateTime5 = GoogleAnalyticsQueryCompilerV1.ComputeDateBound(binaryCubeExpression.Right, lowerBound);
					if (lowerBound == dateTime4 < dateTime5)
					{
						return dateTime4;
					}
					return dateTime5;
				}
			}
			if (!lowerBound)
			{
				return DateTime.MaxValue;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x001072DE File Offset: 0x001054DE
		private bool IsDimensionAttribute(IdentifierCubeExpression identifierExpression)
		{
			return this.cube.GetObject(identifierExpression.Identifier).Kind == GoogleAnalyticsCubeObjectKind.Dimension;
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x001072FC File Offset: 0x001054FC
		private static bool TryGetDateComparisonBound(BinaryCubeExpression binaryExpression, bool lowerBound, out DateTime dateBound)
		{
			if ((binaryExpression.Left.Kind == CubeExpressionKind.Identifier && binaryExpression.Right.Kind == CubeExpressionKind.Constant) || (binaryExpression.Left.Kind == CubeExpressionKind.Constant && binaryExpression.Right.Kind == CubeExpressionKind.Identifier))
			{
				IdentifierCubeExpression identifierCubeExpression;
				ConstantCubeExpression constantCubeExpression;
				BinaryOperator2 binaryOperator;
				if (binaryExpression.Left.Kind == CubeExpressionKind.Identifier && binaryExpression.Right.Kind == CubeExpressionKind.Constant)
				{
					identifierCubeExpression = (IdentifierCubeExpression)binaryExpression.Left;
					constantCubeExpression = (ConstantCubeExpression)binaryExpression.Right;
					binaryOperator = binaryExpression.Operator;
				}
				else
				{
					identifierCubeExpression = (IdentifierCubeExpression)binaryExpression.Right;
					constantCubeExpression = (ConstantCubeExpression)binaryExpression.Left;
					binaryOperator = GoogleAnalyticsQueryCompilerV1.InvertOperator(binaryExpression.Operator);
				}
				if (constantCubeExpression.Value.IsDate && identifierCubeExpression.Identifier == "ga:date")
				{
					if (binaryOperator == BinaryOperator2.Equals)
					{
						dateBound = constantCubeExpression.Value.AsDate.AsClrDateTime;
						return true;
					}
					if (lowerBound)
					{
						if (binaryOperator == BinaryOperator2.GreaterThan)
						{
							dateBound = constantCubeExpression.Value.AsDate.AsClrDateTime.AddDays(1.0);
							return true;
						}
						if (binaryOperator == BinaryOperator2.GreaterThanOrEquals)
						{
							dateBound = constantCubeExpression.Value.AsDate.AsClrDateTime;
							return true;
						}
					}
					else
					{
						if (binaryOperator == BinaryOperator2.LessThan)
						{
							dateBound = constantCubeExpression.Value.AsDate.AsClrDateTime.AddDays(-1.0);
							return true;
						}
						if (binaryOperator == BinaryOperator2.LessThanOrEquals)
						{
							dateBound = constantCubeExpression.Value.AsDate.AsClrDateTime;
							return true;
						}
					}
				}
				if (identifierCubeExpression.Identifier == "ga:yearMonth")
				{
					int num = int.Parse(constantCubeExpression.Value.AsString.Substring(0, 4), CultureInfo.InvariantCulture);
					int num2 = int.Parse(constantCubeExpression.Value.AsString.Substring(4, 2), CultureInfo.InvariantCulture);
					DateTime dateTime = new DateTime(num, num2, 1);
					DateTime dateTime2 = dateTime.AddMonths(1).AddDays(-1.0);
					if (binaryOperator == BinaryOperator2.Equals)
					{
						dateBound = (lowerBound ? dateTime : dateTime2);
						return true;
					}
				}
			}
			dateBound = (lowerBound ? DateTime.MinValue : DateTime.MaxValue);
			return false;
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0010751C File Offset: 0x0010571C
		private static bool TryGetNonFalseExpression(CubeExpression expression, out CubeExpression nonFalse)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					CubeExpression cubeExpression;
					CubeExpression cubeExpression2;
					if (GoogleAnalyticsQueryCompilerV1.TryGetNonFalseExpression(binaryCubeExpression.Left, out cubeExpression) && GoogleAnalyticsQueryCompilerV1.TryGetNonFalseExpression(binaryCubeExpression.Right, out cubeExpression2))
					{
						nonFalse = new BinaryCubeExpression(BinaryOperator2.And, cubeExpression, cubeExpression2);
						return true;
					}
					nonFalse = null;
					return false;
				}
				else if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					CubeExpression cubeExpression3;
					GoogleAnalyticsQueryCompilerV1.TryGetNonFalseExpression(binaryCubeExpression.Left, out cubeExpression3);
					CubeExpression cubeExpression4;
					GoogleAnalyticsQueryCompilerV1.TryGetNonFalseExpression(binaryCubeExpression.Right, out cubeExpression4);
					if (cubeExpression3 == null && cubeExpression4 == null)
					{
						nonFalse = null;
						return false;
					}
					if (cubeExpression3 != null && cubeExpression4 != null)
					{
						nonFalse = new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression3, cubeExpression4);
						return true;
					}
					nonFalse = ((cubeExpression3 != null) ? cubeExpression3 : cubeExpression4);
					return true;
				}
			}
			nonFalse = expression;
			return true;
		}

		// Token: 0x04002A87 RID: 10887
		public const string GoogleAnalyticsDateDimensionId = "ga:date";

		// Token: 0x04002A88 RID: 10888
		public const string GoogleAnalyticsYearMonthDimensionId = "ga:yearMonth";

		// Token: 0x04002A89 RID: 10889
		private static readonly int ExpressionCharacterLimit = 4096;

		// Token: 0x04002A8A RID: 10890
		private readonly IGoogleAnalyticsCube cube;
	}
}
