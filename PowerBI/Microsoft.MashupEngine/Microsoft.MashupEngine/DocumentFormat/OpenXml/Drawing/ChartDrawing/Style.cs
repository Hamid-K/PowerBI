using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002632 RID: 9778
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(LineReference))]
	internal class Style : OpenXmlCompositeElement
	{
		// Token: 0x17005A64 RID: 23140
		// (get) Token: 0x0601280A RID: 75786 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17005A65 RID: 23141
		// (get) Token: 0x0601280B RID: 75787 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A66 RID: 23142
		// (get) Token: 0x0601280C RID: 75788 RVA: 0x002FC06A File Offset: 0x002FA26A
		internal override int ElementTypeId
		{
			get
			{
				return 10597;
			}
		}

		// Token: 0x0601280D RID: 75789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601280E RID: 75790 RVA: 0x00293ECF File Offset: 0x002920CF
		public Style()
		{
		}

		// Token: 0x0601280F RID: 75791 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Style(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012810 RID: 75792 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Style(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012811 RID: 75793 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Style(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012812 RID: 75794 RVA: 0x002FC074 File Offset: 0x002FA274
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

		// Token: 0x17005A67 RID: 23143
		// (get) Token: 0x06012813 RID: 75795 RVA: 0x002FC0E2 File Offset: 0x002FA2E2
		internal override string[] ElementTagNames
		{
			get
			{
				return Style.eleTagNames;
			}
		}

		// Token: 0x17005A68 RID: 23144
		// (get) Token: 0x06012814 RID: 75796 RVA: 0x002FC0E9 File Offset: 0x002FA2E9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Style.eleNamespaceIds;
			}
		}

		// Token: 0x17005A69 RID: 23145
		// (get) Token: 0x06012815 RID: 75797 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A6A RID: 23146
		// (get) Token: 0x06012816 RID: 75798 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x06012817 RID: 75799 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x17005A6B RID: 23147
		// (get) Token: 0x06012818 RID: 75800 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06012819 RID: 75801 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x17005A6C RID: 23148
		// (get) Token: 0x0601281A RID: 75802 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x0601281B RID: 75803 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x17005A6D RID: 23149
		// (get) Token: 0x0601281C RID: 75804 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x0601281D RID: 75805 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x0601281E RID: 75806 RVA: 0x002FC0F0 File Offset: 0x002FA2F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x0400806A RID: 32874
		private const string tagName = "style";

		// Token: 0x0400806B RID: 32875
		private const byte tagNsId = 12;

		// Token: 0x0400806C RID: 32876
		internal const int ElementTypeIdConst = 10597;

		// Token: 0x0400806D RID: 32877
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x0400806E RID: 32878
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
