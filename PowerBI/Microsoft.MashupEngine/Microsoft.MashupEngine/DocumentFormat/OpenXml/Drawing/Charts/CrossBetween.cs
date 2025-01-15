using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BC RID: 9660
	[GeneratedCode("DomGen", "2.0")]
	internal class CrossBetween : OpenXmlLeafElement
	{
		// Token: 0x1700576D RID: 22381
		// (get) Token: 0x06012188 RID: 74120 RVA: 0x002F579D File Offset: 0x002F399D
		public override string LocalName
		{
			get
			{
				return "crossBetween";
			}
		}

		// Token: 0x1700576E RID: 22382
		// (get) Token: 0x06012189 RID: 74121 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700576F RID: 22383
		// (get) Token: 0x0601218A RID: 74122 RVA: 0x002F57A4 File Offset: 0x002F39A4
		internal override int ElementTypeId
		{
			get
			{
				return 10487;
			}
		}

		// Token: 0x0601218B RID: 74123 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005770 RID: 22384
		// (get) Token: 0x0601218C RID: 74124 RVA: 0x002F57AB File Offset: 0x002F39AB
		internal override string[] AttributeTagNames
		{
			get
			{
				return CrossBetween.attributeTagNames;
			}
		}

		// Token: 0x17005771 RID: 22385
		// (get) Token: 0x0601218D RID: 74125 RVA: 0x002F57B2 File Offset: 0x002F39B2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CrossBetween.attributeNamespaceIds;
			}
		}

		// Token: 0x17005772 RID: 22386
		// (get) Token: 0x0601218E RID: 74126 RVA: 0x002F57B9 File Offset: 0x002F39B9
		// (set) Token: 0x0601218F RID: 74127 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<CrossBetweenValues> Val
		{
			get
			{
				return (EnumValue<CrossBetweenValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012191 RID: 74129 RVA: 0x002F57C8 File Offset: 0x002F39C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<CrossBetweenValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012192 RID: 74130 RVA: 0x002F57E8 File Offset: 0x002F39E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CrossBetween>(deep);
		}

		// Token: 0x06012193 RID: 74131 RVA: 0x002F57F4 File Offset: 0x002F39F4
		// Note: this type is marked as 'beforefieldinit'.
		static CrossBetween()
		{
			byte[] array = new byte[1];
			CrossBetween.attributeNamespaceIds = array;
		}

		// Token: 0x04007E32 RID: 32306
		private const string tagName = "crossBetween";

		// Token: 0x04007E33 RID: 32307
		private const byte tagNsId = 11;

		// Token: 0x04007E34 RID: 32308
		internal const int ElementTypeIdConst = 10487;

		// Token: 0x04007E35 RID: 32309
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E36 RID: 32310
		private static byte[] attributeNamespaceIds;
	}
}
