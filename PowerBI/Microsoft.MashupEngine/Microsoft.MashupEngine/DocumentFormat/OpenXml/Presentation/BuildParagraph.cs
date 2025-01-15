using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3F RID: 10815
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TemplateList))]
	internal class BuildParagraph : OpenXmlCompositeElement
	{
		// Token: 0x1700713F RID: 28991
		// (get) Token: 0x06015C15 RID: 89109 RVA: 0x00322C21 File Offset: 0x00320E21
		public override string LocalName
		{
			get
			{
				return "bldP";
			}
		}

		// Token: 0x17007140 RID: 28992
		// (get) Token: 0x06015C16 RID: 89110 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007141 RID: 28993
		// (get) Token: 0x06015C17 RID: 89111 RVA: 0x00322C28 File Offset: 0x00320E28
		internal override int ElementTypeId
		{
			get
			{
				return 12234;
			}
		}

		// Token: 0x06015C18 RID: 89112 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007142 RID: 28994
		// (get) Token: 0x06015C19 RID: 89113 RVA: 0x00322C2F File Offset: 0x00320E2F
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildParagraph.attributeTagNames;
			}
		}

		// Token: 0x17007143 RID: 28995
		// (get) Token: 0x06015C1A RID: 89114 RVA: 0x00322C36 File Offset: 0x00320E36
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildParagraph.attributeNamespaceIds;
			}
		}

		// Token: 0x17007144 RID: 28996
		// (get) Token: 0x06015C1B RID: 89115 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015C1C RID: 89116 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007145 RID: 28997
		// (get) Token: 0x06015C1D RID: 89117 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06015C1E RID: 89118 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "grpId")]
		public UInt32Value GroupId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007146 RID: 28998
		// (get) Token: 0x06015C1F RID: 89119 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015C20 RID: 89120 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "uiExpand")]
		public BooleanValue UiExpand
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

		// Token: 0x17007147 RID: 28999
		// (get) Token: 0x06015C21 RID: 89121 RVA: 0x00322C3D File Offset: 0x00320E3D
		// (set) Token: 0x06015C22 RID: 89122 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "build")]
		public EnumValue<ParagraphBuildValues> Build
		{
			get
			{
				return (EnumValue<ParagraphBuildValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007148 RID: 29000
		// (get) Token: 0x06015C23 RID: 89123 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06015C24 RID: 89124 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "bldLvl")]
		public UInt32Value BuildLevel
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007149 RID: 29001
		// (get) Token: 0x06015C25 RID: 89125 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06015C26 RID: 89126 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "animBg")]
		public BooleanValue AnimateBackground
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700714A RID: 29002
		// (get) Token: 0x06015C27 RID: 89127 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06015C28 RID: 89128 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "autoUpdateAnimBg")]
		public BooleanValue AutoAnimateBackground
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700714B RID: 29003
		// (get) Token: 0x06015C29 RID: 89129 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06015C2A RID: 89130 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "rev")]
		public BooleanValue Reverse
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700714C RID: 29004
		// (get) Token: 0x06015C2B RID: 89131 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06015C2C RID: 89132 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "advAuto")]
		public StringValue AutoAdvance
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06015C2D RID: 89133 RVA: 0x00293ECF File Offset: 0x002920CF
		public BuildParagraph()
		{
		}

		// Token: 0x06015C2E RID: 89134 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BuildParagraph(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C2F RID: 89135 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BuildParagraph(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C30 RID: 89136 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BuildParagraph(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C31 RID: 89137 RVA: 0x00322C4C File Offset: 0x00320E4C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tmplLst" == name)
			{
				return new TemplateList();
			}
			return null;
		}

		// Token: 0x1700714D RID: 29005
		// (get) Token: 0x06015C32 RID: 89138 RVA: 0x00322C67 File Offset: 0x00320E67
		internal override string[] ElementTagNames
		{
			get
			{
				return BuildParagraph.eleTagNames;
			}
		}

		// Token: 0x1700714E RID: 29006
		// (get) Token: 0x06015C33 RID: 89139 RVA: 0x00322C6E File Offset: 0x00320E6E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BuildParagraph.eleNamespaceIds;
			}
		}

		// Token: 0x1700714F RID: 29007
		// (get) Token: 0x06015C34 RID: 89140 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007150 RID: 29008
		// (get) Token: 0x06015C35 RID: 89141 RVA: 0x00322C75 File Offset: 0x00320E75
		// (set) Token: 0x06015C36 RID: 89142 RVA: 0x00322C7E File Offset: 0x00320E7E
		public TemplateList TemplateList
		{
			get
			{
				return base.GetElement<TemplateList>(0);
			}
			set
			{
				base.SetElement<TemplateList>(0, value);
			}
		}

		// Token: 0x06015C37 RID: 89143 RVA: 0x00322C88 File Offset: 0x00320E88
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "grpId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uiExpand" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "build" == name)
			{
				return new EnumValue<ParagraphBuildValues>();
			}
			if (namespaceId == 0 && "bldLvl" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "animBg" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoUpdateAnimBg" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rev" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "advAuto" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015C38 RID: 89144 RVA: 0x00322D63 File Offset: 0x00320F63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildParagraph>(deep);
		}

		// Token: 0x06015C39 RID: 89145 RVA: 0x00322D6C File Offset: 0x00320F6C
		// Note: this type is marked as 'beforefieldinit'.
		static BuildParagraph()
		{
			byte[] array = new byte[9];
			BuildParagraph.attributeNamespaceIds = array;
			BuildParagraph.eleTagNames = new string[] { "tmplLst" };
			BuildParagraph.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040094AF RID: 38063
		private const string tagName = "bldP";

		// Token: 0x040094B0 RID: 38064
		private const byte tagNsId = 24;

		// Token: 0x040094B1 RID: 38065
		internal const int ElementTypeIdConst = 12234;

		// Token: 0x040094B2 RID: 38066
		private static string[] attributeTagNames = new string[] { "spid", "grpId", "uiExpand", "build", "bldLvl", "animBg", "autoUpdateAnimBg", "rev", "advAuto" };

		// Token: 0x040094B3 RID: 38067
		private static byte[] attributeNamespaceIds;

		// Token: 0x040094B4 RID: 38068
		private static readonly string[] eleTagNames;

		// Token: 0x040094B5 RID: 38069
		private static readonly byte[] eleNamespaceIds;
	}
}
