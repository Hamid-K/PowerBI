using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000946 RID: 2374
	[Serializable]
	public class UnsupportedCcsidException : Exception
	{
		// Token: 0x0600439D RID: 17309 RVA: 0x00005F33 File Offset: 0x00004133
		public UnsupportedCcsidException()
		{
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x00002FDF File Offset: 0x000011DF
		public UnsupportedCcsidException(string message)
			: base(message)
		{
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x00005F3B File Offset: 0x0000413B
		public UnsupportedCcsidException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
