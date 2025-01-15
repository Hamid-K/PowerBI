using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A0 RID: 8608
	internal class EmbeddedPackagePart : OpenXmlPart
	{
		// Token: 0x0600DA42 RID: 55874 RVA: 0x002AD7D8 File Offset: 0x002AB9D8
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (EmbeddedPackagePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedPackagePart._partConstraint = dictionary;
			}
			return EmbeddedPackagePart._partConstraint;
		}

		// Token: 0x0600DA43 RID: 55875 RVA: 0x002AD800 File Offset: 0x002ABA00
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (EmbeddedPackagePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedPackagePart._dataPartReferenceConstraint = dictionary;
			}
			return EmbeddedPackagePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA44 RID: 55876 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal EmbeddedPackagePart()
		{
		}

		// Token: 0x17003698 RID: 13976
		// (get) Token: 0x0600DA45 RID: 55877 RVA: 0x002AD825 File Offset: 0x002ABA25
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package";
			}
		}

		// Token: 0x17003699 RID: 13977
		// (get) Token: 0x0600DA46 RID: 55878 RVA: 0x002AD76C File Offset: 0x002AB96C
		internal sealed override string TargetPath
		{
			get
			{
				return "embeddings";
			}
		}

		// Token: 0x1700369A RID: 13978
		// (get) Token: 0x0600DA47 RID: 55879 RVA: 0x002AD82C File Offset: 0x002ABA2C
		internal sealed override string TargetName
		{
			get
			{
				return "package";
			}
		}

		// Token: 0x1700369B RID: 13979
		// (get) Token: 0x0600DA48 RID: 55880 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x1700369C RID: 13980
		// (get) Token: 0x0600DA49 RID: 55881 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BC5 RID: 27589
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/package";

		// Token: 0x04006BC6 RID: 27590
		internal const string TargetPathConstant = "embeddings";

		// Token: 0x04006BC7 RID: 27591
		internal const string TargetNameConstant = "package";

		// Token: 0x04006BC8 RID: 27592
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BC9 RID: 27593
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BCA RID: 27594
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
