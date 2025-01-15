using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FA RID: 1530
	internal class ModifiableIteratorCollection<TElement> : InternalBase
	{
		// Token: 0x06004ADA RID: 19162 RVA: 0x00109596 File Offset: 0x00107796
		internal ModifiableIteratorCollection(IEnumerable<TElement> elements)
		{
			this.m_elements = new List<TElement>(elements);
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004ADB RID: 19163 RVA: 0x001095B1 File Offset: 0x001077B1
		internal bool IsEmpty
		{
			get
			{
				return this.m_elements.Count == 0;
			}
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x001095C1 File Offset: 0x001077C1
		internal TElement RemoveOneElement()
		{
			return this.Remove(this.m_elements.Count - 1);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001095D6 File Offset: 0x001077D6
		internal void ResetIterator()
		{
			this.m_currentIteratorIndex = -1;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x001095DF File Offset: 0x001077DF
		internal void RemoveCurrentOfIterator()
		{
			this.Remove(this.m_currentIteratorIndex);
			this.m_currentIteratorIndex--;
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x001095FC File Offset: 0x001077FC
		internal IEnumerable<TElement> Elements()
		{
			this.m_currentIteratorIndex = 0;
			while (this.m_currentIteratorIndex < this.m_elements.Count)
			{
				yield return this.m_elements[this.m_currentIteratorIndex];
				this.m_currentIteratorIndex++;
			}
			yield break;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x0010960C File Offset: 0x0010780C
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedString(builder, this.m_elements);
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0010961C File Offset: 0x0010781C
		private TElement Remove(int index)
		{
			TElement telement = this.m_elements[index];
			int num = this.m_elements.Count - 1;
			this.m_elements[index] = this.m_elements[num];
			this.m_elements.RemoveAt(num);
			return telement;
		}

		// Token: 0x04001A3F RID: 6719
		private readonly List<TElement> m_elements;

		// Token: 0x04001A40 RID: 6720
		private int m_currentIteratorIndex;
	}
}
