using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.ActiveX
{
	// Token: 0x020022A1 RID: 8865
	[GeneratedCode("DomGen", "2.0")]
	internal class SharedComPicture : OpenXmlLeafElement
	{
		// Token: 0x17004107 RID: 16647
		// (get) Token: 0x0600F06D RID: 61549 RVA: 0x002D0AB9 File Offset: 0x002CECB9
		public override string LocalName
		{
			get
			{
				return "picture";
			}
		}

		// Token: 0x17004108 RID: 16648
		// (get) Token: 0x0600F06E RID: 61550 RVA: 0x002D07C1 File Offset: 0x002CE9C1
		internal override byte NamespaceId
		{
			get
			{
				return 35;
			}
		}

		// Token: 0x17004109 RID: 16649
		// (get) Token: 0x0600F06F RID: 61551 RVA: 0x002D0AC0 File Offset: 0x002CECC0
		internal override int ElementTypeId
		{
			get
			{
				return 12620;
			}
		}

		// Token: 0x0600F070 RID: 61552 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700410A RID: 16650
		// (get) Token: 0x0600F071 RID: 61553 RVA: 0x002D0AC7 File Offset: 0x002CECC7
		internal override string[] AttributeTagNames
		{
			get
			{
				return SharedComPicture.attributeTagNames;
			}
		}

		// Token: 0x1700410B RID: 16651
		// (get) Token: 0x0600F072 RID: 61554 RVA: 0x002D0ACE File Offset: 0x002CECCE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SharedComPicture.attributeNamespaceIds;
			}
		}

		// Token: 0x1700410C RID: 16652
		// (get) Token: 0x0600F073 RID: 61555 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F074 RID: 61556 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600F076 RID: 61558 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F077 RID: 61559 RVA: 0x002D0AF7 File Offset: 0x002CECF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedComPicture>(deep);
		}

		// Token: 0x04007073 RID: 28787
		private const string tagName = "picture";

		// Token: 0x04007074 RID: 28788
		private const byte tagNsId = 35;

		// Token: 0x04007075 RID: 28789
		internal const int ElementTypeIdConst = 12620;

		// Token: 0x04007076 RID: 28790
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04007077 RID: 28791
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
