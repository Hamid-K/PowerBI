using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5D RID: 11357
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagProperties : OpenXmlLeafElement
	{
		// Token: 0x17008267 RID: 33383
		// (get) Token: 0x0601820E RID: 98830 RVA: 0x0033EC1D File Offset: 0x0033CE1D
		public override string LocalName
		{
			get
			{
				return "smartTagPr";
			}
		}

		// Token: 0x17008268 RID: 33384
		// (get) Token: 0x0601820F RID: 98831 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008269 RID: 33385
		// (get) Token: 0x06018210 RID: 98832 RVA: 0x0033EC24 File Offset: 0x0033CE24
		internal override int ElementTypeId
		{
			get
			{
				return 11338;
			}
		}

		// Token: 0x06018211 RID: 98833 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700826A RID: 33386
		// (get) Token: 0x06018212 RID: 98834 RVA: 0x0033EC2B File Offset: 0x0033CE2B
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTagProperties.attributeTagNames;
			}
		}

		// Token: 0x1700826B RID: 33387
		// (get) Token: 0x06018213 RID: 98835 RVA: 0x0033EC32 File Offset: 0x0033CE32
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTagProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700826C RID: 33388
		// (get) Token: 0x06018214 RID: 98836 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018215 RID: 98837 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "embed")]
		public BooleanValue Embed
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700826D RID: 33389
		// (get) Token: 0x06018216 RID: 98838 RVA: 0x0033EC39 File Offset: 0x0033CE39
		// (set) Token: 0x06018217 RID: 98839 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "show")]
		public EnumValue<SmartTagDisplayValues> Show
		{
			get
			{
				return (EnumValue<SmartTagDisplayValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06018219 RID: 98841 RVA: 0x0033EC48 File Offset: 0x0033CE48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "embed" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "show" == name)
			{
				return new EnumValue<SmartTagDisplayValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601821A RID: 98842 RVA: 0x0033EC7E File Offset: 0x0033CE7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagProperties>(deep);
		}

		// Token: 0x0601821B RID: 98843 RVA: 0x0033EC88 File Offset: 0x0033CE88
		// Note: this type is marked as 'beforefieldinit'.
		static SmartTagProperties()
		{
			byte[] array = new byte[2];
			SmartTagProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009EF6 RID: 40694
		private const string tagName = "smartTagPr";

		// Token: 0x04009EF7 RID: 40695
		private const byte tagNsId = 22;

		// Token: 0x04009EF8 RID: 40696
		internal const int ElementTypeIdConst = 11338;

		// Token: 0x04009EF9 RID: 40697
		private static string[] attributeTagNames = new string[] { "embed", "show" };

		// Token: 0x04009EFA RID: 40698
		private static byte[] attributeNamespaceIds;
	}
}
