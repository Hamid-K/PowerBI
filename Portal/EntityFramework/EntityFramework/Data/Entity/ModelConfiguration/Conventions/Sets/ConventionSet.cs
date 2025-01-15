using System;
using System.Collections.Generic;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x020001BA RID: 442
	internal class ConventionSet
	{
		// Token: 0x060017A4 RID: 6052 RVA: 0x000402CA File Offset: 0x0003E4CA
		public ConventionSet()
		{
			this.ConfigurationConventions = new IConvention[0];
			this.ConceptualModelConventions = new IConvention[0];
			this.ConceptualToStoreMappingConventions = new IConvention[0];
			this.StoreModelConventions = new IConvention[0];
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00040302 File Offset: 0x0003E502
		public ConventionSet(IEnumerable<IConvention> configurationConventions, IEnumerable<IConvention> entityModelConventions, IEnumerable<IConvention> dbMappingConventions, IEnumerable<IConvention> dbModelConventions)
		{
			this.ConfigurationConventions = configurationConventions;
			this.ConceptualModelConventions = entityModelConventions;
			this.ConceptualToStoreMappingConventions = dbMappingConventions;
			this.StoreModelConventions = dbModelConventions;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00040327 File Offset: 0x0003E527
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0004032F File Offset: 0x0003E52F
		public IEnumerable<IConvention> ConfigurationConventions { get; private set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00040338 File Offset: 0x0003E538
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x00040340 File Offset: 0x0003E540
		public IEnumerable<IConvention> ConceptualModelConventions { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00040349 File Offset: 0x0003E549
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00040351 File Offset: 0x0003E551
		public IEnumerable<IConvention> ConceptualToStoreMappingConventions { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0004035A File Offset: 0x0003E55A
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x00040362 File Offset: 0x0003E562
		public IEnumerable<IConvention> StoreModelConventions { get; private set; }
	}
}
