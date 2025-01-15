using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FF RID: 9471
	[ChildElementInfo(typeof(LineReference))]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x170053D6 RID: 21462
		// (get) Token: 0x06011997 RID: 72087 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x170053D7 RID: 21463
		// (get) Token: 0x06011998 RID: 72088 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053D8 RID: 21464
		// (get) Token: 0x06011999 RID: 72089 RVA: 0x002F0606 File Offset: 0x002EE806
		internal override int ElementTypeId
		{
			get
			{
				return 13137;
			}
		}

		// Token: 0x0601199A RID: 72090 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601199B RID: 72091 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x0601199C RID: 72092 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601199D RID: 72093 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601199E RID: 72094 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601199F RID: 72095 RVA: 0x002F0610 File Offset: 0x002EE810
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

		// Token: 0x170053D9 RID: 21465
		// (get) Token: 0x060119A0 RID: 72096 RVA: 0x002F067E File Offset: 0x002EE87E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x170053DA RID: 21466
		// (get) Token: 0x060119A1 RID: 72097 RVA: 0x002F0685 File Offset: 0x002EE885
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x170053DB RID: 21467
		// (get) Token: 0x060119A2 RID: 72098 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053DC RID: 21468
		// (get) Token: 0x060119A3 RID: 72099 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x060119A4 RID: 72100 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x170053DD RID: 21469
		// (get) Token: 0x060119A5 RID: 72101 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x060119A6 RID: 72102 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x170053DE RID: 21470
		// (get) Token: 0x060119A7 RID: 72103 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x060119A8 RID: 72104 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x170053DF RID: 21471
		// (get) Token: 0x060119A9 RID: 72105 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x060119AA RID: 72106 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x060119AB RID: 72107 RVA: 0x002F068C File Offset: 0x002EE88C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x04007B7D RID: 31613
		private const string tagName = "style";

		// Token: 0x04007B7E RID: 31614
		private const byte tagNsId = 61;

		// Token: 0x04007B7F RID: 31615
		internal const int ElementTypeIdConst = 13137;

		// Token: 0x04007B80 RID: 31616
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x04007B81 RID: 31617
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
