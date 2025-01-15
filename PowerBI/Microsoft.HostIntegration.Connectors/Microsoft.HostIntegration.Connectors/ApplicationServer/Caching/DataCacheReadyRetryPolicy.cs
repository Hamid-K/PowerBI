using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200000A RID: 10
	public class DataCacheReadyRetryPolicy
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000035C7 File Offset: 0x000017C7
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000035CF File Offset: 0x000017CF
		public int RetryCount
		{
			get
			{
				return this._retryCount;
			}
			set
			{
				DataCacheReadyRetryPolicy.IntegerNonNegativeValidator.Validate(value);
				this._retryCount = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000035E8 File Offset: 0x000017E8
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000035F0 File Offset: 0x000017F0
		public int MaximumRetryIntervalInSeconds
		{
			get
			{
				return this._maxRetryInterval;
			}
			set
			{
				DataCacheReadyRetryPolicy.IntegerNonZeroNonNegativeValidator.Validate(value);
				this._maxRetryInterval = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000360C File Offset: 0x0000180C
		internal DataCacheReadyRetryPolicy(int retryCount, int maxRetryInterval)
		{
			if (retryCount < 0)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidValue"), "retryCount");
			}
			if (maxRetryInterval < 1)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidValue"), "maxRetryInterval");
			}
			this._retryCount = retryCount;
			this._maxRetryInterval = maxRetryInterval;
		}

		// Token: 0x04000048 RID: 72
		private static IntegerValidator IntegerNonNegativeValidator = new IntegerValidator(0, int.MaxValue);

		// Token: 0x04000049 RID: 73
		private static IntegerValidator IntegerNonZeroNonNegativeValidator = new IntegerValidator(1, int.MaxValue);

		// Token: 0x0400004A RID: 74
		private int _retryCount;

		// Token: 0x0400004B RID: 75
		private int _maxRetryInterval;
	}
}
