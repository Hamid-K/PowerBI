using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000009 RID: 9
	public class UnsupportedMediaTypeException : Exception
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002775 File Offset: 0x00000975
		public UnsupportedMediaTypeException(string message, MediaTypeHeaderValue mediaType)
			: base(message)
		{
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = mediaType;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002793 File Offset: 0x00000993
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000279B File Offset: 0x0000099B
		public MediaTypeHeaderValue MediaType { get; private set; }
	}
}
