using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A95 RID: 10901
	[ChildElementInfo(typeof(NonVisualPictureProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x170073EA RID: 29674
		// (get) Token: 0x060161FD RID: 90621 RVA: 0x002FB9AA File Offset: 0x002F9BAA
		public override string LocalName
		{
			get
			{
				return "pic";
			}
		}

		// Token: 0x170073EB RID: 29675
		// (get) Token: 0x060161FE RID: 90622 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073EC RID: 29676
		// (get) Token: 0x060161FF RID: 90623 RVA: 0x00326BC7 File Offset: 0x00324DC7
		internal override int ElementTypeId
		{
			get
			{
				return 12316;
			}
		}

		// Token: 0x06016200 RID: 90624 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016201 RID: 90625 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x06016202 RID: 90626 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016203 RID: 90627 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016204 RID: 90628 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016205 RID: 90629 RVA: 0x00326BD0 File Offset: 0x00324DD0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "nvPicPr" == name)
			{
				return new NonVisualPictureProperties();
			}
			if (24 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (24 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (24 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x170073ED RID: 29677
		// (get) Token: 0x06016206 RID: 90630 RVA: 0x00326C56 File Offset: 0x00324E56
		internal override string[] ElementTagNames
		{
			get
			{
				return Picture.eleTagNames;
			}
		}

		// Token: 0x170073EE RID: 29678
		// (get) Token: 0x06016207 RID: 90631 RVA: 0x00326C5D File Offset: 0x00324E5D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Picture.eleNamespaceIds;
			}
		}

		// Token: 0x170073EF RID: 29679
		// (get) Token: 0x06016208 RID: 90632 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073F0 RID: 29680
		// (get) Token: 0x06016209 RID: 90633 RVA: 0x00326C64 File Offset: 0x00324E64
		// (set) Token: 0x0601620A RID: 90634 RVA: 0x00326C6D File Offset: 0x00324E6D
		public NonVisualPictureProperties NonVisualPictureProperties
		{
			get
			{
				return base.GetElement<NonVisualPictureProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualPictureProperties>(0, value);
			}
		}

		// Token: 0x170073F1 RID: 29681
		// (get) Token: 0x0601620B RID: 90635 RVA: 0x00326C77 File Offset: 0x00324E77
		// (set) Token: 0x0601620C RID: 90636 RVA: 0x00326C80 File Offset: 0x00324E80
		public BlipFill BlipFill
		{
			get
			{
				return base.GetElement<BlipFill>(1);
			}
			set
			{
				base.SetElement<BlipFill>(1, value);
			}
		}

		// Token: 0x170073F2 RID: 29682
		// (get) Token: 0x0601620D RID: 90637 RVA: 0x00326C8A File Offset: 0x00324E8A
		// (set) Token: 0x0601620E RID: 90638 RVA: 0x00326C93 File Offset: 0x00324E93
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(2);
			}
			set
			{
				base.SetElement<ShapeProperties>(2, value);
			}
		}

		// Token: 0x170073F3 RID: 29683
		// (get) Token: 0x0601620F RID: 90639 RVA: 0x00326C9D File Offset: 0x00324E9D
		// (set) Token: 0x06016210 RID: 90640 RVA: 0x00326CA6 File Offset: 0x00324EA6
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(3);
			}
			set
			{
				base.SetElement<ShapeStyle>(3, value);
			}
		}

		// Token: 0x170073F4 RID: 29684
		// (get) Token: 0x06016211 RID: 90641 RVA: 0x0031FB9F File Offset: 0x0031DD9F
		// (set) Token: 0x06016212 RID: 90642 RVA: 0x0031FBA8 File Offset: 0x0031DDA8
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(4);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(4, value);
			}
		}

		// Token: 0x06016213 RID: 90643 RVA: 0x00326CB0 File Offset: 0x00324EB0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x04009654 RID: 38484
		private const string tagName = "pic";

		// Token: 0x04009655 RID: 38485
		private const byte tagNsId = 24;

		// Token: 0x04009656 RID: 38486
		internal const int ElementTypeIdConst = 12316;

		// Token: 0x04009657 RID: 38487
		private static readonly string[] eleTagNames = new string[] { "nvPicPr", "blipFill", "spPr", "style", "extLst" };

		// Token: 0x04009658 RID: 38488
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
	}
}
