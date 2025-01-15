using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000F4 RID: 244
	public class EdmReferentialConstraintPropertyPair
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000CB53 File Offset: 0x0000AD53
		public EdmReferentialConstraintPropertyPair(IEdmStructuralProperty dependentProperty, IEdmStructuralProperty principalProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(dependentProperty, "dependentProperty");
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(principalProperty, "principalProperty");
			this.DependentProperty = dependentProperty;
			this.PrincipalProperty = principalProperty;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000CB81 File Offset: 0x0000AD81
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000CB89 File Offset: 0x0000AD89
		public IEdmStructuralProperty DependentProperty { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000CB92 File Offset: 0x0000AD92
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000CB9A File Offset: 0x0000AD9A
		public IEdmStructuralProperty PrincipalProperty { get; private set; }
	}
}
