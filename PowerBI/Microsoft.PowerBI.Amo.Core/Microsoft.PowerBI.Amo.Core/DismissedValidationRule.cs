using System;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200007F RID: 127
	public sealed class DismissedValidationRule
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x00023C50 File Offset: 0x00021E50
		internal DismissedValidationRule(ValidationRule validationRule, AnnotationCollection annotations, string comments)
		{
			Annotation annotation = annotations.Find("CA1792B06E6B48BDAD71EFE2A95A0F02");
			if (annotation == null)
			{
				annotation = new Annotation("CA1792B06E6B48BDAD71EFE2A95A0F02");
				annotations.Add(annotation);
			}
			XmlNode xmlNode = annotation.Value;
			if (xmlNode == null)
			{
				xmlNode = (annotation.Value = new XmlDocument().CreateNode(XmlNodeType.Element, "Rules", "CA1792B06E6B48BDAD71EFE2A95A0F02"));
			}
			XmlDocument ownerDocument = xmlNode.OwnerDocument;
			XmlNode xmlNode2 = xmlNode.AppendChild(ownerDocument.CreateNode(XmlNodeType.Element, "Rule", "CA1792B06E6B48BDAD71EFE2A95A0F02"));
			xmlNode2.Attributes.Append(ownerDocument.CreateAttribute("id")).Value = XmlConvert.ToString(validationRule.ID);
			if (!string.IsNullOrEmpty(comments))
			{
				xmlNode2.AppendChild(ownerDocument.CreateTextNode(comments));
			}
			this.validationRule = validationRule;
			this.xmlNode = xmlNode2;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00023D15 File Offset: 0x00021F15
		private DismissedValidationRule(ValidationRule validationRule, XmlNode node)
		{
			this.validationRule = validationRule;
			this.xmlNode = node;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00023D2B File Offset: 0x00021F2B
		public ValidationRule ValidationRule
		{
			get
			{
				return this.validationRule;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00023D33 File Offset: 0x00021F33
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00023D40 File Offset: 0x00021F40
		public string Comments
		{
			get
			{
				return this.xmlNode.InnerText;
			}
			set
			{
				this.xmlNode.InnerText = value;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00023D4E File Offset: 0x00021F4E
		internal void RemoveFromAnnotations()
		{
			this.xmlNode.ParentNode.RemoveChild(this.xmlNode);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00023D68 File Offset: 0x00021F68
		internal static DismissedValidationRule TryToLoadFrom(XmlNode node)
		{
			ValidationRule validationRule = ValidationRule.Find(XmlConvert.ToInt32(node.Attributes["id"].Value));
			if (validationRule != null)
			{
				return new DismissedValidationRule(validationRule, node);
			}
			return null;
		}

		// Token: 0x0400043B RID: 1083
		internal const string AnnotationName = "CA1792B06E6B48BDAD71EFE2A95A0F02";

		// Token: 0x0400043C RID: 1084
		internal const string AnnotationNamespace = "CA1792B06E6B48BDAD71EFE2A95A0F02";

		// Token: 0x0400043D RID: 1085
		private ValidationRule validationRule;

		// Token: 0x0400043E RID: 1086
		private XmlNode xmlNode;
	}
}
