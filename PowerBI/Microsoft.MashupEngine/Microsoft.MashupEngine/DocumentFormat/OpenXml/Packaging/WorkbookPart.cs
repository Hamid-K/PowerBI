using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002169 RID: 8553
	internal class WorkbookPart : OpenXmlPart
	{
		// Token: 0x0600D5FD RID: 54781 RVA: 0x002A6DE4 File Offset: 0x002A4FE4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorkbookPart._partConstraint == null)
			{
				WorkbookPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml",
						new PartConstraintRule("CustomXmlPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain",
						new PartConstraintRule("CalculationChainPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.calcChain+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata",
						new PartConstraintRule("CellMetadataPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheetMetadata+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections",
						new PartConstraintRule("ConnectionsPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.connections+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps",
						new PartConstraintRule("CustomXmlMappingsPart", "application/xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings",
						new PartConstraintRule("SharedStringTablePart", "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders",
						new PartConstraintRule("WorkbookRevisionHeaderPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionHeaders+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames",
						new PartConstraintRule("WorkbookUserDataPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.userNames+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles",
						new PartConstraintRule("WorkbookStylesPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme",
						new PartConstraintRule("ThemePart", "application/vnd.openxmlformats-officedocument.theme+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail",
						new PartConstraintRule("ThumbnailPart", null, false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies",
						new PartConstraintRule("VolatileDependenciesPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.volatileDependencies+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet",
						new PartConstraintRule("ChartsheetPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.chartsheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet",
						new PartConstraintRule("DialogsheetPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.dialogsheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink",
						new PartConstraintRule("ExternalWorkbookPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.externalLink+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition",
						new PartConstraintRule("PivotTableCacheDefinitionPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheDefinition+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet",
						new PartConstraintRule("WorksheetPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/attachedToolbars",
						new PartConstraintRule("ExcelAttachedToolbarsPart", "application/vnd.ms-excel.attachedToolbars", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/vbaProject",
						new PartConstraintRule("VbaProjectPart", "application/vnd.ms-office.vbaProject", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/xlMacrosheet",
						new PartConstraintRule("MacroSheetPart", "application/vnd.ms-excel.macrosheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2006/relationships/xlIntlMacrosheet",
						new PartConstraintRule("InternationalMacroSheetPart", "application/vnd.ms-excel.intlmacrosheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/customDataProps",
						new PartConstraintRule("CustomDataPropertiesPart", "application/vnd.ms-excel.customDataProperties+xml", false, true, FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/slicerCache",
						new PartConstraintRule("SlicerCachePart", "application/vnd.ms-excel.slicerCache+xml", false, true, FileFormatVersions.Office2010)
					}
				};
			}
			return WorkbookPart._partConstraint;
		}

		// Token: 0x0600D5FE RID: 54782 RVA: 0x002A70A0 File Offset: 0x002A52A0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorkbookPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookPart._dataPartReferenceConstraint = dictionary;
			}
			return WorkbookPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D5FF RID: 54783 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorkbookPart()
		{
		}

		// Token: 0x0600D600 RID: 54784 RVA: 0x002A70C8 File Offset: 0x002A52C8
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml":
				return new CustomXmlPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/calcChain":
				return new CalculationChainPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata":
				return new CellMetadataPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/connections":
				return new ConnectionsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps":
				return new CustomXmlMappingsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings":
				return new SharedStringTablePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders":
				return new WorkbookRevisionHeaderPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/usernames":
				return new WorkbookUserDataPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles":
				return new WorkbookStylesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme":
				return new ThemePart();
			case "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail":
				return new ThumbnailPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies":
				return new VolatileDependenciesPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet":
				return new ChartsheetPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet":
				return new DialogsheetPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink":
				return new ExternalWorkbookPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition":
				return new PivotTableCacheDefinitionPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet":
				return new WorksheetPart();
			case "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars":
				return new ExcelAttachedToolbarsPart();
			case "http://schemas.microsoft.com/office/2006/relationships/vbaProject":
				return new VbaProjectPart();
			case "http://schemas.microsoft.com/office/2006/relationships/xlMacrosheet":
				return new MacroSheetPart();
			case "http://schemas.microsoft.com/office/2006/relationships/xlIntlMacrosheet":
				return new InternationalMacroSheetPart();
			case "http://schemas.microsoft.com/office/2007/relationships/customDataProps":
				return new CustomDataPropertiesPart();
			case "http://schemas.microsoft.com/office/2007/relationships/slicerCache":
				return new SlicerCachePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D601 RID: 54785 RVA: 0x002A733C File Offset: 0x002A553C
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D602 RID: 54786 RVA: 0x002A7358 File Offset: 0x002A5558
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D603 RID: 54787 RVA: 0x002A738C File Offset: 0x002A558C
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D604 RID: 54788 RVA: 0x002A73AC File Offset: 0x002A55AC
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x0600D605 RID: 54789 RVA: 0x002A73E4 File Offset: 0x002A55E4
		public ThumbnailPart AddThumbnailPart(string contentType)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			base.InitPart<ThumbnailPart>(thumbnailPart, contentType);
			return thumbnailPart;
		}

		// Token: 0x0600D606 RID: 54790 RVA: 0x002A7400 File Offset: 0x002A5600
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType);
		}

		// Token: 0x0600D607 RID: 54791 RVA: 0x002A7434 File Offset: 0x002A5634
		public ThumbnailPart AddThumbnailPart(string contentType, string id)
		{
			ThumbnailPart thumbnailPart = new ThumbnailPart();
			this.InitPart<ThumbnailPart>(thumbnailPart, contentType, id);
			return thumbnailPart;
		}

		// Token: 0x0600D608 RID: 54792 RVA: 0x002A7454 File Offset: 0x002A5654
		public ThumbnailPart AddThumbnailPart(ThumbnailPartType partType, string id)
		{
			string contentType = ThumbnailPartTypeInfo.GetContentType(partType);
			string targetExtension = ThumbnailPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddThumbnailPart(contentType, id);
		}

		// Token: 0x17003439 RID: 13369
		// (get) Token: 0x0600D609 RID: 54793 RVA: 0x002A3F0C File Offset: 0x002A210C
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
			}
		}

		// Token: 0x1700343A RID: 13370
		// (get) Token: 0x0600D60A RID: 54794 RVA: 0x002A7489 File Offset: 0x002A5689
		internal sealed override string TargetPath
		{
			get
			{
				return "xl";
			}
		}

		// Token: 0x1700343B RID: 13371
		// (get) Token: 0x0600D60B RID: 54795 RVA: 0x002A7490 File Offset: 0x002A5690
		internal sealed override string TargetName
		{
			get
			{
				return "workbook";
			}
		}

		// Token: 0x1700343C RID: 13372
		// (get) Token: 0x0600D60C RID: 54796 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x1700343D RID: 13373
		// (get) Token: 0x0600D60D RID: 54797 RVA: 0x002A7497 File Offset: 0x002A5697
		public CalculationChainPart CalculationChainPart
		{
			get
			{
				return base.GetSubPartOfType<CalculationChainPart>();
			}
		}

		// Token: 0x1700343E RID: 13374
		// (get) Token: 0x0600D60E RID: 54798 RVA: 0x002A749F File Offset: 0x002A569F
		public CellMetadataPart CellMetadataPart
		{
			get
			{
				return base.GetSubPartOfType<CellMetadataPart>();
			}
		}

		// Token: 0x1700343F RID: 13375
		// (get) Token: 0x0600D60F RID: 54799 RVA: 0x002A74A7 File Offset: 0x002A56A7
		public ConnectionsPart ConnectionsPart
		{
			get
			{
				return base.GetSubPartOfType<ConnectionsPart>();
			}
		}

		// Token: 0x17003440 RID: 13376
		// (get) Token: 0x0600D610 RID: 54800 RVA: 0x002A74AF File Offset: 0x002A56AF
		public CustomXmlMappingsPart CustomXmlMappingsPart
		{
			get
			{
				return base.GetSubPartOfType<CustomXmlMappingsPart>();
			}
		}

		// Token: 0x17003441 RID: 13377
		// (get) Token: 0x0600D611 RID: 54801 RVA: 0x002A74B7 File Offset: 0x002A56B7
		public SharedStringTablePart SharedStringTablePart
		{
			get
			{
				return base.GetSubPartOfType<SharedStringTablePart>();
			}
		}

		// Token: 0x17003442 RID: 13378
		// (get) Token: 0x0600D612 RID: 54802 RVA: 0x002A74BF File Offset: 0x002A56BF
		public WorkbookRevisionHeaderPart WorkbookRevisionHeaderPart
		{
			get
			{
				return base.GetSubPartOfType<WorkbookRevisionHeaderPart>();
			}
		}

		// Token: 0x17003443 RID: 13379
		// (get) Token: 0x0600D613 RID: 54803 RVA: 0x002A74C7 File Offset: 0x002A56C7
		public WorkbookUserDataPart WorkbookUserDataPart
		{
			get
			{
				return base.GetSubPartOfType<WorkbookUserDataPart>();
			}
		}

		// Token: 0x17003444 RID: 13380
		// (get) Token: 0x0600D614 RID: 54804 RVA: 0x002A74CF File Offset: 0x002A56CF
		public WorkbookStylesPart WorkbookStylesPart
		{
			get
			{
				return base.GetSubPartOfType<WorkbookStylesPart>();
			}
		}

		// Token: 0x17003445 RID: 13381
		// (get) Token: 0x0600D615 RID: 54805 RVA: 0x002A3F31 File Offset: 0x002A2131
		public ThemePart ThemePart
		{
			get
			{
				return base.GetSubPartOfType<ThemePart>();
			}
		}

		// Token: 0x17003446 RID: 13382
		// (get) Token: 0x0600D616 RID: 54806 RVA: 0x002A3F39 File Offset: 0x002A2139
		public ThumbnailPart ThumbnailPart
		{
			get
			{
				return base.GetSubPartOfType<ThumbnailPart>();
			}
		}

		// Token: 0x17003447 RID: 13383
		// (get) Token: 0x0600D617 RID: 54807 RVA: 0x002A74D7 File Offset: 0x002A56D7
		public VolatileDependenciesPart VolatileDependenciesPart
		{
			get
			{
				return base.GetSubPartOfType<VolatileDependenciesPart>();
			}
		}

		// Token: 0x17003448 RID: 13384
		// (get) Token: 0x0600D618 RID: 54808 RVA: 0x002A74DF File Offset: 0x002A56DF
		public IEnumerable<ChartsheetPart> ChartsheetParts
		{
			get
			{
				return base.GetPartsOfType<ChartsheetPart>();
			}
		}

		// Token: 0x17003449 RID: 13385
		// (get) Token: 0x0600D619 RID: 54809 RVA: 0x002A74E7 File Offset: 0x002A56E7
		public IEnumerable<DialogsheetPart> DialogsheetParts
		{
			get
			{
				return base.GetPartsOfType<DialogsheetPart>();
			}
		}

		// Token: 0x1700344A RID: 13386
		// (get) Token: 0x0600D61A RID: 54810 RVA: 0x002A74EF File Offset: 0x002A56EF
		public IEnumerable<ExternalWorkbookPart> ExternalWorkbookParts
		{
			get
			{
				return base.GetPartsOfType<ExternalWorkbookPart>();
			}
		}

		// Token: 0x1700344B RID: 13387
		// (get) Token: 0x0600D61B RID: 54811 RVA: 0x002A74F7 File Offset: 0x002A56F7
		public IEnumerable<PivotTableCacheDefinitionPart> PivotTableCacheDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<PivotTableCacheDefinitionPart>();
			}
		}

		// Token: 0x1700344C RID: 13388
		// (get) Token: 0x0600D61C RID: 54812 RVA: 0x002A74FF File Offset: 0x002A56FF
		public IEnumerable<WorksheetPart> WorksheetParts
		{
			get
			{
				return base.GetPartsOfType<WorksheetPart>();
			}
		}

		// Token: 0x1700344D RID: 13389
		// (get) Token: 0x0600D61D RID: 54813 RVA: 0x002A7507 File Offset: 0x002A5707
		public ExcelAttachedToolbarsPart ExcelAttachedToolbarsPart
		{
			get
			{
				return base.GetSubPartOfType<ExcelAttachedToolbarsPart>();
			}
		}

		// Token: 0x1700344E RID: 13390
		// (get) Token: 0x0600D61E RID: 54814 RVA: 0x002A3FA9 File Offset: 0x002A21A9
		public VbaProjectPart VbaProjectPart
		{
			get
			{
				return base.GetSubPartOfType<VbaProjectPart>();
			}
		}

		// Token: 0x1700344F RID: 13391
		// (get) Token: 0x0600D61F RID: 54815 RVA: 0x002A750F File Offset: 0x002A570F
		public IEnumerable<MacroSheetPart> MacroSheetParts
		{
			get
			{
				return base.GetPartsOfType<MacroSheetPart>();
			}
		}

		// Token: 0x17003450 RID: 13392
		// (get) Token: 0x0600D620 RID: 54816 RVA: 0x002A7517 File Offset: 0x002A5717
		public IEnumerable<InternationalMacroSheetPart> InternationalMacroSheetParts
		{
			get
			{
				return base.GetPartsOfType<InternationalMacroSheetPart>();
			}
		}

		// Token: 0x17003451 RID: 13393
		// (get) Token: 0x0600D621 RID: 54817 RVA: 0x002A751F File Offset: 0x002A571F
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public IEnumerable<CustomDataPropertiesPart> CustomDataPropertiesParts
		{
			get
			{
				return base.GetPartsOfType<CustomDataPropertiesPart>();
			}
		}

		// Token: 0x17003452 RID: 13394
		// (get) Token: 0x0600D622 RID: 54818 RVA: 0x002A7527 File Offset: 0x002A5727
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public IEnumerable<SlicerCachePart> SlicerCacheParts
		{
			get
			{
				return base.GetPartsOfType<SlicerCachePart>();
			}
		}

		// Token: 0x17003453 RID: 13395
		// (get) Token: 0x0600D623 RID: 54819 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003454 RID: 13396
		// (get) Token: 0x0600D624 RID: 54820 RVA: 0x002A752F File Offset: 0x002A572F
		// (set) Token: 0x0600D625 RID: 54821 RVA: 0x002A7537 File Offset: 0x002A5737
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Workbook;
			}
		}

		// Token: 0x17003455 RID: 13397
		// (get) Token: 0x0600D626 RID: 54822 RVA: 0x002A7545 File Offset: 0x002A5745
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Workbook;
			}
		}

		// Token: 0x17003456 RID: 13398
		// (get) Token: 0x0600D627 RID: 54823 RVA: 0x002A754D File Offset: 0x002A574D
		// (set) Token: 0x0600D628 RID: 54824 RVA: 0x002A3296 File Offset: 0x002A1496
		public Workbook Workbook
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Workbook>();
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

		// Token: 0x04006A4B RID: 27211
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";

		// Token: 0x04006A4C RID: 27212
		internal const string TargetPathConstant = "xl";

		// Token: 0x04006A4D RID: 27213
		internal const string TargetNameConstant = "workbook";

		// Token: 0x04006A4E RID: 27214
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A4F RID: 27215
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A50 RID: 27216
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Workbook _rootEle;
	}
}
