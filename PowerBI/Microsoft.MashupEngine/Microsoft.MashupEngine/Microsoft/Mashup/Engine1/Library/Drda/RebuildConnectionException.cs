using System;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CB3 RID: 3251
	[Serializable]
	public class RebuildConnectionException : RuntimeException
	{
		// Token: 0x0600580A RID: 22538 RVA: 0x00002BB1 File Offset: 0x00000DB1
		public RebuildConnectionException()
		{
		}

		// Token: 0x0600580B RID: 22539 RVA: 0x00002BB9 File Offset: 0x00000DB9
		public RebuildConnectionException(string message)
			: base(message)
		{
		}

		// Token: 0x0600580C RID: 22540 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public RebuildConnectionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600580D RID: 22541 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public RebuildConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
