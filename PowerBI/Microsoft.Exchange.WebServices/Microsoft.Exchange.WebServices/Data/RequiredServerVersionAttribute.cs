using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(388, AllowMultiple = false, Inherited = false)]
	internal sealed class RequiredServerVersionAttribute : Attribute
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020EF File Offset: 0x000010EF
		internal RequiredServerVersionAttribute(ExchangeVersion version)
		{
			this.version = version;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020FE File Offset: 0x000010FE
		internal ExchangeVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04000002 RID: 2
		private ExchangeVersion version;
	}
}
