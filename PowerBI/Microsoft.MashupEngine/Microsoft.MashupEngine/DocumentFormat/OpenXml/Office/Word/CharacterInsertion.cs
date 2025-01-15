using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002466 RID: 9318
	[GeneratedCode("DomGen", "2.0")]
	internal class CharacterInsertion : OpenXmlLeafElement
	{
		// Token: 0x170050C6 RID: 20678
		// (get) Token: 0x060112B3 RID: 70323 RVA: 0x002EB39A File Offset: 0x002E959A
		public override string LocalName
		{
			get
			{
				return "wch";
			}
		}

		// Token: 0x170050C7 RID: 20679
		// (get) Token: 0x060112B4 RID: 70324 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050C8 RID: 20680
		// (get) Token: 0x060112B5 RID: 70325 RVA: 0x002EB3A1 File Offset: 0x002E95A1
		internal override int ElementTypeId
		{
			get
			{
				return 12545;
			}
		}

		// Token: 0x060112B6 RID: 70326 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170050C9 RID: 20681
		// (get) Token: 0x060112B7 RID: 70327 RVA: 0x002EB3A8 File Offset: 0x002E95A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharacterInsertion.attributeTagNames;
			}
		}

		// Token: 0x170050CA RID: 20682
		// (get) Token: 0x060112B8 RID: 70328 RVA: 0x002EB3AF File Offset: 0x002E95AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharacterInsertion.attributeNamespaceIds;
			}
		}

		// Token: 0x170050CB RID: 20683
		// (get) Token: 0x060112B9 RID: 70329 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060112BA RID: 70330 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060112BC RID: 70332 RVA: 0x002EB3B6 File Offset: 0x002E95B6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060112BD RID: 70333 RVA: 0x002EB3D8 File Offset: 0x002E95D8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharacterInsertion>(deep);
		}

		// Token: 0x0400787C RID: 30844
		private const string tagName = "wch";

		// Token: 0x0400787D RID: 30845
		private const byte tagNsId = 33;

		// Token: 0x0400787E RID: 30846
		internal const int ElementTypeIdConst = 12545;

		// Token: 0x0400787F RID: 30847
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007880 RID: 30848
		private static byte[] attributeNamespaceIds = new byte[] { 33 };
	}
}
