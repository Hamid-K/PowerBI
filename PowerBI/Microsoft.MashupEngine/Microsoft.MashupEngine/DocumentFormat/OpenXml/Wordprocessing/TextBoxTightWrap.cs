using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2E RID: 11822
	[GeneratedCode("DomGen", "2.0")]
	internal class TextBoxTightWrap : OpenXmlLeafElement
	{
		// Token: 0x17008973 RID: 35187
		// (get) Token: 0x060191AE RID: 102830 RVA: 0x003466F8 File Offset: 0x003448F8
		public override string LocalName
		{
			get
			{
				return "textboxTightWrap";
			}
		}

		// Token: 0x17008974 RID: 35188
		// (get) Token: 0x060191AF RID: 102831 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008975 RID: 35189
		// (get) Token: 0x060191B0 RID: 102832 RVA: 0x003466FF File Offset: 0x003448FF
		internal override int ElementTypeId
		{
			get
			{
				return 11521;
			}
		}

		// Token: 0x060191B1 RID: 102833 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008976 RID: 35190
		// (get) Token: 0x060191B2 RID: 102834 RVA: 0x00346706 File Offset: 0x00344906
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextBoxTightWrap.attributeTagNames;
			}
		}

		// Token: 0x17008977 RID: 35191
		// (get) Token: 0x060191B3 RID: 102835 RVA: 0x0034670D File Offset: 0x0034490D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextBoxTightWrap.attributeNamespaceIds;
			}
		}

		// Token: 0x17008978 RID: 35192
		// (get) Token: 0x060191B4 RID: 102836 RVA: 0x00346714 File Offset: 0x00344914
		// (set) Token: 0x060191B5 RID: 102837 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TextBoxTightWrapValues> Val
		{
			get
			{
				return (EnumValue<TextBoxTightWrapValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060191B7 RID: 102839 RVA: 0x00346723 File Offset: 0x00344923
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TextBoxTightWrapValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060191B8 RID: 102840 RVA: 0x00346745 File Offset: 0x00344945
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBoxTightWrap>(deep);
		}

		// Token: 0x0400A707 RID: 42759
		private const string tagName = "textboxTightWrap";

		// Token: 0x0400A708 RID: 42760
		private const byte tagNsId = 23;

		// Token: 0x0400A709 RID: 42761
		internal const int ElementTypeIdConst = 11521;

		// Token: 0x0400A70A RID: 42762
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A70B RID: 42763
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
