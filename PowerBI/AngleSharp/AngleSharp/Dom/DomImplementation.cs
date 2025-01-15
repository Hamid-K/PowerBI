using System;
using System.Collections.Generic;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Xml;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000151 RID: 337
	internal sealed class DomImplementation : IImplementation
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x00042C55 File Offset: 0x00040E55
		public DomImplementation(Document owner)
		{
			this._owner = owner;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00042C64 File Offset: 0x00040E64
		public IDocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId)
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			if (!qualifiedName.IsXmlName())
			{
				throw new DomException(DomError.InvalidCharacter);
			}
			if (!qualifiedName.IsQualifiedName())
			{
				throw new DomException(DomError.Namespace);
			}
			return new DocumentType(this._owner, qualifiedName)
			{
				PublicIdentifier = publicId,
				SystemIdentifier = systemId
			};
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00042CB8 File Offset: 0x00040EB8
		public IXmlDocument CreateDocument(string namespaceUri = null, string qualifiedName = null, IDocumentType doctype = null)
		{
			XmlDocument xmlDocument = new XmlDocument(null);
			if (doctype != null)
			{
				xmlDocument.AppendChild(doctype);
			}
			if (!string.IsNullOrEmpty(qualifiedName))
			{
				IElement element = xmlDocument.CreateElement(namespaceUri, qualifiedName);
				if (element != null)
				{
					xmlDocument.AppendChild(element);
				}
			}
			xmlDocument.BaseUrl = this._owner.BaseUrl;
			return xmlDocument;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00042D08 File Offset: 0x00040F08
		public IDocument CreateHtmlDocument(string title)
		{
			HtmlDocument htmlDocument = new HtmlDocument(null);
			htmlDocument.AppendChild(new DocumentType(htmlDocument, TagNames.Html));
			htmlDocument.AppendChild(htmlDocument.CreateElement(TagNames.Html));
			htmlDocument.DocumentElement.AppendChild(htmlDocument.CreateElement(TagNames.Head));
			if (!string.IsNullOrEmpty(title))
			{
				IElement element = htmlDocument.CreateElement(TagNames.Title);
				element.AppendChild(htmlDocument.CreateTextNode(title));
				htmlDocument.Head.AppendChild(element);
			}
			htmlDocument.DocumentElement.AppendChild(htmlDocument.CreateElement(TagNames.Body));
			htmlDocument.BaseUrl = this._owner.BaseUrl;
			return htmlDocument;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00042DB0 File Offset: 0x00040FB0
		public bool HasFeature(string feature, string version = null)
		{
			if (feature == null)
			{
				throw new ArgumentNullException("feature");
			}
			string[] array = null;
			return DomImplementation.features.TryGetValue(feature, out array) && array.Contains(version ?? string.Empty, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000937 RID: 2359
		private static readonly Dictionary<string, string[]> features = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"XML",
				new string[] { "1.0", "2.0" }
			},
			{
				"HTML",
				new string[] { "1.0", "2.0" }
			},
			{
				"Core",
				new string[] { "2.0" }
			},
			{
				"Views",
				new string[] { "2.0" }
			},
			{
				"StyleSheets",
				new string[] { "2.0" }
			},
			{
				"CSS",
				new string[] { "2.0" }
			},
			{
				"CSS2",
				new string[] { "2.0" }
			},
			{
				"Traversal",
				new string[] { "2.0" }
			},
			{
				"Events",
				new string[] { "2.0" }
			},
			{
				"UIEvents",
				new string[] { "2.0" }
			},
			{
				"HTMLEvents",
				new string[] { "2.0" }
			},
			{
				"Range",
				new string[] { "2.0" }
			},
			{
				"MutationEvents",
				new string[] { "2.0" }
			}
		};

		// Token: 0x04000938 RID: 2360
		private readonly Document _owner;
	}
}
