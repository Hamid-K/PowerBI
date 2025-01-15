using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000CA RID: 202
	internal class PropertySerializationInfo
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00015AB6 File Offset: 0x00013CB6
		public PropertySerializationInfo(string name, IEdmStructuredType owningType)
		{
			this.PropertyName = name;
			this.IsTopLevel = false;
			this.MetadataType = new PropertyMetadataTypeInfo(name, owningType);
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00015AD9 File Offset: 0x00013CD9
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00015AE1 File Offset: 0x00013CE1
		public string PropertyName { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00015AEA File Offset: 0x00013CEA
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00015AF2 File Offset: 0x00013CF2
		public PropertyValueTypeInfo ValueType { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00015AFB File Offset: 0x00013CFB
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00015B03 File Offset: 0x00013D03
		public PropertyMetadataTypeInfo MetadataType { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00015B0C File Offset: 0x00013D0C
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00015B14 File Offset: 0x00013D14
		public string TypeNameToWrite { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00015B1D File Offset: 0x00013D1D
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00015B25 File Offset: 0x00013D25
		public string WireName { get; private set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00015B2E File Offset: 0x00013D2E
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00015B36 File Offset: 0x00013D36
		public bool IsTopLevel
		{
			get
			{
				return this.isTopLevel;
			}
			set
			{
				this.isTopLevel = value;
				this.WireName = (this.isTopLevel ? "value" : this.PropertyName);
			}
		}

		// Token: 0x04000322 RID: 802
		private bool isTopLevel;
	}
}
