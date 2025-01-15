using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200175C RID: 5980
	internal struct MembersFrame1
	{
		// Token: 0x060097ED RID: 38893 RVA: 0x001F67DA File Offset: 0x001F49DA
		public MembersFrame1(MembersFunctionValue function, Value arg0)
		{
			this.function = function;
			this.arg0 = arg0;
		}

		// Token: 0x060097EE RID: 38894 RVA: 0x001F67EA File Offset: 0x001F49EA
		public Value Member(int index)
		{
			return this.function.Member(index);
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x060097EF RID: 38895 RVA: 0x001F67F8 File Offset: 0x001F49F8
		public Value Arg0
		{
			get
			{
				return this.arg0;
			}
		}

		// Token: 0x04005072 RID: 20594
		private readonly MembersFunctionValue function;

		// Token: 0x04005073 RID: 20595
		private readonly Value arg0;
	}
}
