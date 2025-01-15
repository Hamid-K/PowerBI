using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002180 RID: 8576
	internal class WorksheetPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D76A RID: 55146 RVA: 0x002A8A00 File Offset: 0x002A6C00
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorksheetPart._partConstraint == null)
			{
				WorksheetPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings",
						new PartConstraintRule("SpreadsheetPrinterSettingsPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.printerSettings", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing",
						new PartConstraintRule("DrawingsPart", "application/vnd.openxmlformats-officedocument.drawing+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing",
						new PartConstraintRule("VmlDrawingPart", "application/vnd.openxmlformats-officedocument.vmlDrawing", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments",
						new PartConstraintRule("WorksheetCommentsPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.comments+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable",
						new PartConstraintRule("PivotTablePart", "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotTable+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells",
						new PartConstraintRule("SingleCellTablePart", "application/vnd.openxmlformats-officedocument.spreadsheetml.tableSingleCells+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/table",
						new PartConstraintRule("TableDefinitionPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/control",
						new PartConstraintRule("EmbeddedControlPersistencePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp",
						new PartConstraintRule("ControlPropertiesPart", "application/vnd.ms-excel.controlproperties+xml", false, true, FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject",
						new PartConstraintRule("EmbeddedObjectPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/package",
						new PartConstraintRule("EmbeddedPackagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty",
						new PartConstraintRule("CustomPropertyPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/wsSortMap",
						new PartConstraintRule("WorksheetSortMapPart", "application/vnd.ms-excel.wsSortMap+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable",
						new PartConstraintRule("QueryTablePart", "application/vnd.openxmlformats-officedocument.spreadsheetml.queryTable+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary",
						new PartConstraintRule("EmbeddedControlPersistenceBinaryDataPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/slicer",
						new PartConstraintRule("SlicersPart", "application/vnd.ms-excel.slicer+xml", false, true, FileFormatVersions.Office2010)
					}
				};
			}
			return WorksheetPart._partConstraint;
		}

		// Token: 0x0600D76B RID: 55147 RVA: 0x002A8C00 File Offset: 0x002A6E00
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorksheetPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorksheetPart._dataPartReferenceConstraint = dictionary;
			}
			return WorksheetPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D76C RID: 55148 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorksheetPart()
		{
		}

		// Token: 0x0600D76D RID: 55149 RVA: 0x002A8C28 File Offset: 0x002A6E28
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings":
				return new SpreadsheetPrinterSettingsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing":
				return new DrawingsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing":
				return new VmlDrawingPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments":
				return new WorksheetCommentsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable":
				return new PivotTablePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells":
				return new SingleCellTablePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table":
				return new TableDefinitionPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control":
				return new EmbeddedControlPersistencePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp":
				return new ControlPropertiesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
				return new EmbeddedObjectPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
				return new EmbeddedPackagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
				return new ImagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty":
				return new CustomPropertyPart();
			case "http://schemas.microsoft.com/office/2006/relationships/wsSortMap":
				return new WorksheetSortMapPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable":
				return new QueryTablePart();
			case "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary":
				return new EmbeddedControlPersistenceBinaryDataPart();
			case "http://schemas.microsoft.com/office/2007/relationships/slicer":
				return new SlicersPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D76E RID: 55150 RVA: 0x002A8E10 File Offset: 0x002A7010
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			base.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D76F RID: 55151 RVA: 0x002A8E2C File Offset: 0x002A702C
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType);
		}

		// Token: 0x0600D770 RID: 55152 RVA: 0x002A8E60 File Offset: 0x002A7060
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(string contentType, string id)
		{
			EmbeddedControlPersistencePart embeddedControlPersistencePart = new EmbeddedControlPersistencePart();
			this.InitPart<EmbeddedControlPersistencePart>(embeddedControlPersistencePart, contentType, id);
			return embeddedControlPersistencePart;
		}

		// Token: 0x0600D771 RID: 55153 RVA: 0x002A8E80 File Offset: 0x002A7080
		public EmbeddedControlPersistencePart AddEmbeddedControlPersistencePart(EmbeddedControlPersistencePartType partType, string id)
		{
			string contentType = EmbeddedControlPersistencePartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistencePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistencePart(contentType, id);
		}

		// Token: 0x0600D772 RID: 55154 RVA: 0x002A8EB8 File Offset: 0x002A70B8
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600D773 RID: 55155 RVA: 0x002A8ED4 File Offset: 0x002A70D4
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D774 RID: 55156 RVA: 0x002A8EF0 File Offset: 0x002A70F0
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D775 RID: 55157 RVA: 0x002A8F0C File Offset: 0x002A710C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D776 RID: 55158 RVA: 0x002A8F40 File Offset: 0x002A7140
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D777 RID: 55159 RVA: 0x002A8F60 File Offset: 0x002A7160
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D778 RID: 55160 RVA: 0x002A8F98 File Offset: 0x002A7198
		public CustomPropertyPart AddCustomPropertyPart(string contentType)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			base.InitPart<CustomPropertyPart>(customPropertyPart, contentType);
			return customPropertyPart;
		}

		// Token: 0x0600D779 RID: 55161 RVA: 0x002A8FB4 File Offset: 0x002A71B4
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType);
		}

		// Token: 0x0600D77A RID: 55162 RVA: 0x002A8FE8 File Offset: 0x002A71E8
		public CustomPropertyPart AddCustomPropertyPart(string contentType, string id)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			this.InitPart<CustomPropertyPart>(customPropertyPart, contentType, id);
			return customPropertyPart;
		}

		// Token: 0x0600D77B RID: 55163 RVA: 0x002A9008 File Offset: 0x002A7208
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType, string id)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType, id);
		}

		// Token: 0x0600D77C RID: 55164 RVA: 0x002A9040 File Offset: 0x002A7240
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D77D RID: 55165 RVA: 0x002A905C File Offset: 0x002A725C
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600D77E RID: 55166 RVA: 0x002A9090 File Offset: 0x002A7290
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600D77F RID: 55167 RVA: 0x002A90B0 File Offset: 0x002A72B0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x17003518 RID: 13592
		// (get) Token: 0x0600D780 RID: 55168 RVA: 0x002A90E5 File Offset: 0x002A72E5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";
			}
		}

		// Token: 0x17003519 RID: 13593
		// (get) Token: 0x0600D781 RID: 55169 RVA: 0x002A90EC File Offset: 0x002A72EC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml";
			}
		}

		// Token: 0x1700351A RID: 13594
		// (get) Token: 0x0600D782 RID: 55170 RVA: 0x002A90F3 File Offset: 0x002A72F3
		internal sealed override string TargetPath
		{
			get
			{
				return "worksheets";
			}
		}

		// Token: 0x1700351B RID: 13595
		// (get) Token: 0x0600D783 RID: 55171 RVA: 0x002A77F6 File Offset: 0x002A59F6
		internal sealed override string TargetName
		{
			get
			{
				return "sheet";
			}
		}

		// Token: 0x1700351C RID: 13596
		// (get) Token: 0x0600D784 RID: 55172 RVA: 0x002A77FD File Offset: 0x002A59FD
		public IEnumerable<SpreadsheetPrinterSettingsPart> SpreadsheetPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<SpreadsheetPrinterSettingsPart>();
			}
		}

		// Token: 0x1700351D RID: 13597
		// (get) Token: 0x0600D785 RID: 55173 RVA: 0x002A7805 File Offset: 0x002A5A05
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.GetSubPartOfType<DrawingsPart>();
			}
		}

		// Token: 0x1700351E RID: 13598
		// (get) Token: 0x0600D786 RID: 55174 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x1700351F RID: 13599
		// (get) Token: 0x0600D787 RID: 55175 RVA: 0x002A90FA File Offset: 0x002A72FA
		public WorksheetCommentsPart WorksheetCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<WorksheetCommentsPart>();
			}
		}

		// Token: 0x17003520 RID: 13600
		// (get) Token: 0x0600D788 RID: 55176 RVA: 0x002A9102 File Offset: 0x002A7302
		public IEnumerable<PivotTablePart> PivotTableParts
		{
			get
			{
				return base.GetPartsOfType<PivotTablePart>();
			}
		}

		// Token: 0x17003521 RID: 13601
		// (get) Token: 0x0600D789 RID: 55177 RVA: 0x002A910A File Offset: 0x002A730A
		public SingleCellTablePart SingleCellTablePart
		{
			get
			{
				return base.GetSubPartOfType<SingleCellTablePart>();
			}
		}

		// Token: 0x17003522 RID: 13602
		// (get) Token: 0x0600D78A RID: 55178 RVA: 0x002A9112 File Offset: 0x002A7312
		public IEnumerable<TableDefinitionPart> TableDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<TableDefinitionPart>();
			}
		}

		// Token: 0x17003523 RID: 13603
		// (get) Token: 0x0600D78B RID: 55179 RVA: 0x002A3FE9 File Offset: 0x002A21E9
		public IEnumerable<EmbeddedControlPersistencePart> EmbeddedControlPersistenceParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistencePart>();
			}
		}

		// Token: 0x17003524 RID: 13604
		// (get) Token: 0x0600D78C RID: 55180 RVA: 0x002A911A File Offset: 0x002A731A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public IEnumerable<ControlPropertiesPart> ControlPropertiesParts
		{
			get
			{
				return base.GetPartsOfType<ControlPropertiesPart>();
			}
		}

		// Token: 0x17003525 RID: 13605
		// (get) Token: 0x0600D78D RID: 55181 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003526 RID: 13606
		// (get) Token: 0x0600D78E RID: 55182 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003527 RID: 13607
		// (get) Token: 0x0600D78F RID: 55183 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003528 RID: 13608
		// (get) Token: 0x0600D790 RID: 55184 RVA: 0x002A9122 File Offset: 0x002A7322
		public IEnumerable<CustomPropertyPart> CustomPropertyParts
		{
			get
			{
				return base.GetPartsOfType<CustomPropertyPart>();
			}
		}

		// Token: 0x17003529 RID: 13609
		// (get) Token: 0x0600D791 RID: 55185 RVA: 0x002A912A File Offset: 0x002A732A
		public WorksheetSortMapPart WorksheetSortMapPart
		{
			get
			{
				return base.GetSubPartOfType<WorksheetSortMapPart>();
			}
		}

		// Token: 0x1700352A RID: 13610
		// (get) Token: 0x0600D792 RID: 55186 RVA: 0x002A8929 File Offset: 0x002A6B29
		public IEnumerable<QueryTablePart> QueryTableParts
		{
			get
			{
				return base.GetPartsOfType<QueryTablePart>();
			}
		}

		// Token: 0x1700352B RID: 13611
		// (get) Token: 0x0600D793 RID: 55187 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x1700352C RID: 13612
		// (get) Token: 0x0600D794 RID: 55188 RVA: 0x002A913A File Offset: 0x002A733A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public IEnumerable<SlicersPart> SlicersParts
		{
			get
			{
				return base.GetPartsOfType<SlicersPart>();
			}
		}

		// Token: 0x1700352D RID: 13613
		// (get) Token: 0x0600D795 RID: 55189 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700352E RID: 13614
		// (get) Token: 0x0600D796 RID: 55190 RVA: 0x002A9142 File Offset: 0x002A7342
		// (set) Token: 0x0600D797 RID: 55191 RVA: 0x002A914A File Offset: 0x002A734A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Worksheet;
			}
		}

		// Token: 0x1700352F RID: 13615
		// (get) Token: 0x0600D798 RID: 55192 RVA: 0x002A9158 File Offset: 0x002A7358
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Worksheet;
			}
		}

		// Token: 0x17003530 RID: 13616
		// (get) Token: 0x0600D799 RID: 55193 RVA: 0x002A9160 File Offset: 0x002A7360
		// (set) Token: 0x0600D79A RID: 55194 RVA: 0x002A3296 File Offset: 0x002A1496
		public Worksheet Worksheet
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Worksheet>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006AEA RID: 27370
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet";

		// Token: 0x04006AEB RID: 27371
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml";

		// Token: 0x04006AEC RID: 27372
		internal const string TargetPathConstant = "worksheets";

		// Token: 0x04006AED RID: 27373
		internal const string TargetNameConstant = "sheet";

		// Token: 0x04006AEE RID: 27374
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AEF RID: 27375
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AF0 RID: 27376
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Worksheet _rootEle;
	}
}
