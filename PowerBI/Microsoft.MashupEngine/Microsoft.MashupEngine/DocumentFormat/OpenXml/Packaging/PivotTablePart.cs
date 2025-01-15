using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002174 RID: 8564
	internal class PivotTablePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6C6 RID: 54982 RVA: 0x002A812C File Offset: 0x002A632C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PivotTablePart._partConstraint == null)
			{
				PivotTablePart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition",
					new PartConstraintRule("PivotTableCacheDefinitionPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheDefinition+xml", true, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return PivotTablePart._partConstraint;
		}

		// Token: 0x0600D6C7 RID: 54983 RVA: 0x002A8170 File Offset: 0x002A6370
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PivotTablePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PivotTablePart._dataPartReferenceConstraint = dictionary;
			}
			return PivotTablePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6C8 RID: 54984 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal PivotTablePart()
		{
		}

		// Token: 0x0600D6C9 RID: 54985 RVA: 0x002A8198 File Offset: 0x002A6398
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition")
			{
				return new PivotTableCacheDefinitionPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x170034B4 RID: 13492
		// (get) Token: 0x0600D6CA RID: 54986 RVA: 0x002A81DB File Offset: 0x002A63DB
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable";
			}
		}

		// Token: 0x170034B5 RID: 13493
		// (get) Token: 0x0600D6CB RID: 54987 RVA: 0x002A81E2 File Offset: 0x002A63E2
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotTable+xml";
			}
		}

		// Token: 0x170034B6 RID: 13494
		// (get) Token: 0x0600D6CC RID: 54988 RVA: 0x002A81E9 File Offset: 0x002A63E9
		internal sealed override string TargetPath
		{
			get
			{
				return "../pivotTables";
			}
		}

		// Token: 0x170034B7 RID: 13495
		// (get) Token: 0x0600D6CD RID: 54989 RVA: 0x002A81F0 File Offset: 0x002A63F0
		internal sealed override string TargetName
		{
			get
			{
				return "pivotTable";
			}
		}

		// Token: 0x170034B8 RID: 13496
		// (get) Token: 0x0600D6CE RID: 54990 RVA: 0x002A81F7 File Offset: 0x002A63F7
		public PivotTableCacheDefinitionPart PivotTableCacheDefinitionPart
		{
			get
			{
				return base.GetSubPartOfType<PivotTableCacheDefinitionPart>();
			}
		}

		// Token: 0x170034B9 RID: 13497
		// (get) Token: 0x0600D6CF RID: 54991 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034BA RID: 13498
		// (get) Token: 0x0600D6D0 RID: 54992 RVA: 0x002A81FF File Offset: 0x002A63FF
		// (set) Token: 0x0600D6D1 RID: 54993 RVA: 0x002A8207 File Offset: 0x002A6407
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as PivotTableDefinition;
			}
		}

		// Token: 0x170034BB RID: 13499
		// (get) Token: 0x0600D6D2 RID: 54994 RVA: 0x002A8215 File Offset: 0x002A6415
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.PivotTableDefinition;
			}
		}

		// Token: 0x170034BC RID: 13500
		// (get) Token: 0x0600D6D3 RID: 54995 RVA: 0x002A821D File Offset: 0x002A641D
		// (set) Token: 0x0600D6D4 RID: 54996 RVA: 0x002A3296 File Offset: 0x002A1496
		public PivotTableDefinition PivotTableDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<PivotTableDefinition>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006A96 RID: 27286
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable";

		// Token: 0x04006A97 RID: 27287
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotTable+xml";

		// Token: 0x04006A98 RID: 27288
		internal const string TargetPathConstant = "../pivotTables";

		// Token: 0x04006A99 RID: 27289
		internal const string TargetNameConstant = "pivotTable";

		// Token: 0x04006A9A RID: 27290
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A9B RID: 27291
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A9C RID: 27292
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PivotTableDefinition _rootEle;
	}
}
