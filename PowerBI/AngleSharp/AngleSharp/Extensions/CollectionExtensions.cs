using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Html;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E5 RID: 229
	internal static class CollectionExtensions
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00032FCD File Offset: 0x000311CD
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T element)
		{
			foreach (T t in items)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield return element;
			yield break;
			yield break;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00032FE4 File Offset: 0x000311E4
		public static IEnumerable<T> GetElements<T>(this INode parent, bool deep = true, Predicate<T> predicate = null) where T : class, INode
		{
			Predicate<T> predicate2;
			if ((predicate2 = predicate) == null && (predicate2 = CollectionExtensions.<>c__1<T>.<>9__1_0) == null)
			{
				predicate2 = (CollectionExtensions.<>c__1<T>.<>9__1_0 = (T m) => true);
			}
			predicate = predicate2;
			if (!deep)
			{
				return parent.GetDescendendElements(predicate);
			}
			return parent.GetAllElements(predicate);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00033020 File Offset: 0x00031220
		public static T GetItemByIndex<T>(this IEnumerable<T> items, int index)
		{
			if (index >= 0)
			{
				int num = 0;
				foreach (T t in items)
				{
					if (num++ == index)
					{
						return t;
					}
				}
			}
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00033080 File Offset: 0x00031280
		public static IElement GetElementById(this INodeList children, string id)
		{
			for (int i = 0; i < children.Length; i++)
			{
				IElement element = children[i] as IElement;
				if (element != null)
				{
					if (element.Id.Is(id))
					{
						return element;
					}
					element = element.ChildNodes.GetElementById(id);
					if (element != null)
					{
						return element;
					}
				}
			}
			return null;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000330D4 File Offset: 0x000312D4
		public static void GetElementsByName(this INodeList children, string name, List<IElement> result)
		{
			for (int i = 0; i < children.Length; i++)
			{
				IElement element = children[i] as IElement;
				if (element != null)
				{
					if (element.GetAttribute(null, AttributeNames.Name).Is(name))
					{
						result.Add(element);
					}
					element.ChildNodes.GetElementsByName(name, result);
				}
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0003312C File Offset: 0x0003132C
		public static bool Accepts(this FilterSettings filter, INode node)
		{
			switch (node.NodeType)
			{
			case NodeType.Element:
				return (filter & FilterSettings.Element) == FilterSettings.Element;
			case NodeType.Attribute:
				return (filter & FilterSettings.Attribute) == FilterSettings.Attribute;
			case NodeType.Text:
				return (filter & FilterSettings.Text) == FilterSettings.Text;
			case NodeType.CharacterData:
				return (filter & FilterSettings.CharacterData) == FilterSettings.CharacterData;
			case NodeType.EntityReference:
				return (filter & FilterSettings.EntityReference) == FilterSettings.EntityReference;
			case NodeType.Entity:
				return (filter & FilterSettings.Entity) == FilterSettings.Entity;
			case NodeType.ProcessingInstruction:
				return (filter & FilterSettings.ProcessingInstruction) == FilterSettings.ProcessingInstruction;
			case NodeType.Comment:
				return (filter & FilterSettings.Comment) == FilterSettings.Comment;
			case NodeType.Document:
				return (filter & FilterSettings.Document) == FilterSettings.Document;
			case NodeType.DocumentType:
				return (filter & FilterSettings.DocumentType) == FilterSettings.DocumentType;
			case NodeType.DocumentFragment:
				return (filter & FilterSettings.DocumentFragment) == FilterSettings.DocumentFragment;
			case NodeType.Notation:
				return (filter & FilterSettings.Notation) == FilterSettings.Notation;
			default:
				return filter == (FilterSettings)(-1);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0003321C File Offset: 0x0003141C
		public static IEnumerable<T> GetElements<T>(this INode parent, FilterSettings filter) where T : class, INode
		{
			return parent.GetElements(true, (T node) => filter.Accepts(node));
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0003324C File Offset: 0x0003144C
		public static T GetElementById<T>(this IEnumerable<T> elements, string id) where T : class, IElement
		{
			foreach (T t in elements)
			{
				if (t.Id.Is(id))
				{
					return t;
				}
			}
			foreach (T t2 in elements)
			{
				if (t2.GetAttribute(null, AttributeNames.Name).Is(id))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00033300 File Offset: 0x00031500
		private static IEnumerable<T> GetAllElements<T>(this INode parent, Predicate<T> predicate) where T : class, INode
		{
			int num;
			for (int i = 0; i < parent.ChildNodes.Length; i = num + 1)
			{
				T t = parent.ChildNodes[i] as T;
				if (t != null && predicate(t))
				{
					yield return t;
				}
				foreach (T t2 in parent.ChildNodes[i].GetAllElements(predicate))
				{
					yield return t2;
				}
				IEnumerator<T> enumerator = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00033317 File Offset: 0x00031517
		private static IEnumerable<T> GetDescendendElements<T>(this INode parent, Predicate<T> predicate) where T : class, INode
		{
			int num;
			for (int i = 0; i < parent.ChildNodes.Length; i = num + 1)
			{
				T t = parent.ChildNodes[i] as T;
				if (t != null && predicate(t))
				{
					yield return t;
				}
				num = i;
			}
			yield break;
		}
	}
}
