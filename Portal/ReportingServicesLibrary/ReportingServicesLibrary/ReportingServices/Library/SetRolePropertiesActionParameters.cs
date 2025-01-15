using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001EC RID: 492
	internal sealed class SetRolePropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0003A865 File Offset: 0x00038A65
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x0003A86D File Offset: 0x00038A6D
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

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0003A876 File Offset: 0x00038A76
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x0003A87E File Offset: 0x00038A7E
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

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0003A887 File Offset: 0x00038A87
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x0003A88F File Offset: 0x00038A8F
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0003A898 File Offset: 0x00038A98
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.RoleName, this.Description);
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003A8B5 File Offset: 0x00038AB5
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

		// Token: 0x04000671 RID: 1649
		private string m_roleName;

		// Token: 0x04000672 RID: 1650
		private string m_description;

		// Token: 0x04000673 RID: 1651
		private string[] m_tasks;
	}
}
