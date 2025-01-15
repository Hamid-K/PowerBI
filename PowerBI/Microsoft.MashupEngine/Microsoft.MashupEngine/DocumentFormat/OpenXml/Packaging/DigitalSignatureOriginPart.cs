using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002156 RID: 8534
	internal class DigitalSignatureOriginPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D41C RID: 54300 RVA: 0x002A33A4 File Offset: 0x002A15A4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DigitalSignatureOriginPart._partConstraint == null)
			{
				DigitalSignatureOriginPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature",
					new PartConstraintRule("XmlSignaturePart", "application/vnd.openxmlformats-package.digital-signature-xmlsignature+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return DigitalSignatureOriginPart._partConstraint;
		}

		// Token: 0x0600D41D RID: 54301 RVA: 0x002A33E8 File Offset: 0x002A15E8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DigitalSignatureOriginPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DigitalSignatureOriginPart._dataPartReferenceConstraint = dictionary;
			}
			return DigitalSignatureOriginPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D41E RID: 54302 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DigitalSignatureOriginPart()
		{
		}

		// Token: 0x0600D41F RID: 54303 RVA: 0x002A3410 File Offset: 0x002A1610
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature")
			{
				return new XmlSignaturePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003341 RID: 13121
		// (get) Token: 0x0600D420 RID: 54304 RVA: 0x002A3453 File Offset: 0x002A1653
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin";
			}
		}

		// Token: 0x17003342 RID: 13122
		// (get) Token: 0x0600D421 RID: 54305 RVA: 0x002A345A File Offset: 0x002A165A
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-package.digital-signature-origin";
			}
		}

		// Token: 0x17003343 RID: 13123
		// (get) Token: 0x0600D422 RID: 54306 RVA: 0x002A3461 File Offset: 0x002A1661
		internal sealed override string TargetPath
		{
			get
			{
				return "_xmlsignatures";
			}
		}

		// Token: 0x17003344 RID: 13124
		// (get) Token: 0x0600D423 RID: 54307 RVA: 0x002A3468 File Offset: 0x002A1668
		internal sealed override string TargetName
		{
			get
			{
				return "origin";
			}
		}

		// Token: 0x17003345 RID: 13125
		// (get) Token: 0x0600D424 RID: 54308 RVA: 0x002A346F File Offset: 0x002A166F
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".sigs";
			}
		}

		// Token: 0x17003346 RID: 13126
		// (get) Token: 0x0600D425 RID: 54309 RVA: 0x002A3476 File Offset: 0x002A1676
		public IEnumerable<XmlSignaturePart> XmlSignatureParts
		{
			get
			{
				return base.GetPartsOfType<XmlSignaturePart>();
			}
		}

		// Token: 0x17003347 RID: 13127
		// (get) Token: 0x0600D426 RID: 54310 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040069D1 RID: 27089
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/origin";

		// Token: 0x040069D2 RID: 27090
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-package.digital-signature-origin";

		// Token: 0x040069D3 RID: 27091
		internal const string TargetPathConstant = "_xmlsignatures";

		// Token: 0x040069D4 RID: 27092
		internal const string TargetNameConstant = "origin";

		// Token: 0x040069D5 RID: 27093
		internal const string TargetFileExtensionConstant = ".sigs";

		// Token: 0x040069D6 RID: 27094
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069D7 RID: 27095
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
