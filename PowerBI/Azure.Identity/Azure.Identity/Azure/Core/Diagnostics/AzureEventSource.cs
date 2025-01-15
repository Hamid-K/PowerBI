using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace Azure.Core.Diagnostics
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class AzureEventSource : EventSource
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003BD0 File Offset: 0x00001DD0
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

		// Token: 0x06000092 RID: 146 RVA: 0x00003C2C File Offset: 0x00001E2C
		protected AzureEventSource(string eventSourceName)
			: base(AzureEventSource.DeduplicateName(eventSourceName), EventSourceSettings.Default, AzureEventSource.MainEventSourceTraits)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003C40 File Offset: 0x00001E40
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

		// Token: 0x04000052 RID: 82
		private const string SharedDataKey = "_AzureEventSourceNamesInUse";

		// Token: 0x04000053 RID: 83
		private static readonly HashSet<string> NamesInUse;

		// Token: 0x04000054 RID: 84
		private static readonly string[] MainEventSourceTraits = new string[] { "AzureEventSource", "true" };
	}
}
