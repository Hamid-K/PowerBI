using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000066 RID: 102
	internal sealed class CachedDsvItem<T> where T : DsvItem
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0000DCD3 File Offset: 0x0000BED3
		internal CachedDsvItem(CreatorGetter<DataSourceView> getDsv, CachedDsvItem<T>.DsvItemGetter getDsvItem)
		{
			if (getDsv == null || getDsvItem == null)
			{
				throw new InternalModelingException("getDsv and getDsvItem delegates can not be null.");
			}
			this.m_getDsv = getDsv;
			this.m_getDsvItem = getDsvItem;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		internal T GetItem()
		{
			T t = default(T);
			DataSourceView dataSourceView = this.m_getDsv();
			if (this.__cachedDsv != null && dataSourceView == this.__cachedDsv && this.__cachedDsvItem != null)
			{
				t = this.__cachedDsvItem;
			}
			else if (dataSourceView != null)
			{
				t = this.m_getDsvItem(dataSourceView);
				if (dataSourceView.IsCompiled)
				{
					this.__cachedDsv = dataSourceView;
					this.__cachedDsvItem = t;
				}
				else
				{
					this.__cachedDsv = null;
					this.__cachedDsvItem = default(T);
				}
			}
			else
			{
				this.__cachedDsv = null;
				this.__cachedDsvItem = default(T);
			}
			return t;
		}

		// Token: 0x04000241 RID: 577
		private readonly CreatorGetter<DataSourceView> m_getDsv;

		// Token: 0x04000242 RID: 578
		private readonly CachedDsvItem<T>.DsvItemGetter m_getDsvItem;

		// Token: 0x04000243 RID: 579
		private DataSourceView __cachedDsv;

		// Token: 0x04000244 RID: 580
		private T __cachedDsvItem;

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x06000E11 RID: 3601
		internal delegate T DsvItemGetter(DataSourceView dsv);
	}
}
