using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200001E RID: 30
	internal class PropertySerializationInfo
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00003B81 File Offset: 0x00001D81
		public PropertySerializationInfo(IEdmModel model, string name, IEdmStructuredType owningType)
		{
			this.PropertyName = name;
			this.IsTopLevel = false;
			this.MetadataType = new PropertyMetadataTypeInfo(model, name, owningType);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003BA5 File Offset: 0x00001DA5
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00003BAD File Offset: 0x00001DAD
		public string PropertyName { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003BB6 File Offset: 0x00001DB6
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003BBE File Offset: 0x00001DBE
		public PropertyValueTypeInfo ValueType { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003BC7 File Offset: 0x00001DC7
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00003BCF File Offset: 0x00001DCF
		public PropertyMetadataTypeInfo MetadataType { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003BD8 File Offset: 0x00001DD8
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public string TypeNameToWrite { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003BE9 File Offset: 0x00001DE9
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public string WireName { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003BFA File Offset: 0x00001DFA
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00003C02 File Offset: 0x00001E02
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

		// Token: 0x04000058 RID: 88
		private bool isTopLevel;
	}
}
