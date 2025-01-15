using System;
using System.IO;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000132 RID: 306
	public interface IMultiStreamSource
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000634 RID: 1588
		int Count { get; }

		// Token: 0x06000635 RID: 1589
		string GetPathOrNull(int index);

		// Token: 0x06000636 RID: 1590
		Stream Open(int index);

		// Token: 0x06000637 RID: 1591
		TextReader OpenTextReader(int index);
	}
}
