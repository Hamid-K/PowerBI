using System;
using System.Text;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DDE RID: 7646
	public class FirewallGroup2 : IEquatable<FirewallGroup2>
	{
		// Token: 0x0600BD6E RID: 48494 RVA: 0x000020FD File Offset: 0x000002FD
		public FirewallGroup2()
		{
		}

		// Token: 0x0600BD6F RID: 48495 RVA: 0x00266859 File Offset: 0x00264A59
		public FirewallGroup2(FirewallGroupType2 firewallGroupType, bool isTrusted)
			: this(firewallGroupType, isTrusted, null, null)
		{
		}

		// Token: 0x0600BD70 RID: 48496 RVA: 0x00266865 File Offset: 0x00264A65
		public FirewallGroup2(FirewallGroupType2 firewallGroupType, bool isTrusted, IResource resource)
			: this(firewallGroupType, isTrusted, resource, null)
		{
		}

		// Token: 0x0600BD71 RID: 48497 RVA: 0x00266871 File Offset: 0x00264A71
		public FirewallGroup2(FirewallGroupType2 firewallGroupType, bool isTrusted, IResource resource, string groupName)
		{
			this.groupType = firewallGroupType;
			this.isTrusted = isTrusted;
			if (this.HasResource)
			{
				if (resource == null)
				{
					throw new InvalidOperationException();
				}
				this.resource = resource;
			}
			this.groupName = groupName;
		}

		// Token: 0x17002E97 RID: 11927
		// (get) Token: 0x0600BD72 RID: 48498 RVA: 0x002668A7 File Offset: 0x00264AA7
		public bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x17002E98 RID: 11928
		// (get) Token: 0x0600BD73 RID: 48499 RVA: 0x002668AF File Offset: 0x00264AAF
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x17002E99 RID: 11929
		// (get) Token: 0x0600BD74 RID: 48500 RVA: 0x002668B7 File Offset: 0x00264AB7
		public FirewallGroupType2 GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x17002E9A RID: 11930
		// (get) Token: 0x0600BD75 RID: 48501 RVA: 0x002668BF File Offset: 0x00264ABF
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17002E9B RID: 11931
		// (get) Token: 0x0600BD76 RID: 48502 RVA: 0x002668C7 File Offset: 0x00264AC7
		private bool HasResource
		{
			get
			{
				return this.groupType == FirewallGroupType2.SeparatePrivate || this.groupType == FirewallGroupType2.SingleUnclassified;
			}
		}

		// Token: 0x0600BD77 RID: 48503 RVA: 0x002668DD File Offset: 0x00264ADD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FirewallGroup2);
		}

		// Token: 0x0600BD78 RID: 48504 RVA: 0x002668EC File Offset: 0x00264AEC
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

		// Token: 0x0600BD79 RID: 48505 RVA: 0x00266938 File Offset: 0x00264B38
		public bool Equals(FirewallGroup2 other)
		{
			return other != null && this.groupType == other.groupType && !(this.groupName != other.groupName) && this.isTrusted == other.isTrusted && (!this.HasResource || this.resource.Equals(other.resource));
		}

		// Token: 0x0600BD7A RID: 48506 RVA: 0x002669A0 File Offset: 0x00264BA0
		public bool IsSameAs(FirewallGroup2 group)
		{
			return this.groupType == group.groupType && this.groupType != FirewallGroupType2.MultipleUnclassified && !(this.groupName != group.groupName) && (!this.HasResource || this.resource.Equals(group.resource));
		}

		// Token: 0x0600BD7B RID: 48507 RVA: 0x002669FC File Offset: 0x00264BFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.groupType);
			if (this.groupName != null)
			{
				stringBuilder.Append('(');
				stringBuilder.Append(this.groupName);
				stringBuilder.Append(')');
			}
			stringBuilder.Append('/');
			stringBuilder.Append(this.isTrusted ? "Trusted" : "Untrusted");
			if (this.HasResource)
			{
				stringBuilder.Append('/');
				stringBuilder.Append(this.resource);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04006099 RID: 24729
		private readonly FirewallGroupType2 groupType;

		// Token: 0x0400609A RID: 24730
		private readonly IResource resource;

		// Token: 0x0400609B RID: 24731
		private readonly string groupName;

		// Token: 0x0400609C RID: 24732
		private readonly bool isTrusted;

		// Token: 0x0400609D RID: 24733
		public static readonly FirewallGroup2 None = new FirewallGroup2(FirewallGroupType2.None, true);
	}
}
