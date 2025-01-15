using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175E RID: 5982
	internal struct MembersFrameN
	{
		// Token: 0x060097F4 RID: 38900 RVA: 0x001F6835 File Offset: 0x001F4A35
		public MembersFrameN(MembersFunctionValue function, Value[] args)
		{
			this.function = function;
			this.args = args;
		}

		// Token: 0x060097F5 RID: 38901 RVA: 0x001F6845 File Offset: 0x001F4A45
		public Value Member(int index)
		{
			return this.function.Member(index);
		}

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x060097F6 RID: 38902 RVA: 0x001F6853 File Offset: 0x001F4A53
		public Value Arg0
		{
			get
			{
				return this.args[0];
			}
		}

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x060097F7 RID: 38903 RVA: 0x001F685D File Offset: 0x001F4A5D
		public Value Arg1
		{
			get
			{
				return this.args[1];
			}
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x060097F8 RID: 38904 RVA: 0x001F6867 File Offset: 0x001F4A67
		public Value Arg2
		{
			get
			{
				return this.args[2];
			}
		}

		// Token: 0x060097F9 RID: 38905 RVA: 0x001F6871 File Offset: 0x001F4A71
		public Value Arg(int index)
		{
			return this.args[index];
		}

		// Token: 0x04005077 RID: 20599
		private readonly MembersFunctionValue function;

		// Token: 0x04005078 RID: 20600
		private readonly Value[] args;
	}
}
