using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016C RID: 364
	public class FrameBackground : ReportObject
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00020416 File Offset: 0x0001E616
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00020429 File Offset: 0x0001E629
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00020438 File Offset: 0x0001E638
		public FrameBackground()
		{
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00020440 File Offset: 0x0001E640
		internal FrameBackground(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200039D RID: 925
		internal class Definition : DefinitionStore<FrameBackground, FrameBackground.Definition.Properties>
		{
			// Token: 0x06001840 RID: 6208 RVA: 0x0003B52B File Offset: 0x0003972B
			private Definition()
			{
			}

			// Token: 0x020004B6 RID: 1206
			internal enum Properties
			{
				// Token: 0x04000DDB RID: 3547
				Style,
				// Token: 0x04000DDC RID: 3548
				PropertyCount
			}
		}
	}
}
