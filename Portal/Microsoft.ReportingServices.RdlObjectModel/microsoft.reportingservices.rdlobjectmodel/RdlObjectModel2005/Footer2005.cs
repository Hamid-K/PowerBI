using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004D RID: 77
	internal class Footer2005 : ReportObject
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000492C File Offset: 0x00002B2C
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000493F File Offset: 0x00002B3F
		[XmlElement(typeof(RdlCollection<TableRow2005>))]
		[XmlArrayItem("TableRow", typeof(TableRow2005))]
		public IList<TableRow2005> TableRows
		{
			get
			{
				return (IList<TableRow2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000494E File Offset: 0x00002B4E
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000495C File Offset: 0x00002B5C
		[DefaultValue(false)]
		public bool RepeatOnNewPage
		{
			get
			{
				return base.PropertyStore.GetBoolean(1);
			}
			set
			{
				base.PropertyStore.SetBoolean(1, value);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000496B File Offset: 0x00002B6B
		public Footer2005()
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00004973 File Offset: 0x00002B73
		public Footer2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000497C File Offset: 0x00002B7C
		public override void Initialize()
		{
			base.Initialize();
			this.TableRows = new RdlCollection<TableRow2005>();
		}

		// Token: 0x0200031B RID: 795
		internal class Definition : DefinitionStore<Footer2005, Footer2005.Definition.Properties>
		{
			// Token: 0x06001717 RID: 5911 RVA: 0x00036552 File Offset: 0x00034752
			private Definition()
			{
			}

			// Token: 0x0200044F RID: 1103
			public enum Properties
			{
				// Token: 0x040008F4 RID: 2292
				TableRows,
				// Token: 0x040008F5 RID: 2293
				RepeatOnNewPage
			}
		}
	}
}
