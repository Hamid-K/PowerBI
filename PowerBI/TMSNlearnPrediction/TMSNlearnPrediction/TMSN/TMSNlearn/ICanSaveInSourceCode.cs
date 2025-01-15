using System;
using System.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004BD RID: 1213
	public interface ICanSaveInSourceCode
	{
		// Token: 0x060018EB RID: 6379
		void SaveAsCode(TextWriter writer, FeatureNameCollection names);
	}
}
