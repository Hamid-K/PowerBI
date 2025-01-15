using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016C RID: 364
	internal static class INamedDataModelItemExtensions
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x0003B90E File Offset: 0x00039B0E
		public static string UniquifyName(this IEnumerable<INamedDataModelItem> namedDataModelItems, string name)
		{
			return namedDataModelItems.Select((INamedDataModelItem i) => i.Name).Uniquify(name);
		}
	}
}
