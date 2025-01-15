using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000275 RID: 629
	public sealed class ODataWorkspace : ODataAnnotatable
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00049B67 File Offset: 0x00047D67
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00049B6F File Offset: 0x00047D6F
		public IEnumerable<ODataResourceCollectionInfo> Collections { get; set; }
	}
}
