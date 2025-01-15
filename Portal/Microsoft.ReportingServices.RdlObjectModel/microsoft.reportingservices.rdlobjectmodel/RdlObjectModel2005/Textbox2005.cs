using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000051 RID: 81
	internal class Textbox2005 : Textbox, IReportItem2005, IUpgradeable
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00004B93 File Offset: 0x00002D93
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(26);
			}
			set
			{
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00004BB7 File Offset: 0x00002DB7
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00004BBF File Offset: 0x00002DBF
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00004BC8 File Offset: 0x00002DC8
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00004BDC File Offset: 0x00002DDC
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00004BEC File Offset: 0x00002DEC
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00004BF9 File Offset: 0x00002DF9
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00004C02 File Offset: 0x00002E02
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00004C0A File Offset: 0x00002E0A
		public new DataElementStyles2005 DataElementStyle
		{
			get
			{
				return (DataElementStyles2005)base.DataElementStyle;
			}
			set
			{
				base.DataElementStyle = (DataElementStyles)value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00004C13 File Offset: 0x00002E13
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00004C27 File Offset: 0x00002E27
		[ReportExpressionDefaultValue("")]
		public ReportExpression Value
		{
			get
			{
				return (ReportExpression)base.PropertyStore.GetObject(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00004C3C File Offset: 0x00002E3C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00004C50 File Offset: 0x00002E50
		[XmlChildAttribute("Value", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string ValueLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00004C60 File Offset: 0x00002E60
		public Textbox2005()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00004C68 File Offset: 0x00002E68
		public Textbox2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00004C71 File Offset: 0x00002E71
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeTextbox(this);
		}

		// Token: 0x0200031F RID: 799
		internal new class Definition : DefinitionStore<Textbox2005, Textbox2005.Definition.Properties>
		{
			// Token: 0x0600171B RID: 5915 RVA: 0x00036572 File Offset: 0x00034772
			private Definition()
			{
			}

			// Token: 0x02000453 RID: 1107
			public enum Properties
			{
				// Token: 0x04000905 RID: 2309
				Action = 26,
				// Token: 0x04000906 RID: 2310
				Value,
				// Token: 0x04000907 RID: 2311
				ValueLocID,
				// Token: 0x04000908 RID: 2312
				PropertyCount
			}
		}
	}
}
