using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A08 RID: 10760
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionListModify), FileFormatVersions.Office2010)]
	internal class ContentPart : OpenXmlCompositeElement
	{
		// Token: 0x17006F80 RID: 28544
		// (get) Token: 0x06015840 RID: 88128 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x17006F81 RID: 28545
		// (get) Token: 0x06015841 RID: 88129 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F82 RID: 28546
		// (get) Token: 0x06015842 RID: 88130 RVA: 0x0032016E File Offset: 0x0031E36E
		internal override int ElementTypeId
		{
			get
			{
				return 12187;
			}
		}

		// Token: 0x06015843 RID: 88131 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F83 RID: 28547
		// (get) Token: 0x06015844 RID: 88132 RVA: 0x00320175 File Offset: 0x0031E375
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x17006F84 RID: 28548
		// (get) Token: 0x06015845 RID: 88133 RVA: 0x0032017C File Offset: 0x0031E37C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F85 RID: 28549
		// (get) Token: 0x06015846 RID: 88134 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06015847 RID: 88135 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(49, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BwMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006F86 RID: 28550
		// (get) Token: 0x06015848 RID: 88136 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015849 RID: 88137 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601584A RID: 88138 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPart()
		{
		}

		// Token: 0x0601584B RID: 88139 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601584C RID: 88140 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601584D RID: 88141 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601584E RID: 88142 RVA: 0x00320184 File Offset: 0x0031E384
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "nvContentPartPr" == name)
			{
				return new NonVisualContentPartProperties();
			}
			if (49 == namespaceId && "xfrm" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D();
			}
			if (49 == namespaceId && "extLst" == name)
			{
				return new ExtensionListModify();
			}
			return null;
		}

		// Token: 0x17006F87 RID: 28551
		// (get) Token: 0x0601584F RID: 88143 RVA: 0x003201DA File Offset: 0x0031E3DA
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPart.eleTagNames;
			}
		}

		// Token: 0x17006F88 RID: 28552
		// (get) Token: 0x06015850 RID: 88144 RVA: 0x003201E1 File Offset: 0x0031E3E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17006F89 RID: 28553
		// (get) Token: 0x06015851 RID: 88145 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F8A RID: 28554
		// (get) Token: 0x06015852 RID: 88146 RVA: 0x003201E8 File Offset: 0x0031E3E8
		// (set) Token: 0x06015853 RID: 88147 RVA: 0x003201F1 File Offset: 0x0031E3F1
		public NonVisualContentPartProperties NonVisualContentPartProperties
		{
			get
			{
				return base.GetElement<NonVisualContentPartProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualContentPartProperties>(0, value);
			}
		}

		// Token: 0x17006F8B RID: 28555
		// (get) Token: 0x06015854 RID: 88148 RVA: 0x003201FB File Offset: 0x0031E3FB
		// (set) Token: 0x06015855 RID: 88149 RVA: 0x00320204 File Offset: 0x0031E404
		public DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D Transform2D
		{
			get
			{
				return base.GetElement<DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D>(1);
			}
			set
			{
				base.SetElement<DocumentFormat.OpenXml.Office2010.PowerPoint.Transform2D>(1, value);
			}
		}

		// Token: 0x17006F8C RID: 28556
		// (get) Token: 0x06015856 RID: 88150 RVA: 0x0032020E File Offset: 0x0031E40E
		// (set) Token: 0x06015857 RID: 88151 RVA: 0x00320217 File Offset: 0x0031E417
		public ExtensionListModify ExtensionListModify
		{
			get
			{
				return base.GetElement<ExtensionListModify>(2);
			}
			set
			{
				base.SetElement<ExtensionListModify>(2, value);
			}
		}

		// Token: 0x06015858 RID: 88152 RVA: 0x00320221 File Offset: 0x0031E421
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015859 RID: 88153 RVA: 0x0032025B File Offset: 0x0031E45B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x040093A9 RID: 37801
		private const string tagName = "contentPart";

		// Token: 0x040093AA RID: 37802
		private const byte tagNsId = 24;

		// Token: 0x040093AB RID: 37803
		internal const int ElementTypeIdConst = 12187;

		// Token: 0x040093AC RID: 37804
		private static string[] attributeTagNames = new string[] { "bwMode", "id" };

		// Token: 0x040093AD RID: 37805
		private static byte[] attributeNamespaceIds = new byte[] { 49, 19 };

		// Token: 0x040093AE RID: 37806
		private static readonly string[] eleTagNames = new string[] { "nvContentPartPr", "xfrm", "extLst" };

		// Token: 0x040093AF RID: 37807
		private static readonly byte[] eleNamespaceIds = new byte[] { 49, 49, 49 };
	}
}
