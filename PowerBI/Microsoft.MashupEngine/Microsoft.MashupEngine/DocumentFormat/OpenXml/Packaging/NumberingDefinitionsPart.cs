using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002164 RID: 8548
	internal class NumberingDefinitionsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D5C0 RID: 54720 RVA: 0x002A6974 File Offset: 0x002A4B74
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (NumberingDefinitionsPart._partConstraint == null)
			{
				NumberingDefinitionsPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return NumberingDefinitionsPart._partConstraint;
		}

		// Token: 0x0600D5C1 RID: 54721 RVA: 0x002A69B4 File Offset: 0x002A4BB4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (NumberingDefinitionsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				NumberingDefinitionsPart._dataPartReferenceConstraint = dictionary;
			}
			return NumberingDefinitionsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D5C2 RID: 54722 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal NumberingDefinitionsPart()
		{
		}

		// Token: 0x0600D5C3 RID: 54723 RVA: 0x002A69DC File Offset: 0x002A4BDC
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

		// Token: 0x0600D5C4 RID: 54724 RVA: 0x002A6A20 File Offset: 0x002A4C20
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D5C5 RID: 54725 RVA: 0x002A6A3C File Offset: 0x002A4C3C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D5C6 RID: 54726 RVA: 0x002A6A70 File Offset: 0x002A4C70
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D5C7 RID: 54727 RVA: 0x002A6A90 File Offset: 0x002A4C90
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003418 RID: 13336
		// (get) Token: 0x0600D5C8 RID: 54728 RVA: 0x002A6AC5 File Offset: 0x002A4CC5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering";
			}
		}

		// Token: 0x17003419 RID: 13337
		// (get) Token: 0x0600D5C9 RID: 54729 RVA: 0x002A6ACC File Offset: 0x002A4CCC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.numbering+xml";
			}
		}

		// Token: 0x1700341A RID: 13338
		// (get) Token: 0x0600D5CA RID: 54730 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700341B RID: 13339
		// (get) Token: 0x0600D5CB RID: 54731 RVA: 0x002A6AD3 File Offset: 0x002A4CD3
		internal sealed override string TargetName
		{
			get
			{
				return "numbering";
			}
		}

		// Token: 0x1700341C RID: 13340
		// (get) Token: 0x0600D5CC RID: 54732 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700341D RID: 13341
		// (get) Token: 0x0600D5CD RID: 54733 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700341E RID: 13342
		// (get) Token: 0x0600D5CE RID: 54734 RVA: 0x002A6ADA File Offset: 0x002A4CDA
		// (set) Token: 0x0600D5CF RID: 54735 RVA: 0x002A6AE2 File Offset: 0x002A4CE2
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Numbering;
			}
		}

		// Token: 0x1700341F RID: 13343
		// (get) Token: 0x0600D5D0 RID: 54736 RVA: 0x002A6AF0 File Offset: 0x002A4CF0
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Numbering;
			}
		}

		// Token: 0x17003420 RID: 13344
		// (get) Token: 0x0600D5D1 RID: 54737 RVA: 0x002A6AF8 File Offset: 0x002A4CF8
		// (set) Token: 0x0600D5D2 RID: 54738 RVA: 0x002A3296 File Offset: 0x002A1496
		public Numbering Numbering
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Numbering>();
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

		// Token: 0x04006A30 RID: 27184
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering";

		// Token: 0x04006A31 RID: 27185
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.numbering+xml";

		// Token: 0x04006A32 RID: 27186
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A33 RID: 27187
		internal const string TargetNameConstant = "numbering";

		// Token: 0x04006A34 RID: 27188
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A35 RID: 27189
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A36 RID: 27190
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Numbering _rootEle;
	}
}
