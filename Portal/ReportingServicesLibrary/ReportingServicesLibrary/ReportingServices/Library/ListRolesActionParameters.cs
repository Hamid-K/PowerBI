using System;
using System.Globalization;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E4 RID: 484
	internal sealed class ListRolesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0003A311 File Offset: 0x00038511
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0003A319 File Offset: 0x00038519
		public SecurityScopeEnum Scope
		{
			get
			{
				return this.m_scope;
			}
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003A322 File Offset: 0x00038522
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0003A32A File Offset: 0x0003852A
		public string ItemPath
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0003A333 File Offset: 0x00038533
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x0003A33B File Offset: 0x0003853B
		public Role[] Roles
		{
			get
			{
				return this.m_roles;
			}
			set
			{
				this.m_roles = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0003A344 File Offset: 0x00038544
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.Scope);
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000666 RID: 1638
		private SecurityScopeEnum m_scope;

		// Token: 0x04000667 RID: 1639
		private string m_path;

		// Token: 0x04000668 RID: 1640
		private Role[] m_roles;
	}
}
