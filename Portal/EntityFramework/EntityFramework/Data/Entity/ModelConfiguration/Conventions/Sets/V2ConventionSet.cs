using System;
using System.Collections.Generic;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x020001BC RID: 444
	internal static class V2ConventionSet
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x000404E0 File Offset: 0x0003E6E0
		static V2ConventionSet()
		{
			List<IConvention> list = new List<IConvention>(V1ConventionSet.Conventions.StoreModelConventions);
			int num = list.FindIndex((IConvention c) => c.GetType() == typeof(ColumnOrderingConvention));
			list[num] = new ColumnOrderingConventionStrict();
			V2ConventionSet._conventions = new ConventionSet(V1ConventionSet.Conventions.ConfigurationConventions, V1ConventionSet.Conventions.ConceptualModelConventions, V1ConventionSet.Conventions.ConceptualToStoreMappingConventions, list);
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00040549 File Offset: 0x0003E749
		public static ConventionSet Conventions
		{
			get
			{
				return V2ConventionSet._conventions;
			}
		}

		// Token: 0x04000A41 RID: 2625
		private static readonly ConventionSet _conventions;
	}
}
