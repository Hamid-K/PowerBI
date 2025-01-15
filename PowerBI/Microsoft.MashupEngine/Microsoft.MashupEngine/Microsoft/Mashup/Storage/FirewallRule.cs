using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002073 RID: 8307
	public class FirewallRule : IEquatable<FirewallRule>
	{
		// Token: 0x0600CB47 RID: 52039 RVA: 0x000020FD File Offset: 0x000002FD
		public FirewallRule()
		{
		}

		// Token: 0x0600CB48 RID: 52040 RVA: 0x002883FF File Offset: 0x002865FF
		public FirewallRule(Resource resource, FirewallGroupType firewallGroupType, string customGroupName = null, bool? isTrusted = null)
		{
			this.resource = resource;
			this.firewallGroupType = firewallGroupType;
			this.customGroupName = customGroupName;
			this.isTrusted = isTrusted;
		}

		// Token: 0x170030F1 RID: 12529
		// (get) Token: 0x0600CB49 RID: 52041 RVA: 0x00288424 File Offset: 0x00286624
		// (set) Token: 0x0600CB4A RID: 52042 RVA: 0x0028842C File Offset: 0x0028662C
		public Resource Resource
		{
			get
			{
				return this.resource;
			}
			set
			{
				this.resource = value;
			}
		}

		// Token: 0x170030F2 RID: 12530
		// (get) Token: 0x0600CB4B RID: 52043 RVA: 0x00288435 File Offset: 0x00286635
		// (set) Token: 0x0600CB4C RID: 52044 RVA: 0x0028843D File Offset: 0x0028663D
		public FirewallGroupType GroupType
		{
			get
			{
				return this.firewallGroupType;
			}
			set
			{
				this.firewallGroupType = value;
			}
		}

		// Token: 0x170030F3 RID: 12531
		// (get) Token: 0x0600CB4D RID: 52045 RVA: 0x00288446 File Offset: 0x00286646
		public string GroupName
		{
			get
			{
				return this.customGroupName;
			}
		}

		// Token: 0x170030F4 RID: 12532
		// (get) Token: 0x0600CB4E RID: 52046 RVA: 0x0028844E File Offset: 0x0028664E
		public bool? IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x0600CB4F RID: 52047 RVA: 0x00288456 File Offset: 0x00286656
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FirewallRule);
		}

		// Token: 0x0600CB50 RID: 52048 RVA: 0x00288464 File Offset: 0x00286664
		public override int GetHashCode()
		{
			return this.resource.GetHashCode() ^ this.firewallGroupType.GetHashCode();
		}

		// Token: 0x0600CB51 RID: 52049 RVA: 0x00288483 File Offset: 0x00286683
		public bool Equals(FirewallRule other)
		{
			return other != null && this.firewallGroupType == other.firewallGroupType && this.resource.Equals(other.resource);
		}

		// Token: 0x0600CB52 RID: 52050 RVA: 0x002884B0 File Offset: 0x002866B0
		public bool Matches(Resource resource)
		{
			return this.Resource.Equals(resource);
		}

		// Token: 0x04006738 RID: 26424
		private Resource resource;

		// Token: 0x04006739 RID: 26425
		private FirewallGroupType firewallGroupType;

		// Token: 0x0400673A RID: 26426
		private readonly string customGroupName;

		// Token: 0x0400673B RID: 26427
		private readonly bool? isTrusted;
	}
}
