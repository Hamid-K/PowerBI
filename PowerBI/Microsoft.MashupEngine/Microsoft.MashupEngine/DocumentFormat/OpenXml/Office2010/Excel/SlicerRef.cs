using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240D RID: 9229
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerRef : OpenXmlLeafElement
	{
		// Token: 0x17004ED4 RID: 20180
		// (get) Token: 0x06010E37 RID: 69175 RVA: 0x002AED9A File Offset: 0x002ACF9A
		public override string LocalName
		{
			get
			{
				return "slicer";
			}
		}

		// Token: 0x17004ED5 RID: 20181
		// (get) Token: 0x06010E38 RID: 69176 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004ED6 RID: 20182
		// (get) Token: 0x06010E39 RID: 69177 RVA: 0x002E85E9 File Offset: 0x002E67E9
		internal override int ElementTypeId
		{
			get
			{
				return 12947;
			}
		}

		// Token: 0x06010E3A RID: 69178 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004ED7 RID: 20183
		// (get) Token: 0x06010E3B RID: 69179 RVA: 0x002E85F0 File Offset: 0x002E67F0
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerRef.attributeTagNames;
			}
		}

		// Token: 0x17004ED8 RID: 20184
		// (get) Token: 0x06010E3C RID: 69180 RVA: 0x002E85F7 File Offset: 0x002E67F7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerRef.attributeNamespaceIds;
			}
		}

		// Token: 0x17004ED9 RID: 20185
		// (get) Token: 0x06010E3D RID: 69181 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010E3E RID: 69182 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010E40 RID: 69184 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E41 RID: 69185 RVA: 0x002E85FE File Offset: 0x002E67FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerRef>(deep);
		}

		// Token: 0x040076B8 RID: 30392
		private const string tagName = "slicer";

		// Token: 0x040076B9 RID: 30393
		private const byte tagNsId = 53;

		// Token: 0x040076BA RID: 30394
		internal const int ElementTypeIdConst = 12947;

		// Token: 0x040076BB RID: 30395
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040076BC RID: 30396
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
