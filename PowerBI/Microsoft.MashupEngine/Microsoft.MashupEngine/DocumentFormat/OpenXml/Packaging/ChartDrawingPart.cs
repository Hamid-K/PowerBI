using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002194 RID: 8596
	internal class ChartDrawingPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D98D RID: 55693 RVA: 0x002ACA4C File Offset: 0x002AAC4C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ChartDrawingPart._partConstraint == null)
			{
				ChartDrawingPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart",
						new PartConstraintRule("ChartPart", "application/vnd.openxmlformats-officedocument.drawingml.chart+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return ChartDrawingPart._partConstraint;
		}

		// Token: 0x0600D98E RID: 55694 RVA: 0x002ACAA8 File Offset: 0x002AACA8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ChartDrawingPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ChartDrawingPart._dataPartReferenceConstraint = dictionary;
			}
			return ChartDrawingPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D98F RID: 55695 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ChartDrawingPart()
		{
		}

		// Token: 0x0600D990 RID: 55696 RVA: 0x002ACAD0 File Offset: 0x002AACD0
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chart")
				{
					return new ChartPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D991 RID: 55697 RVA: 0x002ACB28 File Offset: 0x002AAD28
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D992 RID: 55698 RVA: 0x002ACB44 File Offset: 0x002AAD44
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D993 RID: 55699 RVA: 0x002ACB78 File Offset: 0x002AAD78
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D994 RID: 55700 RVA: 0x002ACB98 File Offset: 0x002AAD98
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003638 RID: 13880
		// (get) Token: 0x0600D995 RID: 55701 RVA: 0x002ACBCD File Offset: 0x002AADCD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes";
			}
		}

		// Token: 0x17003639 RID: 13881
		// (get) Token: 0x0600D996 RID: 55702 RVA: 0x002ACBD4 File Offset: 0x002AADD4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.chartshapes+xml";
			}
		}

		// Token: 0x1700363A RID: 13882
		// (get) Token: 0x0600D997 RID: 55703 RVA: 0x002A7FAF File Offset: 0x002A61AF
		internal sealed override string TargetPath
		{
			get
			{
				return "../drawings";
			}
		}

		// Token: 0x1700363B RID: 13883
		// (get) Token: 0x0600D998 RID: 55704 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		internal sealed override string TargetName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x1700363C RID: 13884
		// (get) Token: 0x0600D999 RID: 55705 RVA: 0x002ACBDB File Offset: 0x002AADDB
		public ChartPart ChartPart
		{
			get
			{
				return base.GetSubPartOfType<ChartPart>();
			}
		}

		// Token: 0x1700363D RID: 13885
		// (get) Token: 0x0600D99A RID: 55706 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700363E RID: 13886
		// (get) Token: 0x0600D99B RID: 55707 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700363F RID: 13887
		// (get) Token: 0x0600D99C RID: 55708 RVA: 0x002ACBE3 File Offset: 0x002AADE3
		// (set) Token: 0x0600D99D RID: 55709 RVA: 0x002ACBEB File Offset: 0x002AADEB
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as UserShapes;
			}
		}

		// Token: 0x17003640 RID: 13888
		// (get) Token: 0x0600D99E RID: 55710 RVA: 0x002ACBF9 File Offset: 0x002AADF9
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.UserShapes;
			}
		}

		// Token: 0x17003641 RID: 13889
		// (get) Token: 0x0600D99F RID: 55711 RVA: 0x002ACC01 File Offset: 0x002AAE01
		// (set) Token: 0x0600D9A0 RID: 55712 RVA: 0x002A3296 File Offset: 0x002A1496
		public UserShapes UserShapes
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<UserShapes>();
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

		// Token: 0x04006B75 RID: 27509
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/chartUserShapes";

		// Token: 0x04006B76 RID: 27510
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.chartshapes+xml";

		// Token: 0x04006B77 RID: 27511
		internal const string TargetPathConstant = "../drawings";

		// Token: 0x04006B78 RID: 27512
		internal const string TargetNameConstant = "drawing";

		// Token: 0x04006B79 RID: 27513
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B7A RID: 27514
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B7B RID: 27515
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UserShapes _rootEle;
	}
}
