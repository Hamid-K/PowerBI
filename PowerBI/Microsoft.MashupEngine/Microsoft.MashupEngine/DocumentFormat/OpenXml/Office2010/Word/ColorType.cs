using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CC RID: 9420
	[ChildElementInfo(typeof(RgbColorModelHex), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SchemeColor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ColorType : OpenXmlCompositeElement
	{
		// Token: 0x0601179B RID: 71579 RVA: 0x002ED009 File Offset: 0x002EB209
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (52 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			return null;
		}

		// Token: 0x170052F8 RID: 21240
		// (get) Token: 0x0601179C RID: 71580 RVA: 0x002EED7E File Offset: 0x002ECF7E
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorType.eleTagNames;
			}
		}

		// Token: 0x170052F9 RID: 21241
		// (get) Token: 0x0601179D RID: 71581 RVA: 0x002EED85 File Offset: 0x002ECF85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorType.eleNamespaceIds;
			}
		}

		// Token: 0x170052FA RID: 21242
		// (get) Token: 0x0601179E RID: 71582 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170052FB RID: 21243
		// (get) Token: 0x0601179F RID: 71583 RVA: 0x002ED04A File Offset: 0x002EB24A
		// (set) Token: 0x060117A0 RID: 71584 RVA: 0x002ED053 File Offset: 0x002EB253
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(0);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(0, value);
			}
		}

		// Token: 0x170052FC RID: 21244
		// (get) Token: 0x060117A1 RID: 71585 RVA: 0x002ED05D File Offset: 0x002EB25D
		// (set) Token: 0x060117A2 RID: 71586 RVA: 0x002ED066 File Offset: 0x002EB266
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(1);
			}
			set
			{
				base.SetElement<SchemeColor>(1, value);
			}
		}

		// Token: 0x060117A3 RID: 71587 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ColorType()
		{
		}

		// Token: 0x060117A4 RID: 71588 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ColorType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117A5 RID: 71589 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ColorType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117A6 RID: 71590 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ColorType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007A03 RID: 31235
		private static readonly string[] eleTagNames = new string[] { "srgbClr", "schemeClr" };

		// Token: 0x04007A04 RID: 31236
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
