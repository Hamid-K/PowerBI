using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000026 RID: 38
	internal class List2005 : Tablix, IReportItem2005, IUpgradeable, IPageBreakLocation2005
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000300B File Offset: 0x0000120B
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000301F File Offset: 0x0000121F
		public Group Grouping
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(39);
			}
			set
			{
				base.PropertyStore.SetObject(39, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000302F File Offset: 0x0000122F
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003043 File Offset: 0x00001243
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> Sorting
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(40);
			}
			set
			{
				base.PropertyStore.SetObject(40, value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003053 File Offset: 0x00001253
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003067 File Offset: 0x00001267
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(41);
			}
			set
			{
				base.PropertyStore.SetObject(41, value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003077 File Offset: 0x00001277
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000308B File Offset: 0x0000128B
		public string DataInstanceName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(42);
			}
			set
			{
				base.PropertyStore.SetObject(42, value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000309B File Offset: 0x0000129B
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000030AA File Offset: 0x000012AA
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues(typeof(Constants2005), "List2005DataInstanceElementOutputTypes")]
		public DataElementOutputTypes DataInstanceElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(43);
			}
			set
			{
				((EnumProperty)DefinitionStore<List2005, List2005.Definition.Properties>.GetProperty(43)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(43, (int)value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000030CD File Offset: 0x000012CD
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000030DC File Offset: 0x000012DC
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(37);
			}
			set
			{
				base.PropertyStore.SetBoolean(37, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000030EC File Offset: 0x000012EC
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000030F4 File Offset: 0x000012F4
		[ReportExpressionDefaultValue]
		public ReportExpression NoRows
		{
			get
			{
				return base.NoRowsMessage;
			}
			set
			{
				base.NoRowsMessage = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000030FD File Offset: 0x000012FD
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000310C File Offset: 0x0000130C
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(38);
			}
			set
			{
				base.PropertyStore.SetBoolean(38, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000311C File Offset: 0x0000131C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003130 File Offset: 0x00001330
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003140 File Offset: 0x00001340
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003148 File Offset: 0x00001348
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003151 File Offset: 0x00001351
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003165 File Offset: 0x00001365
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003175 File Offset: 0x00001375
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00003182 File Offset: 0x00001382
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

		// Token: 0x06000130 RID: 304 RVA: 0x0000318B File Offset: 0x0000138B
		public List2005()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003193 File Offset: 0x00001393
		public List2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000319C File Offset: 0x0000139C
		public override void Initialize()
		{
			base.Initialize();
			this.DataInstanceElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000031AB File Offset: 0x000013AB
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeList(this);
		}

		// Token: 0x02000301 RID: 769
		internal new class Definition : DefinitionStore<List2005, List2005.Definition.Properties>
		{
			// Token: 0x060016FD RID: 5885 RVA: 0x00036482 File Offset: 0x00034682
			private Definition()
			{
			}

			// Token: 0x02000435 RID: 1077
			public enum Properties
			{
				// Token: 0x04000864 RID: 2148
				Action = 36,
				// Token: 0x04000865 RID: 2149
				PageBreakAtStart,
				// Token: 0x04000866 RID: 2150
				PageBreakAtEnd,
				// Token: 0x04000867 RID: 2151
				Grouping,
				// Token: 0x04000868 RID: 2152
				Sorting,
				// Token: 0x04000869 RID: 2153
				ReportItems,
				// Token: 0x0400086A RID: 2154
				DataInstanceName,
				// Token: 0x0400086B RID: 2155
				DataInstanceElementOutput,
				// Token: 0x0400086C RID: 2156
				PropertyCount
			}
		}
	}
}
