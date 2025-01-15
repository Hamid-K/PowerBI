using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A1 RID: 673
	public struct InterlockedString
	{
		// Token: 0x06001247 RID: 4679 RVA: 0x000400B3 File Offset: 0x0003E2B3
		public InterlockedString(string value)
		{
			this.m_value = value;
			Interlocked.Exchange(ref this.m_value, value);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000400C9 File Offset: 0x0003E2C9
		public string InterlockedRead()
		{
			return (string)Interlocked.CompareExchange(ref this.m_value, null, null);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000400DD File Offset: 0x0003E2DD
		public void InterlockedWrite(string newValue)
		{
			Interlocked.Exchange(ref this.m_value, newValue);
		}

		// Token: 0x040006CD RID: 1741
		private object m_value;
	}
}
