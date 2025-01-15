using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EE RID: 494
	internal class CsdlEntityContainer : CsdlNamedElement
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x00025F98 File Offset: 0x00024198
		public CsdlEntityContainer(string name, string extends, IEnumerable<CsdlEntitySet> entitySets, IEnumerable<CsdlSingleton> singletons, IEnumerable<CsdlOperationImport> operationImports, CsdlLocation location)
			: base(name, location)
		{
			this.extends = extends;
			this.entitySets = new List<CsdlEntitySet>(entitySets);
			this.singletons = new List<CsdlSingleton>(singletons);
			this.operationImports = new List<CsdlOperationImport>(operationImports);
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00025FD0 File Offset: 0x000241D0
		public string Extends
		{
			get
			{
				return this.extends;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00025FD8 File Offset: 0x000241D8
		public IEnumerable<CsdlEntitySet> EntitySets
		{
			get
			{
				return this.entitySets;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00025FE0 File Offset: 0x000241E0
		public IEnumerable<CsdlSingleton> Singletons
		{
			get
			{
				return this.singletons;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00025FE8 File Offset: 0x000241E8
		public IEnumerable<CsdlOperationImport> OperationImports
		{
			get
			{
				return this.operationImports;
			}
		}

		// Token: 0x04000771 RID: 1905
		private readonly string extends;

		// Token: 0x04000772 RID: 1906
		private readonly List<CsdlEntitySet> entitySets;

		// Token: 0x04000773 RID: 1907
		private readonly List<CsdlSingleton> singletons;

		// Token: 0x04000774 RID: 1908
		private readonly List<CsdlOperationImport> operationImports;
	}
}
