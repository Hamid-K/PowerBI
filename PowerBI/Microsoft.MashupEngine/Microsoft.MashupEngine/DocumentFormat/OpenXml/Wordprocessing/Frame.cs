using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F94 RID: 12180
	[ChildElementInfo(typeof(FrameSize))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FrameName))]
	[ChildElementInfo(typeof(SourceFileReference))]
	[ChildElementInfo(typeof(MarginWidth))]
	[ChildElementInfo(typeof(MarginHeight))]
	[ChildElementInfo(typeof(ScrollbarVisibility))]
	[ChildElementInfo(typeof(NoResizeAllowed))]
	[ChildElementInfo(typeof(LinkedToFile))]
	internal class Frame : OpenXmlCompositeElement
	{
		// Token: 0x170091DE RID: 37342
		// (get) Token: 0x0601A3F9 RID: 107513 RVA: 0x0035F78C File Offset: 0x0035D98C
		public override string LocalName
		{
			get
			{
				return "frame";
			}
		}

		// Token: 0x170091DF RID: 37343
		// (get) Token: 0x0601A3FA RID: 107514 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091E0 RID: 37344
		// (get) Token: 0x0601A3FB RID: 107515 RVA: 0x0035F793 File Offset: 0x0035D993
		internal override int ElementTypeId
		{
			get
			{
				return 11861;
			}
		}

		// Token: 0x0601A3FC RID: 107516 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3FD RID: 107517 RVA: 0x00293ECF File Offset: 0x002920CF
		public Frame()
		{
		}

		// Token: 0x0601A3FE RID: 107518 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Frame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A3FF RID: 107519 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Frame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A400 RID: 107520 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Frame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A401 RID: 107521 RVA: 0x0035F79C File Offset: 0x0035D99C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "sz" == name)
			{
				return new FrameSize();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new FrameName();
			}
			if (23 == namespaceId && "sourceFileName" == name)
			{
				return new SourceFileReference();
			}
			if (23 == namespaceId && "marW" == name)
			{
				return new MarginWidth();
			}
			if (23 == namespaceId && "marH" == name)
			{
				return new MarginHeight();
			}
			if (23 == namespaceId && "scrollbar" == name)
			{
				return new ScrollbarVisibility();
			}
			if (23 == namespaceId && "noResizeAllowed" == name)
			{
				return new NoResizeAllowed();
			}
			if (23 == namespaceId && "linkedToFile" == name)
			{
				return new LinkedToFile();
			}
			return null;
		}

		// Token: 0x170091E1 RID: 37345
		// (get) Token: 0x0601A402 RID: 107522 RVA: 0x0035F86A File Offset: 0x0035DA6A
		internal override string[] ElementTagNames
		{
			get
			{
				return Frame.eleTagNames;
			}
		}

		// Token: 0x170091E2 RID: 37346
		// (get) Token: 0x0601A403 RID: 107523 RVA: 0x0035F871 File Offset: 0x0035DA71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Frame.eleNamespaceIds;
			}
		}

		// Token: 0x170091E3 RID: 37347
		// (get) Token: 0x0601A404 RID: 107524 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170091E4 RID: 37348
		// (get) Token: 0x0601A405 RID: 107525 RVA: 0x0035F208 File Offset: 0x0035D408
		// (set) Token: 0x0601A406 RID: 107526 RVA: 0x0035F211 File Offset: 0x0035D411
		public FrameSize FrameSize
		{
			get
			{
				return base.GetElement<FrameSize>(0);
			}
			set
			{
				base.SetElement<FrameSize>(0, value);
			}
		}

		// Token: 0x170091E5 RID: 37349
		// (get) Token: 0x0601A407 RID: 107527 RVA: 0x0035F878 File Offset: 0x0035DA78
		// (set) Token: 0x0601A408 RID: 107528 RVA: 0x0035F881 File Offset: 0x0035DA81
		public FrameName FrameName
		{
			get
			{
				return base.GetElement<FrameName>(1);
			}
			set
			{
				base.SetElement<FrameName>(1, value);
			}
		}

		// Token: 0x170091E6 RID: 37350
		// (get) Token: 0x0601A409 RID: 107529 RVA: 0x0035F88B File Offset: 0x0035DA8B
		// (set) Token: 0x0601A40A RID: 107530 RVA: 0x0035F894 File Offset: 0x0035DA94
		public SourceFileReference SourceFileReference
		{
			get
			{
				return base.GetElement<SourceFileReference>(2);
			}
			set
			{
				base.SetElement<SourceFileReference>(2, value);
			}
		}

		// Token: 0x170091E7 RID: 37351
		// (get) Token: 0x0601A40B RID: 107531 RVA: 0x0035F89E File Offset: 0x0035DA9E
		// (set) Token: 0x0601A40C RID: 107532 RVA: 0x0035F8A7 File Offset: 0x0035DAA7
		public MarginWidth MarginWidth
		{
			get
			{
				return base.GetElement<MarginWidth>(3);
			}
			set
			{
				base.SetElement<MarginWidth>(3, value);
			}
		}

		// Token: 0x170091E8 RID: 37352
		// (get) Token: 0x0601A40D RID: 107533 RVA: 0x0035F8B1 File Offset: 0x0035DAB1
		// (set) Token: 0x0601A40E RID: 107534 RVA: 0x0035F8BA File Offset: 0x0035DABA
		public MarginHeight MarginHeight
		{
			get
			{
				return base.GetElement<MarginHeight>(4);
			}
			set
			{
				base.SetElement<MarginHeight>(4, value);
			}
		}

		// Token: 0x170091E9 RID: 37353
		// (get) Token: 0x0601A40F RID: 107535 RVA: 0x0035F8C4 File Offset: 0x0035DAC4
		// (set) Token: 0x0601A410 RID: 107536 RVA: 0x0035F8CD File Offset: 0x0035DACD
		public ScrollbarVisibility ScrollbarVisibility
		{
			get
			{
				return base.GetElement<ScrollbarVisibility>(5);
			}
			set
			{
				base.SetElement<ScrollbarVisibility>(5, value);
			}
		}

		// Token: 0x170091EA RID: 37354
		// (get) Token: 0x0601A411 RID: 107537 RVA: 0x0035F8D7 File Offset: 0x0035DAD7
		// (set) Token: 0x0601A412 RID: 107538 RVA: 0x0035F8E0 File Offset: 0x0035DAE0
		public NoResizeAllowed NoResizeAllowed
		{
			get
			{
				return base.GetElement<NoResizeAllowed>(6);
			}
			set
			{
				base.SetElement<NoResizeAllowed>(6, value);
			}
		}

		// Token: 0x170091EB RID: 37355
		// (get) Token: 0x0601A413 RID: 107539 RVA: 0x0035F8EA File Offset: 0x0035DAEA
		// (set) Token: 0x0601A414 RID: 107540 RVA: 0x0035F8F3 File Offset: 0x0035DAF3
		public LinkedToFile LinkedToFile
		{
			get
			{
				return base.GetElement<LinkedToFile>(7);
			}
			set
			{
				base.SetElement<LinkedToFile>(7, value);
			}
		}

		// Token: 0x0601A415 RID: 107541 RVA: 0x0035F8FD File Offset: 0x0035DAFD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Frame>(deep);
		}

		// Token: 0x0400AC6F RID: 44143
		private const string tagName = "frame";

		// Token: 0x0400AC70 RID: 44144
		private const byte tagNsId = 23;

		// Token: 0x0400AC71 RID: 44145
		internal const int ElementTypeIdConst = 11861;

		// Token: 0x0400AC72 RID: 44146
		private static readonly string[] eleTagNames = new string[] { "sz", "name", "sourceFileName", "marW", "marH", "scrollbar", "noResizeAllowed", "linkedToFile" };

		// Token: 0x0400AC73 RID: 44147
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
