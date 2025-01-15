using System;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006BF RID: 1727
	public sealed class LearningTask<TSpec> : LearningTask where TSpec : Spec
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x000689C6 File Offset: 0x00066BC6
		internal LearningTask(LearningTask @base)
			: base(@base.Language, @base.Spec)
		{
			base.PruningRequest = @base.PruningRequest;
			base.CloneInternals(@base);
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000689ED File Offset: 0x00066BED
		public new TSpec Spec
		{
			get
			{
				return base.Spec as TSpec;
			}
		}
	}
}
