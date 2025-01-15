using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200001D RID: 29
	internal class PropertyMetadataTypeInfo
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00003A24 File Offset: 0x00001C24
		public PropertyMetadataTypeInfo(IEdmModel model, string name, IEdmStructuredType owningType)
		{
			this.PropertyName = name;
			this.OwningType = owningType;
			this.EdmProperty = ((owningType == null) ? null : owningType.FindProperty(name));
			this.IsUndeclaredProperty = this.EdmProperty == null;
			this.IsOpenProperty = owningType != null && owningType.IsOpen && this.IsUndeclaredProperty;
			this.TypeReference = (this.IsUndeclaredProperty ? null : this.EdmProperty.Type);
			this.FullName = ((this.TypeReference == null) ? null : this.TypeReference.Definition.AsActualType().FullTypeName());
			this.model = model;
			this.derivedTypeConstraintsLoaded = false;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003AD1 File Offset: 0x00001CD1
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003AD9 File Offset: 0x00001CD9
		public string PropertyName { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003AE2 File Offset: 0x00001CE2
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003AEA File Offset: 0x00001CEA
		public IEdmProperty EdmProperty { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003AF3 File Offset: 0x00001CF3
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003AFB File Offset: 0x00001CFB
		public bool IsOpenProperty { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003B04 File Offset: 0x00001D04
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003B0C File Offset: 0x00001D0C
		public bool IsUndeclaredProperty { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003B15 File Offset: 0x00001D15
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003B1D File Offset: 0x00001D1D
		public IEdmStructuredType OwningType { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003B26 File Offset: 0x00001D26
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00003B2E File Offset: 0x00001D2E
		public IEdmTypeReference TypeReference { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003B37 File Offset: 0x00001D37
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00003B3F File Offset: 0x00001D3F
		public string FullName { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003B48 File Offset: 0x00001D48
		public IEnumerable<string> DerivedTypeConstraints
		{
			get
			{
				if (!this.derivedTypeConstraintsLoaded)
				{
					this.derivedTypeConstraints = ((this.EdmProperty == null) ? null : this.model.GetDerivedTypeConstraints(this.EdmProperty));
					this.derivedTypeConstraintsLoaded = true;
				}
				return this.derivedTypeConstraints;
			}
		}

		// Token: 0x0400004E RID: 78
		private bool derivedTypeConstraintsLoaded;

		// Token: 0x0400004F RID: 79
		private IEnumerable<string> derivedTypeConstraints;

		// Token: 0x04000050 RID: 80
		private IEdmModel model;
	}
}
