using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000201 RID: 513
	public class TablixRow : ReportObject
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00027D39 File Offset: 0x00025F39
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00027D47 File Offset: 0x00025F47
		public ReportSize Height
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

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00027D56 File Offset: 0x00025F56
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x00027D69 File Offset: 0x00025F69
		[XmlElement(typeof(RdlCollection<TablixCell>))]
		public IList<TablixCell> TablixCells
		{
			get
			{
				return (IList<TablixCell>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00027D78 File Offset: 0x00025F78
		public TablixRow()
		{
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00027D80 File Offset: 0x00025F80
		internal TablixRow(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00027D89 File Offset: 0x00025F89
		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.TablixCells = new RdlCollection<TablixCell>();
		}

		// Token: 0x02000407 RID: 1031
		internal class Definition : DefinitionStore<TablixRow, TablixRow.Definition.Properties>
		{
			// Token: 0x060018E0 RID: 6368 RVA: 0x0003BF73 File Offset: 0x0003A173
			private Definition()
			{
			}

			// Token: 0x02000516 RID: 1302
			internal enum Properties
			{
				// Token: 0x04001121 RID: 4385
				Height,
				// Token: 0x04001122 RID: 4386
				TablixCells
			}
		}
	}
}
