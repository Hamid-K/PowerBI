using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4A RID: 10826
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomerData : OpenXmlLeafElement
	{
		// Token: 0x17007197 RID: 29079
		// (get) Token: 0x06015CDA RID: 89306 RVA: 0x00323488 File Offset: 0x00321688
		public override string LocalName
		{
			get
			{
				return "custData";
			}
		}

		// Token: 0x17007198 RID: 29080
		// (get) Token: 0x06015CDB RID: 89307 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007199 RID: 29081
		// (get) Token: 0x06015CDC RID: 89308 RVA: 0x0032348F File Offset: 0x0032168F
		internal override int ElementTypeId
		{
			get
			{
				return 12245;
			}
		}

		// Token: 0x06015CDD RID: 89309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700719A RID: 29082
		// (get) Token: 0x06015CDE RID: 89310 RVA: 0x00323496 File Offset: 0x00321696
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomerData.attributeTagNames;
			}
		}

		// Token: 0x1700719B RID: 29083
		// (get) Token: 0x06015CDF RID: 89311 RVA: 0x0032349D File Offset: 0x0032169D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomerData.attributeNamespaceIds;
			}
		}

		// Token: 0x1700719C RID: 29084
		// (get) Token: 0x06015CE0 RID: 89312 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015CE1 RID: 89313 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015CE3 RID: 89315 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015CE4 RID: 89316 RVA: 0x003234A4 File Offset: 0x003216A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomerData>(deep);
		}

		// Token: 0x040094E1 RID: 38113
		private const string tagName = "custData";

		// Token: 0x040094E2 RID: 38114
		private const byte tagNsId = 24;

		// Token: 0x040094E3 RID: 38115
		internal const int ElementTypeIdConst = 12245;

		// Token: 0x040094E4 RID: 38116
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040094E5 RID: 38117
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
