using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240E RID: 9230
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlicerCache : OpenXmlLeafElement
	{
		// Token: 0x17004EDA RID: 20186
		// (get) Token: 0x06010E43 RID: 69187 RVA: 0x002AECFA File Offset: 0x002ACEFA
		public override string LocalName
		{
			get
			{
				return "slicerCache";
			}
		}

		// Token: 0x17004EDB RID: 20187
		// (get) Token: 0x06010E44 RID: 69188 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EDC RID: 20188
		// (get) Token: 0x06010E45 RID: 69189 RVA: 0x002E863C File Offset: 0x002E683C
		internal override int ElementTypeId
		{
			get
			{
				return 12948;
			}
		}

		// Token: 0x06010E46 RID: 69190 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004EDD RID: 20189
		// (get) Token: 0x06010E47 RID: 69191 RVA: 0x002E8643 File Offset: 0x002E6843
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerCache.attributeTagNames;
			}
		}

		// Token: 0x17004EDE RID: 20190
		// (get) Token: 0x06010E48 RID: 69192 RVA: 0x002E864A File Offset: 0x002E684A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerCache.attributeNamespaceIds;
			}
		}

		// Token: 0x17004EDF RID: 20191
		// (get) Token: 0x06010E49 RID: 69193 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010E4A RID: 69194 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010E4C RID: 69196 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E4D RID: 69197 RVA: 0x002E8651 File Offset: 0x002E6851
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCache>(deep);
		}

		// Token: 0x040076BD RID: 30397
		private const string tagName = "slicerCache";

		// Token: 0x040076BE RID: 30398
		private const byte tagNsId = 53;

		// Token: 0x040076BF RID: 30399
		internal const int ElementTypeIdConst = 12948;

		// Token: 0x040076C0 RID: 30400
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040076C1 RID: 30401
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
