using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	internal class JsonException : Exception
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002FC5 File Offset: 0x000011C5
		public JsonException()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FCD File Offset: 0x000011CD
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002FD6 File Offset: 0x000011D6
		public JsonException(string message, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FE0 File Offset: 0x000011E0
		public JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FEA File Offset: 0x000011EA
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
