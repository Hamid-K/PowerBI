using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001CD RID: 461
	public class Rectangle : ReportItem
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002474F File Offset: 0x0002294F
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00024763 File Offset: 0x00022963
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00024773 File Offset: 0x00022973
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00024787 File Offset: 0x00022987
		public PageBreak PageBreak
		{
			get
			{
				return (PageBreak)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00024797 File Offset: 0x00022997
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x000247A6 File Offset: 0x000229A6
		[ReportExpressionDefaultValue]
		public ReportExpression PageName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000247BB File Offset: 0x000229BB
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x000247CA File Offset: 0x000229CA
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return base.PropertyStore.GetBoolean(20);
			}
			set
			{
				base.PropertyStore.SetBoolean(20, value);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000247DA File Offset: 0x000229DA
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000247E9 File Offset: 0x000229E9
		[DefaultValue(false)]
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return base.PropertyStore.GetBoolean(21);
			}
			set
			{
				base.PropertyStore.SetBoolean(21, value);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000247F9 File Offset: 0x000229F9
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0002480D File Offset: 0x00022A0D
		[DefaultValue("")]
		public string LinkToChild
		{
			get
			{
				return (string)base.PropertyStore.GetObject(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002481D File Offset: 0x00022A1D
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00024825 File Offset: 0x00022A25
		[DefaultValue(DataElementOutputTypes.Auto)]
		[ValidEnumValues("RectangleDataElementOutputTypes")]
		public new DataElementOutputTypes DataElementOutput
		{
			get
			{
				return base.DataElementOutput;
			}
			set
			{
				base.DataElementOutput = value;
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002482E File Offset: 0x00022A2E
		public Rectangle()
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00024836 File Offset: 0x00022A36
		internal Rectangle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002483F File Offset: 0x00022A3F
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
		}

		// Token: 0x020003E4 RID: 996
		internal new class Definition : DefinitionStore<Rectangle, Rectangle.Definition.Properties>
		{
			// Token: 0x060018A6 RID: 6310 RVA: 0x0003BAEF File Offset: 0x00039CEF
			private Definition()
			{
			}

			// Token: 0x020004F6 RID: 1270
			internal enum Properties
			{
				// Token: 0x0400105F RID: 4191
				Style,
				// Token: 0x04001060 RID: 4192
				Name,
				// Token: 0x04001061 RID: 4193
				ActionInfo,
				// Token: 0x04001062 RID: 4194
				Top,
				// Token: 0x04001063 RID: 4195
				Left,
				// Token: 0x04001064 RID: 4196
				Height,
				// Token: 0x04001065 RID: 4197
				Width,
				// Token: 0x04001066 RID: 4198
				ZIndex,
				// Token: 0x04001067 RID: 4199
				Visibility,
				// Token: 0x04001068 RID: 4200
				ToolTip,
				// Token: 0x04001069 RID: 4201
				ToolTipLocID,
				// Token: 0x0400106A RID: 4202
				DocumentMapLabel,
				// Token: 0x0400106B RID: 4203
				DocumentMapLabelLocID,
				// Token: 0x0400106C RID: 4204
				Bookmark,
				// Token: 0x0400106D RID: 4205
				RepeatWith,
				// Token: 0x0400106E RID: 4206
				CustomProperties,
				// Token: 0x0400106F RID: 4207
				DataElementName,
				// Token: 0x04001070 RID: 4208
				DataElementOutput,
				// Token: 0x04001071 RID: 4209
				ReportItems,
				// Token: 0x04001072 RID: 4210
				PageBreak,
				// Token: 0x04001073 RID: 4211
				KeepTogether,
				// Token: 0x04001074 RID: 4212
				OmitBorderOnPageBreak,
				// Token: 0x04001075 RID: 4213
				LinkToChild,
				// Token: 0x04001076 RID: 4214
				PageName,
				// Token: 0x04001077 RID: 4215
				PropertyCount
			}
		}
	}
}
