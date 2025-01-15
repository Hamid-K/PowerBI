using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C3 RID: 195
	[ImmutableObject(true)]
	public sealed class RelatedEntityInferenceResult
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x000070E1 File Offset: 0x000052E1
		public RelatedEntityInferenceResult(IReadOnlyList<int> selectedOptions, double score)
		{
			this.SelectedOptions = selectedOptions.AsReadOnlyList<int>();
			this.Score = score;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000070FC File Offset: 0x000052FC
		public IReadOnlyList<int> SelectedOptions { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00007104 File Offset: 0x00005304
		public double Score { get; }
	}
}
