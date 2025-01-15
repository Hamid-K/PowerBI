using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage.Local
{
	// Token: 0x020020A5 RID: 8357
	public class LocalFirewallRule
	{
		// Token: 0x0600CCA8 RID: 52392 RVA: 0x0028AF41 File Offset: 0x00289141
		public LocalFirewallRule()
		{
			this.resource = new Resource();
		}

		// Token: 0x0600CCA9 RID: 52393 RVA: 0x0028AF54 File Offset: 0x00289154
		public LocalFirewallRule(FirewallRule firewallRule)
		{
			this.resource = firewallRule.Resource;
			this.firewallGroupType = firewallRule.GroupType;
		}

		// Token: 0x17003142 RID: 12610
		// (get) Token: 0x0600CCAA RID: 52394 RVA: 0x0028AF74 File Offset: 0x00289174
		// (set) Token: 0x0600CCAB RID: 52395 RVA: 0x0028AF81 File Offset: 0x00289181
		[XmlAttribute("Kind")]
		public string ResourceKind
		{
			get
			{
				return this.resource.Kind;
			}
			set
			{
				this.resource.Kind = value;
			}
		}

		// Token: 0x17003143 RID: 12611
		// (get) Token: 0x0600CCAC RID: 52396 RVA: 0x0028AF8F File Offset: 0x0028918F
		// (set) Token: 0x0600CCAD RID: 52397 RVA: 0x0028AF9C File Offset: 0x0028919C
		[XmlAttribute("Path")]
		public string ResourcePath
		{
			get
			{
				return this.resource.Path;
			}
			set
			{
				this.resource.Path = value;
			}
		}

		// Token: 0x17003144 RID: 12612
		// (get) Token: 0x0600CCAE RID: 52398 RVA: 0x0028AFAA File Offset: 0x002891AA
		// (set) Token: 0x0600CCAF RID: 52399 RVA: 0x0028AFB7 File Offset: 0x002891B7
		[XmlAttribute("NonNormalizedResourcePath")]
		public string NonNormalizedResourcePath
		{
			get
			{
				return this.resource.NonNormalizedPath;
			}
			set
			{
				this.resource.NonNormalizedPath = value;
			}
		}

		// Token: 0x17003145 RID: 12613
		// (get) Token: 0x0600CCB0 RID: 52400 RVA: 0x0028AFC5 File Offset: 0x002891C5
		// (set) Token: 0x0600CCB1 RID: 52401 RVA: 0x0028AFCD File Offset: 0x002891CD
		[XmlAttribute("Group")]
		public FirewallGroupType FirewallGroupType
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

		// Token: 0x0600CCB2 RID: 52402 RVA: 0x0028AFD6 File Offset: 0x002891D6
		public bool Matches(Resource resource)
		{
			return this.resource.Kind == resource.Kind && this.resource.Path == resource.Path;
		}

		// Token: 0x0600CCB3 RID: 52403 RVA: 0x0028B008 File Offset: 0x00289208
		public FirewallRule ToFirewallRule()
		{
			return new FirewallRule(this.resource, this.firewallGroupType, null, null);
		}

		// Token: 0x0400679D RID: 26525
		private Resource resource;

		// Token: 0x0400679E RID: 26526
		private FirewallGroupType firewallGroupType;
	}
}
