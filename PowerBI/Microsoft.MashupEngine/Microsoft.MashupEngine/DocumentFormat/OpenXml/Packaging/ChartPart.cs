using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002193 RID: 8595
	internal class ChartPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D976 RID: 55670 RVA: 0x002AC7EC File Offset: 0x002AA9EC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ChartPart._partConstraint == null)
			{
				ChartPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes",
						new PartConstraintRule("ChartDrawingPart", "application/vnd.openxmlformats-officedocument.drawingml.chartshapes+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/package",
						new PartConstraintRule("EmbeddedPackagePart", null, false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride",
						new PartConstraintRule("ThemeOverridePart", "application/vnd.openxmlformats-officedocument.themeOverride+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return ChartPart._partConstraint;
		}

		// Token: 0x0600D977 RID: 55671 RVA: 0x002AC880 File Offset: 0x002AAA80
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ChartPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ChartPart._dataPartReferenceConstraint = dictionary;
			}
			return ChartPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D978 RID: 55672 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ChartPart()
		{
		}

		// Token: 0x0600D979 RID: 55673 RVA: 0x002AC8A8 File Offset: 0x002AAAA8
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes")
				{
					return new ChartDrawingPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package")
				{
					return new EmbeddedPackagePart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/themeOverride")
				{
					return new ThemeOverridePart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D97A RID: 55674 RVA: 0x002AC928 File Offset: 0x002AAB28
		public EmbeddedPackagePart AddEmbeddedPackagePart(string contentType)
		{
			EmbeddedPackagePart embeddedPackagePart = new EmbeddedPackagePart();
			base.InitPart<EmbeddedPackagePart>(embeddedPackagePart, contentType);
			return embeddedPackagePart;
		}

		// Token: 0x0600D97B RID: 55675 RVA: 0x002AC944 File Offset: 0x002AAB44
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D97C RID: 55676 RVA: 0x002AC960 File Offset: 0x002AAB60
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D97D RID: 55677 RVA: 0x002AC994 File Offset: 0x002AAB94
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D97E RID: 55678 RVA: 0x002AC9B4 File Offset: 0x002AABB4
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x1700362C RID: 13868
		// (get) Token: 0x0600D97F RID: 55679 RVA: 0x002AC9E9 File Offset: 0x002AABE9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart";
			}
		}

		// Token: 0x1700362D RID: 13869
		// (get) Token: 0x0600D980 RID: 55680 RVA: 0x002AC9F0 File Offset: 0x002AABF0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.chart+xml";
			}
		}

		// Token: 0x1700362E RID: 13870
		// (get) Token: 0x0600D981 RID: 55681 RVA: 0x002AC9F7 File Offset: 0x002AABF7
		internal sealed override string TargetPath
		{
			get
			{
				return "charts";
			}
		}

		// Token: 0x1700362F RID: 13871
		// (get) Token: 0x0600D982 RID: 55682 RVA: 0x002AC9FE File Offset: 0x002AABFE
		internal sealed override string TargetName
		{
			get
			{
				return "chart";
			}
		}

		// Token: 0x17003630 RID: 13872
		// (get) Token: 0x0600D983 RID: 55683 RVA: 0x002ACA05 File Offset: 0x002AAC05
		public ChartDrawingPart ChartDrawingPart
		{
			get
			{
				return base.GetSubPartOfType<ChartDrawingPart>();
			}
		}

		// Token: 0x17003631 RID: 13873
		// (get) Token: 0x0600D984 RID: 55684 RVA: 0x002ACA0D File Offset: 0x002AAC0D
		public EmbeddedPackagePart EmbeddedPackagePart
		{
			get
			{
				return base.GetSubPartOfType<EmbeddedPackagePart>();
			}
		}

		// Token: 0x17003632 RID: 13874
		// (get) Token: 0x0600D985 RID: 55685 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x17003633 RID: 13875
		// (get) Token: 0x0600D986 RID: 55686 RVA: 0x002AAC61 File Offset: 0x002A8E61
		public ThemeOverridePart ThemeOverridePart
		{
			get
			{
				return base.GetSubPartOfType<ThemeOverridePart>();
			}
		}

		// Token: 0x17003634 RID: 13876
		// (get) Token: 0x0600D987 RID: 55687 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003635 RID: 13877
		// (get) Token: 0x0600D988 RID: 55688 RVA: 0x002ACA15 File Offset: 0x002AAC15
		// (set) Token: 0x0600D989 RID: 55689 RVA: 0x002ACA1D File Offset: 0x002AAC1D
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as ChartSpace;
			}
		}

		// Token: 0x17003636 RID: 13878
		// (get) Token: 0x0600D98A RID: 55690 RVA: 0x002ACA2B File Offset: 0x002AAC2B
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.ChartSpace;
			}
		}

		// Token: 0x17003637 RID: 13879
		// (get) Token: 0x0600D98B RID: 55691 RVA: 0x002ACA33 File Offset: 0x002AAC33
		// (set) Token: 0x0600D98C RID: 55692 RVA: 0x002A3296 File Offset: 0x002A1496
		public ChartSpace ChartSpace
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<ChartSpace>();
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

		// Token: 0x04006B6E RID: 27502
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart";

		// Token: 0x04006B6F RID: 27503
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.chart+xml";

		// Token: 0x04006B70 RID: 27504
		internal const string TargetPathConstant = "charts";

		// Token: 0x04006B71 RID: 27505
		internal const string TargetNameConstant = "chart";

		// Token: 0x04006B72 RID: 27506
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B73 RID: 27507
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B74 RID: 27508
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ChartSpace _rootEle;
	}
}
