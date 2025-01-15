using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015CE RID: 5582
	public class Parameter
	{
		// Token: 0x06008C04 RID: 35844 RVA: 0x001D71A1 File Offset: 0x001D53A1
		public Parameter(string name)
			: this(name, TypeValue.Any)
		{
		}

		// Token: 0x06008C05 RID: 35845 RVA: 0x001D71AF File Offset: 0x001D53AF
		public Parameter(string name, TypeValue type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x06008C06 RID: 35846 RVA: 0x001D71C5 File Offset: 0x001D53C5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x06008C07 RID: 35847 RVA: 0x001D71CD File Offset: 0x001D53CD
		public TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04004CB1 RID: 19633
		private string name;

		// Token: 0x04004CB2 RID: 19634
		private TypeValue type;
	}
}
