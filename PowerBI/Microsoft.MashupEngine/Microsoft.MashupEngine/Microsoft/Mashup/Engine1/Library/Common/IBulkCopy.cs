using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010CE RID: 4302
	public interface IBulkCopy
	{
		// Token: 0x060070C2 RID: 28866
		bool TryCopyFrom(IPageReader reader, out long rowsAffected);
	}
}
