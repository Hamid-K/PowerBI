using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000052 RID: 82
	public class PseudoElementSelectorFactory : IPseudoElementSelectorFactory
	{
		// Token: 0x06000198 RID: 408 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		public void Register(string name, ISelector selector)
		{
			this._selectors.Add(name, selector);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		public ISelector Unregister(string name)
		{
			ISelector selector = null;
			if (this._selectors.TryGetValue(name, out selector))
			{
				this._selectors.Remove(name);
			}
			return selector;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C295 File Offset: 0x0000A495
		protected virtual ISelector CreateDefault(string name)
		{
			return null;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		public ISelector Create(string name)
		{
			ISelector selector = null;
			if (this._selectors.TryGetValue(name, out selector))
			{
				return selector;
			}
			return this.CreateDefault(name);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000CA34 File Offset: 0x0000AC34
		public PseudoElementSelectorFactory()
		{
			Dictionary<string, ISelector> dictionary = new Dictionary<string, ISelector>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(PseudoElementNames.Before, SimpleSelector.PseudoElement((IElement el) => el.IsPseudo("::" + PseudoElementNames.Before), PseudoElementNames.Before));
			dictionary.Add(PseudoElementNames.After, SimpleSelector.PseudoElement((IElement el) => el.IsPseudo("::" + PseudoElementNames.After), PseudoElementNames.After));
			dictionary.Add(PseudoElementNames.Selection, SimpleSelector.PseudoElement((IElement el) => false, PseudoElementNames.Selection));
			dictionary.Add(PseudoElementNames.FirstLine, SimpleSelector.PseudoElement((IElement el) => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine));
			dictionary.Add(PseudoElementNames.FirstLetter, SimpleSelector.PseudoElement((IElement el) => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter));
			dictionary.Add(PseudoElementNames.Content, SimpleSelector.PseudoElement((IElement el) => false, PseudoElementNames.Content));
			this._selectors = dictionary;
			base..ctor();
		}

		// Token: 0x040001D0 RID: 464
		private readonly Dictionary<string, ISelector> _selectors;
	}
}
