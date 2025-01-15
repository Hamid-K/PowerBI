using System;
using System.ComponentModel;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav
{
	// Token: 0x02000024 RID: 36
	[ImmutableObject(true)]
	internal struct Range : IRange
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00005F24 File Offset: 0x00004124
		internal Range(int firstIndex, int lastIndex)
		{
			this._firstIndex = firstIndex;
			this._lastIndex = lastIndex;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005F34 File Offset: 0x00004134
		public int FirstIndex
		{
			get
			{
				return this._firstIndex;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005F3C File Offset: 0x0000413C
		public int LastIndex
		{
			get
			{
				return this._lastIndex;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005F44 File Offset: 0x00004144
		public override string ToString()
		{
			return StringUtil.FormatInvariant("[{0},{1}]", this.FirstIndex, this.LastIndex);
		}

		// Token: 0x0400005D RID: 93
		private readonly int _firstIndex;

		// Token: 0x0400005E RID: 94
		private readonly int _lastIndex;
	}
}
