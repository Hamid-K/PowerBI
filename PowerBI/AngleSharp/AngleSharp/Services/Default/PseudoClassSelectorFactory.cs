using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000051 RID: 81
	public class PseudoClassSelectorFactory : IPseudoClassSelectorFactory
	{
		// Token: 0x06000193 RID: 403 RVA: 0x0000C257 File Offset: 0x0000A457
		public void Register(string name, ISelector selector)
		{
			this._selectors.Add(name, selector);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C268 File Offset: 0x0000A468
		public ISelector Unregister(string name)
		{
			ISelector selector = null;
			if (this._selectors.TryGetValue(name, out selector))
			{
				this._selectors.Remove(name);
			}
			return selector;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C295 File Offset: 0x0000A495
		protected virtual ISelector CreateDefault(string name)
		{
			return null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C298 File Offset: 0x0000A498
		public ISelector Create(string name)
		{
			ISelector selector = null;
			if (this._selectors.TryGetValue(name, out selector))
			{
				return selector;
			}
			return this.CreateDefault(name);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public PseudoClassSelectorFactory()
		{
			Dictionary<string, ISelector> dictionary = new Dictionary<string, ISelector>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(PseudoClassNames.Root, SimpleSelector.PseudoClass((IElement el) => el.Owner.DocumentElement == el, PseudoClassNames.Root));
			dictionary.Add(PseudoClassNames.Scope, SimpleSelector.PseudoClass((IElement el) => el.Owner.DocumentElement == el, PseudoClassNames.Scope));
			dictionary.Add(PseudoClassNames.OnlyType, SimpleSelector.PseudoClass((IElement el) => el.IsOnlyOfType(), PseudoClassNames.OnlyType));
			dictionary.Add(PseudoClassNames.FirstOfType, SimpleSelector.PseudoClass((IElement el) => el.IsFirstOfType(), PseudoClassNames.FirstOfType));
			dictionary.Add(PseudoClassNames.LastOfType, SimpleSelector.PseudoClass((IElement el) => el.IsLastOfType(), PseudoClassNames.LastOfType));
			dictionary.Add(PseudoClassNames.OnlyChild, SimpleSelector.PseudoClass((IElement el) => el.IsOnlyChild(), PseudoClassNames.OnlyChild));
			dictionary.Add(PseudoClassNames.FirstChild, SimpleSelector.PseudoClass((IElement el) => el.IsFirstChild(), PseudoClassNames.FirstChild));
			dictionary.Add(PseudoClassNames.LastChild, SimpleSelector.PseudoClass((IElement el) => el.IsLastChild(), PseudoClassNames.LastChild));
			dictionary.Add(PseudoClassNames.Empty, SimpleSelector.PseudoClass((IElement el) => el.ChildElementCount == 0 && el.TextContent.Is(string.Empty), PseudoClassNames.Empty));
			dictionary.Add(PseudoClassNames.AnyLink, SimpleSelector.PseudoClass((IElement el) => el.IsLink() || el.IsVisited(), PseudoClassNames.AnyLink));
			dictionary.Add(PseudoClassNames.Link, SimpleSelector.PseudoClass((IElement el) => el.IsLink(), PseudoClassNames.Link));
			dictionary.Add(PseudoClassNames.Visited, SimpleSelector.PseudoClass((IElement el) => el.IsVisited(), PseudoClassNames.Visited));
			dictionary.Add(PseudoClassNames.Active, SimpleSelector.PseudoClass((IElement el) => el.IsActive(), PseudoClassNames.Active));
			dictionary.Add(PseudoClassNames.Hover, SimpleSelector.PseudoClass((IElement el) => el.IsHovered(), PseudoClassNames.Hover));
			dictionary.Add(PseudoClassNames.Focus, SimpleSelector.PseudoClass((IElement el) => el.IsFocused, PseudoClassNames.Focus));
			dictionary.Add(PseudoClassNames.Target, SimpleSelector.PseudoClass((IElement el) => el.IsTarget(), PseudoClassNames.Target));
			dictionary.Add(PseudoClassNames.Enabled, SimpleSelector.PseudoClass((IElement el) => el.IsEnabled(), PseudoClassNames.Enabled));
			dictionary.Add(PseudoClassNames.Disabled, SimpleSelector.PseudoClass((IElement el) => el.IsDisabled(), PseudoClassNames.Disabled));
			dictionary.Add(PseudoClassNames.Default, SimpleSelector.PseudoClass((IElement el) => el.IsDefault(), PseudoClassNames.Default));
			dictionary.Add(PseudoClassNames.Checked, SimpleSelector.PseudoClass((IElement el) => el.IsChecked(), PseudoClassNames.Checked));
			dictionary.Add(PseudoClassNames.Indeterminate, SimpleSelector.PseudoClass((IElement el) => el.IsIndeterminate(), PseudoClassNames.Indeterminate));
			dictionary.Add(PseudoClassNames.PlaceholderShown, SimpleSelector.PseudoClass((IElement el) => el.IsPlaceholderShown(), PseudoClassNames.PlaceholderShown));
			dictionary.Add(PseudoClassNames.Unchecked, SimpleSelector.PseudoClass((IElement el) => el.IsUnchecked(), PseudoClassNames.Unchecked));
			dictionary.Add(PseudoClassNames.Valid, SimpleSelector.PseudoClass((IElement el) => el.IsValid(), PseudoClassNames.Valid));
			dictionary.Add(PseudoClassNames.Invalid, SimpleSelector.PseudoClass((IElement el) => el.IsInvalid(), PseudoClassNames.Invalid));
			dictionary.Add(PseudoClassNames.Required, SimpleSelector.PseudoClass((IElement el) => el.IsRequired(), PseudoClassNames.Required));
			dictionary.Add(PseudoClassNames.ReadOnly, SimpleSelector.PseudoClass((IElement el) => el.IsReadOnly(), PseudoClassNames.ReadOnly));
			dictionary.Add(PseudoClassNames.ReadWrite, SimpleSelector.PseudoClass((IElement el) => el.IsEditable(), PseudoClassNames.ReadWrite));
			dictionary.Add(PseudoClassNames.InRange, SimpleSelector.PseudoClass((IElement el) => el.IsInRange(), PseudoClassNames.InRange));
			dictionary.Add(PseudoClassNames.OutOfRange, SimpleSelector.PseudoClass((IElement el) => el.IsOutOfRange(), PseudoClassNames.OutOfRange));
			dictionary.Add(PseudoClassNames.Optional, SimpleSelector.PseudoClass((IElement el) => el.IsOptional(), PseudoClassNames.Optional));
			dictionary.Add(PseudoClassNames.Shadow, SimpleSelector.PseudoClass((IElement el) => el.IsShadow(), PseudoClassNames.Shadow));
			dictionary.Add(PseudoElementNames.Before, Factory.PseudoElementSelector.Create(PseudoElementNames.Before));
			dictionary.Add(PseudoElementNames.After, Factory.PseudoElementSelector.Create(PseudoElementNames.After));
			dictionary.Add(PseudoElementNames.FirstLine, Factory.PseudoElementSelector.Create(PseudoElementNames.FirstLine));
			dictionary.Add(PseudoElementNames.FirstLetter, Factory.PseudoElementSelector.Create(PseudoElementNames.FirstLetter));
			this._selectors = dictionary;
			base..ctor();
		}

		// Token: 0x040001CF RID: 463
		private readonly Dictionary<string, ISelector> _selectors;
	}
}
