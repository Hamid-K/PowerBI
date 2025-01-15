using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AC RID: 8620
	internal class MacroSheetPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DAB7 RID: 55991 RVA: 0x002ADF1C File Offset: 0x002AC11C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (MacroSheetPart._partConstraint == null)
			{
				MacroSheetPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
			return MacroSheetPart._partConstraint;
		}

		// Token: 0x0600DAB8 RID: 55992 RVA: 0x002AE01C File Offset: 0x002AC21C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (MacroSheetPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				MacroSheetPart._dataPartReferenceConstraint = dictionary;
			}
			return MacroSheetPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DAB9 RID: 55993 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal MacroSheetPart()
		{
		}

		// Token: 0x0600DABA RID: 55994 RVA: 0x002AE044 File Offset: 0x002AC244
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

		// Token: 0x0600DABB RID: 55995 RVA: 0x002AE158 File Offset: 0x002AC358
		public CustomPropertyPart AddCustomPropertyPart(string contentType)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			base.InitPart<CustomPropertyPart>(customPropertyPart, contentType);
			return customPropertyPart;
		}

		// Token: 0x0600DABC RID: 55996 RVA: 0x002AE174 File Offset: 0x002AC374
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType);
		}

		// Token: 0x0600DABD RID: 55997 RVA: 0x002AE1A8 File Offset: 0x002AC3A8
		public CustomPropertyPart AddCustomPropertyPart(string contentType, string id)
		{
			CustomPropertyPart customPropertyPart = new CustomPropertyPart();
			this.InitPart<CustomPropertyPart>(customPropertyPart, contentType, id);
			return customPropertyPart;
		}

		// Token: 0x0600DABE RID: 55998 RVA: 0x002AE1C8 File Offset: 0x002AC3C8
		public CustomPropertyPart AddCustomPropertyPart(CustomPropertyPartType partType, string id)
		{
			string contentType = CustomPropertyPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomPropertyPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomPropertyPart(contentType, id);
		}

		// Token: 0x0600DABF RID: 55999 RVA: 0x002AE200 File Offset: 0x002AC400
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x0600DAC0 RID: 56000 RVA: 0x002AE21C File Offset: 0x002AC41C
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600DAC1 RID: 56001 RVA: 0x002AE238 File Offset: 0x002AC438
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DAC2 RID: 56002 RVA: 0x002AE254 File Offset: 0x002AC454
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DAC3 RID: 56003 RVA: 0x002AE288 File Offset: 0x002AC488
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DAC4 RID: 56004 RVA: 0x002AE2A8 File Offset: 0x002AC4A8
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x170036DC RID: 14044
		// (get) Token: 0x0600DAC5 RID: 56005 RVA: 0x002AE2DD File Offset: 0x002AC4DD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/xlMacrosheet";
			}
		}

		// Token: 0x170036DD RID: 14045
		// (get) Token: 0x0600DAC6 RID: 56006 RVA: 0x002AE2E4 File Offset: 0x002AC4E4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.macrosheet+xml";
			}
		}

		// Token: 0x170036DE RID: 14046
		// (get) Token: 0x0600DAC7 RID: 56007 RVA: 0x002AE2EB File Offset: 0x002AC4EB
		internal sealed override string TargetPath
		{
			get
			{
				return "macrosheets";
			}
		}

		// Token: 0x170036DF RID: 14047
		// (get) Token: 0x0600DAC8 RID: 56008 RVA: 0x002A77F6 File Offset: 0x002A59F6
		internal sealed override string TargetName
		{
			get
			{
				return "sheet";
			}
		}

		// Token: 0x170036E0 RID: 14048
		// (get) Token: 0x0600DAC9 RID: 56009 RVA: 0x002A77FD File Offset: 0x002A59FD
		public IEnumerable<SpreadsheetPrinterSettingsPart> SpreadsheetPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<SpreadsheetPrinterSettingsPart>();
			}
		}

		// Token: 0x170036E1 RID: 14049
		// (get) Token: 0x0600DACA RID: 56010 RVA: 0x002A7805 File Offset: 0x002A5A05
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.GetSubPartOfType<DrawingsPart>();
			}
		}

		// Token: 0x170036E2 RID: 14050
		// (get) Token: 0x0600DACB RID: 56011 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x170036E3 RID: 14051
		// (get) Token: 0x0600DACC RID: 56012 RVA: 0x002A90FA File Offset: 0x002A72FA
		public WorksheetCommentsPart WorksheetCommentsPart
		{
			get
			{
				return base.GetSubPartOfType<WorksheetCommentsPart>();
			}
		}

		// Token: 0x170036E4 RID: 14052
		// (get) Token: 0x0600DACD RID: 56013 RVA: 0x002A9122 File Offset: 0x002A7322
		public IEnumerable<CustomPropertyPart> CustomPropertyParts
		{
			get
			{
				return base.GetPartsOfType<CustomPropertyPart>();
			}
		}

		// Token: 0x170036E5 RID: 14053
		// (get) Token: 0x0600DACE RID: 56014 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x170036E6 RID: 14054
		// (get) Token: 0x0600DACF RID: 56015 RVA: 0x002A3FF9 File Offset: 0x002A21F9
		public IEnumerable<EmbeddedPackagePart> EmbeddedPackageParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x170036E7 RID: 14055
		// (get) Token: 0x0600DAD0 RID: 56016 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170036E8 RID: 14056
		// (get) Token: 0x0600DAD1 RID: 56017 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170036E9 RID: 14057
		// (get) Token: 0x0600DAD2 RID: 56018 RVA: 0x002AE2F2 File Offset: 0x002AC4F2
		// (set) Token: 0x0600DAD3 RID: 56019 RVA: 0x002AE2FA File Offset: 0x002AC4FA
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Macrosheet;
			}
		}

		// Token: 0x170036EA RID: 14058
		// (get) Token: 0x0600DAD4 RID: 56020 RVA: 0x002AE308 File Offset: 0x002AC508
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Macrosheet;
			}
		}

		// Token: 0x170036EB RID: 14059
		// (get) Token: 0x0600DAD5 RID: 56021 RVA: 0x002AE310 File Offset: 0x002AC510
		// (set) Token: 0x0600DAD6 RID: 56022 RVA: 0x002A3296 File Offset: 0x002A1496
		public Macrosheet Macrosheet
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Macrosheet>();
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

		// Token: 0x04006C0C RID: 27660
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/xlMacrosheet";

		// Token: 0x04006C0D RID: 27661
		internal const string ContentTypeConstant = "application/vnd.ms-excel.macrosheet+xml";

		// Token: 0x04006C0E RID: 27662
		internal const string TargetPathConstant = "macrosheets";

		// Token: 0x04006C0F RID: 27663
		internal const string TargetNameConstant = "sheet";

		// Token: 0x04006C10 RID: 27664
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C11 RID: 27665
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C12 RID: 27666
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Macrosheet _rootEle;
	}
}
