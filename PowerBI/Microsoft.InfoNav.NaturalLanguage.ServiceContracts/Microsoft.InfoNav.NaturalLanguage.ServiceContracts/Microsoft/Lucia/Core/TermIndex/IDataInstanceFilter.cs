using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000163 RID: 355
	public interface IDataInstanceFilter : IEntityTermFilter<DataInstanceInfo>
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000704 RID: 1796
		bool? IsAccessDenied { get; }

		// Token: 0x06000705 RID: 1797
		IEnumerable<string> FilterSampleValues(EdmPropertyRef propertyRef, IEnumerable<string> sampleValues);
	}
}
