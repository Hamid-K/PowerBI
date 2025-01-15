using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FAC RID: 4012
	internal class AdobeAnalyticsQueryCompilerV1 : AdobeAnalyticsQueryCompiler<AdobeAnalyticsReportDescriptionV1>
	{
		// Token: 0x06006987 RID: 27015 RVA: 0x0016AC7A File Offset: 0x00168E7A
		public AdobeAnalyticsQueryCompilerV1(AdobeAnalyticsCube cube, IList<ParameterArguments> arguments)
			: base(cube, arguments)
		{
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x0016AC84 File Offset: 0x00168E84
		protected override AdobeAnalyticsReportDescriptionV1 CreateDescription(AdobeAnalyticsReportBuilder builder)
		{
			return new AdobeAnalyticsReportDescriptionV1(builder.ReportSuiteId, builder.Measures, builder.Dimensions, builder.DimensionToTop, builder.Segments, builder.DateStart, builder.DateEnd, builder.SortBy, builder.Filter, builder.Skip);
		}

		// Token: 0x06006989 RID: 27017 RVA: 0x0016ACD4 File Offset: 0x00168ED4
		protected override bool TryApplyDateRangeParameters(AdobeAnalyticsReportBuilder builder, IEnumerable<ParameterArguments> dateParameters)
		{
			int num = dateParameters.Count<ParameterArguments>();
			if (num > 0)
			{
				ParameterArguments parameterArguments = dateParameters.First<ParameterArguments>();
				if (num != 1 || parameterArguments.Values.Length != 2)
				{
					return false;
				}
				builder.DateStart = AdobeAnalyticsQueryCompilerV1.GetDateTime(parameterArguments.Values[0]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				builder.DateEnd = AdobeAnalyticsQueryCompilerV1.GetDateTime(parameterArguments.Values[1]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return true;
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x0016AD51 File Offset: 0x00168F51
		private static DateTime GetDateTime(Value value)
		{
			if (value.IsDate)
			{
				return value.AsDate.AsClrDateTime;
			}
			if (value.IsDateTime)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.AdobeStartAndEndOnlySupportDateTimeWithV2, null, null);
			}
			throw ValueException.NewExpressionError<Message0>(Strings.AdobeStartOrEndMustBeDateOrDateTime, null, null);
		}
	}
}
