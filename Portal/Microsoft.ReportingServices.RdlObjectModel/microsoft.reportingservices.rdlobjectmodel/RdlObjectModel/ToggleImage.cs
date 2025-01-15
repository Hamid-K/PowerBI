using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000203 RID: 515
	public class ToggleImage : ReportObject
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00027E45 File Offset: 0x00026045
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00027E53 File Offset: 0x00026053
		public ReportExpression<bool> InitialState
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00027E67 File Offset: 0x00026067
		public ToggleImage()
		{
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00027E6F File Offset: 0x0002606F
		internal ToggleImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000409 RID: 1033
		internal class Definition : DefinitionStore<ToggleImage, ToggleImage.Definition.Properties>
		{
			// Token: 0x060018E2 RID: 6370 RVA: 0x0003BF83 File Offset: 0x0003A183
			private Definition()
			{
			}

			// Token: 0x02000518 RID: 1304
			internal enum Properties
			{
				// Token: 0x04001129 RID: 4393
				InitialState
			}
		}
	}
}
