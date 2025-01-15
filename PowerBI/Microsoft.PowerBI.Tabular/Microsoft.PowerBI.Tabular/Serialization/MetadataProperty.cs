using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000183 RID: 387
	internal struct MetadataProperty
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x000A31AE File Offset: 0x000A13AE
		public MetadataProperty(string name, object value)
		{
			this.Name = name;
			this.Nature = MetadataPropertyNature.None;
			this.ValueType = value.GetType();
			this.Value = value;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000A31D1 File Offset: 0x000A13D1
		public MetadataProperty(string name, MetadataPropertyNature nature, Type valueType, object value)
		{
			this.Name = name;
			this.Nature = nature;
			this.ValueType = valueType;
			this.Value = value;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000A31F0 File Offset: 0x000A13F0
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.Name);
			}
		}

		// Token: 0x04000467 RID: 1127
		public readonly string Name;

		// Token: 0x04000468 RID: 1128
		public readonly MetadataPropertyNature Nature;

		// Token: 0x04000469 RID: 1129
		public readonly Type ValueType;

		// Token: 0x0400046A RID: 1130
		public readonly object Value;
	}
}
