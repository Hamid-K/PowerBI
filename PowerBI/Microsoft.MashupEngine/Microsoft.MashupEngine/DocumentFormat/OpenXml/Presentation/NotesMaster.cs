using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A03 RID: 10755
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NotesStyle))]
	[ChildElementInfo(typeof(CommonSlideData))]
	[ChildElementInfo(typeof(ColorMap))]
	internal class NotesMaster : OpenXmlPartRootElement
	{
		// Token: 0x17006F42 RID: 28482
		// (get) Token: 0x060157AC RID: 87980 RVA: 0x002AA5CE File Offset: 0x002A87CE
		public override string LocalName
		{
			get
			{
				return "notesMaster";
			}
		}

		// Token: 0x17006F43 RID: 28483
		// (get) Token: 0x060157AD RID: 87981 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F44 RID: 28484
		// (get) Token: 0x060157AE RID: 87982 RVA: 0x0031FAE4 File Offset: 0x0031DCE4
		internal override int ElementTypeId
		{
			get
			{
				return 12182;
			}
		}

		// Token: 0x060157AF RID: 87983 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060157B0 RID: 87984 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal NotesMaster(NotesMasterPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060157B1 RID: 87985 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(NotesMasterPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F45 RID: 28485
		// (get) Token: 0x060157B2 RID: 87986 RVA: 0x0031FAEB File Offset: 0x0031DCEB
		// (set) Token: 0x060157B3 RID: 87987 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public NotesMasterPart NotesMasterPart
		{
			get
			{
				return base.OpenXmlPart as NotesMasterPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060157B4 RID: 87988 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public NotesMaster(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157B5 RID: 87989 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public NotesMaster(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157B6 RID: 87990 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public NotesMaster(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060157B7 RID: 87991 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public NotesMaster()
		{
		}

		// Token: 0x060157B8 RID: 87992 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(NotesMasterPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060157B9 RID: 87993 RVA: 0x0031FAF8 File Offset: 0x0031DCF8
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
			if (24 == namespaceId && "notesStyle" == name)
			{
				return new NotesStyle();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17006F46 RID: 28486
		// (get) Token: 0x060157BA RID: 87994 RVA: 0x0031FB7E File Offset: 0x0031DD7E
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesMaster.eleTagNames;
			}
		}

		// Token: 0x17006F47 RID: 28487
		// (get) Token: 0x060157BB RID: 87995 RVA: 0x0031FB85 File Offset: 0x0031DD85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesMaster.eleNamespaceIds;
			}
		}

		// Token: 0x17006F48 RID: 28488
		// (get) Token: 0x060157BC RID: 87996 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F49 RID: 28489
		// (get) Token: 0x060157BD RID: 87997 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x060157BE RID: 87998 RVA: 0x0031F3ED File Offset: 0x0031D5ED
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

		// Token: 0x17006F4A RID: 28490
		// (get) Token: 0x060157BF RID: 87999 RVA: 0x0031F890 File Offset: 0x0031DA90
		// (set) Token: 0x060157C0 RID: 88000 RVA: 0x0031F899 File Offset: 0x0031DA99
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

		// Token: 0x17006F4B RID: 28491
		// (get) Token: 0x060157C1 RID: 88001 RVA: 0x0031FA64 File Offset: 0x0031DC64
		// (set) Token: 0x060157C2 RID: 88002 RVA: 0x0031FA6D File Offset: 0x0031DC6D
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

		// Token: 0x17006F4C RID: 28492
		// (get) Token: 0x060157C3 RID: 88003 RVA: 0x0031FB8C File Offset: 0x0031DD8C
		// (set) Token: 0x060157C4 RID: 88004 RVA: 0x0031FB95 File Offset: 0x0031DD95
		public NotesStyle NotesStyle
		{
			get
			{
				return base.GetElement<NotesStyle>(3);
			}
			set
			{
				base.SetElement<NotesStyle>(3, value);
			}
		}

		// Token: 0x17006F4D RID: 28493
		// (get) Token: 0x060157C5 RID: 88005 RVA: 0x0031FB9F File Offset: 0x0031DD9F
		// (set) Token: 0x060157C6 RID: 88006 RVA: 0x0031FBA8 File Offset: 0x0031DDA8
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

		// Token: 0x060157C7 RID: 88007 RVA: 0x0031FBB2 File Offset: 0x0031DDB2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesMaster>(deep);
		}

		// Token: 0x0400938C RID: 37772
		private const string tagName = "notesMaster";

		// Token: 0x0400938D RID: 37773
		private const byte tagNsId = 24;

		// Token: 0x0400938E RID: 37774
		internal const int ElementTypeIdConst = 12182;

		// Token: 0x0400938F RID: 37775
		private static readonly string[] eleTagNames = new string[] { "cSld", "clrMap", "hf", "notesStyle", "extLst" };

		// Token: 0x04009390 RID: 37776
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
	}
}
