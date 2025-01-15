using System;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200007D RID: 125
	public sealed class DismissedValidationResult
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x000238EC File Offset: 0x00021AEC
		internal DismissedValidationResult(ValidationResult validationResult, AnnotationCollection annotations, string comments)
		{
			Annotation annotation = annotations.Find("B77886FF900E4C18A95F79A6BFA488A9");
			if (annotation == null)
			{
				annotation = new Annotation("B77886FF900E4C18A95F79A6BFA488A9");
				annotations.Add(annotation);
			}
			XmlNode xmlNode = annotation.Value;
			if (xmlNode == null)
			{
				xmlNode = (annotation.Value = new XmlDocument().CreateNode(XmlNodeType.Element, "Results", "B77886FF900E4C18A95F79A6BFA488A9"));
			}
			this.xmlNode = validationResult.AppendTo(xmlNode);
			this.Comments = comments;
			this.validationResult = validationResult;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00023963 File Offset: 0x00021B63
		private DismissedValidationResult(ValidationResult validationResult, XmlNode node)
		{
			this.validationResult = validationResult;
			this.xmlNode = node;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0002397C File Offset: 0x00021B7C
		internal static DismissedValidationResult TryToLoadFrom(XmlNode node)
		{
			ValidationResult validationResult = ValidationResult.TryToLoadFrom(node);
			if (validationResult != null)
			{
				return new DismissedValidationResult(validationResult, node);
			}
			return null;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0002399C File Offset: 0x00021B9C
		public ValidationResult ValidationResult
		{
			get
			{
				return this.validationResult;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000239A4 File Offset: 0x00021BA4
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x000239B1 File Offset: 0x00021BB1
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

		// Token: 0x06000689 RID: 1673 RVA: 0x000239BF File Offset: 0x00021BBF
		internal void RemoveFromAnnotations()
		{
			this.xmlNode.ParentNode.RemoveChild(this.xmlNode);
		}

		// Token: 0x04000434 RID: 1076
		internal const string AnnotationName = "B77886FF900E4C18A95F79A6BFA488A9";

		// Token: 0x04000435 RID: 1077
		internal const string AnnotationNamespace = "B77886FF900E4C18A95F79A6BFA488A9";

		// Token: 0x04000436 RID: 1078
		private ValidationResult validationResult;

		// Token: 0x04000437 RID: 1079
		private XmlNode xmlNode;
	}
}
