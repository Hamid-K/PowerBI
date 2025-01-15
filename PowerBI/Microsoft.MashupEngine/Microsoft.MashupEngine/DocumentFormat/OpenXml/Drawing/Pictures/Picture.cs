using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Pictures;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002870 RID: 10352
	[ChildElementInfo(typeof(BlipFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShapeStyle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualPictureProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x17006680 RID: 26240
		// (get) Token: 0x060143AF RID: 82863 RVA: 0x002FB9AA File Offset: 0x002F9BAA
		public override string LocalName
		{
			get
			{
				return "pic";
			}
		}

		// Token: 0x17006681 RID: 26241
		// (get) Token: 0x060143B0 RID: 82864 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x17006682 RID: 26242
		// (get) Token: 0x060143B1 RID: 82865 RVA: 0x00310AC4 File Offset: 0x0030ECC4
		internal override int ElementTypeId
		{
			get
			{
				return 10714;
			}
		}

		// Token: 0x060143B2 RID: 82866 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060143B3 RID: 82867 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x060143B4 RID: 82868 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143B5 RID: 82869 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143B6 RID: 82870 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060143B7 RID: 82871 RVA: 0x00310ACC File Offset: 0x0030ECCC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (17 == namespaceId && "nvPicPr" == name)
			{
				return new NonVisualPictureProperties();
			}
			if (17 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (17 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (50 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (50 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17006683 RID: 26243
		// (get) Token: 0x060143B8 RID: 82872 RVA: 0x00310B52 File Offset: 0x0030ED52
		internal override string[] ElementTagNames
		{
			get
			{
				return Picture.eleTagNames;
			}
		}

		// Token: 0x17006684 RID: 26244
		// (get) Token: 0x060143B9 RID: 82873 RVA: 0x00310B59 File Offset: 0x0030ED59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Picture.eleNamespaceIds;
			}
		}

		// Token: 0x17006685 RID: 26245
		// (get) Token: 0x060143BA RID: 82874 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006686 RID: 26246
		// (get) Token: 0x060143BB RID: 82875 RVA: 0x00310B60 File Offset: 0x0030ED60
		// (set) Token: 0x060143BC RID: 82876 RVA: 0x00310B69 File Offset: 0x0030ED69
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

		// Token: 0x17006687 RID: 26247
		// (get) Token: 0x060143BD RID: 82877 RVA: 0x00310B73 File Offset: 0x0030ED73
		// (set) Token: 0x060143BE RID: 82878 RVA: 0x00310B7C File Offset: 0x0030ED7C
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

		// Token: 0x17006688 RID: 26248
		// (get) Token: 0x060143BF RID: 82879 RVA: 0x00310B86 File Offset: 0x0030ED86
		// (set) Token: 0x060143C0 RID: 82880 RVA: 0x00310B8F File Offset: 0x0030ED8F
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

		// Token: 0x17006689 RID: 26249
		// (get) Token: 0x060143C1 RID: 82881 RVA: 0x00310B99 File Offset: 0x0030ED99
		// (set) Token: 0x060143C2 RID: 82882 RVA: 0x00310BA2 File Offset: 0x0030EDA2
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x1700668A RID: 26250
		// (get) Token: 0x060143C3 RID: 82883 RVA: 0x00310BAC File Offset: 0x0030EDAC
		// (set) Token: 0x060143C4 RID: 82884 RVA: 0x00310BB5 File Offset: 0x0030EDB5
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(4);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(4, value);
			}
		}

		// Token: 0x060143C5 RID: 82885 RVA: 0x00310BBF File Offset: 0x0030EDBF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x04008D31 RID: 36145
		private const string tagName = "pic";

		// Token: 0x04008D32 RID: 36146
		private const byte tagNsId = 17;

		// Token: 0x04008D33 RID: 36147
		internal const int ElementTypeIdConst = 10714;

		// Token: 0x04008D34 RID: 36148
		private static readonly string[] eleTagNames = new string[] { "nvPicPr", "blipFill", "spPr", "style", "extLst" };

		// Token: 0x04008D35 RID: 36149
		private static readonly byte[] eleNamespaceIds = new byte[] { 17, 17, 17, 50, 50 };
	}
}
