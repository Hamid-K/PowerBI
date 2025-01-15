using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000029 RID: 41
	public class EdmReferentialConstraintPropertyPair
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000031A9 File Offset: 0x000013A9
		public EdmReferentialConstraintPropertyPair(IEdmStructuralProperty dependentProperty, IEdmStructuralProperty principalProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(dependentProperty, "dependentProperty");
			EdmUtil.CheckArgumentNull<IEdmStructuralProperty>(principalProperty, "principalProperty");
			this.DependentProperty = dependentProperty;
			this.PrincipalProperty = principalProperty;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000031D7 File Offset: 0x000013D7
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000031DF File Offset: 0x000013DF
		public IEdmStructuralProperty DependentProperty { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000031E8 File Offset: 0x000013E8
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000031F0 File Offset: 0x000013F0
		public IEdmStructuralProperty PrincipalProperty { get; private set; }
	}
}
