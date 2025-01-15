using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AB RID: 9387
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingFormat : OpenXmlLeafElement
	{
		// Token: 0x17005239 RID: 21049
		// (get) Token: 0x060115F6 RID: 71158 RVA: 0x002EDD88 File Offset: 0x002EBF88
		public override string LocalName
		{
			get
			{
				return "numForm";
			}
		}

		// Token: 0x1700523A RID: 21050
		// (get) Token: 0x060115F7 RID: 71159 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700523B RID: 21051
		// (get) Token: 0x060115F8 RID: 71160 RVA: 0x002EDD8F File Offset: 0x002EBF8F
		internal override int ElementTypeId
		{
			get
			{
				return 12861;
			}
		}

		// Token: 0x060115F9 RID: 71161 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700523C RID: 21052
		// (get) Token: 0x060115FA RID: 71162 RVA: 0x002EDD96 File Offset: 0x002EBF96
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingFormat.attributeTagNames;
			}
		}

		// Token: 0x1700523D RID: 21053
		// (get) Token: 0x060115FB RID: 71163 RVA: 0x002EDD9D File Offset: 0x002EBF9D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x1700523E RID: 21054
		// (get) Token: 0x060115FC RID: 71164 RVA: 0x002EDDA4 File Offset: 0x002EBFA4
		// (set) Token: 0x060115FD RID: 71165 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public EnumValue<NumberFormValues> Val
		{
			get
			{
				return (EnumValue<NumberFormValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060115FF RID: 71167 RVA: 0x002EDDB3 File Offset: 0x002EBFB3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<NumberFormValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011600 RID: 71168 RVA: 0x002EDDD5 File Offset: 0x002EBFD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingFormat>(deep);
		}

		// Token: 0x0400797F RID: 31103
		private const string tagName = "numForm";

		// Token: 0x04007980 RID: 31104
		private const byte tagNsId = 52;

		// Token: 0x04007981 RID: 31105
		internal const int ElementTypeIdConst = 12861;

		// Token: 0x04007982 RID: 31106
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007983 RID: 31107
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
