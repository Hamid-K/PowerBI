using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021F RID: 543
	public abstract class ChartObjectCollectionBase<T, U> : IEnumerable<T>, IEnumerable where T : ChartObjectCollectionItem<U> where U : BaseInstance
	{
		// Token: 0x17000AE1 RID: 2785
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.m_collection == null)
				{
					this.m_collection = new T[this.Count];
				}
				T t = this.m_collection[index];
				if (t == null)
				{
					this.m_collection[index] = this.CreateChartObject(index);
					t = this.m_collection[index];
				}
				return t;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001475 RID: 5237
		public abstract int Count { get; }

		// Token: 0x06001476 RID: 5238
		protected abstract T CreateChartObject(int index);

		// Token: 0x06001477 RID: 5239 RVA: 0x00053B3E File Offset: 0x00051D3E
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00053B4D File Offset: 0x00051D4D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00053B58 File Offset: 0x00051D58
		internal void SetNewContext()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					T t = this.m_collection[i];
					if (t != null)
					{
						t.SetNewContext();
					}
				}
			}
		}

		// Token: 0x040009AB RID: 2475
		private T[] m_collection;
	}
}
