using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class JsonException : Exception
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002FB1 File Offset: 0x000011B1
		public JsonException()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FB9 File Offset: 0x000011B9
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002FC2 File Offset: 0x000011C2
		public JsonException(string message, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FCC File Offset: 0x000011CC
		public JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FD6 File Offset: 0x000011D6
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
