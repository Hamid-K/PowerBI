using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Office2010.Word.DrawingGroup;
using DocumentFormat.OpenXml.Office2010.Word.DrawingShape;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas
{
	// Token: 0x020024E3 RID: 9443
	[ChildElementInfo(typeof(WholeFormatting), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackgroundFormatting), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordprocessingShape), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordprocessingGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas.OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WordprocessingCanvas : OpenXmlCompositeElement
	{
		// Token: 0x17005314 RID: 21268
		// (get) Token: 0x060117DD RID: 71645 RVA: 0x002EEF83 File Offset: 0x002ED183
		public override string LocalName
		{
			get
			{
				return "wpc";
			}
		}

		// Token: 0x17005315 RID: 21269
		// (get) Token: 0x060117DE RID: 71646 RVA: 0x002EEF8A File Offset: 0x002ED18A
		internal override byte NamespaceId
		{
			get
			{
				return 59;
			}
		}

		// Token: 0x17005316 RID: 21270
		// (get) Token: 0x060117DF RID: 71647 RVA: 0x002EEF8E File Offset: 0x002ED18E
		internal override int ElementTypeId
		{
			get
			{
				return 13118;
			}
		}

		// Token: 0x060117E0 RID: 71648 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117E1 RID: 71649 RVA: 0x00293ECF File Offset: 0x002920CF
		public WordprocessingCanvas()
		{
		}

		// Token: 0x060117E2 RID: 71650 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WordprocessingCanvas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117E3 RID: 71651 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WordprocessingCanvas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117E4 RID: 71652 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WordprocessingCanvas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060117E5 RID: 71653 RVA: 0x002EEF98 File Offset: 0x002ED198
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (59 == namespaceId && "bg" == name)
			{
				return new BackgroundFormatting();
			}
			if (59 == namespaceId && "whole" == name)
			{
				return new WholeFormatting();
			}
			if (61 == namespaceId && "wsp" == name)
			{
				return new WordprocessingShape();
			}
			if (17 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (52 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			if (60 == namespaceId && "wgp" == name)
			{
				return new WordprocessingGroup();
			}
			if (59 == namespaceId && "extLst" == name)
			{
				return new DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas.OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17005317 RID: 21271
		// (get) Token: 0x060117E6 RID: 71654 RVA: 0x002EF04E File Offset: 0x002ED24E
		internal override string[] ElementTagNames
		{
			get
			{
				return WordprocessingCanvas.eleTagNames;
			}
		}

		// Token: 0x17005318 RID: 21272
		// (get) Token: 0x060117E7 RID: 71655 RVA: 0x002EF055 File Offset: 0x002ED255
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WordprocessingCanvas.eleNamespaceIds;
			}
		}

		// Token: 0x17005319 RID: 21273
		// (get) Token: 0x060117E8 RID: 71656 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700531A RID: 21274
		// (get) Token: 0x060117E9 RID: 71657 RVA: 0x002EF05C File Offset: 0x002ED25C
		// (set) Token: 0x060117EA RID: 71658 RVA: 0x002EF065 File Offset: 0x002ED265
		public BackgroundFormatting BackgroundFormatting
		{
			get
			{
				return base.GetElement<BackgroundFormatting>(0);
			}
			set
			{
				base.SetElement<BackgroundFormatting>(0, value);
			}
		}

		// Token: 0x1700531B RID: 21275
		// (get) Token: 0x060117EB RID: 71659 RVA: 0x002EF06F File Offset: 0x002ED26F
		// (set) Token: 0x060117EC RID: 71660 RVA: 0x002EF078 File Offset: 0x002ED278
		public WholeFormatting WholeFormatting
		{
			get
			{
				return base.GetElement<WholeFormatting>(1);
			}
			set
			{
				base.SetElement<WholeFormatting>(1, value);
			}
		}

		// Token: 0x060117ED RID: 71661 RVA: 0x002EF082 File Offset: 0x002ED282
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordprocessingCanvas>(deep);
		}

		// Token: 0x04007AF2 RID: 31474
		private const string tagName = "wpc";

		// Token: 0x04007AF3 RID: 31475
		private const byte tagNsId = 59;

		// Token: 0x04007AF4 RID: 31476
		internal const int ElementTypeIdConst = 13118;

		// Token: 0x04007AF5 RID: 31477
		private static readonly string[] eleTagNames = new string[] { "bg", "whole", "wsp", "pic", "contentPart", "wgp", "extLst" };

		// Token: 0x04007AF6 RID: 31478
		private static readonly byte[] eleNamespaceIds = new byte[] { 59, 59, 61, 17, 52, 60, 59 };
	}
}
