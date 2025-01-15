using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E1E RID: 7710
	public interface IPackageSectionConfig
	{
		// Token: 0x17002EC1 RID: 11969
		// (get) Token: 0x0600BE08 RID: 48648
		string Culture { get; }

		// Token: 0x17002EC2 RID: 11970
		// (get) Token: 0x0600BE09 RID: 48649
		string TimeZone { get; }

		// Token: 0x17002EC3 RID: 11971
		// (get) Token: 0x0600BE0A RID: 48650
		string Version { get; }

		// Token: 0x17002EC4 RID: 11972
		// (get) Token: 0x0600BE0B RID: 48651
		string MinVersion { get; }

		// Token: 0x17002EC5 RID: 11973
		// (get) Token: 0x0600BE0C RID: 48652
		KeyValuePair<string, VersionRange>[] Dependencies { get; }
	}
}
