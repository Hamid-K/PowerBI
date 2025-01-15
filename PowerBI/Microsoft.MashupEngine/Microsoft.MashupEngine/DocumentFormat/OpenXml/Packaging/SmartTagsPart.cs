using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002190 RID: 8592
	internal class SmartTagsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D95B RID: 55643 RVA: 0x002AC6C0 File Offset: 0x002AA8C0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SmartTagsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SmartTagsPart._partConstraint = dictionary;
			}
			return SmartTagsPart._partConstraint;
		}

		// Token: 0x0600D95C RID: 55644 RVA: 0x002AC6E8 File Offset: 0x002AA8E8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SmartTagsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SmartTagsPart._dataPartReferenceConstraint = dictionary;
			}
			return SmartTagsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D95D RID: 55645 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SmartTagsPart()
		{
		}

		// Token: 0x1700361A RID: 13850
		// (get) Token: 0x0600D95E RID: 55646 RVA: 0x002AC70D File Offset: 0x002AA90D
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/smartTags";
			}
		}

		// Token: 0x1700361B RID: 13851
		// (get) Token: 0x0600D95F RID: 55647 RVA: 0x002AC714 File Offset: 0x002AA914
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-powerpoint.smartTags";
			}
		}

		// Token: 0x1700361C RID: 13852
		// (get) Token: 0x0600D960 RID: 55648 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700361D RID: 13853
		// (get) Token: 0x0600D961 RID: 55649 RVA: 0x002AC71B File Offset: 0x002AA91B
		internal sealed override string TargetName
		{
			get
			{
				return "smartTags";
			}
		}

		// Token: 0x1700361E RID: 13854
		// (get) Token: 0x0600D962 RID: 55650 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x1700361F RID: 13855
		// (get) Token: 0x0600D963 RID: 55651 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006B59 RID: 27481
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/smartTags";

		// Token: 0x04006B5A RID: 27482
		internal const string ContentTypeConstant = "application/vnd.ms-powerpoint.smartTags";

		// Token: 0x04006B5B RID: 27483
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B5C RID: 27484
		internal const string TargetNameConstant = "smartTags";

		// Token: 0x04006B5D RID: 27485
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006B5E RID: 27486
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B5F RID: 27487
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
