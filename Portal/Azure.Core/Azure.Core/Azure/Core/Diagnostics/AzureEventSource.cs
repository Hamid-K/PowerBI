using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace Azure.Core.Diagnostics
{
	// Token: 0x020000CA RID: 202
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class AzureEventSource : EventSource
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x00016F54 File Offset: 0x00015154
		static AzureEventSource()
		{
			HashSet<string> hashSet = AppDomain.CurrentDomain.GetData("_AzureEventSourceNamesInUse") as HashSet<string>;
			if (hashSet == null)
			{
				hashSet = new HashSet<string>();
				AppDomain.CurrentDomain.SetData("_AzureEventSourceNamesInUse", hashSet);
			}
			AzureEventSource.NamesInUse = hashSet;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00016FB0 File Offset: 0x000151B0
		protected AzureEventSource(string eventSourceName)
			: base(AzureEventSource.DeduplicateName(eventSourceName), EventSourceSettings.Default, AzureEventSource.MainEventSourceTraits)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00016FC4 File Offset: 0x000151C4
		private static string DeduplicateName(string eventSourceName)
		{
			try
			{
				HashSet<string> namesInUse = AzureEventSource.NamesInUse;
				lock (namesInUse)
				{
					foreach (EventSource eventSource in EventSource.GetSources())
					{
						AzureEventSource.NamesInUse.Add(eventSource.Name);
					}
					if (!AzureEventSource.NamesInUse.Contains(eventSourceName))
					{
						AzureEventSource.NamesInUse.Add(eventSourceName);
						return eventSourceName;
					}
					int num = 1;
					string text;
					for (;;)
					{
						text = string.Format("{0}-{1}", eventSourceName, num);
						if (!AzureEventSource.NamesInUse.Contains(text))
						{
							break;
						}
						num++;
					}
					AzureEventSource.NamesInUse.Add(text);
					return text;
				}
			}
			catch (NotImplementedException)
			{
			}
			return eventSourceName;
		}

		// Token: 0x040002A8 RID: 680
		private const string SharedDataKey = "_AzureEventSourceNamesInUse";

		// Token: 0x040002A9 RID: 681
		private static readonly HashSet<string> NamesInUse;

		// Token: 0x040002AA RID: 682
		private static readonly string[] MainEventSourceTraits = new string[] { "AzureEventSource", "true" };
	}
}
