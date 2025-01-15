using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219C RID: 8604
	internal class CustomXmlPart : OpenXmlPart
	{
		// Token: 0x0600DA16 RID: 55830 RVA: 0x002AD4B0 File Offset: 0x002AB6B0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomXmlPart._partConstraint == null)
			{
				CustomXmlPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps",
					new PartConstraintRule("CustomXmlPropertiesPart", "application/vnd.openxmlformats-officedocument.customXmlProperties+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return CustomXmlPart._partConstraint;
		}

		// Token: 0x0600DA17 RID: 55831 RVA: 0x002AD4F4 File Offset: 0x002AB6F4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomXmlPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomXmlPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomXmlPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA18 RID: 55832 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomXmlPart()
		{
		}

		// Token: 0x0600DA19 RID: 55833 RVA: 0x002AD51C File Offset: 0x002AB71C
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps")
			{
				return new CustomXmlPropertiesPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003680 RID: 13952
		// (get) Token: 0x0600DA1A RID: 55834 RVA: 0x002AD55F File Offset: 0x002AB75F
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml";
			}
		}

		// Token: 0x17003681 RID: 13953
		// (get) Token: 0x0600DA1B RID: 55835 RVA: 0x002AD566 File Offset: 0x002AB766
		internal sealed override string TargetPath
		{
			get
			{
				return "../customXML";
			}
		}

		// Token: 0x17003682 RID: 13954
		// (get) Token: 0x0600DA1C RID: 55836 RVA: 0x002AD56D File Offset: 0x002AB76D
		internal sealed override string TargetName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x17003683 RID: 13955
		// (get) Token: 0x0600DA1D RID: 55837 RVA: 0x002AD574 File Offset: 0x002AB774
		public CustomXmlPropertiesPart CustomXmlPropertiesPart
		{
			get
			{
				return base.GetSubPartOfType<CustomXmlPropertiesPart>();
			}
		}

		// Token: 0x17003684 RID: 13956
		// (get) Token: 0x0600DA1E RID: 55838 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BAD RID: 27565
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml";

		// Token: 0x04006BAE RID: 27566
		internal const string TargetPathConstant = "../customXML";

		// Token: 0x04006BAF RID: 27567
		internal const string TargetNameConstant = "item";

		// Token: 0x04006BB0 RID: 27568
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BB1 RID: 27569
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
