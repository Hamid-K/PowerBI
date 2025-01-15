using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp.Dom
{
	// Token: 0x02000162 RID: 354
	internal sealed class StyleSheetList : IStyleSheetList, IEnumerable<IStyleSheet>, IEnumerable
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x00045E80 File Offset: 0x00044080
		internal StyleSheetList(IEnumerable<IStyleSheet> sheets)
		{
			this._sheets = sheets;
		}

		// Token: 0x17000266 RID: 614
		public IStyleSheet this[int index]
		{
			get
			{
				return this._sheets.Skip(index).FirstOrDefault<IStyleSheet>();
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00045EA2 File Offset: 0x000440A2
		public int Length
		{
			get
			{
				return this._sheets.Count<IStyleSheet>();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00045EAF File Offset: 0x000440AF
		public IEnumerator<IStyleSheet> GetEnumerator()
		{
			return this._sheets.GetEnumerator();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00045EBC File Offset: 0x000440BC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400096C RID: 2412
		private readonly IEnumerable<IStyleSheet> _sheets;
	}
}
