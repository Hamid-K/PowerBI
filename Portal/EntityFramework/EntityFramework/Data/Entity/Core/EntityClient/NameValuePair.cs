using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005E2 RID: 1506
	internal sealed class NameValuePair
	{
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x00104F24 File Offset: 0x00103124
		// (set) Token: 0x06004997 RID: 18839 RVA: 0x00104F2C File Offset: 0x0010312C
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1014));
				}
				this._next = value;
			}
		}

		// Token: 0x040019FA RID: 6650
		private NameValuePair _next;
	}
}
