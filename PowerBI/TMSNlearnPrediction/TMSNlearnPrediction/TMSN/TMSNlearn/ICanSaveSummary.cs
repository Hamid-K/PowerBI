using System;
using System.IO;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000220 RID: 544
	public interface ICanSaveSummary
	{
		// Token: 0x06000C2D RID: 3117
		void SaveSummary(TextWriter writer, FeatureNameCollection names);
	}
}
