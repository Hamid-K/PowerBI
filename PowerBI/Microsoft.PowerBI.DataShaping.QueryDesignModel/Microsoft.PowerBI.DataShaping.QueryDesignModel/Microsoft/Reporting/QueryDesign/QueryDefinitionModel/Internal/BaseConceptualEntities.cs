using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000107 RID: 263
	internal sealed class BaseConceptualEntities
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x000294EA File Offset: 0x000276EA
		internal BaseConceptualEntities(IReadOnlyList<IConceptualEntity> completeEntities, IReadOnlyList<IConceptualEntity> prunedEntities)
		{
			this.CompleteSet = ArgumentValidation.CheckNotNull<IReadOnlyList<IConceptualEntity>>(completeEntities, "completeEntities");
			this.PrunedSet = ArgumentValidation.CheckNotNull<IReadOnlyList<IConceptualEntity>>(prunedEntities, "prunedEntities");
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00029514 File Offset: 0x00027714
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0002951C File Offset: 0x0002771C
		internal IReadOnlyList<IConceptualEntity> CompleteSet { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00029525 File Offset: 0x00027725
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0002952D File Offset: 0x0002772D
		internal IReadOnlyList<IConceptualEntity> PrunedSet { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00029536 File Offset: 0x00027736
		internal bool IsEmpty
		{
			get
			{
				return !this.CompleteSet.Any<IConceptualEntity>();
			}
		}

		// Token: 0x040009F2 RID: 2546
		internal static readonly BaseConceptualEntities Empty = new BaseConceptualEntities(Util.EmptyReadOnlyCollection<IConceptualEntity>(), Util.EmptyReadOnlyCollection<IConceptualEntity>());
	}
}
