using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000201 RID: 513
	internal class CsdlPropertyReference : CsdlElement
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x00026473 File Offset: 0x00024673
		public CsdlPropertyReference(string propertyName, CsdlLocation location)
			: base(location)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00026483 File Offset: 0x00024683
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040007A0 RID: 1952
		private readonly string propertyName;
	}
}
