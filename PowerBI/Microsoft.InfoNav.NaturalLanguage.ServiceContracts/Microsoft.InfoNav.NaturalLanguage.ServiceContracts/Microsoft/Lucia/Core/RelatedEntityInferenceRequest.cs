using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000BF RID: 191
	[ImmutableObject(true)]
	public sealed class RelatedEntityInferenceRequest
	{
		// Token: 0x060003DF RID: 991 RVA: 0x00007034 File Offset: 0x00005234
		public RelatedEntityInferenceRequest(IEnumerable<EntityChoice> choices, int numberOfSolutions, string identifier = null, IEnumerable<FeatureSwitch> featureSwitches = null)
		{
			this.Choices = choices.AsReadOnlyList<EntityChoice>();
			this.NumberOfSolutions = numberOfSolutions;
			this.Identifier = identifier;
			this.FeatureSwitches = featureSwitches.AsReadOnlyList<FeatureSwitch>();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00007063 File Offset: 0x00005263
		public IReadOnlyList<EntityChoice> Choices { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000706B File Offset: 0x0000526B
		public int NumberOfSolutions { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00007073 File Offset: 0x00005273
		public string Identifier { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000707B File Offset: 0x0000527B
		public IReadOnlyList<FeatureSwitch> FeatureSwitches { get; }
	}
}
