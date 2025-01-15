using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x0200000F RID: 15
	public class ObjectContent<T> : ObjectContent
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000029E2 File Offset: 0x00000BE2
		public ObjectContent(T value, MediaTypeFormatter formatter)
			: this(value, formatter, null)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029ED File Offset: 0x00000BED
		public ObjectContent(T value, MediaTypeFormatter formatter, string mediaType)
			: this(value, formatter, ObjectContent.BuildHeaderValue(mediaType))
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029FD File Offset: 0x00000BFD
		public ObjectContent(T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
			: base(typeof(T), value, formatter, mediaType)
		{
		}
	}
}
