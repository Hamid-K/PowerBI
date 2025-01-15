using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A9 RID: 10153
	[ChildElementInfo(typeof(LineReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectReference))]
	[ChildElementInfo(typeof(FontReference))]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x170062C3 RID: 25283
		// (get) Token: 0x06013ADD RID: 80605 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x170062C4 RID: 25284
		// (get) Token: 0x06013ADE RID: 80606 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062C5 RID: 25285
		// (get) Token: 0x06013ADF RID: 80607 RVA: 0x0030AB10 File Offset: 0x00308D10
		internal override int ElementTypeId
		{
			get
			{
				return 10186;
			}
		}

		// Token: 0x06013AE0 RID: 80608 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013AE1 RID: 80609 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x06013AE2 RID: 80610 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AE3 RID: 80611 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AE4 RID: 80612 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013AE5 RID: 80613 RVA: 0x0030AB18 File Offset: 0x00308D18
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

		// Token: 0x170062C6 RID: 25286
		// (get) Token: 0x06013AE6 RID: 80614 RVA: 0x0030AB86 File Offset: 0x00308D86
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x170062C7 RID: 25287
		// (get) Token: 0x06013AE7 RID: 80615 RVA: 0x0030AB8D File Offset: 0x00308D8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x170062C8 RID: 25288
		// (get) Token: 0x06013AE8 RID: 80616 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062C9 RID: 25289
		// (get) Token: 0x06013AE9 RID: 80617 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x06013AEA RID: 80618 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
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

		// Token: 0x170062CA RID: 25290
		// (get) Token: 0x06013AEB RID: 80619 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06013AEC RID: 80620 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
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

		// Token: 0x170062CB RID: 25291
		// (get) Token: 0x06013AED RID: 80621 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x06013AEE RID: 80622 RVA: 0x002DEFFB File Offset: 0x002DD1FB
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

		// Token: 0x170062CC RID: 25292
		// (get) Token: 0x06013AEF RID: 80623 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x06013AF0 RID: 80624 RVA: 0x002DF00E File Offset: 0x002DD20E
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

		// Token: 0x06013AF1 RID: 80625 RVA: 0x0030AB94 File Offset: 0x00308D94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x04008745 RID: 34629
		private const string tagName = "style";

		// Token: 0x04008746 RID: 34630
		private const byte tagNsId = 10;

		// Token: 0x04008747 RID: 34631
		internal const int ElementTypeIdConst = 10186;

		// Token: 0x04008748 RID: 34632
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x04008749 RID: 34633
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
