using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AA RID: 9386
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Ligatures : OpenXmlLeafElement
	{
		// Token: 0x17005233 RID: 21043
		// (get) Token: 0x060115EA RID: 71146 RVA: 0x002EDCFB File Offset: 0x002EBEFB
		public override string LocalName
		{
			get
			{
				return "ligatures";
			}
		}

		// Token: 0x17005234 RID: 21044
		// (get) Token: 0x060115EB RID: 71147 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005235 RID: 21045
		// (get) Token: 0x060115EC RID: 71148 RVA: 0x002EDD02 File Offset: 0x002EBF02
		internal override int ElementTypeId
		{
			get
			{
				return 12860;
			}
		}

		// Token: 0x060115ED RID: 71149 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005236 RID: 21046
		// (get) Token: 0x060115EE RID: 71150 RVA: 0x002EDD09 File Offset: 0x002EBF09
		internal override string[] AttributeTagNames
		{
			get
			{
				return Ligatures.attributeTagNames;
			}
		}

		// Token: 0x17005237 RID: 21047
		// (get) Token: 0x060115EF RID: 71151 RVA: 0x002EDD10 File Offset: 0x002EBF10
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Ligatures.attributeNamespaceIds;
			}
		}

		// Token: 0x17005238 RID: 21048
		// (get) Token: 0x060115F0 RID: 71152 RVA: 0x002EDD17 File Offset: 0x002EBF17
		// (set) Token: 0x060115F1 RID: 71153 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public EnumValue<LigaturesValues> Val
		{
			get
			{
				return (EnumValue<LigaturesValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060115F3 RID: 71155 RVA: 0x002EDD26 File Offset: 0x002EBF26
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<LigaturesValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060115F4 RID: 71156 RVA: 0x002EDD48 File Offset: 0x002EBF48
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ligatures>(deep);
		}

		// Token: 0x0400797A RID: 31098
		private const string tagName = "ligatures";

		// Token: 0x0400797B RID: 31099
		private const byte tagNsId = 52;

		// Token: 0x0400797C RID: 31100
		internal const int ElementTypeIdConst = 12860;

		// Token: 0x0400797D RID: 31101
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400797E RID: 31102
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
