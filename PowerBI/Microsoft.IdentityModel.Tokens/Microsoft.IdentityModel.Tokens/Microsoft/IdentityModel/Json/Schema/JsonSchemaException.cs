using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000AB RID: 171
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	internal class JsonSchemaException : JsonException
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00026457 File Offset: 0x00024657
		public int LineNumber { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0002645F File Offset: 0x0002465F
		public int LinePosition { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00026467 File Offset: 0x00024667
		public string Path { get; }

		// Token: 0x06000904 RID: 2308 RVA: 0x0002646F File Offset: 0x0002466F
		public JsonSchemaException()
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00026477 File Offset: 0x00024677
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00026480 File Offset: 0x00024680
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002648A File Offset: 0x0002468A
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00026494 File Offset: 0x00024694
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
