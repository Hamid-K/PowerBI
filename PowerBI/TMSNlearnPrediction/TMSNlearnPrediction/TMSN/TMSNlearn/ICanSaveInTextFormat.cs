using System;
using System.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B9 RID: 1209
	public interface ICanSaveInTextFormat
	{
		// Token: 0x060018E7 RID: 6375
		void SaveAsText(TextWriter writer, FeatureNameCollection names);
	}
}
