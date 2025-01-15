using System;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000040 RID: 64
	internal class BackgroundImage2005 : BackgroundImage
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000042A1 File Offset: 0x000024A1
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000042AF File Offset: 0x000024AF
		public new ReportExpression<BackgroundRepeatTypes2005> BackgroundRepeat
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BackgroundRepeatTypes2005>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000042C3 File Offset: 0x000024C3
		public BackgroundImage2005()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000042CB File Offset: 0x000024CB
		public BackgroundImage2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000315 RID: 789
		internal new class Definition : DefinitionStore<BackgroundImage, BackgroundImage2005.Definition.Properties>
		{
			// Token: 0x06001711 RID: 5905 RVA: 0x00036522 File Offset: 0x00034722
			private Definition()
			{
			}

			// Token: 0x02000449 RID: 1097
			public enum Properties
			{
				// Token: 0x040008D6 RID: 2262
				BackgroundRepeat = 6,
				// Token: 0x040008D7 RID: 2263
				PropertyCount
			}
		}
	}
}
