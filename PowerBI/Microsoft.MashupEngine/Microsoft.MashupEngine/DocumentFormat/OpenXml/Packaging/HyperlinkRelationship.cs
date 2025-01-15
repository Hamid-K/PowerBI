using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200214B RID: 8523
	internal class HyperlinkRelationship : ReferenceRelationship
	{
		// Token: 0x0600D3D8 RID: 54232 RVA: 0x002A1608 File Offset: 0x0029F808
		protected internal HyperlinkRelationship(Uri hyperlinkUri, bool isExternal, string id)
			: base(hyperlinkUri, isExternal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink", id)
		{
		}

		// Token: 0x17003320 RID: 13088
		// (get) Token: 0x0600D3D9 RID: 54233 RVA: 0x002A1618 File Offset: 0x0029F818
		public override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";
			}
		}

		// Token: 0x040069AA RID: 27050
		internal const string RelationshipTypeConst = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink";
	}
}
