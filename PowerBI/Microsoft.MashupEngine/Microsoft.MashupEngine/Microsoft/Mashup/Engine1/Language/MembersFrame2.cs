using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175D RID: 5981
	internal struct MembersFrame2
	{
		// Token: 0x060097F0 RID: 38896 RVA: 0x001F6800 File Offset: 0x001F4A00
		public MembersFrame2(MembersFunctionValue function, Value arg0, Value arg1)
		{
			this.function = function;
			this.arg0 = arg0;
			this.arg1 = arg1;
		}

		// Token: 0x060097F1 RID: 38897 RVA: 0x001F6817 File Offset: 0x001F4A17
		public Value Member(int index)
		{
			return this.function.Member(index);
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x060097F2 RID: 38898 RVA: 0x001F6825 File Offset: 0x001F4A25
		public Value Arg0
		{
			get
			{
				return this.arg0;
			}
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x060097F3 RID: 38899 RVA: 0x001F682D File Offset: 0x001F4A2D
		public Value Arg1
		{
			get
			{
				return this.arg1;
			}
		}

		// Token: 0x04005074 RID: 20596
		private readonly MembersFunctionValue function;

		// Token: 0x04005075 RID: 20597
		private readonly Value arg0;

		// Token: 0x04005076 RID: 20598
		private readonly Value arg1;
	}
}
