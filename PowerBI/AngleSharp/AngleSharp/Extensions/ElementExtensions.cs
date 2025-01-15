using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;
using AngleSharp.Services.Media;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E9 RID: 233
	internal static class ElementExtensions
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x00033A48 File Offset: 0x00031C48
		public static string LocatePrefixFor(this IElement element, string namespaceUri)
		{
			if (element.NamespaceUri.Is(namespaceUri) && element.Prefix != null)
			{
				return element.Prefix;
			}
			foreach (IAttr attr in element.Attributes)
			{
				if (attr.Prefix.Is(NamespaceNames.XmlNsPrefix) && attr.Value.Is(namespaceUri))
				{
					return attr.LocalName;
				}
			}
			IElement parentElement = element.ParentElement;
			if (parentElement == null)
			{
				return null;
			}
			return parentElement.LocatePrefixFor(namespaceUri);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00033AE8 File Offset: 0x00031CE8
		public static string LocateNamespaceFor(this IElement element, string prefix)
		{
			string namespaceUri = element.NamespaceUri;
			string prefix2 = element.Prefix;
			if (!string.IsNullOrEmpty(namespaceUri) && prefix2.Is(prefix))
			{
				return namespaceUri;
			}
			Predicate<IAttr> predicate;
			if (prefix != null)
			{
				predicate = (IAttr attr) => attr.NamespaceUri.Is(NamespaceNames.XmlNsUri) && attr.Prefix.Is(NamespaceNames.XmlNsPrefix) && attr.LocalName.Is(prefix);
			}
			else
			{
				predicate = (IAttr attr) => attr.NamespaceUri.Is(NamespaceNames.XmlNsUri) && attr.Prefix == null && attr.LocalName.Is(NamespaceNames.XmlNsPrefix);
			}
			Predicate<IAttr> predicate2 = predicate;
			foreach (IAttr attr2 in element.Attributes)
			{
				if (predicate2(attr2))
				{
					string text = attr2.Value;
					if (string.IsNullOrEmpty(text))
					{
						text = null;
					}
					return text;
				}
			}
			IElement parentElement = element.ParentElement;
			if (parentElement == null)
			{
				return null;
			}
			return parentElement.LocateNamespaceFor(prefix);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00033BDC File Offset: 0x00031DDC
		public static ResourceRequest CreateRequestFor(this IElement element, Url url)
		{
			return new ResourceRequest(element, url);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00033BE8 File Offset: 0x00031DE8
		public static bool MatchesCssNamespace(this IElement el, string prefix)
		{
			if (prefix.Is(Keywords.Asterisk))
			{
				return true;
			}
			string text = el.GetAttribute(NamespaceNames.XmlNsPrefix) ?? el.NamespaceUri;
			if (prefix.Is(string.Empty))
			{
				return text.Is(string.Empty);
			}
			return text.Is(el.GetCssNamespace(prefix));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00033C40 File Offset: 0x00031E40
		public static string GetCssNamespace(this IElement el, string prefix)
		{
			IDocument owner = el.Owner;
			return ((owner != null) ? owner.StyleSheets.LocateNamespace(prefix) : null) ?? el.LocateNamespaceFor(prefix);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public static bool IsHovered(this IElement element)
		{
			return false;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00033C68 File Offset: 0x00031E68
		public static bool IsOnlyOfType(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				for (int i = 0; i < parentElement.ChildNodes.Length; i++)
				{
					if (parentElement.ChildNodes[i].NodeName.Is(element.NodeName) && parentElement.ChildNodes[i] != element)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00033CC8 File Offset: 0x00031EC8
		public static bool IsFirstOfType(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				for (int i = 0; i < parentElement.ChildNodes.Length; i++)
				{
					if (parentElement.ChildNodes[i].NodeName.Is(element.NodeName))
					{
						return parentElement.ChildNodes[i] == element;
					}
				}
			}
			return false;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00033D24 File Offset: 0x00031F24
		public static bool IsLastOfType(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				for (int i = parentElement.ChildNodes.Length - 1; i >= 0; i--)
				{
					if (parentElement.ChildNodes[i].NodeName.Is(element.NodeName))
					{
						return parentElement.ChildNodes[i] == element;
					}
				}
			}
			return false;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00033D84 File Offset: 0x00031F84
		public static bool IsTarget(this IElement element)
		{
			string id = element.Id;
			IDocument owner = element.Owner;
			string text = ((owner != null) ? owner.Location.Hash : null);
			return id != null && text != null && string.Compare(id, 0, text, (text.Length > 0) ? 1 : 0, int.MaxValue) == 0;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00033DD8 File Offset: 0x00031FD8
		public static bool IsEnabled(this IElement element)
		{
			if (element is HtmlAnchorElement || element is HtmlAreaElement || element is HtmlLinkElement)
			{
				return !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href));
			}
			if (element is HtmlButtonElement)
			{
				return !((HtmlButtonElement)element).IsDisabled;
			}
			if (element is HtmlInputElement)
			{
				return !((HtmlInputElement)element).IsDisabled;
			}
			if (element is HtmlSelectElement)
			{
				return !((HtmlSelectElement)element).IsDisabled;
			}
			if (element is HtmlTextAreaElement)
			{
				return !((HtmlTextAreaElement)element).IsDisabled;
			}
			if (element is HtmlOptionElement)
			{
				return !((HtmlOptionElement)element).IsDisabled;
			}
			return (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement) && string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Disabled));
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00033EB0 File Offset: 0x000320B0
		public static bool IsDisabled(this IElement element)
		{
			if (element is HtmlButtonElement)
			{
				return ((HtmlButtonElement)element).IsDisabled;
			}
			if (element is HtmlInputElement)
			{
				return ((HtmlInputElement)element).IsDisabled;
			}
			if (element is HtmlSelectElement)
			{
				return ((HtmlSelectElement)element).IsDisabled;
			}
			if (element is HtmlTextAreaElement)
			{
				return ((HtmlTextAreaElement)element).IsDisabled;
			}
			if (element is HtmlOptionElement)
			{
				return ((HtmlOptionElement)element).IsDisabled;
			}
			return (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement) && !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Disabled));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00033F50 File Offset: 0x00032150
		public static bool IsDefault(this IElement element)
		{
			if (element is HtmlButtonElement)
			{
				if (((HtmlButtonElement)element).Form != null)
				{
					return true;
				}
			}
			else if (element is HtmlInputElement)
			{
				HtmlInputElement htmlInputElement = (HtmlInputElement)element;
				string type = htmlInputElement.Type;
				if ((type == InputTypeNames.Submit || type == InputTypeNames.Image) && htmlInputElement.Form != null)
				{
					return true;
				}
			}
			else if (element is HtmlOptionElement)
			{
				return !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Selected));
			}
			return false;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00033FCC File Offset: 0x000321CC
		public static bool IsPseudo(this IElement element, string name)
		{
			PseudoElement pseudoElement = element as PseudoElement;
			return pseudoElement != null && pseudoElement.PseudoName.Is(name);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00033FF4 File Offset: 0x000321F4
		public static bool IsChecked(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				HtmlInputElement htmlInputElement = (HtmlInputElement)element;
				return htmlInputElement.Type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio) && htmlInputElement.IsChecked;
			}
			if (element is HtmlMenuItemElement)
			{
				HtmlMenuItemElement htmlMenuItemElement = (HtmlMenuItemElement)element;
				return htmlMenuItemElement.Type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio) && htmlMenuItemElement.IsChecked;
			}
			return element is HtmlOptionElement && ((HtmlOptionElement)element).IsSelected;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00034074 File Offset: 0x00032274
		public static bool IsIndeterminate(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				HtmlInputElement htmlInputElement = (HtmlInputElement)element;
				return htmlInputElement.Type.Is(InputTypeNames.Checkbox) && htmlInputElement.IsIndeterminate;
			}
			return element is HtmlProgressElement && string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Value));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000340C8 File Offset: 0x000322C8
		public static bool IsPlaceholderShown(this IElement element)
		{
			HtmlInputElement htmlInputElement = element as HtmlInputElement;
			if (htmlInputElement != null)
			{
				bool flag = !string.IsNullOrEmpty(htmlInputElement.Placeholder);
				bool flag2 = string.IsNullOrEmpty(htmlInputElement.Value);
				return flag && flag2;
			}
			return false;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00034100 File Offset: 0x00032300
		public static bool IsUnchecked(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				HtmlInputElement htmlInputElement = (HtmlInputElement)element;
				return htmlInputElement.Type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio) && !htmlInputElement.IsChecked;
			}
			if (element is HtmlMenuItemElement)
			{
				HtmlMenuItemElement htmlMenuItemElement = (HtmlMenuItemElement)element;
				return htmlMenuItemElement.Type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio) && !htmlMenuItemElement.IsChecked;
			}
			return element is HtmlOptionElement && !((HtmlOptionElement)element).IsSelected;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0003418C File Offset: 0x0003238C
		public static bool IsActive(this IElement element)
		{
			if (element is HtmlAnchorElement)
			{
				HtmlAnchorElement htmlAnchorElement = (HtmlAnchorElement)element;
				return !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && htmlAnchorElement.IsActive;
			}
			if (element is HtmlAreaElement)
			{
				HtmlAreaElement htmlAreaElement = (HtmlAreaElement)element;
				return !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && htmlAreaElement.IsActive;
			}
			if (element is HtmlLinkElement)
			{
				HtmlLinkElement htmlLinkElement = (HtmlLinkElement)element;
				return !string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && htmlLinkElement.IsActive;
			}
			if (element is HtmlButtonElement)
			{
				HtmlButtonElement htmlButtonElement = (HtmlButtonElement)element;
				return !htmlButtonElement.IsDisabled && htmlButtonElement.IsActive;
			}
			if (element is HtmlInputElement)
			{
				HtmlInputElement htmlInputElement = (HtmlInputElement)element;
				return htmlInputElement.Type.IsOneOf(InputTypeNames.Submit, InputTypeNames.Image, InputTypeNames.Reset, InputTypeNames.Button) && htmlInputElement.IsActive;
			}
			if (element is HtmlMenuItemElement)
			{
				HtmlMenuItemElement htmlMenuItemElement = (HtmlMenuItemElement)element;
				return !htmlMenuItemElement.IsDisabled && htmlMenuItemElement.IsActive;
			}
			return false;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0003429C File Offset: 0x0003249C
		public static bool IsVisited(this IElement element)
		{
			if (element is HtmlAnchorElement)
			{
				string attribute = element.GetAttribute(null, AttributeNames.Href);
				HtmlAnchorElement htmlAnchorElement = (HtmlAnchorElement)element;
				return !string.IsNullOrEmpty(attribute) && htmlAnchorElement.IsVisited;
			}
			if (element is HtmlAreaElement)
			{
				string attribute2 = element.GetAttribute(null, AttributeNames.Href);
				HtmlAreaElement htmlAreaElement = (HtmlAreaElement)element;
				return !string.IsNullOrEmpty(attribute2) && htmlAreaElement.IsVisited;
			}
			if (element is HtmlLinkElement)
			{
				string attribute3 = element.GetAttribute(null, AttributeNames.Href);
				HtmlLinkElement htmlLinkElement = (HtmlLinkElement)element;
				return !string.IsNullOrEmpty(attribute3) && htmlLinkElement.IsVisited;
			}
			return false;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0003432C File Offset: 0x0003252C
		public static bool IsLink(this IElement element)
		{
			if (element is HtmlAnchorElement)
			{
				string attribute = element.GetAttribute(null, AttributeNames.Href);
				HtmlAnchorElement htmlAnchorElement = (HtmlAnchorElement)element;
				return !string.IsNullOrEmpty(attribute) && !htmlAnchorElement.IsVisited;
			}
			if (element is HtmlAreaElement)
			{
				string attribute2 = element.GetAttribute(null, AttributeNames.Href);
				HtmlAreaElement htmlAreaElement = (HtmlAreaElement)element;
				return !string.IsNullOrEmpty(attribute2) && !htmlAreaElement.IsVisited;
			}
			if (element is HtmlLinkElement)
			{
				string attribute3 = element.GetAttribute(null, AttributeNames.Href);
				HtmlLinkElement htmlLinkElement = (HtmlLinkElement)element;
				return !string.IsNullOrEmpty(attribute3) && !htmlLinkElement.IsVisited;
			}
			return false;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000343C4 File Offset: 0x000325C4
		public static bool IsShadow(this IElement element)
		{
			return ((element != null) ? element.ShadowRoot : null) != null;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000343D8 File Offset: 0x000325D8
		public static bool IsOptional(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				return !((HtmlInputElement)element).IsRequired;
			}
			if (element is HtmlSelectElement)
			{
				return !((HtmlSelectElement)element).IsRequired;
			}
			return element is HtmlTextAreaElement && !((HtmlTextAreaElement)element).IsRequired;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0003442B File Offset: 0x0003262B
		public static bool IsRequired(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				return ((HtmlInputElement)element).IsRequired;
			}
			if (element is HtmlSelectElement)
			{
				return ((HtmlSelectElement)element).IsRequired;
			}
			return element is HtmlTextAreaElement && ((HtmlTextAreaElement)element).IsRequired;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0003446A File Offset: 0x0003266A
		public static bool IsInvalid(this IElement element)
		{
			if (element is IValidation)
			{
				return !((IValidation)element).CheckValidity();
			}
			return element is HtmlFormElement && !((HtmlFormElement)element).CheckValidity();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003449B File Offset: 0x0003269B
		public static bool IsValid(this IElement element)
		{
			if (element is IValidation)
			{
				return ((IValidation)element).CheckValidity();
			}
			return element is HtmlFormElement && ((HtmlFormElement)element).CheckValidity();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000344C8 File Offset: 0x000326C8
		public static bool IsReadOnly(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				return !((HtmlInputElement)element).IsMutable;
			}
			if (element is HtmlTextAreaElement)
			{
				return !((HtmlTextAreaElement)element).IsMutable;
			}
			return !(element is IHtmlElement) || !((IHtmlElement)element).IsContentEditable;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0003451B File Offset: 0x0003271B
		public static bool IsEditable(this IElement element)
		{
			if (element is HtmlInputElement)
			{
				return ((HtmlInputElement)element).IsMutable;
			}
			if (element is HtmlTextAreaElement)
			{
				return ((HtmlTextAreaElement)element).IsMutable;
			}
			return element is IHtmlElement && ((IHtmlElement)element).IsContentEditable;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0003455C File Offset: 0x0003275C
		public static bool IsOutOfRange(this IElement element)
		{
			IValidation validation = element as IValidation;
			if (validation != null)
			{
				IValidityState validity = validation.Validity;
				return validity.IsRangeOverflow || validity.IsRangeUnderflow;
			}
			return false;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0003458C File Offset: 0x0003278C
		public static bool IsInRange(this IElement element)
		{
			IValidation validation = element as IValidation;
			if (validation != null)
			{
				IValidityState validity = validation.Validity;
				return !validity.IsRangeOverflow && !validity.IsRangeUnderflow;
			}
			return false;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000345C0 File Offset: 0x000327C0
		public static bool IsOnlyChild(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			return parentElement != null && parentElement.ChildElementCount == 1 && parentElement.FirstElementChild == element;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000345EB File Offset: 0x000327EB
		public static bool IsFirstChild(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			return ((parentElement != null) ? parentElement.FirstElementChild : null) == element;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00034602 File Offset: 0x00032802
		public static bool IsLastChild(this IElement element)
		{
			IElement parentElement = element.ParentElement;
			return ((parentElement != null) ? parentElement.LastElementChild : null) == element;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0003461C File Offset: 0x0003281C
		public static void Process(this Element element, IRequestProcessor processor, Url url)
		{
			ResourceRequest resourceRequest = element.CreateRequestFor(url);
			Task task = ((processor != null) ? processor.ProcessAsync(resourceRequest) : null);
			if (task != null)
			{
				Document owner = element.Owner;
				if (owner == null)
				{
					return;
				}
				owner.DelayLoad(task);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00034654 File Offset: 0x00032854
		public static Url GetImageCandidate(this HtmlImageElement img)
		{
			Document owner = img.Owner;
			SourceSet sourceSet = new SourceSet(owner);
			IConfiguration options = owner.Options;
			Stack<IHtmlSourceElement> sources = img.GetSources();
			while (sources.Count > 0)
			{
				IHtmlSourceElement htmlSourceElement = sources.Pop();
				string type = htmlSourceElement.Type;
				if (string.IsNullOrEmpty(type) || options.GetResourceService(type) != null)
				{
					using (IEnumerator<string> enumerator = sourceSet.GetCandidates(htmlSourceElement.SourceSet, htmlSourceElement.Sizes).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							return new Url(img.BaseUrl, text);
						}
					}
					continue;
				}
			}
			using (IEnumerator<string> enumerator = sourceSet.GetCandidates(img.SourceSet, img.Sizes).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string text2 = enumerator.Current;
					return new Url(img.BaseUrl, text2);
				}
			}
			return Url.Create(img.Source);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0003476C File Offset: 0x0003296C
		public static async Task<IDocument> NavigateToAsync(this Element element, DocumentRequest request)
		{
			IResponse response = await element.Owner.Context.Loader.DownloadAsync(request).Task.ConfigureAwait(false);
			CancellationToken none = CancellationToken.None;
			return await element.Owner.Context.OpenAsync(response, none).ConfigureAwait(false);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000347B9 File Offset: 0x000329B9
		public static string GetOwnAttribute(this Element element, string name)
		{
			IAttr namedItem = element.Attributes.GetNamedItem(null, name);
			if (namedItem == null)
			{
				return null;
			}
			return namedItem.Value;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000347D3 File Offset: 0x000329D3
		public static bool HasOwnAttribute(this Element element, string name)
		{
			return element.Attributes.GetNamedItem(null, name) != null;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000347E8 File Offset: 0x000329E8
		public static string GetUrlAttribute(this Element element, string name)
		{
			string ownAttribute = element.GetOwnAttribute(name);
			Url url = ((ownAttribute != null) ? new Url(element.BaseUrl, ownAttribute) : null);
			if (url == null || url.IsInvalid)
			{
				return string.Empty;
			}
			return url.Href;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00034827 File Offset: 0x00032A27
		public static bool GetBoolAttribute(this Element element, string name)
		{
			return element.GetOwnAttribute(name) != null;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00034833 File Offset: 0x00032A33
		public static void SetBoolAttribute(this Element element, string name, bool value)
		{
			if (value)
			{
				element.SetOwnAttribute(name, string.Empty, false);
				return;
			}
			element.Attributes.RemoveNamedItemOrDefault(name, true);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00034854 File Offset: 0x00032A54
		public static void SetOwnAttribute(this Element element, string name, string value, bool suppressCallbacks = false)
		{
			element.Attributes.SetNamedItemWithNamespaceUri(new Attr(name, value), suppressCallbacks);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0003486C File Offset: 0x00032A6C
		private static Stack<IHtmlSourceElement> GetSources(this IHtmlImageElement img)
		{
			IElement parentElement = img.ParentElement;
			Stack<IHtmlSourceElement> stack = new Stack<IHtmlSourceElement>();
			if (parentElement != null && parentElement.LocalName.Is(TagNames.Picture))
			{
				for (IHtmlSourceElement htmlSourceElement = img.PreviousElementSibling as IHtmlSourceElement; htmlSourceElement != null; htmlSourceElement = htmlSourceElement.PreviousElementSibling as IHtmlSourceElement)
				{
					stack.Push(htmlSourceElement);
				}
			}
			return stack;
		}
	}
}
