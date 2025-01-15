using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B7 RID: 183
	public class EdmReferentialConstraintPropertyPair
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0000C883 File Offset: 0x0000AA83
		public EdmReferentialConstraintPropertyPair(IEdmStructuralProperty dependentProperty, IEdmStructuralProperty principalProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(dependentProperty, "dependentProperty");
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(principalProperty, "principalProperty");
			this.DependentProperty = dependentProperty;
			this.PrincipalProperty = principalProperty;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000C8B9 File Offset: 0x0000AAB9
		public IEdmStructuralProperty DependentProperty { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000C8C2 File Offset: 0x0000AAC2
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000C8CA File Offset: 0x0000AACA
		public IEdmStructuralProperty PrincipalProperty { get; private set; }
	}
}
