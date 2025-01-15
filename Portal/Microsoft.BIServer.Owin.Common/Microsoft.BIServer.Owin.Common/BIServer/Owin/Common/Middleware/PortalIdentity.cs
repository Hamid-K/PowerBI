using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common.Services;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000020 RID: 32
	internal sealed class PortalIdentity : GenericIdentity, IUserContextContainer
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000384D File Offset: 0x00001A4D
		public PortalIdentity(IWebHostUserContext userContext, string authenticationType, IEnumerable<KeyValuePair<string, string>> cookies)
			: base(userContext.UserName, authenticationType)
		{
			this.userContext = userContext;
			this.authenticationType = authenticationType;
			this.userCookies = cookies;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003871 File Offset: 0x00001A71
		public object UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003879 File Offset: 0x00001A79
		public override string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003881 File Offset: 0x00001A81
		public override string Name
		{
			get
			{
				return this.userContext.UserName;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000388E File Offset: 0x00001A8E
		public IEnumerable<KeyValuePair<string, string>> UserCookies
		{
			get
			{
				return this.userCookies;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly IWebHostUserContext userContext;

		// Token: 0x0400005B RID: 91
		private readonly string authenticationType;

		// Token: 0x0400005C RID: 92
		private readonly IEnumerable<KeyValuePair<string, string>> userCookies;
	}
}
