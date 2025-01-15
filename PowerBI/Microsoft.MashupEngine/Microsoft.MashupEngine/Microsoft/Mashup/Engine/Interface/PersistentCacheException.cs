using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public class PersistentCacheException : RuntimeException
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public PersistentCacheException()
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public PersistentCacheException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00002BC2 File Offset: 0x00000DC2
		protected PersistentCacheException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
