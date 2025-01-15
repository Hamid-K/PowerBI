using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FCB RID: 12235
	[GeneratedCode("DomGen", "2.0")]
	internal class Caption : OpenXmlLeafElement
	{
		// Token: 0x17009420 RID: 37920
		// (get) Token: 0x0601A8B9 RID: 108729 RVA: 0x00363E6D File Offset: 0x0036206D
		public override string LocalName
		{
			get
			{
				return "caption";
			}
		}

		// Token: 0x17009421 RID: 37921
		// (get) Token: 0x0601A8BA RID: 108730 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009422 RID: 37922
		// (get) Token: 0x0601A8BB RID: 108731 RVA: 0x00363E74 File Offset: 0x00362074
		internal override int ElementTypeId
		{
			get
			{
				return 11943;
			}
		}

		// Token: 0x0601A8BC RID: 108732 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009423 RID: 37923
		// (get) Token: 0x0601A8BD RID: 108733 RVA: 0x00363E7B File Offset: 0x0036207B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Caption.attributeTagNames;
			}
		}

		// Token: 0x17009424 RID: 37924
		// (get) Token: 0x0601A8BE RID: 108734 RVA: 0x00363E82 File Offset: 0x00362082
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Caption.attributeNamespaceIds;
			}
		}

		// Token: 0x17009425 RID: 37925
		// (get) Token: 0x0601A8BF RID: 108735 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A8C0 RID: 108736 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17009426 RID: 37926
		// (get) Token: 0x0601A8C1 RID: 108737 RVA: 0x00363E89 File Offset: 0x00362089
		// (set) Token: 0x0601A8C2 RID: 108738 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "pos")]
		public EnumValue<CaptionPositionValues> Position
		{
			get
			{
				return (EnumValue<CaptionPositionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009427 RID: 37927
		// (get) Token: 0x0601A8C3 RID: 108739 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601A8C4 RID: 108740 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "chapNum")]
		public OnOffValue ChapterNumber
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

		// Token: 0x17009428 RID: 37928
		// (get) Token: 0x0601A8C5 RID: 108741 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601A8C6 RID: 108742 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "heading")]
		public Int32Value Heading
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17009429 RID: 37929
		// (get) Token: 0x0601A8C7 RID: 108743 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601A8C8 RID: 108744 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "noLabel")]
		public OnOffValue NoLabel
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700942A RID: 37930
		// (get) Token: 0x0601A8C9 RID: 108745 RVA: 0x00363E98 File Offset: 0x00362098
		// (set) Token: 0x0601A8CA RID: 108746 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "numFmt")]
		public EnumValue<NumberFormatValues> NumberFormat
		{
			get
			{
				return (EnumValue<NumberFormatValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700942B RID: 37931
		// (get) Token: 0x0601A8CB RID: 108747 RVA: 0x00363EA7 File Offset: 0x003620A7
		// (set) Token: 0x0601A8CC RID: 108748 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "sep")]
		public EnumValue<ChapterSeparatorValues> Separator
		{
			get
			{
				return (EnumValue<ChapterSeparatorValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0601A8CE RID: 108750 RVA: 0x00363EB8 File Offset: 0x003620B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "pos" == name)
			{
				return new EnumValue<CaptionPositionValues>();
			}
			if (23 == namespaceId && "chapNum" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "heading" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "noLabel" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "numFmt" == name)
			{
				return new EnumValue<NumberFormatValues>();
			}
			if (23 == namespaceId && "sep" == name)
			{
				return new EnumValue<ChapterSeparatorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A8CF RID: 108751 RVA: 0x00363F75 File Offset: 0x00362175
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Caption>(deep);
		}

		// Token: 0x0400AD73 RID: 44403
		private const string tagName = "caption";

		// Token: 0x0400AD74 RID: 44404
		private const byte tagNsId = 23;

		// Token: 0x0400AD75 RID: 44405
		internal const int ElementTypeIdConst = 11943;

		// Token: 0x0400AD76 RID: 44406
		private static string[] attributeTagNames = new string[] { "name", "pos", "chapNum", "heading", "noLabel", "numFmt", "sep" };

		// Token: 0x0400AD77 RID: 44407
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
