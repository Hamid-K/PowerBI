using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E76 RID: 11894
	[GeneratedCode("DomGen", "2.0")]
	internal class SymbolChar : OpenXmlLeafElement
	{
		// Token: 0x17008AA3 RID: 35491
		// (get) Token: 0x06019427 RID: 103463 RVA: 0x0030658B File Offset: 0x0030478B
		public override string LocalName
		{
			get
			{
				return "sym";
			}
		}

		// Token: 0x17008AA4 RID: 35492
		// (get) Token: 0x06019428 RID: 103464 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AA5 RID: 35493
		// (get) Token: 0x06019429 RID: 103465 RVA: 0x00347C37 File Offset: 0x00345E37
		internal override int ElementTypeId
		{
			get
			{
				return 11561;
			}
		}

		// Token: 0x0601942A RID: 103466 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008AA6 RID: 35494
		// (get) Token: 0x0601942B RID: 103467 RVA: 0x00347C3E File Offset: 0x00345E3E
		internal override string[] AttributeTagNames
		{
			get
			{
				return SymbolChar.attributeTagNames;
			}
		}

		// Token: 0x17008AA7 RID: 35495
		// (get) Token: 0x0601942C RID: 103468 RVA: 0x00347C45 File Offset: 0x00345E45
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SymbolChar.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AA8 RID: 35496
		// (get) Token: 0x0601942D RID: 103469 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601942E RID: 103470 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "font")]
		public StringValue Font
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

		// Token: 0x17008AA9 RID: 35497
		// (get) Token: 0x0601942F RID: 103471 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06019430 RID: 103472 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "char")]
		public HexBinaryValue Char
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

		// Token: 0x06019432 RID: 103474 RVA: 0x00347C4C File Offset: 0x00345E4C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "font" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "char" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019433 RID: 103475 RVA: 0x00347C86 File Offset: 0x00345E86
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SymbolChar>(deep);
		}

		// Token: 0x0400A7FA RID: 43002
		private const string tagName = "sym";

		// Token: 0x0400A7FB RID: 43003
		private const byte tagNsId = 23;

		// Token: 0x0400A7FC RID: 43004
		internal const int ElementTypeIdConst = 11561;

		// Token: 0x0400A7FD RID: 43005
		private static string[] attributeTagNames = new string[] { "font", "char" };

		// Token: 0x0400A7FE RID: 43006
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
