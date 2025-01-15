using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002196 RID: 8598
	internal class DiagramDataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9AE RID: 55726 RVA: 0x002ACCB8 File Offset: 0x002AAEB8
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DiagramDataPart._partConstraint == null)
			{
				DiagramDataPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
						new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet",
						new PartConstraintRule("WorksheetPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return DiagramDataPart._partConstraint;
		}

		// Token: 0x0600D9AF RID: 55727 RVA: 0x002ACD30 File Offset: 0x002AAF30
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DiagramDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramDataPart._dataPartReferenceConstraint = dictionary;
			}
			return DiagramDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9B0 RID: 55728 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DiagramDataPart()
		{
		}

		// Token: 0x0600D9B1 RID: 55729 RVA: 0x002ACD58 File Offset: 0x002AAF58
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide")
				{
					return new SlidePart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet")
				{
					return new WorksheetPart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D9B2 RID: 55730 RVA: 0x002ACDC4 File Offset: 0x002AAFC4
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D9B3 RID: 55731 RVA: 0x002ACDE0 File Offset: 0x002AAFE0
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D9B4 RID: 55732 RVA: 0x002ACE14 File Offset: 0x002AB014
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D9B5 RID: 55733 RVA: 0x002ACE34 File Offset: 0x002AB034
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x1700364A RID: 13898
		// (get) Token: 0x0600D9B6 RID: 55734 RVA: 0x002ACE69 File Offset: 0x002AB069
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData";
			}
		}

		// Token: 0x1700364B RID: 13899
		// (get) Token: 0x0600D9B7 RID: 55735 RVA: 0x002ACE70 File Offset: 0x002AB070
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml";
			}
		}

		// Token: 0x1700364C RID: 13900
		// (get) Token: 0x0600D9B8 RID: 55736 RVA: 0x002ACC73 File Offset: 0x002AAE73
		internal sealed override string TargetPath
		{
			get
			{
				return "../graphics";
			}
		}

		// Token: 0x1700364D RID: 13901
		// (get) Token: 0x0600D9B9 RID: 55737 RVA: 0x002958E1 File Offset: 0x00293AE1
		internal sealed override string TargetName
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x1700364E RID: 13902
		// (get) Token: 0x0600D9BA RID: 55738 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700364F RID: 13903
		// (get) Token: 0x0600D9BB RID: 55739 RVA: 0x002A9797 File Offset: 0x002A7997
		public IEnumerable<SlidePart> SlideParts
		{
			get
			{
				return base.GetPartsOfType<SlidePart>();
			}
		}

		// Token: 0x17003650 RID: 13904
		// (get) Token: 0x0600D9BC RID: 55740 RVA: 0x002A74FF File Offset: 0x002A56FF
		public IEnumerable<WorksheetPart> WorksheetParts
		{
			get
			{
				return base.GetPartsOfType<WorksheetPart>();
			}
		}

		// Token: 0x17003651 RID: 13905
		// (get) Token: 0x0600D9BD RID: 55741 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003652 RID: 13906
		// (get) Token: 0x0600D9BE RID: 55742 RVA: 0x002ACE77 File Offset: 0x002AB077
		// (set) Token: 0x0600D9BF RID: 55743 RVA: 0x002ACE7F File Offset: 0x002AB07F
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as DataModelRoot;
			}
		}

		// Token: 0x17003653 RID: 13907
		// (get) Token: 0x0600D9C0 RID: 55744 RVA: 0x002ACE8D File Offset: 0x002AB08D
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.DataModelRoot;
			}
		}

		// Token: 0x17003654 RID: 13908
		// (get) Token: 0x0600D9C1 RID: 55745 RVA: 0x002ACE95 File Offset: 0x002AB095
		// (set) Token: 0x0600D9C2 RID: 55746 RVA: 0x002A3296 File Offset: 0x002A1496
		public DataModelRoot DataModelRoot
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<DataModelRoot>();
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

		// Token: 0x04006B83 RID: 27523
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramData";

		// Token: 0x04006B84 RID: 27524
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.diagramData+xml";

		// Token: 0x04006B85 RID: 27525
		internal const string TargetPathConstant = "../graphics";

		// Token: 0x04006B86 RID: 27526
		internal const string TargetNameConstant = "data";

		// Token: 0x04006B87 RID: 27527
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B88 RID: 27528
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B89 RID: 27529
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DataModelRoot _rootEle;
	}
}
