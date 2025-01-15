using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AC RID: 9388
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberSpacing : OpenXmlLeafElement
	{
		// Token: 0x1700523F RID: 21055
		// (get) Token: 0x06011602 RID: 71170 RVA: 0x002EDE14 File Offset: 0x002EC014
		public override string LocalName
		{
			get
			{
				return "numSpacing";
			}
		}

		// Token: 0x17005240 RID: 21056
		// (get) Token: 0x06011603 RID: 71171 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005241 RID: 21057
		// (get) Token: 0x06011604 RID: 71172 RVA: 0x002EDE1B File Offset: 0x002EC01B
		internal override int ElementTypeId
		{
			get
			{
				return 12862;
			}
		}

		// Token: 0x06011605 RID: 71173 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005242 RID: 21058
		// (get) Token: 0x06011606 RID: 71174 RVA: 0x002EDE22 File Offset: 0x002EC022
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberSpacing.attributeTagNames;
			}
		}

		// Token: 0x17005243 RID: 21059
		// (get) Token: 0x06011607 RID: 71175 RVA: 0x002EDE29 File Offset: 0x002EC029
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberSpacing.attributeNamespaceIds;
			}
		}

		// Token: 0x17005244 RID: 21060
		// (get) Token: 0x06011608 RID: 71176 RVA: 0x002EDE30 File Offset: 0x002EC030
		// (set) Token: 0x06011609 RID: 71177 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public EnumValue<NumberSpacingValues> Val
		{
			get
			{
				return (EnumValue<NumberSpacingValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601160B RID: 71179 RVA: 0x002EDE3F File Offset: 0x002EC03F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<NumberSpacingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601160C RID: 71180 RVA: 0x002EDE61 File Offset: 0x002EC061
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberSpacing>(deep);
		}

		// Token: 0x04007984 RID: 31108
		private const string tagName = "numSpacing";

		// Token: 0x04007985 RID: 31109
		private const byte tagNsId = 52;

		// Token: 0x04007986 RID: 31110
		internal const int ElementTypeIdConst = 12862;

		// Token: 0x04007987 RID: 31111
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007988 RID: 31112
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
