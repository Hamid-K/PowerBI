using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A1 RID: 8609
	internal class FontPart : OpenXmlPart
	{
		// Token: 0x0600DA4A RID: 55882 RVA: 0x002AD834 File Offset: 0x002ABA34
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (FontPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				FontPart._partConstraint = dictionary;
			}
			return FontPart._partConstraint;
		}

		// Token: 0x0600DA4B RID: 55883 RVA: 0x002AD85C File Offset: 0x002ABA5C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (FontPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				FontPart._dataPartReferenceConstraint = dictionary;
			}
			return FontPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA4C RID: 55884 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal FontPart()
		{
		}

		// Token: 0x1700369D RID: 13981
		// (get) Token: 0x0600DA4D RID: 55885 RVA: 0x002AD881 File Offset: 0x002ABA81
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font";
			}
		}

		// Token: 0x1700369E RID: 13982
		// (get) Token: 0x0600DA4E RID: 55886 RVA: 0x002AD888 File Offset: 0x002ABA88
		internal sealed override string TargetPath
		{
			get
			{
				return "fonts";
			}
		}

		// Token: 0x1700369F RID: 13983
		// (get) Token: 0x0600DA4F RID: 55887 RVA: 0x002AD88F File Offset: 0x002ABA8F
		internal sealed override string TargetName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x170036A0 RID: 13984
		// (get) Token: 0x0600DA50 RID: 55888 RVA: 0x002A1AE4 File Offset: 0x0029FCE4
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".dat";
			}
		}

		// Token: 0x170036A1 RID: 13985
		// (get) Token: 0x0600DA51 RID: 55889 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BCB RID: 27595
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/font";

		// Token: 0x04006BCC RID: 27596
		internal const string TargetPathConstant = "fonts";

		// Token: 0x04006BCD RID: 27597
		internal const string TargetNameConstant = "font";

		// Token: 0x04006BCE RID: 27598
		internal const string TargetFileExtensionConstant = ".dat";

		// Token: 0x04006BCF RID: 27599
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BD0 RID: 27600
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
