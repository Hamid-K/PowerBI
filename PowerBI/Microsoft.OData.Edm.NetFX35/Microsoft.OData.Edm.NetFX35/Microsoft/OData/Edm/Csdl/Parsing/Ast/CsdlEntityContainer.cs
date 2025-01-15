using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017E RID: 382
	internal class CsdlEntityContainer : CsdlNamedElement
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00011817 File Offset: 0x0000FA17
		public CsdlEntityContainer(string name, string extends, IEnumerable<CsdlEntitySet> entitySets, IEnumerable<CsdlSingleton> singletons, IEnumerable<CsdlOperationImport> operationImports, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.extends = extends;
			this.entitySets = new List<CsdlEntitySet>(entitySets);
			this.singletons = new List<CsdlSingleton>(singletons);
			this.operationImports = new List<CsdlOperationImport>(operationImports);
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00011851 File Offset: 0x0000FA51
		public string Extends
		{
			get
			{
				return this.extends;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00011859 File Offset: 0x0000FA59
		public IEnumerable<CsdlEntitySet> EntitySets
		{
			get
			{
				return this.entitySets;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00011861 File Offset: 0x0000FA61
		public IEnumerable<CsdlSingleton> Singletons
		{
			get
			{
				return this.singletons;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00011869 File Offset: 0x0000FA69
		public IEnumerable<CsdlOperationImport> OperationImports
		{
			get
			{
				return this.operationImports;
			}
		}

		// Token: 0x040003BB RID: 955
		private readonly string extends;

		// Token: 0x040003BC RID: 956
		private readonly List<CsdlEntitySet> entitySets;

		// Token: 0x040003BD RID: 957
		private readonly List<CsdlSingleton> singletons;

		// Token: 0x040003BE RID: 958
		private readonly List<CsdlOperationImport> operationImports;
	}
}
