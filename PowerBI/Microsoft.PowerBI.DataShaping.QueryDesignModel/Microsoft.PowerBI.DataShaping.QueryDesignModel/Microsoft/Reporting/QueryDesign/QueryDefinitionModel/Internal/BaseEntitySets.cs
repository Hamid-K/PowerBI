using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000106 RID: 262
	internal sealed class BaseEntitySets
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00029478 File Offset: 0x00027678
		internal BaseEntitySets(ReadOnlyCollection<EntitySet> completeSet, ReadOnlyCollection<EntitySet> prunedSet)
		{
			this.CompleteSet = ArgumentValidation.CheckNotNull<ReadOnlyCollection<EntitySet>>(completeSet, "completeSet");
			this.PrunedSet = ArgumentValidation.CheckNotNull<ReadOnlyCollection<EntitySet>>(prunedSet, "prunedSet");
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x000294A2 File Offset: 0x000276A2
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x000294AA File Offset: 0x000276AA
		internal ReadOnlyCollection<EntitySet> CompleteSet { get; private set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000294B3 File Offset: 0x000276B3
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x000294BB File Offset: 0x000276BB
		internal ReadOnlyCollection<EntitySet> PrunedSet { get; private set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x000294C4 File Offset: 0x000276C4
		internal bool IsEmpty
		{
			get
			{
				return !this.CompleteSet.Any<EntitySet>();
			}
		}

		// Token: 0x040009EF RID: 2543
		internal static readonly BaseEntitySets Empty = new BaseEntitySets(Util.EmptyReadOnlyCollection<EntitySet>(), Util.EmptyReadOnlyCollection<EntitySet>());
	}
}
