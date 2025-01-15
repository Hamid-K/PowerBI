using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002175 RID: 8565
	internal class PivotTableCacheDefinitionPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6D5 RID: 54997 RVA: 0x002A8234 File Offset: 0x002A6434
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PivotTableCacheDefinitionPart._partConstraint == null)
			{
				PivotTableCacheDefinitionPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords",
					new PartConstraintRule("PivotTableCacheRecordsPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheRecords+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return PivotTableCacheDefinitionPart._partConstraint;
		}

		// Token: 0x0600D6D6 RID: 54998 RVA: 0x002A8278 File Offset: 0x002A6478
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PivotTableCacheDefinitionPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PivotTableCacheDefinitionPart._dataPartReferenceConstraint = dictionary;
			}
			return PivotTableCacheDefinitionPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6D7 RID: 54999 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal PivotTableCacheDefinitionPart()
		{
		}

		// Token: 0x0600D6D8 RID: 55000 RVA: 0x002A82A0 File Offset: 0x002A64A0
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords")
			{
				return new PivotTableCacheRecordsPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x170034BD RID: 13501
		// (get) Token: 0x0600D6D9 RID: 55001 RVA: 0x002A82E3 File Offset: 0x002A64E3
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition";
			}
		}

		// Token: 0x170034BE RID: 13502
		// (get) Token: 0x0600D6DA RID: 55002 RVA: 0x002A82EA File Offset: 0x002A64EA
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheDefinition+xml";
			}
		}

		// Token: 0x170034BF RID: 13503
		// (get) Token: 0x0600D6DB RID: 55003 RVA: 0x002A82F1 File Offset: 0x002A64F1
		internal sealed override string TargetPath
		{
			get
			{
				return "../pivotCache";
			}
		}

		// Token: 0x170034C0 RID: 13504
		// (get) Token: 0x0600D6DC RID: 55004 RVA: 0x002A82F8 File Offset: 0x002A64F8
		internal sealed override string TargetName
		{
			get
			{
				return "pivotCacheDefinition";
			}
		}

		// Token: 0x170034C1 RID: 13505
		// (get) Token: 0x0600D6DD RID: 55005 RVA: 0x002A82FF File Offset: 0x002A64FF
		public PivotTableCacheRecordsPart PivotTableCacheRecordsPart
		{
			get
			{
				return base.GetSubPartOfType<PivotTableCacheRecordsPart>();
			}
		}

		// Token: 0x170034C2 RID: 13506
		// (get) Token: 0x0600D6DE RID: 55006 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034C3 RID: 13507
		// (get) Token: 0x0600D6DF RID: 55007 RVA: 0x002A8307 File Offset: 0x002A6507
		// (set) Token: 0x0600D6E0 RID: 55008 RVA: 0x002A830F File Offset: 0x002A650F
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as PivotCacheDefinition;
			}
		}

		// Token: 0x170034C4 RID: 13508
		// (get) Token: 0x0600D6E1 RID: 55009 RVA: 0x002A831D File Offset: 0x002A651D
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.PivotCacheDefinition;
			}
		}

		// Token: 0x170034C5 RID: 13509
		// (get) Token: 0x0600D6E2 RID: 55010 RVA: 0x002A8325 File Offset: 0x002A6525
		// (set) Token: 0x0600D6E3 RID: 55011 RVA: 0x002A3296 File Offset: 0x002A1496
		public PivotCacheDefinition PivotCacheDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<PivotCacheDefinition>();
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

		// Token: 0x04006A9D RID: 27293
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition";

		// Token: 0x04006A9E RID: 27294
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheDefinition+xml";

		// Token: 0x04006A9F RID: 27295
		internal const string TargetPathConstant = "../pivotCache";

		// Token: 0x04006AA0 RID: 27296
		internal const string TargetNameConstant = "pivotCacheDefinition";

		// Token: 0x04006AA1 RID: 27297
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AA2 RID: 27298
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AA3 RID: 27299
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PivotCacheDefinition _rootEle;
	}
}
