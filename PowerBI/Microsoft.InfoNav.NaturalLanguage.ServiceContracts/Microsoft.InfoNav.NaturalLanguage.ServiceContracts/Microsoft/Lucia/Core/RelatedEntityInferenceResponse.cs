using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C1 RID: 193
	[ImmutableObject(true)]
	public sealed class RelatedEntityInferenceResponse
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x0000709F File Offset: 0x0000529F
		public RelatedEntityInferenceResponse(IEnumerable<RelatedEntityInferenceResult> results, RelatedEntityInferenceWarning warning = RelatedEntityInferenceWarning.Success)
		{
			this.Results = results.AsReadOnlyList<RelatedEntityInferenceResult>();
			this.Warning = warning;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000070BA File Offset: 0x000052BA
		public RelatedEntityInferenceResponse(RelatedEntityInferenceWarning warning)
			: this(null, warning)
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000070C4 File Offset: 0x000052C4
		public IReadOnlyList<RelatedEntityInferenceResult> Results { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000070CC File Offset: 0x000052CC
		public RelatedEntityInferenceWarning Warning { get; }

		// Token: 0x040003F7 RID: 1015
		public static readonly RelatedEntityInferenceResponse EmptyResponse = new RelatedEntityInferenceResponse(RelatedEntityInferenceWarning.Success);
	}
}
