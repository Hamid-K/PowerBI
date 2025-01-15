using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000054 RID: 84
	internal static class ExtenderMatcherExtensions
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000F874 File Offset: 0x0000DA74
		internal static IEnumerable<PartialExtender> WhereExtenderMatches(this IEnumerable<PartialExtender> extenders, Context selection)
		{
			List<Element> selectionElements = selection.SelectMany((IEnumerable<Selector> selectors) => selectors.SelectMany((Selector s) => s.Elements)).ToList<Element>();
			return extenders.Where((PartialExtender e) => e.ElementListMatches(selectionElements));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000F8CC File Offset: 0x0000DACC
		private static bool ElementListMatches(this PartialExtender extender, IList<Element> list)
		{
			int count = extender.BaseSelector.Elements.Count;
			return extender.BaseSelector.Elements.IsSubsequenceOf(list, delegate(int subIndex, Element subElement, int index, Element seqelement)
			{
				if (subIndex < count - 1)
				{
					return object.Equals(subElement.Combinator, seqelement.Combinator) && string.Equals(subElement.Value, seqelement.Value) && object.Equals(subElement.NodeValue, seqelement.NodeValue);
				}
				return string.Equals(subElement.Value, seqelement.Value) && object.Equals(subElement.NodeValue, seqelement.NodeValue);
			});
		}
	}
}
