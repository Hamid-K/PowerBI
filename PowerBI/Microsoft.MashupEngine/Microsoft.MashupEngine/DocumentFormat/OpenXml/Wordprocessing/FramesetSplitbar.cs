using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F92 RID: 12178
	[ChildElementInfo(typeof(NoBorder))]
	[ChildElementInfo(typeof(Width))]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(FlatBorders))]
	[GeneratedCode("DomGen", "2.0")]
	internal class FramesetSplitbar : OpenXmlCompositeElement
	{
		// Token: 0x170091CE RID: 37326
		// (get) Token: 0x0601A3D7 RID: 107479 RVA: 0x0035F5CB File Offset: 0x0035D7CB
		public override string LocalName
		{
			get
			{
				return "framesetSplitbar";
			}
		}

		// Token: 0x170091CF RID: 37327
		// (get) Token: 0x0601A3D8 RID: 107480 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091D0 RID: 37328
		// (get) Token: 0x0601A3D9 RID: 107481 RVA: 0x0035F5D2 File Offset: 0x0035D7D2
		internal override int ElementTypeId
		{
			get
			{
				return 11859;
			}
		}

		// Token: 0x0601A3DA RID: 107482 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A3DB RID: 107483 RVA: 0x00293ECF File Offset: 0x002920CF
		public FramesetSplitbar()
		{
		}

		// Token: 0x0601A3DC RID: 107484 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FramesetSplitbar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A3DD RID: 107485 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FramesetSplitbar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A3DE RID: 107486 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FramesetSplitbar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A3DF RID: 107487 RVA: 0x0035F5DC File Offset: 0x0035D7DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new Width();
			}
			if (23 == namespaceId && "color" == name)
			{
				return new Color();
			}
			if (23 == namespaceId && "noBorder" == name)
			{
				return new NoBorder();
			}
			if (23 == namespaceId && "flatBorders" == name)
			{
				return new FlatBorders();
			}
			return null;
		}

		// Token: 0x170091D1 RID: 37329
		// (get) Token: 0x0601A3E0 RID: 107488 RVA: 0x0035F64A File Offset: 0x0035D84A
		internal override string[] ElementTagNames
		{
			get
			{
				return FramesetSplitbar.eleTagNames;
			}
		}

		// Token: 0x170091D2 RID: 37330
		// (get) Token: 0x0601A3E1 RID: 107489 RVA: 0x0035F651 File Offset: 0x0035D851
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FramesetSplitbar.eleNamespaceIds;
			}
		}

		// Token: 0x170091D3 RID: 37331
		// (get) Token: 0x0601A3E2 RID: 107490 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170091D4 RID: 37332
		// (get) Token: 0x0601A3E3 RID: 107491 RVA: 0x0035F658 File Offset: 0x0035D858
		// (set) Token: 0x0601A3E4 RID: 107492 RVA: 0x0035F661 File Offset: 0x0035D861
		public Width Width
		{
			get
			{
				return base.GetElement<Width>(0);
			}
			set
			{
				base.SetElement<Width>(0, value);
			}
		}

		// Token: 0x170091D5 RID: 37333
		// (get) Token: 0x0601A3E5 RID: 107493 RVA: 0x0035F66B File Offset: 0x0035D86B
		// (set) Token: 0x0601A3E6 RID: 107494 RVA: 0x0035F674 File Offset: 0x0035D874
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(1);
			}
			set
			{
				base.SetElement<Color>(1, value);
			}
		}

		// Token: 0x170091D6 RID: 37334
		// (get) Token: 0x0601A3E7 RID: 107495 RVA: 0x0035F67E File Offset: 0x0035D87E
		// (set) Token: 0x0601A3E8 RID: 107496 RVA: 0x0035F687 File Offset: 0x0035D887
		public NoBorder NoBorder
		{
			get
			{
				return base.GetElement<NoBorder>(2);
			}
			set
			{
				base.SetElement<NoBorder>(2, value);
			}
		}

		// Token: 0x170091D7 RID: 37335
		// (get) Token: 0x0601A3E9 RID: 107497 RVA: 0x0035F691 File Offset: 0x0035D891
		// (set) Token: 0x0601A3EA RID: 107498 RVA: 0x0035F69A File Offset: 0x0035D89A
		public FlatBorders FlatBorders
		{
			get
			{
				return base.GetElement<FlatBorders>(3);
			}
			set
			{
				base.SetElement<FlatBorders>(3, value);
			}
		}

		// Token: 0x0601A3EB RID: 107499 RVA: 0x0035F6A4 File Offset: 0x0035D8A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FramesetSplitbar>(deep);
		}

		// Token: 0x0400AC65 RID: 44133
		private const string tagName = "framesetSplitbar";

		// Token: 0x0400AC66 RID: 44134
		private const byte tagNsId = 23;

		// Token: 0x0400AC67 RID: 44135
		internal const int ElementTypeIdConst = 11859;

		// Token: 0x0400AC68 RID: 44136
		private static readonly string[] eleTagNames = new string[] { "w", "color", "noBorder", "flatBorders" };

		// Token: 0x0400AC69 RID: 44137
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
