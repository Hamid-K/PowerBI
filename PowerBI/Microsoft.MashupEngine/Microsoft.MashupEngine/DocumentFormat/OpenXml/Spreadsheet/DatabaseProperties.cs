using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD3 RID: 11475
	[GeneratedCode("DomGen", "2.0")]
	internal class DatabaseProperties : OpenXmlLeafElement
	{
		// Token: 0x1700856F RID: 34159
		// (get) Token: 0x06018985 RID: 100741 RVA: 0x00342DDE File Offset: 0x00340FDE
		public override string LocalName
		{
			get
			{
				return "dbPr";
			}
		}

		// Token: 0x17008570 RID: 34160
		// (get) Token: 0x06018986 RID: 100742 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008571 RID: 34161
		// (get) Token: 0x06018987 RID: 100743 RVA: 0x00342DE5 File Offset: 0x00340FE5
		internal override int ElementTypeId
		{
			get
			{
				return 11456;
			}
		}

		// Token: 0x06018988 RID: 100744 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008572 RID: 34162
		// (get) Token: 0x06018989 RID: 100745 RVA: 0x00342DEC File Offset: 0x00340FEC
		internal override string[] AttributeTagNames
		{
			get
			{
				return DatabaseProperties.attributeTagNames;
			}
		}

		// Token: 0x17008573 RID: 34163
		// (get) Token: 0x0601898A RID: 100746 RVA: 0x00342DF3 File Offset: 0x00340FF3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DatabaseProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008574 RID: 34164
		// (get) Token: 0x0601898B RID: 100747 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601898C RID: 100748 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "connection")]
		public StringValue Connection
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

		// Token: 0x17008575 RID: 34165
		// (get) Token: 0x0601898D RID: 100749 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601898E RID: 100750 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "command")]
		public StringValue Command
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008576 RID: 34166
		// (get) Token: 0x0601898F RID: 100751 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018990 RID: 100752 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "serverCommand")]
		public StringValue ServerCommand
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008577 RID: 34167
		// (get) Token: 0x06018991 RID: 100753 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06018992 RID: 100754 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "commandType")]
		public UInt32Value CommandType
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06018994 RID: 100756 RVA: 0x00342DFC File Offset: 0x00340FFC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "connection" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "command" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "serverCommand" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "commandType" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018995 RID: 100757 RVA: 0x00342E69 File Offset: 0x00341069
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DatabaseProperties>(deep);
		}

		// Token: 0x06018996 RID: 100758 RVA: 0x00342E74 File Offset: 0x00341074
		// Note: this type is marked as 'beforefieldinit'.
		static DatabaseProperties()
		{
			byte[] array = new byte[4];
			DatabaseProperties.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0FC RID: 41212
		private const string tagName = "dbPr";

		// Token: 0x0400A0FD RID: 41213
		private const byte tagNsId = 22;

		// Token: 0x0400A0FE RID: 41214
		internal const int ElementTypeIdConst = 11456;

		// Token: 0x0400A0FF RID: 41215
		private static string[] attributeTagNames = new string[] { "connection", "command", "serverCommand", "commandType" };

		// Token: 0x0400A100 RID: 41216
		private static byte[] attributeNamespaceIds;
	}
}
