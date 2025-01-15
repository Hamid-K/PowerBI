using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000055 RID: 85
	public class EnumeratorToDataReaderAdapter<T> : SimpleDataReader
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00014EEA File Offset: 0x000130EA
		public EnumeratorToDataReaderAdapter()
			: this(new ObjectToDataRecordAdapter<T>())
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00014EF7 File Offset: 0x000130F7
		public EnumeratorToDataReaderAdapter(IEnumerator<T> items)
			: this(new ObjectToDataRecordAdapter<T>())
		{
			this.Reset(items);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00014F0B File Offset: 0x0001310B
		public EnumeratorToDataReaderAdapter(IDataRecordProvider<T> recordProvider)
			: base(recordProvider.GetSchemaTable())
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00014F19 File Offset: 0x00013119
		public EnumeratorToDataReaderAdapter(IDataRecordProvider<T> recordProvider, IEnumerator<T> items)
			: base(recordProvider.GetSchemaTable())
		{
			this.Reset(items);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00014F2E File Offset: 0x0001312E
		public void Reset(IEnumerator<T> items)
		{
			this.m_enumerator = items;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00014F37 File Offset: 0x00013137
		protected override IDataRecord Current
		{
			get
			{
				return this.m_recordProvider.GetRecord(this.m_enumerator.Current);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014F4F File Offset: 0x0001314F
		public override bool Read()
		{
			return this.m_enumerator.MoveNext();
		}

		// Token: 0x04000075 RID: 117
		private IDataRecordProvider<T> m_recordProvider;

		// Token: 0x04000076 RID: 118
		private IEnumerator<T> m_enumerator;
	}
}
