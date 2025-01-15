using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CCB RID: 7371
	internal static class FirewallStrings
	{
		// Token: 0x0600B7B3 RID: 47027 RVA: 0x00254576 File Offset: 0x00252776
		public static string FirewallFlow_CantCreateGroup(string partitionKey)
		{
			return Strings.FirewallFlow_CantCreateGroup(partitionKey);
		}

		// Token: 0x0600B7B4 RID: 47028 RVA: 0x0025457E File Offset: 0x0025277E
		public static string FirewallFlow_IllegalReference(string from, string to)
		{
			return Strings.FirewallFlow_IllegalReference(from, to);
		}

		// Token: 0x0600B7B5 RID: 47029 RVA: 0x00254587 File Offset: 0x00252787
		public static string FirewallFlow_NoGroupsAllowed(string partitionKey)
		{
			return Strings.FirewallFlow_NoGroupsAllowed(partitionKey);
		}

		// Token: 0x0600B7B6 RID: 47030 RVA: 0x0025458F File Offset: 0x0025278F
		public static string FirewallFlow_DeviationFromGroup(string partitionKey, FirewallGroup2 firewallGroup, FirewallGroup2 newGroup)
		{
			return Strings.FirewallFlow_DeviationFromGroup(partitionKey, firewallGroup.ToString(), newGroup.ToString());
		}
	}
}
