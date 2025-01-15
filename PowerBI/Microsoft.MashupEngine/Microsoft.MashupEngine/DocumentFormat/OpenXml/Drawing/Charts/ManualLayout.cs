using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258C RID: 9612
	[ChildElementInfo(typeof(Height))]
	[ChildElementInfo(typeof(HeightMode))]
	[ChildElementInfo(typeof(Left))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TopMode))]
	[ChildElementInfo(typeof(WidthMode))]
	[ChildElementInfo(typeof(Top))]
	[ChildElementInfo(typeof(LeftMode))]
	[ChildElementInfo(typeof(Width))]
	[ChildElementInfo(typeof(LayoutTarget))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ManualLayout : OpenXmlCompositeElement
	{
		// Token: 0x17005656 RID: 22102
		// (get) Token: 0x06011F29 RID: 73513 RVA: 0x002F3EB7 File Offset: 0x002F20B7
		public override string LocalName
		{
			get
			{
				return "manualLayout";
			}
		}

		// Token: 0x17005657 RID: 22103
		// (get) Token: 0x06011F2A RID: 73514 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005658 RID: 22104
		// (get) Token: 0x06011F2B RID: 73515 RVA: 0x002F3EBE File Offset: 0x002F20BE
		internal override int ElementTypeId
		{
			get
			{
				return 10415;
			}
		}

		// Token: 0x06011F2C RID: 73516 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F2D RID: 73517 RVA: 0x00293ECF File Offset: 0x002920CF
		public ManualLayout()
		{
		}

		// Token: 0x06011F2E RID: 73518 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ManualLayout(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011F2F RID: 73519 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ManualLayout(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011F30 RID: 73520 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ManualLayout(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011F31 RID: 73521 RVA: 0x002F3EC8 File Offset: 0x002F20C8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "layoutTarget" == name)
			{
				return new LayoutTarget();
			}
			if (11 == namespaceId && "xMode" == name)
			{
				return new LeftMode();
			}
			if (11 == namespaceId && "yMode" == name)
			{
				return new TopMode();
			}
			if (11 == namespaceId && "wMode" == name)
			{
				return new WidthMode();
			}
			if (11 == namespaceId && "hMode" == name)
			{
				return new HeightMode();
			}
			if (11 == namespaceId && "x" == name)
			{
				return new Left();
			}
			if (11 == namespaceId && "y" == name)
			{
				return new Top();
			}
			if (11 == namespaceId && "w" == name)
			{
				return new Width();
			}
			if (11 == namespaceId && "h" == name)
			{
				return new Height();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005659 RID: 22105
		// (get) Token: 0x06011F32 RID: 73522 RVA: 0x002F3FC6 File Offset: 0x002F21C6
		internal override string[] ElementTagNames
		{
			get
			{
				return ManualLayout.eleTagNames;
			}
		}

		// Token: 0x1700565A RID: 22106
		// (get) Token: 0x06011F33 RID: 73523 RVA: 0x002F3FCD File Offset: 0x002F21CD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ManualLayout.eleNamespaceIds;
			}
		}

		// Token: 0x1700565B RID: 22107
		// (get) Token: 0x06011F34 RID: 73524 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700565C RID: 22108
		// (get) Token: 0x06011F35 RID: 73525 RVA: 0x002F3FD4 File Offset: 0x002F21D4
		// (set) Token: 0x06011F36 RID: 73526 RVA: 0x002F3FDD File Offset: 0x002F21DD
		public LayoutTarget LayoutTarget
		{
			get
			{
				return base.GetElement<LayoutTarget>(0);
			}
			set
			{
				base.SetElement<LayoutTarget>(0, value);
			}
		}

		// Token: 0x1700565D RID: 22109
		// (get) Token: 0x06011F37 RID: 73527 RVA: 0x002F3FE7 File Offset: 0x002F21E7
		// (set) Token: 0x06011F38 RID: 73528 RVA: 0x002F3FF0 File Offset: 0x002F21F0
		public LeftMode LeftMode
		{
			get
			{
				return base.GetElement<LeftMode>(1);
			}
			set
			{
				base.SetElement<LeftMode>(1, value);
			}
		}

		// Token: 0x1700565E RID: 22110
		// (get) Token: 0x06011F39 RID: 73529 RVA: 0x002F3FFA File Offset: 0x002F21FA
		// (set) Token: 0x06011F3A RID: 73530 RVA: 0x002F4003 File Offset: 0x002F2203
		public TopMode TopMode
		{
			get
			{
				return base.GetElement<TopMode>(2);
			}
			set
			{
				base.SetElement<TopMode>(2, value);
			}
		}

		// Token: 0x1700565F RID: 22111
		// (get) Token: 0x06011F3B RID: 73531 RVA: 0x002F400D File Offset: 0x002F220D
		// (set) Token: 0x06011F3C RID: 73532 RVA: 0x002F4016 File Offset: 0x002F2216
		public WidthMode WidthMode
		{
			get
			{
				return base.GetElement<WidthMode>(3);
			}
			set
			{
				base.SetElement<WidthMode>(3, value);
			}
		}

		// Token: 0x17005660 RID: 22112
		// (get) Token: 0x06011F3D RID: 73533 RVA: 0x002F4020 File Offset: 0x002F2220
		// (set) Token: 0x06011F3E RID: 73534 RVA: 0x002F4029 File Offset: 0x002F2229
		public HeightMode HeightMode
		{
			get
			{
				return base.GetElement<HeightMode>(4);
			}
			set
			{
				base.SetElement<HeightMode>(4, value);
			}
		}

		// Token: 0x17005661 RID: 22113
		// (get) Token: 0x06011F3F RID: 73535 RVA: 0x002F4033 File Offset: 0x002F2233
		// (set) Token: 0x06011F40 RID: 73536 RVA: 0x002F403C File Offset: 0x002F223C
		public Left Left
		{
			get
			{
				return base.GetElement<Left>(5);
			}
			set
			{
				base.SetElement<Left>(5, value);
			}
		}

		// Token: 0x17005662 RID: 22114
		// (get) Token: 0x06011F41 RID: 73537 RVA: 0x002F4046 File Offset: 0x002F2246
		// (set) Token: 0x06011F42 RID: 73538 RVA: 0x002F404F File Offset: 0x002F224F
		public Top Top
		{
			get
			{
				return base.GetElement<Top>(6);
			}
			set
			{
				base.SetElement<Top>(6, value);
			}
		}

		// Token: 0x17005663 RID: 22115
		// (get) Token: 0x06011F43 RID: 73539 RVA: 0x002F4059 File Offset: 0x002F2259
		// (set) Token: 0x06011F44 RID: 73540 RVA: 0x002F4062 File Offset: 0x002F2262
		public Width Width
		{
			get
			{
				return base.GetElement<Width>(7);
			}
			set
			{
				base.SetElement<Width>(7, value);
			}
		}

		// Token: 0x17005664 RID: 22116
		// (get) Token: 0x06011F45 RID: 73541 RVA: 0x002F406C File Offset: 0x002F226C
		// (set) Token: 0x06011F46 RID: 73542 RVA: 0x002F4075 File Offset: 0x002F2275
		public Height Height
		{
			get
			{
				return base.GetElement<Height>(8);
			}
			set
			{
				base.SetElement<Height>(8, value);
			}
		}

		// Token: 0x17005665 RID: 22117
		// (get) Token: 0x06011F47 RID: 73543 RVA: 0x002F407F File Offset: 0x002F227F
		// (set) Token: 0x06011F48 RID: 73544 RVA: 0x002F4089 File Offset: 0x002F2289
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(9);
			}
			set
			{
				base.SetElement<ExtensionList>(9, value);
			}
		}

		// Token: 0x06011F49 RID: 73545 RVA: 0x002F4094 File Offset: 0x002F2294
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ManualLayout>(deep);
		}

		// Token: 0x04007D66 RID: 32102
		private const string tagName = "manualLayout";

		// Token: 0x04007D67 RID: 32103
		private const byte tagNsId = 11;

		// Token: 0x04007D68 RID: 32104
		internal const int ElementTypeIdConst = 10415;

		// Token: 0x04007D69 RID: 32105
		private static readonly string[] eleTagNames = new string[] { "layoutTarget", "xMode", "yMode", "wMode", "hMode", "x", "y", "w", "h", "extLst" };

		// Token: 0x04007D6A RID: 32106
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
