using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000115 RID: 277
	internal class EntitySetTypePair : Tuple<EntitySet, Type>
	{
		// Token: 0x0600135E RID: 4958 RVA: 0x000328F0 File Offset: 0x00030AF0
		public EntitySetTypePair(EntitySet entitySet, Type type)
			: base(entitySet, type)
		{
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x000328FA File Offset: 0x00030AFA
		public EntitySet EntitySet
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00032902 File Offset: 0x00030B02
		public Type BaseType
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
