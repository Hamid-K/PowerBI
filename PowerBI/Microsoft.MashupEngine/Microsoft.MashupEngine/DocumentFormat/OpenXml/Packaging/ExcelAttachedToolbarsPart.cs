using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002181 RID: 8577
	internal class ExcelAttachedToolbarsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D79B RID: 55195 RVA: 0x002A9178 File Offset: 0x002A7378
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ExcelAttachedToolbarsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExcelAttachedToolbarsPart._partConstraint = dictionary;
			}
			return ExcelAttachedToolbarsPart._partConstraint;
		}

		// Token: 0x0600D79C RID: 55196 RVA: 0x002A91A0 File Offset: 0x002A73A0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ExcelAttachedToolbarsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExcelAttachedToolbarsPart._dataPartReferenceConstraint = dictionary;
			}
			return ExcelAttachedToolbarsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D79D RID: 55197 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ExcelAttachedToolbarsPart()
		{
		}

		// Token: 0x17003531 RID: 13617
		// (get) Token: 0x0600D79E RID: 55198 RVA: 0x002A4195 File Offset: 0x002A2395
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars";
			}
		}

		// Token: 0x17003532 RID: 13618
		// (get) Token: 0x0600D79F RID: 55199 RVA: 0x002A91C5 File Offset: 0x002A73C5
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.attachedToolbars";
			}
		}

		// Token: 0x17003533 RID: 13619
		// (get) Token: 0x0600D7A0 RID: 55200 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003534 RID: 13620
		// (get) Token: 0x0600D7A1 RID: 55201 RVA: 0x002A41A3 File Offset: 0x002A23A3
		internal sealed override string TargetName
		{
			get
			{
				return "attachedToolbars";
			}
		}

		// Token: 0x17003535 RID: 13621
		// (get) Token: 0x0600D7A2 RID: 55202 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x17003536 RID: 13622
		// (get) Token: 0x0600D7A3 RID: 55203 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006AF1 RID: 27377
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/attachedToolbars";

		// Token: 0x04006AF2 RID: 27378
		internal const string ContentTypeConstant = "application/vnd.ms-excel.attachedToolbars";

		// Token: 0x04006AF3 RID: 27379
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AF4 RID: 27380
		internal const string TargetNameConstant = "attachedToolbars";

		// Token: 0x04006AF5 RID: 27381
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006AF6 RID: 27382
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AF7 RID: 27383
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
