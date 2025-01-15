using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E1 RID: 481
	internal class CsdlEntityContainer : CsdlNamedElement
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x00023E36 File Offset: 0x00022036
		public CsdlEntityContainer(string name, string extends, IEnumerable<CsdlEntitySet> entitySets, IEnumerable<CsdlSingleton> singletons, IEnumerable<CsdlOperationImport> operationImports, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.extends = extends;
			this.entitySets = new List<CsdlEntitySet>(entitySets);
			this.singletons = new List<CsdlSingleton>(singletons);
			this.operationImports = new List<CsdlOperationImport>(operationImports);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00023E70 File Offset: 0x00022070
		public string Extends
		{
			get
			{
				return this.extends;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00023E78 File Offset: 0x00022078
		public IEnumerable<CsdlEntitySet> EntitySets
		{
			get
			{
				return this.entitySets;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00023E80 File Offset: 0x00022080
		public IEnumerable<CsdlSingleton> Singletons
		{
			get
			{
				return this.singletons;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00023E88 File Offset: 0x00022088
		public IEnumerable<CsdlOperationImport> OperationImports
		{
			get
			{
				return this.operationImports;
			}
		}

		// Token: 0x040006FB RID: 1787
		private readonly string extends;

		// Token: 0x040006FC RID: 1788
		private readonly List<CsdlEntitySet> entitySets;

		// Token: 0x040006FD RID: 1789
		private readonly List<CsdlSingleton> singletons;

		// Token: 0x040006FE RID: 1790
		private readonly List<CsdlOperationImport> operationImports;
	}
}
