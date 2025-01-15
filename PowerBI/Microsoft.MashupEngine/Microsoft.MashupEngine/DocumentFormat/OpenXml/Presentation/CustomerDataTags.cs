using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4B RID: 10827
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomerDataTags : OpenXmlLeafElement
	{
		// Token: 0x1700719D RID: 29085
		// (get) Token: 0x06015CE6 RID: 89318 RVA: 0x002AC583 File Offset: 0x002AA783
		public override string LocalName
		{
			get
			{
				return "tags";
			}
		}

		// Token: 0x1700719E RID: 29086
		// (get) Token: 0x06015CE7 RID: 89319 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700719F RID: 29087
		// (get) Token: 0x06015CE8 RID: 89320 RVA: 0x003234E4 File Offset: 0x003216E4
		internal override int ElementTypeId
		{
			get
			{
				return 12246;
			}
		}

		// Token: 0x06015CE9 RID: 89321 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071A0 RID: 29088
		// (get) Token: 0x06015CEA RID: 89322 RVA: 0x003234EB File Offset: 0x003216EB
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomerDataTags.attributeTagNames;
			}
		}

		// Token: 0x170071A1 RID: 29089
		// (get) Token: 0x06015CEB RID: 89323 RVA: 0x003234F2 File Offset: 0x003216F2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomerDataTags.attributeNamespaceIds;
			}
		}

		// Token: 0x170071A2 RID: 29090
		// (get) Token: 0x06015CEC RID: 89324 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015CED RID: 89325 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015CEF RID: 89327 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015CF0 RID: 89328 RVA: 0x003234F9 File Offset: 0x003216F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomerDataTags>(deep);
		}

		// Token: 0x040094E6 RID: 38118
		private const string tagName = "tags";

		// Token: 0x040094E7 RID: 38119
		private const byte tagNsId = 24;

		// Token: 0x040094E8 RID: 38120
		internal const int ElementTypeIdConst = 12246;

		// Token: 0x040094E9 RID: 38121
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040094EA RID: 38122
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
