using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A62 RID: 10850
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineReference))]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x17007267 RID: 29287
		// (get) Token: 0x06015E93 RID: 89747 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17007268 RID: 29288
		// (get) Token: 0x06015E94 RID: 89748 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007269 RID: 29289
		// (get) Token: 0x06015E95 RID: 89749 RVA: 0x0032477E File Offset: 0x0032297E
		internal override int ElementTypeId
		{
			get
			{
				return 12268;
			}
		}

		// Token: 0x06015E96 RID: 89750 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015E97 RID: 89751 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x06015E98 RID: 89752 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E99 RID: 89753 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E9A RID: 89754 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E9B RID: 89755 RVA: 0x00324788 File Offset: 0x00322988
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

		// Token: 0x1700726A RID: 29290
		// (get) Token: 0x06015E9C RID: 89756 RVA: 0x003247F6 File Offset: 0x003229F6
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x1700726B RID: 29291
		// (get) Token: 0x06015E9D RID: 89757 RVA: 0x003247FD File Offset: 0x003229FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x1700726C RID: 29292
		// (get) Token: 0x06015E9E RID: 89758 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700726D RID: 29293
		// (get) Token: 0x06015E9F RID: 89759 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x06015EA0 RID: 89760 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x1700726E RID: 29294
		// (get) Token: 0x06015EA1 RID: 89761 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06015EA2 RID: 89762 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x1700726F RID: 29295
		// (get) Token: 0x06015EA3 RID: 89763 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x06015EA4 RID: 89764 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x17007270 RID: 29296
		// (get) Token: 0x06015EA5 RID: 89765 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x06015EA6 RID: 89766 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x06015EA7 RID: 89767 RVA: 0x00324804 File Offset: 0x00322A04
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x04009562 RID: 38242
		private const string tagName = "style";

		// Token: 0x04009563 RID: 38243
		private const byte tagNsId = 24;

		// Token: 0x04009564 RID: 38244
		internal const int ElementTypeIdConst = 12268;

		// Token: 0x04009565 RID: 38245
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x04009566 RID: 38246
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
