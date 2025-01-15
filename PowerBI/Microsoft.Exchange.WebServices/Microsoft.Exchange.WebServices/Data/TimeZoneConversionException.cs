using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public class TimeZoneConversionException : ServiceLocalException
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x0003CFB3 File Offset: 0x0003BFB3
		public TimeZoneConversionException()
		{
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0003CFBB File Offset: 0x0003BFBB
		public TimeZoneConversionException(string message)
			: base(message)
		{
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0003CFC4 File Offset: 0x0003BFC4
		public TimeZoneConversionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
