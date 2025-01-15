using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020000E5 RID: 229
	public abstract class EdmTypeReference : EdmElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000C276 File Offset: 0x0000A476
		protected EdmTypeReference(IEdmType definition, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(definition, "definition");
			this.definition = definition;
			this.isNullable = isNullable;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000C298 File Offset: 0x0000A498
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x040001BA RID: 442
		private readonly IEdmType definition;

		// Token: 0x040001BB RID: 443
		private readonly bool isNullable;
	}
}
