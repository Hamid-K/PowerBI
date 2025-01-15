using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A94 RID: 10900
	[ChildElementInfo(typeof(SectionProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SectionList), FileFormatVersions.Office2010)]
	internal class PresentationExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073E4 RID: 29668
		// (get) Token: 0x060161ED RID: 90605 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073E5 RID: 29669
		// (get) Token: 0x060161EE RID: 90606 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073E6 RID: 29670
		// (get) Token: 0x060161EF RID: 90607 RVA: 0x00326B47 File Offset: 0x00324D47
		internal override int ElementTypeId
		{
			get
			{
				return 12313;
			}
		}

		// Token: 0x060161F0 RID: 90608 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073E7 RID: 29671
		// (get) Token: 0x060161F1 RID: 90609 RVA: 0x00326B4E File Offset: 0x00324D4E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresentationExtension.attributeTagNames;
			}
		}

		// Token: 0x170073E8 RID: 29672
		// (get) Token: 0x060161F2 RID: 90610 RVA: 0x00326B55 File Offset: 0x00324D55
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresentationExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073E9 RID: 29673
		// (get) Token: 0x060161F3 RID: 90611 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161F4 RID: 90612 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060161F5 RID: 90613 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresentationExtension()
		{
		}

		// Token: 0x060161F6 RID: 90614 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresentationExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161F7 RID: 90615 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresentationExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161F8 RID: 90616 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresentationExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161F9 RID: 90617 RVA: 0x00326B5C File Offset: 0x00324D5C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "sectionPr" == name)
			{
				return new SectionProperties();
			}
			if (49 == namespaceId && "sectionLst" == name)
			{
				return new SectionList();
			}
			return null;
		}

		// Token: 0x060161FA RID: 90618 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161FB RID: 90619 RVA: 0x00326B8F File Offset: 0x00324D8F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationExtension>(deep);
		}

		// Token: 0x060161FC RID: 90620 RVA: 0x00326B98 File Offset: 0x00324D98
		// Note: this type is marked as 'beforefieldinit'.
		static PresentationExtension()
		{
			byte[] array = new byte[1];
			PresentationExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400964F RID: 38479
		private const string tagName = "ext";

		// Token: 0x04009650 RID: 38480
		private const byte tagNsId = 24;

		// Token: 0x04009651 RID: 38481
		internal const int ElementTypeIdConst = 12313;

		// Token: 0x04009652 RID: 38482
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009653 RID: 38483
		private static byte[] attributeNamespaceIds;
	}
}
