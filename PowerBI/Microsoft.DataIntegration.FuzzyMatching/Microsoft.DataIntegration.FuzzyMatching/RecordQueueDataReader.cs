using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000083 RID: 131
	internal class RecordQueueDataReader : SimpleDataReader
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x000186EF File Offset: 0x000168EF
		public RecordQueueDataReader(DataTable schema)
			: base(schema)
		{
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001870A File Offset: 0x0001690A
		public int Count
		{
			get
			{
				return this.m_queue.Count;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018717 File Offset: 0x00016917
		public void Add(object[] recordValues)
		{
			this.m_queue.Push(recordValues);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00018728 File Offset: 0x00016928
		public void Add(IDataRecord record)
		{
			object[] array = new object[record.FieldCount];
			record.GetValues(array);
			this.Add(array);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00018750 File Offset: 0x00016950
		public override bool Read()
		{
			if (this.m_beforeFirst)
			{
				this.m_beforeFirst = false;
			}
			else
			{
				this.m_queue.Pop();
			}
			if (this.m_queue.Count == 0)
			{
				this.m_beforeFirst = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00018786 File Offset: 0x00016986
		public override object GetValue(int i)
		{
			return this[i];
		}

		// Token: 0x17000122 RID: 290
		public override object this[int i]
		{
			get
			{
				return this.m_queue.Peek()[i];
			}
		}

		// Token: 0x040001D2 RID: 466
		private Stack<object[]> m_queue = new Stack<object[]>();

		// Token: 0x040001D3 RID: 467
		private bool m_beforeFirst = true;
	}
}
