using System;
using System.Security.Principal;
using Microsoft.BIServer.Configuration;
using Microsoft.BIServer.Owin.Common.Services;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.BIServer.Owin.Common.Middleware
{
	// Token: 0x02000022 RID: 34
	public sealed class RsUserContext : IWebHostUserContext
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004050 File Offset: 0x00002250
		public RsUserContext(IIdentity identity, object token, Microsoft.BIServer.Configuration.AuthenticationType authenticationType)
		{
			this.identity = identity;
			this.token = token;
			this.authenticationType = authenticationType;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000406D File Offset: 0x0000226D
		public string UserName
		{
			get
			{
				return this.identity.Name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000407A File Offset: 0x0000227A
		public object Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004082 File Offset: 0x00002282
		public Microsoft.BIServer.Configuration.AuthenticationType AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000408C File Offset: 0x0000228C
		public Microsoft.ReportingServices.Interfaces.AuthenticationType RSInterfacesAuthenticationType
		{
			get
			{
				return (Microsoft.ReportingServices.Interfaces.AuthenticationType)Enum.Parse(typeof(Microsoft.ReportingServices.Interfaces.AuthenticationType), this.AuthenticationType.ToString());
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000040C1 File Offset: 0x000022C1
		public IIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000061 RID: 97
		private readonly object token;

		// Token: 0x04000062 RID: 98
		private readonly Microsoft.BIServer.Configuration.AuthenticationType authenticationType;

		// Token: 0x04000063 RID: 99
		private readonly IIdentity identity;
	}
}
