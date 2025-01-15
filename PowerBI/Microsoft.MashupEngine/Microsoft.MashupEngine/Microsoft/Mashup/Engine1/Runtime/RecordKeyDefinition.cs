using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015E5 RID: 5605
	internal struct RecordKeyDefinition
	{
		// Token: 0x06008CDC RID: 36060 RVA: 0x001D86A9 File Offset: 0x001D68A9
		internal RecordKeyDefinition(string key, IValueReference value, TypeValue type)
		{
			this.key = key;
			this.value = value;
			this.type = type;
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x06008CDD RID: 36061 RVA: 0x001D86C0 File Offset: 0x001D68C0
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x06008CDE RID: 36062 RVA: 0x001D86C8 File Offset: 0x001D68C8
		internal IValueReference Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x06008CDF RID: 36063 RVA: 0x001D86D0 File Offset: 0x001D68D0
		internal TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04004CD3 RID: 19667
		private readonly string key;

		// Token: 0x04004CD4 RID: 19668
		private readonly IValueReference value;

		// Token: 0x04004CD5 RID: 19669
		private readonly TypeValue type;
	}
}
