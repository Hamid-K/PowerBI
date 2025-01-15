using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000204 RID: 516
	internal class CsdlSchema : CsdlElement
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x000264EC File Offset: 0x000246EC
		public CsdlSchema(string namespaceName, string alias, Version version, IEnumerable<CsdlStructuredType> structuredTypes, IEnumerable<CsdlEnumType> enumTypes, IEnumerable<CsdlOperation> operations, IEnumerable<CsdlTerm> terms, IEnumerable<CsdlEntityContainer> entityContainers, IEnumerable<CsdlAnnotations> outOfLineAnnotations, IEnumerable<CsdlTypeDefinition> typeDefinitions, CsdlLocation location)
			: base(location)
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

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00026571 File Offset: 0x00024771
		public IEnumerable<CsdlStructuredType> StructuredTypes
		{
			get
			{
				return this.structuredTypes;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00026579 File Offset: 0x00024779
		public IEnumerable<CsdlEnumType> EnumTypes
		{
			get
			{
				return this.enumTypes;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00026581 File Offset: 0x00024781
		public IEnumerable<CsdlOperation> Operations
		{
			get
			{
				return this.operations;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00026589 File Offset: 0x00024789
		public IEnumerable<CsdlTerm> Terms
		{
			get
			{
				return this.terms;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00026591 File Offset: 0x00024791
		public IEnumerable<CsdlEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00026599 File Offset: 0x00024799
		public IEnumerable<CsdlAnnotations> OutOfLineAnnotations
		{
			get
			{
				return this.outOfLineAnnotations;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x000265A1 File Offset: 0x000247A1
		public IEnumerable<CsdlTypeDefinition> TypeDefinitions
		{
			get
			{
				return this.typeDefinitions;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x000265A9 File Offset: 0x000247A9
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x000265B1 File Offset: 0x000247B1
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x000265B9 File Offset: 0x000247B9
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x040007A5 RID: 1957
		private readonly List<CsdlStructuredType> structuredTypes;

		// Token: 0x040007A6 RID: 1958
		private readonly List<CsdlEnumType> enumTypes;

		// Token: 0x040007A7 RID: 1959
		private readonly List<CsdlOperation> operations;

		// Token: 0x040007A8 RID: 1960
		private readonly List<CsdlTerm> terms;

		// Token: 0x040007A9 RID: 1961
		private readonly List<CsdlEntityContainer> entityContainers;

		// Token: 0x040007AA RID: 1962
		private readonly List<CsdlAnnotations> outOfLineAnnotations;

		// Token: 0x040007AB RID: 1963
		private readonly List<CsdlTypeDefinition> typeDefinitions;

		// Token: 0x040007AC RID: 1964
		private readonly string alias;

		// Token: 0x040007AD RID: 1965
		private readonly string namespaceName;

		// Token: 0x040007AE RID: 1966
		private readonly Version version;
	}
}
