using System;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001969 RID: 6505
	public class FirewallGroup : IEquatable<FirewallGroup>
	{
		// Token: 0x0600A51D RID: 42269 RVA: 0x000020FD File Offset: 0x000002FD
		public FirewallGroup()
		{
		}

		// Token: 0x0600A51E RID: 42270 RVA: 0x00222CAC File Offset: 0x00220EAC
		public FirewallGroup(FirewallGroupType firewallGroupType, bool isTrusted)
			: this(firewallGroupType, isTrusted, null, null)
		{
		}

		// Token: 0x0600A51F RID: 42271 RVA: 0x00222CB8 File Offset: 0x00220EB8
		public FirewallGroup(FirewallGroupType firewallGroupType, bool isTrusted, Resource resource, string groupName)
		{
			this.groupType = firewallGroupType;
			this.isTrusted = isTrusted;
			this.groupName = groupName;
			if (this.HasResource)
			{
				if (resource == null)
				{
					throw new InvalidOperationException();
				}
				this.resource = resource;
			}
		}

		// Token: 0x17002A28 RID: 10792
		// (get) Token: 0x0600A520 RID: 42272 RVA: 0x00222CEE File Offset: 0x00220EEE
		public bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x17002A29 RID: 10793
		// (get) Token: 0x0600A521 RID: 42273 RVA: 0x00222CF6 File Offset: 0x00220EF6
		private bool HasResource
		{
			get
			{
				return this.groupType == FirewallGroupType.SeparatePrivate || this.groupType == FirewallGroupType.SingleUnclassified;
			}
		}

		// Token: 0x17002A2A RID: 10794
		// (get) Token: 0x0600A522 RID: 42274 RVA: 0x00222D0C File Offset: 0x00220F0C
		public FirewallGroupType GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x17002A2B RID: 10795
		// (get) Token: 0x0600A523 RID: 42275 RVA: 0x00222D14 File Offset: 0x00220F14
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x17002A2C RID: 10796
		// (get) Token: 0x0600A524 RID: 42276 RVA: 0x00222D1C File Offset: 0x00220F1C
		public Resource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x0600A525 RID: 42277 RVA: 0x00222D24 File Offset: 0x00220F24
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FirewallGroup);
		}

		// Token: 0x0600A526 RID: 42278 RVA: 0x00222D34 File Offset: 0x00220F34
		public override int GetHashCode()
		{
			int num = this.groupType.GetHashCode();
			num ^= this.isTrusted.GetHashCode();
			if (this.HasResource)
			{
				num ^= this.resource.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600A527 RID: 42279 RVA: 0x00222D80 File Offset: 0x00220F80
		public bool Equals(FirewallGroup other)
		{
			return other != null && this.groupType == other.groupType && this.isTrusted == other.isTrusted && (!this.HasResource || this.resource.Equals(other.resource));
		}

		// Token: 0x0600A528 RID: 42280 RVA: 0x00222DD0 File Offset: 0x00220FD0
		public bool IsSameAs(FirewallGroup group)
		{
			return this.groupType == group.groupType && this.groupType != FirewallGroupType.MultipleUnclassified && (!this.HasResource || this.resource.Equals(group.resource));
		}

		// Token: 0x0600A529 RID: 42281 RVA: 0x00222E0C File Offset: 0x0022100C
		public static FirewallGroup Create(FirewallRuleManager firewallRuleManager, Resource resource)
		{
			FirewallGroupInfo firewallGroupInfo = FirewallGroupInfo.GetFirewallGroupInfo(resource);
			bool? flag = null;
			FirewallGroupType firewallGroupType;
			string text;
			if (!firewallGroupInfo.IsFixed)
			{
				if (firewallRuleManager.TryGetGroupType(resource, out firewallGroupType, out text, out flag))
				{
					if (!firewallGroupInfo.GroupTypes.Contains(firewallGroupType))
					{
						text = null;
						firewallGroupType = FirewallGroupType.SingleUnclassified;
					}
				}
				else
				{
					text = null;
					firewallGroupType = FirewallGroupType.SingleUnclassified;
				}
			}
			else
			{
				text = null;
				firewallGroupType = firewallGroupInfo.DefaultGroupType;
			}
			return new FirewallGroup(firewallGroupType, flag ?? firewallGroupInfo.IsTrusted, resource, text);
		}

		// Token: 0x0600A52A RID: 42282 RVA: 0x00222E88 File Offset: 0x00221088
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.groupType);
			stringBuilder.Append('/');
			stringBuilder.Append(this.isTrusted ? "Trusted" : "Untrusted");
			if (this.HasResource)
			{
				stringBuilder.Append('/');
				stringBuilder.Append(this.resource);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040055FA RID: 22010
		private readonly FirewallGroupType groupType;

		// Token: 0x040055FB RID: 22011
		private readonly Resource resource;

		// Token: 0x040055FC RID: 22012
		private readonly bool isTrusted;

		// Token: 0x040055FD RID: 22013
		private readonly string groupName;

		// Token: 0x040055FE RID: 22014
		public static readonly FirewallGroup None = new FirewallGroup(FirewallGroupType.None, true);
	}
}
