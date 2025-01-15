using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030DA RID: 12506
	internal static class OpenXmlElementExtensionMethods
	{
		// Token: 0x0601B2A4 RID: 111268 RVA: 0x0036F7C0 File Offset: 0x0036D9C0
		internal static OpenXmlElement GetFirstChildMc(this OpenXmlElement parent, MCContext mcContext, FileFormatVersions format)
		{
			OpenXmlElement firstNonMiscElementChild = parent.GetFirstNonMiscElementChild();
			return parent.GetChildMc(firstNonMiscElementChild, mcContext, format);
		}

		// Token: 0x0601B2A5 RID: 111269 RVA: 0x0036F7E0 File Offset: 0x0036D9E0
		internal static OpenXmlElement GetNextChildMc(this OpenXmlElement parent, OpenXmlElement child, MCContext mcContext, FileFormatVersions format)
		{
			OpenXmlElement nextNonMiscElementSibling = child.GetNextNonMiscElementSibling();
			OpenXmlElement openXmlElement = child.Parent;
			if (nextNonMiscElementSibling == null && openXmlElement != parent)
			{
				if (openXmlElement is AlternateContentChoice || openXmlElement is AlternateContentFallback)
				{
					openXmlElement = openXmlElement.Parent;
				}
				return parent.GetNextChildMc(openXmlElement, mcContext, format);
			}
			return parent.GetChildMc(nextNonMiscElementSibling, mcContext, format);
		}

		// Token: 0x0601B2A6 RID: 111270 RVA: 0x0036F82C File Offset: 0x0036DA2C
		private static OpenXmlElement GetChildMc(this OpenXmlElement parent, OpenXmlElement child, MCContext mcContext, FileFormatVersions format)
		{
			Stack<OpenXmlElement> stack = new Stack<OpenXmlElement>();
			while (child != null)
			{
				AlternateContent alternateContent = child as AlternateContent;
				if (alternateContent == null && child.IsInVersion(format))
				{
					return child;
				}
				mcContext.PushMCAttributes2(child.MCAttributes, new MCContext.LookupNamespace(child.LookupNamespace));
				if (alternateContent != null)
				{
					stack.Push(child.GetNextNonMiscElementSibling());
					OpenXmlCompositeElement contentFromACBlock = mcContext.GetContentFromACBlock(alternateContent, format);
					if (contentFromACBlock != null)
					{
						child = contentFromACBlock.GetFirstNonMiscElementChild();
					}
					else
					{
						child = null;
					}
				}
				else
				{
					if (!mcContext.IsIgnorableNs(child.NamespaceUri))
					{
						mcContext.PopMCAttributes2();
						return child;
					}
					if (mcContext.IsProcessContent(child))
					{
						stack.Push(child.GetNextNonMiscElementSibling());
						child = child.GetFirstNonMiscElementChild();
					}
					else
					{
						child = child.GetNextNonMiscElementSibling();
					}
				}
				mcContext.PopMCAttributes2();
				while (child == null && stack.Count > 0)
				{
					child = stack.Pop();
				}
			}
			return child;
		}
	}
}
