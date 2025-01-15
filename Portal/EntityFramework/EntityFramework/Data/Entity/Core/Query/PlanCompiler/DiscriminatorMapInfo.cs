using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033C RID: 828
	internal class DiscriminatorMapInfo
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x0007423E File Offset: 0x0007243E
		internal DiscriminatorMapInfo(EntityTypeBase rootEntityType, bool includesSubTypes, ExplicitDiscriminatorMap discriminatorMap)
		{
			this.RootEntityType = rootEntityType;
			this.IncludesSubTypes = includesSubTypes;
			this.DiscriminatorMap = discriminatorMap;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0007425C File Offset: 0x0007245C
		internal void Merge(EntityTypeBase neededRootEntityType, bool includesSubtypes, ExplicitDiscriminatorMap discriminatorMap)
		{
			if (this.RootEntityType != neededRootEntityType || this.IncludesSubTypes != includesSubtypes)
			{
				if (!this.IncludesSubTypes || !includesSubtypes)
				{
					this.DiscriminatorMap = null;
				}
				if (TypeSemantics.IsSubTypeOf(this.RootEntityType, neededRootEntityType))
				{
					this.RootEntityType = neededRootEntityType;
					this.DiscriminatorMap = discriminatorMap;
				}
				if (!TypeSemantics.IsSubTypeOf(neededRootEntityType, this.RootEntityType))
				{
					this.DiscriminatorMap = null;
				}
			}
		}

		// Token: 0x04000DC7 RID: 3527
		internal EntityTypeBase RootEntityType;

		// Token: 0x04000DC8 RID: 3528
		internal bool IncludesSubTypes;

		// Token: 0x04000DC9 RID: 3529
		internal ExplicitDiscriminatorMap DiscriminatorMap;
	}
}
