using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200001C RID: 28
	internal class CustomReportItem2005 : CustomReportItem, IReportItem2005, IUpgradeable
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002CBA File Offset: 0x00000EBA
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002CCE File Offset: 0x00000ECE
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002CE6 File Offset: 0x00000EE6
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002CEF File Offset: 0x00000EEF
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002D03 File Offset: 0x00000F03
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002D13 File Offset: 0x00000F13
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002D20 File Offset: 0x00000F20
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

		// Token: 0x060000D6 RID: 214 RVA: 0x00002D29 File Offset: 0x00000F29
		public CustomReportItem2005()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002D31 File Offset: 0x00000F31
		public CustomReportItem2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002D3A File Offset: 0x00000F3A
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeCustomReportItem(this);
		}

		// Token: 0x020002FD RID: 765
		internal new class Definition : DefinitionStore<CustomReportItem2005, CustomReportItem2005.Definition.Properties>
		{
			// Token: 0x060016F9 RID: 5881 RVA: 0x00036462 File Offset: 0x00034662
			private Definition()
			{
			}

			// Token: 0x02000431 RID: 1073
			public enum Properties
			{
				// Token: 0x04000855 RID: 2133
				Action = 21,
				// Token: 0x04000856 RID: 2134
				PropertyCount
			}
		}
	}
}
