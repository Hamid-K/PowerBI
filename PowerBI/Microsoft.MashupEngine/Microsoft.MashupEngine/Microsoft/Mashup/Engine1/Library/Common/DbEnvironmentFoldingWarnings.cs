using System;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001083 RID: 4227
	internal static class DbEnvironmentFoldingWarnings
	{
		// Token: 0x06006EB7 RID: 28343 RVA: 0x0017E31C File Offset: 0x0017C51C
		public static FoldingWarnings.FoldingWarning<ContextLabel, ContextLabel> InvalidContext(ContextLabel newMilestone, ContextLabel oldMilestone)
		{
			return new FoldingWarnings.FoldingWarning<ContextLabel, ContextLabel>("Context milestone of \"{0}\" is invalid inside a Context milestone of \"{1}\"", newMilestone, oldMilestone);
		}

		// Token: 0x04003D69 RID: 15721
		public const string InvalidContextFormat = "Context milestone of \"{0}\" is invalid inside a Context milestone of \"{1}\"";
	}
}
