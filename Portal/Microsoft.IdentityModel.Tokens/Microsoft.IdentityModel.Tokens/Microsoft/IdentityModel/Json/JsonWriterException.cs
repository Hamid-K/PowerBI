using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	internal class JsonWriterException : JsonException
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001024F File Offset: 0x0000E44F
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010257 File Offset: 0x0000E457
		public JsonWriterException()
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001025F File Offset: 0x0000E45F
		public JsonWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00010268 File Offset: 0x0000E468
		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010272 File Offset: 0x0000E472
		public JsonWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001027C File Offset: 0x0000E47C
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001028D File Offset: 0x0000E48D
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001029C File Offset: 0x0000E49C
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}
