using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200006B RID: 107
	internal sealed class KeyValuePairTypeInfo<K, V> : TraceLoggingTypeInfo<KeyValuePair<K, V>>
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000CB81 File Offset: 0x0000AD81
		public KeyValuePairTypeInfo(List<Type> recursionCheck)
		{
			this.keyInfo = TraceLoggingTypeInfo<K>.GetInstance(recursionCheck);
			this.valueInfo = TraceLoggingTypeInfo<V>.GetInstance(recursionCheck);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			this.keyInfo.WriteMetadata(traceLoggingMetadataCollector, "Key", EventFieldFormat.Default);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public override void WriteData(TraceLoggingDataCollector collector, ref KeyValuePair<K, V> value)
		{
			K key = value.Key;
			V value2 = value.Value;
			this.keyInfo.WriteData(collector, ref key);
			this.valueInfo.WriteData(collector, ref value2);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public override object GetData(object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)value;
			dictionary.Add("Key", this.keyInfo.GetData(keyValuePair.Key));
			dictionary.Add("Value", this.valueInfo.GetData(keyValuePair.Value));
			return dictionary;
		}

		// Token: 0x040000FF RID: 255
		private readonly TraceLoggingTypeInfo<K> keyInfo;

		// Token: 0x04000100 RID: 256
		private readonly TraceLoggingTypeInfo<V> valueInfo;
	}
}
