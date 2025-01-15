using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9C RID: 12188
	[GeneratedCode("DomGen", "2.0")]
	internal class MultiLevelType : OpenXmlLeafElement
	{
		// Token: 0x17009259 RID: 37465
		// (get) Token: 0x0601A4F9 RID: 107769 RVA: 0x003606C9 File Offset: 0x0035E8C9
		public override string LocalName
		{
			get
			{
				return "multiLevelType";
			}
		}

		// Token: 0x1700925A RID: 37466
		// (get) Token: 0x0601A4FA RID: 107770 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700925B RID: 37467
		// (get) Token: 0x0601A4FB RID: 107771 RVA: 0x003606D0 File Offset: 0x0035E8D0
		internal override int ElementTypeId
		{
			get
			{
				return 11875;
			}
		}

		// Token: 0x0601A4FC RID: 107772 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700925C RID: 37468
		// (get) Token: 0x0601A4FD RID: 107773 RVA: 0x003606D7 File Offset: 0x0035E8D7
		internal override string[] AttributeTagNames
		{
			get
			{
				return MultiLevelType.attributeTagNames;
			}
		}

		// Token: 0x1700925D RID: 37469
		// (get) Token: 0x0601A4FE RID: 107774 RVA: 0x003606DE File Offset: 0x0035E8DE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MultiLevelType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700925E RID: 37470
		// (get) Token: 0x0601A4FF RID: 107775 RVA: 0x003606E5 File Offset: 0x0035E8E5
		// (set) Token: 0x0601A500 RID: 107776 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MultiLevelValues> Val
		{
			get
			{
				return (EnumValue<MultiLevelValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A502 RID: 107778 RVA: 0x003606F4 File Offset: 0x0035E8F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MultiLevelValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A503 RID: 107779 RVA: 0x00360716 File Offset: 0x0035E916
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultiLevelType>(deep);
		}

		// Token: 0x0400AC95 RID: 44181
		private const string tagName = "multiLevelType";

		// Token: 0x0400AC96 RID: 44182
		private const byte tagNsId = 23;

		// Token: 0x0400AC97 RID: 44183
		internal const int ElementTypeIdConst = 11875;

		// Token: 0x0400AC98 RID: 44184
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC99 RID: 44185
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
