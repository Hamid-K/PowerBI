using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B67 RID: 11111
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupMember : OpenXmlLeafElement
	{
		// Token: 0x17007910 RID: 30992
		// (get) Token: 0x06016DC9 RID: 93641 RVA: 0x0032FE7B File Offset: 0x0032E07B
		public override string LocalName
		{
			get
			{
				return "groupMember";
			}
		}

		// Token: 0x17007911 RID: 30993
		// (get) Token: 0x06016DCA RID: 93642 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007912 RID: 30994
		// (get) Token: 0x06016DCB RID: 93643 RVA: 0x0032FE82 File Offset: 0x0032E082
		internal override int ElementTypeId
		{
			get
			{
				return 11090;
			}
		}

		// Token: 0x06016DCC RID: 93644 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007913 RID: 30995
		// (get) Token: 0x06016DCD RID: 93645 RVA: 0x0032FE89 File Offset: 0x0032E089
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupMember.attributeTagNames;
			}
		}

		// Token: 0x17007914 RID: 30996
		// (get) Token: 0x06016DCE RID: 93646 RVA: 0x0032FE90 File Offset: 0x0032E090
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupMember.attributeNamespaceIds;
			}
		}

		// Token: 0x17007915 RID: 30997
		// (get) Token: 0x06016DCF RID: 93647 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016DD0 RID: 93648 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x17007916 RID: 30998
		// (get) Token: 0x06016DD1 RID: 93649 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06016DD2 RID: 93650 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "group")]
		public BooleanValue Group
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

		// Token: 0x06016DD4 RID: 93652 RVA: 0x0032FE97 File Offset: 0x0032E097
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "group" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016DD5 RID: 93653 RVA: 0x0032FECD File Offset: 0x0032E0CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupMember>(deep);
		}

		// Token: 0x06016DD6 RID: 93654 RVA: 0x0032FED8 File Offset: 0x0032E0D8
		// Note: this type is marked as 'beforefieldinit'.
		static GroupMember()
		{
			byte[] array = new byte[2];
			GroupMember.attributeNamespaceIds = array;
		}

		// Token: 0x04009A37 RID: 39479
		private const string tagName = "groupMember";

		// Token: 0x04009A38 RID: 39480
		private const byte tagNsId = 22;

		// Token: 0x04009A39 RID: 39481
		internal const int ElementTypeIdConst = 11090;

		// Token: 0x04009A3A RID: 39482
		private static string[] attributeTagNames = new string[] { "uniqueName", "group" };

		// Token: 0x04009A3B RID: 39483
		private static byte[] attributeNamespaceIds;
	}
}
