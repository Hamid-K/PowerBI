using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002950 RID: 10576
	[GeneratedCode("DomGen", "2.0")]
	internal class Style : OpenXmlLeafElement
	{
		// Token: 0x17006B77 RID: 27511
		// (get) Token: 0x06014F4B RID: 85835 RVA: 0x00319068 File Offset: 0x00317268
		public override string LocalName
		{
			get
			{
				return "sty";
			}
		}

		// Token: 0x17006B78 RID: 27512
		// (get) Token: 0x06014F4C RID: 85836 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B79 RID: 27513
		// (get) Token: 0x06014F4D RID: 85837 RVA: 0x0031906F File Offset: 0x0031726F
		internal override int ElementTypeId
		{
			get
			{
				return 10840;
			}
		}

		// Token: 0x06014F4E RID: 85838 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006B7A RID: 27514
		// (get) Token: 0x06014F4F RID: 85839 RVA: 0x00319076 File Offset: 0x00317276
		internal override string[] AttributeTagNames
		{
			get
			{
				return Style.attributeTagNames;
			}
		}

		// Token: 0x17006B7B RID: 27515
		// (get) Token: 0x06014F50 RID: 85840 RVA: 0x0031907D File Offset: 0x0031727D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Style.attributeNamespaceIds;
			}
		}

		// Token: 0x17006B7C RID: 27516
		// (get) Token: 0x06014F51 RID: 85841 RVA: 0x00319084 File Offset: 0x00317284
		// (set) Token: 0x06014F52 RID: 85842 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<StyleValues> Val
		{
			get
			{
				return (EnumValue<StyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014F54 RID: 85844 RVA: 0x00319093 File Offset: 0x00317293
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<StyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014F55 RID: 85845 RVA: 0x003190B5 File Offset: 0x003172B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Style>(deep);
		}

		// Token: 0x040090CF RID: 37071
		private const string tagName = "sty";

		// Token: 0x040090D0 RID: 37072
		private const byte tagNsId = 21;

		// Token: 0x040090D1 RID: 37073
		internal const int ElementTypeIdConst = 10840;

		// Token: 0x040090D2 RID: 37074
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040090D3 RID: 37075
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
