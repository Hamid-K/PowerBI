using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001EA RID: 490
	internal sealed class GetRolePropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003A734 File Offset: 0x00038934
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0003A73C File Offset: 0x0003893C
		public string RoleName
		{
			get
			{
				return this.m_roleName;
			}
			set
			{
				this.m_roleName = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0003A745 File Offset: 0x00038945
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x0003A74D File Offset: 0x0003894D
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0003A756 File Offset: 0x00038956
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0003A75E File Offset: 0x0003895E
		public Task[] Tasks
		{
			get
			{
				return this.m_tasks;
			}
			set
			{
				this.m_tasks = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0003A767 File Offset: 0x00038967
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0003A76F File Offset: 0x0003896F
		public string SiteName
		{
			get
			{
				return this.m_siteName;
			}
			set
			{
				this.m_siteName = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0003A778 File Offset: 0x00038978
		internal override string InputTrace
		{
			get
			{
				return this.RoleName;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0003A780 File Offset: 0x00038980
		internal override string OutputTrace
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003A788 File Offset: 0x00038988
		internal override void Validate()
		{
			if (this.RoleName == null)
			{
				throw new MissingParameterException("Name");
			}
			if (this.RoleName.Length == 0 || this.RoleName.Length > 260)
			{
				throw new InvalidParameterException("Name");
			}
		}

		// Token: 0x0400066D RID: 1645
		private string m_roleName;

		// Token: 0x0400066E RID: 1646
		private string m_description;

		// Token: 0x0400066F RID: 1647
		private Task[] m_tasks;

		// Token: 0x04000670 RID: 1648
		private string m_siteName;
	}
}
