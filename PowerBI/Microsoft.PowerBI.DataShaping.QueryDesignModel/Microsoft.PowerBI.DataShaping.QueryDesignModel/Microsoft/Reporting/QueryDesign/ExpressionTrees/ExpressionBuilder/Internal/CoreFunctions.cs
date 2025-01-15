using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000153 RID: 339
	internal static class CoreFunctions
	{
		// Token: 0x06001297 RID: 4759 RVA: 0x00035CB7 File Offset: 0x00033EB7
		public static QueryFunctionExpression Not(this QueryExpression arg)
		{
			return CoreFunctions.InvokeFunction("Core.Not", new QueryExpression[] { arg });
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00035CCD File Offset: 0x00033ECD
		public static QueryOperatorExpression And(this QueryExpression left, QueryExpression right)
		{
			return left.And(right, true);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00035CD7 File Offset: 0x00033ED7
		public static QueryOperatorExpression And(this QueryExpression left, QueryExpression right, bool useBinaryEquivalent)
		{
			return new QueryExpression[] { left, right }.And(true);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00035CED File Offset: 0x00033EED
		public static QueryOperatorExpression And(this IReadOnlyList<QueryExpression> arguments, bool useBinaryEquivalent)
		{
			return CoreFunctions.InvokeOperator("Core.And", arguments, useBinaryEquivalent);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00035CFB File Offset: 0x00033EFB
		public static QueryFunctionExpression Or(this QueryExpression left, QueryExpression right)
		{
			return CoreFunctions.InvokeFunction("Core.Or", new QueryExpression[] { left, right });
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00035D15 File Offset: 0x00033F15
		public static QueryFunctionExpression Sum(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Sum", new QueryExpression[] { value });
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00035D37 File Offset: 0x00033F37
		public static QueryFunctionExpression Count(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Count", new QueryExpression[] { value });
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00035D59 File Offset: 0x00033F59
		public static QueryFunctionExpression DistinctCount(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.DistinctCount", new QueryExpression[] { value });
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00035D7B File Offset: 0x00033F7B
		public static QueryFunctionExpression Average(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Average", new QueryExpression[] { value });
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00035D9D File Offset: 0x00033F9D
		public static QueryFunctionExpression Min(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Min", new QueryExpression[] { value });
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00035DBF File Offset: 0x00033FBF
		public static QueryFunctionExpression Max(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Max", new QueryExpression[] { value });
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00035DE1 File Offset: 0x00033FE1
		public static QueryFunctionExpression Median(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Median", new QueryExpression[] { value });
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00035E03 File Offset: 0x00034003
		public static QueryFunctionExpression StandardDeviationP(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.StandardDeviation", new QueryExpression[] { value });
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00035E25 File Offset: 0x00034025
		public static QueryFunctionExpression Variance(this QueryExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction("Core.Variance", new QueryExpression[] { value });
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00035E47 File Offset: 0x00034047
		public static QueryFunctionExpression PercentileInc(this QueryExpression value, QueryExpression k)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			ArgumentValidation.CheckNotNull<QueryExpression>(k, "k");
			return CoreFunctions.InvokeFunction("Core.PercentileInc", new QueryExpression[] { value, k });
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00035E79 File Offset: 0x00034079
		public static QueryFunctionExpression PercentileExc(this QueryExpression value, QueryExpression k)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			ArgumentValidation.CheckNotNull<QueryExpression>(k, "k");
			return CoreFunctions.InvokeFunction("Core.PercentileExc", new QueryExpression[] { value, k });
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00035EAB File Offset: 0x000340AB
		public static QueryFunctionExpression InvokeAggregate(this QueryExpression value, AggregateFunction aggFunc)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value, "value");
			return CoreFunctions.InvokeFunction(CoreFunctionLibrary.FunctionNameFromAggregateFunction(aggFunc), new QueryExpression[] { value });
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00035ECE File Offset: 0x000340CE
		public static QueryFunctionExpression TextContains(this QueryExpression fullString, QueryExpression substring)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(fullString, "fullString");
			ArgumentValidation.CheckNotNull<QueryExpression>(substring, "substring");
			return CoreFunctions.InvokeFunction("Core.Contains", new QueryExpression[] { fullString, substring });
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00035F00 File Offset: 0x00034100
		public static QueryFunctionExpression StartsWith(this QueryExpression fullString, QueryExpression substring)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(fullString, "fullString");
			ArgumentValidation.CheckNotNull<QueryExpression>(substring, "substring");
			return CoreFunctions.InvokeFunction("Core.StartsWith", new QueryExpression[] { fullString, substring });
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00035F32 File Offset: 0x00034132
		public static QueryFunctionExpression EndsWith(this QueryExpression fullString, QueryExpression substring)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(fullString, "fullString");
			ArgumentValidation.CheckNotNull<QueryExpression>(substring, "substring");
			return CoreFunctions.InvokeFunction("Core.EndsWith", new QueryExpression[] { fullString, substring });
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00035F64 File Offset: 0x00034164
		public static QueryFunctionExpression DateTimeEqualToSecond(this QueryExpression actualDateTime, QueryExpression referenceDateTime)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(actualDateTime, "actualDateTime");
			ArgumentValidation.CheckNotNull<QueryExpression>(referenceDateTime, "referenceDateTime");
			return CoreFunctions.InvokeFunction("Core.DateTimeEqualToSecond", new QueryExpression[] { actualDateTime, referenceDateTime });
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00035F96 File Offset: 0x00034196
		public static QueryFunctionExpression Date(this QueryExpression year, QueryExpression month, QueryExpression day)
		{
			return CoreFunctions.InvokeFunction("Core.Date", new QueryExpression[] { year, month, day });
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00035FB4 File Offset: 0x000341B4
		public static QueryFunctionExpression Year(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Year", new QueryExpression[] { date });
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00035FCA File Offset: 0x000341CA
		public static QueryFunctionExpression Month(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Month", new QueryExpression[] { date });
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00035FE0 File Offset: 0x000341E0
		public static QueryFunctionExpression Day(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Day", new QueryExpression[] { date });
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00035FF6 File Offset: 0x000341F6
		public static QueryFunctionExpression Hour(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Hour", new QueryExpression[] { date });
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003600C File Offset: 0x0003420C
		public static QueryFunctionExpression Minute(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Minute", new QueryExpression[] { date });
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00036022 File Offset: 0x00034222
		public static QueryFunctionExpression Second(this QueryExpression date)
		{
			return CoreFunctions.InvokeFunction("Core.Second", new QueryExpression[] { date });
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00036038 File Offset: 0x00034238
		public static QueryFunctionExpression EDate(this QueryExpression startDate, QueryExpression months)
		{
			return CoreFunctions.InvokeFunction("Core.EDate", new QueryExpression[] { startDate, months });
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00036054 File Offset: 0x00034254
		public static QueryFunctionExpression If(this QueryExpression logicalTest, QueryExpression valueIfTrue, QueryExpression valueIfFalse)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(logicalTest, "logicalTest");
			ArgumentValidation.CheckNotNull<QueryExpression>(valueIfTrue, "valueIfTrue");
			ArgumentValidation.CheckNotNull<QueryExpression>(valueIfFalse, "valueIfFalse");
			return CoreFunctions.InvokeFunction("Core.If", new QueryExpression[] { logicalTest, valueIfTrue, valueIfFalse });
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000360A1 File Offset: 0x000342A1
		public static QueryFunctionExpression If(this QueryExpression logicalTest, QueryExpression valueIfTrue)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(logicalTest, "logicalTest");
			ArgumentValidation.CheckNotNull<QueryExpression>(valueIfTrue, "valueIfTrue");
			return CoreFunctions.InvokeFunction("Core.If", new QueryExpression[] { logicalTest, valueIfTrue });
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000360D4 File Offset: 0x000342D4
		public static QueryFunctionExpression Between(this QueryExpression candidate, QueryExpression value1, QueryExpression value2)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(candidate, "candidate");
			ArgumentValidation.CheckNotNull<QueryExpression>(value1, "value1");
			ArgumentValidation.CheckNotNull<QueryExpression>(value2, "value2");
			return CoreFunctions.InvokeFunction("Core.Between", new QueryExpression[] { candidate, value1, value2 });
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00036121 File Offset: 0x00034321
		public static QueryFunctionExpression Ceiling(this QueryExpression number, QueryExpression significance)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(number, "number");
			ArgumentValidation.CheckNotNull<QueryExpression>(significance, "significance");
			return CoreFunctions.InvokeFunction("Core.Ceiling", new QueryExpression[] { number, significance });
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00036153 File Offset: 0x00034353
		public static QueryFunctionExpression Floor(this QueryExpression number, QueryExpression significance)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(number, "number");
			ArgumentValidation.CheckNotNull<QueryExpression>(significance, "significance");
			return CoreFunctions.InvokeFunction("Core.Floor", new QueryExpression[] { number, significance });
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00036185 File Offset: 0x00034385
		public static QueryFunctionExpression Sqrt(this QueryExpression number)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(number, "number");
			return CoreFunctions.InvokeFunction("Core.Sqrt", new QueryExpression[] { number });
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000361A7 File Offset: 0x000343A7
		public static QueryFunctionExpression MinValue(this QueryExpression value1, QueryExpression value2)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value1, "value1");
			ArgumentValidation.CheckNotNull<QueryExpression>(value2, "value2");
			return CoreFunctions.InvokeFunction("Core.MinValue", new QueryExpression[] { value1, value2 });
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000361D9 File Offset: 0x000343D9
		public static QueryFunctionExpression MaxValue(this QueryExpression value1, QueryExpression value2)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(value1, "value1");
			ArgumentValidation.CheckNotNull<QueryExpression>(value2, "value2");
			return CoreFunctions.InvokeFunction("Core.MaxValue", new QueryExpression[] { value1, value2 });
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003620B File Offset: 0x0003440B
		public static QueryFunctionExpression Int(this QueryExpression number)
		{
			return CoreFunctions.InvokeFunction("Core.Int", new QueryExpression[] { number });
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00036221 File Offset: 0x00034421
		public static QueryFunctionExpression RoundDown(this QueryExpression number, QueryExpression numberOfDigits)
		{
			return CoreFunctions.InvokeFunction("Core.RoundDown", new QueryExpression[] { number, numberOfDigits });
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0003623B File Offset: 0x0003443B
		public static QueryFunctionExpression RoundUp(this QueryExpression number, QueryExpression numberOfDigits)
		{
			return CoreFunctions.InvokeFunction("Core.RoundUp", new QueryExpression[] { number, numberOfDigits });
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00036255 File Offset: 0x00034455
		public static QueryFunctionExpression Log10(this QueryExpression number)
		{
			return CoreFunctions.InvokeFunction("Core.Log10", new QueryExpression[] { number });
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003626B File Offset: 0x0003446B
		public static QueryFunctionExpression Power(this QueryExpression number, QueryExpression power)
		{
			return CoreFunctions.InvokeFunction("Core.Power", new QueryExpression[] { number, power });
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00036285 File Offset: 0x00034485
		public static QueryFunctionExpression IfError(this QueryExpression value, QueryExpression valueIfError)
		{
			return CoreFunctions.InvokeFunction("Core.IfError", new QueryExpression[] { value, valueIfError });
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003629F File Offset: 0x0003449F
		public static QueryFunctionExpression Plus(this QueryExpression left, QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<QueryExpression>(right, "right");
			return CoreFunctions.InvokeFunction("Core.Plus", new QueryExpression[] { left, right });
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000362D1 File Offset: 0x000344D1
		public static QueryFunctionExpression Minus(this QueryExpression left, QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<QueryExpression>(right, "right");
			return CoreFunctions.InvokeFunction("Core.Minus", new QueryExpression[] { left, right });
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00036303 File Offset: 0x00034503
		public static QueryFunctionExpression MinusSign(this QueryExpression number)
		{
			return CoreFunctions.InvokeFunction("Core.MinusSign", new QueryExpression[] { number });
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00036319 File Offset: 0x00034519
		public static QueryFunctionExpression Multiply(this QueryExpression left, QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<QueryExpression>(right, "right");
			return CoreFunctions.InvokeFunction("Core.Multiply", new QueryExpression[] { left, right });
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003634B File Offset: 0x0003454B
		public static QueryFunctionExpression Divide(this QueryExpression left, QueryExpression right)
		{
			return CoreFunctions.InvokeFunction("Core.Divide", new QueryExpression[] { left, right });
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00036365 File Offset: 0x00034565
		public static QueryFunctionExpression Divide(this QueryExpression left, QueryExpression right, QueryExpression alternateResult)
		{
			return CoreFunctions.InvokeFunction("Core.Divide", new QueryExpression[] { left, right, alternateResult });
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00036383 File Offset: 0x00034583
		public static QueryFunctionExpression DivideRaw(this QueryExpression left, QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<QueryExpression>(right, "right");
			return CoreFunctions.InvokeFunction("Core.DivideRaw", new QueryExpression[] { left, right });
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000363B8 File Offset: 0x000345B8
		internal static QueryFunctionExpression InvokeFunction(string functionName, params QueryExpression[] arguments)
		{
			return QueryExpressionBuilder.InvokeFunction<string>((string funcName, IReadOnlyList<ConceptualResultType> argumentTypes) => FunctionHelper.InvokeUserFunction(CoreFunctions.Library.GetFunctions(functionName), funcName, argumentTypes), functionName, arguments);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000363EC File Offset: 0x000345EC
		internal static QueryOperatorExpression InvokeOperator(string operatorName, IReadOnlyList<QueryExpression> arguments, bool useBinaryEquivalent)
		{
			return QueryExpressionBuilder.InvokeOperator<string>((string opName, IReadOnlyList<ConceptualResultType> argumentTypes) => FunctionHelper.ResolveUserOperator(CoreFunctions.Library.GetOperators(operatorName), opName, argumentTypes, true), operatorName, arguments, useBinaryEquivalent);
		}

		// Token: 0x04000AF7 RID: 2807
		private static readonly FunctionLibrary Library = FunctionLibrary.Core;
	}
}
