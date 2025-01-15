using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal class JsonWriterException : JsonException
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001002F File Offset: 0x0000E22F
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010037 File Offset: 0x0000E237
		public JsonWriterException()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001003F File Offset: 0x0000E23F
		public JsonWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010048 File Offset: 0x0000E248
		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010052 File Offset: 0x0000E252
		public JsonWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001005C File Offset: 0x0000E25C
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001006D File Offset: 0x0000E26D
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001007C File Offset: 0x0000E27C
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}
