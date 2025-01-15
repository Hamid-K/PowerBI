using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A65 RID: 2661
	internal sealed class HttpResponse : Response
	{
		// Token: 0x06004A40 RID: 19008 RVA: 0x000F799C File Offset: 0x000F5B9C
		public HttpResponse(MashupHttpWebRequest httpWebRequest, MashupHttpWebResponse httpWebResponse, bool keepCompressed, Action<Exception, long> errorLogger, IEngineHost host, Func<HttpResponse> getNewResponse = null, WebException innerException = null)
			: base(errorLogger, innerException)
		{
			this.httpWebRequest = httpWebRequest;
			this.httpWebResponse = httpWebResponse;
			this.keepCompressed = keepCompressed;
			this.host = host;
			this.getNewResponse = getNewResponse;
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000F79CD File Offset: 0x000F5BCD
		public override string CharacterSet
		{
			get
			{
				return this.httpWebResponse.CharacterSet;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06004A42 RID: 19010 RVA: 0x000F79DA File Offset: 0x000F5BDA
		public override string ContentEncoding
		{
			get
			{
				return this.httpWebResponse.ContentEncoding;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x000F79E7 File Offset: 0x000F5BE7
		public override long ContentLength
		{
			get
			{
				return this.httpWebResponse.ContentLength;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x000F79F4 File Offset: 0x000F5BF4
		public override string ContentType
		{
			get
			{
				return this.httpWebResponse.ContentType;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x000F7A01 File Offset: 0x000F5C01
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.httpWebResponse.Headers;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x000F7A0E File Offset: 0x000F5C0E
		public MashupHttpWebResponse HttpWebResponse
		{
			get
			{
				return this.httpWebResponse;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x000F7A16 File Offset: 0x000F5C16
		public override Uri ResponseUri
		{
			get
			{
				return this.httpWebResponse.ResponseUri;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x000F7A23 File Offset: 0x000F5C23
		public override int StatusCode
		{
			get
			{
				return (int)this.httpWebResponse.StatusCode;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x000F7A30 File Offset: 0x000F5C30
		public override string StatusDescription
		{
			get
			{
				return this.httpWebResponse.StatusDescription;
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x000F7A3D File Offset: 0x000F5C3D
		public override void Close()
		{
			this.httpWebResponse.Close();
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x000F7A4A File Offset: 0x000F5C4A
		public override Stream GetResponseStream()
		{
			return new Response.WebResponseStream(this.httpWebRequest, this.GetStream(this.httpWebResponse), this.errorLogger, this.host, (this.getNewResponse == null) ? null : new Func<Tuple<WebRequest, Stream>>(this.GetNewRequestAndStream));
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x000F7A86 File Offset: 0x000F5C86
		public override RecordValue GetHeaders()
		{
			return RequestHeaders.GetHeaders(this.Headers, this.ContentEncoding);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x000F7A9C File Offset: 0x000F5C9C
		private Tuple<WebRequest, Stream> GetNewRequestAndStream()
		{
			HttpResponse httpResponse = this.getNewResponse();
			this.httpWebRequest = httpResponse.httpWebRequest;
			this.httpWebResponse = httpResponse.httpWebResponse;
			return new Tuple<WebRequest, Stream>(this.httpWebRequest, this.GetStream(this.httpWebResponse));
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000F7AE4 File Offset: 0x000F5CE4
		private Stream GetStream(WebResponse response)
		{
			if (!this.keepCompressed)
			{
				return response.GetDecompressedResponseStream();
			}
			return response.GetResponseStream();
		}

		// Token: 0x04002781 RID: 10113
		private readonly IEngineHost host;

		// Token: 0x04002782 RID: 10114
		private readonly bool keepCompressed;

		// Token: 0x04002783 RID: 10115
		private readonly Func<HttpResponse> getNewResponse;

		// Token: 0x04002784 RID: 10116
		private MashupHttpWebRequest httpWebRequest;

		// Token: 0x04002785 RID: 10117
		private MashupHttpWebResponse httpWebResponse;
	}
}
