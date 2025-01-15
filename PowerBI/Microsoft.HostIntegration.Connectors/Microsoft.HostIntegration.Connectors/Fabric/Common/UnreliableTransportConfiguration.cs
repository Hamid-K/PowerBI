using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200042E RID: 1070
	internal static class UnreliableTransportConfiguration
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x000720AC File Offset: 0x000702AC
		private static List<UnreliableTransportSpec> LoadSpec()
		{
			List<UnreliableTransportSpec> list = new List<UnreliableTransportSpec>();
			ArrayList arrayList = (ArrayList)ConfigFile.Config.GetValue("unreliableTransport/blockList");
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					Dictionary<string, string> dictionary = (Dictionary<string, string>)obj;
					string text = dictionary["channelPattern"];
					string text2 = dictionary["actionPattern"];
					int num = int.Parse(dictionary["priority"], CultureInfo.InvariantCulture);
					double num2 = double.Parse(dictionary["faultRatio"], CultureInfo.InvariantCulture);
					int num3 = int.Parse(dictionary["ratio"], CultureInfo.InvariantCulture);
					int num4 = int.Parse(dictionary["low"], CultureInfo.InvariantCulture);
					int num5 = int.Parse(dictionary["high"], CultureInfo.InvariantCulture);
					UnreliableTransportSpec unreliableTransportSpec = new UnreliableTransportSpec(text, text2, num, num2, num3, num4, num5);
					list.Add(unreliableTransportSpec);
				}
			}
			return list;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000721D0 File Offset: 0x000703D0
		public static void Add(UnreliableTransportSpec spec)
		{
			UnreliableTransportConfiguration.s_specs = new List<UnreliableTransportSpec>(UnreliableTransportConfiguration.s_specs) { spec };
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000721F8 File Offset: 0x000703F8
		public static void Remove(string channelPattern, string actionPattern)
		{
			List<UnreliableTransportSpec> list = new List<UnreliableTransportSpec>();
			if (!string.IsNullOrEmpty(channelPattern) || !string.IsNullOrEmpty(actionPattern))
			{
				foreach (UnreliableTransportSpec unreliableTransportSpec in UnreliableTransportConfiguration.s_specs)
				{
					if (unreliableTransportSpec.ActionPattern != actionPattern || unreliableTransportSpec.ChannelPattern != channelPattern)
					{
						list.Add(unreliableTransportSpec);
					}
				}
			}
			UnreliableTransportConfiguration.s_specs = list;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00072284 File Offset: 0x00070484
		public static TimeSpan GetDelay(string id, Message msg)
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			int num = 0;
			foreach (UnreliableTransportSpec unreliableTransportSpec in UnreliableTransportConfiguration.s_specs)
			{
				int num2 = unreliableTransportSpec.Match(id, msg);
				if (num2 > num)
				{
					num = num2;
					timeSpan = unreliableTransportSpec.GetDelay();
				}
			}
			return timeSpan;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000722F0 File Offset: 0x000704F0
		public static List<string> GetBlockList()
		{
			List<string> list = new List<string>();
			foreach (UnreliableTransportSpec unreliableTransportSpec in UnreliableTransportConfiguration.s_specs)
			{
				if (unreliableTransportSpec.IsBlocked && unreliableTransportSpec.ChannelPattern != null)
				{
					list.Add(unreliableTransportSpec.ChannelPattern);
				}
			}
			return list;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00072360 File Offset: 0x00070560
		public static string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (UnreliableTransportSpec unreliableTransportSpec in UnreliableTransportConfiguration.s_specs)
			{
				stringBuilder.AppendLine(unreliableTransportSpec.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040016A5 RID: 5797
		private static List<UnreliableTransportSpec> s_specs = UnreliableTransportConfiguration.LoadSpec();
	}
}
