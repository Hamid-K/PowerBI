using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002197 RID: 8599
	internal class DiagramLayoutDefinitionPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9C3 RID: 55747 RVA: 0x002ACEAC File Offset: 0x002AB0AC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DiagramLayoutDefinitionPart._partConstraint == null)
			{
				DiagramLayoutDefinitionPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return DiagramLayoutDefinitionPart._partConstraint;
		}

		// Token: 0x0600D9C4 RID: 55748 RVA: 0x002ACEEC File Offset: 0x002AB0EC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DiagramLayoutDefinitionPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramLayoutDefinitionPart._dataPartReferenceConstraint = dictionary;
			}
			return DiagramLayoutDefinitionPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9C5 RID: 55749 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DiagramLayoutDefinitionPart()
		{
		}

		// Token: 0x0600D9C6 RID: 55750 RVA: 0x002ACF14 File Offset: 0x002AB114
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

		// Token: 0x0600D9C7 RID: 55751 RVA: 0x002ACF58 File Offset: 0x002AB158
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D9C8 RID: 55752 RVA: 0x002ACF74 File Offset: 0x002AB174
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D9C9 RID: 55753 RVA: 0x002ACFA8 File Offset: 0x002AB1A8
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D9CA RID: 55754 RVA: 0x002ACFC8 File Offset: 0x002AB1C8
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003655 RID: 13909
		// (get) Token: 0x0600D9CB RID: 55755 RVA: 0x002ACFFD File Offset: 0x002AB1FD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout";
			}
		}

		// Token: 0x17003656 RID: 13910
		// (get) Token: 0x0600D9CC RID: 55756 RVA: 0x002AD004 File Offset: 0x002AB204
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.diagramLayout+xml";
			}
		}

		// Token: 0x17003657 RID: 13911
		// (get) Token: 0x0600D9CD RID: 55757 RVA: 0x002ACC73 File Offset: 0x002AAE73
		internal sealed override string TargetPath
		{
			get
			{
				return "../graphics";
			}
		}

		// Token: 0x17003658 RID: 13912
		// (get) Token: 0x0600D9CE RID: 55758 RVA: 0x002AD00B File Offset: 0x002AB20B
		internal sealed override string TargetName
		{
			get
			{
				return "layout";
			}
		}

		// Token: 0x17003659 RID: 13913
		// (get) Token: 0x0600D9CF RID: 55759 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700365A RID: 13914
		// (get) Token: 0x0600D9D0 RID: 55760 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700365B RID: 13915
		// (get) Token: 0x0600D9D1 RID: 55761 RVA: 0x002AD012 File Offset: 0x002AB212
		// (set) Token: 0x0600D9D2 RID: 55762 RVA: 0x002AD01A File Offset: 0x002AB21A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as LayoutDefinition;
			}
		}

		// Token: 0x1700365C RID: 13916
		// (get) Token: 0x0600D9D3 RID: 55763 RVA: 0x002AD028 File Offset: 0x002AB228
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.LayoutDefinition;
			}
		}

		// Token: 0x1700365D RID: 13917
		// (get) Token: 0x0600D9D4 RID: 55764 RVA: 0x002AD030 File Offset: 0x002AB230
		// (set) Token: 0x0600D9D5 RID: 55765 RVA: 0x002A3296 File Offset: 0x002A1496
		public LayoutDefinition LayoutDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<LayoutDefinition>();
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

		// Token: 0x04006B8A RID: 27530
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramLayout";

		// Token: 0x04006B8B RID: 27531
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.diagramLayout+xml";

		// Token: 0x04006B8C RID: 27532
		internal const string TargetPathConstant = "../graphics";

		// Token: 0x04006B8D RID: 27533
		internal const string TargetNameConstant = "layout";

		// Token: 0x04006B8E RID: 27534
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B8F RID: 27535
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B90 RID: 27536
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LayoutDefinition _rootEle;
	}
}
