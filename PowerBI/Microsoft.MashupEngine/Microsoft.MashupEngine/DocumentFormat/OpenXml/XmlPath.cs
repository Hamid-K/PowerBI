using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02003155 RID: 12629
	[DebuggerDisplay("XPath={XPath}")]
	internal class XmlPath
	{
		// Token: 0x0601B5F7 RID: 112119 RVA: 0x003771D1 File Offset: 0x003753D1
		private XmlPath()
		{
		}

		// Token: 0x0601B5F8 RID: 112120 RVA: 0x003771E4 File Offset: 0x003753E4
		internal XmlPath(OpenXmlPart part)
		{
			this.PartUri = part.Uri;
		}

		// Token: 0x170099BF RID: 39359
		// (get) Token: 0x0601B5F9 RID: 112121 RVA: 0x00377203 File Offset: 0x00375403
		public IList<string> NamespacesDefinitions
		{
			get
			{
				return this._namespacesDefinitions;
			}
		}

		// Token: 0x170099C0 RID: 39360
		// (get) Token: 0x0601B5FA RID: 112122 RVA: 0x0037720B File Offset: 0x0037540B
		// (set) Token: 0x0601B5FB RID: 112123 RVA: 0x00377213 File Offset: 0x00375413
		public string XPath { get; private set; }

		// Token: 0x170099C1 RID: 39361
		// (get) Token: 0x0601B5FC RID: 112124 RVA: 0x0037721C File Offset: 0x0037541C
		// (set) Token: 0x0601B5FD RID: 112125 RVA: 0x00377224 File Offset: 0x00375424
		public Uri PartUri { get; private set; }

		// Token: 0x0601B5FE RID: 112126 RVA: 0x00377230 File Offset: 0x00375430
		internal static XmlPath GetXPath(OpenXmlElement element)
		{
			if (element == null)
			{
				return null;
			}
			XmlPath xmlPath = new XmlPath();
			xmlPath.PartUri = element.GetPartUri();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StringBuilder stringBuilder = new StringBuilder("");
			XmlPath.BuildXPath(element, stringBuilder, dictionary);
			xmlPath.XPath = stringBuilder.ToString();
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				StringBuilder stringBuilder2 = new StringBuilder("");
				stringBuilder2.Append("xmlns:");
				stringBuilder2.Append(keyValuePair.Key);
				stringBuilder2.Append("=\"");
				stringBuilder2.Append(keyValuePair.Value);
				stringBuilder2.Append("\"");
				xmlPath.NamespacesDefinitions.Add(stringBuilder2.ToString());
			}
			return xmlPath;
		}

		// Token: 0x0601B5FF RID: 112127 RVA: 0x00377314 File Offset: 0x00375514
		internal static void BuildXPath(OpenXmlElement element, StringBuilder xpath, Dictionary<string, string> namespaces)
		{
			if (element.Parent != null)
			{
				XmlPath.BuildXPath(element.Parent, xpath, namespaces);
			}
			xpath.Append("/");
			if (element is OpenXmlMiscNode)
			{
				xpath.Append(element.OuterXml);
				return;
			}
			if (!string.IsNullOrEmpty(element.Prefix))
			{
				if (!namespaces.ContainsKey(element.Prefix))
				{
					namespaces.Add(element.Prefix, element.NamespaceUri);
				}
				xpath.Append(element.Prefix);
				xpath.Append(":");
			}
			else if (!string.IsNullOrEmpty(element.NamespaceUri))
			{
				xpath.Append(element.NamespaceUri);
				xpath.Append(":");
			}
			xpath.Append(element.LocalName);
			xpath.Append("[");
			xpath.Append(element.GetXPathIndex());
			xpath.Append("]");
		}

		// Token: 0x0400B567 RID: 46439
		private List<string> _namespacesDefinitions = new List<string>();
	}
}
