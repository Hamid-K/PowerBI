using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB1 RID: 12209
	[GeneratedCode("DomGen", "2.0")]
	internal class Panose1Number : OpenXmlLeafElement
	{
		// Token: 0x17009383 RID: 37763
		// (get) Token: 0x0601A76A RID: 108394 RVA: 0x00362B4C File Offset: 0x00360D4C
		public override string LocalName
		{
			get
			{
				return "panose1";
			}
		}

		// Token: 0x17009384 RID: 37764
		// (get) Token: 0x0601A76B RID: 108395 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009385 RID: 37765
		// (get) Token: 0x0601A76C RID: 108396 RVA: 0x00362B53 File Offset: 0x00360D53
		internal override int ElementTypeId
		{
			get
			{
				return 11916;
			}
		}

		// Token: 0x0601A76D RID: 108397 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009386 RID: 37766
		// (get) Token: 0x0601A76E RID: 108398 RVA: 0x00362B5A File Offset: 0x00360D5A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Panose1Number.attributeTagNames;
			}
		}

		// Token: 0x17009387 RID: 37767
		// (get) Token: 0x0601A76F RID: 108399 RVA: 0x00362B61 File Offset: 0x00360D61
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Panose1Number.attributeNamespaceIds;
			}
		}

		// Token: 0x17009388 RID: 37768
		// (get) Token: 0x0601A770 RID: 108400 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601A771 RID: 108401 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x0601A773 RID: 108403 RVA: 0x0035E18F File Offset: 0x0035C38F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A774 RID: 108404 RVA: 0x00362B68 File Offset: 0x00360D68
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Panose1Number>(deep);
		}

		// Token: 0x0400AD0A RID: 44298
		private const string tagName = "panose1";

		// Token: 0x0400AD0B RID: 44299
		private const byte tagNsId = 23;

		// Token: 0x0400AD0C RID: 44300
		internal const int ElementTypeIdConst = 11916;

		// Token: 0x0400AD0D RID: 44301
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD0E RID: 44302
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
