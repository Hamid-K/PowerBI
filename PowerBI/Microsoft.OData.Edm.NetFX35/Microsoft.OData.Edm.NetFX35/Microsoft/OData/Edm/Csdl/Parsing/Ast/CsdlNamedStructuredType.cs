using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200017A RID: 378
	internal abstract class CsdlNamedStructuredType : CsdlStructuredType
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x00011766 File Offset: 0x0000F966
		protected CsdlNamedStructuredType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> properties, CsdlDocumentation documentation, CsdlLocation location)
			: base(properties, documentation, location)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.name = name;
			this.baseTypeName = baseTypeName;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00011791 File Offset: 0x0000F991
		public string BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00011799 File Offset: 0x0000F999
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x000117A1 File Offset: 0x0000F9A1
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000117A9 File Offset: 0x0000F9A9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040003B3 RID: 947
		protected string baseTypeName;

		// Token: 0x040003B4 RID: 948
		protected bool isAbstract;

		// Token: 0x040003B5 RID: 949
		protected bool isOpen;

		// Token: 0x040003B6 RID: 950
		protected string name;
	}
}
