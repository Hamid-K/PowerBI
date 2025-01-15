using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D6 RID: 6102
	public interface INativeQueryDomain : IQueryDomain
	{
		// Token: 0x06009A47 RID: 39495
		bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options);
	}
}
