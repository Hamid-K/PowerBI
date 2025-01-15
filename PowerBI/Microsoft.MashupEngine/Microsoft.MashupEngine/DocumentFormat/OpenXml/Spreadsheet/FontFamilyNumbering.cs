using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C17 RID: 11287
	[GeneratedCode("DomGen", "2.0")]
	internal class FontFamilyNumbering : OpenXmlLeafElement
	{
		// Token: 0x17008030 RID: 32816
		// (get) Token: 0x06017D00 RID: 97536 RVA: 0x0033375B File Offset: 0x0033195B
		public override string LocalName
		{
			get
			{
				return "family";
			}
		}

		// Token: 0x17008031 RID: 32817
		// (get) Token: 0x06017D01 RID: 97537 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008032 RID: 32818
		// (get) Token: 0x06017D02 RID: 97538 RVA: 0x0033B7EB File Offset: 0x003399EB
		internal override int ElementTypeId
		{
			get
			{
				return 11268;
			}
		}

		// Token: 0x06017D03 RID: 97539 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008033 RID: 32819
		// (get) Token: 0x06017D04 RID: 97540 RVA: 0x0033B7F2 File Offset: 0x003399F2
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontFamilyNumbering.attributeTagNames;
			}
		}

		// Token: 0x17008034 RID: 32820
		// (get) Token: 0x06017D05 RID: 97541 RVA: 0x0033B7F9 File Offset: 0x003399F9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontFamilyNumbering.attributeNamespaceIds;
			}
		}

		// Token: 0x17008035 RID: 32821
		// (get) Token: 0x06017D06 RID: 97542 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06017D07 RID: 97543 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
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

		// Token: 0x06017D09 RID: 97545 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D0A RID: 97546 RVA: 0x0033B800 File Offset: 0x00339A00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontFamilyNumbering>(deep);
		}

		// Token: 0x06017D0B RID: 97547 RVA: 0x0033B80C File Offset: 0x00339A0C
		// Note: this type is marked as 'beforefieldinit'.
		static FontFamilyNumbering()
		{
			byte[] array = new byte[1];
			FontFamilyNumbering.attributeNamespaceIds = array;
		}

		// Token: 0x04009DA6 RID: 40358
		private const string tagName = "family";

		// Token: 0x04009DA7 RID: 40359
		private const byte tagNsId = 22;

		// Token: 0x04009DA8 RID: 40360
		internal const int ElementTypeIdConst = 11268;

		// Token: 0x04009DA9 RID: 40361
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009DAA RID: 40362
		private static byte[] attributeNamespaceIds;
	}
}
