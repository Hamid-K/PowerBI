using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FA RID: 506
	internal abstract class CsdlNamedStructuredType : CsdlStructuredType
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002621D File Offset: 0x0002441D
		protected CsdlNamedStructuredType(string name, string baseTypeName, bool isAbstract, bool isOpen, IEnumerable<CsdlProperty> structuralproperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlLocation location)
			: base(structuralproperties, navigationProperties, location)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.name = name;
			this.baseTypeName = baseTypeName;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00026248 File Offset: 0x00024448
		public string BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00026250 File Offset: 0x00024450
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00026258 File Offset: 0x00024458
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00026260 File Offset: 0x00024460
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000789 RID: 1929
		protected string baseTypeName;

		// Token: 0x0400078A RID: 1930
		protected bool isAbstract;

		// Token: 0x0400078B RID: 1931
		protected bool isOpen;

		// Token: 0x0400078C RID: 1932
		protected string name;
	}
}
