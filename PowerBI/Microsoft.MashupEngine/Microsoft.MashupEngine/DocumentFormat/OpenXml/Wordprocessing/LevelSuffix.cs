using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F96 RID: 12182
	[GeneratedCode("DomGen", "2.0")]
	internal class LevelSuffix : OpenXmlLeafElement
	{
		// Token: 0x170091EF RID: 37359
		// (get) Token: 0x0601A421 RID: 107553 RVA: 0x0035FA6F File Offset: 0x0035DC6F
		public override string LocalName
		{
			get
			{
				return "suff";
			}
		}

		// Token: 0x170091F0 RID: 37360
		// (get) Token: 0x0601A422 RID: 107554 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091F1 RID: 37361
		// (get) Token: 0x0601A423 RID: 107555 RVA: 0x0035FA76 File Offset: 0x0035DC76
		internal override int ElementTypeId
		{
			get
			{
				return 11867;
			}
		}

		// Token: 0x0601A424 RID: 107556 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091F2 RID: 37362
		// (get) Token: 0x0601A425 RID: 107557 RVA: 0x0035FA7D File Offset: 0x0035DC7D
		internal override string[] AttributeTagNames
		{
			get
			{
				return LevelSuffix.attributeTagNames;
			}
		}

		// Token: 0x170091F3 RID: 37363
		// (get) Token: 0x0601A426 RID: 107558 RVA: 0x0035FA84 File Offset: 0x0035DC84
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LevelSuffix.attributeNamespaceIds;
			}
		}

		// Token: 0x170091F4 RID: 37364
		// (get) Token: 0x0601A427 RID: 107559 RVA: 0x0035FA8B File Offset: 0x0035DC8B
		// (set) Token: 0x0601A428 RID: 107560 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<LevelSuffixValues> Val
		{
			get
			{
				return (EnumValue<LevelSuffixValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A42A RID: 107562 RVA: 0x0035FA9A File Offset: 0x0035DC9A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<LevelSuffixValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A42B RID: 107563 RVA: 0x0035FABC File Offset: 0x0035DCBC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelSuffix>(deep);
		}

		// Token: 0x0400AC77 RID: 44151
		private const string tagName = "suff";

		// Token: 0x0400AC78 RID: 44152
		private const byte tagNsId = 23;

		// Token: 0x0400AC79 RID: 44153
		internal const int ElementTypeIdConst = 11867;

		// Token: 0x0400AC7A RID: 44154
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC7B RID: 44155
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
