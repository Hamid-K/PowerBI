using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219A RID: 8602
	internal class ThemeOverridePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9F6 RID: 55798 RVA: 0x002AD27C File Offset: 0x002AB47C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ThemeOverridePart._partConstraint == null)
			{
				ThemeOverridePart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
					new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return ThemeOverridePart._partConstraint;
		}

		// Token: 0x0600D9F7 RID: 55799 RVA: 0x002AD2BC File Offset: 0x002AB4BC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ThemeOverridePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ThemeOverridePart._dataPartReferenceConstraint = dictionary;
			}
			return ThemeOverridePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9F8 RID: 55800 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ThemeOverridePart()
		{
		}

		// Token: 0x0600D9F9 RID: 55801 RVA: 0x002AD2E4 File Offset: 0x002AB4E4
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

		// Token: 0x0600D9FA RID: 55802 RVA: 0x002AD328 File Offset: 0x002AB528
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D9FB RID: 55803 RVA: 0x002AD344 File Offset: 0x002AB544
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D9FC RID: 55804 RVA: 0x002AD378 File Offset: 0x002AB578
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D9FD RID: 55805 RVA: 0x002AD398 File Offset: 0x002AB598
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x1700366F RID: 13935
		// (get) Token: 0x0600D9FE RID: 55806 RVA: 0x002AD3CD File Offset: 0x002AB5CD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride";
			}
		}

		// Token: 0x17003670 RID: 13936
		// (get) Token: 0x0600D9FF RID: 55807 RVA: 0x002AD3D4 File Offset: 0x002AB5D4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.themeOverride+xml";
			}
		}

		// Token: 0x17003671 RID: 13937
		// (get) Token: 0x0600DA00 RID: 55808 RVA: 0x002AD23F File Offset: 0x002AB43F
		internal sealed override string TargetPath
		{
			get
			{
				return "theme";
			}
		}

		// Token: 0x17003672 RID: 13938
		// (get) Token: 0x0600DA01 RID: 55809 RVA: 0x002AD3DB File Offset: 0x002AB5DB
		internal sealed override string TargetName
		{
			get
			{
				return "themeoverride";
			}
		}

		// Token: 0x17003673 RID: 13939
		// (get) Token: 0x0600DA02 RID: 55810 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003674 RID: 13940
		// (get) Token: 0x0600DA03 RID: 55811 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003675 RID: 13941
		// (get) Token: 0x0600DA04 RID: 55812 RVA: 0x002AD3E2 File Offset: 0x002AB5E2
		// (set) Token: 0x0600DA05 RID: 55813 RVA: 0x002AD3EA File Offset: 0x002AB5EA
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as ThemeOverride;
			}
		}

		// Token: 0x17003676 RID: 13942
		// (get) Token: 0x0600DA06 RID: 55814 RVA: 0x002AD3F8 File Offset: 0x002AB5F8
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.ThemeOverride;
			}
		}

		// Token: 0x17003677 RID: 13943
		// (get) Token: 0x0600DA07 RID: 55815 RVA: 0x002AD400 File Offset: 0x002AB600
		// (set) Token: 0x0600DA08 RID: 55816 RVA: 0x002A3296 File Offset: 0x002A1496
		public ThemeOverride ThemeOverride
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<ThemeOverride>();
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

		// Token: 0x04006B9F RID: 27551
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride";

		// Token: 0x04006BA0 RID: 27552
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.themeOverride+xml";

		// Token: 0x04006BA1 RID: 27553
		internal const string TargetPathConstant = "theme";

		// Token: 0x04006BA2 RID: 27554
		internal const string TargetNameConstant = "themeoverride";

		// Token: 0x04006BA3 RID: 27555
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BA4 RID: 27556
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006BA5 RID: 27557
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ThemeOverride _rootEle;
	}
}
