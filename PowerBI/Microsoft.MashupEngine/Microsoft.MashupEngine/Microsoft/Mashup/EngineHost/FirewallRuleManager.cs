using System;
using System.Collections.Generic;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200196B RID: 6507
	public class FirewallRuleManager
	{
		// Token: 0x0600A534 RID: 42292 RVA: 0x00223010 File Offset: 0x00221210
		public FirewallRuleManager(FirewallRule[] firewallRules)
		{
			this.firewallRules = new Dictionary<Resource, FirewallRuleManager.FirewallGroupEntry>();
			foreach (FirewallRule firewallRule in firewallRules)
			{
				this.firewallRules.Add(firewallRule.Resource, new FirewallRuleManager.FirewallGroupEntry
				{
					Type = firewallRule.GroupType,
					Name = firewallRule.GroupName,
					IsTrusted = firewallRule.IsTrusted
				});
			}
		}

		// Token: 0x0600A535 RID: 42293 RVA: 0x00223084 File Offset: 0x00221284
		public IEnumerable<Resource> GetRequiredResources(IEnumerable<Resource> resources)
		{
			List<Resource> list = new List<Resource>();
			foreach (Resource resource in resources)
			{
				if (!FirewallGroupInfo.GetFirewallGroupInfo(resource).IsFixed && !this.Matches(resource))
				{
					list.Add(resource);
				}
			}
			return list;
		}

		// Token: 0x0600A536 RID: 42294 RVA: 0x002230EC File Offset: 0x002212EC
		public bool Contains(Resource resource)
		{
			return this.firewallRules.ContainsKey(resource);
		}

		// Token: 0x0600A537 RID: 42295 RVA: 0x002230FC File Offset: 0x002212FC
		public bool TryGetGroupType(Resource resource, out FirewallGroupType groupType, out string groupName, out bool? isTrusted)
		{
			Resource resource2;
			FirewallRuleManager.FirewallGroupEntry firewallGroupEntry;
			if (this.TryMatchResource(resource, out resource2) && this.firewallRules.TryGetValue(resource2, out firewallGroupEntry))
			{
				groupType = firewallGroupEntry.Type;
				groupName = firewallGroupEntry.Name;
				isTrusted = firewallGroupEntry.IsTrusted;
				return true;
			}
			groupType = FirewallGroupType.None;
			groupName = null;
			isTrusted = null;
			return false;
		}

		// Token: 0x0600A538 RID: 42296 RVA: 0x00223154 File Offset: 0x00221354
		private bool Matches(Resource resource)
		{
			Resource resource2;
			return this.TryMatchResource(resource, out resource2);
		}

		// Token: 0x0600A539 RID: 42297 RVA: 0x0022316C File Offset: 0x0022136C
		private bool TryMatchResource(Resource resource, out Resource bestMatch)
		{
			bestMatch = null;
			foreach (Resource resource2 in this.firewallRules.Keys)
			{
				if (ResourcePath.StartsWith(resource2, resource) && (bestMatch == null || ResourcePath.Length(resource2.Path) > ResourcePath.Length(bestMatch.Path)))
				{
					bestMatch = resource2;
				}
			}
			return bestMatch != null;
		}

		// Token: 0x04005604 RID: 22020
		private readonly Dictionary<Resource, FirewallRuleManager.FirewallGroupEntry> firewallRules;

		// Token: 0x0200196C RID: 6508
		private struct FirewallGroupEntry
		{
			// Token: 0x04005605 RID: 22021
			public FirewallGroupType Type;

			// Token: 0x04005606 RID: 22022
			public string Name;

			// Token: 0x04005607 RID: 22023
			public bool? IsTrusted;
		}
	}
}
