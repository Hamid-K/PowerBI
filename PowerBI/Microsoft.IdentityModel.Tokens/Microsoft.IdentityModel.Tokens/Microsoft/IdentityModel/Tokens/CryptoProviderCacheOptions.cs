using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000128 RID: 296
	public class CryptoProviderCacheOptions
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00039AC2 File Offset: 0x00037CC2
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x00039ACA File Offset: 0x00037CCA
		public int SizeLimit
		{
			get
			{
				return this._sizeLimit;
			}
			set
			{
				if (value <= 10)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("SizeLimit", LogHelper.FormatInvariant("IDX10901: CryptoProviderCacheOptions.SizeLimit must be greater than 10. Value: '{0}'", new object[] { LogHelper.MarkAsNonPII(value) })));
				}
				this._sizeLimit = value;
			}
		}

		// Token: 0x040004A6 RID: 1190
		private int _sizeLimit = CryptoProviderCacheOptions.DefaultSizeLimit;

		// Token: 0x040004A7 RID: 1191
		public static readonly int DefaultSizeLimit = 1000;
	}
}
