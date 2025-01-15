using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F1 RID: 497
	internal class CsdlEntityType : CsdlNamedStructuredType
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x0002604A File Offset: 0x0002424A
		public CsdlEntityType(string name, string baseTypeName, bool isAbstract, bool isOpen, bool hasStream, CsdlKey key, IEnumerable<CsdlProperty> structualProperties, IEnumerable<CsdlNavigationProperty> navigationProperties, CsdlLocation location)
			: base(name, baseTypeName, isAbstract, isOpen, structualProperties, navigationProperties, location)
		{
			this.key = key;
			this.hasStream = hasStream;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002606D File Offset: 0x0002426D
		public CsdlKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00026075 File Offset: 0x00024275
		public bool HasStream
		{
			get
			{
				return this.hasStream;
			}
		}

		// Token: 0x04000778 RID: 1912
		private readonly CsdlKey key;

		// Token: 0x04000779 RID: 1913
		private readonly bool hasStream;
	}
}
