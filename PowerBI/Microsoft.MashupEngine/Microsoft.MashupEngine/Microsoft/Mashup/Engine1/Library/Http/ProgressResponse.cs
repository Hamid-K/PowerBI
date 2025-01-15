using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A70 RID: 2672
	internal class ProgressResponse : Response
	{
		// Token: 0x06004AC6 RID: 19142 RVA: 0x000F8892 File Offset: 0x000F6A92
		public ProgressResponse(Response response, IHostProgress hostProgress)
			: base(null)
		{
			this.response = response;
			this.hostProgress = hostProgress;
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x000F88A9 File Offset: 0x000F6AA9
		public override long ContentLength
		{
			get
			{
				return this.response.ContentLength;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x000F88B6 File Offset: 0x000F6AB6
		public override string ContentType
		{
			get
			{
				return this.response.ContentType;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x000F88C3 File Offset: 0x000F6AC3
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x000F88D0 File Offset: 0x000F6AD0
		public override void Close()
		{
			this.response.Close();
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x000F88DD File Offset: 0x000F6ADD
		public override Stream GetResponseStream()
		{
			return new ProgressStream(this.response.GetResponseStream(), this.hostProgress);
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x000F88F5 File Offset: 0x000F6AF5
		public override Uri ResponseUri
		{
			get
			{
				return this.response.ResponseUri;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06004ACD RID: 19149 RVA: 0x000F8902 File Offset: 0x000F6B02
		public override int StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x000F890F File Offset: 0x000F6B0F
		public override string StatusDescription
		{
			get
			{
				return this.response.StatusDescription;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x000F891C File Offset: 0x000F6B1C
		public override string CharacterSet
		{
			get
			{
				return this.response.CharacterSet;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x000F8929 File Offset: 0x000F6B29
		public override string ContentEncoding
		{
			get
			{
				return this.response.ContentEncoding;
			}
		}

		// Token: 0x040027A5 RID: 10149
		private Response response;

		// Token: 0x040027A6 RID: 10150
		private IHostProgress hostProgress;
	}
}
