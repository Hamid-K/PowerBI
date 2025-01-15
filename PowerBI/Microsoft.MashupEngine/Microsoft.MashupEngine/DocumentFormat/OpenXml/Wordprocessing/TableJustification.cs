using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F02 RID: 12034
	[GeneratedCode("DomGen", "2.0")]
	internal class TableJustification : OpenXmlLeafElement
	{
		// Token: 0x17008DCD RID: 36301
		// (get) Token: 0x06019AEC RID: 105196 RVA: 0x0031DF1C File Offset: 0x0031C11C
		public override string LocalName
		{
			get
			{
				return "jc";
			}
		}

		// Token: 0x17008DCE RID: 36302
		// (get) Token: 0x06019AED RID: 105197 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DCF RID: 36303
		// (get) Token: 0x06019AEE RID: 105198 RVA: 0x00353D55 File Offset: 0x00351F55
		internal override int ElementTypeId
		{
			get
			{
				return 11670;
			}
		}

		// Token: 0x06019AEF RID: 105199 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DD0 RID: 36304
		// (get) Token: 0x06019AF0 RID: 105200 RVA: 0x00353D5C File Offset: 0x00351F5C
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableJustification.attributeTagNames;
			}
		}

		// Token: 0x17008DD1 RID: 36305
		// (get) Token: 0x06019AF1 RID: 105201 RVA: 0x00353D63 File Offset: 0x00351F63
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableJustification.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DD2 RID: 36306
		// (get) Token: 0x06019AF2 RID: 105202 RVA: 0x00353D6A File Offset: 0x00351F6A
		// (set) Token: 0x06019AF3 RID: 105203 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TableRowAlignmentValues> Val
		{
			get
			{
				return (EnumValue<TableRowAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019AF5 RID: 105205 RVA: 0x00353D79 File Offset: 0x00351F79
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TableRowAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019AF6 RID: 105206 RVA: 0x00353D9B File Offset: 0x00351F9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableJustification>(deep);
		}

		// Token: 0x0400AA1B RID: 43547
		private const string tagName = "jc";

		// Token: 0x0400AA1C RID: 43548
		private const byte tagNsId = 23;

		// Token: 0x0400AA1D RID: 43549
		internal const int ElementTypeIdConst = 11670;

		// Token: 0x0400AA1E RID: 43550
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA1F RID: 43551
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
