using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001ED RID: 493
	internal abstract class CsdlNamedStructuredType : CsdlStructuredType
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x000240DB File Offset: 0x000222DB
		protected CsdlNamedStructuredType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> structuralproperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlDocumentation documentation, CsdlLocation location)
			: base(structuralproperties, navigationProperties, documentation, location)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.name = name;
			this.baseTypeName = baseTypeName;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00024108 File Offset: 0x00022308
		public string BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00024110 File Offset: 0x00022310
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00024118 File Offset: 0x00022318
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00024120 File Offset: 0x00022320
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000713 RID: 1811
		protected string baseTypeName;

		// Token: 0x04000714 RID: 1812
		protected bool isAbstract;

		// Token: 0x04000715 RID: 1813
		protected bool isOpen;

		// Token: 0x04000716 RID: 1814
		protected string name;
	}
}
