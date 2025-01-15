using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200229A RID: 8858
	[ChildElementInfo(typeof(OfficeMenu))]
	[ChildElementInfo(typeof(ContextualTabSets))]
	[ChildElementInfo(typeof(Tabs))]
	[ChildElementInfo(typeof(QuickAccessToolbar))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Ribbon : OpenXmlCompositeElement
	{
		// Token: 0x170040DE RID: 16606
		// (get) Token: 0x0600F011 RID: 61457 RVA: 0x002D0638 File Offset: 0x002CE838
		public override string LocalName
		{
			get
			{
				return "ribbon";
			}
		}

		// Token: 0x170040DF RID: 16607
		// (get) Token: 0x0600F012 RID: 61458 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040E0 RID: 16608
		// (get) Token: 0x0600F013 RID: 61459 RVA: 0x002D063F File Offset: 0x002CE83F
		internal override int ElementTypeId
		{
			get
			{
				return 12616;
			}
		}

		// Token: 0x0600F014 RID: 61460 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040E1 RID: 16609
		// (get) Token: 0x0600F015 RID: 61461 RVA: 0x002D0646 File Offset: 0x002CE846
		internal override string[] AttributeTagNames
		{
			get
			{
				return Ribbon.attributeTagNames;
			}
		}

		// Token: 0x170040E2 RID: 16610
		// (get) Token: 0x0600F016 RID: 61462 RVA: 0x002D064D File Offset: 0x002CE84D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Ribbon.attributeNamespaceIds;
			}
		}

		// Token: 0x170040E3 RID: 16611
		// (get) Token: 0x0600F017 RID: 61463 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600F018 RID: 61464 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "startFromScratch")]
		public BooleanValue StartFromScratch
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

		// Token: 0x0600F019 RID: 61465 RVA: 0x00293ECF File Offset: 0x002920CF
		public Ribbon()
		{
		}

		// Token: 0x0600F01A RID: 61466 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Ribbon(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F01B RID: 61467 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Ribbon(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F01C RID: 61468 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Ribbon(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F01D RID: 61469 RVA: 0x002D0654 File Offset: 0x002CE854
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "officeMenu" == name)
			{
				return new OfficeMenu();
			}
			if (34 == namespaceId && "qat" == name)
			{
				return new QuickAccessToolbar();
			}
			if (34 == namespaceId && "tabs" == name)
			{
				return new Tabs();
			}
			if (34 == namespaceId && "contextualTabs" == name)
			{
				return new ContextualTabSets();
			}
			return null;
		}

		// Token: 0x170040E4 RID: 16612
		// (get) Token: 0x0600F01E RID: 61470 RVA: 0x002D06C2 File Offset: 0x002CE8C2
		internal override string[] ElementTagNames
		{
			get
			{
				return Ribbon.eleTagNames;
			}
		}

		// Token: 0x170040E5 RID: 16613
		// (get) Token: 0x0600F01F RID: 61471 RVA: 0x002D06C9 File Offset: 0x002CE8C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Ribbon.eleNamespaceIds;
			}
		}

		// Token: 0x170040E6 RID: 16614
		// (get) Token: 0x0600F020 RID: 61472 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x170040E7 RID: 16615
		// (get) Token: 0x0600F021 RID: 61473 RVA: 0x002D06D0 File Offset: 0x002CE8D0
		// (set) Token: 0x0600F022 RID: 61474 RVA: 0x002D06D9 File Offset: 0x002CE8D9
		public OfficeMenu OfficeMenu
		{
			get
			{
				return base.GetElement<OfficeMenu>(0);
			}
			set
			{
				base.SetElement<OfficeMenu>(0, value);
			}
		}

		// Token: 0x170040E8 RID: 16616
		// (get) Token: 0x0600F023 RID: 61475 RVA: 0x002D06E3 File Offset: 0x002CE8E3
		// (set) Token: 0x0600F024 RID: 61476 RVA: 0x002D06EC File Offset: 0x002CE8EC
		public QuickAccessToolbar QuickAccessToolbar
		{
			get
			{
				return base.GetElement<QuickAccessToolbar>(1);
			}
			set
			{
				base.SetElement<QuickAccessToolbar>(1, value);
			}
		}

		// Token: 0x170040E9 RID: 16617
		// (get) Token: 0x0600F025 RID: 61477 RVA: 0x002D06F6 File Offset: 0x002CE8F6
		// (set) Token: 0x0600F026 RID: 61478 RVA: 0x002D06FF File Offset: 0x002CE8FF
		public Tabs Tabs
		{
			get
			{
				return base.GetElement<Tabs>(2);
			}
			set
			{
				base.SetElement<Tabs>(2, value);
			}
		}

		// Token: 0x170040EA RID: 16618
		// (get) Token: 0x0600F027 RID: 61479 RVA: 0x002D0709 File Offset: 0x002CE909
		// (set) Token: 0x0600F028 RID: 61480 RVA: 0x002D0712 File Offset: 0x002CE912
		public ContextualTabSets ContextualTabSets
		{
			get
			{
				return base.GetElement<ContextualTabSets>(3);
			}
			set
			{
				base.SetElement<ContextualTabSets>(3, value);
			}
		}

		// Token: 0x0600F029 RID: 61481 RVA: 0x002D071C File Offset: 0x002CE91C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "startFromScratch" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F02A RID: 61482 RVA: 0x002D073C File Offset: 0x002CE93C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ribbon>(deep);
		}

		// Token: 0x0600F02B RID: 61483 RVA: 0x002D0748 File Offset: 0x002CE948
		// Note: this type is marked as 'beforefieldinit'.
		static Ribbon()
		{
			byte[] array = new byte[1];
			Ribbon.attributeNamespaceIds = array;
			Ribbon.eleTagNames = new string[] { "officeMenu", "qat", "tabs", "contextualTabs" };
			Ribbon.eleNamespaceIds = new byte[] { 34, 34, 34, 34 };
		}

		// Token: 0x04007052 RID: 28754
		private const string tagName = "ribbon";

		// Token: 0x04007053 RID: 28755
		private const byte tagNsId = 34;

		// Token: 0x04007054 RID: 28756
		internal const int ElementTypeIdConst = 12616;

		// Token: 0x04007055 RID: 28757
		private static string[] attributeTagNames = new string[] { "startFromScratch" };

		// Token: 0x04007056 RID: 28758
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007057 RID: 28759
		private static readonly string[] eleTagNames;

		// Token: 0x04007058 RID: 28760
		private static readonly byte[] eleNamespaceIds;
	}
}
