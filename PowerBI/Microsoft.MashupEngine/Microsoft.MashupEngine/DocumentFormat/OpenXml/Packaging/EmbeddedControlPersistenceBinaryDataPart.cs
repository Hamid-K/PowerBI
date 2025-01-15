using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A6 RID: 8614
	internal class EmbeddedControlPersistenceBinaryDataPart : OpenXmlPart
	{
		// Token: 0x0600DA7C RID: 55932 RVA: 0x002ADB64 File Offset: 0x002ABD64
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (EmbeddedControlPersistenceBinaryDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedControlPersistenceBinaryDataPart._partConstraint = dictionary;
			}
			return EmbeddedControlPersistenceBinaryDataPart._partConstraint;
		}

		// Token: 0x0600DA7D RID: 55933 RVA: 0x002ADB8C File Offset: 0x002ABD8C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (EmbeddedControlPersistenceBinaryDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedControlPersistenceBinaryDataPart._dataPartReferenceConstraint = dictionary;
			}
			return EmbeddedControlPersistenceBinaryDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA7E RID: 55934 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal EmbeddedControlPersistenceBinaryDataPart()
		{
		}

		// Token: 0x170036BB RID: 14011
		// (get) Token: 0x0600DA7F RID: 55935 RVA: 0x002ADBB1 File Offset: 0x002ABDB1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary";
			}
		}

		// Token: 0x170036BC RID: 14012
		// (get) Token: 0x0600DA80 RID: 55936 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170036BD RID: 14013
		// (get) Token: 0x0600DA81 RID: 55937 RVA: 0x002ADBB8 File Offset: 0x002ABDB8
		internal sealed override string TargetName
		{
			get
			{
				return "ActiveXControl";
			}
		}

		// Token: 0x170036BE RID: 14014
		// (get) Token: 0x0600DA82 RID: 55938 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x170036BF RID: 14015
		// (get) Token: 0x0600DA83 RID: 55939 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BEC RID: 27628
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary";

		// Token: 0x04006BED RID: 27629
		internal const string TargetPathConstant = ".";

		// Token: 0x04006BEE RID: 27630
		internal const string TargetNameConstant = "ActiveXControl";

		// Token: 0x04006BEF RID: 27631
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BF0 RID: 27632
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BF1 RID: 27633
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
