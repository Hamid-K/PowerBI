using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200001D RID: 29
	internal class CustomData2005 : CustomData
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002D43 File Offset: 0x00000F43
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002D4B File Offset: 0x00000F4B
		public DataHierarchy DataColumnGroupings
		{
			get
			{
				return base.DataColumnHierarchy;
			}
			set
			{
				base.DataColumnHierarchy = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002D5C File Offset: 0x00000F5C
		public DataHierarchy DataRowGroupings
		{
			get
			{
				return base.DataRowHierarchy;
			}
			set
			{
				base.DataRowHierarchy = value;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002D65 File Offset: 0x00000F65
		public CustomData2005()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002D6D File Offset: 0x00000F6D
		public CustomData2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002D76 File Offset: 0x00000F76
		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
