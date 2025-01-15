using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Buffers
{
	// Token: 0x0200026A RID: 618
	public interface ICharArrayPool
	{
		// Token: 0x06001BE3 RID: 7139
		char[] Rent(int minSize);

		// Token: 0x06001BE4 RID: 7140
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Return")]
		void Return(char[] array);
	}
}
