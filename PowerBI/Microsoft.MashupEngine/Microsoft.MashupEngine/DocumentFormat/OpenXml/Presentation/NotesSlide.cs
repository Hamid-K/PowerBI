using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A04 RID: 10756
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonSlideData))]
	[ChildElementInfo(typeof(ColorMapOverride))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class NotesSlide : OpenXmlPartRootElement
	{
		// Token: 0x17006F4E RID: 28494
		// (get) Token: 0x060157C9 RID: 88009 RVA: 0x0031FC14 File Offset: 0x0031DE14
		public override string LocalName
		{
			get
			{
				return "notes";
			}
		}

		// Token: 0x17006F4F RID: 28495
		// (get) Token: 0x060157CA RID: 88010 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F50 RID: 28496
		// (get) Token: 0x060157CB RID: 88011 RVA: 0x0031FC1B File Offset: 0x0031DE1B
		internal override int ElementTypeId
		{
			get
			{
				return 12183;
			}
		}

		// Token: 0x060157CC RID: 88012 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F51 RID: 28497
		// (get) Token: 0x060157CD RID: 88013 RVA: 0x0031FC22 File Offset: 0x0031DE22
		internal override string[] AttributeTagNames
		{
			get
			{
				return NotesSlide.attributeTagNames;
			}
		}

		// Token: 0x17006F52 RID: 28498
		// (get) Token: 0x060157CE RID: 88014 RVA: 0x0031FC29 File Offset: 0x0031DE29
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NotesSlide.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F53 RID: 28499
		// (get) Token: 0x060157CF RID: 88015 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060157D0 RID: 88016 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showMasterSp")]
		public BooleanValue ShowMasterShapes
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006F54 RID: 28500
		// (get) Token: 0x060157D1 RID: 88017 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060157D2 RID: 88018 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showMasterPhAnim")]
		public BooleanValue ShowMasterPlaceholderAnimations
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060157D3 RID: 88019 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal NotesSlide(NotesSlidePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060157D4 RID: 88020 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(NotesSlidePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F55 RID: 28501
		// (get) Token: 0x060157D5 RID: 88021 RVA: 0x0031FC30 File Offset: 0x0031DE30
		// (set) Token: 0x060157D6 RID: 88022 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public NotesSlidePart NotesSlidePart
		{
			get
			{
				return base.OpenXmlPart as NotesSlidePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060157D7 RID: 88023 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public NotesSlide(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157D8 RID: 88024 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public NotesSlide(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157D9 RID: 88025 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public NotesSlide(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060157DA RID: 88026 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public NotesSlide()
		{
		}

		// Token: 0x060157DB RID: 88027 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(NotesSlidePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060157DC RID: 88028 RVA: 0x0031FC40 File Offset: 0x0031DE40
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSld" == name)
			{
				return new CommonSlideData();
			}
			if (24 == namespaceId && "clrMapOvr" == name)
			{
				return new ColorMapOverride();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17006F56 RID: 28502
		// (get) Token: 0x060157DD RID: 88029 RVA: 0x0031FC96 File Offset: 0x0031DE96
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesSlide.eleTagNames;
			}
		}

		// Token: 0x17006F57 RID: 28503
		// (get) Token: 0x060157DE RID: 88030 RVA: 0x0031FC9D File Offset: 0x0031DE9D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesSlide.eleNamespaceIds;
			}
		}

		// Token: 0x17006F58 RID: 28504
		// (get) Token: 0x060157DF RID: 88031 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F59 RID: 28505
		// (get) Token: 0x060157E0 RID: 88032 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x060157E1 RID: 88033 RVA: 0x0031F3ED File Offset: 0x0031D5ED
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

		// Token: 0x17006F5A RID: 28506
		// (get) Token: 0x060157E2 RID: 88034 RVA: 0x0031F3F7 File Offset: 0x0031D5F7
		// (set) Token: 0x060157E3 RID: 88035 RVA: 0x0031F400 File Offset: 0x0031D600
		public ColorMapOverride ColorMapOverride
		{
			get
			{
				return base.GetElement<ColorMapOverride>(1);
			}
			set
			{
				base.SetElement<ColorMapOverride>(1, value);
			}
		}

		// Token: 0x17006F5B RID: 28507
		// (get) Token: 0x060157E4 RID: 88036 RVA: 0x0031FCA4 File Offset: 0x0031DEA4
		// (set) Token: 0x060157E5 RID: 88037 RVA: 0x0031FCAD File Offset: 0x0031DEAD
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(2);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(2, value);
			}
		}

		// Token: 0x060157E6 RID: 88038 RVA: 0x0031FCB7 File Offset: 0x0031DEB7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showMasterSp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showMasterPhAnim" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060157E7 RID: 88039 RVA: 0x0031FCED File Offset: 0x0031DEED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesSlide>(deep);
		}

		// Token: 0x060157E8 RID: 88040 RVA: 0x0031FCF8 File Offset: 0x0031DEF8
		// Note: this type is marked as 'beforefieldinit'.
		static NotesSlide()
		{
			byte[] array = new byte[2];
			NotesSlide.attributeNamespaceIds = array;
			NotesSlide.eleTagNames = new string[] { "cSld", "clrMapOvr", "extLst" };
			NotesSlide.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x04009391 RID: 37777
		private const string tagName = "notes";

		// Token: 0x04009392 RID: 37778
		private const byte tagNsId = 24;

		// Token: 0x04009393 RID: 37779
		internal const int ElementTypeIdConst = 12183;

		// Token: 0x04009394 RID: 37780
		private static string[] attributeTagNames = new string[] { "showMasterSp", "showMasterPhAnim" };

		// Token: 0x04009395 RID: 37781
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009396 RID: 37782
		private static readonly string[] eleTagNames;

		// Token: 0x04009397 RID: 37783
		private static readonly byte[] eleNamespaceIds;
	}
}
