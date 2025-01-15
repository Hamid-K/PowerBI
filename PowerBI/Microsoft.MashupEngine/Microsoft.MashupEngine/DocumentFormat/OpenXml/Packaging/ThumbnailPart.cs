using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002155 RID: 8533
	internal class ThumbnailPart : OpenXmlPart
	{
		// Token: 0x0600D414 RID: 54292 RVA: 0x002A3348 File Offset: 0x002A1548
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ThumbnailPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ThumbnailPart._partConstraint = dictionary;
			}
			return ThumbnailPart._partConstraint;
		}

		// Token: 0x0600D415 RID: 54293 RVA: 0x002A3370 File Offset: 0x002A1570
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ThumbnailPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ThumbnailPart._dataPartReferenceConstraint = dictionary;
			}
			return ThumbnailPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D416 RID: 54294 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ThumbnailPart()
		{
		}

		// Token: 0x1700333C RID: 13116
		// (get) Token: 0x0600D417 RID: 54295 RVA: 0x002A3395 File Offset: 0x002A1595
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail";
			}
		}

		// Token: 0x1700333D RID: 13117
		// (get) Token: 0x0600D418 RID: 54296 RVA: 0x002A31EF File Offset: 0x002A13EF
		internal sealed override string TargetPath
		{
			get
			{
				return "docProps";
			}
		}

		// Token: 0x1700333E RID: 13118
		// (get) Token: 0x0600D419 RID: 54297 RVA: 0x002A339C File Offset: 0x002A159C
		internal sealed override string TargetName
		{
			get
			{
				return "thumbnail";
			}
		}

		// Token: 0x1700333F RID: 13119
		// (get) Token: 0x0600D41A RID: 54298 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x17003340 RID: 13120
		// (get) Token: 0x0600D41B RID: 54299 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040069CB RID: 27083
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail";

		// Token: 0x040069CC RID: 27084
		internal const string TargetPathConstant = "docProps";

		// Token: 0x040069CD RID: 27085
		internal const string TargetNameConstant = "thumbnail";

		// Token: 0x040069CE RID: 27086
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x040069CF RID: 27087
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069D0 RID: 27088
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
