using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DE RID: 222
	internal sealed class ExistingBindingContext
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00026A4C File Offset: 0x00024C4C
		internal ExistingBindingContext()
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00026A5F File Offset: 0x00024C5F
		public ExistingTableBindingInfo GetBindingInfo(DsvTable dsvTable)
		{
			return this.GetBindingInfoCore<ExistingTableBindingInfo>(dsvTable);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00026A68 File Offset: 0x00024C68
		public ExistingColumnBindingInfo GetBindingInfo(DsvColumn dsvColumn)
		{
			return this.GetBindingInfoCore<ExistingColumnBindingInfo>(dsvColumn);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00026A71 File Offset: 0x00024C71
		public ExistingRelationBindingInfo GetBindingInfo(DsvRelation dsvRelation)
		{
			return this.GetBindingInfoCore<ExistingRelationBindingInfo>(dsvRelation);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00026A7C File Offset: 0x00024C7C
		private T GetBindingInfoCore<T>(DsvItem dsvItem) where T : ExistingBindingInfo, new()
		{
			if (dsvItem == null)
			{
				throw new InternalModelingException("dsvItem is null");
			}
			ExistingBindingInfo existingBindingInfo;
			if (!this.m_infos.TryGetValue(dsvItem, out existingBindingInfo))
			{
				existingBindingInfo = new T();
				this.m_infos.Add(dsvItem, existingBindingInfo);
			}
			return (T)((object)existingBindingInfo);
		}

		// Token: 0x040004D8 RID: 1240
		private readonly Dictionary<DsvItem, ExistingBindingInfo> m_infos = new Dictionary<DsvItem, ExistingBindingInfo>();
	}
}
