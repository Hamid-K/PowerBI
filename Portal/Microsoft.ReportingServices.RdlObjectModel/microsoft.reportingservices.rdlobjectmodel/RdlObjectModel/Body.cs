using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001CF RID: 463
	public class Body : ReportElement
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00024885 File Offset: 0x00022A85
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00024898 File Offset: 0x00022A98
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000248A7 File Offset: 0x00022AA7
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x000248B5 File Offset: 0x00022AB5
		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(2);
			}
			set
			{
				base.PropertyStore.SetSize(2, value);
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000248C4 File Offset: 0x00022AC4
		public Body()
		{
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000248CC File Offset: 0x00022ACC
		internal Body(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000248D5 File Offset: 0x00022AD5
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
			this.Height = Constants.DefaultZeroSize;
		}

		// Token: 0x020003E6 RID: 998
		internal new class Definition : DefinitionStore<Report, Body.Definition.Properties>
		{
			// Token: 0x060018A8 RID: 6312 RVA: 0x0003BAFF File Offset: 0x00039CFF
			private Definition()
			{
			}

			// Token: 0x020004F8 RID: 1272
			internal enum Properties
			{
				// Token: 0x0400107B RID: 4219
				Style,
				// Token: 0x0400107C RID: 4220
				ReportItems,
				// Token: 0x0400107D RID: 4221
				Height,
				// Token: 0x0400107E RID: 4222
				PropertyCount
			}
		}
	}
}
