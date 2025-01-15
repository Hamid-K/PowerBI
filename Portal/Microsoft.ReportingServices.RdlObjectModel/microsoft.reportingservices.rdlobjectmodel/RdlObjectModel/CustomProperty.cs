using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D1 RID: 465
	public class CustomProperty : ReportObject
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00024965 File Offset: 0x00022B65
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x00024973 File Offset: 0x00022B73
		public ReportExpression Name
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00024987 File Offset: 0x00022B87
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00024995 File Offset: 0x00022B95
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000249A9 File Offset: 0x00022BA9
		public CustomProperty()
		{
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000249B1 File Offset: 0x00022BB1
		internal CustomProperty(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003E8 RID: 1000
		internal class Definition : DefinitionStore<CustomProperty, CustomProperty.Definition.Properties>
		{
			// Token: 0x060018AA RID: 6314 RVA: 0x0003BB0F File Offset: 0x00039D0F
			private Definition()
			{
			}

			// Token: 0x020004FA RID: 1274
			internal enum Properties
			{
				// Token: 0x04001084 RID: 4228
				Name,
				// Token: 0x04001085 RID: 4229
				Value
			}
		}
	}
}
