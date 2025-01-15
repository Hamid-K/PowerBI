using System;
using System.IO;
using System.Net;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4D RID: 2637
	internal abstract class DelegatingHttpWebResponse : MashupHttpWebResponse
	{
		// Token: 0x0600499E RID: 18846 RVA: 0x000F5A51 File Offset: 0x000F3C51
		public DelegatingHttpWebResponse(MashupHttpWebResponse response)
		{
			this.response = response;
		}

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x000F5A60 File Offset: 0x000F3C60
		public override string CharacterSet
		{
			get
			{
				return this.response.CharacterSet;
			}
		}

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000F5A6D File Offset: 0x000F3C6D
		public override string ContentEncoding
		{
			get
			{
				return this.response.ContentEncoding;
			}
		}

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x000F5A7A File Offset: 0x000F3C7A
		public override long ContentLength
		{
			get
			{
				return this.response.ContentLength;
			}
		}

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x000F5A87 File Offset: 0x000F3C87
		public override string ContentType
		{
			get
			{
				return this.response.ContentType;
			}
		}

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x000F5A94 File Offset: 0x000F3C94
		// (set) Token: 0x060049A4 RID: 18852 RVA: 0x000F5AA1 File Offset: 0x000F3CA1
		public override CookieCollection Cookies
		{
			get
			{
				return this.response.Cookies;
			}
			set
			{
				this.response.Cookies = value;
			}
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x000F5AAF File Offset: 0x000F3CAF
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x000F5ABC File Offset: 0x000F3CBC
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this.response.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x000F5AC9 File Offset: 0x000F3CC9
		public override DateTime LastModified
		{
			get
			{
				return this.response.LastModified;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x000F5AD6 File Offset: 0x000F3CD6
		public override string Method
		{
			get
			{
				return this.response.Method;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x000F5AE3 File Offset: 0x000F3CE3
		public override Version ProtocolVersion
		{
			get
			{
				return this.response.ProtocolVersion;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x000F5AF0 File Offset: 0x000F3CF0
		public override Uri ResponseUri
		{
			get
			{
				return this.response.ResponseUri;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x000F5AFD File Offset: 0x000F3CFD
		public override string Server
		{
			get
			{
				return this.response.Server;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x000F5B0A File Offset: 0x000F3D0A
		public override HttpStatusCode StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x000F5B17 File Offset: 0x000F3D17
		public override string StatusDescription
		{
			get
			{
				return this.response.StatusDescription;
			}
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x000F5B24 File Offset: 0x000F3D24
		public override void Close()
		{
			this.response.Close();
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x000F5B31 File Offset: 0x000F3D31
		public override string GetResponseHeader(string headerName)
		{
			return this.response.GetResponseHeader(headerName);
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x000F5B3F File Offset: 0x000F3D3F
		public override Stream GetResponseStream()
		{
			return this.response.GetResponseStream();
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x000F5B4C File Offset: 0x000F3D4C
		public override void Buffer()
		{
			this.response.Buffer();
		}

		// Token: 0x04002746 RID: 10054
		private readonly MashupHttpWebResponse response;
	}
}
