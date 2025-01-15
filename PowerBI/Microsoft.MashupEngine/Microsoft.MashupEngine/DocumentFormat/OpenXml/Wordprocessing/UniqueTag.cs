using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6E RID: 12142
	[GeneratedCode("DomGen", "2.0")]
	internal class UniqueTag : OpenXmlLeafElement
	{
		// Token: 0x170090DA RID: 37082
		// (get) Token: 0x0601A1D1 RID: 106961 RVA: 0x0035D963 File Offset: 0x0035BB63
		public override string LocalName
		{
			get
			{
				return "uniqueTag";
			}
		}

		// Token: 0x170090DB RID: 37083
		// (get) Token: 0x0601A1D2 RID: 106962 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090DC RID: 37084
		// (get) Token: 0x0601A1D3 RID: 106963 RVA: 0x0035D96A File Offset: 0x0035BB6A
		internal override int ElementTypeId
		{
			get
			{
				return 11798;
			}
		}

		// Token: 0x0601A1D4 RID: 106964 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170090DD RID: 37085
		// (get) Token: 0x0601A1D5 RID: 106965 RVA: 0x0035D971 File Offset: 0x0035BB71
		internal override string[] AttributeTagNames
		{
			get
			{
				return UniqueTag.attributeTagNames;
			}
		}

		// Token: 0x170090DE RID: 37086
		// (get) Token: 0x0601A1D6 RID: 106966 RVA: 0x0035D978 File Offset: 0x0035BB78
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UniqueTag.attributeNamespaceIds;
			}
		}

		// Token: 0x170090DF RID: 37087
		// (get) Token: 0x0601A1D7 RID: 106967 RVA: 0x002BDD8A File Offset: 0x002BBF8A
		// (set) Token: 0x0601A1D8 RID: 106968 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Base64BinaryValue Val
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A1DA RID: 106970 RVA: 0x0035D97F File Offset: 0x0035BB7F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Base64BinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A1DB RID: 106971 RVA: 0x0035D9A1 File Offset: 0x0035BBA1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UniqueTag>(deep);
		}

		// Token: 0x0400ABDB RID: 43995
		private const string tagName = "uniqueTag";

		// Token: 0x0400ABDC RID: 43996
		private const byte tagNsId = 23;

		// Token: 0x0400ABDD RID: 43997
		internal const int ElementTypeIdConst = 11798;

		// Token: 0x0400ABDE RID: 43998
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABDF RID: 43999
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
