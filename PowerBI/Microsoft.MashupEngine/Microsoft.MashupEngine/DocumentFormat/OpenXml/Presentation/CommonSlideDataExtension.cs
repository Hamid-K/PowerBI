using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A91 RID: 10897
	[ChildElementInfo(typeof(CreationId), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CommonSlideDataExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073D2 RID: 29650
		// (get) Token: 0x060161BD RID: 90557 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073D3 RID: 29651
		// (get) Token: 0x060161BE RID: 90558 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073D4 RID: 29652
		// (get) Token: 0x060161BF RID: 90559 RVA: 0x00326997 File Offset: 0x00324B97
		internal override int ElementTypeId
		{
			get
			{
				return 12310;
			}
		}

		// Token: 0x060161C0 RID: 90560 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073D5 RID: 29653
		// (get) Token: 0x060161C1 RID: 90561 RVA: 0x0032699E File Offset: 0x00324B9E
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonSlideDataExtension.attributeTagNames;
			}
		}

		// Token: 0x170073D6 RID: 29654
		// (get) Token: 0x060161C2 RID: 90562 RVA: 0x003269A5 File Offset: 0x00324BA5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonSlideDataExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073D7 RID: 29655
		// (get) Token: 0x060161C3 RID: 90563 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161C4 RID: 90564 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060161C5 RID: 90565 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonSlideDataExtension()
		{
		}

		// Token: 0x060161C6 RID: 90566 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonSlideDataExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161C7 RID: 90567 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonSlideDataExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161C8 RID: 90568 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonSlideDataExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161C9 RID: 90569 RVA: 0x003269AC File Offset: 0x00324BAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "creationId" == name)
			{
				return new CreationId();
			}
			return null;
		}

		// Token: 0x060161CA RID: 90570 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161CB RID: 90571 RVA: 0x003269C7 File Offset: 0x00324BC7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonSlideDataExtension>(deep);
		}

		// Token: 0x060161CC RID: 90572 RVA: 0x003269D0 File Offset: 0x00324BD0
		// Note: this type is marked as 'beforefieldinit'.
		static CommonSlideDataExtension()
		{
			byte[] array = new byte[1];
			CommonSlideDataExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009640 RID: 38464
		private const string tagName = "ext";

		// Token: 0x04009641 RID: 38465
		private const byte tagNsId = 24;

		// Token: 0x04009642 RID: 38466
		internal const int ElementTypeIdConst = 12310;

		// Token: 0x04009643 RID: 38467
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009644 RID: 38468
		private static byte[] attributeNamespaceIds;
	}
}
