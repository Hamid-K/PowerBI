using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000023 RID: 35
	internal class Grouping2005 : Group, IPageBreakLocation2005, IUpgradeable
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002E7A File Offset: 0x0000107A
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002E82 File Offset: 0x00001082
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002E8B File Offset: 0x0000108B
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00002E9A File Offset: 0x0000109A
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(13);
			}
			set
			{
				base.PropertyStore.SetBoolean(13, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002EAA File Offset: 0x000010AA
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00002EB9 File Offset: 0x000010B9
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(14);
			}
			set
			{
				base.PropertyStore.SetBoolean(14, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00002EDD File Offset: 0x000010DD
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002EED File Offset: 0x000010ED
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002F01 File Offset: 0x00001101
		[DefaultValue("")]
		public string DataCollectionName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002F11 File Offset: 0x00001111
		public Grouping2005()
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002F19 File Offset: 0x00001119
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002F2C File Offset: 0x0000112C
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002F3B File Offset: 0x0000113B
		public Grouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002F44 File Offset: 0x00001144
		public override void Initialize()
		{
			base.Initialize();
			this.CustomProperties = new RdlCollection<CustomProperty>();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002F57 File Offset: 0x00001157
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradePageBreak(this);
		}

		// Token: 0x020002FF RID: 767
		public new class Definition : DefinitionStore<Grouping2005, Grouping2005.Definition.Properties>
		{
			// Token: 0x060016FB RID: 5883 RVA: 0x00036472 File Offset: 0x00034672
			private Definition()
			{
			}

			// Token: 0x02000433 RID: 1075
			public enum Properties
			{
				// Token: 0x0400085B RID: 2139
				PageBreakAtStart = 13,
				// Token: 0x0400085C RID: 2140
				PageBreakAtEnd,
				// Token: 0x0400085D RID: 2141
				CustomProperties,
				// Token: 0x0400085E RID: 2142
				DataCollectionName,
				// Token: 0x0400085F RID: 2143
				PropertyCount
			}
		}
	}
}
