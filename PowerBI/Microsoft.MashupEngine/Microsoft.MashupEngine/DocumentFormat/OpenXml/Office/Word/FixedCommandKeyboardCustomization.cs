using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200245F RID: 9311
	[GeneratedCode("DomGen", "2.0")]
	internal class FixedCommandKeyboardCustomization : OpenXmlLeafElement
	{
		// Token: 0x170050AC RID: 20652
		// (get) Token: 0x0601127D RID: 70269 RVA: 0x002EB188 File Offset: 0x002E9388
		public override string LocalName
		{
			get
			{
				return "fci";
			}
		}

		// Token: 0x170050AD RID: 20653
		// (get) Token: 0x0601127E RID: 70270 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050AE RID: 20654
		// (get) Token: 0x0601127F RID: 70271 RVA: 0x002EB18F File Offset: 0x002E938F
		internal override int ElementTypeId
		{
			get
			{
				return 12541;
			}
		}

		// Token: 0x06011280 RID: 70272 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170050AF RID: 20655
		// (get) Token: 0x06011281 RID: 70273 RVA: 0x002EB196 File Offset: 0x002E9396
		internal override string[] AttributeTagNames
		{
			get
			{
				return FixedCommandKeyboardCustomization.attributeTagNames;
			}
		}

		// Token: 0x170050B0 RID: 20656
		// (get) Token: 0x06011282 RID: 70274 RVA: 0x002EB19D File Offset: 0x002E939D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FixedCommandKeyboardCustomization.attributeNamespaceIds;
			}
		}

		// Token: 0x170050B1 RID: 20657
		// (get) Token: 0x06011283 RID: 70275 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011284 RID: 70276 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "fciName")]
		public StringValue CommandName
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

		// Token: 0x170050B2 RID: 20658
		// (get) Token: 0x06011285 RID: 70277 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06011286 RID: 70278 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(33, "fciIndex")]
		public HexBinaryValue CommandIndex
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170050B3 RID: 20659
		// (get) Token: 0x06011287 RID: 70279 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06011288 RID: 70280 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(33, "swArg")]
		public HexBinaryValue Argument
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601128A RID: 70282 RVA: 0x002EB1B4 File Offset: 0x002E93B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "fciName" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "fciIndex" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "swArg" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601128B RID: 70283 RVA: 0x002EB211 File Offset: 0x002E9411
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FixedCommandKeyboardCustomization>(deep);
		}

		// Token: 0x04007867 RID: 30823
		private const string tagName = "fci";

		// Token: 0x04007868 RID: 30824
		private const byte tagNsId = 33;

		// Token: 0x04007869 RID: 30825
		internal const int ElementTypeIdConst = 12541;

		// Token: 0x0400786A RID: 30826
		private static string[] attributeTagNames = new string[] { "fciName", "fciIndex", "swArg" };

		// Token: 0x0400786B RID: 30827
		private static byte[] attributeNamespaceIds = new byte[] { 33, 33, 33 };
	}
}
