using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000EB RID: 235
	public interface IBinaryValue : IValue
	{
		// Token: 0x06000390 RID: 912
		bool TryGetLength(out long length);

		// Token: 0x06000391 RID: 913
		Stream Open();
	}
}
