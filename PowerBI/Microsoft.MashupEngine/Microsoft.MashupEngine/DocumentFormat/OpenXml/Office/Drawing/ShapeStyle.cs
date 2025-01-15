using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232A RID: 9002
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	[ChildElementInfo(typeof(LineReference))]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x170048A7 RID: 18599
		// (get) Token: 0x060100A8 RID: 65704 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x170048A8 RID: 18600
		// (get) Token: 0x060100A9 RID: 65705 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048A9 RID: 18601
		// (get) Token: 0x060100AA RID: 65706 RVA: 0x002DEF46 File Offset: 0x002DD146
		internal override int ElementTypeId
		{
			get
			{
				return 13025;
			}
		}

		// Token: 0x060100AB RID: 65707 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060100AC RID: 65708 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x060100AD RID: 65709 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100AE RID: 65710 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100AF RID: 65711 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060100B0 RID: 65712 RVA: 0x002DEF50 File Offset: 0x002DD150
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

		// Token: 0x170048AA RID: 18602
		// (get) Token: 0x060100B1 RID: 65713 RVA: 0x002DEFBE File Offset: 0x002DD1BE
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x170048AB RID: 18603
		// (get) Token: 0x060100B2 RID: 65714 RVA: 0x002DEFC5 File Offset: 0x002DD1C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x170048AC RID: 18604
		// (get) Token: 0x060100B3 RID: 65715 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048AD RID: 18605
		// (get) Token: 0x060100B4 RID: 65716 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x060100B5 RID: 65717 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x170048AE RID: 18606
		// (get) Token: 0x060100B6 RID: 65718 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x060100B7 RID: 65719 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x170048AF RID: 18607
		// (get) Token: 0x060100B8 RID: 65720 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x060100B9 RID: 65721 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x170048B0 RID: 18608
		// (get) Token: 0x060100BA RID: 65722 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x060100BB RID: 65723 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x060100BC RID: 65724 RVA: 0x002DF018 File Offset: 0x002DD218
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x040072D4 RID: 29396
		private const string tagName = "style";

		// Token: 0x040072D5 RID: 29397
		private const byte tagNsId = 56;

		// Token: 0x040072D6 RID: 29398
		internal const int ElementTypeIdConst = 13025;

		// Token: 0x040072D7 RID: 29399
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x040072D8 RID: 29400
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
