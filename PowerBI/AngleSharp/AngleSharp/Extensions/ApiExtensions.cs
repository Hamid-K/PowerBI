using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Html;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E2 RID: 226
	public static class ApiExtensions
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00031754 File Offset: 0x0002F954
		public static TElement CreateElement<TElement>(this IDocument document) where TElement : IElement
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			Type type = (from m in typeof(ApiExtensions).GetAssembly().GetTypes()
				where m.Implements<TElement>()
				select m).FirstOrDefault((Type m) => !m.IsAbstractClass());
			if (type != null)
			{
				foreach (ConstructorInfo constructorInfo in from m in type.GetConstructors()
					orderby m.GetParameters().Length
					select m)
				{
					ParameterInfo[] parameters = constructorInfo.GetParameters();
					object[] array = new object[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						bool flag = parameters[i].ParameterType == typeof(Document);
						array[i] = (flag ? document : parameters[i].DefaultValue);
					}
					object obj = constructorInfo.Invoke(array);
					if (obj != null)
					{
						TElement telement = (TElement)((object)obj);
						Element element = telement as Element;
						if (element != null)
						{
							element.SetupElement();
						}
						document.Adopt(telement);
						return telement;
					}
				}
			}
			throw new ArgumentException("No element could be created for the provided interface.");
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000318DC File Offset: 0x0002FADC
		public static async Task<Event> AwaitEventAsync<TEventTarget>(this TEventTarget node, string eventName) where TEventTarget : IEventTarget
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			TaskCompletionSource<Event> completion = new TaskCompletionSource<Event>();
			DomEventHandler handler = delegate(object s, Event ev)
			{
				completion.TrySetResult(ev);
			};
			node.AddEventListener(eventName, handler, false);
			Event @event;
			try
			{
				@event = await completion.Task.ConfigureAwait(false);
			}
			finally
			{
				node.RemoveEventListener(eventName, handler, false);
			}
			return @event;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00031929 File Offset: 0x0002FB29
		public static TElement AppendElement<TElement>(this INode parent, TElement element) where TElement : class, IElement
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return parent.AppendChild(element) as TElement;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003194F File Offset: 0x0002FB4F
		public static TElement InsertElement<TElement>(this INode parent, TElement newElement, INode referenceElement) where TElement : class, IElement
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return parent.InsertBefore(newElement, referenceElement) as TElement;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00031976 File Offset: 0x0002FB76
		public static TElement RemoveElement<TElement>(this INode parent, TElement element) where TElement : class, IElement
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return parent.RemoveChild(element) as TElement;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003199C File Offset: 0x0002FB9C
		public static TElement QuerySelector<TElement>(this IParentNode parent, string selectors) where TElement : class, IElement
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (selectors == null)
			{
				throw new ArgumentNullException("selectors");
			}
			return parent.QuerySelector(selectors) as TElement;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000319CB File Offset: 0x0002FBCB
		public static IEnumerable<TElement> QuerySelectorAll<TElement>(this IParentNode parent, string selectors) where TElement : IElement
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (selectors == null)
			{
				throw new ArgumentNullException("selectors");
			}
			return parent.QuerySelectorAll(selectors).OfType<TElement>();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000319F5 File Offset: 0x0002FBF5
		public static IEnumerable<TNode> Descendents<TNode>(this INode parent)
		{
			return parent.Descendents().OfType<TNode>();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00031A02 File Offset: 0x0002FC02
		public static IEnumerable<INode> Descendents(this INode parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			return parent.GetDescendants();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00031A18 File Offset: 0x0002FC18
		public static IEnumerable<TNode> Ancestors<TNode>(this INode child)
		{
			return child.Ancestors().OfType<TNode>();
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00031A25 File Offset: 0x0002FC25
		public static IEnumerable<INode> Ancestors(this INode child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			return child.GetAncestors();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00031A3C File Offset: 0x0002FC3C
		public static IHtmlFormElement SetValues(this IHtmlFormElement form, IDictionary<string, string> fields, bool createMissing = false)
		{
			if (form == null)
			{
				throw new ArgumentNullException("form");
			}
			if (fields == null)
			{
				throw new ArgumentNullException("fields");
			}
			IEnumerable<HtmlFormControlElement> enumerable = form.Elements.OfType<HtmlFormControlElement>();
			using (IEnumerator<KeyValuePair<string, string>> enumerator = fields.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> field = enumerator.Current;
					HtmlFormControlElement targetInput = enumerable.FirstOrDefault((HtmlFormControlElement e) => e.Name.Is(field.Key));
					if (targetInput != null)
					{
						if (targetInput is IHtmlInputElement)
						{
							IHtmlInputElement htmlInputElement = (IHtmlInputElement)targetInput;
							if (htmlInputElement.Type.Is(InputTypeNames.Radio))
							{
								using (IEnumerator<IHtmlInputElement> enumerator2 = (from i in enumerable.OfType<IHtmlInputElement>()
									where i.Name.Is(targetInput.Name)
									select i).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										IHtmlInputElement htmlInputElement2 = enumerator2.Current;
										htmlInputElement2.IsChecked = htmlInputElement2.Value.Is(field.Value);
									}
									continue;
								}
							}
							htmlInputElement.Value = field.Value;
						}
						else if (targetInput is IHtmlTextAreaElement)
						{
							((IHtmlTextAreaElement)targetInput).Value = field.Value;
						}
						else if (targetInput is IHtmlSelectElement)
						{
							((IHtmlSelectElement)targetInput).Value = field.Value;
						}
					}
					else
					{
						if (!createMissing)
						{
							throw new KeyNotFoundException(string.Format("Field {0} not found.", field.Key));
						}
						IHtmlInputElement htmlInputElement3 = form.Owner.CreateElement<IHtmlInputElement>();
						htmlInputElement3.Type = InputTypeNames.Hidden;
						htmlInputElement3.Name = field.Key;
						htmlInputElement3.Value = field.Value;
						form.AppendChild(htmlInputElement3);
					}
				}
			}
			return form;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00031C68 File Offset: 0x0002FE68
		public static Task<IDocument> NavigateAsync<TElement>(this TElement element) where TElement : IUrlUtilities, IElement
		{
			return element.NavigateAsync(CancellationToken.None);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00031C78 File Offset: 0x0002FE78
		public static Task<IDocument> NavigateAsync<TElement>(this TElement element, CancellationToken cancel) where TElement : IUrlUtilities, IElement
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			Url url = Url.Create(element.Href);
			return element.Owner.Context.OpenAsync(url, cancel);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, object fields)
		{
			return form.SubmitAsync(fields.ToDictionary(), false);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00031CD3 File Offset: 0x0002FED3
		public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, IDictionary<string, string> fields, bool createMissing = false)
		{
			form.SetValues(fields, createMissing);
			return form.SubmitAsync();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00031CE4 File Offset: 0x0002FEE4
		public static Task<IDocument> SubmitAsync(this IHtmlElement element, object fields = null)
		{
			return element.SubmitAsync(fields.ToDictionary(), false);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00031CF4 File Offset: 0x0002FEF4
		public static Task<IDocument> SubmitAsync(this IHtmlElement element, IDictionary<string, string> fields, bool createMissing = false)
		{
			HtmlFormControlElement htmlFormControlElement = element as HtmlFormControlElement;
			if (htmlFormControlElement == null)
			{
				throw new ArgumentException("element");
			}
			IHtmlFormElement form = htmlFormControlElement.Form;
			if (form != null)
			{
				form.SetValues(fields, createMissing);
				return form.SubmitAsync(htmlFormControlElement);
			}
			return null;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00031D32 File Offset: 0x0002FF32
		public static T Eq<T>(this IEnumerable<T> elements, int index) where T : IElement
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			return elements.Skip(index).FirstOrDefault<T>();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00031D4E File Offset: 0x0002FF4E
		public static IEnumerable<T> Gt<T>(this IEnumerable<T> elements, int index) where T : IElement
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			return elements.Skip(index + 1);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00031D67 File Offset: 0x0002FF67
		public static IEnumerable<T> Lt<T>(this IEnumerable<T> elements, int index) where T : IElement
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			return elements.Take(index);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00031D7E File Offset: 0x0002FF7E
		public static IEnumerable<T> Even<T>(this IEnumerable<T> elements) where T : IElement
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			bool even = true;
			foreach (T t in elements)
			{
				if (even)
				{
					yield return t;
				}
				even = !even;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00031D8E File Offset: 0x0002FF8E
		public static IEnumerable<T> Odd<T>(this IEnumerable<T> elements) where T : IElement
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			bool odd = false;
			foreach (T t in elements)
			{
				if (odd)
				{
					yield return t;
				}
				odd = !odd;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00031D9E File Offset: 0x0002FF9E
		public static ICssStyleDeclaration ComputeCurrentStyle(this IElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			IDocument owner = element.Owner;
			IWindow window = ((owner != null) ? owner.DefaultView : null);
			if (window == null)
			{
				return null;
			}
			return window.GetComputedStyle(element, null);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00031DD0 File Offset: 0x0002FFD0
		public static T Attr<T>(this T elements, string attributeName, string attributeValue) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			foreach (IElement element in elements)
			{
				element.SetAttribute(attributeName, attributeValue);
			}
			return elements;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00031E40 File Offset: 0x00030040
		public static T Attr<T>(this T elements, IEnumerable<KeyValuePair<string, string>> attributes) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			foreach (IElement element in elements)
			{
				foreach (KeyValuePair<string, string> keyValuePair in attributes)
				{
					element.SetAttribute(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return elements;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00031EF0 File Offset: 0x000300F0
		public static T Attr<T>(this T elements, object attributes) where T : IEnumerable<IElement>
		{
			Dictionary<string, string> dictionary = attributes.ToDictionary();
			return elements.Attr(dictionary);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00031F0C File Offset: 0x0003010C
		public static IEnumerable<string> Attr<T>(this T elements, string attributeName) where T : IEnumerable<IElement>
		{
			return elements.Select((IElement m) => m.GetAttribute(attributeName));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00031F3D File Offset: 0x0003013D
		public static IElement ClearAttr(this IElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.Attributes.Clear();
			return element;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00031F5C File Offset: 0x0003015C
		public static T ClearAttr<T>(this T elements) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				element.ClearAttr();
			}
			return elements;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00031FC0 File Offset: 0x000301C0
		public static IElement Empty(this IElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.InnerHtml = string.Empty;
			return element;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00031FDC File Offset: 0x000301DC
		public static T Empty<T>(this T elements) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				element.Empty();
			}
			return elements;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00032040 File Offset: 0x00030240
		public static T Css<T>(this T elements, string propertyName, string propertyValue) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			foreach (IHtmlElement htmlElement in elements.OfType<IHtmlElement>())
			{
				htmlElement.Style.SetProperty(propertyName, propertyValue, null);
			}
			return elements;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000320BC File Offset: 0x000302BC
		public static T Css<T>(this T elements, IEnumerable<KeyValuePair<string, string>> properties) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			foreach (IHtmlElement htmlElement in elements.OfType<IHtmlElement>())
			{
				foreach (KeyValuePair<string, string> keyValuePair in properties)
				{
					htmlElement.Style.SetProperty(keyValuePair.Key, keyValuePair.Value, null);
				}
			}
			return elements;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00032174 File Offset: 0x00030374
		public static T Css<T>(this T elements, object properties) where T : IEnumerable<IElement>
		{
			Dictionary<string, string> dictionary = properties.ToDictionary();
			return elements.Css(dictionary);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003218F File Offset: 0x0003038F
		public static string Html(this IElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.InnerHtml;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000321A8 File Offset: 0x000303A8
		public static T Html<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				element.InnerHtml = html;
			}
			return elements;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0003220C File Offset: 0x0003040C
		public static T AddClass<T>(this T elements, string className) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			string[] array = className.SplitSpaces();
			foreach (IElement element in elements)
			{
				element.ClassList.Add(array);
			}
			return elements;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00032288 File Offset: 0x00030488
		public static T RemoveClass<T>(this T elements, string className) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			string[] array = className.SplitSpaces();
			foreach (IElement element in elements)
			{
				element.ClassList.Remove(array);
			}
			return elements;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00032304 File Offset: 0x00030504
		public static T ToggleClass<T>(this T elements, string className) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			string[] array = className.SplitSpaces();
			foreach (IElement element in elements)
			{
				foreach (string text in array)
				{
					element.ClassList.Toggle(text, false);
				}
			}
			return elements;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000323A0 File Offset: 0x000305A0
		public static bool HasClass(this IEnumerable<IElement> elements, string className)
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			string[] array = className.SplitSpaces();
			foreach (IElement element in elements)
			{
				bool flag = true;
				foreach (string text in array)
				{
					if (!element.ClassList.Contains(text))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00032444 File Offset: 0x00030644
		public static T Before<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IElement parentElement = element.ParentElement;
				if (parentElement != null)
				{
					IDocumentFragment documentFragment = parentElement.CreateFragment(html);
					parentElement.InsertBefore(documentFragment, element);
				}
			}
			return elements;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000324BC File Offset: 0x000306BC
		public static T After<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IElement parentElement = element.ParentElement;
				if (parentElement != null)
				{
					IDocumentFragment documentFragment = parentElement.CreateFragment(html);
					parentElement.InsertBefore(documentFragment, element.NextSibling);
				}
			}
			return elements;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00032538 File Offset: 0x00030738
		public static T Append<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IDocumentFragment documentFragment = element.CreateFragment(html);
				element.Append(new INode[] { documentFragment });
			}
			return elements;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000325AC File Offset: 0x000307AC
		public static T Prepend<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IDocumentFragment documentFragment = element.CreateFragment(html);
				element.InsertBefore(documentFragment, element.FirstChild);
			}
			return elements;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00032620 File Offset: 0x00030820
		public static T Wrap<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IDocumentFragment documentFragment = element.CreateFragment(html);
				INode innerMostElement = documentFragment.GetInnerMostElement();
				INode parent = element.Parent;
				if (parent != null)
				{
					parent.InsertBefore(documentFragment, element);
				}
				innerMostElement.AppendChild(element);
			}
			return elements;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000326A8 File Offset: 0x000308A8
		public static T WrapInner<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			foreach (IElement element in elements)
			{
				IDocumentFragment documentFragment = element.CreateFragment(html);
				IElement innerMostElement = documentFragment.GetInnerMostElement();
				while (element.ChildNodes.Length > 0)
				{
					INode node = element.ChildNodes[0];
					innerMostElement.AppendChild(node);
				}
				element.AppendChild(documentFragment);
			}
			return elements;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00032744 File Offset: 0x00030944
		public static T WrapAll<T>(this T elements, string html) where T : IEnumerable<IElement>
		{
			if (elements == null)
			{
				throw new ArgumentNullException("elements");
			}
			IElement element = elements.FirstOrDefault<IElement>();
			if (element != null)
			{
				IDocumentFragment documentFragment = element.CreateFragment(html);
				IElement innerMostElement = documentFragment.GetInnerMostElement();
				INode parent = element.Parent;
				if (parent != null)
				{
					parent.InsertBefore(documentFragment, element);
				}
				foreach (IElement element2 in elements)
				{
					innerMostElement.AppendChild(element2);
				}
			}
			return elements;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000327DC File Offset: 0x000309DC
		public static IHtmlCollection<TElement> ToCollection<TElement>(this IEnumerable<TElement> elements) where TElement : class, IElement
		{
			return new HtmlCollection<TElement>(elements);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000327E4 File Offset: 0x000309E4
		public static INamedNodeMap Clear(this INamedNodeMap attributes)
		{
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			while (attributes.Length > 0)
			{
				string name = attributes[attributes.Length - 1].Name;
				attributes.RemoveNamedItem(name);
			}
			return attributes;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00032828 File Offset: 0x00030A28
		public static IEnumerable<IDownload> GetDownloads(this IDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			return from m in document.All.OfType<ILoadableElement>()
				select m.CurrentDownload into m
				where m != null
				select m;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00032896 File Offset: 0x00030A96
		private static IDocumentFragment CreateFromHtml(this IDocument document, string html)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			Element element = document.Body as Element;
			if (element == null)
			{
				throw new ArgumentException("The provided document does not have a valid body element.");
			}
			return new DocumentFragment(element, html ?? string.Empty);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000328CE File Offset: 0x00030ACE
		public static string Text(this INode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return node.TextContent;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000328E4 File Offset: 0x00030AE4
		public static T Text<T>(this T nodes, string text) where T : IEnumerable<INode>
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			foreach (INode node in nodes)
			{
				node.TextContent = text;
			}
			return nodes;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00032948 File Offset: 0x00030B48
		public static int Index(this IEnumerable<INode> nodes, INode item)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (item != null)
			{
				int num = 0;
				using (IEnumerator<INode> enumerator = nodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == item)
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000329AC File Offset: 0x00030BAC
		private static IDocumentFragment CreateFragment(this IElement context, string html)
		{
			Element element = context as Element;
			string text = html ?? string.Empty;
			return new DocumentFragment(element, text);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000329D0 File Offset: 0x00030BD0
		private static IElement GetInnerMostElement(this IDocumentFragment fragment)
		{
			if (fragment.ChildElementCount != 1)
			{
				throw new InvalidOperationException("The provided HTML code did not result in any element.");
			}
			IElement element = fragment.FirstElementChild;
			IElement element2;
			do
			{
				element2 = element;
				element = element2.FirstElementChild;
			}
			while (element != null);
			return element2;
		}
	}
}
