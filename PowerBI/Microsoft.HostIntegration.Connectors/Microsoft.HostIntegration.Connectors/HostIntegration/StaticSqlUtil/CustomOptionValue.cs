using System;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A74 RID: 2676
	public class CustomOptionValue
	{
		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x00150A1A File Offset: 0x0014EC1A
		// (set) Token: 0x060052F9 RID: 21241 RVA: 0x00150A22 File Offset: 0x0014EC22
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x00150A2B File Offset: 0x0014EC2B
		// (set) Token: 0x060052FB RID: 21243 RVA: 0x00150A33 File Offset: 0x0014EC33
		public string XmlName
		{
			get
			{
				return this.xmlName;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x040041D0 RID: 16848
		private int value;

		// Token: 0x040041D1 RID: 16849
		private string xmlName;
	}
}
