using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	internal class JsonSerializationException : JsonException
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004C04 File Offset: 0x00002E04
		public int LineNumber { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004C0C File Offset: 0x00002E0C
		public int LinePosition { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004C14 File Offset: 0x00002E14
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004C1C File Offset: 0x00002E1C
		public JsonSerializationException()
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004C24 File Offset: 0x00002E24
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004C2D File Offset: 0x00002E2D
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004C37 File Offset: 0x00002E37
		public JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004C41 File Offset: 0x00002E41
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004C62 File Offset: 0x00002E62
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C6C File Offset: 0x00002E6C
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004C84 File Offset: 0x00002E84
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
