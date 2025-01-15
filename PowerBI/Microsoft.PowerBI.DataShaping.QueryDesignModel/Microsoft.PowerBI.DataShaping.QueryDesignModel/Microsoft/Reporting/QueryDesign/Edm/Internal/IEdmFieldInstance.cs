using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001FA RID: 506
	public interface IEdmFieldInstance : IEquatable<IEdmFieldInstance>
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060017FA RID: 6138
		EntitySet Entity { get; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060017FB RID: 6139
		EdmField Field { get; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060017FC RID: 6140
		bool IsValid { get; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060017FD RID: 6141
		string Caption { get; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060017FE RID: 6142
		string ParentCaption { get; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060017FF RID: 6143
		IEnumerable<EdmDisplayFolder> DisplayFolderParents { get; }

		// Token: 0x06001800 RID: 6144
		EdmPropertyInstance ToPropertyInstance();
	}
}
