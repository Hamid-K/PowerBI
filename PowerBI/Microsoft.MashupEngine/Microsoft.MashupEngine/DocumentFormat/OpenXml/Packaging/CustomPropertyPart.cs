using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216E RID: 8558
	internal class CustomPropertyPart : OpenXmlPart
	{
		// Token: 0x0600D666 RID: 54886 RVA: 0x002A7974 File Offset: 0x002A5B74
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomPropertyPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomPropertyPart._partConstraint = dictionary;
			}
			return CustomPropertyPart._partConstraint;
		}

		// Token: 0x0600D667 RID: 54887 RVA: 0x002A799C File Offset: 0x002A5B9C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomPropertyPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomPropertyPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomPropertyPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D668 RID: 54888 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomPropertyPart()
		{
		}

		// Token: 0x1700347B RID: 13435
		// (get) Token: 0x0600D669 RID: 54889 RVA: 0x002A79C1 File Offset: 0x002A5BC1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty";
			}
		}

		// Token: 0x1700347C RID: 13436
		// (get) Token: 0x0600D66A RID: 54890 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700347D RID: 13437
		// (get) Token: 0x0600D66B RID: 54891 RVA: 0x002A79C8 File Offset: 0x002A5BC8
		internal sealed override string TargetName
		{
			get
			{
				return "CustomProperty";
			}
		}

		// Token: 0x1700347E RID: 13438
		// (get) Token: 0x0600D66C RID: 54892 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x1700347F RID: 13439
		// (get) Token: 0x0600D66D RID: 54893 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006A6D RID: 27245
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customProperty";

		// Token: 0x04006A6E RID: 27246
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A6F RID: 27247
		internal const string TargetNameConstant = "CustomProperty";

		// Token: 0x04006A70 RID: 27248
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006A71 RID: 27249
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A72 RID: 27250
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
