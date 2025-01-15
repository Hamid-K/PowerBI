using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200204B RID: 8267
	[Serializable]
	public class ConvertCredentialsException : Exception
	{
		// Token: 0x0600CA4E RID: 51790 RVA: 0x00002FDF File Offset: 0x000011DF
		public ConvertCredentialsException(string message)
			: base(message)
		{
		}

		// Token: 0x0600CA4F RID: 51791 RVA: 0x00005F3B File Offset: 0x0000413B
		public ConvertCredentialsException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600CA50 RID: 51792 RVA: 0x00005F45 File Offset: 0x00004145
		protected ConvertCredentialsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
