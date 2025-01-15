using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F7 RID: 503
	internal class CsdlSchema : CsdlElementWithDocumentation
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x000243B8 File Offset: 0x000225B8
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

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002443F File Offset: 0x0002263F
		public IEnumerable<CsdlStructuredType> StructuredTypes
		{
			get
			{
				return this.structuredTypes;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00024447 File Offset: 0x00022647
		public IEnumerable<CsdlEnumType> EnumTypes
		{
			get
			{
				return this.enumTypes;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0002444F File Offset: 0x0002264F
		public IEnumerable<CsdlOperation> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00024457 File Offset: 0x00022657
		public IEnumerable<CsdlTerm> Terms
		{
			get
			{
				return this.terms;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0002445F File Offset: 0x0002265F
		public IEnumerable<CsdlEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00024467 File Offset: 0x00022667
		public IEnumerable<CsdlAnnotations> OutOfLineAnnotations
		{
			get
			{
				return this.outOfLineAnnotations;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0002446F File Offset: 0x0002266F
		public IEnumerable<CsdlTypeDefinition> TypeDefinitions
		{
			get
			{
				return this.typeDefinitions;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00024477 File Offset: 0x00022677
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0002447F File Offset: 0x0002267F
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00024487 File Offset: 0x00022687
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x0400072F RID: 1839
		private readonly List<CsdlStructuredType> structuredTypes;

		// Token: 0x04000730 RID: 1840
		private readonly List<CsdlEnumType> enumTypes;

		// Token: 0x04000731 RID: 1841
		private readonly List<CsdlOperation> operations;

		// Token: 0x04000732 RID: 1842
		private readonly List<CsdlTerm> terms;

		// Token: 0x04000733 RID: 1843
		private readonly List<CsdlEntityContainer> entityContainers;

		// Token: 0x04000734 RID: 1844
		private readonly List<CsdlAnnotations> outOfLineAnnotations;

		// Token: 0x04000735 RID: 1845
		private readonly List<CsdlTypeDefinition> typeDefinitions;

		// Token: 0x04000736 RID: 1846
		private readonly string alias;

		// Token: 0x04000737 RID: 1847
		private readonly string namespaceName;

		// Token: 0x04000738 RID: 1848
		private readonly Version version;
	}
}
