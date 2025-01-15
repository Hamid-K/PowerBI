using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A53 RID: 2643
	internal sealed class FtpResponse : Response
	{
		// Token: 0x060049D5 RID: 18901 RVA: 0x000F61C8 File Offset: 0x000F43C8
		public FtpResponse(FtpWebRequest ftpWebRequest, FtpWebResponse ftpWebResponse, Action<Exception, long> errorLogger, IEngineHost host)
			: base(errorLogger)
		{
			this.ftpWebRequest = ftpWebRequest;
			this.ftpWebResponse = ftpWebResponse;
			this.host = host;
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x000020FA File Offset: 0x000002FA
		public override string CharacterSet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x000020FA File Offset: 0x000002FA
		public override string ContentEncoding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x000F61E7 File Offset: 0x000F43E7
		public override long ContentLength
		{
			get
			{
				return this.ftpWebResponse.ContentLength;
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x000020FA File Offset: 0x000002FA
		public override string ContentType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x000F61F4 File Offset: 0x000F43F4
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.ftpWebResponse.Headers;
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x000F6201 File Offset: 0x000F4401
		public override Uri ResponseUri
		{
			get
			{
				return this.ftpWebResponse.ResponseUri;
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x000F620E File Offset: 0x000F440E
		public override int StatusCode
		{
			get
			{
				return (int)this.ftpWebResponse.StatusCode;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x000F621B File Offset: 0x000F441B
		public override string StatusDescription
		{
			get
			{
				return this.ftpWebResponse.StatusDescription;
			}
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x000F6228 File Offset: 0x000F4428
		public override void Close()
		{
			this.ftpWebResponse.Close();
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x000F6235 File Offset: 0x000F4435
		public override Stream GetResponseStream()
		{
			return new Response.WebResponseStream(this.ftpWebRequest, this.ftpWebResponse.GetResponseStream(), this.errorLogger, this.host, null);
		}

		// Token: 0x0400274D RID: 10061
		private readonly FtpWebRequest ftpWebRequest;

		// Token: 0x0400274E RID: 10062
		private readonly FtpWebResponse ftpWebResponse;

		// Token: 0x0400274F RID: 10063
		private readonly IEngineHost host;
	}
}
