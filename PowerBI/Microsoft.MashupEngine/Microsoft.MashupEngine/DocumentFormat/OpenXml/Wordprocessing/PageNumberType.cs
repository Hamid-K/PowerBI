using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E48 RID: 11848
	[GeneratedCode("DomGen", "2.0")]
	internal class PageNumberType : OpenXmlLeafElement
	{
		// Token: 0x17008A04 RID: 35332
		// (get) Token: 0x060192D8 RID: 103128 RVA: 0x0034736C File Offset: 0x0034556C
		public override string LocalName
		{
			get
			{
				return "pgNumType";
			}
		}

		// Token: 0x17008A05 RID: 35333
		// (get) Token: 0x060192D9 RID: 103129 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A06 RID: 35334
		// (get) Token: 0x060192DA RID: 103130 RVA: 0x00347373 File Offset: 0x00345573
		internal override int ElementTypeId
		{
			get
			{
				return 11534;
			}
		}

		// Token: 0x060192DB RID: 103131 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008A07 RID: 35335
		// (get) Token: 0x060192DC RID: 103132 RVA: 0x0034737A File Offset: 0x0034557A
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageNumberType.attributeTagNames;
			}
		}

		// Token: 0x17008A08 RID: 35336
		// (get) Token: 0x060192DD RID: 103133 RVA: 0x00347381 File Offset: 0x00345581
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A09 RID: 35337
		// (get) Token: 0x060192DE RID: 103134 RVA: 0x00347388 File Offset: 0x00345588
		// (set) Token: 0x060192DF RID: 103135 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "fmt")]
		public EnumValue<NumberFormatValues> Format
		{
			get
			{
				return (EnumValue<NumberFormatValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008A0A RID: 35338
		// (get) Token: 0x060192E0 RID: 103136 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060192E1 RID: 103137 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "start")]
		public Int32Value Start
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008A0B RID: 35339
		// (get) Token: 0x060192E2 RID: 103138 RVA: 0x00347397 File Offset: 0x00345597
		// (set) Token: 0x060192E3 RID: 103139 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "chapStyle")]
		public ByteValue ChapterStyle
		{
			get
			{
				return (ByteValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008A0C RID: 35340
		// (get) Token: 0x060192E4 RID: 103140 RVA: 0x003473A6 File Offset: 0x003455A6
		// (set) Token: 0x060192E5 RID: 103141 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "chapSep")]
		public EnumValue<ChapterSeparatorValues> ChapterSeparator
		{
			get
			{
				return (EnumValue<ChapterSeparatorValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060192E7 RID: 103143 RVA: 0x003473B8 File Offset: 0x003455B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fmt" == name)
			{
				return new EnumValue<NumberFormatValues>();
			}
			if (23 == namespaceId && "start" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "chapStyle" == name)
			{
				return new ByteValue();
			}
			if (23 == namespaceId && "chapSep" == name)
			{
				return new EnumValue<ChapterSeparatorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060192E8 RID: 103144 RVA: 0x0034742D File Offset: 0x0034562D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageNumberType>(deep);
		}

		// Token: 0x0400A76B RID: 42859
		private const string tagName = "pgNumType";

		// Token: 0x0400A76C RID: 42860
		private const byte tagNsId = 23;

		// Token: 0x0400A76D RID: 42861
		internal const int ElementTypeIdConst = 11534;

		// Token: 0x0400A76E RID: 42862
		private static string[] attributeTagNames = new string[] { "fmt", "start", "chapStyle", "chapSep" };

		// Token: 0x0400A76F RID: 42863
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
