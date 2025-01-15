using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007C RID: 124
	public abstract class EdmTypeReference : EdmElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000C736 File Offset: 0x0000A936
		protected EdmTypeReference(IEdmType definition, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(definition, "definition");
			this.definition = definition;
			this.isNullable = isNullable;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000C758 File Offset: 0x0000A958
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000C760 File Offset: 0x0000A960
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000C768 File Offset: 0x0000A968
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x04000115 RID: 277
		private readonly IEdmType definition;

		// Token: 0x04000116 RID: 278
		private readonly bool isNullable;
	}
}
