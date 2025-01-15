using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A49 RID: 10825
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideListEntry : OpenXmlLeafElement
	{
		// Token: 0x17007191 RID: 29073
		// (get) Token: 0x06015CCE RID: 89294 RVA: 0x0031F324 File Offset: 0x0031D524
		public override string LocalName
		{
			get
			{
				return "sld";
			}
		}

		// Token: 0x17007192 RID: 29074
		// (get) Token: 0x06015CCF RID: 89295 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007193 RID: 29075
		// (get) Token: 0x06015CD0 RID: 89296 RVA: 0x00323433 File Offset: 0x00321633
		internal override int ElementTypeId
		{
			get
			{
				return 12244;
			}
		}

		// Token: 0x06015CD1 RID: 89297 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007194 RID: 29076
		// (get) Token: 0x06015CD2 RID: 89298 RVA: 0x0032343A File Offset: 0x0032163A
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideListEntry.attributeTagNames;
			}
		}

		// Token: 0x17007195 RID: 29077
		// (get) Token: 0x06015CD3 RID: 89299 RVA: 0x00323441 File Offset: 0x00321641
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideListEntry.attributeNamespaceIds;
			}
		}

		// Token: 0x17007196 RID: 29078
		// (get) Token: 0x06015CD4 RID: 89300 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015CD5 RID: 89301 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015CD7 RID: 89303 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015CD8 RID: 89304 RVA: 0x00323448 File Offset: 0x00321648
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideListEntry>(deep);
		}

		// Token: 0x040094DC RID: 38108
		private const string tagName = "sld";

		// Token: 0x040094DD RID: 38109
		private const byte tagNsId = 24;

		// Token: 0x040094DE RID: 38110
		internal const int ElementTypeIdConst = 12244;

		// Token: 0x040094DF RID: 38111
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040094E0 RID: 38112
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
