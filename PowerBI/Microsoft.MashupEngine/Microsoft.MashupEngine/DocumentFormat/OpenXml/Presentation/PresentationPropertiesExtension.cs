using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A92 RID: 10898
	[ChildElementInfo(typeof(DiscardImageEditData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DefaultImageDpi), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextMath), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PresentationPropertiesExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073D8 RID: 29656
		// (get) Token: 0x060161CD RID: 90573 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073D9 RID: 29657
		// (get) Token: 0x060161CE RID: 90574 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073DA RID: 29658
		// (get) Token: 0x060161CF RID: 90575 RVA: 0x003269FF File Offset: 0x00324BFF
		internal override int ElementTypeId
		{
			get
			{
				return 12311;
			}
		}

		// Token: 0x060161D0 RID: 90576 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073DB RID: 29659
		// (get) Token: 0x060161D1 RID: 90577 RVA: 0x00326A06 File Offset: 0x00324C06
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresentationPropertiesExtension.attributeTagNames;
			}
		}

		// Token: 0x170073DC RID: 29660
		// (get) Token: 0x060161D2 RID: 90578 RVA: 0x00326A0D File Offset: 0x00324C0D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresentationPropertiesExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073DD RID: 29661
		// (get) Token: 0x060161D3 RID: 90579 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161D4 RID: 90580 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060161D5 RID: 90581 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresentationPropertiesExtension()
		{
		}

		// Token: 0x060161D6 RID: 90582 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresentationPropertiesExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161D7 RID: 90583 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresentationPropertiesExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161D8 RID: 90584 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresentationPropertiesExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161D9 RID: 90585 RVA: 0x00326A14 File Offset: 0x00324C14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "discardImageEditData" == name)
			{
				return new DiscardImageEditData();
			}
			if (49 == namespaceId && "defaultImageDpi" == name)
			{
				return new DefaultImageDpi();
			}
			if (48 == namespaceId && "m" == name)
			{
				return new TextMath();
			}
			return null;
		}

		// Token: 0x060161DA RID: 90586 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161DB RID: 90587 RVA: 0x00326A6A File Offset: 0x00324C6A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationPropertiesExtension>(deep);
		}

		// Token: 0x060161DC RID: 90588 RVA: 0x00326A74 File Offset: 0x00324C74
		// Note: this type is marked as 'beforefieldinit'.
		static PresentationPropertiesExtension()
		{
			byte[] array = new byte[1];
			PresentationPropertiesExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009645 RID: 38469
		private const string tagName = "ext";

		// Token: 0x04009646 RID: 38470
		private const byte tagNsId = 24;

		// Token: 0x04009647 RID: 38471
		internal const int ElementTypeIdConst = 12311;

		// Token: 0x04009648 RID: 38472
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009649 RID: 38473
		private static byte[] attributeNamespaceIds;
	}
}
