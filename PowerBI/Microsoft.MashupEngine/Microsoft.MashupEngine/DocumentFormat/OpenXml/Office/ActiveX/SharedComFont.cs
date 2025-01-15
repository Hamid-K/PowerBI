using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.ActiveX
{
	// Token: 0x020022A0 RID: 8864
	[ChildElementInfo(typeof(ActiveXObjectProperty))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SharedComFont : OpenXmlCompositeElement
	{
		// Token: 0x17004100 RID: 16640
		// (get) Token: 0x0600F05B RID: 61531 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x17004101 RID: 16641
		// (get) Token: 0x0600F05C RID: 61532 RVA: 0x002D07C1 File Offset: 0x002CE9C1
		internal override byte NamespaceId
		{
			get
			{
				return 35;
			}
		}

		// Token: 0x17004102 RID: 16642
		// (get) Token: 0x0600F05D RID: 61533 RVA: 0x002D0A11 File Offset: 0x002CEC11
		internal override int ElementTypeId
		{
			get
			{
				return 12619;
			}
		}

		// Token: 0x0600F05E RID: 61534 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004103 RID: 16643
		// (get) Token: 0x0600F05F RID: 61535 RVA: 0x002D0A18 File Offset: 0x002CEC18
		internal override string[] AttributeTagNames
		{
			get
			{
				return SharedComFont.attributeTagNames;
			}
		}

		// Token: 0x17004104 RID: 16644
		// (get) Token: 0x0600F060 RID: 61536 RVA: 0x002D0A1F File Offset: 0x002CEC1F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SharedComFont.attributeNamespaceIds;
			}
		}

		// Token: 0x17004105 RID: 16645
		// (get) Token: 0x0600F061 RID: 61537 RVA: 0x002D0A26 File Offset: 0x002CEC26
		// (set) Token: 0x0600F062 RID: 61538 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(35, "persistence")]
		public EnumValue<PersistenceValues> Persistence
		{
			get
			{
				return (EnumValue<PersistenceValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004106 RID: 16646
		// (get) Token: 0x0600F063 RID: 61539 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F064 RID: 61540 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0600F065 RID: 61541 RVA: 0x00293ECF File Offset: 0x002920CF
		public SharedComFont()
		{
		}

		// Token: 0x0600F066 RID: 61542 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SharedComFont(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F067 RID: 61543 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SharedComFont(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F068 RID: 61544 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SharedComFont(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F069 RID: 61545 RVA: 0x002D07E9 File Offset: 0x002CE9E9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "ocxPr" == name)
			{
				return new ActiveXObjectProperty();
			}
			return null;
		}

		// Token: 0x0600F06A RID: 61546 RVA: 0x002D0A35 File Offset: 0x002CEC35
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "persistence" == name)
			{
				return new EnumValue<PersistenceValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F06B RID: 61547 RVA: 0x002D0A6F File Offset: 0x002CEC6F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedComFont>(deep);
		}

		// Token: 0x0400706E RID: 28782
		private const string tagName = "font";

		// Token: 0x0400706F RID: 28783
		private const byte tagNsId = 35;

		// Token: 0x04007070 RID: 28784
		internal const int ElementTypeIdConst = 12619;

		// Token: 0x04007071 RID: 28785
		private static string[] attributeTagNames = new string[] { "persistence", "id" };

		// Token: 0x04007072 RID: 28786
		private static byte[] attributeNamespaceIds = new byte[] { 35, 19 };
	}
}
