using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DE1 RID: 7649
	public class FirewallRule2 : IEquatable<FirewallRule2>
	{
		// Token: 0x0600BD82 RID: 48514 RVA: 0x00266ADB File Offset: 0x00264CDB
		public FirewallRule2(IResource resource, FirewallGroupType2 firewallGroupType, bool? isTrusted = null)
		{
			this.resource = resource;
			this.firewallGroupType = firewallGroupType;
			this.isTrusted = isTrusted;
		}

		// Token: 0x17002EA0 RID: 11936
		// (get) Token: 0x0600BD83 RID: 48515 RVA: 0x00266AF8 File Offset: 0x00264CF8
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17002EA1 RID: 11937
		// (get) Token: 0x0600BD84 RID: 48516 RVA: 0x00266B00 File Offset: 0x00264D00
		public FirewallGroupType2 GroupType
		{
			get
			{
				return this.firewallGroupType;
			}
		}

		// Token: 0x17002EA2 RID: 11938
		// (get) Token: 0x0600BD85 RID: 48517 RVA: 0x00266B08 File Offset: 0x00264D08
		public bool? IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x0600BD86 RID: 48518 RVA: 0x00266B10 File Offset: 0x00264D10
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FirewallRule2);
		}

		// Token: 0x0600BD87 RID: 48519 RVA: 0x00266B20 File Offset: 0x00264D20
		public override int GetHashCode()
		{
			return this.resource.GetHashCode() ^ this.firewallGroupType.GetHashCode() ^ this.isTrusted.GetHashCode();
		}

		// Token: 0x0600BD88 RID: 48520 RVA: 0x00266B64 File Offset: 0x00264D64
		public bool Equals(FirewallRule2 other)
		{
			return other != null && this.firewallGroupType == other.firewallGroupType && this.resource.Equals(other.resource) && this.isTrusted.Equals(other.isTrusted);
		}

		// Token: 0x040060A9 RID: 24745
		private readonly IResource resource;

		// Token: 0x040060AA RID: 24746
		private readonly FirewallGroupType2 firewallGroupType;

		// Token: 0x040060AB RID: 24747
		private readonly bool? isTrusted;
	}
}
