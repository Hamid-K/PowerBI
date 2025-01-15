using System;
using System.IO;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD4 RID: 7380
	internal static class IFirewallPlanSerializationExtensions
	{
		// Token: 0x0600B7C8 RID: 47048 RVA: 0x00254632 File Offset: 0x00252832
		public static void WriteIFirewallPlan(this BinaryWriter writer, IFirewallPlan firewallPlan)
		{
			((FirewallPlan)firewallPlan).Serialize(writer);
		}

		// Token: 0x0600B7C9 RID: 47049 RVA: 0x00254640 File Offset: 0x00252840
		public static IFirewallPlan ReadIFirewallPlan(this BinaryReader reader)
		{
			FirewallPlan firewallPlan = new FirewallPlan();
			firewallPlan.Deserialize(reader);
			return firewallPlan;
		}

		// Token: 0x0600B7CA RID: 47050 RVA: 0x0025464E File Offset: 0x0025284E
		public static void WriteFirewallPartitionPlan(this BinaryWriter writer, FirewallPartitionPlan partitionPlan)
		{
			partitionPlan.Serialize(writer);
		}

		// Token: 0x0600B7CB RID: 47051 RVA: 0x00254657 File Offset: 0x00252857
		public static FirewallPartitionPlan ReadFirewallPartitionPlan(this BinaryReader reader)
		{
			FirewallPartitionPlan firewallPartitionPlan = new FirewallPartitionPlan();
			firewallPartitionPlan.Deserialize(reader);
			return firewallPartitionPlan;
		}
	}
}
