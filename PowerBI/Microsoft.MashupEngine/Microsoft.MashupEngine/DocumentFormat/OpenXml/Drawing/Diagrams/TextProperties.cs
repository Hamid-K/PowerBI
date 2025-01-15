using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268C RID: 9868
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(FlatText))]
	internal class TextProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005CF9 RID: 23801
		// (get) Token: 0x06012E00 RID: 77312 RVA: 0x002F10F0 File Offset: 0x002EF2F0
		public override string LocalName
		{
			get
			{
				return "txPr";
			}
		}

		// Token: 0x17005CFA RID: 23802
		// (get) Token: 0x06012E01 RID: 77313 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CFB RID: 23803
		// (get) Token: 0x06012E02 RID: 77314 RVA: 0x003004AA File Offset: 0x002FE6AA
		internal override int ElementTypeId
		{
			get
			{
				return 10683;
			}
		}

		// Token: 0x06012E03 RID: 77315 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012E04 RID: 77316 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextProperties()
		{
		}

		// Token: 0x06012E05 RID: 77317 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E06 RID: 77318 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E07 RID: 77319 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012E08 RID: 77320 RVA: 0x003004B1 File Offset: 0x002FE6B1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "sp3d" == name)
			{
				return new Shape3DType();
			}
			if (10 == namespaceId && "flatTx" == name)
			{
				return new FlatText();
			}
			return null;
		}

		// Token: 0x17005CFC RID: 23804
		// (get) Token: 0x06012E09 RID: 77321 RVA: 0x003004E4 File Offset: 0x002FE6E4
		internal override string[] ElementTagNames
		{
			get
			{
				return TextProperties.eleTagNames;
			}
		}

		// Token: 0x17005CFD RID: 23805
		// (get) Token: 0x06012E0A RID: 77322 RVA: 0x003004EB File Offset: 0x002FE6EB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005CFE RID: 23806
		// (get) Token: 0x06012E0B RID: 77323 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005CFF RID: 23807
		// (get) Token: 0x06012E0C RID: 77324 RVA: 0x003004F2 File Offset: 0x002FE6F2
		// (set) Token: 0x06012E0D RID: 77325 RVA: 0x003004FB File Offset: 0x002FE6FB
		public Shape3DType Shape3DType
		{
			get
			{
				return base.GetElement<Shape3DType>(0);
			}
			set
			{
				base.SetElement<Shape3DType>(0, value);
			}
		}

		// Token: 0x17005D00 RID: 23808
		// (get) Token: 0x06012E0E RID: 77326 RVA: 0x00300505 File Offset: 0x002FE705
		// (set) Token: 0x06012E0F RID: 77327 RVA: 0x0030050E File Offset: 0x002FE70E
		public FlatText FlatText
		{
			get
			{
				return base.GetElement<FlatText>(1);
			}
			set
			{
				base.SetElement<FlatText>(1, value);
			}
		}

		// Token: 0x06012E10 RID: 77328 RVA: 0x00300518 File Offset: 0x002FE718
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextProperties>(deep);
		}

		// Token: 0x04008204 RID: 33284
		private const string tagName = "txPr";

		// Token: 0x04008205 RID: 33285
		private const byte tagNsId = 14;

		// Token: 0x04008206 RID: 33286
		internal const int ElementTypeIdConst = 10683;

		// Token: 0x04008207 RID: 33287
		private static readonly string[] eleTagNames = new string[] { "sp3d", "flatTx" };

		// Token: 0x04008208 RID: 33288
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
