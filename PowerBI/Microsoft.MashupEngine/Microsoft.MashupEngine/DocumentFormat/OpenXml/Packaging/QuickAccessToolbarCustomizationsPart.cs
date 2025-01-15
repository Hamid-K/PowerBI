using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A8 RID: 8616
	internal class QuickAccessToolbarCustomizationsPart : CustomUIPart, IFixedContentTypePart
	{
		// Token: 0x0600DA8A RID: 55946 RVA: 0x002ADBF4 File Offset: 0x002ABDF4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (QuickAccessToolbarCustomizationsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				QuickAccessToolbarCustomizationsPart._partConstraint = dictionary;
			}
			return QuickAccessToolbarCustomizationsPart._partConstraint;
		}

		// Token: 0x0600DA8B RID: 55947 RVA: 0x002ADC1C File Offset: 0x002ABE1C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (QuickAccessToolbarCustomizationsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				QuickAccessToolbarCustomizationsPart._dataPartReferenceConstraint = dictionary;
			}
			return QuickAccessToolbarCustomizationsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA8C RID: 55948 RVA: 0x002ADC41 File Offset: 0x002ABE41
		protected internal QuickAccessToolbarCustomizationsPart()
		{
		}

		// Token: 0x170036C3 RID: 14019
		// (get) Token: 0x0600DA8D RID: 55949 RVA: 0x002ADC49 File Offset: 0x002ABE49
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization";
			}
		}

		// Token: 0x170036C4 RID: 14020
		// (get) Token: 0x0600DA8E RID: 55950 RVA: 0x002A7A24 File Offset: 0x002A5C24
		public sealed override string ContentType
		{
			get
			{
				return "application/xml";
			}
		}

		// Token: 0x170036C5 RID: 14021
		// (get) Token: 0x0600DA8F RID: 55951 RVA: 0x002ADC50 File Offset: 0x002ABE50
		internal sealed override string TargetPath
		{
			get
			{
				return "userCustomization";
			}
		}

		// Token: 0x170036C6 RID: 14022
		// (get) Token: 0x0600DA90 RID: 55952 RVA: 0x002ADC57 File Offset: 0x002ABE57
		internal sealed override string TargetName
		{
			get
			{
				return "customUI";
			}
		}

		// Token: 0x170036C7 RID: 14023
		// (get) Token: 0x0600DA91 RID: 55953 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BF3 RID: 27635
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/ui/userCustomization";

		// Token: 0x04006BF4 RID: 27636
		internal const string ContentTypeConstant = "application/xml";

		// Token: 0x04006BF5 RID: 27637
		internal const string TargetPathConstant = "userCustomization";

		// Token: 0x04006BF6 RID: 27638
		internal const string TargetNameConstant = "customUI";

		// Token: 0x04006BF7 RID: 27639
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BF8 RID: 27640
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
