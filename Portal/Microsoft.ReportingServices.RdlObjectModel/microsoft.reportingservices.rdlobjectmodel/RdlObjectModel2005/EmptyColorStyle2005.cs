using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003B RID: 59
	internal class EmptyColorStyle2005 : EmptyColorStyle, IUpgradeable
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00003ECB File Offset: 0x000020CB
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00003EDF File Offset: 0x000020DF
		public EmptyBorderColor2005 BorderColor
		{
			get
			{
				return (EmptyBorderColor2005)base.PropertyStore.GetObject(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00003EEF File Offset: 0x000020EF
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00003F03 File Offset: 0x00002103
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00003F13 File Offset: 0x00002113
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00003F27 File Offset: 0x00002127
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00003F37 File Offset: 0x00002137
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00003F46 File Offset: 0x00002146
		public new ReportExpression<ReportEnum<FontWeight2005>> FontWeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<FontWeight2005>>>(37);
			}
			set
			{
				base.PropertyStore.SetObject(37, value);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00003F5B File Offset: 0x0000215B
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00003F6A File Offset: 0x0000216A
		public new ReportExpression<ReportEnum<WritingMode2005>> WritingMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<WritingMode2005>>>(38);
			}
			set
			{
				base.PropertyStore.SetObject(38, value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003F7F File Offset: 0x0000217F
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00003F8E File Offset: 0x0000218E
		public new ReportExpression<ReportEnum<UnicodeBiDi2005>> UnicodeBiDi
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<UnicodeBiDi2005>>>(39);
			}
			set
			{
				base.PropertyStore.SetObject(39, value);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00003FA3 File Offset: 0x000021A3
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00003FB2 File Offset: 0x000021B2
		public new ReportExpression<ReportEnum<Calendar2005>> Calendar
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportEnum<Calendar2005>>>(40);
			}
			set
			{
				base.PropertyStore.SetObject(40, value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00003FC7 File Offset: 0x000021C7
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003FCF File Offset: 0x000021CF
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public new ReportExpression<ReportColor> Color
		{
			get
			{
				return base.Color;
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00003FD8 File Offset: 0x000021D8
		public EmptyColorStyle2005()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00003FE0 File Offset: 0x000021E0
		public EmptyColorStyle2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00003FE9 File Offset: 0x000021E9
		public override void Initialize()
		{
			base.Initialize();
			this.Color = Constants.DefaultEmptyColor;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00004001 File Offset: 0x00002201
		public virtual void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeEmptyColorStyle(this);
		}

		// Token: 0x02000311 RID: 785
		internal new class Definition : DefinitionStore<EmptyColorStyle, EmptyColorStyle2005.Definition.Properties>
		{
			// Token: 0x0600170D RID: 5901 RVA: 0x00036502 File Offset: 0x00034702
			private Definition()
			{
			}

			// Token: 0x02000445 RID: 1093
			public enum Properties
			{
				// Token: 0x040008BB RID: 2235
				BorderColor = 34,
				// Token: 0x040008BC RID: 2236
				BorderStyle,
				// Token: 0x040008BD RID: 2237
				BorderWidth,
				// Token: 0x040008BE RID: 2238
				FontWeight,
				// Token: 0x040008BF RID: 2239
				WritingMode,
				// Token: 0x040008C0 RID: 2240
				UnicodeBiDi,
				// Token: 0x040008C1 RID: 2241
				Calendar,
				// Token: 0x040008C2 RID: 2242
				PropertyCount
			}
		}
	}
}
