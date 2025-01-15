using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283A RID: 10298
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualPictureProperties))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(ShapeProperties))]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x17006646 RID: 26182
		// (get) Token: 0x06014328 RID: 82728 RVA: 0x002FB9AA File Offset: 0x002F9BAA
		public override string LocalName
		{
			get
			{
				return "pic";
			}
		}

		// Token: 0x17006647 RID: 26183
		// (get) Token: 0x06014329 RID: 82729 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006648 RID: 26184
		// (get) Token: 0x0601432A RID: 82730 RVA: 0x0031046C File Offset: 0x0030E66C
		internal override int ElementTypeId
		{
			get
			{
				return 10334;
			}
		}

		// Token: 0x0601432B RID: 82731 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601432C RID: 82732 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x0601432D RID: 82733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601432E RID: 82734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601432F RID: 82735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014330 RID: 82736 RVA: 0x00310474 File Offset: 0x0030E674
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvPicPr" == name)
			{
				return new NonVisualPictureProperties();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (10 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006649 RID: 26185
		// (get) Token: 0x06014331 RID: 82737 RVA: 0x003104FA File Offset: 0x0030E6FA
		internal override string[] ElementTagNames
		{
			get
			{
				return Picture.eleTagNames;
			}
		}

		// Token: 0x1700664A RID: 26186
		// (get) Token: 0x06014332 RID: 82738 RVA: 0x00310501 File Offset: 0x0030E701
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Picture.eleNamespaceIds;
			}
		}

		// Token: 0x1700664B RID: 26187
		// (get) Token: 0x06014333 RID: 82739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700664C RID: 26188
		// (get) Token: 0x06014334 RID: 82740 RVA: 0x00310508 File Offset: 0x0030E708
		// (set) Token: 0x06014335 RID: 82741 RVA: 0x00310511 File Offset: 0x0030E711
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

		// Token: 0x1700664D RID: 26189
		// (get) Token: 0x06014336 RID: 82742 RVA: 0x0031051B File Offset: 0x0030E71B
		// (set) Token: 0x06014337 RID: 82743 RVA: 0x00310524 File Offset: 0x0030E724
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

		// Token: 0x1700664E RID: 26190
		// (get) Token: 0x06014338 RID: 82744 RVA: 0x0031052E File Offset: 0x0030E72E
		// (set) Token: 0x06014339 RID: 82745 RVA: 0x00310537 File Offset: 0x0030E737
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

		// Token: 0x1700664F RID: 26191
		// (get) Token: 0x0601433A RID: 82746 RVA: 0x0030CDF5 File Offset: 0x0030AFF5
		// (set) Token: 0x0601433B RID: 82747 RVA: 0x0030CDFE File Offset: 0x0030AFFE
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

		// Token: 0x17006650 RID: 26192
		// (get) Token: 0x0601433C RID: 82748 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x0601433D RID: 82749 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x0601433E RID: 82750 RVA: 0x00310541 File Offset: 0x0030E741
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x0400897A RID: 35194
		private const string tagName = "pic";

		// Token: 0x0400897B RID: 35195
		private const byte tagNsId = 10;

		// Token: 0x0400897C RID: 35196
		internal const int ElementTypeIdConst = 10334;

		// Token: 0x0400897D RID: 35197
		private static readonly string[] eleTagNames = new string[] { "nvPicPr", "blipFill", "spPr", "style", "extLst" };

		// Token: 0x0400897E RID: 35198
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
	}
}
