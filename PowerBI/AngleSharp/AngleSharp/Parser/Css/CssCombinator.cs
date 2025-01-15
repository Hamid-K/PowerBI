using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000078 RID: 120
	internal abstract class CssCombinator
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00018A23 File Offset: 0x00016C23
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00018A2B File Offset: 0x00016C2B
		public Func<IElement, IEnumerable<IElement>> Transform { get; protected set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00018A34 File Offset: 0x00016C34
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00018A3C File Offset: 0x00016C3C
		public string Delimiter { get; protected set; }

		// Token: 0x06000384 RID: 900 RVA: 0x00018A45 File Offset: 0x00016C45
		public virtual ISelector Change(ISelector selector)
		{
			return selector;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00018A48 File Offset: 0x00016C48
		protected static IEnumerable<IElement> Single(IElement element)
		{
			if (element != null)
			{
				yield return element;
			}
			yield break;
		}

		// Token: 0x040002D5 RID: 725
		public static readonly CssCombinator Child = new CssCombinator.ChildCombinator();

		// Token: 0x040002D6 RID: 726
		public static readonly CssCombinator Deep = new CssCombinator.DeepCombinator();

		// Token: 0x040002D7 RID: 727
		public static readonly CssCombinator Descendent = new CssCombinator.DescendentCombinator();

		// Token: 0x040002D8 RID: 728
		public static readonly CssCombinator AdjacentSibling = new CssCombinator.AdjacentSiblingCombinator();

		// Token: 0x040002D9 RID: 729
		public static readonly CssCombinator Sibling = new CssCombinator.SiblingCombinator();

		// Token: 0x040002DA RID: 730
		public static readonly CssCombinator Namespace = new CssCombinator.NamespaceCombinator();

		// Token: 0x040002DB RID: 731
		public static readonly CssCombinator Column = new CssCombinator.ColumnCombinator();

		// Token: 0x02000444 RID: 1092
		private sealed class ChildCombinator : CssCombinator
		{
			// Token: 0x06002340 RID: 9024 RVA: 0x0005B092 File Offset: 0x00059292
			public ChildCombinator()
			{
				base.Delimiter = CombinatorSymbols.Child;
				base.Transform = (IElement el) => CssCombinator.Single(el.ParentElement);
			}
		}

		// Token: 0x02000445 RID: 1093
		private sealed class DeepCombinator : CssCombinator
		{
			// Token: 0x06002341 RID: 9025 RVA: 0x0005B0CA File Offset: 0x000592CA
			public DeepCombinator()
			{
				base.Delimiter = CombinatorSymbols.Deep;
				base.Transform = (IElement el) => CssCombinator.Single((el.Parent is IShadowRoot) ? ((IShadowRoot)el.Parent).Host : null);
			}
		}

		// Token: 0x02000446 RID: 1094
		private sealed class DescendentCombinator : CssCombinator
		{
			// Token: 0x06002342 RID: 9026 RVA: 0x0005B102 File Offset: 0x00059302
			public DescendentCombinator()
			{
				base.Delimiter = CombinatorSymbols.Descendent;
				base.Transform = delegate(IElement el)
				{
					List<IElement> list = new List<IElement>();
					for (IElement element = el.ParentElement; element != null; element = element.ParentElement)
					{
						list.Add(element);
					}
					return list;
				};
			}
		}

		// Token: 0x02000447 RID: 1095
		private sealed class AdjacentSiblingCombinator : CssCombinator
		{
			// Token: 0x06002343 RID: 9027 RVA: 0x0005B13A File Offset: 0x0005933A
			public AdjacentSiblingCombinator()
			{
				base.Delimiter = CombinatorSymbols.Adjacent;
				base.Transform = (IElement el) => CssCombinator.Single(el.PreviousElementSibling);
			}
		}

		// Token: 0x02000448 RID: 1096
		private sealed class SiblingCombinator : CssCombinator
		{
			// Token: 0x06002344 RID: 9028 RVA: 0x0005B172 File Offset: 0x00059372
			public SiblingCombinator()
			{
				base.Delimiter = CombinatorSymbols.Sibling;
				base.Transform = delegate(IElement el)
				{
					IElement parentElement = el.ParentElement;
					if (parentElement != null)
					{
						List<IElement> list = new List<IElement>();
						foreach (INode node in parentElement.ChildNodes)
						{
							IElement element = node as IElement;
							if (element != null)
							{
								if (element == el)
								{
									break;
								}
								list.Add(element);
							}
						}
						return list;
					}
					return new IElement[0];
				};
			}
		}

		// Token: 0x02000449 RID: 1097
		private sealed class NamespaceCombinator : CssCombinator
		{
			// Token: 0x06002345 RID: 9029 RVA: 0x0005B1AA File Offset: 0x000593AA
			public NamespaceCombinator()
			{
				base.Delimiter = CombinatorSymbols.Pipe;
				base.Transform = (IElement el) => CssCombinator.Single(el);
			}

			// Token: 0x06002346 RID: 9030 RVA: 0x0005B1E4 File Offset: 0x000593E4
			public override ISelector Change(ISelector selector)
			{
				string prefix = selector.Text;
				return new SimpleSelector((IElement el) => el.MatchesCssNamespace(prefix), Priority.Zero, prefix);
			}
		}

		// Token: 0x0200044A RID: 1098
		private sealed class ColumnCombinator : CssCombinator
		{
			// Token: 0x06002347 RID: 9031 RVA: 0x0005B21F File Offset: 0x0005941F
			public ColumnCombinator()
			{
				base.Delimiter = CombinatorSymbols.Column;
			}
		}
	}
}
