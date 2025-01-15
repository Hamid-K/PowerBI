using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002157 RID: 8535
	internal class XmlSignaturePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D427 RID: 54311 RVA: 0x002A3480 File Offset: 0x002A1680
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (XmlSignaturePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				XmlSignaturePart._partConstraint = dictionary;
			}
			return XmlSignaturePart._partConstraint;
		}

		// Token: 0x0600D428 RID: 54312 RVA: 0x002A34A8 File Offset: 0x002A16A8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (XmlSignaturePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				XmlSignaturePart._dataPartReferenceConstraint = dictionary;
			}
			return XmlSignaturePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D429 RID: 54313 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal XmlSignaturePart()
		{
		}

		// Token: 0x17003348 RID: 13128
		// (get) Token: 0x0600D42A RID: 54314 RVA: 0x002A34CD File Offset: 0x002A16CD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature";
			}
		}

		// Token: 0x17003349 RID: 13129
		// (get) Token: 0x0600D42B RID: 54315 RVA: 0x002A34D4 File Offset: 0x002A16D4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-package.digital-signature-xmlsignature+xml";
			}
		}

		// Token: 0x1700334A RID: 13130
		// (get) Token: 0x0600D42C RID: 54316 RVA: 0x002A3461 File Offset: 0x002A1661
		internal sealed override string TargetPath
		{
			get
			{
				return "_xmlsignatures";
			}
		}

		// Token: 0x1700334B RID: 13131
		// (get) Token: 0x0600D42D RID: 54317 RVA: 0x002A34DB File Offset: 0x002A16DB
		internal sealed override string TargetName
		{
			get
			{
				return "sig";
			}
		}

		// Token: 0x1700334C RID: 13132
		// (get) Token: 0x0600D42E RID: 54318 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040069D8 RID: 27096
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/package/2006/relationships/digital-signature/signature";

		// Token: 0x040069D9 RID: 27097
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-package.digital-signature-xmlsignature+xml";

		// Token: 0x040069DA RID: 27098
		internal const string TargetPathConstant = "_xmlsignatures";

		// Token: 0x040069DB RID: 27099
		internal const string TargetNameConstant = "sig";

		// Token: 0x040069DC RID: 27100
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069DD RID: 27101
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
