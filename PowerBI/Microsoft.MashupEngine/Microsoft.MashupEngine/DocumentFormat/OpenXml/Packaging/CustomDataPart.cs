using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B3 RID: 8627
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomDataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB40 RID: 56128 RVA: 0x002AEC3C File Offset: 0x002ACE3C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomDataPart._partConstraint = dictionary;
			}
			return CustomDataPart._partConstraint;
		}

		// Token: 0x0600DB41 RID: 56129 RVA: 0x002AEC64 File Offset: 0x002ACE64
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomDataPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB42 RID: 56130 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomDataPart()
		{
		}

		// Token: 0x17003721 RID: 14113
		// (get) Token: 0x0600DB43 RID: 56131 RVA: 0x002AEC89 File Offset: 0x002ACE89
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/customData";
			}
		}

		// Token: 0x17003722 RID: 14114
		// (get) Token: 0x0600DB44 RID: 56132 RVA: 0x002AEC90 File Offset: 0x002ACE90
		public sealed override string ContentType
		{
			get
			{
				return "application/binary";
			}
		}

		// Token: 0x17003723 RID: 14115
		// (get) Token: 0x0600DB45 RID: 56133 RVA: 0x002AEBF1 File Offset: 0x002ACDF1
		internal sealed override string TargetPath
		{
			get
			{
				return "customData";
			}
		}

		// Token: 0x17003724 RID: 14116
		// (get) Token: 0x0600DB46 RID: 56134 RVA: 0x002AEBF1 File Offset: 0x002ACDF1
		internal sealed override string TargetName
		{
			get
			{
				return "customData";
			}
		}

		// Token: 0x0600DB47 RID: 56135 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x17003725 RID: 14117
		// (get) Token: 0x0600DB48 RID: 56136 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006C3B RID: 27707
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/customData";

		// Token: 0x04006C3C RID: 27708
		internal const string ContentTypeConstant = "application/binary";

		// Token: 0x04006C3D RID: 27709
		internal const string TargetPathConstant = "customData";

		// Token: 0x04006C3E RID: 27710
		internal const string TargetNameConstant = "customData";

		// Token: 0x04006C3F RID: 27711
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C40 RID: 27712
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
