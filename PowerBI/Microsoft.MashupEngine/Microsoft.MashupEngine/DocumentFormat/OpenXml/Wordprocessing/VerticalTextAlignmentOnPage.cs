using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4A RID: 11850
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalTextAlignmentOnPage : OpenXmlLeafElement
	{
		// Token: 0x17008A16 RID: 35350
		// (get) Token: 0x06019300 RID: 103168 RVA: 0x003475A8 File Offset: 0x003457A8
		public override string LocalName
		{
			get
			{
				return "vAlign";
			}
		}

		// Token: 0x17008A17 RID: 35351
		// (get) Token: 0x06019301 RID: 103169 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A18 RID: 35352
		// (get) Token: 0x06019302 RID: 103170 RVA: 0x003475AF File Offset: 0x003457AF
		internal override int ElementTypeId
		{
			get
			{
				return 11537;
			}
		}

		// Token: 0x06019303 RID: 103171 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008A19 RID: 35353
		// (get) Token: 0x06019304 RID: 103172 RVA: 0x003475B6 File Offset: 0x003457B6
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalTextAlignmentOnPage.attributeTagNames;
			}
		}

		// Token: 0x17008A1A RID: 35354
		// (get) Token: 0x06019305 RID: 103173 RVA: 0x003475BD File Offset: 0x003457BD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalTextAlignmentOnPage.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A1B RID: 35355
		// (get) Token: 0x06019306 RID: 103174 RVA: 0x003475C4 File Offset: 0x003457C4
		// (set) Token: 0x06019307 RID: 103175 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<VerticalJustificationValues> Val
		{
			get
			{
				return (EnumValue<VerticalJustificationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019309 RID: 103177 RVA: 0x003475D3 File Offset: 0x003457D3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<VerticalJustificationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601930A RID: 103178 RVA: 0x003475F5 File Offset: 0x003457F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalTextAlignmentOnPage>(deep);
		}

		// Token: 0x0400A775 RID: 42869
		private const string tagName = "vAlign";

		// Token: 0x0400A776 RID: 42870
		private const byte tagNsId = 23;

		// Token: 0x0400A777 RID: 42871
		internal const int ElementTypeIdConst = 11537;

		// Token: 0x0400A778 RID: 42872
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A779 RID: 42873
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
