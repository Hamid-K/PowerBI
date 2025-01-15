using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0A RID: 12042
	[GeneratedCode("DomGen", "2.0")]
	internal class TableLayout : OpenXmlLeafElement
	{
		// Token: 0x17008E06 RID: 36358
		// (get) Token: 0x06019B61 RID: 105313 RVA: 0x003542E4 File Offset: 0x003524E4
		public override string LocalName
		{
			get
			{
				return "tblLayout";
			}
		}

		// Token: 0x17008E07 RID: 36359
		// (get) Token: 0x06019B62 RID: 105314 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E08 RID: 36360
		// (get) Token: 0x06019B63 RID: 105315 RVA: 0x003542EB File Offset: 0x003524EB
		internal override int ElementTypeId
		{
			get
			{
				return 11680;
			}
		}

		// Token: 0x06019B64 RID: 105316 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008E09 RID: 36361
		// (get) Token: 0x06019B65 RID: 105317 RVA: 0x003542F2 File Offset: 0x003524F2
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableLayout.attributeTagNames;
			}
		}

		// Token: 0x17008E0A RID: 36362
		// (get) Token: 0x06019B66 RID: 105318 RVA: 0x003542F9 File Offset: 0x003524F9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableLayout.attributeNamespaceIds;
			}
		}

		// Token: 0x17008E0B RID: 36363
		// (get) Token: 0x06019B67 RID: 105319 RVA: 0x00354300 File Offset: 0x00352500
		// (set) Token: 0x06019B68 RID: 105320 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<TableLayoutValues> Type
		{
			get
			{
				return (EnumValue<TableLayoutValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019B6A RID: 105322 RVA: 0x0035430F File Offset: 0x0035250F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<TableLayoutValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B6B RID: 105323 RVA: 0x00354331 File Offset: 0x00352531
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableLayout>(deep);
		}

		// Token: 0x0400AA3C RID: 43580
		private const string tagName = "tblLayout";

		// Token: 0x0400AA3D RID: 43581
		private const byte tagNsId = 23;

		// Token: 0x0400AA3E RID: 43582
		internal const int ElementTypeIdConst = 11680;

		// Token: 0x0400AA3F RID: 43583
		private static string[] attributeTagNames = new string[] { "type" };

		// Token: 0x0400AA40 RID: 43584
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
