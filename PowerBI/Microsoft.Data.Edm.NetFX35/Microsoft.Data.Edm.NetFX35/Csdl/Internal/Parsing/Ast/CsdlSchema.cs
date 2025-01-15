using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200014B RID: 331
	internal class CsdlSchema : CsdlElementWithDocumentation
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0000F84C File Offset: 0x0000DA4C
		public CsdlSchema(string namespaceName, string alias, Version version, IEnumerable<CsdlUsing> usings, IEnumerable<CsdlAssociation> associations, IEnumerable<CsdlStructuredType> structuredTypes, IEnumerable<CsdlEnumType> enumTypes, IEnumerable<CsdlFunction> functions, IEnumerable<CsdlValueTerm> valueTerms, IEnumerable<CsdlEntityContainer> entityContainers, IEnumerable<CsdlAnnotations> outOfLineAnnotations, CsdlDocumentation documentation, CsdlLocation location)
			: base(documentation, location)
		{
			this.alias = alias;
			this.namespaceName = namespaceName;
			this.version = version;
			this.usings = new List<CsdlUsing>(usings);
			this.associations = new List<CsdlAssociation>(associations);
			this.structuredTypes = new List<CsdlStructuredType>(structuredTypes);
			this.enumTypes = new List<CsdlEnumType>(enumTypes);
			this.functions = new List<CsdlFunction>(functions);
			this.valueTerms = new List<CsdlValueTerm>(valueTerms);
			this.entityContainers = new List<CsdlEntityContainer>(entityContainers);
			this.outOfLineAnnotations = new List<CsdlAnnotations>(outOfLineAnnotations);
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public IEnumerable<CsdlUsing> Usings
		{
			get
			{
				return this.usings;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public IEnumerable<CsdlAssociation> Associations
		{
			get
			{
				return this.associations;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public IEnumerable<CsdlStructuredType> StructuredTypes
		{
			get
			{
				return this.structuredTypes;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public IEnumerable<CsdlEnumType> EnumTypes
		{
			get
			{
				return this.enumTypes;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000F900 File Offset: 0x0000DB00
		public IEnumerable<CsdlFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000F908 File Offset: 0x0000DB08
		public IEnumerable<CsdlValueTerm> ValueTerms
		{
			get
			{
				return this.valueTerms;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000F910 File Offset: 0x0000DB10
		public IEnumerable<CsdlEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000F918 File Offset: 0x0000DB18
		public IEnumerable<CsdlAnnotations> OutOfLineAnnotations
		{
			get
			{
				return this.outOfLineAnnotations;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000F920 File Offset: 0x0000DB20
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000F928 File Offset: 0x0000DB28
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000F930 File Offset: 0x0000DB30
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04000348 RID: 840
		private readonly List<CsdlUsing> usings;

		// Token: 0x04000349 RID: 841
		private readonly List<CsdlAssociation> associations;

		// Token: 0x0400034A RID: 842
		private readonly List<CsdlStructuredType> structuredTypes;

		// Token: 0x0400034B RID: 843
		private readonly List<CsdlEnumType> enumTypes;

		// Token: 0x0400034C RID: 844
		private readonly List<CsdlFunction> functions;

		// Token: 0x0400034D RID: 845
		private readonly List<CsdlValueTerm> valueTerms;

		// Token: 0x0400034E RID: 846
		private readonly List<CsdlEntityContainer> entityContainers;

		// Token: 0x0400034F RID: 847
		private readonly List<CsdlAnnotations> outOfLineAnnotations;

		// Token: 0x04000350 RID: 848
		private readonly string alias;

		// Token: 0x04000351 RID: 849
		private readonly string namespaceName;

		// Token: 0x04000352 RID: 850
		private readonly Version version;
	}
}
