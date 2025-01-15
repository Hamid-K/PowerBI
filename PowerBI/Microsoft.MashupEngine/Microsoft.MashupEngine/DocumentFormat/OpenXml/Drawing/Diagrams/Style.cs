using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267F RID: 9855
	[ChildElementInfo(typeof(LineReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	internal class Style : OpenXmlCompositeElement
	{
		// Token: 0x17005C97 RID: 23703
		// (get) Token: 0x06012D37 RID: 77111 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17005C98 RID: 23704
		// (get) Token: 0x06012D38 RID: 77112 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C99 RID: 23705
		// (get) Token: 0x06012D39 RID: 77113 RVA: 0x002FFCEE File Offset: 0x002FDEEE
		internal override int ElementTypeId
		{
			get
			{
				return 10670;
			}
		}

		// Token: 0x06012D3A RID: 77114 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012D3B RID: 77115 RVA: 0x00293ECF File Offset: 0x002920CF
		public Style()
		{
		}

		// Token: 0x06012D3C RID: 77116 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Style(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D3D RID: 77117 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Style(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D3E RID: 77118 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Style(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D3F RID: 77119 RVA: 0x002FFCF8 File Offset: 0x002FDEF8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "lnRef" == name)
			{
				return new LineReference();
			}
			if (10 == namespaceId && "fillRef" == name)
			{
				return new FillReference();
			}
			if (10 == namespaceId && "effectRef" == name)
			{
				return new EffectReference();
			}
			if (10 == namespaceId && "fontRef" == name)
			{
				return new FontReference();
			}
			return null;
		}

		// Token: 0x17005C9A RID: 23706
		// (get) Token: 0x06012D40 RID: 77120 RVA: 0x002FFD66 File Offset: 0x002FDF66
		internal override string[] ElementTagNames
		{
			get
			{
				return Style.eleTagNames;
			}
		}

		// Token: 0x17005C9B RID: 23707
		// (get) Token: 0x06012D41 RID: 77121 RVA: 0x002FFD6D File Offset: 0x002FDF6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Style.eleNamespaceIds;
			}
		}

		// Token: 0x17005C9C RID: 23708
		// (get) Token: 0x06012D42 RID: 77122 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C9D RID: 23709
		// (get) Token: 0x06012D43 RID: 77123 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x06012D44 RID: 77124 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
		public LineReference LineReference
		{
			get
			{
				return base.GetElement<LineReference>(0);
			}
			set
			{
				base.SetElement<LineReference>(0, value);
			}
		}

		// Token: 0x17005C9E RID: 23710
		// (get) Token: 0x06012D45 RID: 77125 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06012D46 RID: 77126 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
		public FillReference FillReference
		{
			get
			{
				return base.GetElement<FillReference>(1);
			}
			set
			{
				base.SetElement<FillReference>(1, value);
			}
		}

		// Token: 0x17005C9F RID: 23711
		// (get) Token: 0x06012D47 RID: 77127 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x06012D48 RID: 77128 RVA: 0x002DEFFB File Offset: 0x002DD1FB
		public EffectReference EffectReference
		{
			get
			{
				return base.GetElement<EffectReference>(2);
			}
			set
			{
				base.SetElement<EffectReference>(2, value);
			}
		}

		// Token: 0x17005CA0 RID: 23712
		// (get) Token: 0x06012D49 RID: 77129 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x06012D4A RID: 77130 RVA: 0x002DF00E File Offset: 0x002DD20E
		public FontReference FontReference
		{
			get
			{
				return base.GetElement<FontReference>(3);
			}
			set
			{
				base.SetElement<FontReference>(3, value);
			}
		}

		// Token: 0x06012D4B RID: 77131 RVA: 0x002FFD74 File Offset: 0x002FDF74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x040081C1 RID: 33217
		private const string tagName = "style";

		// Token: 0x040081C2 RID: 33218
		private const byte tagNsId = 14;

		// Token: 0x040081C3 RID: 33219
		internal const int ElementTypeIdConst = 10670;

		// Token: 0x040081C4 RID: 33220
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x040081C5 RID: 33221
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
