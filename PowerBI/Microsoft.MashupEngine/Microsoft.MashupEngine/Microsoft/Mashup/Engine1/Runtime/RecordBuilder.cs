using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015E4 RID: 5604
	internal struct RecordBuilder
	{
		// Token: 0x06008CD6 RID: 36054 RVA: 0x001D84AF File Offset: 0x001D66AF
		public RecordBuilder(int capacity)
		{
			this.keys = new KeysBuilder(capacity);
			this.values = new List<IValueReference>(capacity);
			this.types = null;
		}

		// Token: 0x06008CD7 RID: 36055 RVA: 0x001D84D0 File Offset: 0x001D66D0
		public void Add(string key, IValueReference value, TypeValue type)
		{
			this.keys.Add(key);
			this.values.Add(value);
			if (!type.Equals(TypeValue.Any))
			{
				if (this.types == null)
				{
					this.types = new List<KeyValuePair<int, TypeValue>>();
				}
				this.types.Add(new KeyValuePair<int, TypeValue>(this.keys.Count - 1, type));
			}
		}

		// Token: 0x06008CD8 RID: 36056 RVA: 0x001D8533 File Offset: 0x001D6733
		public void Add(RecordKeyDefinition recordKeyDefinition)
		{
			this.Add(recordKeyDefinition.Key, recordKeyDefinition.Value, recordKeyDefinition.Type);
		}

		// Token: 0x06008CD9 RID: 36057 RVA: 0x001D8550 File Offset: 0x001D6750
		public void Add(IEnumerable<RecordKeyDefinition> recordKeyDefinitions)
		{
			foreach (RecordKeyDefinition recordKeyDefinition in recordKeyDefinitions)
			{
				this.Add(recordKeyDefinition);
			}
		}

		// Token: 0x06008CDA RID: 36058 RVA: 0x001D8598 File Offset: 0x001D6798
		public RecordValue ToRecord()
		{
			if (this.types == null)
			{
				return RecordValue.New(this.keys.ToKeys(), this.values.ToArray());
			}
			Value[] array = new Value[this.keys.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RecordTypeAlgebra.DefaultFieldRecord;
			}
			foreach (KeyValuePair<int, TypeValue> keyValuePair in this.types)
			{
				array[keyValuePair.Key] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					keyValuePair.Value,
					LogicalValue.False
				});
			}
			return RecordValue.New(RecordTypeValue.New(RecordValue.New(this.keys.ToKeys(), array)), this.values.ToArray());
		}

		// Token: 0x06008CDB RID: 36059 RVA: 0x001D8680 File Offset: 0x001D6880
		public static RecordValue ToRecord(IList<RecordKeyDefinition> recordKeyDefinitions)
		{
			RecordBuilder recordBuilder = new RecordBuilder(recordKeyDefinitions.Count);
			recordBuilder.Add(recordKeyDefinitions);
			return recordBuilder.ToRecord();
		}

		// Token: 0x04004CD0 RID: 19664
		private KeysBuilder keys;

		// Token: 0x04004CD1 RID: 19665
		private List<IValueReference> values;

		// Token: 0x04004CD2 RID: 19666
		private List<KeyValuePair<int, TypeValue>> types;
	}
}
