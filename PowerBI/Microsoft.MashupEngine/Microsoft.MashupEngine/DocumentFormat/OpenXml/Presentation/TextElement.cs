using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACA RID: 10954
	[ChildElementInfo(typeof(CharRange))]
	[ChildElementInfo(typeof(ParagraphIndexRange))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TextElement : OpenXmlCompositeElement
	{
		// Token: 0x1700754A RID: 30026
		// (get) Token: 0x0601652A RID: 91434 RVA: 0x00328FCB File Offset: 0x003271CB
		public override string LocalName
		{
			get
			{
				return "txEl";
			}
		}

		// Token: 0x1700754B RID: 30027
		// (get) Token: 0x0601652B RID: 91435 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700754C RID: 30028
		// (get) Token: 0x0601652C RID: 91436 RVA: 0x00328FD2 File Offset: 0x003271D2
		internal override int ElementTypeId
		{
			get
			{
				return 12373;
			}
		}

		// Token: 0x0601652D RID: 91437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601652E RID: 91438 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextElement()
		{
		}

		// Token: 0x0601652F RID: 91439 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016530 RID: 91440 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016531 RID: 91441 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016532 RID: 91442 RVA: 0x00328FD9 File Offset: 0x003271D9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "charRg" == name)
			{
				return new CharRange();
			}
			if (24 == namespaceId && "pRg" == name)
			{
				return new ParagraphIndexRange();
			}
			return null;
		}

		// Token: 0x1700754D RID: 30029
		// (get) Token: 0x06016533 RID: 91443 RVA: 0x0032900C File Offset: 0x0032720C
		internal override string[] ElementTagNames
		{
			get
			{
				return TextElement.eleTagNames;
			}
		}

		// Token: 0x1700754E RID: 30030
		// (get) Token: 0x06016534 RID: 91444 RVA: 0x00329013 File Offset: 0x00327213
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextElement.eleNamespaceIds;
			}
		}

		// Token: 0x1700754F RID: 30031
		// (get) Token: 0x06016535 RID: 91445 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007550 RID: 30032
		// (get) Token: 0x06016536 RID: 91446 RVA: 0x0032901A File Offset: 0x0032721A
		// (set) Token: 0x06016537 RID: 91447 RVA: 0x00329023 File Offset: 0x00327223
		public CharRange CharRange
		{
			get
			{
				return base.GetElement<CharRange>(0);
			}
			set
			{
				base.SetElement<CharRange>(0, value);
			}
		}

		// Token: 0x17007551 RID: 30033
		// (get) Token: 0x06016538 RID: 91448 RVA: 0x0032902D File Offset: 0x0032722D
		// (set) Token: 0x06016539 RID: 91449 RVA: 0x00329036 File Offset: 0x00327236
		public ParagraphIndexRange ParagraphIndexRange
		{
			get
			{
				return base.GetElement<ParagraphIndexRange>(1);
			}
			set
			{
				base.SetElement<ParagraphIndexRange>(1, value);
			}
		}

		// Token: 0x0601653A RID: 91450 RVA: 0x00329040 File Offset: 0x00327240
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextElement>(deep);
		}

		// Token: 0x04009735 RID: 38709
		private const string tagName = "txEl";

		// Token: 0x04009736 RID: 38710
		private const byte tagNsId = 24;

		// Token: 0x04009737 RID: 38711
		internal const int ElementTypeIdConst = 12373;

		// Token: 0x04009738 RID: 38712
		private static readonly string[] eleTagNames = new string[] { "charRg", "pRg" };

		// Token: 0x04009739 RID: 38713
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
