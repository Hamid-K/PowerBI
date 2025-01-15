using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2D RID: 11821
	[GeneratedCode("DomGen", "2.0")]
	internal class TextAlignment : OpenXmlLeafElement
	{
		// Token: 0x1700896D RID: 35181
		// (get) Token: 0x060191A2 RID: 102818 RVA: 0x0034666C File Offset: 0x0034486C
		public override string LocalName
		{
			get
			{
				return "textAlignment";
			}
		}

		// Token: 0x1700896E RID: 35182
		// (get) Token: 0x060191A3 RID: 102819 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700896F RID: 35183
		// (get) Token: 0x060191A4 RID: 102820 RVA: 0x00346673 File Offset: 0x00344873
		internal override int ElementTypeId
		{
			get
			{
				return 11520;
			}
		}

		// Token: 0x060191A5 RID: 102821 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008970 RID: 35184
		// (get) Token: 0x060191A6 RID: 102822 RVA: 0x0034667A File Offset: 0x0034487A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextAlignment.attributeTagNames;
			}
		}

		// Token: 0x17008971 RID: 35185
		// (get) Token: 0x060191A7 RID: 102823 RVA: 0x00346681 File Offset: 0x00344881
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextAlignment.attributeNamespaceIds;
			}
		}

		// Token: 0x17008972 RID: 35186
		// (get) Token: 0x060191A8 RID: 102824 RVA: 0x00346688 File Offset: 0x00344888
		// (set) Token: 0x060191A9 RID: 102825 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<VerticalTextAlignmentValues> Val
		{
			get
			{
				return (EnumValue<VerticalTextAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060191AB RID: 102827 RVA: 0x00346697 File Offset: 0x00344897
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<VerticalTextAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060191AC RID: 102828 RVA: 0x003466B9 File Offset: 0x003448B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextAlignment>(deep);
		}

		// Token: 0x0400A702 RID: 42754
		private const string tagName = "textAlignment";

		// Token: 0x0400A703 RID: 42755
		private const byte tagNsId = 23;

		// Token: 0x0400A704 RID: 42756
		internal const int ElementTypeIdConst = 11520;

		// Token: 0x0400A705 RID: 42757
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A706 RID: 42758
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
