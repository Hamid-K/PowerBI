using System;
using System.Runtime.Serialization;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020F1 RID: 8433
	[Serializable]
	internal sealed class NamespaceNotUnderstandException : Exception
	{
		// Token: 0x0600CF83 RID: 53123 RVA: 0x00005F33 File Offset: 0x00004133
		public NamespaceNotUnderstandException()
		{
		}

		// Token: 0x0600CF84 RID: 53124 RVA: 0x00002FDF File Offset: 0x000011DF
		public NamespaceNotUnderstandException(string message)
			: base(message)
		{
		}

		// Token: 0x0600CF85 RID: 53125 RVA: 0x00005F45 File Offset: 0x00004145
		private NamespaceNotUnderstandException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600CF86 RID: 53126 RVA: 0x00005F3B File Offset: 0x0000413B
		public NamespaceNotUnderstandException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
