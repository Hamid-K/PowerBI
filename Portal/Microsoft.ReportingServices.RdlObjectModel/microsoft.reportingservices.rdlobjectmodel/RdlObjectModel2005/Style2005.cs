using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000039 RID: 57
	internal class Style2005 : Style, IUpgradeable
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00003C3C File Offset: 0x00001E3C
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00003C50 File Offset: 0x00001E50
		public BorderColor2005 BorderColor
		{
			get
			{
				return (BorderColor2005)base.PropertyStore.GetObject(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00003C60 File Offset: 0x00001E60
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00003C74 File Offset: 0x00001E74
		public BorderStyle2005 BorderStyle
		{
			get
			{
				return (BorderStyle2005)base.PropertyStore.GetObject(35);
			}
			set
			{
				base.PropertyStore.SetObject(35, value);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00003C84 File Offset: 0x00001E84
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00003C98 File Offset: 0x00001E98
		public BorderWidth2005 BorderWidth
		{
			get
			{
				return (BorderWidth2005)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00003CA8 File Offset: 0x00001EA8
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003CBC File Offset: 0x00001EBC
		public new BackgroundImage2005 BackgroundImage
		{
			get
			{
				return (BackgroundImage2005)base.PropertyStore.GetObject(37);
			}
			set
			{
				base.PropertyStore.SetObject(37, value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003CCC File Offset: 0x00001ECC
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00003CDB File Offset: 0x00001EDB
		[ValidEnumValues(typeof(Constants2005), "Style2005FontStyles")]
		public new ReportExpression<FontStyles> FontStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<FontStyles>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003CF0 File Offset: 0x00001EF0
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00003CFF File Offset: 0x00001EFF
		public new ReportExpression<ReportEnum<FontWeight2005>> FontWeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<FontWeight2005>>>(38);
			}
			set
			{
				base.PropertyStore.SetObject(38, value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00003D14 File Offset: 0x00001F14
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00003D23 File Offset: 0x00001F23
		[ValidEnumValues(typeof(Constants2005), "Style2005TextDecorations")]
		public new ReportExpression<TextDecorations> TextDecoration
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextDecorations>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00003D38 File Offset: 0x00001F38
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00003D47 File Offset: 0x00001F47
		[ValidEnumValues(typeof(Constants2005), "Style2005TextAlignments")]
		public new ReportExpression<TextAlignments> TextAlign
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextAlignments>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003D5C File Offset: 0x00001F5C
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00003D6B File Offset: 0x00001F6B
		[ValidEnumValues(typeof(Constants2005), "Style2005VerticalAlignments")]
		public new ReportExpression<VerticalAlignments> VerticalAlign
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<VerticalAlignments>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003D80 File Offset: 0x00001F80
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00003D8F File Offset: 0x00001F8F
		[ValidEnumValues(typeof(Constants2005), "Style2005TextDirections")]
		public new ReportExpression<TextDirections> Direction
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextDirections>>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00003DA4 File Offset: 0x00001FA4
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00003DB3 File Offset: 0x00001FB3
		public new ReportExpression<ReportEnum<WritingMode2005>> WritingMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<WritingMode2005>>>(39);
			}
			set
			{
				base.PropertyStore.SetObject(39, value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00003DC8 File Offset: 0x00001FC8
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00003DD7 File Offset: 0x00001FD7
		public new ReportExpression<ReportEnum<UnicodeBiDi2005>> UnicodeBiDi
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<UnicodeBiDi2005>>>(40);
			}
			set
			{
				base.PropertyStore.SetObject(40, value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00003DEC File Offset: 0x00001FEC
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00003DFB File Offset: 0x00001FFB
		public new ReportExpression<ReportEnum<Calendar2005>> Calendar
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<Calendar2005>>>(41);
			}
			set
			{
				base.PropertyStore.SetObject(41, value);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00003E10 File Offset: 0x00002010
		public Style2005()
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00003E18 File Offset: 0x00002018
		public Style2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00003E21 File Offset: 0x00002021
		public virtual void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeStyle(this);
		}

		// Token: 0x02000310 RID: 784
		internal new class Definition : DefinitionStore<Style, Style2005.Definition.Properties>
		{
			// Token: 0x0600170C RID: 5900 RVA: 0x000364FA File Offset: 0x000346FA
			private Definition()
			{
			}

			// Token: 0x02000444 RID: 1092
			public enum Properties
			{
				// Token: 0x040008B1 RID: 2225
				BorderColor = 34,
				// Token: 0x040008B2 RID: 2226
				BorderStyle,
				// Token: 0x040008B3 RID: 2227
				BorderWidth,
				// Token: 0x040008B4 RID: 2228
				BackgroundImage,
				// Token: 0x040008B5 RID: 2229
				FontWeight,
				// Token: 0x040008B6 RID: 2230
				WritingMode,
				// Token: 0x040008B7 RID: 2231
				UnicodeBiDi,
				// Token: 0x040008B8 RID: 2232
				Calendar,
				// Token: 0x040008B9 RID: 2233
				PropertyCount
			}
		}
	}
}
