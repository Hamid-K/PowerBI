using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8F RID: 10895
	[ChildElementInfo(typeof(Media), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ModificationId), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplicationNonVisualDrawingPropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073C6 RID: 29638
		// (get) Token: 0x0601619D RID: 90525 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073C7 RID: 29639
		// (get) Token: 0x0601619E RID: 90526 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073C8 RID: 29640
		// (get) Token: 0x0601619F RID: 90527 RVA: 0x00326897 File Offset: 0x00324A97
		internal override int ElementTypeId
		{
			get
			{
				return 12308;
			}
		}

		// Token: 0x060161A0 RID: 90528 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073C9 RID: 29641
		// (get) Token: 0x060161A1 RID: 90529 RVA: 0x0032689E File Offset: 0x00324A9E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingPropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x170073CA RID: 29642
		// (get) Token: 0x060161A2 RID: 90530 RVA: 0x003268A5 File Offset: 0x00324AA5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingPropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073CB RID: 29643
		// (get) Token: 0x060161A3 RID: 90531 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161A4 RID: 90532 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060161A5 RID: 90533 RVA: 0x00293ECF File Offset: 0x002920CF
		public ApplicationNonVisualDrawingPropertiesExtension()
		{
		}

		// Token: 0x060161A6 RID: 90534 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ApplicationNonVisualDrawingPropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161A7 RID: 90535 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ApplicationNonVisualDrawingPropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161A8 RID: 90536 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ApplicationNonVisualDrawingPropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161A9 RID: 90537 RVA: 0x003268AC File Offset: 0x00324AAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "media" == name)
			{
				return new Media();
			}
			if (49 == namespaceId && "modId" == name)
			{
				return new ModificationId();
			}
			return null;
		}

		// Token: 0x060161AA RID: 90538 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161AB RID: 90539 RVA: 0x003268DF File Offset: 0x00324ADF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingPropertiesExtension>(deep);
		}

		// Token: 0x060161AC RID: 90540 RVA: 0x003268E8 File Offset: 0x00324AE8
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationNonVisualDrawingPropertiesExtension()
		{
			byte[] array = new byte[1];
			ApplicationNonVisualDrawingPropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009636 RID: 38454
		private const string tagName = "ext";

		// Token: 0x04009637 RID: 38455
		private const byte tagNsId = 24;

		// Token: 0x04009638 RID: 38456
		internal const int ElementTypeIdConst = 12308;

		// Token: 0x04009639 RID: 38457
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400963A RID: 38458
		private static byte[] attributeNamespaceIds;
	}
}
