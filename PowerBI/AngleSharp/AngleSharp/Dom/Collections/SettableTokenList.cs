using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000402 RID: 1026
	internal sealed class SettableTokenList : TokenList, ISettableTokenList, ITokenList, IEnumerable<string>, IEnumerable
	{
		// Token: 0x060020C6 RID: 8390 RVA: 0x00058034 File Offset: 0x00056234
		internal SettableTokenList(string value)
			: base(value)
		{
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0005803D File Offset: 0x0005623D
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00058045 File Offset: 0x00056245
		public string Value
		{
			get
			{
				return this.ToString();
			}
			set
			{
				base.Update(value);
			}
		}
	}
}
