using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003C RID: 60
	internal class BorderColor2005 : ReportObject
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000400A File Offset: 0x0000220A
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00004018 File Offset: 0x00002218
		public ReportExpression<ReportColor> Default
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000402C File Offset: 0x0000222C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000403A File Offset: 0x0000223A
		public ReportExpression<ReportColor> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000404E File Offset: 0x0000224E
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000405C File Offset: 0x0000225C
		public ReportExpression<ReportColor> Right
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004070 File Offset: 0x00002270
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000407E File Offset: 0x0000227E
		public ReportExpression<ReportColor> Top
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00004092 File Offset: 0x00002292
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000040A0 File Offset: 0x000022A0
		public ReportExpression<ReportColor> Bottom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000040B4 File Offset: 0x000022B4
		public BorderColor2005()
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000040BC File Offset: 0x000022BC
		public BorderColor2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000040C5 File Offset: 0x000022C5
		public override void Initialize()
		{
			this.Default = Constants.DefaultBorderColor;
		}

		// Token: 0x02000312 RID: 786
		internal class Definition : DefinitionStore<BorderColor2005, BorderColor2005.Definition.Properties>
		{
			// Token: 0x0600170E RID: 5902 RVA: 0x0003650A File Offset: 0x0003470A
			private Definition()
			{
			}

			// Token: 0x02000446 RID: 1094
			public enum Properties
			{
				// Token: 0x040008C4 RID: 2244
				Default,
				// Token: 0x040008C5 RID: 2245
				Left,
				// Token: 0x040008C6 RID: 2246
				Right,
				// Token: 0x040008C7 RID: 2247
				Top,
				// Token: 0x040008C8 RID: 2248
				Bottom
			}
		}
	}
}
