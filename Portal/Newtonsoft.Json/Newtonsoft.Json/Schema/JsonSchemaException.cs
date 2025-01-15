using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AA RID: 170
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00026473 File Offset: 0x00024673
		public int LineNumber { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0002647B File Offset: 0x0002467B
		public int LinePosition { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00026483 File Offset: 0x00024683
		public string Path { get; }

		// Token: 0x06000903 RID: 2307 RVA: 0x0002648B File Offset: 0x0002468B
		public JsonSchemaException()
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00026493 File Offset: 0x00024693
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002649C File Offset: 0x0002469C
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000264A6 File Offset: 0x000246A6
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000264B0 File Offset: 0x000246B0
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
