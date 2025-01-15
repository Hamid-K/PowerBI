using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004C RID: 76
	internal class TableRow2005 : ReportObject
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000489C File Offset: 0x00002A9C
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000048AF File Offset: 0x00002AAF
		[XmlElement(typeof(RdlCollection<TableCell2005>))]
		[XmlArrayItem("TableCell", typeof(TableCell2005))]
		public IList<TableCell2005> TableCells
		{
			get
			{
				return (IList<TableCell2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000048BE File Offset: 0x00002ABE
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000048CC File Offset: 0x00002ACC
		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(1);
			}
			set
			{
				base.PropertyStore.SetSize(1, value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000048DB File Offset: 0x00002ADB
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000048EE File Offset: 0x00002AEE
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000048FD File Offset: 0x00002AFD
		public TableRow2005()
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00004905 File Offset: 0x00002B05
		public TableRow2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000490E File Offset: 0x00002B0E
		public override void Initialize()
		{
			base.Initialize();
			this.TableCells = new RdlCollection<TableCell2005>();
			this.Height = Constants.DefaultZeroSize;
		}

		// Token: 0x0200031A RID: 794
		internal class Definition : DefinitionStore<TableRow2005, TableRow2005.Definition.Properties>
		{
			// Token: 0x06001716 RID: 5910 RVA: 0x0003654A File Offset: 0x0003474A
			private Definition()
			{
			}

			// Token: 0x0200044E RID: 1102
			public enum Properties
			{
				// Token: 0x040008F0 RID: 2288
				TableCells,
				// Token: 0x040008F1 RID: 2289
				Height,
				// Token: 0x040008F2 RID: 2290
				Visibility
			}
		}
	}
}
