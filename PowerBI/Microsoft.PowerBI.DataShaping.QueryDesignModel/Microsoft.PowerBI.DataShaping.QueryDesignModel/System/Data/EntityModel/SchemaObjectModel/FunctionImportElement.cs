using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002B RID: 43
	internal class FunctionImportElement : Function
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		internal FunctionImportElement(EntityContainer container)
			: base(container)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		public override bool IsFunctionImport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0000C7FF File Offset: 0x0000A9FF
		public override string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000C807 File Offset: 0x0000AA07
		public override string Identity
		{
			get
			{
				return base.Name;
			}
		}
	}
}
