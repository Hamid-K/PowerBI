using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000039 RID: 57
	internal abstract class Property : SchemaElement
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x0000D67B File Offset: 0x0000B87B
		internal Property(StructuredType parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600071C RID: 1820
		public abstract SchemaType Type { get; }
	}
}
