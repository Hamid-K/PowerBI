using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9B RID: 11931
	[GeneratedCode("DomGen", "2.0")]
	internal class TextEffect : OpenXmlLeafElement
	{
		// Token: 0x17008B61 RID: 35681
		// (get) Token: 0x060195B3 RID: 103859 RVA: 0x00303BC8 File Offset: 0x00301DC8
		public override string LocalName
		{
			get
			{
				return "effect";
			}
		}

		// Token: 0x17008B62 RID: 35682
		// (get) Token: 0x060195B4 RID: 103860 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B63 RID: 35683
		// (get) Token: 0x060195B5 RID: 103861 RVA: 0x00348DE8 File Offset: 0x00346FE8
		internal override int ElementTypeId
		{
			get
			{
				return 11601;
			}
		}

		// Token: 0x060195B6 RID: 103862 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B64 RID: 35684
		// (get) Token: 0x060195B7 RID: 103863 RVA: 0x00348DEF File Offset: 0x00346FEF
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextEffect.attributeTagNames;
			}
		}

		// Token: 0x17008B65 RID: 35685
		// (get) Token: 0x060195B8 RID: 103864 RVA: 0x00348DF6 File Offset: 0x00346FF6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B66 RID: 35686
		// (get) Token: 0x060195B9 RID: 103865 RVA: 0x00348DFD File Offset: 0x00346FFD
		// (set) Token: 0x060195BA RID: 103866 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TextEffectValues> Val
		{
			get
			{
				return (EnumValue<TextEffectValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060195BC RID: 103868 RVA: 0x00348E0C File Offset: 0x0034700C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TextEffectValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060195BD RID: 103869 RVA: 0x00348E2E File Offset: 0x0034702E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextEffect>(deep);
		}

		// Token: 0x0400A886 RID: 43142
		private const string tagName = "effect";

		// Token: 0x0400A887 RID: 43143
		private const byte tagNsId = 23;

		// Token: 0x0400A888 RID: 43144
		internal const int ElementTypeIdConst = 11601;

		// Token: 0x0400A889 RID: 43145
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A88A RID: 43146
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
