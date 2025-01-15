using System;
using System.Runtime.Serialization;

namespace System.Text.Json
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal sealed class JsonReaderException : JsonException
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00009C52 File Offset: 0x00007E52
		public JsonReaderException(string message, long lineNumber, long bytePositionInLine)
			: base(message, null, new long?(lineNumber), new long?(bytePositionInLine))
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009C68 File Offset: 0x00007E68
		private JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
