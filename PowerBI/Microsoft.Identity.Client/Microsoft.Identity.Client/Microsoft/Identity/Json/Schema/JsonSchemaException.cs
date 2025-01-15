using System;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000AA RID: 170
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	internal class JsonSchemaException : JsonException
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00025DEB File Offset: 0x00023FEB
		public int LineNumber { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00025DF3 File Offset: 0x00023FF3
		public int LinePosition { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00025DFB File Offset: 0x00023FFB
		public string Path { get; }

		// Token: 0x060008FA RID: 2298 RVA: 0x00025E03 File Offset: 0x00024003
		public JsonSchemaException()
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00025E0B File Offset: 0x0002400B
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00025E14 File Offset: 0x00024014
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00025E1E File Offset: 0x0002401E
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00025E28 File Offset: 0x00024028
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
