using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000CA RID: 202
	public class CellContents : ReportObject
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001D656 File Offset: 0x0001B856
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0001D65E File Offset: 0x0001B85E
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return this.m_selected;
			}
			set
			{
				this.m_selected = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001D667 File Offset: 0x0001B867
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0001D67A File Offset: 0x0001B87A
		public ReportItem ReportItem
		{
			get
			{
				return (ReportItem)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001D689 File Offset: 0x0001B889
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0001D697 File Offset: 0x0001B897
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
				((IntProperty)DefinitionStore<CellContents, CellContents.Definition.Properties>.GetProperty(1)).Validate(this, value);
				base.PropertyStore.SetInteger(1, value);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0001D6C6 File Offset: 0x0001B8C6
		[DefaultValue(1)]
		[ValidValues(1, 2147483647)]
		public int RowSpan
		{
			get
			{
				return base.PropertyStore.GetInteger(2);
			}
			set
			{
				((IntProperty)DefinitionStore<CellContents, CellContents.Definition.Properties>.GetProperty(2)).Validate(this, value);
				base.PropertyStore.SetInteger(2, value);
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001D6E7 File Offset: 0x0001B8E7
		public CellContents()
		{
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001D6EF File Offset: 0x0001B8EF
		internal CellContents(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
		public override void Initialize()
		{
			base.Initialize();
			this.ColSpan = 1;
			this.RowSpan = 1;
		}

		// Token: 0x0400017E RID: 382
		private bool m_selected;

		// Token: 0x02000374 RID: 884
		internal class Definition : DefinitionStore<CellContents, CellContents.Definition.Properties>
		{
			// Token: 0x06001809 RID: 6153 RVA: 0x0003B264 File Offset: 0x00039464
			private Definition()
			{
			}

			// Token: 0x0200048F RID: 1167
			internal enum Properties
			{
				// Token: 0x04000B93 RID: 2963
				ReportItem,
				// Token: 0x04000B94 RID: 2964
				ColSpan,
				// Token: 0x04000B95 RID: 2965
				RowSpan
			}
		}
	}
}
