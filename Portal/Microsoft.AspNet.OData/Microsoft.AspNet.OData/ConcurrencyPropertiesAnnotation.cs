using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000017 RID: 23
	public class ConcurrencyPropertiesAnnotation : ConcurrentDictionary<IEdmNavigationSource, IEnumerable<IEdmStructuralProperty>>
	{
	}
}
