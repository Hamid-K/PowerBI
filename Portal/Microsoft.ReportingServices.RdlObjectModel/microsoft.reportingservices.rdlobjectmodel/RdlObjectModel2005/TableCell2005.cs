using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000050 RID: 80
	internal class TableCell2005 : ReportObject
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00004B17 File Offset: 0x00002D17
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00004B2A File Offset: 0x00002D2A
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00004B39 File Offset: 0x00002D39
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00004B47 File Offset: 0x00002D47
		[DefaultValue(1)]
		[ValidValues(1, 2147483647)]
		public int ColSpan
		{
			get
			{
				return base.PropertyStore.GetInteger(1);
			}
			set
			{
				((IntProperty)DefinitionStore<TableCell2005, TableCell2005.Definition.Properties>.GetProperty(1)).Validate(this, value);
				base.PropertyStore.SetInteger(1, value);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00004B68 File Offset: 0x00002D68
		public TableCell2005()
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00004B70 File Offset: 0x00002D70
		public TableCell2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00004B79 File Offset: 0x00002D79
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
			this.ColSpan = 1;
		}

		// Token: 0x0200031E RID: 798
		internal class Definition : DefinitionStore<TableCell2005, TableCell2005.Definition.Properties>
		{
			// Token: 0x0600171A RID: 5914 RVA: 0x0003656A File Offset: 0x0003476A
			private Definition()
			{
			}

			// Token: 0x02000452 RID: 1106
			public enum Properties
			{
				// Token: 0x04000902 RID: 2306
				ReportItems,
				// Token: 0x04000903 RID: 2307
				ColSpan
			}
		}
	}
}
