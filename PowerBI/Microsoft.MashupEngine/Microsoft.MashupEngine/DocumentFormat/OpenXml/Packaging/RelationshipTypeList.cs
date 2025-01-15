using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200214C RID: 8524
	internal static class RelationshipTypeList
	{
		// Token: 0x17003321 RID: 13089
		// (get) Token: 0x0600D3DA RID: 54234 RVA: 0x002A1620 File Offset: 0x0029F820
		public static Dictionary<string, int> IsoKnownRelationships
		{
			get
			{
				if (RelationshipTypeList._list == null)
				{
					RelationshipTypeList._list = new Dictionary<string, int>();
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/glossaryDocument", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/header", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/table", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/handoutMaster", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesMaster", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/notesSlide", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideLayout", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideMaster", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/control", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/package", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/font", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/audio", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/video", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/image", 1);
					RelationshipTypeList._list.Add("http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing", 1);
				}
				return RelationshipTypeList._list;
			}
		}

		// Token: 0x040069AB RID: 27051
		private static Dictionary<string, int> _list;
	}
}
