using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000223 RID: 547
	internal abstract class ODataJsonLightReaderNestedInfo
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x00044855 File Offset: 0x00042A55
		internal ODataJsonLightReaderNestedInfo(IEdmProperty nestedProperty)
		{
			this.NestedProperty = nestedProperty;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00044864 File Offset: 0x00042A64
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0004486C File Offset: 0x00042A6C
		internal IEdmProperty NestedProperty { get; private set; }
	}
}
