using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200062E RID: 1582
	internal sealed class SerializableValues : Dictionary<string, object>, ISerializableValues, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x060056EC RID: 22252 RVA: 0x0016EAE4 File Offset: 0x0016CCE4
		internal void AddProcessingMessages(string key, ProcessingMessageList messages)
		{
			if (messages == null || messages.Count == 0)
			{
				return;
			}
			string[] array = new string[messages.Count];
			for (int i = 0; i < messages.Count; i++)
			{
				array[i] = messages[i].FormatMessage();
			}
			base[key] = array;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x0016EB34 File Offset: 0x0016CD34
		internal void AddRange(ISerializableValues values)
		{
			if (values == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
