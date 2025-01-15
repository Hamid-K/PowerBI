using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0A RID: 11274
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingFormat : OpenXmlLeafElement
	{
		// Token: 0x17007FA0 RID: 32672
		// (get) Token: 0x06017BCD RID: 97229 RVA: 0x002F0D56 File Offset: 0x002EEF56
		public override string LocalName
		{
			get
			{
				return "numFmt";
			}
		}

		// Token: 0x17007FA1 RID: 32673
		// (get) Token: 0x06017BCE RID: 97230 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FA2 RID: 32674
		// (get) Token: 0x06017BCF RID: 97231 RVA: 0x0033A826 File Offset: 0x00338A26
		internal override int ElementTypeId
		{
			get
			{
				return 11255;
			}
		}

		// Token: 0x06017BD0 RID: 97232 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007FA3 RID: 32675
		// (get) Token: 0x06017BD1 RID: 97233 RVA: 0x0033A82D File Offset: 0x00338A2D
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingFormat.attributeTagNames;
			}
		}

		// Token: 0x17007FA4 RID: 32676
		// (get) Token: 0x06017BD2 RID: 97234 RVA: 0x0033A834 File Offset: 0x00338A34
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17007FA5 RID: 32677
		// (get) Token: 0x06017BD3 RID: 97235 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017BD4 RID: 97236 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007FA6 RID: 32678
		// (get) Token: 0x06017BD5 RID: 97237 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017BD6 RID: 97238 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "formatCode")]
		public StringValue FormatCode
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

		// Token: 0x06017BD8 RID: 97240 RVA: 0x0033A83B File Offset: 0x00338A3B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "formatCode" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017BD9 RID: 97241 RVA: 0x0033A871 File Offset: 0x00338A71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingFormat>(deep);
		}

		// Token: 0x06017BDA RID: 97242 RVA: 0x0033A87C File Offset: 0x00338A7C
		// Note: this type is marked as 'beforefieldinit'.
		static NumberingFormat()
		{
			byte[] array = new byte[2];
			NumberingFormat.attributeNamespaceIds = array;
		}

		// Token: 0x04009D63 RID: 40291
		private const string tagName = "numFmt";

		// Token: 0x04009D64 RID: 40292
		private const byte tagNsId = 22;

		// Token: 0x04009D65 RID: 40293
		internal const int ElementTypeIdConst = 11255;

		// Token: 0x04009D66 RID: 40294
		private static string[] attributeTagNames = new string[] { "numFmtId", "formatCode" };

		// Token: 0x04009D67 RID: 40295
		private static byte[] attributeNamespaceIds;
	}
}
