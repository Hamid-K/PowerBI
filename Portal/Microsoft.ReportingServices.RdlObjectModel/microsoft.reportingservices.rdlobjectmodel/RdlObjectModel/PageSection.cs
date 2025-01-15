using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B2 RID: 434
	public class PageSection : ReportElement
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002359C File Offset: 0x0002179C
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x000235AA File Offset: 0x000217AA
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

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000235B9 File Offset: 0x000217B9
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x000235C7 File Offset: 0x000217C7
		[DefaultValue(false)]
		public bool PrintOnFirstPage
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

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000235D6 File Offset: 0x000217D6
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x000235E4 File Offset: 0x000217E4
		[DefaultValue(false)]
		public bool PrintOnLastPage
		{
			get
			{
				return base.PropertyStore.GetBoolean(3);
			}
			set
			{
				base.PropertyStore.SetBoolean(3, value);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000235F3 File Offset: 0x000217F3
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00023601 File Offset: 0x00021801
		[DefaultValue(false)]
		public bool PrintBetweenSections
		{
			get
			{
				return base.PropertyStore.GetBoolean(5);
			}
			set
			{
				base.PropertyStore.SetBoolean(5, value);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00023610 File Offset: 0x00021810
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00023623 File Offset: 0x00021823
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00023632 File Offset: 0x00021832
		public PageSection()
		{
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002363A File Offset: 0x0002183A
		internal PageSection(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00023643 File Offset: 0x00021843
		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x020003E1 RID: 993
		internal new class Definition : DefinitionStore<PageSection, PageSection.Definition.Properties>
		{
			// Token: 0x06001898 RID: 6296 RVA: 0x0003B9E9 File Offset: 0x00039BE9
			private Definition()
			{
			}

			// Token: 0x020004F5 RID: 1269
			internal enum Properties
			{
				// Token: 0x04001058 RID: 4184
				Style,
				// Token: 0x04001059 RID: 4185
				Height,
				// Token: 0x0400105A RID: 4186
				PrintOnFirstPage,
				// Token: 0x0400105B RID: 4187
				PrintOnLastPage,
				// Token: 0x0400105C RID: 4188
				ReportItems,
				// Token: 0x0400105D RID: 4189
				PrintBetweenSections
			}
		}
	}
}
