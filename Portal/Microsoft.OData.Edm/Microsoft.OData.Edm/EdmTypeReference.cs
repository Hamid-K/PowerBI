using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C6 RID: 198
	public abstract class EdmTypeReference : EdmElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0000C033 File Offset: 0x0000A233
		protected EdmTypeReference(IEdmType definition, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(definition, "definition");
			this.definition = definition;
			this.isNullable = isNullable;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000C055 File Offset: 0x0000A255
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000C05D File Offset: 0x0000A25D
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000C065 File Offset: 0x0000A265
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x0400017A RID: 378
		private readonly IEdmType definition;

		// Token: 0x0400017B RID: 379
		private readonly bool isNullable;
	}
}
