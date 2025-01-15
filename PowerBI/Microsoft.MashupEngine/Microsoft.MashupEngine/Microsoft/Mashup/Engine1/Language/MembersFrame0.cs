using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175B RID: 5979
	internal struct MembersFrame0
	{
		// Token: 0x060097EB RID: 38891 RVA: 0x001F67C3 File Offset: 0x001F49C3
		public MembersFrame0(MembersFunctionValue function)
		{
			this.function = function;
		}

		// Token: 0x060097EC RID: 38892 RVA: 0x001F67CC File Offset: 0x001F49CC
		public Value Member(int index)
		{
			return this.function.Member(index);
		}

		// Token: 0x04005071 RID: 20593
		private readonly MembersFunctionValue function;
	}
}
