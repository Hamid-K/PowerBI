using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000066 RID: 102
	public sealed class CloudContext
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000E776 File Offset: 0x0000C976
		internal CloudContext()
		{
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000E77E File Offset: 0x0000C97E
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000E795 File Offset: 0x0000C995
		public string RoleName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.roleName))
				{
					return this.roleName;
				}
				return null;
			}
			set
			{
				this.roleName = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000E79E File Offset: 0x0000C99E
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000E7B5 File Offset: 0x0000C9B5
		public string RoleInstance
		{
			get
			{
				if (!string.IsNullOrEmpty(this.roleInstance))
				{
					return this.roleInstance;
				}
				return null;
			}
			set
			{
				this.roleInstance = value;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E7BE File Offset: 0x0000C9BE
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.CloudRole, this.RoleName);
			tags.UpdateTagValue(ContextTagKeys.Keys.CloudRoleInstance, this.RoleInstance);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		internal void CopyTo(CloudContext target)
		{
			Tags.CopyTagValue(this.RoleName, ref target.roleName);
			Tags.CopyTagValue(this.RoleInstance, ref target.roleInstance);
		}

		// Token: 0x04000152 RID: 338
		private string roleName;

		// Token: 0x04000153 RID: 339
		private string roleInstance;
	}
}
