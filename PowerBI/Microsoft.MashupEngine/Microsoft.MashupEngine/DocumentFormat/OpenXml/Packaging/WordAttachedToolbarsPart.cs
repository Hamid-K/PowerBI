using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200215A RID: 8538
	internal class WordAttachedToolbarsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D480 RID: 54400 RVA: 0x002A4148 File Offset: 0x002A2348
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WordAttachedToolbarsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WordAttachedToolbarsPart._partConstraint = dictionary;
			}
			return WordAttachedToolbarsPart._partConstraint;
		}

		// Token: 0x0600D481 RID: 54401 RVA: 0x002A4170 File Offset: 0x002A2370
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WordAttachedToolbarsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WordAttachedToolbarsPart._dataPartReferenceConstraint = dictionary;
			}
			return WordAttachedToolbarsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D482 RID: 54402 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WordAttachedToolbarsPart()
		{
		}

		// Token: 0x1700337A RID: 13178
		// (get) Token: 0x0600D483 RID: 54403 RVA: 0x002A4195 File Offset: 0x002A2395
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars";
			}
		}

		// Token: 0x1700337B RID: 13179
		// (get) Token: 0x0600D484 RID: 54404 RVA: 0x002A419C File Offset: 0x002A239C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-word.attachedToolbars";
			}
		}

		// Token: 0x1700337C RID: 13180
		// (get) Token: 0x0600D485 RID: 54405 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700337D RID: 13181
		// (get) Token: 0x0600D486 RID: 54406 RVA: 0x002A41A3 File Offset: 0x002A23A3
		internal sealed override string TargetName
		{
			get
			{
				return "attachedToolbars";
			}
		}

		// Token: 0x1700337E RID: 13182
		// (get) Token: 0x0600D487 RID: 54407 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x1700337F RID: 13183
		// (get) Token: 0x0600D488 RID: 54408 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040069EB RID: 27115
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars";

		// Token: 0x040069EC RID: 27116
		internal const string ContentTypeConstant = "application/vnd.ms-word.attachedToolbars";

		// Token: 0x040069ED RID: 27117
		internal const string TargetPathConstant = ".";

		// Token: 0x040069EE RID: 27118
		internal const string TargetNameConstant = "attachedToolbars";

		// Token: 0x040069EF RID: 27119
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x040069F0 RID: 27120
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069F1 RID: 27121
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
