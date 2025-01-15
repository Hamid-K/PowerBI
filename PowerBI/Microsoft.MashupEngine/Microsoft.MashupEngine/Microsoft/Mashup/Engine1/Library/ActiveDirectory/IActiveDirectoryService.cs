using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF2 RID: 4082
	internal interface IActiveDirectoryService
	{
		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06006B12 RID: 27410
		string ComputerDomainName { get; }

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06006B13 RID: 27411
		int PageSize { get; }

		// Token: 0x06006B14 RID: 27412
		ActiveDirectoryRootServiceEntry GetRootServiceEntry(DirectoryEntry root);

		// Token: 0x06006B15 RID: 27413
		IEnumerable<ActiveDirectoryServiceSearchResult> FindAll(DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, params string[] propertiesToLoad);

		// Token: 0x06006B16 RID: 27414
		ActiveDirectoryServiceSearchResult FindOne(DirectoryEntry searchRoot, string filter, SearchScope searchScope, params string[] propertiesToLoad);
	}
}
