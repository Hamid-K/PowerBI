using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001EE RID: 494
	internal sealed class CoreFunctionLibrary : FunctionLibrary
	{
		// Token: 0x06001794 RID: 6036 RVA: 0x00040F07 File Offset: 0x0003F107
		private CoreFunctionLibrary()
			: base("Core", CoreFunctionLibrary.InitFunctionsMetadata(), CoreFunctionLibrary.InitOperatorMetadata())
		{
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00040F20 File Offset: 0x0003F120
		private static ItemCollection InitFunctionsMetadata()
		{
			ItemCollection itemCollection;
			using (XmlReader xmlReader = XmlReader.Create(typeof(CoreFunctionLibrary).Assembly.GetManifestResourceStream("Microsoft.Reporting.QueryDesign.Edm.CoreFunctions.csdl"), CoreFunctionLibrary.ReaderSettings))
			{
				itemCollection = new EdmItemCollection(new XmlReader[] { xmlReader });
			}
			return itemCollection;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00040F80 File Offset: 0x0003F180
		private static IReadOnlyList<EdmOperator> InitOperatorMetadata()
		{
			return new EdmOperator[]
			{
				new EdmOperator("Core.And", ConceptualPrimitiveResultType.Boolean, ConceptualPrimitiveResultType.Boolean)
			};
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00040FA0 File Offset: 0x0003F1A0
		internal static string FunctionNameFromAggregateFunction(AggregateFunction aggFunc)
		{
			switch (aggFunc)
			{
			case AggregateFunction.Sum:
				return "Core.Sum";
			case AggregateFunction.Average:
				return "Core.Average";
			case AggregateFunction.Count:
				return "Core.Count";
			case AggregateFunction.DistinctCount:
				return "Core.DistinctCount";
			case AggregateFunction.Min:
				return "Core.Min";
			case AggregateFunction.Max:
				return "Core.Max";
			case AggregateFunction.Median:
				return "Core.Median";
			case AggregateFunction.StandardDeviation:
				return "Core.StandardDeviation";
			case AggregateFunction.Variance:
				return "Core.Variance";
			default:
				throw new ArgumentOutOfRangeException("aggFunc");
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004101C File Offset: 0x0003F21C
		internal static bool IsAggregateFunction(QueryFunctionExpression expression)
		{
			ArgumentValidation.CheckNotNull<QueryFunctionExpression>(expression, "expression");
			return expression.Function.FullName == "Core.Average" || expression.Function.FullName == "Core.Sum" || expression.Function.FullName == "Core.Median" || expression.Function.FullName == "Core.StandardDeviation" || expression.Function.FullName == "Core.Variance" || expression.Function.FullName == "Core.Min" || expression.Function.FullName == "Core.Max" || expression.Function.FullName == "Core.Count" || expression.Function.FullName == "Core.DistinctCount";
		}

		// Token: 0x04000C78 RID: 3192
		internal const string CoreNamespace = "Core";

		// Token: 0x04000C79 RID: 3193
		private const string NsPrefix = "Core.";

		// Token: 0x04000C7A RID: 3194
		internal const string VariantTypeName = "Core.Variant";

		// Token: 0x04000C7B RID: 3195
		internal const string Not = "Core.Not";

		// Token: 0x04000C7C RID: 3196
		internal const string And = "Core.And";

		// Token: 0x04000C7D RID: 3197
		internal const string Or = "Core.Or";

		// Token: 0x04000C7E RID: 3198
		internal const string Sum = "Core.Sum";

		// Token: 0x04000C7F RID: 3199
		internal const string Median = "Core.Median";

		// Token: 0x04000C80 RID: 3200
		internal const string StandardDeviation = "Core.StandardDeviation";

		// Token: 0x04000C81 RID: 3201
		internal const string Variance = "Core.Variance";

		// Token: 0x04000C82 RID: 3202
		internal const string Count = "Core.Count";

		// Token: 0x04000C83 RID: 3203
		internal const string DistinctCount = "Core.DistinctCount";

		// Token: 0x04000C84 RID: 3204
		internal const string Average = "Core.Average";

		// Token: 0x04000C85 RID: 3205
		internal const string Min = "Core.Min";

		// Token: 0x04000C86 RID: 3206
		internal const string Max = "Core.Max";

		// Token: 0x04000C87 RID: 3207
		internal const string PercentileExc = "Core.PercentileExc";

		// Token: 0x04000C88 RID: 3208
		internal const string PercentileInc = "Core.PercentileInc";

		// Token: 0x04000C89 RID: 3209
		internal const string Contains = "Core.Contains";

		// Token: 0x04000C8A RID: 3210
		internal const string EndsWith = "Core.EndsWith";

		// Token: 0x04000C8B RID: 3211
		internal const string StartsWith = "Core.StartsWith";

		// Token: 0x04000C8C RID: 3212
		internal const string DateTimeEqualToSecond = "Core.DateTimeEqualToSecond";

		// Token: 0x04000C8D RID: 3213
		internal const string Date = "Core.Date";

		// Token: 0x04000C8E RID: 3214
		internal const string Year = "Core.Year";

		// Token: 0x04000C8F RID: 3215
		internal const string Month = "Core.Month";

		// Token: 0x04000C90 RID: 3216
		internal const string Day = "Core.Day";

		// Token: 0x04000C91 RID: 3217
		internal const string Hour = "Core.Hour";

		// Token: 0x04000C92 RID: 3218
		internal const string Minute = "Core.Minute";

		// Token: 0x04000C93 RID: 3219
		internal const string Second = "Core.Second";

		// Token: 0x04000C94 RID: 3220
		internal const string EDate = "Core.EDate";

		// Token: 0x04000C95 RID: 3221
		internal const string If = "Core.If";

		// Token: 0x04000C96 RID: 3222
		internal const string IfError = "Core.IfError";

		// Token: 0x04000C97 RID: 3223
		internal const string Plus = "Core.Plus";

		// Token: 0x04000C98 RID: 3224
		internal const string Minus = "Core.Minus";

		// Token: 0x04000C99 RID: 3225
		internal const string Multiply = "Core.Multiply";

		// Token: 0x04000C9A RID: 3226
		internal const string MinusSign = "Core.MinusSign";

		// Token: 0x04000C9B RID: 3227
		internal const string Divide = "Core.Divide";

		// Token: 0x04000C9C RID: 3228
		internal const string DivideRaw = "Core.DivideRaw";

		// Token: 0x04000C9D RID: 3229
		internal const string Between = "Core.Between";

		// Token: 0x04000C9E RID: 3230
		internal const string Ceiling = "Core.Ceiling";

		// Token: 0x04000C9F RID: 3231
		internal const string Floor = "Core.Floor";

		// Token: 0x04000CA0 RID: 3232
		internal const string Sqrt = "Core.Sqrt";

		// Token: 0x04000CA1 RID: 3233
		internal const string MinValue = "Core.MinValue";

		// Token: 0x04000CA2 RID: 3234
		internal const string MaxValue = "Core.MaxValue";

		// Token: 0x04000CA3 RID: 3235
		internal const string Int = "Core.Int";

		// Token: 0x04000CA4 RID: 3236
		internal const string RoundDown = "Core.RoundDown";

		// Token: 0x04000CA5 RID: 3237
		internal const string RoundUp = "Core.RoundUp";

		// Token: 0x04000CA6 RID: 3238
		internal const string Log10 = "Core.Log10";

		// Token: 0x04000CA7 RID: 3239
		internal const string Power = "Core.Power";

		// Token: 0x04000CA8 RID: 3240
		private static readonly XmlReaderSettings ReaderSettings = new XmlReaderSettings
		{
			DtdProcessing = DtdProcessing.Prohibit,
			XmlResolver = null
		};

		// Token: 0x04000CA9 RID: 3241
		internal static readonly CoreFunctionLibrary Instance = new CoreFunctionLibrary();
	}
}
