using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA0 RID: 12192
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Nsid))]
	[ChildElementInfo(typeof(MultiLevelType))]
	[ChildElementInfo(typeof(TemplateCode))]
	[ChildElementInfo(typeof(AbstractNumDefinitionName))]
	[ChildElementInfo(typeof(StyleLink))]
	[ChildElementInfo(typeof(NumberingStyleLink))]
	[ChildElementInfo(typeof(Level))]
	internal class AbstractNum : OpenXmlCompositeElement
	{
		// Token: 0x1700928B RID: 37515
		// (get) Token: 0x0601A560 RID: 107872 RVA: 0x00360CAF File Offset: 0x0035EEAF
		public override string LocalName
		{
			get
			{
				return "abstractNum";
			}
		}

		// Token: 0x1700928C RID: 37516
		// (get) Token: 0x0601A561 RID: 107873 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700928D RID: 37517
		// (get) Token: 0x0601A562 RID: 107874 RVA: 0x00360CB6 File Offset: 0x0035EEB6
		internal override int ElementTypeId
		{
			get
			{
				return 11885;
			}
		}

		// Token: 0x0601A563 RID: 107875 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700928E RID: 37518
		// (get) Token: 0x0601A564 RID: 107876 RVA: 0x00360CBD File Offset: 0x0035EEBD
		internal override string[] AttributeTagNames
		{
			get
			{
				return AbstractNum.attributeTagNames;
			}
		}

		// Token: 0x1700928F RID: 37519
		// (get) Token: 0x0601A565 RID: 107877 RVA: 0x00360CC4 File Offset: 0x0035EEC4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AbstractNum.attributeNamespaceIds;
			}
		}

		// Token: 0x17009290 RID: 37520
		// (get) Token: 0x0601A566 RID: 107878 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A567 RID: 107879 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "abstractNumId")]
		public Int32Value AbstractNumberId
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A568 RID: 107880 RVA: 0x00293ECF File Offset: 0x002920CF
		public AbstractNum()
		{
		}

		// Token: 0x0601A569 RID: 107881 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AbstractNum(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A56A RID: 107882 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AbstractNum(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A56B RID: 107883 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AbstractNum(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A56C RID: 107884 RVA: 0x00360CCC File Offset: 0x0035EECC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "nsid" == name)
			{
				return new Nsid();
			}
			if (23 == namespaceId && "multiLevelType" == name)
			{
				return new MultiLevelType();
			}
			if (23 == namespaceId && "tmpl" == name)
			{
				return new TemplateCode();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new AbstractNumDefinitionName();
			}
			if (23 == namespaceId && "styleLink" == name)
			{
				return new StyleLink();
			}
			if (23 == namespaceId && "numStyleLink" == name)
			{
				return new NumberingStyleLink();
			}
			if (23 == namespaceId && "lvl" == name)
			{
				return new Level();
			}
			return null;
		}

		// Token: 0x17009291 RID: 37521
		// (get) Token: 0x0601A56D RID: 107885 RVA: 0x00360D82 File Offset: 0x0035EF82
		internal override string[] ElementTagNames
		{
			get
			{
				return AbstractNum.eleTagNames;
			}
		}

		// Token: 0x17009292 RID: 37522
		// (get) Token: 0x0601A56E RID: 107886 RVA: 0x00360D89 File Offset: 0x0035EF89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AbstractNum.eleNamespaceIds;
			}
		}

		// Token: 0x17009293 RID: 37523
		// (get) Token: 0x0601A56F RID: 107887 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009294 RID: 37524
		// (get) Token: 0x0601A570 RID: 107888 RVA: 0x00360D90 File Offset: 0x0035EF90
		// (set) Token: 0x0601A571 RID: 107889 RVA: 0x00360D99 File Offset: 0x0035EF99
		public Nsid Nsid
		{
			get
			{
				return base.GetElement<Nsid>(0);
			}
			set
			{
				base.SetElement<Nsid>(0, value);
			}
		}

		// Token: 0x17009295 RID: 37525
		// (get) Token: 0x0601A572 RID: 107890 RVA: 0x00360DA3 File Offset: 0x0035EFA3
		// (set) Token: 0x0601A573 RID: 107891 RVA: 0x00360DAC File Offset: 0x0035EFAC
		public MultiLevelType MultiLevelType
		{
			get
			{
				return base.GetElement<MultiLevelType>(1);
			}
			set
			{
				base.SetElement<MultiLevelType>(1, value);
			}
		}

		// Token: 0x17009296 RID: 37526
		// (get) Token: 0x0601A574 RID: 107892 RVA: 0x00360DB6 File Offset: 0x0035EFB6
		// (set) Token: 0x0601A575 RID: 107893 RVA: 0x00360DBF File Offset: 0x0035EFBF
		public TemplateCode TemplateCode
		{
			get
			{
				return base.GetElement<TemplateCode>(2);
			}
			set
			{
				base.SetElement<TemplateCode>(2, value);
			}
		}

		// Token: 0x17009297 RID: 37527
		// (get) Token: 0x0601A576 RID: 107894 RVA: 0x00360DC9 File Offset: 0x0035EFC9
		// (set) Token: 0x0601A577 RID: 107895 RVA: 0x00360DD2 File Offset: 0x0035EFD2
		public AbstractNumDefinitionName AbstractNumDefinitionName
		{
			get
			{
				return base.GetElement<AbstractNumDefinitionName>(3);
			}
			set
			{
				base.SetElement<AbstractNumDefinitionName>(3, value);
			}
		}

		// Token: 0x17009298 RID: 37528
		// (get) Token: 0x0601A578 RID: 107896 RVA: 0x00360DDC File Offset: 0x0035EFDC
		// (set) Token: 0x0601A579 RID: 107897 RVA: 0x00360DE5 File Offset: 0x0035EFE5
		public StyleLink StyleLink
		{
			get
			{
				return base.GetElement<StyleLink>(4);
			}
			set
			{
				base.SetElement<StyleLink>(4, value);
			}
		}

		// Token: 0x17009299 RID: 37529
		// (get) Token: 0x0601A57A RID: 107898 RVA: 0x00360DEF File Offset: 0x0035EFEF
		// (set) Token: 0x0601A57B RID: 107899 RVA: 0x00360DF8 File Offset: 0x0035EFF8
		public NumberingStyleLink NumberingStyleLink
		{
			get
			{
				return base.GetElement<NumberingStyleLink>(5);
			}
			set
			{
				base.SetElement<NumberingStyleLink>(5, value);
			}
		}

		// Token: 0x0601A57C RID: 107900 RVA: 0x00360E02 File Offset: 0x0035F002
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "abstractNumId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A57D RID: 107901 RVA: 0x00360E24 File Offset: 0x0035F024
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbstractNum>(deep);
		}

		// Token: 0x0400ACAF RID: 44207
		private const string tagName = "abstractNum";

		// Token: 0x0400ACB0 RID: 44208
		private const byte tagNsId = 23;

		// Token: 0x0400ACB1 RID: 44209
		internal const int ElementTypeIdConst = 11885;

		// Token: 0x0400ACB2 RID: 44210
		private static string[] attributeTagNames = new string[] { "abstractNumId" };

		// Token: 0x0400ACB3 RID: 44211
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400ACB4 RID: 44212
		private static readonly string[] eleTagNames = new string[] { "nsid", "multiLevelType", "tmpl", "name", "styleLink", "numStyleLink", "lvl" };

		// Token: 0x0400ACB5 RID: 44213
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
