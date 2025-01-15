using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public class ConfigStoreException : Exception
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0001E12D File Offset: 0x0001C32D
		public ConfigStoreException()
		{
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001E135 File Offset: 0x0001C335
		public ConfigStoreException(string exceptionMessage)
			: base(exceptionMessage)
		{
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001E13E File Offset: 0x0001C33E
		public ConfigStoreException(string exceptionMessage, Exception inner)
			: base(exceptionMessage, inner)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E148 File Offset: 0x0001C348
		protected ConfigStoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
