using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E6 RID: 486
	internal sealed class CreateRoleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0003A3BD File Offset: 0x000385BD
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0003A3C5 File Offset: 0x000385C5
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0003A3CE File Offset: 0x000385CE
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x0003A3D6 File Offset: 0x000385D6
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003A3DF File Offset: 0x000385DF
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x0003A3E7 File Offset: 0x000385E7
		public string[] TaskIDs
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003A3F0 File Offset: 0x000385F0
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.RoleName, this.Description);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0003A40D File Offset: 0x0003860D
		internal override void Validate()
		{
			if (this.RoleName == null)
			{
				throw new MissingParameterException("Name");
			}
			if (this.TaskIDs == null)
			{
				throw new MissingParameterException("TaskIDs");
			}
		}

		// Token: 0x04000669 RID: 1641
		private string m_roleName;

		// Token: 0x0400066A RID: 1642
		private string m_description;

		// Token: 0x0400066B RID: 1643
		private string[] m_tasks;
	}
}
