using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E29 RID: 11817
	[GeneratedCode("DomGen", "2.0")]
	internal class SpacingBetweenLines : OpenXmlLeafElement
	{
		// Token: 0x17008943 RID: 35139
		// (get) Token: 0x0601914E RID: 102734 RVA: 0x003461D1 File Offset: 0x003443D1
		public override string LocalName
		{
			get
			{
				return "spacing";
			}
		}

		// Token: 0x17008944 RID: 35140
		// (get) Token: 0x0601914F RID: 102735 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008945 RID: 35141
		// (get) Token: 0x06019150 RID: 102736 RVA: 0x003461D8 File Offset: 0x003443D8
		internal override int ElementTypeId
		{
			get
			{
				return 11513;
			}
		}

		// Token: 0x06019151 RID: 102737 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008946 RID: 35142
		// (get) Token: 0x06019152 RID: 102738 RVA: 0x003461DF File Offset: 0x003443DF
		internal override string[] AttributeTagNames
		{
			get
			{
				return SpacingBetweenLines.attributeTagNames;
			}
		}

		// Token: 0x17008947 RID: 35143
		// (get) Token: 0x06019153 RID: 102739 RVA: 0x003461E6 File Offset: 0x003443E6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SpacingBetweenLines.attributeNamespaceIds;
			}
		}

		// Token: 0x17008948 RID: 35144
		// (get) Token: 0x06019154 RID: 102740 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019155 RID: 102741 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "before")]
		public StringValue Before
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

		// Token: 0x17008949 RID: 35145
		// (get) Token: 0x06019156 RID: 102742 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06019157 RID: 102743 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "beforeLines")]
		public Int32Value BeforeLines
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700894A RID: 35146
		// (get) Token: 0x06019158 RID: 102744 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x06019159 RID: 102745 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "beforeAutospacing")]
		public OnOffValue BeforeAutoSpacing
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700894B RID: 35147
		// (get) Token: 0x0601915A RID: 102746 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601915B RID: 102747 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "after")]
		public StringValue After
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700894C RID: 35148
		// (get) Token: 0x0601915C RID: 102748 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0601915D RID: 102749 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "afterLines")]
		public Int32Value AfterLines
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700894D RID: 35149
		// (get) Token: 0x0601915E RID: 102750 RVA: 0x003461FC File Offset: 0x003443FC
		// (set) Token: 0x0601915F RID: 102751 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "afterAutospacing")]
		public OnOffValue AfterAutoSpacing
		{
			get
			{
				return (OnOffValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700894E RID: 35150
		// (get) Token: 0x06019160 RID: 102752 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06019161 RID: 102753 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "line")]
		public StringValue Line
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700894F RID: 35151
		// (get) Token: 0x06019162 RID: 102754 RVA: 0x0034620B File Offset: 0x0034440B
		// (set) Token: 0x06019163 RID: 102755 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "lineRule")]
		public EnumValue<LineSpacingRuleValues> LineRule
		{
			get
			{
				return (EnumValue<LineSpacingRuleValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06019165 RID: 102757 RVA: 0x0034621C File Offset: 0x0034441C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "before" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "beforeLines" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "beforeAutospacing" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "after" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "afterLines" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "afterAutospacing" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "line" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "lineRule" == name)
			{
				return new EnumValue<LineSpacingRuleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019166 RID: 102758 RVA: 0x003462F1 File Offset: 0x003444F1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpacingBetweenLines>(deep);
		}

		// Token: 0x0400A6EE RID: 42734
		private const string tagName = "spacing";

		// Token: 0x0400A6EF RID: 42735
		private const byte tagNsId = 23;

		// Token: 0x0400A6F0 RID: 42736
		internal const int ElementTypeIdConst = 11513;

		// Token: 0x0400A6F1 RID: 42737
		private static string[] attributeTagNames = new string[] { "before", "beforeLines", "beforeAutospacing", "after", "afterLines", "afterAutospacing", "line", "lineRule" };

		// Token: 0x0400A6F2 RID: 42738
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
