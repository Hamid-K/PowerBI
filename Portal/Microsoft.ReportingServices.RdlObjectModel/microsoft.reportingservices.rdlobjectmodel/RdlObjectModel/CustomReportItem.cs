using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000AC RID: 172
	public class CustomReportItem : ReportItem
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001B49F File Offset: 0x0001969F
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001B4B3 File Offset: 0x000196B3
		public string Type
		{
			get
			{
				return (string)base.PropertyStore.GetObject(18);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001B4D1 File Offset: 0x000196D1
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0001B4E5 File Offset: 0x000196E5
		public AltReportItem AltReportItem
		{
			get
			{
				return (AltReportItem)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001B4F5 File Offset: 0x000196F5
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0001B509 File Offset: 0x00019709
		public CustomData CustomData
		{
			get
			{
				return (CustomData)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001B519 File Offset: 0x00019719
		public CustomReportItem()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001B521 File Offset: 0x00019721
		internal CustomReportItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001B52A File Offset: 0x0001972A
		public override void Initialize()
		{
			base.Initialize();
			this.Type = "";
		}

		// Token: 0x0200035D RID: 861
		internal new class Definition : DefinitionStore<CustomReportItem, CustomReportItem.Definition.Properties>
		{
			// Token: 0x060017E0 RID: 6112 RVA: 0x0003AC23 File Offset: 0x00038E23
			private Definition()
			{
			}

			// Token: 0x0200047C RID: 1148
			internal enum Properties
			{
				// Token: 0x04000AB4 RID: 2740
				Style,
				// Token: 0x04000AB5 RID: 2741
				Name,
				// Token: 0x04000AB6 RID: 2742
				ActionInfo,
				// Token: 0x04000AB7 RID: 2743
				Top,
				// Token: 0x04000AB8 RID: 2744
				Left,
				// Token: 0x04000AB9 RID: 2745
				Height,
				// Token: 0x04000ABA RID: 2746
				Width,
				// Token: 0x04000ABB RID: 2747
				ZIndex,
				// Token: 0x04000ABC RID: 2748
				Visibility,
				// Token: 0x04000ABD RID: 2749
				ToolTip,
				// Token: 0x04000ABE RID: 2750
				ToolTipLocID,
				// Token: 0x04000ABF RID: 2751
				DocumentMapLabel,
				// Token: 0x04000AC0 RID: 2752
				DocumentMapLabelLocID,
				// Token: 0x04000AC1 RID: 2753
				Bookmark,
				// Token: 0x04000AC2 RID: 2754
				RepeatWith,
				// Token: 0x04000AC3 RID: 2755
				CustomProperties,
				// Token: 0x04000AC4 RID: 2756
				DataElementName,
				// Token: 0x04000AC5 RID: 2757
				DataElementOutput,
				// Token: 0x04000AC6 RID: 2758
				Type,
				// Token: 0x04000AC7 RID: 2759
				AltReportItem,
				// Token: 0x04000AC8 RID: 2760
				CustomData,
				// Token: 0x04000AC9 RID: 2761
				PropertyCount
			}
		}
	}
}
