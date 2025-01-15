using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002882 RID: 10370
	[ChildElementInfo(typeof(LineReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x17006744 RID: 26436
		// (get) Token: 0x06014555 RID: 83285 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17006745 RID: 26437
		// (get) Token: 0x06014556 RID: 83286 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006746 RID: 26438
		// (get) Token: 0x06014557 RID: 83287 RVA: 0x003123AA File Offset: 0x003105AA
		internal override int ElementTypeId
		{
			get
			{
				return 10732;
			}
		}

		// Token: 0x06014558 RID: 83288 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014559 RID: 83289 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x0601455A RID: 83290 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601455B RID: 83291 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601455C RID: 83292 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601455D RID: 83293 RVA: 0x003123B4 File Offset: 0x003105B4
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

		// Token: 0x17006747 RID: 26439
		// (get) Token: 0x0601455E RID: 83294 RVA: 0x00312422 File Offset: 0x00310622
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x17006748 RID: 26440
		// (get) Token: 0x0601455F RID: 83295 RVA: 0x00312429 File Offset: 0x00310629
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17006749 RID: 26441
		// (get) Token: 0x06014560 RID: 83296 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700674A RID: 26442
		// (get) Token: 0x06014561 RID: 83297 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x06014562 RID: 83298 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x1700674B RID: 26443
		// (get) Token: 0x06014563 RID: 83299 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06014564 RID: 83300 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x1700674C RID: 26444
		// (get) Token: 0x06014565 RID: 83301 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x06014566 RID: 83302 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x1700674D RID: 26445
		// (get) Token: 0x06014567 RID: 83303 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x06014568 RID: 83304 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x06014569 RID: 83305 RVA: 0x00312430 File Offset: 0x00310630
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x04008D9F RID: 36255
		private const string tagName = "style";

		// Token: 0x04008DA0 RID: 36256
		private const byte tagNsId = 18;

		// Token: 0x04008DA1 RID: 36257
		internal const int ElementTypeIdConst = 10732;

		// Token: 0x04008DA2 RID: 36258
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x04008DA3 RID: 36259
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
