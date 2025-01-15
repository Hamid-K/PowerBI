using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F4 RID: 500
	internal class CsdlPropertyReference : CsdlElement
	{
		// Token: 0x06000D2F RID: 3375 RVA: 0x0002433A File Offset: 0x0002253A
		public CsdlPropertyReference(string propertyName, CsdlLocation location)
			: base(location)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002434A File Offset: 0x0002254A
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0400072A RID: 1834
		private readonly string propertyName;
	}
}
