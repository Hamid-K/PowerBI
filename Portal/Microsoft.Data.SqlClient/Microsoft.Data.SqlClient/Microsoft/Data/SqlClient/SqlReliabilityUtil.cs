using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A4 RID: 164
	internal static class SqlReliabilityUtil
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x000270A8 File Offset: 0x000252A8
		internal static AggregateException ConfigurableRetryFail(IList<Exception> exceptions, SqlRetryLogicBase retryLogic, bool canceled)
		{
			if (!canceled)
			{
				return new AggregateException(StringsHelper.GetString(Strings.SqlRetryLogic_RetryExceeded, new object[] { retryLogic.NumberOfTries }), exceptions);
			}
			return new AggregateException(StringsHelper.GetString(Strings.SqlRetryLogic_RetryCanceled, new object[] { retryLogic.Current }), exceptions);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00027101 File Offset: 0x00025301
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, int value, int minValue, int MaxValue)
		{
			return new ArgumentOutOfRangeException(paramName, StringsHelper.GetString(Strings.SqlRetryLogic_InvalidRange, new object[] { value, minValue, MaxValue }));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00027134 File Offset: 0x00025334
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, TimeSpan value, TimeSpan minValue, TimeSpan MaxValue)
		{
			return new ArgumentOutOfRangeException(paramName, StringsHelper.GetString(Strings.SqlRetryLogic_InvalidRange, new object[] { value, minValue, MaxValue }));
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00027167 File Offset: 0x00025367
		internal static ArgumentNullException ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002716F File Offset: 0x0002536F
		internal static ArgumentOutOfRangeException InvalidMinAndMaxPair(string minParamName, TimeSpan minValue, string maxParamName, TimeSpan maxValue)
		{
			return new ArgumentOutOfRangeException(minParamName, StringsHelper.GetString(Strings.SqlRetryLogic_InvalidMinMaxPair, new object[] { minValue, maxValue, minParamName, maxParamName }));
		}
	}
}
