using System;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000052 RID: 82
	public class ExactExtender : Extender
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000F833 File Offset: 0x0000DA33
		[Obsolete("Use the overload that accepts the Extend node")]
		public ExactExtender(Selector baseSelector)
			: this(baseSelector, null)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000F83D File Offset: 0x0000DA3D
		public ExactExtender(Selector baseSelector, Extend extend)
			: base(baseSelector, extend)
		{
		}
	}
}
