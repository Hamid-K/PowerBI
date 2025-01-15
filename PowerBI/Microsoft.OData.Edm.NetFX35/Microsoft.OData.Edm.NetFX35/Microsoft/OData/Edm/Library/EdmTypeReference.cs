using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FE RID: 254
	public abstract class EdmTypeReference : EdmElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0000D262 File Offset: 0x0000B462
		protected EdmTypeReference(IEdmType definition, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(definition, "definition");
			this.definition = definition;
			this.isNullable = isNullable;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000D284 File Offset: 0x0000B484
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000D28C File Offset: 0x0000B48C
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000D294 File Offset: 0x0000B494
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x040001E1 RID: 481
		private readonly IEdmType definition;

		// Token: 0x040001E2 RID: 482
		private readonly bool isNullable;
	}
}
