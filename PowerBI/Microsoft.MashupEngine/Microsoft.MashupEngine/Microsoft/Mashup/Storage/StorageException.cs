using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002091 RID: 8337
	[Serializable]
	public class StorageException : Exception
	{
		// Token: 0x0600CC0F RID: 52239 RVA: 0x00002FDF File Offset: 0x000011DF
		public StorageException(string message)
			: base(message)
		{
		}

		// Token: 0x0600CC10 RID: 52240 RVA: 0x00005F3B File Offset: 0x0000413B
		public StorageException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600CC11 RID: 52241 RVA: 0x00005F45 File Offset: 0x00004145
		protected StorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
