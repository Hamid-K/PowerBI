using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.LegacyCompatibility
{
	// Token: 0x02002340 RID: 9024
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CompatibilityShape : OpenXmlLeafElement
	{
		// Token: 0x1700494D RID: 18765
		// (get) Token: 0x06010224 RID: 66084 RVA: 0x002E013B File Offset: 0x002DE33B
		public override string LocalName
		{
			get
			{
				return "compatSp";
			}
		}

		// Token: 0x1700494E RID: 18766
		// (get) Token: 0x06010225 RID: 66085 RVA: 0x002E0142 File Offset: 0x002DE342
		internal override byte NamespaceId
		{
			get
			{
				return 63;
			}
		}

		// Token: 0x1700494F RID: 18767
		// (get) Token: 0x06010226 RID: 66086 RVA: 0x002E0146 File Offset: 0x002DE346
		internal override int ElementTypeId
		{
			get
			{
				return 13143;
			}
		}

		// Token: 0x06010227 RID: 66087 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004950 RID: 18768
		// (get) Token: 0x06010228 RID: 66088 RVA: 0x002E014D File Offset: 0x002DE34D
		internal override string[] AttributeTagNames
		{
			get
			{
				return CompatibilityShape.attributeTagNames;
			}
		}

		// Token: 0x17004951 RID: 18769
		// (get) Token: 0x06010229 RID: 66089 RVA: 0x002E0154 File Offset: 0x002DE354
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CompatibilityShape.attributeNamespaceIds;
			}
		}

		// Token: 0x17004952 RID: 18770
		// (get) Token: 0x0601022A RID: 66090 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601022B RID: 66091 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
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

		// Token: 0x0601022D RID: 66093 RVA: 0x002E015B File Offset: 0x002DE35B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601022E RID: 66094 RVA: 0x002E017B File Offset: 0x002DE37B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompatibilityShape>(deep);
		}

		// Token: 0x0601022F RID: 66095 RVA: 0x002E0184 File Offset: 0x002DE384
		// Note: this type is marked as 'beforefieldinit'.
		static CompatibilityShape()
		{
			byte[] array = new byte[1];
			CompatibilityShape.attributeNamespaceIds = array;
		}

		// Token: 0x04007337 RID: 29495
		private const string tagName = "compatSp";

		// Token: 0x04007338 RID: 29496
		private const byte tagNsId = 63;

		// Token: 0x04007339 RID: 29497
		internal const int ElementTypeIdConst = 13143;

		// Token: 0x0400733A RID: 29498
		private static string[] attributeTagNames = new string[] { "spid" };

		// Token: 0x0400733B RID: 29499
		private static byte[] attributeNamespaceIds;
	}
}
