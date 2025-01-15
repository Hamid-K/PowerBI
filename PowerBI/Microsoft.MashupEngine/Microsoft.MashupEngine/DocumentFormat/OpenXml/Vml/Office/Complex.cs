using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200220B RID: 8715
	[GeneratedCode("DomGen", "2.0")]
	internal class Complex : OpenXmlLeafElement
	{
		// Token: 0x170038C1 RID: 14529
		// (get) Token: 0x0600DF1A RID: 57114 RVA: 0x002BEE6B File Offset: 0x002BD06B
		public override string LocalName
		{
			get
			{
				return "complex";
			}
		}

		// Token: 0x170038C2 RID: 14530
		// (get) Token: 0x0600DF1B RID: 57115 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038C3 RID: 14531
		// (get) Token: 0x0600DF1C RID: 57116 RVA: 0x002BEE72 File Offset: 0x002BD072
		internal override int ElementTypeId
		{
			get
			{
				return 12409;
			}
		}

		// Token: 0x0600DF1D RID: 57117 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170038C4 RID: 14532
		// (get) Token: 0x0600DF1E RID: 57118 RVA: 0x002BEE79 File Offset: 0x002BD079
		internal override string[] AttributeTagNames
		{
			get
			{
				return Complex.attributeTagNames;
			}
		}

		// Token: 0x170038C5 RID: 14533
		// (get) Token: 0x0600DF1F RID: 57119 RVA: 0x002BEE80 File Offset: 0x002BD080
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Complex.attributeNamespaceIds;
			}
		}

		// Token: 0x170038C6 RID: 14534
		// (get) Token: 0x0600DF20 RID: 57120 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DF21 RID: 57121 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0600DF23 RID: 57123 RVA: 0x002BDA15 File Offset: 0x002BBC15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DF24 RID: 57124 RVA: 0x002BEE87 File Offset: 0x002BD087
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Complex>(deep);
		}

		// Token: 0x04006D88 RID: 28040
		private const string tagName = "complex";

		// Token: 0x04006D89 RID: 28041
		private const byte tagNsId = 27;

		// Token: 0x04006D8A RID: 28042
		internal const int ElementTypeIdConst = 12409;

		// Token: 0x04006D8B RID: 28043
		private static string[] attributeTagNames = new string[] { "ext" };

		// Token: 0x04006D8C RID: 28044
		private static byte[] attributeNamespaceIds = new byte[] { 26 };
	}
}
