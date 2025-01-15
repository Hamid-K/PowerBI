using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002199 RID: 8601
	internal class ThemePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9E3 RID: 55779 RVA: 0x002AD0E0 File Offset: 0x002AB2E0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ThemePart._partConstraint == null)
			{
				ThemePart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return ThemePart._partConstraint;
		}

		// Token: 0x0600D9E4 RID: 55780 RVA: 0x002AD120 File Offset: 0x002AB320
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ThemePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ThemePart._dataPartReferenceConstraint = dictionary;
			}
			return ThemePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9E5 RID: 55781 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ThemePart()
		{
		}

		// Token: 0x0600D9E6 RID: 55782 RVA: 0x002AD148 File Offset: 0x002AB348
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

		// Token: 0x0600D9E7 RID: 55783 RVA: 0x002AD18C File Offset: 0x002AB38C
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D9E8 RID: 55784 RVA: 0x002AD1A8 File Offset: 0x002AB3A8
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D9E9 RID: 55785 RVA: 0x002AD1DC File Offset: 0x002AB3DC
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D9EA RID: 55786 RVA: 0x002AD1FC File Offset: 0x002AB3FC
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003666 RID: 13926
		// (get) Token: 0x0600D9EB RID: 55787 RVA: 0x002AD231 File Offset: 0x002AB431
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme";
			}
		}

		// Token: 0x17003667 RID: 13927
		// (get) Token: 0x0600D9EC RID: 55788 RVA: 0x002AD238 File Offset: 0x002AB438
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.theme+xml";
			}
		}

		// Token: 0x17003668 RID: 13928
		// (get) Token: 0x0600D9ED RID: 55789 RVA: 0x002AD23F File Offset: 0x002AB43F
		internal sealed override string TargetPath
		{
			get
			{
				return "theme";
			}
		}

		// Token: 0x17003669 RID: 13929
		// (get) Token: 0x0600D9EE RID: 55790 RVA: 0x002AD23F File Offset: 0x002AB43F
		internal sealed override string TargetName
		{
			get
			{
				return "theme";
			}
		}

		// Token: 0x1700366A RID: 13930
		// (get) Token: 0x0600D9EF RID: 55791 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700366B RID: 13931
		// (get) Token: 0x0600D9F0 RID: 55792 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700366C RID: 13932
		// (get) Token: 0x0600D9F1 RID: 55793 RVA: 0x002AD246 File Offset: 0x002AB446
		// (set) Token: 0x0600D9F2 RID: 55794 RVA: 0x002AD24E File Offset: 0x002AB44E
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Theme;
			}
		}

		// Token: 0x1700366D RID: 13933
		// (get) Token: 0x0600D9F3 RID: 55795 RVA: 0x002AD25C File Offset: 0x002AB45C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Theme;
			}
		}

		// Token: 0x1700366E RID: 13934
		// (get) Token: 0x0600D9F4 RID: 55796 RVA: 0x002AD264 File Offset: 0x002AB464
		// (set) Token: 0x0600D9F5 RID: 55797 RVA: 0x002A3296 File Offset: 0x002A1496
		public Theme Theme
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Theme>();
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

		// Token: 0x04006B98 RID: 27544
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme";

		// Token: 0x04006B99 RID: 27545
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.theme+xml";

		// Token: 0x04006B9A RID: 27546
		internal const string TargetPathConstant = "theme";

		// Token: 0x04006B9B RID: 27547
		internal const string TargetNameConstant = "theme";

		// Token: 0x04006B9C RID: 27548
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B9D RID: 27549
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B9E RID: 27550
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Theme _rootEle;
	}
}
