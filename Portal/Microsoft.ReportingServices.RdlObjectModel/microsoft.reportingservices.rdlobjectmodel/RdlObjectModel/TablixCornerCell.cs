using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001FB RID: 507
	public class TablixCornerCell : ReportObject
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00027948 File Offset: 0x00025B48
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x0002795B File Offset: 0x00025B5B
		public CellContents CellContents
		{
			get
			{
				return (CellContents)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0002796A File Offset: 0x00025B6A
		public TablixCornerCell()
		{
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00027972 File Offset: 0x00025B72
		internal TablixCornerCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003FF RID: 1023
		internal class Definition : DefinitionStore<TablixCornerCell, TablixCornerCell.Definition.Properties>
		{
			// Token: 0x060018C8 RID: 6344 RVA: 0x0003BC54 File Offset: 0x00039E54
			private Definition()
			{
			}

			// Token: 0x02000510 RID: 1296
			internal enum Properties
			{
				// Token: 0x04001107 RID: 4359
				CellContents
			}
		}
	}
}
