using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002215 RID: 8725
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Entry))]
	internal class RegroupTable : OpenXmlCompositeElement
	{
		// Token: 0x17003907 RID: 14599
		// (get) Token: 0x0600DFA7 RID: 57255 RVA: 0x002BF59C File Offset: 0x002BD79C
		public override string LocalName
		{
			get
			{
				return "regrouptable";
			}
		}

		// Token: 0x17003908 RID: 14600
		// (get) Token: 0x0600DFA8 RID: 57256 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003909 RID: 14601
		// (get) Token: 0x0600DFA9 RID: 57257 RVA: 0x002BF5A3 File Offset: 0x002BD7A3
		internal override int ElementTypeId
		{
			get
			{
				return 12418;
			}
		}

		// Token: 0x0600DFAA RID: 57258 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700390A RID: 14602
		// (get) Token: 0x0600DFAB RID: 57259 RVA: 0x002BF5AA File Offset: 0x002BD7AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return RegroupTable.attributeTagNames;
			}
		}

		// Token: 0x1700390B RID: 14603
		// (get) Token: 0x0600DFAC RID: 57260 RVA: 0x002BF5B1 File Offset: 0x002BD7B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RegroupTable.attributeNamespaceIds;
			}
		}

		// Token: 0x1700390C RID: 14604
		// (get) Token: 0x0600DFAD RID: 57261 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DFAE RID: 57262 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0600DFAF RID: 57263 RVA: 0x00293ECF File Offset: 0x002920CF
		public RegroupTable()
		{
		}

		// Token: 0x0600DFB0 RID: 57264 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RegroupTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFB1 RID: 57265 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RegroupTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFB2 RID: 57266 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RegroupTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DFB3 RID: 57267 RVA: 0x002BF5B8 File Offset: 0x002BD7B8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "entry" == name)
			{
				return new Entry();
			}
			return null;
		}

		// Token: 0x0600DFB4 RID: 57268 RVA: 0x002BDA15 File Offset: 0x002BBC15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFB5 RID: 57269 RVA: 0x002BF5D3 File Offset: 0x002BD7D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RegroupTable>(deep);
		}

		// Token: 0x04006DAD RID: 28077
		private const string tagName = "regrouptable";

		// Token: 0x04006DAE RID: 28078
		private const byte tagNsId = 27;

		// Token: 0x04006DAF RID: 28079
		internal const int ElementTypeIdConst = 12418;

		// Token: 0x04006DB0 RID: 28080
		private static string[] attributeTagNames = new string[] { "ext" };

		// Token: 0x04006DB1 RID: 28081
		private static byte[] attributeNamespaceIds = new byte[] { 26 };
	}
}
