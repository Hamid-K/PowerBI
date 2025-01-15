using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.Drawing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AE RID: 8622
	internal class DiagramPersistLayoutPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DAF2 RID: 56050 RVA: 0x002AE700 File Offset: 0x002AC900
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DiagramPersistLayoutPart._partConstraint == null)
			{
				DiagramPersistLayoutPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return DiagramPersistLayoutPart._partConstraint;
		}

		// Token: 0x0600DAF3 RID: 56051 RVA: 0x002AE740 File Offset: 0x002AC940
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DiagramPersistLayoutPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramPersistLayoutPart._dataPartReferenceConstraint = dictionary;
			}
			return DiagramPersistLayoutPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DAF4 RID: 56052 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DiagramPersistLayoutPart()
		{
		}

		// Token: 0x0600DAF5 RID: 56053 RVA: 0x002AE768 File Offset: 0x002AC968
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
			{
				return new ImagePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600DAF6 RID: 56054 RVA: 0x002AE7AC File Offset: 0x002AC9AC
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600DAF7 RID: 56055 RVA: 0x002AE7C8 File Offset: 0x002AC9C8
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600DAF8 RID: 56056 RVA: 0x002AE7FC File Offset: 0x002AC9FC
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600DAF9 RID: 56057 RVA: 0x002AE81C File Offset: 0x002ACA1C
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x170036F9 RID: 14073
		// (get) Token: 0x0600DAFA RID: 56058 RVA: 0x002AE851 File Offset: 0x002ACA51
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing";
			}
		}

		// Token: 0x170036FA RID: 14074
		// (get) Token: 0x0600DAFB RID: 56059 RVA: 0x002AE858 File Offset: 0x002ACA58
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-office.drawingml.diagramDrawing+xml";
			}
		}

		// Token: 0x170036FB RID: 14075
		// (get) Token: 0x0600DAFC RID: 56060 RVA: 0x002AE85F File Offset: 0x002ACA5F
		internal sealed override string TargetPath
		{
			get
			{
				return "../diagrams";
			}
		}

		// Token: 0x170036FC RID: 14076
		// (get) Token: 0x0600DAFD RID: 56061 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		internal sealed override string TargetName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x170036FD RID: 14077
		// (get) Token: 0x0600DAFE RID: 56062 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x170036FE RID: 14078
		// (get) Token: 0x0600DAFF RID: 56063 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170036FF RID: 14079
		// (get) Token: 0x0600DB00 RID: 56064 RVA: 0x002AE866 File Offset: 0x002ACA66
		// (set) Token: 0x0600DB01 RID: 56065 RVA: 0x002AE86E File Offset: 0x002ACA6E
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Drawing;
			}
		}

		// Token: 0x17003700 RID: 14080
		// (get) Token: 0x0600DB02 RID: 56066 RVA: 0x002AE87C File Offset: 0x002ACA7C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Drawing;
			}
		}

		// Token: 0x17003701 RID: 14081
		// (get) Token: 0x0600DB03 RID: 56067 RVA: 0x002AE884 File Offset: 0x002ACA84
		// (set) Token: 0x0600DB04 RID: 56068 RVA: 0x002A3296 File Offset: 0x002A1496
		public Drawing Drawing
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Drawing>();
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

		// Token: 0x04006C19 RID: 27673
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/diagramDrawing";

		// Token: 0x04006C1A RID: 27674
		internal const string ContentTypeConstant = "application/vnd.ms-office.drawingml.diagramDrawing+xml";

		// Token: 0x04006C1B RID: 27675
		internal const string TargetPathConstant = "../diagrams";

		// Token: 0x04006C1C RID: 27676
		internal const string TargetNameConstant = "drawing";

		// Token: 0x04006C1D RID: 27677
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C1E RID: 27678
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C1F RID: 27679
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Drawing _rootEle;
	}
}
