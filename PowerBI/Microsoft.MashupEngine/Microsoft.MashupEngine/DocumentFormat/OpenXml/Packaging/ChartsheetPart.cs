using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216B RID: 8555
	internal class ChartsheetPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D636 RID: 54838 RVA: 0x002A75FC File Offset: 0x002A57FC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ChartsheetPart._partConstraint == null)
			{
				ChartsheetPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return ChartsheetPart._partConstraint;
		}

		// Token: 0x0600D637 RID: 54839 RVA: 0x002A7694 File Offset: 0x002A5894
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ChartsheetPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ChartsheetPart._dataPartReferenceConstraint = dictionary;
			}
			return ChartsheetPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D638 RID: 54840 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ChartsheetPart()
		{
		}

		// Token: 0x0600D639 RID: 54841 RVA: 0x002A76BC File Offset: 0x002A58BC
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings")
				{
					return new SpreadsheetPrinterSettingsPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing")
				{
					return new DrawingsPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/vmlDrawing")
				{
					return new VmlDrawingPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D63A RID: 54842 RVA: 0x002A773C File Offset: 0x002A593C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D63B RID: 54843 RVA: 0x002A7758 File Offset: 0x002A5958
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D63C RID: 54844 RVA: 0x002A778C File Offset: 0x002A598C
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D63D RID: 54845 RVA: 0x002A77AC File Offset: 0x002A59AC
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x1700345F RID: 13407
		// (get) Token: 0x0600D63E RID: 54846 RVA: 0x002A77E1 File Offset: 0x002A59E1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet";
			}
		}

		// Token: 0x17003460 RID: 13408
		// (get) Token: 0x0600D63F RID: 54847 RVA: 0x002A77E8 File Offset: 0x002A59E8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.chartsheet+xml";
			}
		}

		// Token: 0x17003461 RID: 13409
		// (get) Token: 0x0600D640 RID: 54848 RVA: 0x002A77EF File Offset: 0x002A59EF
		internal sealed override string TargetPath
		{
			get
			{
				return "chartsheets";
			}
		}

		// Token: 0x17003462 RID: 13410
		// (get) Token: 0x0600D641 RID: 54849 RVA: 0x002A77F6 File Offset: 0x002A59F6
		internal sealed override string TargetName
		{
			get
			{
				return "sheet";
			}
		}

		// Token: 0x17003463 RID: 13411
		// (get) Token: 0x0600D642 RID: 54850 RVA: 0x002A77FD File Offset: 0x002A59FD
		public IEnumerable<SpreadsheetPrinterSettingsPart> SpreadsheetPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<SpreadsheetPrinterSettingsPart>();
			}
		}

		// Token: 0x17003464 RID: 13412
		// (get) Token: 0x0600D643 RID: 54851 RVA: 0x002A7805 File Offset: 0x002A5A05
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.GetSubPartOfType<DrawingsPart>();
			}
		}

		// Token: 0x17003465 RID: 13413
		// (get) Token: 0x0600D644 RID: 54852 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x17003466 RID: 13414
		// (get) Token: 0x0600D645 RID: 54853 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003467 RID: 13415
		// (get) Token: 0x0600D646 RID: 54854 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003468 RID: 13416
		// (get) Token: 0x0600D647 RID: 54855 RVA: 0x002A7815 File Offset: 0x002A5A15
		// (set) Token: 0x0600D648 RID: 54856 RVA: 0x002A781D File Offset: 0x002A5A1D
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Chartsheet;
			}
		}

		// Token: 0x17003469 RID: 13417
		// (get) Token: 0x0600D649 RID: 54857 RVA: 0x002A782B File Offset: 0x002A5A2B
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Chartsheet;
			}
		}

		// Token: 0x1700346A RID: 13418
		// (get) Token: 0x0600D64A RID: 54858 RVA: 0x002A7833 File Offset: 0x002A5A33
		// (set) Token: 0x0600D64B RID: 54859 RVA: 0x002A3296 File Offset: 0x002A1496
		public Chartsheet Chartsheet
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Chartsheet>();
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

		// Token: 0x04006A58 RID: 27224
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartsheet";

		// Token: 0x04006A59 RID: 27225
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.chartsheet+xml";

		// Token: 0x04006A5A RID: 27226
		internal const string TargetPathConstant = "chartsheets";

		// Token: 0x04006A5B RID: 27227
		internal const string TargetNameConstant = "sheet";

		// Token: 0x04006A5C RID: 27228
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A5D RID: 27229
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A5E RID: 27230
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Chartsheet _rootEle;
	}
}
