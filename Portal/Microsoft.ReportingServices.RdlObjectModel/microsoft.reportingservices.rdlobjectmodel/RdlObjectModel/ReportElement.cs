using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001CE RID: 462
	public abstract class ReportElement : ReportObject
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00024852 File Offset: 0x00022A52
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00024865 File Offset: 0x00022A65
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

		// Token: 0x06000F18 RID: 3864 RVA: 0x00024874 File Offset: 0x00022A74
		public ReportElement()
		{
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002487C File Offset: 0x00022A7C
		internal ReportElement(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003E5 RID: 997
		internal class Definition : DefinitionStore<ReportElement, ReportElement.Definition.Properties>
		{
			// Token: 0x060018A7 RID: 6311 RVA: 0x0003BAF7 File Offset: 0x00039CF7
			private Definition()
			{
			}

			// Token: 0x020004F7 RID: 1271
			internal enum Properties
			{
				// Token: 0x04001079 RID: 4217
				Style
			}
		}
	}
}
