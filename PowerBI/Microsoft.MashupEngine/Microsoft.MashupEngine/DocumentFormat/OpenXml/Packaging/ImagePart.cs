using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A4 RID: 8612
	internal class ImagePart : OpenXmlPart
	{
		// Token: 0x0600DA64 RID: 55908 RVA: 0x002AD958 File Offset: 0x002ABB58
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ImagePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ImagePart._partConstraint = dictionary;
			}
			return ImagePart._partConstraint;
		}

		// Token: 0x0600DA65 RID: 55909 RVA: 0x002AD980 File Offset: 0x002ABB80
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ImagePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ImagePart._dataPartReferenceConstraint = dictionary;
			}
			return ImagePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA66 RID: 55910 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ImagePart()
		{
		}

		// Token: 0x170036AE RID: 13998
		// (get) Token: 0x0600DA67 RID: 55911 RVA: 0x002AD9A5 File Offset: 0x002ABBA5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";
			}
		}

		// Token: 0x170036AF RID: 13999
		// (get) Token: 0x0600DA68 RID: 55912 RVA: 0x002AD9AC File Offset: 0x002ABBAC
		internal sealed override string TargetPath
		{
			get
			{
				return "../media";
			}
		}

		// Token: 0x170036B0 RID: 14000
		// (get) Token: 0x0600DA69 RID: 55913 RVA: 0x002AD9B3 File Offset: 0x002ABBB3
		internal sealed override string TargetName
		{
			get
			{
				return "image";
			}
		}

		// Token: 0x170036B1 RID: 14001
		// (get) Token: 0x0600DA6A RID: 55914 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x170036B2 RID: 14002
		// (get) Token: 0x0600DA6B RID: 55915 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BDF RID: 27615
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image";

		// Token: 0x04006BE0 RID: 27616
		internal const string TargetPathConstant = "../media";

		// Token: 0x04006BE1 RID: 27617
		internal const string TargetNameConstant = "image";

		// Token: 0x04006BE2 RID: 27618
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BE3 RID: 27619
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BE4 RID: 27620
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
