using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A79 RID: 10873
	[ChildElementInfo(typeof(TitleStyle))]
	[ChildElementInfo(typeof(OtherStyle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BodyStyle))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class TextStyles : OpenXmlCompositeElement
	{
		// Token: 0x1700732A RID: 29482
		// (get) Token: 0x0601604B RID: 90187 RVA: 0x00325B81 File Offset: 0x00323D81
		public override string LocalName
		{
			get
			{
				return "txStyles";
			}
		}

		// Token: 0x1700732B RID: 29483
		// (get) Token: 0x0601604C RID: 90188 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700732C RID: 29484
		// (get) Token: 0x0601604D RID: 90189 RVA: 0x00325B88 File Offset: 0x00323D88
		internal override int ElementTypeId
		{
			get
			{
				return 12288;
			}
		}

		// Token: 0x0601604E RID: 90190 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601604F RID: 90191 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextStyles()
		{
		}

		// Token: 0x06016050 RID: 90192 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextStyles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016051 RID: 90193 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextStyles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016052 RID: 90194 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextStyles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016053 RID: 90195 RVA: 0x00325B90 File Offset: 0x00323D90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "titleStyle" == name)
			{
				return new TitleStyle();
			}
			if (24 == namespaceId && "bodyStyle" == name)
			{
				return new BodyStyle();
			}
			if (24 == namespaceId && "otherStyle" == name)
			{
				return new OtherStyle();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700732D RID: 29485
		// (get) Token: 0x06016054 RID: 90196 RVA: 0x00325BFE File Offset: 0x00323DFE
		internal override string[] ElementTagNames
		{
			get
			{
				return TextStyles.eleTagNames;
			}
		}

		// Token: 0x1700732E RID: 29486
		// (get) Token: 0x06016055 RID: 90197 RVA: 0x00325C05 File Offset: 0x00323E05
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextStyles.eleNamespaceIds;
			}
		}

		// Token: 0x1700732F RID: 29487
		// (get) Token: 0x06016056 RID: 90198 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007330 RID: 29488
		// (get) Token: 0x06016057 RID: 90199 RVA: 0x00325C0C File Offset: 0x00323E0C
		// (set) Token: 0x06016058 RID: 90200 RVA: 0x00325C15 File Offset: 0x00323E15
		public TitleStyle TitleStyle
		{
			get
			{
				return base.GetElement<TitleStyle>(0);
			}
			set
			{
				base.SetElement<TitleStyle>(0, value);
			}
		}

		// Token: 0x17007331 RID: 29489
		// (get) Token: 0x06016059 RID: 90201 RVA: 0x00325C1F File Offset: 0x00323E1F
		// (set) Token: 0x0601605A RID: 90202 RVA: 0x00325C28 File Offset: 0x00323E28
		public BodyStyle BodyStyle
		{
			get
			{
				return base.GetElement<BodyStyle>(1);
			}
			set
			{
				base.SetElement<BodyStyle>(1, value);
			}
		}

		// Token: 0x17007332 RID: 29490
		// (get) Token: 0x0601605B RID: 90203 RVA: 0x00325C32 File Offset: 0x00323E32
		// (set) Token: 0x0601605C RID: 90204 RVA: 0x00325C3B File Offset: 0x00323E3B
		public OtherStyle OtherStyle
		{
			get
			{
				return base.GetElement<OtherStyle>(2);
			}
			set
			{
				base.SetElement<OtherStyle>(2, value);
			}
		}

		// Token: 0x17007333 RID: 29491
		// (get) Token: 0x0601605D RID: 90205 RVA: 0x00325C45 File Offset: 0x00323E45
		// (set) Token: 0x0601605E RID: 90206 RVA: 0x00325C4E File Offset: 0x00323E4E
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x0601605F RID: 90207 RVA: 0x00325C58 File Offset: 0x00323E58
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextStyles>(deep);
		}

		// Token: 0x040095D2 RID: 38354
		private const string tagName = "txStyles";

		// Token: 0x040095D3 RID: 38355
		private const byte tagNsId = 24;

		// Token: 0x040095D4 RID: 38356
		internal const int ElementTypeIdConst = 12288;

		// Token: 0x040095D5 RID: 38357
		private static readonly string[] eleTagNames = new string[] { "titleStyle", "bodyStyle", "otherStyle", "extLst" };

		// Token: 0x040095D6 RID: 38358
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24 };
	}
}
