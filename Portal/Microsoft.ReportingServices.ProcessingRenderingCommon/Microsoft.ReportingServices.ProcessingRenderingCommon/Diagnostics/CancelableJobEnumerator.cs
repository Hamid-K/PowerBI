using System;
using System.Collections;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000063 RID: 99
	internal sealed class CancelableJobEnumerator : IEnumerator
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000A966 File Offset: 0x00008B66
		public CancelableJobEnumerator(IEnumerator innerEnumerator)
		{
			this.m_innerEnumerator = innerEnumerator;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000A978 File Offset: 0x00008B78
		object IEnumerator.Current
		{
			get
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)this.m_innerEnumerator.Current;
				return (RunningJobContext)dictionaryEntry.Value;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		public bool MoveNext()
		{
			return this.m_innerEnumerator.MoveNext();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A9AF File Offset: 0x00008BAF
		public void Reset()
		{
			this.m_innerEnumerator.Reset();
		}

		// Token: 0x04000167 RID: 359
		private IEnumerator m_innerEnumerator;
	}
}
