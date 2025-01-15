using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A51 RID: 2641
	internal sealed class FileResponse : Response
	{
		// Token: 0x060049C2 RID: 18882 RVA: 0x000F5F7A File Offset: 0x000F417A
		public FileResponse(IEngineHost host, FileWebRequest fileWebRequest, FileWebResponse fileWebResponse, Action<Exception, long> errorLogger)
			: base(errorLogger)
		{
			this.host = host;
			this.fileWebRequest = fileWebRequest;
			this.fileWebResponse = fileWebResponse;
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x000020FA File Offset: 0x000002FA
		public override string CharacterSet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x000020FA File Offset: 0x000002FA
		public override string ContentEncoding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x000F5F99 File Offset: 0x000F4199
		public override long ContentLength
		{
			get
			{
				return this.fileWebResponse.ContentLength;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x000F5FA6 File Offset: 0x000F41A6
		public override string ContentType
		{
			get
			{
				return this.fileWebResponse.ContentType;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x000F5FB3 File Offset: 0x000F41B3
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.fileWebResponse.Headers;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x000F5FC0 File Offset: 0x000F41C0
		public override Uri ResponseUri
		{
			get
			{
				return this.fileWebResponse.ResponseUri;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x00002105 File Offset: 0x00000305
		public override int StatusCode
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x000020FA File Offset: 0x000002FA
		public override string StatusDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x000F5FCD File Offset: 0x000F41CD
		public override void Close()
		{
			this.fileWebResponse.Close();
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x000F5FDA File Offset: 0x000F41DA
		public override Stream GetResponseStream()
		{
			return new Response.WebResponseStream(this.fileWebRequest, this.fileWebResponse.GetResponseStream(), this.errorLogger, this.host, null);
		}

		// Token: 0x0400274A RID: 10058
		private readonly IEngineHost host;

		// Token: 0x0400274B RID: 10059
		private readonly FileWebRequest fileWebRequest;

		// Token: 0x0400274C RID: 10060
		private readonly FileWebResponse fileWebResponse;
	}
}
