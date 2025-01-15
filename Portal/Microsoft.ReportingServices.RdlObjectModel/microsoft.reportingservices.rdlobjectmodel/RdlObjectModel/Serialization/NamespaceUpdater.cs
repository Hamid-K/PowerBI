using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002DE RID: 734
	internal sealed class NamespaceUpdater
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x00034D24 File Offset: 0x00032F24
		public string[] Update(XmlWriter writer, string xml, ISerializerHost host = null)
		{
			XmlDocument xmlDocument = XmlUtils.CreateXmlDocumentWithNullResolver();
			xmlDocument.LoadXmlWithNullResolver(xml);
			string[] array = new NamespaceUpdater().Update(xmlDocument, host);
			xmlDocument.Save(writer);
			return array;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00034D54 File Offset: 0x00032F54
		public string[] Update(XmlDocument document, ISerializerHost host = null)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			Dictionary<string, NamespaceUpdater.ExtensionNamespaceCounter> dictionary;
			if (host != null)
			{
				dictionary = host.GetExtensionNamespaces().ToDictionary((ExtensionNamespace v) => v.Namespace, (ExtensionNamespace v) => new NamespaceUpdater.ExtensionNamespaceCounter(v));
			}
			else
			{
				dictionary = new Dictionary<string, NamespaceUpdater.ExtensionNamespaceCounter>();
			}
			XmlElement documentElement = document.DocumentElement;
			if (documentElement == null)
			{
				throw new InvalidOperationException("Document contains no elements");
			}
			this.AddMustUnderstandNamespaces(dictionary, documentElement);
			this.AddXmlnsNamespaces(dictionary, documentElement);
			this.UpdateLocalNames(documentElement, dictionary);
			ExtensionNamespace[] array = (from v in dictionary
				where v.Value.Count > 0
				select v.Value.ExtensionNamespace).ToArray<ExtensionNamespace>();
			NamespaceUpdater.UpdateRootNamespaces(array, documentElement);
			string[] array2 = (from v in array
				where v.MustUnderstand
				select v.LocalName).ToArray<string>();
			string text = string.Join(" ", array2);
			if (text.Any<char>())
			{
				documentElement.SetAttribute("MustUnderstand", text);
			}
			else
			{
				documentElement.RemoveAttribute("MustUnderstand");
			}
			return array2;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00034EC4 File Offset: 0x000330C4
		private void AddXmlnsNamespaces(Dictionary<string, NamespaceUpdater.ExtensionNamespaceCounter> xmlnsDictionary, XmlElement rootElem)
		{
			IEnumerable<XmlAttribute> enumerable = rootElem.Attributes.Cast<XmlAttribute>();
			Func<XmlAttribute, bool> <>9__0;
			Func<XmlAttribute, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (XmlAttribute a) => a.Prefix == "xmlns" && !xmlnsDictionary.ContainsKey(a.Value));
			}
			foreach (XmlAttribute xmlAttribute in enumerable.Where(func))
			{
				ExtensionNamespace extensionNamespace = new ExtensionNamespace(xmlAttribute.LocalName, xmlAttribute.Value, false);
				xmlnsDictionary.Add(xmlAttribute.Value, new NamespaceUpdater.ExtensionNamespaceCounter(extensionNamespace));
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00034F6C File Offset: 0x0003316C
		private static void UpdateRootNamespaces(IEnumerable<ExtensionNamespace> namespaces, XmlElement rootElem)
		{
			int count = rootElem.Attributes.Count;
			while (count-- > 0)
			{
				if (rootElem.Attributes[count].Prefix == "xmlns")
				{
					rootElem.Attributes.RemoveAt(count);
				}
			}
			foreach (ExtensionNamespace extensionNamespace in namespaces)
			{
				rootElem.SetAttribute(string.Format("xmlns:{0}", extensionNamespace.LocalName), extensionNamespace.Namespace);
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0003500C File Offset: 0x0003320C
		private void AddMustUnderstandNamespaces(Dictionary<string, NamespaceUpdater.ExtensionNamespaceCounter> xmlnsDictionary, XmlElement rootElem)
		{
			XmlNode namedItem = rootElem.Attributes.GetNamedItem("MustUnderstand");
			if (namedItem == null || string.IsNullOrEmpty(namedItem.Value))
			{
				return;
			}
			foreach (string text in namedItem.Value.Split(Array.Empty<char>()))
			{
				string namespaceOfPrefix = rootElem.GetNamespaceOfPrefix(text);
				if (!string.IsNullOrEmpty(namespaceOfPrefix) && !xmlnsDictionary.ContainsKey(namespaceOfPrefix))
				{
					xmlnsDictionary.Add(namespaceOfPrefix, new NamespaceUpdater.ExtensionNamespaceCounter(new ExtensionNamespace(text, namespaceOfPrefix, true)));
				}
			}
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00035090 File Offset: 0x00033290
		private void UpdateLocalNames(XmlElement xmlElement, Dictionary<string, NamespaceUpdater.ExtensionNamespaceCounter> xmlnsDictionary)
		{
			Stack<XmlNode> stack = new Stack<XmlNode>(new XmlElement[] { xmlElement });
			while (stack.Count != 0)
			{
				XmlNode xmlNode = stack.Pop();
				NamespaceUpdater.ExtensionNamespaceCounter extensionNamespaceCounter;
				if (xmlnsDictionary.TryGetValue(xmlNode.NamespaceURI, out extensionNamespaceCounter))
				{
					xmlNode.Prefix = extensionNamespaceCounter.ExtensionNamespace.LocalName;
					XmlElement xmlElement2 = xmlNode as XmlElement;
					if (xmlElement2 != null)
					{
						xmlElement2.RemoveAttribute("xmlns");
					}
					NamespaceUpdater.ExtensionNamespaceCounter extensionNamespaceCounter2 = extensionNamespaceCounter;
					int count = extensionNamespaceCounter2.Count;
					extensionNamespaceCounter2.Count = count + 1;
				}
				IEnumerable<XmlNode> enumerable;
				if (xmlNode.Attributes != null)
				{
					enumerable = xmlNode.Attributes.Cast<XmlNode>();
				}
				else
				{
					IEnumerable<XmlNode> enumerable2 = new XmlNode[0];
					enumerable = enumerable2;
				}
				IEnumerable<XmlNode> enumerable3 = xmlNode.ChildNodes.Cast<XmlNode>();
				foreach (XmlNode xmlNode2 in enumerable.Concat(enumerable3))
				{
					stack.Push(xmlNode2);
				}
			}
		}

		// Token: 0x02000416 RID: 1046
		private sealed class ExtensionNamespaceCounter
		{
			// Token: 0x06001905 RID: 6405 RVA: 0x0003C3D3 File Offset: 0x0003A5D3
			public ExtensionNamespaceCounter(ExtensionNamespace extensionNamespace)
			{
				this.ExtensionNamespace = extensionNamespace;
				this.Count = 0;
			}

			// Token: 0x17000758 RID: 1880
			// (get) Token: 0x06001906 RID: 6406 RVA: 0x0003C3E9 File Offset: 0x0003A5E9
			// (set) Token: 0x06001907 RID: 6407 RVA: 0x0003C3F1 File Offset: 0x0003A5F1
			public ExtensionNamespace ExtensionNamespace { get; private set; }

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x06001908 RID: 6408 RVA: 0x0003C3FA File Offset: 0x0003A5FA
			// (set) Token: 0x06001909 RID: 6409 RVA: 0x0003C402 File Offset: 0x0003A602
			public int Count { get; set; }
		}
	}
}
