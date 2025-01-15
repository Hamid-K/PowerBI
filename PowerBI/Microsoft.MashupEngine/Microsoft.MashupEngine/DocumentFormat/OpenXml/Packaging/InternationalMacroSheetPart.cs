using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AD RID: 8621
	internal class InternationalMacroSheetPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DAD7 RID: 56023 RVA: 0x002AE328 File Offset: 0x002AC528
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (InternationalMacroSheetPart._partConstraint == null)
			{
				InternationalMacroSheetPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty",
						new PartConstraintRule("CustomPropertyPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
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
					}
				};
			}
			return InternationalMacroSheetPart._partConstraint;
		}

		// Token: 0x0600DAD8 RID: 56024 RVA: 0x002AE428 File Offset: 0x002AC628
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (InternationalMacroSheetPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				InternationalMacroSheetPart._dataPartReferenceConstraint = dictionary;
			}
			return InternationalMacroSheetPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DAD9 RID: 56025 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal InternationalMacroSheetPart()
		{
		}

		// Token: 0x0600DADA RID: 56026 RVA: 0x002AE450 File Offset: 0x002AC650
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
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty":
				return new CustomPropertyPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject":
				return new EmbeddedObjectPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package":
				return new EmbeddedPackagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
				return new ImagePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600DADB RID: 56027 RVA: 0x002AE564 File Offset: 0x002AC764
		public CustomPropertyPart AddCustomPropertyPart(string contentType)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			base.InitPart<CustomPropertyPart>(customPropertyPart, contentType);
			return customPropertyPart;
		}

		// Token: 0x0600DADC RID: 56028 RVA: 0x002AE580 File Offset: 0x002AC780
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType);
		}

		// Token: 0x0600DADD RID: 56029 RVA: 0x002AE5B4 File Offset: 0x002AC7B4
		public CustomPropertyPart AddCustomPropertyPart(string contentType, string id)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			this.InitPart<CustomPropertyPart>(customPropertyPart, contentType, id);
			return customPropertyPart;
		}

		// Token: 0x0600DADE RID: 56030 RVA: 0x002AE5D4 File Offset: 0x002AC7D4
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType, string id)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType, id);
		}

		// Token: 0x0600DADF RID: 56031 RVA: 0x002AE60C File Offset: 0x002AC80C
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600DAE0 RID: 56032 RVA: 0x002AE628 File Offset: 0x002AC828
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600DAE1 RID: 56033 RVA: 0x002AE644 File Offset: 0x002AC844
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DAE2 RID: 56034 RVA: 0x002AE660 File Offset: 0x002AC860
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DAE3 RID: 56035 RVA: 0x002AE694 File Offset: 0x002AC894
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DAE4 RID: 56036 RVA: 0x002AE6B4 File Offset: 0x002AC8B4
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x170036EC RID: 14060
		// (get) Token: 0x0600DAE5 RID: 56037 RVA: 0x002AE6E9 File Offset: 0x002AC8E9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/xlIntlMacrosheet";
			}
		}

		// Token: 0x170036ED RID: 14061
		// (get) Token: 0x0600DAE6 RID: 56038 RVA: 0x002AE6F0 File Offset: 0x002AC8F0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.intlmacrosheet+xml";
			}
		}

		// Token: 0x170036EE RID: 14062
		// (get) Token: 0x0600DAE7 RID: 56039 RVA: 0x002AE2EB File Offset: 0x002AC4EB
		internal sealed override string TargetPath
		{
			get
			{
				return "macrosheets";
			}
		}

		// Token: 0x170036EF RID: 14063
		// (get) Token: 0x0600DAE8 RID: 56040 RVA: 0x002AE6F7 File Offset: 0x002AC8F7
		internal sealed override string TargetName
		{
			get
			{
				return "intlsheet";
			}
		}

		// Token: 0x170036F0 RID: 14064
		// (get) Token: 0x0600DAE9 RID: 56041 RVA: 0x002A77FD File Offset: 0x002A59FD
		public IEnumerable<SpreadsheetPrinterSettingsPart> SpreadsheetPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<SpreadsheetPrinterSettingsPart>();
			}
		}

		// Token: 0x170036F1 RID: 14065
		// (get) Token: 0x0600DAEA RID: 56042 RVA: 0x002A7805 File Offset: 0x002A5A05
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.GetSubPartOfType<DrawingsPart>();
			}
		}

		// Token: 0x170036F2 RID: 14066
		// (get) Token: 0x0600DAEB RID: 56043 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x170036F3 RID: 14067
		// (get) Token: 0x0600DAEC RID: 56044 RVA: 0x002A90FA File Offset: 0x002A72FA
		public WorksheetCommentsPart WorksheetCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<WorksheetCommentsPart>();
			}
		}

		// Token: 0x170036F4 RID: 14068
		// (get) Token: 0x0600DAED RID: 56045 RVA: 0x002A9122 File Offset: 0x002A7322
		public IEnumerable<CustomPropertyPart> CustomPropertyParts
		{
			get
			{
				return base.GetPartsOfType<CustomPropertyPart>();
			}
		}

		// Token: 0x170036F5 RID: 14069
		// (get) Token: 0x0600DAEE RID: 56046 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170036F6 RID: 14070
		// (get) Token: 0x0600DAEF RID: 56047 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170036F7 RID: 14071
		// (get) Token: 0x0600DAF0 RID: 56048 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170036F8 RID: 14072
		// (get) Token: 0x0600DAF1 RID: 56049 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006C13 RID: 27667
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/xlIntlMacrosheet";

		// Token: 0x04006C14 RID: 27668
		internal const string ContentTypeConstant = "application/vnd.ms-excel.intlmacrosheet+xml";

		// Token: 0x04006C15 RID: 27669
		internal const string TargetPathConstant = "macrosheets";

		// Token: 0x04006C16 RID: 27670
		internal const string TargetNameConstant = "intlsheet";

		// Token: 0x04006C17 RID: 27671
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C18 RID: 27672
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
