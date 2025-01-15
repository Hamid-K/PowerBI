using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AF RID: 8623
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class StylesWithEffectsPart : StylesPart, IFixedContentTypePart
	{
		// Token: 0x0600DB05 RID: 56069 RVA: 0x002AE89C File Offset: 0x002ACA9C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (StylesWithEffectsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				StylesWithEffectsPart._partConstraint = dictionary;
			}
			return StylesWithEffectsPart._partConstraint;
		}

		// Token: 0x0600DB06 RID: 56070 RVA: 0x002AE8C4 File Offset: 0x002ACAC4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (StylesWithEffectsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				StylesWithEffectsPart._dataPartReferenceConstraint = dictionary;
			}
			return StylesWithEffectsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB07 RID: 56071 RVA: 0x002A6B91 File Offset: 0x002A4D91
		protected internal StylesWithEffectsPart()
		{
		}

		// Token: 0x17003702 RID: 14082
		// (get) Token: 0x0600DB08 RID: 56072 RVA: 0x002AE8E9 File Offset: 0x002ACAE9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/stylesWithEffects";
			}
		}

		// Token: 0x17003703 RID: 14083
		// (get) Token: 0x0600DB09 RID: 56073 RVA: 0x002AE8F0 File Offset: 0x002ACAF0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-word.stylesWithEffects+xml";
			}
		}

		// Token: 0x17003704 RID: 14084
		// (get) Token: 0x0600DB0A RID: 56074 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003705 RID: 14085
		// (get) Token: 0x0600DB0B RID: 56075 RVA: 0x002AE8F7 File Offset: 0x002ACAF7
		internal sealed override string TargetName
		{
			get
			{
				return "stylesWithEffects";
			}
		}

		// Token: 0x0600DB0C RID: 56076 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x17003706 RID: 14086
		// (get) Token: 0x0600DB0D RID: 56077 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006C20 RID: 27680
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/stylesWithEffects";

		// Token: 0x04006C21 RID: 27681
		internal const string ContentTypeConstant = "application/vnd.ms-word.stylesWithEffects+xml";

		// Token: 0x04006C22 RID: 27682
		internal const string TargetPathConstant = ".";

		// Token: 0x04006C23 RID: 27683
		internal const string TargetNameConstant = "stylesWithEffects";

		// Token: 0x04006C24 RID: 27684
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C25 RID: 27685
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
