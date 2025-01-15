using System;
using System.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.IO
{
	// Token: 0x0200002F RID: 47
	public interface ISerializer<T>
	{
		// Token: 0x0600011C RID: 284
		void Write(Stream s, T item);

		// Token: 0x0600011D RID: 285
		T Read(Stream s);
	}
}
