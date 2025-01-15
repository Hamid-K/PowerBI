using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003F RID: 63
	internal class BorderWidth2005 : ReportObject
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000041D4 File Offset: 0x000023D4
		// (set) Token: 0x06000242 RID: 578 RVA: 0x000041E2 File Offset: 0x000023E2
		public ReportExpression<ReportSize> Default
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000041F6 File Offset: 0x000023F6
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00004204 File Offset: 0x00002404
		public ReportExpression<ReportSize> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00004218 File Offset: 0x00002418
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00004226 File Offset: 0x00002426
		public ReportExpression<ReportSize> Right
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000423A File Offset: 0x0000243A
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00004248 File Offset: 0x00002448
		public ReportExpression<ReportSize> Top
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000425C File Offset: 0x0000245C
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000426A File Offset: 0x0000246A
		public ReportExpression<ReportSize> Bottom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000427E File Offset: 0x0000247E
		public BorderWidth2005()
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004286 File Offset: 0x00002486
		public BorderWidth2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000428F File Offset: 0x0000248F
		public override void Initialize()
		{
			this.Default = Constants.DefaultBorderWidth;
		}

		// Token: 0x02000314 RID: 788
		internal class Definition : DefinitionStore<BorderWidth2005, BorderWidth2005.Definition.Properties>
		{
			// Token: 0x06001710 RID: 5904 RVA: 0x0003651A File Offset: 0x0003471A
			private Definition()
			{
			}

			// Token: 0x02000448 RID: 1096
			public enum Properties
			{
				// Token: 0x040008D0 RID: 2256
				Default,
				// Token: 0x040008D1 RID: 2257
				Left,
				// Token: 0x040008D2 RID: 2258
				Right,
				// Token: 0x040008D3 RID: 2259
				Top,
				// Token: 0x040008D4 RID: 2260
				Bottom
			}
		}
	}
}
