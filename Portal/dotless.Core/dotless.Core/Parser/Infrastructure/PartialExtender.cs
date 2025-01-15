using System;
using System.Collections.Generic;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000053 RID: 83
	public class PartialExtender : Extender
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000F847 File Offset: 0x0000DA47
		[Obsolete("Use the overload that accepts the Extend node")]
		public PartialExtender(Selector baseSelector)
			: this(baseSelector, null)
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000F851 File Offset: 0x0000DA51
		public PartialExtender(Selector baseSelector, Extend extend)
			: base(baseSelector, extend)
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000F85B File Offset: 0x0000DA5B
		public IEnumerable<Selector> Replacements(string selection)
		{
			foreach (Selector selector in base.ExtendedBy)
			{
				yield return new Selector(new Element[]
				{
					new Element(null, selection.Replace(base.BaseSelector.ToString().Trim(), selector.ToString().Trim()))
				});
			}
			List<Selector>.Enumerator enumerator = default(List<Selector>.Enumerator);
			yield break;
			yield break;
		}
	}
}
