using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002170 RID: 8560
	internal class DialogsheetPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D67B RID: 54907 RVA: 0x002A7A68 File Offset: 0x002A5C68
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DialogsheetPart._partConstraint == null)
			{
				DialogsheetPart._partConstraint = new Dictionary<string, PartConstraintRule>
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
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject",
						new PartConstraintRule("EmbeddedObjectPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return DialogsheetPart._partConstraint;
		}

		// Token: 0x0600D67C RID: 54908 RVA: 0x002A7B00 File Offset: 0x002A5D00
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DialogsheetPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DialogsheetPart._dataPartReferenceConstraint = dictionary;
			}
			return DialogsheetPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D67D RID: 54909 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DialogsheetPart()
		{
		}

		// Token: 0x0600D67E RID: 54910 RVA: 0x002A7B28 File Offset: 0x002A5D28
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
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject")
				{
					return new EmbeddedObjectPart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D67F RID: 54911 RVA: 0x002A7BA8 File Offset: 0x002A5DA8
		public EmbeddedObjectPart AddEmbeddedObjectPart(string contentType)
		{
			EmbeddedObjectPart embeddedObjectPart = new EmbeddedObjectPart();
			base.InitPart<EmbeddedObjectPart>(embeddedObjectPart, contentType);
			return embeddedObjectPart;
		}

		// Token: 0x17003488 RID: 13448
		// (get) Token: 0x0600D680 RID: 54912 RVA: 0x002A7BC4 File Offset: 0x002A5DC4
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet";
			}
		}

		// Token: 0x17003489 RID: 13449
		// (get) Token: 0x0600D681 RID: 54913 RVA: 0x002A7BCB File Offset: 0x002A5DCB
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.dialogsheet+xml";
			}
		}

		// Token: 0x1700348A RID: 13450
		// (get) Token: 0x0600D682 RID: 54914 RVA: 0x002A7BD2 File Offset: 0x002A5DD2
		internal sealed override string TargetPath
		{
			get
			{
				return "dialogsheets";
			}
		}

		// Token: 0x1700348B RID: 13451
		// (get) Token: 0x0600D683 RID: 54915 RVA: 0x002A77F6 File Offset: 0x002A59F6
		internal sealed override string TargetName
		{
			get
			{
				return "sheet";
			}
		}

		// Token: 0x1700348C RID: 13452
		// (get) Token: 0x0600D684 RID: 54916 RVA: 0x002A77FD File Offset: 0x002A59FD
		public IEnumerable<SpreadsheetPrinterSettingsPart> SpreadsheetPrinterSettingsParts
		{
			get
			{
				return base.GetPartsOfType<SpreadsheetPrinterSettingsPart>();
			}
		}

		// Token: 0x1700348D RID: 13453
		// (get) Token: 0x0600D685 RID: 54917 RVA: 0x002A7805 File Offset: 0x002A5A05
		public DrawingsPart DrawingsPart
		{
			get
			{
				return base.GetSubPartOfType<DrawingsPart>();
			}
		}

		// Token: 0x1700348E RID: 13454
		// (get) Token: 0x0600D686 RID: 54918 RVA: 0x002A780D File Offset: 0x002A5A0D
		public IEnumerable<VmlDrawingPart> VmlDrawingParts
		{
			get
			{
				return base.GetPartsOfType<VmlDrawingPart>();
			}
		}

		// Token: 0x1700348F RID: 13455
		// (get) Token: 0x0600D687 RID: 54919 RVA: 0x002A3FF1 File Offset: 0x002A21F1
		public IEnumerable<EmbeddedObjectPart> EmbeddedObjectParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedObjectPart>();
			}
		}

		// Token: 0x17003490 RID: 13456
		// (get) Token: 0x0600D688 RID: 54920 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003491 RID: 13457
		// (get) Token: 0x0600D689 RID: 54921 RVA: 0x002A7BD9 File Offset: 0x002A5DD9
		// (set) Token: 0x0600D68A RID: 54922 RVA: 0x002A7BE1 File Offset: 0x002A5DE1
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as DialogSheet;
			}
		}

		// Token: 0x17003492 RID: 13458
		// (get) Token: 0x0600D68B RID: 54923 RVA: 0x002A7BEF File Offset: 0x002A5DEF
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.DialogSheet;
			}
		}

		// Token: 0x17003493 RID: 13459
		// (get) Token: 0x0600D68C RID: 54924 RVA: 0x002A7BF7 File Offset: 0x002A5DF7
		// (set) Token: 0x0600D68D RID: 54925 RVA: 0x002A3296 File Offset: 0x002A1496
		public DialogSheet DialogSheet
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<DialogSheet>();
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

		// Token: 0x04006A7A RID: 27258
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/dialogsheet";

		// Token: 0x04006A7B RID: 27259
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.dialogsheet+xml";

		// Token: 0x04006A7C RID: 27260
		internal const string TargetPathConstant = "dialogsheets";

		// Token: 0x04006A7D RID: 27261
		internal const string TargetNameConstant = "sheet";

		// Token: 0x04006A7E RID: 27262
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A7F RID: 27263
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A80 RID: 27264
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DialogSheet _rootEle;
	}
}
