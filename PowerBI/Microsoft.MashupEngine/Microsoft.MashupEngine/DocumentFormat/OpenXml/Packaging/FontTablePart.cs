using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215F RID: 8543
	internal class FontTablePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D4FB RID: 54523 RVA: 0x002A4F50 File Offset: 0x002A3150
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (FontTablePart._partConstraint == null)
			{
				FontTablePart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/font",
					new PartConstraintRule("FontPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return FontTablePart._partConstraint;
		}

		// Token: 0x0600D4FC RID: 54524 RVA: 0x002A4F90 File Offset: 0x002A3190
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (FontTablePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				FontTablePart._dataPartReferenceConstraint = dictionary;
			}
			return FontTablePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D4FD RID: 54525 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal FontTablePart()
		{
		}

		// Token: 0x0600D4FE RID: 54526 RVA: 0x002A4FB8 File Offset: 0x002A31B8
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font")
			{
				return new FontPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600D4FF RID: 54527 RVA: 0x002A4FFC File Offset: 0x002A31FC
		public FontPart AddFontPart(string contentType)
		{
			FontPart fontPart = new FontPart();
			base.InitPart<FontPart>(fontPart, contentType);
			return fontPart;
		}

		// Token: 0x0600D500 RID: 54528 RVA: 0x002A5018 File Offset: 0x002A3218
		public FontPart AddFontPart(FontPartType partType)
		{
			string contentType = FontPartTypeInfo.GetContentType(partType);
			string targetExtension = FontPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddFontPart(contentType);
		}

		// Token: 0x0600D501 RID: 54529 RVA: 0x002A504C File Offset: 0x002A324C
		public FontPart AddFontPart(string contentType, string id)
		{
			FontPart fontPart = new FontPart();
			this.InitPart<FontPart>(fontPart, contentType, id);
			return fontPart;
		}

		// Token: 0x0600D502 RID: 54530 RVA: 0x002A506C File Offset: 0x002A326C
		public FontPart AddFontPart(FontPartType partType, string id)
		{
			string contentType = FontPartTypeInfo.GetContentType(partType);
			string targetExtension = FontPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddFontPart(contentType, id);
		}

		// Token: 0x170033B5 RID: 13237
		// (get) Token: 0x0600D503 RID: 54531 RVA: 0x002A50A1 File Offset: 0x002A32A1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable";
			}
		}

		// Token: 0x170033B6 RID: 13238
		// (get) Token: 0x0600D504 RID: 54532 RVA: 0x002A50A8 File Offset: 0x002A32A8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml";
			}
		}

		// Token: 0x170033B7 RID: 13239
		// (get) Token: 0x0600D505 RID: 54533 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170033B8 RID: 13240
		// (get) Token: 0x0600D506 RID: 54534 RVA: 0x002A50AF File Offset: 0x002A32AF
		internal sealed override string TargetName
		{
			get
			{
				return "fontTable";
			}
		}

		// Token: 0x170033B9 RID: 13241
		// (get) Token: 0x0600D507 RID: 54535 RVA: 0x002A50B6 File Offset: 0x002A32B6
		public IEnumerable<FontPart> FontParts
		{
			get
			{
				return base.GetPartsOfType<FontPart>();
			}
		}

		// Token: 0x170033BA RID: 13242
		// (get) Token: 0x0600D508 RID: 54536 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170033BB RID: 13243
		// (get) Token: 0x0600D509 RID: 54537 RVA: 0x002A50BE File Offset: 0x002A32BE
		// (set) Token: 0x0600D50A RID: 54538 RVA: 0x002A50C6 File Offset: 0x002A32C6
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Fonts;
			}
		}

		// Token: 0x170033BC RID: 13244
		// (get) Token: 0x0600D50B RID: 54539 RVA: 0x002A50D4 File Offset: 0x002A32D4
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Fonts;
			}
		}

		// Token: 0x170033BD RID: 13245
		// (get) Token: 0x0600D50C RID: 54540 RVA: 0x002A50DC File Offset: 0x002A32DC
		// (set) Token: 0x0600D50D RID: 54541 RVA: 0x002A3296 File Offset: 0x002A1496
		public Fonts Fonts
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Fonts>();
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

		// Token: 0x04006A0D RID: 27149
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable";

		// Token: 0x04006A0E RID: 27150
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml";

		// Token: 0x04006A0F RID: 27151
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A10 RID: 27152
		internal const string TargetNameConstant = "fontTable";

		// Token: 0x04006A11 RID: 27153
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A12 RID: 27154
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A13 RID: 27155
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Fonts _rootEle;
	}
}
