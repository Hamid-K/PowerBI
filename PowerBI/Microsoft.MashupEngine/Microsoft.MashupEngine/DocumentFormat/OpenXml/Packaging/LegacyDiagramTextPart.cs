using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002192 RID: 8594
	internal class LegacyDiagramTextPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D96D RID: 55661 RVA: 0x002AC788 File Offset: 0x002AA988
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (LegacyDiagramTextPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				LegacyDiagramTextPart._partConstraint = dictionary;
			}
			return LegacyDiagramTextPart._partConstraint;
		}

		// Token: 0x0600D96E RID: 55662 RVA: 0x002AC7B0 File Offset: 0x002AA9B0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (LegacyDiagramTextPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				LegacyDiagramTextPart._dataPartReferenceConstraint = dictionary;
			}
			return LegacyDiagramTextPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D96F RID: 55663 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal LegacyDiagramTextPart()
		{
		}

		// Token: 0x17003626 RID: 13862
		// (get) Token: 0x0600D970 RID: 55664 RVA: 0x002AC7D5 File Offset: 0x002AA9D5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText";
			}
		}

		// Token: 0x17003627 RID: 13863
		// (get) Token: 0x0600D971 RID: 55665 RVA: 0x002AC7DC File Offset: 0x002AA9DC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-office.legacyDiagramText";
			}
		}

		// Token: 0x17003628 RID: 13864
		// (get) Token: 0x0600D972 RID: 55666 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003629 RID: 13865
		// (get) Token: 0x0600D973 RID: 55667 RVA: 0x002AC7E3 File Offset: 0x002AA9E3
		internal sealed override string TargetName
		{
			get
			{
				return "legacyDiagramText";
			}
		}

		// Token: 0x1700362A RID: 13866
		// (get) Token: 0x0600D974 RID: 55668 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x1700362B RID: 13867
		// (get) Token: 0x0600D975 RID: 55669 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006B67 RID: 27495
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/legacyDiagramText";

		// Token: 0x04006B68 RID: 27496
		internal const string ContentTypeConstant = "application/vnd.ms-office.legacyDiagramText";

		// Token: 0x04006B69 RID: 27497
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B6A RID: 27498
		internal const string TargetNameConstant = "legacyDiagramText";

		// Token: 0x04006B6B RID: 27499
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006B6C RID: 27500
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B6D RID: 27501
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
