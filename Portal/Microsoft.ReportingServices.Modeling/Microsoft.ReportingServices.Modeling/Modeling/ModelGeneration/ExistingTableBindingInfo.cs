using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DB RID: 219
	internal sealed class ExistingTableBindingInfo : ExistingBindingInfo
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000269C5 File Offset: 0x00024BC5
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000269CD File Offset: 0x00024BCD
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

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000269D6 File Offset: 0x00024BD6
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x000269DE File Offset: 0x00024BDE
		public ModelAttribute CountAttribute
		{
			get
			{
				return this.m_countAttribute;
			}
			set
			{
				this.m_countAttribute = value;
			}
		}

		// Token: 0x040004D1 RID: 1233
		private ModelEntity m_entity;

		// Token: 0x040004D2 RID: 1234
		private ModelAttribute m_countAttribute;
	}
}
