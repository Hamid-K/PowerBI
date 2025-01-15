using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B62 RID: 11106
	[GeneratedCode("DomGen", "2.0")]
	internal class FieldUsage : OpenXmlLeafElement
	{
		// Token: 0x170078E2 RID: 30946
		// (get) Token: 0x06016D63 RID: 93539 RVA: 0x0032FA6E File Offset: 0x0032DC6E
		public override string LocalName
		{
			get
			{
				return "fieldUsage";
			}
		}

		// Token: 0x170078E3 RID: 30947
		// (get) Token: 0x06016D64 RID: 93540 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078E4 RID: 30948
		// (get) Token: 0x06016D65 RID: 93541 RVA: 0x0032FA75 File Offset: 0x0032DC75
		internal override int ElementTypeId
		{
			get
			{
				return 11085;
			}
		}

		// Token: 0x06016D66 RID: 93542 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078E5 RID: 30949
		// (get) Token: 0x06016D67 RID: 93543 RVA: 0x0032FA7C File Offset: 0x0032DC7C
		internal override string[] AttributeTagNames
		{
			get
			{
				return FieldUsage.attributeTagNames;
			}
		}

		// Token: 0x170078E6 RID: 30950
		// (get) Token: 0x06016D68 RID: 93544 RVA: 0x0032FA83 File Offset: 0x0032DC83
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FieldUsage.attributeNamespaceIds;
			}
		}

		// Token: 0x170078E7 RID: 30951
		// (get) Token: 0x06016D69 RID: 93545 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016D6A RID: 93546 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int32Value Index
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

		// Token: 0x06016D6C RID: 93548 RVA: 0x0032FA8A File Offset: 0x0032DC8A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D6D RID: 93549 RVA: 0x0032FAAA File Offset: 0x0032DCAA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldUsage>(deep);
		}

		// Token: 0x06016D6E RID: 93550 RVA: 0x0032FAB4 File Offset: 0x0032DCB4
		// Note: this type is marked as 'beforefieldinit'.
		static FieldUsage()
		{
			byte[] array = new byte[1];
			FieldUsage.attributeNamespaceIds = array;
		}

		// Token: 0x04009A1A RID: 39450
		private const string tagName = "fieldUsage";

		// Token: 0x04009A1B RID: 39451
		private const byte tagNsId = 22;

		// Token: 0x04009A1C RID: 39452
		internal const int ElementTypeIdConst = 11085;

		// Token: 0x04009A1D RID: 39453
		private static string[] attributeTagNames = new string[] { "x" };

		// Token: 0x04009A1E RID: 39454
		private static byte[] attributeNamespaceIds;
	}
}
