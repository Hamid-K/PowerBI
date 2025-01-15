using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000179 RID: 377
	public abstract class MapObjectCollectionBase<T> : ReportElementCollectionBase<T> where T : IMapObjectCollectionItem
	{
		// Token: 0x17000868 RID: 2152
		public override T this[int index]
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
				if (this.m_collection[index] == null)
				{
					this.m_collection[index] = this.CreateMapObject(index);
				}
				return this.m_collection[index];
			}
		}

		// Token: 0x06000FC8 RID: 4040
		protected abstract T CreateMapObject(int index);

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000441FC File Offset: 0x000423FC
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

		// Token: 0x0400074E RID: 1870
		private T[] m_collection;
	}
}
