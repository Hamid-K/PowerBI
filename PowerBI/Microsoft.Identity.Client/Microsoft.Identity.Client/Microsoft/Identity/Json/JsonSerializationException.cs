using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Json
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal class JsonSerializationException : JsonException
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public int LineNumber { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public int LinePosition { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004C00 File Offset: 0x00002E00
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004C08 File Offset: 0x00002E08
		public JsonSerializationException()
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004C10 File Offset: 0x00002E10
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004C19 File Offset: 0x00002E19
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004C23 File Offset: 0x00002E23
		public JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004C2D File Offset: 0x00002E2D
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004C4E File Offset: 0x00002E4E
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C58 File Offset: 0x00002E58
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004C70 File Offset: 0x00002E70
		internal static JsonSerializationException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int num;
			int num2;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				num = lineInfo.LineNumber;
				num2 = lineInfo.LinePosition;
			}
			else
			{
				num = 0;
				num2 = 0;
			}
			return new JsonSerializationException(message, path, num, num2, ex);
		}
	}
}
