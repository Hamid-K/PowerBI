using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000222 RID: 546
	internal class ODataJsonLightReaderNestedPropertyInfo : ODataJsonLightReaderNestedInfo
	{
		// Token: 0x060017F1 RID: 6129 RVA: 0x00044834 File Offset: 0x00042A34
		internal ODataJsonLightReaderNestedPropertyInfo(ODataPropertyInfo nestedPropertyInfo, IEdmProperty nestedProperty)
			: base(nestedProperty)
		{
			this.NestedPropertyInfo = nestedPropertyInfo;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00044844 File Offset: 0x00042A44
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0004484C File Offset: 0x00042A4C
		internal ODataPropertyInfo NestedPropertyInfo { get; set; }
	}
}
