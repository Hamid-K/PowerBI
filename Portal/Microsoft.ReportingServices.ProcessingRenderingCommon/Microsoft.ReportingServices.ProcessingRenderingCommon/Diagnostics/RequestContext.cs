using System;
using System.Collections.Specialized;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000085 RID: 133
	public abstract class RequestContext : RequestContextBase
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003B7 RID: 951
		public abstract bool IsClientConnected { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003B8 RID: 952
		public abstract IPrincipal User { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public virtual string ClientPageId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003BA RID: 954
		public abstract bool IsClientLocal { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000BEF7 File Offset: 0x0000A0F7
		public virtual bool HasBrowserCapabilities
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000BEFA File Offset: 0x0000A0FA
		public virtual NameValueCollection BrowserCapabilities
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003BD RID: 957
		public abstract Uri ReportServerVirtualDirectoryUri { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000BEFD File Offset: 0x0000A0FD
		public virtual string CrescentXapLocation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003BF RID: 959
		public abstract string Namespace { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003C0 RID: 960
		public abstract bool IsMemberOfWindowsAdminGroup { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000BF00 File Offset: 0x0000A100
		public virtual string ContextUrl
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000BF07 File Offset: 0x0000A107
		public virtual bool GWRefreshFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000BF0A File Offset: 0x0000A10A
		public virtual string TenantName
		{
			get
			{
				return null;
			}
		}
	}
}
