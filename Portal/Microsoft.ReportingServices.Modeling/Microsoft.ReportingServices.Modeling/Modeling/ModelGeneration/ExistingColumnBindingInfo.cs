using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DC RID: 220
	internal sealed class ExistingColumnBindingInfo : ExistingBindingInfo
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000269EF File Offset: 0x00024BEF
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x000269F7 File Offset: 0x00024BF7
		public ModelAttribute Attribute
		{
			get
			{
				return this.m_attribute;
			}
			set
			{
				this.m_attribute = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00026A00 File Offset: 0x00024C00
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x00026A08 File Offset: 0x00024C08
		public ModelEntity Entity
		{
			get
			{
				return this.m_entity;
			}
			set
			{
				this.m_entity = value;
			}
		}

		// Token: 0x040004D3 RID: 1235
		private ModelAttribute m_attribute;

		// Token: 0x040004D4 RID: 1236
		private ModelEntity m_entity;
	}
}
