using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215B RID: 8539
	internal class AlternativeFormatImportPart : OpenXmlPart
	{
		// Token: 0x0600D489 RID: 54409 RVA: 0x002A41AC File Offset: 0x002A23AC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (AlternativeFormatImportPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				AlternativeFormatImportPart._partConstraint = dictionary;
			}
			return AlternativeFormatImportPart._partConstraint;
		}

		// Token: 0x0600D48A RID: 54410 RVA: 0x002A41D4 File Offset: 0x002A23D4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (AlternativeFormatImportPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				AlternativeFormatImportPart._dataPartReferenceConstraint = dictionary;
			}
			return AlternativeFormatImportPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D48B RID: 54411 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal AlternativeFormatImportPart()
		{
		}

		// Token: 0x17003380 RID: 13184
		// (get) Token: 0x0600D48C RID: 54412 RVA: 0x002A41F9 File Offset: 0x002A23F9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk";
			}
		}

		// Token: 0x17003381 RID: 13185
		// (get) Token: 0x0600D48D RID: 54413 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003382 RID: 13186
		// (get) Token: 0x0600D48E RID: 54414 RVA: 0x002A4200 File Offset: 0x002A2400
		internal sealed override string TargetName
		{
			get
			{
				return "afchunk";
			}
		}

		// Token: 0x17003383 RID: 13187
		// (get) Token: 0x0600D48F RID: 54415 RVA: 0x002A1AE4 File Offset: 0x0029FCE4
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".dat";
			}
		}

		// Token: 0x17003384 RID: 13188
		// (get) Token: 0x0600D490 RID: 54416 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040069F2 RID: 27122
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk";

		// Token: 0x040069F3 RID: 27123
		internal const string TargetPathConstant = ".";

		// Token: 0x040069F4 RID: 27124
		internal const string TargetNameConstant = "afchunk";

		// Token: 0x040069F5 RID: 27125
		internal const string TargetFileExtensionConstant = ".dat";

		// Token: 0x040069F6 RID: 27126
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069F7 RID: 27127
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
