using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E5 RID: 485
	[Serializable]
	public class DataCacheStoreException : Exception
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0001E12D File Offset: 0x0001C32D
		public DataCacheStoreException()
		{
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0001E13E File Offset: 0x0001C33E
		public DataCacheStoreException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0001E135 File Offset: 0x0001C335
		public DataCacheStoreException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0001E148 File Offset: 0x0001C348
		protected DataCacheStoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
