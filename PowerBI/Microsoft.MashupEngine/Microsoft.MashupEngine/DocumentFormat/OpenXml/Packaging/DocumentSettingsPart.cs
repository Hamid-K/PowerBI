using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215D RID: 8541
	internal class DocumentSettingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D4BA RID: 54458 RVA: 0x002A4770 File Offset: 0x002A2970
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DocumentSettingsPart._partConstraint == null)
			{
				DocumentSettingsPart._partConstraint = new Dictionary<string, PartConstraintRule>
				{
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData",
						new PartConstraintRule("MailMergeRecipientDataPart", null, false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					},
					{
						"http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
						new PartConstraintRule("ImagePart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
					}
				};
			}
			return DocumentSettingsPart._partConstraint;
		}

		// Token: 0x0600D4BB RID: 54459 RVA: 0x002A47C8 File Offset: 0x002A29C8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DocumentSettingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DocumentSettingsPart._dataPartReferenceConstraint = dictionary;
			}
			return DocumentSettingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D4BC RID: 54460 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DocumentSettingsPart()
		{
		}

		// Token: 0x0600D4BD RID: 54461 RVA: 0x002A47F0 File Offset: 0x002A29F0
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null)
			{
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/recipientData")
				{
					return new MailMergeRecipientDataPart();
				}
				if (relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image")
				{
					return new ImagePart();
				}
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D4BE RID: 54462 RVA: 0x002A4848 File Offset: 0x002A2A48
		public MailMergeRecipientDataPart AddMailMergeRecipientDataPart(string contentType)
		{
			MailMergeRecipientDataPart mailMergeRecipientDataPart = new MailMergeRecipientDataPart();
			base.InitPart<MailMergeRecipientDataPart>(mailMergeRecipientDataPart, contentType);
			return mailMergeRecipientDataPart;
		}

		// Token: 0x0600D4BF RID: 54463 RVA: 0x002A4864 File Offset: 0x002A2A64
		public MailMergeRecipientDataPart AddMailMergeRecipientDataPart(MailMergeRecipientDataPartType partType)
		{
			string contentType = MailMergeRecipientDataPartTypeInfo.GetContentType(partType);
			string targetExtension = MailMergeRecipientDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddMailMergeRecipientDataPart(contentType);
		}

		// Token: 0x0600D4C0 RID: 54464 RVA: 0x002A4898 File Offset: 0x002A2A98
		public MailMergeRecipientDataPart AddMailMergeRecipientDataPart(string contentType, string id)
		{
			MailMergeRecipientDataPart mailMergeRecipientDataPart = new MailMergeRecipientDataPart();
			this.InitPart<MailMergeRecipientDataPart>(mailMergeRecipientDataPart, contentType, id);
			return mailMergeRecipientDataPart;
		}

		// Token: 0x0600D4C1 RID: 54465 RVA: 0x002A48B8 File Offset: 0x002A2AB8
		public MailMergeRecipientDataPart AddMailMergeRecipientDataPart(MailMergeRecipientDataPartType partType, string id)
		{
			string contentType = MailMergeRecipientDataPartTypeInfo.GetContentType(partType);
			string targetExtension = MailMergeRecipientDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddMailMergeRecipientDataPart(contentType, id);
		}

		// Token: 0x0600D4C2 RID: 54466 RVA: 0x002A48F0 File Offset: 0x002A2AF0
		public ImagePart AddImagePart(string contentType)
		{
			ImagePart imagePart = new ImagePart();
			base.InitPart<ImagePart>(imagePart, contentType);
			return imagePart;
		}

		// Token: 0x0600D4C3 RID: 54467 RVA: 0x002A490C File Offset: 0x002A2B0C
		public ImagePart AddImagePart(ImagePartType partType)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType);
		}

		// Token: 0x0600D4C4 RID: 54468 RVA: 0x002A4940 File Offset: 0x002A2B40
		public ImagePart AddImagePart(string contentType, string id)
		{
			ImagePart imagePart = new ImagePart();
			this.InitPart<ImagePart>(imagePart, contentType, id);
			return imagePart;
		}

		// Token: 0x0600D4C5 RID: 54469 RVA: 0x002A4960 File Offset: 0x002A2B60
		public ImagePart AddImagePart(ImagePartType partType, string id)
		{
			string contentType = ImagePartTypeInfo.GetContentType(partType);
			string targetExtension = ImagePartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddImagePart(contentType, id);
		}

		// Token: 0x17003398 RID: 13208
		// (get) Token: 0x0600D4C6 RID: 54470 RVA: 0x002A4995 File Offset: 0x002A2B95
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings";
			}
		}

		// Token: 0x17003399 RID: 13209
		// (get) Token: 0x0600D4C7 RID: 54471 RVA: 0x002A499C File Offset: 0x002A2B9C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml";
			}
		}

		// Token: 0x1700339A RID: 13210
		// (get) Token: 0x0600D4C8 RID: 54472 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700339B RID: 13211
		// (get) Token: 0x0600D4C9 RID: 54473 RVA: 0x002A49A3 File Offset: 0x002A2BA3
		internal sealed override string TargetName
		{
			get
			{
				return "settings";
			}
		}

		// Token: 0x1700339C RID: 13212
		// (get) Token: 0x0600D4CA RID: 54474 RVA: 0x002A49AA File Offset: 0x002A2BAA
		public MailMergeRecipientDataPart MailMergeRecipientDataPart
		{
			get
			{
				return base.GetSubPartOfType<MailMergeRecipientDataPart>();
			}
		}

		// Token: 0x1700339D RID: 13213
		// (get) Token: 0x0600D4CB RID: 54475 RVA: 0x002A4001 File Offset: 0x002A2201
		public IEnumerable<ImagePart> ImageParts
		{
			get
			{
				return base.GetPartsOfType<ImagePart>();
			}
		}

		// Token: 0x1700339E RID: 13214
		// (get) Token: 0x0600D4CC RID: 54476 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700339F RID: 13215
		// (get) Token: 0x0600D4CD RID: 54477 RVA: 0x002A49B2 File Offset: 0x002A2BB2
		// (set) Token: 0x0600D4CE RID: 54478 RVA: 0x002A49BA File Offset: 0x002A2BBA
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Settings;
			}
		}

		// Token: 0x170033A0 RID: 13216
		// (get) Token: 0x0600D4CF RID: 54479 RVA: 0x002A49C8 File Offset: 0x002A2BC8
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Settings;
			}
		}

		// Token: 0x170033A1 RID: 13217
		// (get) Token: 0x0600D4D0 RID: 54480 RVA: 0x002A49D0 File Offset: 0x002A2BD0
		// (set) Token: 0x0600D4D1 RID: 54481 RVA: 0x002A3296 File Offset: 0x002A1496
		public Settings Settings
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Settings>();
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

		// Token: 0x040069FF RID: 27135
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings";

		// Token: 0x04006A00 RID: 27136
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml";

		// Token: 0x04006A01 RID: 27137
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A02 RID: 27138
		internal const string TargetNameConstant = "settings";

		// Token: 0x04006A03 RID: 27139
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A04 RID: 27140
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A05 RID: 27141
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Settings _rootEle;
	}
}
