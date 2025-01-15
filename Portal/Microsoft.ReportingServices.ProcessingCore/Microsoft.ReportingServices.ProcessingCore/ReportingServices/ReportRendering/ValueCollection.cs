using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200006D RID: 109
	public sealed class ValueCollection
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001AC39 File Offset: 0x00018E39
		internal ValueCollection()
		{
			this.m_values = new ArrayList();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001AC4C File Offset: 0x00018E4C
		internal ValueCollection(int capacity)
		{
			this.m_values = new ArrayList(capacity);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001AC60 File Offset: 0x00018E60
		internal ValueCollection(ArrayList values)
		{
			this.m_values = values;
		}

		// Token: 0x17000531 RID: 1329
		public object this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_values[index];
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001ACC7 File Offset: 0x00018EC7
		public int Count
		{
			get
			{
				return this.m_values.Count;
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		internal void Add(object value)
		{
			this.m_values.Add(value);
		}

		// Token: 0x040001F3 RID: 499
		private ArrayList m_values;
	}
}
