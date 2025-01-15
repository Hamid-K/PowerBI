using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200018D RID: 397
	internal class CsdlSchema : CsdlElementWithDocumentation
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x00011B10 File Offset: 0x0000FD10
		public CsdlSchema(string namespaceName, string alias, Version version, IEnumerable<CsdlStructuredType> structuredTypes, IEnumerable<CsdlEnumType> enumTypes, IEnumerable<CsdlOperation> operations, IEnumerable<CsdlTerm> terms, IEnumerable<CsdlEntityContainer> entityContainers, IEnumerable<CsdlAnnotations> outOfLineAnnotations, IEnumerable<CsdlTypeDefinition> typeDefinitions, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.alias = alias;
			this.namespaceName = namespaceName;
			this.version = version;
			this.structuredTypes = new List<CsdlStructuredType>(structuredTypes);
			this.enumTypes = new List<CsdlEnumType>(enumTypes);
			this.operations = new List<CsdlOperation>(operations);
			this.terms = new List<CsdlTerm>(terms);
			this.entityContainers = new List<CsdlEntityContainer>(entityContainers);
			this.outOfLineAnnotations = new List<CsdlAnnotations>(outOfLineAnnotations);
			this.typeDefinitions = new List<CsdlTypeDefinition>(typeDefinitions);
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00011B97 File Offset: 0x0000FD97
		public IEnumerable<CsdlStructuredType> StructuredTypes
		{
			get
			{
				return this.structuredTypes;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00011B9F File Offset: 0x0000FD9F
		public IEnumerable<CsdlEnumType> EnumTypes
		{
			get
			{
				return this.enumTypes;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00011BA7 File Offset: 0x0000FDA7
		public IEnumerable<CsdlOperation> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00011BAF File Offset: 0x0000FDAF
		public IEnumerable<CsdlTerm> Terms
		{
			get
			{
				return this.terms;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00011BB7 File Offset: 0x0000FDB7
		public IEnumerable<CsdlEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00011BBF File Offset: 0x0000FDBF
		public IEnumerable<CsdlAnnotations> OutOfLineAnnotations
		{
			get
			{
				return this.outOfLineAnnotations;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00011BC7 File Offset: 0x0000FDC7
		public IEnumerable<CsdlTypeDefinition> TypeDefinitions
		{
			get
			{
				return this.typeDefinitions;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00011BCF File Offset: 0x0000FDCF
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00011BD7 File Offset: 0x0000FDD7
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00011BDF File Offset: 0x0000FDDF
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x040003DB RID: 987
		private readonly List<CsdlStructuredType> structuredTypes;

		// Token: 0x040003DC RID: 988
		private readonly List<CsdlEnumType> enumTypes;

		// Token: 0x040003DD RID: 989
		private readonly List<CsdlOperation> operations;

		// Token: 0x040003DE RID: 990
		private readonly List<CsdlTerm> terms;

		// Token: 0x040003DF RID: 991
		private readonly List<CsdlEntityContainer> entityContainers;

		// Token: 0x040003E0 RID: 992
		private readonly List<CsdlAnnotations> outOfLineAnnotations;

		// Token: 0x040003E1 RID: 993
		private readonly List<CsdlTypeDefinition> typeDefinitions;

		// Token: 0x040003E2 RID: 994
		private readonly string alias;

		// Token: 0x040003E3 RID: 995
		private readonly string namespaceName;

		// Token: 0x040003E4 RID: 996
		private readonly Version version;
	}
}
