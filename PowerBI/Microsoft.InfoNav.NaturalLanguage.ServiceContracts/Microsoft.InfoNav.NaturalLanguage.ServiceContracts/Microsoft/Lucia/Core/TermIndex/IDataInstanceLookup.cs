using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000164 RID: 356
	public interface IDataInstanceLookup : IEntityTermLookup<DataInstanceInfo>, IDisposable
	{
		// Token: 0x06000706 RID: 1798
		IEnumerable<string> GetSampleValues(EdmPropertyRef edmPropertyRef, int maxSamples);
	}
}
