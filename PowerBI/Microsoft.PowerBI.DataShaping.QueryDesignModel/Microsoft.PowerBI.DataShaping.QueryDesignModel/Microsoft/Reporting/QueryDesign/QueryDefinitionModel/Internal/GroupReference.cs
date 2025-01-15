using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000109 RID: 265
	internal sealed class GroupReference : IEquatable<GroupReference>
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0002AD4A File Offset: 0x00028F4A
		internal GroupReference(Group group, GroupReference.GroupUsage usage, int referenceId)
		{
			this.Group = group;
			this.ReferenceId = referenceId;
			this.Usage = usage;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002AD67 File Offset: 0x00028F67
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0002AD6F File Offset: 0x00028F6F
		public Group Group { get; private set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002AD78 File Offset: 0x00028F78
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0002AD80 File Offset: 0x00028F80
		internal int ReferenceId { get; private set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002AD89 File Offset: 0x00028F89
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0002AD91 File Offset: 0x00028F91
		internal GroupReference.GroupUsage Usage { get; private set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0002AD9A File Offset: 0x00028F9A
		internal bool WasReused
		{
			get
			{
				return this.Usage == GroupReference.GroupUsage.Reused || this.Usage == GroupReference.GroupUsage.Promoted;
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002ADB0 File Offset: 0x00028FB0
		internal bool RefersTo(Group group)
		{
			return this.Group == group;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0002ADBB File Offset: 0x00028FBB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GroupReference);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002ADCC File Offset: 0x00028FCC
		public override int GetHashCode()
		{
			return this.Group.GetHashCode() ^ this.ReferenceId.GetHashCode();
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002ADF3 File Offset: 0x00028FF3
		public bool Equals(GroupReference other)
		{
			return other != null && other.Group == this.Group && other.ReferenceId == this.ReferenceId;
		}

		// Token: 0x0200035B RID: 859
		internal enum GroupUsage
		{
			// Token: 0x0400121D RID: 4637
			New,
			// Token: 0x0400121E RID: 4638
			Reused,
			// Token: 0x0400121F RID: 4639
			Promoted
		}
	}
}
