using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002214 RID: 8724
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeIdMap : OpenXmlLeafElement
	{
		// Token: 0x17003900 RID: 14592
		// (get) Token: 0x0600DF99 RID: 57241 RVA: 0x002BF500 File Offset: 0x002BD700
		public override string LocalName
		{
			get
			{
				return "idmap";
			}
		}

		// Token: 0x17003901 RID: 14593
		// (get) Token: 0x0600DF9A RID: 57242 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003902 RID: 14594
		// (get) Token: 0x0600DF9B RID: 57243 RVA: 0x002BF507 File Offset: 0x002BD707
		internal override int ElementTypeId
		{
			get
			{
				return 12417;
			}
		}

		// Token: 0x0600DF9C RID: 57244 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003903 RID: 14595
		// (get) Token: 0x0600DF9D RID: 57245 RVA: 0x002BF50E File Offset: 0x002BD70E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeIdMap.attributeTagNames;
			}
		}

		// Token: 0x17003904 RID: 14596
		// (get) Token: 0x0600DF9E RID: 57246 RVA: 0x002BF515 File Offset: 0x002BD715
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeIdMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17003905 RID: 14597
		// (get) Token: 0x0600DF9F RID: 57247 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DFA0 RID: 57248 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003906 RID: 14598
		// (get) Token: 0x0600DFA1 RID: 57249 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600DFA2 RID: 57250 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "data")]
		public StringValue Data
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0600DFA4 RID: 57252 RVA: 0x002BF51C File Offset: 0x002BD71C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "data" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFA5 RID: 57253 RVA: 0x002BF554 File Offset: 0x002BD754
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeIdMap>(deep);
		}

		// Token: 0x0600DFA6 RID: 57254 RVA: 0x002BF560 File Offset: 0x002BD760
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeIdMap()
		{
			byte[] array = new byte[2];
			array[0] = 26;
			ShapeIdMap.attributeNamespaceIds = array;
		}

		// Token: 0x04006DA8 RID: 28072
		private const string tagName = "idmap";

		// Token: 0x04006DA9 RID: 28073
		private const byte tagNsId = 27;

		// Token: 0x04006DAA RID: 28074
		internal const int ElementTypeIdConst = 12417;

		// Token: 0x04006DAB RID: 28075
		private static string[] attributeTagNames = new string[] { "ext", "data" };

		// Token: 0x04006DAC RID: 28076
		private static byte[] attributeNamespaceIds;
	}
}
