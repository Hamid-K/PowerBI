using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200004B RID: 75
	internal class Header2005 : ReportObject
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000481C File Offset: 0x00002A1C
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000482F File Offset: 0x00002A2F
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000483E File Offset: 0x00002A3E
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000484C File Offset: 0x00002A4C
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000485B File Offset: 0x00002A5B
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00004869 File Offset: 0x00002A69
		[DefaultValue(false)]
		public bool FixedHeader
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00004878 File Offset: 0x00002A78
		public Header2005()
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00004880 File Offset: 0x00002A80
		public Header2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00004889 File Offset: 0x00002A89
		public override void Initialize()
		{
			base.Initialize();
			this.TableRows = new RdlCollection<TableRow2005>();
		}

		// Token: 0x02000319 RID: 793
		internal class Definition : DefinitionStore<Header2005, Header2005.Definition.Properties>
		{
			// Token: 0x06001715 RID: 5909 RVA: 0x00036542 File Offset: 0x00034742
			private Definition()
			{
			}

			// Token: 0x0200044D RID: 1101
			public enum Properties
			{
				// Token: 0x040008EC RID: 2284
				TableRows,
				// Token: 0x040008ED RID: 2285
				RepeatOnNewPage,
				// Token: 0x040008EE RID: 2286
				FixedHeader
			}
		}
	}
}
