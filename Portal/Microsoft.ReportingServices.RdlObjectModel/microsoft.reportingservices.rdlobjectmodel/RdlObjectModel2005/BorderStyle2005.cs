using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200003E RID: 62
	internal class BorderStyle2005 : ReportObject
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000410B File Offset: 0x0000230B
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00004119 File Offset: 0x00002319
		public ReportExpression<BorderStyles2005> Default
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles2005>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000412D File Offset: 0x0000232D
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000413B File Offset: 0x0000233B
		public ReportExpression<BorderStyles2005> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles2005>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000414F File Offset: 0x0000234F
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000415D File Offset: 0x0000235D
		public ReportExpression<BorderStyles2005> Right
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles2005>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00004171 File Offset: 0x00002371
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000417F File Offset: 0x0000237F
		public ReportExpression<BorderStyles2005> Top
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles2005>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00004193 File Offset: 0x00002393
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000041A1 File Offset: 0x000023A1
		public ReportExpression<BorderStyles2005> Bottom
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BorderStyles2005>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000041B5 File Offset: 0x000023B5
		public BorderStyle2005()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000041BD File Offset: 0x000023BD
		public BorderStyle2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000041C6 File Offset: 0x000023C6
		public override void Initialize()
		{
			this.Default = BorderStyles2005.None;
		}

		// Token: 0x02000313 RID: 787
		internal class Definition : DefinitionStore<BorderStyle2005, BorderStyle2005.Definition.Properties>
		{
			// Token: 0x0600170F RID: 5903 RVA: 0x00036512 File Offset: 0x00034712
			private Definition()
			{
			}

			// Token: 0x02000447 RID: 1095
			public enum Properties
			{
				// Token: 0x040008CA RID: 2250
				Default,
				// Token: 0x040008CB RID: 2251
				Left,
				// Token: 0x040008CC RID: 2252
				Right,
				// Token: 0x040008CD RID: 2253
				Top,
				// Token: 0x040008CE RID: 2254
				Bottom
			}
		}
	}
}
