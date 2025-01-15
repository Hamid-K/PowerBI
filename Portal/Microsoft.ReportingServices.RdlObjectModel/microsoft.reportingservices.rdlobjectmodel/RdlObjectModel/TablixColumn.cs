using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000200 RID: 512
	public class TablixColumn : ReportObject
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00027CF8 File Offset: 0x00025EF8
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x00027D06 File Offset: 0x00025F06
		public ReportSize Width
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

		// Token: 0x06001142 RID: 4418 RVA: 0x00027D15 File Offset: 0x00025F15
		public TablixColumn()
		{
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00027D1D File Offset: 0x00025F1D
		internal TablixColumn(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00027D26 File Offset: 0x00025F26
		public override void Initialize()
		{
			base.Initialize();
			this.Width = Constants.DefaultZeroSize;
		}

		// Token: 0x02000406 RID: 1030
		internal class Definition : DefinitionStore<TablixColumn, TablixColumn.Definition.Properties>
		{
			// Token: 0x060018DF RID: 6367 RVA: 0x0003BF6B File Offset: 0x0003A16B
			private Definition()
			{
			}

			// Token: 0x02000515 RID: 1301
			internal enum Properties
			{
				// Token: 0x0400111F RID: 4383
				Width
			}
		}
	}
}
