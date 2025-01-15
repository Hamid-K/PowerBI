using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F8 RID: 9464
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualGraphicFrameProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GraphicFrame : OpenXmlCompositeElement
	{
		// Token: 0x1700538F RID: 21391
		// (get) Token: 0x060118FD RID: 71933 RVA: 0x002EFCFA File Offset: 0x002EDEFA
		public override string LocalName
		{
			get
			{
				return "graphicFrame";
			}
		}

		// Token: 0x17005390 RID: 21392
		// (get) Token: 0x060118FE RID: 71934 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005391 RID: 21393
		// (get) Token: 0x060118FF RID: 71935 RVA: 0x002EFD01 File Offset: 0x002EDF01
		internal override int ElementTypeId
		{
			get
			{
				return 13130;
			}
		}

		// Token: 0x06011900 RID: 71936 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011901 RID: 71937 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrame()
		{
		}

		// Token: 0x06011902 RID: 71938 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011903 RID: 71939 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011904 RID: 71940 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011905 RID: 71941 RVA: 0x002EFD08 File Offset: 0x002EDF08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (60 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (60 == namespaceId && "cNvFrPr" == name)
			{
				return new NonVisualGraphicFrameProperties();
			}
			if (60 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			if (60 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17005392 RID: 21394
		// (get) Token: 0x06011906 RID: 71942 RVA: 0x002EFD8E File Offset: 0x002EDF8E
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrame.eleTagNames;
			}
		}

		// Token: 0x17005393 RID: 21395
		// (get) Token: 0x06011907 RID: 71943 RVA: 0x002EFD95 File Offset: 0x002EDF95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrame.eleNamespaceIds;
			}
		}

		// Token: 0x17005394 RID: 21396
		// (get) Token: 0x06011908 RID: 71944 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005395 RID: 21397
		// (get) Token: 0x06011909 RID: 71945 RVA: 0x002EF658 File Offset: 0x002ED858
		// (set) Token: 0x0601190A RID: 71946 RVA: 0x002EF661 File Offset: 0x002ED861
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x17005396 RID: 21398
		// (get) Token: 0x0601190B RID: 71947 RVA: 0x002EFD9C File Offset: 0x002EDF9C
		// (set) Token: 0x0601190C RID: 71948 RVA: 0x002EFDA5 File Offset: 0x002EDFA5
		public NonVisualGraphicFrameProperties NonVisualGraphicFrameProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameProperties>(1, value);
			}
		}

		// Token: 0x17005397 RID: 21399
		// (get) Token: 0x0601190D RID: 71949 RVA: 0x002EFDAF File Offset: 0x002EDFAF
		// (set) Token: 0x0601190E RID: 71950 RVA: 0x002EFDB8 File Offset: 0x002EDFB8
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(2);
			}
			set
			{
				base.SetElement<Transform2D>(2, value);
			}
		}

		// Token: 0x17005398 RID: 21400
		// (get) Token: 0x0601190F RID: 71951 RVA: 0x002EFDC2 File Offset: 0x002EDFC2
		// (set) Token: 0x06011910 RID: 71952 RVA: 0x002EFDCB File Offset: 0x002EDFCB
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(3);
			}
			set
			{
				base.SetElement<Graphic>(3, value);
			}
		}

		// Token: 0x17005399 RID: 21401
		// (get) Token: 0x06011911 RID: 71953 RVA: 0x002EFDD5 File Offset: 0x002EDFD5
		// (set) Token: 0x06011912 RID: 71954 RVA: 0x002EFDDE File Offset: 0x002EDFDE
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

		// Token: 0x06011913 RID: 71955 RVA: 0x002EFDE8 File Offset: 0x002EDFE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrame>(deep);
		}

		// Token: 0x04007B54 RID: 31572
		private const string tagName = "graphicFrame";

		// Token: 0x04007B55 RID: 31573
		private const byte tagNsId = 60;

		// Token: 0x04007B56 RID: 31574
		internal const int ElementTypeIdConst = 13130;

		// Token: 0x04007B57 RID: 31575
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvFrPr", "xfrm", "graphic", "extLst" };

		// Token: 0x04007B58 RID: 31576
		private static readonly byte[] eleNamespaceIds = new byte[] { 60, 60, 60, 10, 60 };
	}
}
