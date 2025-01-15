using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F91 RID: 3985
	internal class AdobeAnalyticsQueryCompilerV2 : AdobeAnalyticsQueryCompiler<AdobeAnalyticsReportDescriptionV2>
	{
		// Token: 0x060068EA RID: 26858 RVA: 0x001685C4 File Offset: 0x001667C4
		public AdobeAnalyticsQueryCompilerV2(AdobeAnalyticsCube cube, IList<ParameterArguments> arguments)
			: base(cube, arguments)
		{
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x001685CE File Offset: 0x001667CE
		protected override AdobeAnalyticsReportDescriptionV2 CreateDescription(AdobeAnalyticsReportBuilder builder)
		{
			return AdobeAnalyticsReportDescriptionV2.New(builder);
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x001685D8 File Offset: 0x001667D8
		protected override bool TryApplyDateRangeParameters(AdobeAnalyticsReportBuilder builder, IEnumerable<ParameterArguments> dateParameters)
		{
			DateTime now = DateTime.Now;
			DateTime dateTime = now.Date;
			DateTime dateTime2 = now.Date + new TimeSpan(23, 59, 59);
			int num = dateParameters.Count<ParameterArguments>();
			if (num > 0)
			{
				ParameterArguments parameterArguments = dateParameters.First<ParameterArguments>();
				if (num != 1 || parameterArguments.Values.Length != 2)
				{
					return false;
				}
				dateTime = AdobeAnalyticsQueryCompilerV2.GetDateTime(parameterArguments.Values[0], dateTime);
				dateTime2 = AdobeAnalyticsQueryCompilerV2.GetDateTime(parameterArguments.Values[1], dateTime2);
				if (!parameterArguments.Values[0].IsNull && !parameterArguments.Values[1].IsNull && parameterArguments.Values[0].Type != parameterArguments.Values[1].Type)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.AdobeStartAndEndMustBeOfSameType, null, null);
				}
				if (parameterArguments.Values[1].IsDate)
				{
					dateTime2 += new TimeSpan(23, 59, 59);
				}
			}
			builder.DateStart = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
			builder.DateEnd = dateTime2.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x001686F7 File Offset: 0x001668F7
		private static DateTime GetDateTime(Value value, DateTime defaultValue)
		{
			if (value.IsNull)
			{
				return defaultValue;
			}
			if (value.IsDate)
			{
				return value.AsDate.AsClrDateTime;
			}
			if (value.IsDateTime)
			{
				return value.AsDateTime.AsClrDateTime;
			}
			throw ValueException.NewExpressionError<Message0>(Strings.AdobeStartOrEndMustBeDateOrDateTime, null, null);
		}

		// Token: 0x040039D2 RID: 14802
		private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
	}
}
