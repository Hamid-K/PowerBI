using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A02 RID: 10754
	[ChildElementInfo(typeof(HeaderFooter))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonSlideData))]
	[ChildElementInfo(typeof(ColorMap))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class HandoutMaster : OpenXmlPartRootElement
	{
		// Token: 0x17006F37 RID: 28471
		// (get) Token: 0x06015791 RID: 87953 RVA: 0x002A9F62 File Offset: 0x002A8162
		public override string LocalName
		{
			get
			{
				return "handoutMaster";
			}
		}

		// Token: 0x17006F38 RID: 28472
		// (get) Token: 0x06015792 RID: 87954 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F39 RID: 28473
		// (get) Token: 0x06015793 RID: 87955 RVA: 0x0031F9D2 File Offset: 0x0031DBD2
		internal override int ElementTypeId
		{
			get
			{
				return 12181;
			}
		}

		// Token: 0x06015794 RID: 87956 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015795 RID: 87957 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal HandoutMaster(HandoutMasterPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015796 RID: 87958 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(HandoutMasterPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F3A RID: 28474
		// (get) Token: 0x06015797 RID: 87959 RVA: 0x0031F9D9 File Offset: 0x0031DBD9
		// (set) Token: 0x06015798 RID: 87960 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public HandoutMasterPart HandoutMasterPart
		{
			get
			{
				return base.OpenXmlPart as HandoutMasterPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015799 RID: 87961 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public HandoutMaster(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601579A RID: 87962 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public HandoutMaster(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601579B RID: 87963 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public HandoutMaster(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601579C RID: 87964 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public HandoutMaster()
		{
		}

		// Token: 0x0601579D RID: 87965 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(HandoutMasterPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601579E RID: 87966 RVA: 0x0031F9E8 File Offset: 0x0031DBE8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSld" == name)
			{
				return new CommonSlideData();
			}
			if (24 == namespaceId && "clrMap" == name)
			{
				return new ColorMap();
			}
			if (24 == namespaceId && "hf" == name)
			{
				return new HeaderFooter();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17006F3B RID: 28475
		// (get) Token: 0x0601579F RID: 87967 RVA: 0x0031FA56 File Offset: 0x0031DC56
		internal override string[] ElementTagNames
		{
			get
			{
				return HandoutMaster.eleTagNames;
			}
		}

		// Token: 0x17006F3C RID: 28476
		// (get) Token: 0x060157A0 RID: 87968 RVA: 0x0031FA5D File Offset: 0x0031DC5D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HandoutMaster.eleNamespaceIds;
			}
		}

		// Token: 0x17006F3D RID: 28477
		// (get) Token: 0x060157A1 RID: 87969 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F3E RID: 28478
		// (get) Token: 0x060157A2 RID: 87970 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x060157A3 RID: 87971 RVA: 0x0031F3ED File Offset: 0x0031D5ED
		public CommonSlideData CommonSlideData
		{
			get
			{
				return base.GetElement<CommonSlideData>(0);
			}
			set
			{
				base.SetElement<CommonSlideData>(0, value);
			}
		}

		// Token: 0x17006F3F RID: 28479
		// (get) Token: 0x060157A4 RID: 87972 RVA: 0x0031F890 File Offset: 0x0031DA90
		// (set) Token: 0x060157A5 RID: 87973 RVA: 0x0031F899 File Offset: 0x0031DA99
		public ColorMap ColorMap
		{
			get
			{
				return base.GetElement<ColorMap>(1);
			}
			set
			{
				base.SetElement<ColorMap>(1, value);
			}
		}

		// Token: 0x17006F40 RID: 28480
		// (get) Token: 0x060157A6 RID: 87974 RVA: 0x0031FA64 File Offset: 0x0031DC64
		// (set) Token: 0x060157A7 RID: 87975 RVA: 0x0031FA6D File Offset: 0x0031DC6D
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(2);
			}
			set
			{
				base.SetElement<HeaderFooter>(2, value);
			}
		}

		// Token: 0x17006F41 RID: 28481
		// (get) Token: 0x060157A8 RID: 87976 RVA: 0x0031FA77 File Offset: 0x0031DC77
		// (set) Token: 0x060157A9 RID: 87977 RVA: 0x0031FA80 File Offset: 0x0031DC80
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(3);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(3, value);
			}
		}

		// Token: 0x060157AA RID: 87978 RVA: 0x0031FA8A File Offset: 0x0031DC8A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HandoutMaster>(deep);
		}

		// Token: 0x04009387 RID: 37767
		private const string tagName = "handoutMaster";

		// Token: 0x04009388 RID: 37768
		private const byte tagNsId = 24;

		// Token: 0x04009389 RID: 37769
		internal const int ElementTypeIdConst = 12181;

		// Token: 0x0400938A RID: 37770
		private static readonly string[] eleTagNames = new string[] { "cSld", "clrMap", "hf", "extLst" };

		// Token: 0x0400938B RID: 37771
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24 };
	}
}
