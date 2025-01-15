using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001FE RID: 510
	public class TablixHeader : ReportObject
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00027BD9 File Offset: 0x00025DD9
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00027BE7 File Offset: 0x00025DE7
		public ReportSize Size
		{
			get
			{
				return base.PropertyStore.GetSize(0);
			}
			set
			{
				base.PropertyStore.SetSize(0, value);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00027BF6 File Offset: 0x00025DF6
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00027C09 File Offset: 0x00025E09
		public CellContents CellContents
		{
			get
			{
				return (CellContents)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00027C18 File Offset: 0x00025E18
		public TablixHeader()
		{
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00027C20 File Offset: 0x00025E20
		internal TablixHeader(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00027C29 File Offset: 0x00025E29
		public override void Initialize()
		{
			base.Initialize();
			this.Size = Constants.DefaultZeroSize;
			this.CellContents = new CellContents();
		}

		// Token: 0x02000404 RID: 1028
		internal class Definition : DefinitionStore<TablixHeader, TablixHeader.Definition.Properties>
		{
			// Token: 0x060018DD RID: 6365 RVA: 0x0003BF5B File Offset: 0x0003A15B
			private Definition()
			{
			}

			// Token: 0x02000513 RID: 1299
			internal enum Properties
			{
				// Token: 0x04001119 RID: 4377
				Size,
				// Token: 0x0400111A RID: 4378
				CellContents
			}
		}
	}
}
