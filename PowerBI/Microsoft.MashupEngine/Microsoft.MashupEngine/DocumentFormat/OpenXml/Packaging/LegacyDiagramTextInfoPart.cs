using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002191 RID: 8593
	internal class LegacyDiagramTextInfoPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D964 RID: 55652 RVA: 0x002AC724 File Offset: 0x002AA924
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (LegacyDiagramTextInfoPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				LegacyDiagramTextInfoPart._partConstraint = dictionary;
			}
			return LegacyDiagramTextInfoPart._partConstraint;
		}

		// Token: 0x0600D965 RID: 55653 RVA: 0x002AC74C File Offset: 0x002AA94C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (LegacyDiagramTextInfoPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				LegacyDiagramTextInfoPart._dataPartReferenceConstraint = dictionary;
			}
			return LegacyDiagramTextInfoPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D966 RID: 55654 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal LegacyDiagramTextInfoPart()
		{
		}

		// Token: 0x17003620 RID: 13856
		// (get) Token: 0x0600D967 RID: 55655 RVA: 0x002AC771 File Offset: 0x002AA971
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/legacyDocTextInfo";
			}
		}

		// Token: 0x17003621 RID: 13857
		// (get) Token: 0x0600D968 RID: 55656 RVA: 0x002AC778 File Offset: 0x002AA978
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-office.legacyDocTextInfo";
			}
		}

		// Token: 0x17003622 RID: 13858
		// (get) Token: 0x0600D969 RID: 55657 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003623 RID: 13859
		// (get) Token: 0x0600D96A RID: 55658 RVA: 0x002AC77F File Offset: 0x002AA97F
		internal sealed override string TargetName
		{
			get
			{
				return "legacyDocTextInfo";
			}
		}

		// Token: 0x17003624 RID: 13860
		// (get) Token: 0x0600D96B RID: 55659 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x17003625 RID: 13861
		// (get) Token: 0x0600D96C RID: 55660 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006B60 RID: 27488
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/legacyDocTextInfo";

		// Token: 0x04006B61 RID: 27489
		internal const string ContentTypeConstant = "application/vnd.ms-office.legacyDocTextInfo";

		// Token: 0x04006B62 RID: 27490
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B63 RID: 27491
		internal const string TargetNameConstant = "legacyDocTextInfo";

		// Token: 0x04006B64 RID: 27492
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006B65 RID: 27493
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B66 RID: 27494
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
