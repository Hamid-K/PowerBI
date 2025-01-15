using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage.Local
{
	// Token: 0x020020A4 RID: 8356
	[XmlRoot("LocalFirewall")]
	public class LocalFirewall : XmlRoot
	{
		// Token: 0x0600CCA4 RID: 52388 RVA: 0x0028AECB File Offset: 0x002890CB
		public LocalFirewall()
		{
			this.rules = new List<LocalFirewallRule>();
		}

		// Token: 0x0600CCA5 RID: 52389 RVA: 0x0028AEDE File Offset: 0x002890DE
		public LocalFirewall(List<LocalFirewallRule> rules)
		{
			this.rules = rules;
		}

		// Token: 0x17003141 RID: 12609
		// (get) Token: 0x0600CCA6 RID: 52390 RVA: 0x0028AEED File Offset: 0x002890ED
		[XmlArray("Rules")]
		[XmlArrayItem("Rule")]
		public List<LocalFirewallRule> Rules
		{
			get
			{
				return this.rules;
			}
		}

		// Token: 0x0600CCA7 RID: 52391 RVA: 0x0028AEF8 File Offset: 0x002890F8
		public FirewallRule[] GetFirewallRules()
		{
			FirewallRule[] array = new FirewallRule[this.rules.Count];
			for (int i = 0; i < this.rules.Count; i++)
			{
				array[i] = this.rules[i].ToFirewallRule();
			}
			return array;
		}

		// Token: 0x0400679C RID: 26524
		private List<LocalFirewallRule> rules;
	}
}
