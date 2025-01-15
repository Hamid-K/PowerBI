using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000C9 RID: 201
	internal class PropertyMetadataTypeInfo
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x000159A0 File Offset: 0x00013BA0
		public PropertyMetadataTypeInfo(string name, IEdmStructuredType owningType)
		{
			this.PropertyName = name;
			this.OwningType = owningType;
			this.EdmProperty = ((owningType == null) ? null : owningType.FindProperty(name));
			this.IsUndeclaredProperty = this.EdmProperty == null;
			this.IsOpenProperty = owningType != null && owningType.IsOpen && this.IsUndeclaredProperty;
			this.TypeReference = (this.IsUndeclaredProperty ? null : this.EdmProperty.Type);
			this.FullName = ((this.TypeReference == null) ? null : this.TypeReference.Definition.AsActualType().FullTypeName());
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00015A3F File Offset: 0x00013C3F
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00015A47 File Offset: 0x00013C47
		public string PropertyName { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00015A50 File Offset: 0x00013C50
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00015A58 File Offset: 0x00013C58
		public IEdmProperty EdmProperty { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00015A61 File Offset: 0x00013C61
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00015A69 File Offset: 0x00013C69
		public bool IsOpenProperty { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00015A72 File Offset: 0x00013C72
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00015A7A File Offset: 0x00013C7A
		public bool IsUndeclaredProperty { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00015A83 File Offset: 0x00013C83
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00015A8B File Offset: 0x00013C8B
		public IEdmStructuredType OwningType { get; private set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00015A94 File Offset: 0x00013C94
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00015A9C File Offset: 0x00013C9C
		public IEdmTypeReference TypeReference { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00015AA5 File Offset: 0x00013CA5
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00015AAD File Offset: 0x00013CAD
		public string FullName { get; private set; }
	}
}
