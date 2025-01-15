using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002171 RID: 8561
	internal class DrawingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D68E RID: 54926 RVA: 0x002A7C10 File Offset: 0x002A5E10
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DrawingsPart._partConstraint == null)
			{
				DrawingsPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart",
						new PartConstraintRule("ChartPart", "application/vnd.openxmlformats-officedocument.drawingml.chart+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors",
						new PartConstraintRule("DiagramColorsPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramColors+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData",
						new PartConstraintRule("DiagramDataPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.microsoft.com/office/2007/relationships/diagramDrawing",
						new PartConstraintRule("DiagramPersistLayoutPart", "application/vnd.ms-office.drawingml.diagramDrawing+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout",
						new PartConstraintRule("DiagramLayoutDefinitionPart", "application/vnd.openxmlformats-officedocument.drawingml.diagramLayout+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle",
						new PartConstraintRule("DiagramStylePart", "application/vnd.openxmlformats-officedocument.drawingml.diagramStyle+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml",
						new PartConstraintRule("CustomXmlPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return DrawingsPart._partConstraint;
		}

		// Token: 0x0600D68F RID: 54927 RVA: 0x002A7D18 File Offset: 0x002A5F18
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DrawingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DrawingsPart._dataPartReferenceConstraint = dictionary;
			}
			return DrawingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D690 RID: 54928 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DrawingsPart()
		{
		}

		// Token: 0x0600D691 RID: 54929 RVA: 0x002A7D40 File Offset: 0x002A5F40
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			switch (relationshipType)
			{
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart":
				return new ChartPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors":
				return new DiagramColorsPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData":
				return new DiagramDataPart();
			case "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing":
				return new DiagramPersistLayoutPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout":
				return new DiagramLayoutDefinitionPart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle":
				return new DiagramStylePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image":
				return new ImagePart();
			case "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml":
				return new CustomXmlPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D692 RID: 54930 RVA: 0x002A7E54 File Offset: 0x002A6054
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D693 RID: 54931 RVA: 0x002A7E70 File Offset: 0x002A6070
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D694 RID: 54932 RVA: 0x002A7EA4 File Offset: 0x002A60A4
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D695 RID: 54933 RVA: 0x002A7EC4 File Offset: 0x002A60C4
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x0600D696 RID: 54934 RVA: 0x002A7EFC File Offset: 0x002A60FC
		public CustomXmlPart AddCustomXmlPart(string contentType)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			base.InitPart<CustomXmlPart>(customXmlPart, contentType);
			return customXmlPart;
		}

		// Token: 0x0600D697 RID: 54935 RVA: 0x002A7F18 File Offset: 0x002A6118
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType);
		}

		// Token: 0x0600D698 RID: 54936 RVA: 0x002A7F4C File Offset: 0x002A614C
		public CustomXmlPart AddCustomXmlPart(string contentType, string id)
		{
			CustomXmlPart customXmlPart = new CustomXmlPart();
			this.InitPart<CustomXmlPart>(customXmlPart, contentType, id);
			return customXmlPart;
		}

		// Token: 0x0600D699 RID: 54937 RVA: 0x002A7F6C File Offset: 0x002A616C
		public CustomXmlPart AddCustomXmlPart(CustomXmlPartType partType, string id)
		{
			string contentType = CustomXmlPartTypeInfo.GetContentType(partType);
			string targetExtension = CustomXmlPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddCustomXmlPart(contentType, id);
		}

		// Token: 0x17003494 RID: 13460
		// (get) Token: 0x0600D69A RID: 54938 RVA: 0x002A7FA1 File Offset: 0x002A61A1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing";
			}
		}

		// Token: 0x17003495 RID: 13461
		// (get) Token: 0x0600D69B RID: 54939 RVA: 0x002A7FA8 File Offset: 0x002A61A8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawing+xml";
			}
		}

		// Token: 0x17003496 RID: 13462
		// (get) Token: 0x0600D69C RID: 54940 RVA: 0x002A7FAF File Offset: 0x002A61AF
		internal sealed override string TargetPath
		{
			get
			{
				return "../drawings";
			}
		}

		// Token: 0x17003497 RID: 13463
		// (get) Token: 0x0600D69D RID: 54941 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		internal sealed override string TargetName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x17003498 RID: 13464
		// (get) Token: 0x0600D69E RID: 54942 RVA: 0x002A3FB9 File Offset: 0x002A21B9
		public IEnumerable<ChartPart> ChartParts
		{
			get
			{
				return base.GetPartsOfType<ChartPart>();
			}
		}

		// Token: 0x17003499 RID: 13465
		// (get) Token: 0x0600D69F RID: 54943 RVA: 0x002A3FC1 File Offset: 0x002A21C1
		public IEnumerable<DiagramColorsPart> DiagramColorsParts
		{
			get
			{
				return base.GetPartsOfType<DiagramColorsPart>();
			}
		}

		// Token: 0x1700349A RID: 13466
		// (get) Token: 0x0600D6A0 RID: 54944 RVA: 0x002A3FC9 File Offset: 0x002A21C9
		public IEnumerable<DiagramDataPart> DiagramDataParts
		{
			get
			{
				return base.GetPartsOfType<DiagramDataPart>();
			}
		}

		// Token: 0x1700349B RID: 13467
		// (get) Token: 0x0600D6A1 RID: 54945 RVA: 0x002A3FD1 File Offset: 0x002A21D1
		public IEnumerable<DiagramPersistLayoutPart> DiagramPersistLayoutParts
		{
			get
			{
				return base.GetPartsOfType<DiagramPersistLayoutPart>();
			}
		}

		// Token: 0x1700349C RID: 13468
		// (get) Token: 0x0600D6A2 RID: 54946 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		public IEnumerable<DiagramLayoutDefinitionPart> DiagramLayoutDefinitionParts
		{
			get
			{
				return base.GetPartsOfType<DiagramLayoutDefinitionPart>();
			}
		}

		// Token: 0x1700349D RID: 13469
		// (get) Token: 0x0600D6A3 RID: 54947 RVA: 0x002A3FE1 File Offset: 0x002A21E1
		public IEnumerable<DiagramStylePart> DiagramStyleParts
		{
			get
			{
				return base.GetPartsOfType<DiagramStylePart>();
			}
		}

		// Token: 0x1700349E RID: 13470
		// (get) Token: 0x0600D6A4 RID: 54948 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700349F RID: 13471
		// (get) Token: 0x0600D6A5 RID: 54949 RVA: 0x002A3F21 File Offset: 0x002A2121
		public IEnumerable<CustomXmlPart> CustomXmlParts
		{
			get
			{
				return base.GetPartsOfType<CustomXmlPart>();
			}
		}

		// Token: 0x170034A0 RID: 13472
		// (get) Token: 0x0600D6A6 RID: 54950 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034A1 RID: 13473
		// (get) Token: 0x0600D6A7 RID: 54951 RVA: 0x002A7FBD File Offset: 0x002A61BD
		// (set) Token: 0x0600D6A8 RID: 54952 RVA: 0x002A7FC5 File Offset: 0x002A61C5
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as WorksheetDrawing;
			}
		}

		// Token: 0x170034A2 RID: 13474
		// (get) Token: 0x0600D6A9 RID: 54953 RVA: 0x002A7FD3 File Offset: 0x002A61D3
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.WorksheetDrawing;
			}
		}

		// Token: 0x170034A3 RID: 13475
		// (get) Token: 0x0600D6AA RID: 54954 RVA: 0x002A7FDB File Offset: 0x002A61DB
		// (set) Token: 0x0600D6AB RID: 54955 RVA: 0x002A3296 File Offset: 0x002A1496
		public WorksheetDrawing WorksheetDrawing
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<WorksheetDrawing>();
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

		// Token: 0x04006A81 RID: 27265
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing";

		// Token: 0x04006A82 RID: 27266
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawing+xml";

		// Token: 0x04006A83 RID: 27267
		internal const string TargetPathConstant = "../drawings";

		// Token: 0x04006A84 RID: 27268
		internal const string TargetNameConstant = "drawing";

		// Token: 0x04006A85 RID: 27269
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A86 RID: 27270
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A87 RID: 27271
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WorksheetDrawing _rootEle;
	}
}
