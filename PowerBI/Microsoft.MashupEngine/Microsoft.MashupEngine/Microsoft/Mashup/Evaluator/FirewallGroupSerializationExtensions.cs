using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CBF RID: 7359
	public static class FirewallGroupSerializationExtensions
	{
		// Token: 0x0600B75F RID: 46943 RVA: 0x002535B0 File Offset: 0x002517B0
		public static void WriteFirewallGroup(this BinaryWriter writer, FirewallGroup2 firewallGroup)
		{
			writer.WriteInt32((int)firewallGroup.GroupType);
			writer.WriteBool(firewallGroup.IsTrusted);
			writer.WriteBool(firewallGroup.Resource != null);
			if (firewallGroup.Resource != null)
			{
				writer.WriteIResource(firewallGroup.Resource);
			}
			writer.WriteBool(firewallGroup.GroupName != null);
			if (firewallGroup.GroupName != null)
			{
				writer.WriteString(firewallGroup.GroupName);
			}
		}

		// Token: 0x0600B760 RID: 46944 RVA: 0x0025361C File Offset: 0x0025181C
		public static FirewallGroup2 ReadFirewallGroup(this BinaryReader reader)
		{
			FirewallGroupType2 firewallGroupType = (FirewallGroupType2)reader.ReadInt32();
			bool flag = reader.ReadBool();
			IResource resource = null;
			if (reader.ReadBool())
			{
				resource = reader.ReadIResource();
			}
			string text = null;
			if (reader.ReadBool())
			{
				text = reader.ReadString();
			}
			return new FirewallGroup2(firewallGroupType, flag, resource, text);
		}
	}
}
