using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAA RID: 10922
	[ChildElementInfo(typeof(KioskSlideMode))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlideAll))]
	[ChildElementInfo(typeof(PresenterSlideMode))]
	[ChildElementInfo(typeof(BrowseSlideMode))]
	[ChildElementInfo(typeof(CustomShowReference))]
	[ChildElementInfo(typeof(ShowPropertiesExtensionList))]
	[ChildElementInfo(typeof(SlideRange))]
	[ChildElementInfo(typeof(PenColor))]
	internal class ShowProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007499 RID: 29849
		// (get) Token: 0x0601638C RID: 91020 RVA: 0x00327E4E File Offset: 0x0032604E
		public override string LocalName
		{
			get
			{
				return "showPr";
			}
		}

		// Token: 0x1700749A RID: 29850
		// (get) Token: 0x0601638D RID: 91021 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700749B RID: 29851
		// (get) Token: 0x0601638E RID: 91022 RVA: 0x00327E55 File Offset: 0x00326055
		internal override int ElementTypeId
		{
			get
			{
				return 12336;
			}
		}

		// Token: 0x0601638F RID: 91023 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700749C RID: 29852
		// (get) Token: 0x06016390 RID: 91024 RVA: 0x00327E5C File Offset: 0x0032605C
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShowProperties.attributeTagNames;
			}
		}

		// Token: 0x1700749D RID: 29853
		// (get) Token: 0x06016391 RID: 91025 RVA: 0x00327E63 File Offset: 0x00326063
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShowProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700749E RID: 29854
		// (get) Token: 0x06016392 RID: 91026 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016393 RID: 91027 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "loop")]
		public BooleanValue Loop
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

		// Token: 0x1700749F RID: 29855
		// (get) Token: 0x06016394 RID: 91028 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016395 RID: 91029 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showNarration")]
		public BooleanValue ShowNarration
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

		// Token: 0x170074A0 RID: 29856
		// (get) Token: 0x06016396 RID: 91030 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016397 RID: 91031 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showAnimation")]
		public BooleanValue ShowAnimation
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170074A1 RID: 29857
		// (get) Token: 0x06016398 RID: 91032 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016399 RID: 91033 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "useTimings")]
		public BooleanValue UseTimings
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601639A RID: 91034 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShowProperties()
		{
		}

		// Token: 0x0601639B RID: 91035 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShowProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601639C RID: 91036 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShowProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601639D RID: 91037 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShowProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601639E RID: 91038 RVA: 0x00327E6C File Offset: 0x0032606C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "present" == name)
			{
				return new PresenterSlideMode();
			}
			if (24 == namespaceId && "browse" == name)
			{
				return new BrowseSlideMode();
			}
			if (24 == namespaceId && "kiosk" == name)
			{
				return new KioskSlideMode();
			}
			if (24 == namespaceId && "sldAll" == name)
			{
				return new SlideAll();
			}
			if (24 == namespaceId && "sldRg" == name)
			{
				return new SlideRange();
			}
			if (24 == namespaceId && "custShow" == name)
			{
				return new CustomShowReference();
			}
			if (24 == namespaceId && "penClr" == name)
			{
				return new PenColor();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ShowPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x0601639F RID: 91039 RVA: 0x00327F3C File Offset: 0x0032613C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "loop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showNarration" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showAnimation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "useTimings" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060163A0 RID: 91040 RVA: 0x00327FA9 File Offset: 0x003261A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowProperties>(deep);
		}

		// Token: 0x060163A1 RID: 91041 RVA: 0x00327FB4 File Offset: 0x003261B4
		// Note: this type is marked as 'beforefieldinit'.
		static ShowProperties()
		{
			byte[] array = new byte[4];
			ShowProperties.attributeNamespaceIds = array;
		}

		// Token: 0x040096BC RID: 38588
		private const string tagName = "showPr";

		// Token: 0x040096BD RID: 38589
		private const byte tagNsId = 24;

		// Token: 0x040096BE RID: 38590
		internal const int ElementTypeIdConst = 12336;

		// Token: 0x040096BF RID: 38591
		private static string[] attributeTagNames = new string[] { "loop", "showNarration", "showAnimation", "useTimings" };

		// Token: 0x040096C0 RID: 38592
		private static byte[] attributeNamespaceIds;
	}
}
