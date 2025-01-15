using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219F RID: 8607
	internal class EmbeddedObjectPart : OpenXmlPart
	{
		// Token: 0x0600DA3A RID: 55866 RVA: 0x002AD77C File Offset: 0x002AB97C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (EmbeddedObjectPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedObjectPart._partConstraint = dictionary;
			}
			return EmbeddedObjectPart._partConstraint;
		}

		// Token: 0x0600DA3B RID: 55867 RVA: 0x002AD7A4 File Offset: 0x002AB9A4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (EmbeddedObjectPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedObjectPart._dataPartReferenceConstraint = dictionary;
			}
			return EmbeddedObjectPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA3C RID: 55868 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal EmbeddedObjectPart()
		{
		}

		// Token: 0x17003693 RID: 13971
		// (get) Token: 0x0600DA3D RID: 55869 RVA: 0x002AD7C9 File Offset: 0x002AB9C9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject";
			}
		}

		// Token: 0x17003694 RID: 13972
		// (get) Token: 0x0600DA3E RID: 55870 RVA: 0x002AD76C File Offset: 0x002AB96C
		internal sealed override string TargetPath
		{
			get
			{
				return "embeddings";
			}
		}

		// Token: 0x17003695 RID: 13973
		// (get) Token: 0x0600DA3F RID: 55871 RVA: 0x002AD7D0 File Offset: 0x002AB9D0
		internal sealed override string TargetName
		{
			get
			{
				return "embeddedObject";
			}
		}

		// Token: 0x17003696 RID: 13974
		// (get) Token: 0x0600DA40 RID: 55872 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x17003697 RID: 13975
		// (get) Token: 0x0600DA41 RID: 55873 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BBF RID: 27583
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/oleObject";

		// Token: 0x04006BC0 RID: 27584
		internal const string TargetPathConstant = "embeddings";

		// Token: 0x04006BC1 RID: 27585
		internal const string TargetNameConstant = "embeddedObject";

		// Token: 0x04006BC2 RID: 27586
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BC3 RID: 27587
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BC4 RID: 27588
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
