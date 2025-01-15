using System;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000328 RID: 808
	public class TimeExpiration : ExpirationDefinition
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x0006FA88 File Offset: 0x0006DC88
		internal static TimeExpiration IntToThis(int expireCacheOnTime)
		{
			return new TimeExpiration
			{
				Minutes = expireCacheOnTime
			};
		}

		// Token: 0x04000AEF RID: 2799
		public int Minutes;
	}
}
