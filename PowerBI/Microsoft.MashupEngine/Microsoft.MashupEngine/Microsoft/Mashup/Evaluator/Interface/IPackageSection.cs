using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E1F RID: 7711
	public interface IPackageSection : IDocumentHost
	{
		// Token: 0x17002EC6 RID: 11974
		// (get) Token: 0x0600BE0D RID: 48653
		IPackageSectionConfig Config { get; }

		// Token: 0x17002EC7 RID: 11975
		// (get) Token: 0x0600BE0E RID: 48654
		SegmentedString Text { get; }
	}
}
