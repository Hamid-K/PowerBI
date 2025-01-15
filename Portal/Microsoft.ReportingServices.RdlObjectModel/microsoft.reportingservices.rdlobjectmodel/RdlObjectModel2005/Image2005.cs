using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000025 RID: 37
	internal class Image2005 : Image, IReportItem2005, IUpgradeable
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002F82 File Offset: 0x00001182
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002F96 File Offset: 0x00001196
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002FA6 File Offset: 0x000011A6
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002FAE File Offset: 0x000011AE
		[ReportExpressionDefaultValue]
		public ReportExpression Label
		{
			get
			{
				return base.DocumentMapLabel;
			}
			set
			{
				base.DocumentMapLabel = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002FB7 File Offset: 0x000011B7
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002FCB File Offset: 0x000011CB
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002FDB File Offset: 0x000011DB
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002FE8 File Offset: 0x000011E8
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00002FF1 File Offset: 0x000011F1
		public Image2005()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002FF9 File Offset: 0x000011F9
		public Image2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003002 File Offset: 0x00001202
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeReportItem(this);
		}

		// Token: 0x02000300 RID: 768
		internal new class Definition : DefinitionStore<Image2005, Image2005.Definition.Properties>
		{
			// Token: 0x060016FC RID: 5884 RVA: 0x0003647A File Offset: 0x0003467A
			private Definition()
			{
			}

			// Token: 0x02000434 RID: 1076
			public enum Properties
			{
				// Token: 0x04000861 RID: 2145
				Action = 24,
				// Token: 0x04000862 RID: 2146
				PropertyCount
			}
		}
	}
}
