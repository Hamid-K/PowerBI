using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001584 RID: 5508
	public struct NamedValue
	{
		// Token: 0x06008936 RID: 35126 RVA: 0x001D08EF File Offset: 0x001CEAEF
		public NamedValue(string key, Value value)
		{
			this.key = key;
			this.value = value;
		}

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06008937 RID: 35127 RVA: 0x001D08FF File Offset: 0x001CEAFF
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06008938 RID: 35128 RVA: 0x001D0907 File Offset: 0x001CEB07
		public Value Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04004BC3 RID: 19395
		private readonly string key;

		// Token: 0x04004BC4 RID: 19396
		private readonly Value value;
	}
}
