using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217E RID: 8574
	internal class TableDefinitionPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D74E RID: 55118 RVA: 0x002A886C File Offset: 0x002A6A6C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (TableDefinitionPart._partConstraint == null)
			{
				TableDefinitionPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable",
					new PartConstraintRule("QueryTablePart", "application/vnd.openxmlformats-officedocument.spreadsheetml.queryTable+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return TableDefinitionPart._partConstraint;
		}

		// Token: 0x0600D74F RID: 55119 RVA: 0x002A88B0 File Offset: 0x002A6AB0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (TableDefinitionPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				TableDefinitionPart._dataPartReferenceConstraint = dictionary;
			}
			return TableDefinitionPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D750 RID: 55120 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal TableDefinitionPart()
		{
		}

		// Token: 0x0600D751 RID: 55121 RVA: 0x002A88D8 File Offset: 0x002A6AD8
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable")
			{
				return new QueryTablePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003507 RID: 13575
		// (get) Token: 0x0600D752 RID: 55122 RVA: 0x002A891B File Offset: 0x002A6B1B
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table";
			}
		}

		// Token: 0x17003508 RID: 13576
		// (get) Token: 0x0600D753 RID: 55123 RVA: 0x002A8922 File Offset: 0x002A6B22
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml";
			}
		}

		// Token: 0x17003509 RID: 13577
		// (get) Token: 0x0600D754 RID: 55124 RVA: 0x002A879F File Offset: 0x002A699F
		internal sealed override string TargetPath
		{
			get
			{
				return "../tables";
			}
		}

		// Token: 0x1700350A RID: 13578
		// (get) Token: 0x0600D755 RID: 55125 RVA: 0x00049581 File Offset: 0x00047781
		internal sealed override string TargetName
		{
			get
			{
				return "table";
			}
		}

		// Token: 0x1700350B RID: 13579
		// (get) Token: 0x0600D756 RID: 55126 RVA: 0x002A8929 File Offset: 0x002A6B29
		public IEnumerable<QueryTablePart> QueryTableParts
		{
			get
			{
				return base.GetPartsOfType<QueryTablePart>();
			}
		}

		// Token: 0x1700350C RID: 13580
		// (get) Token: 0x0600D757 RID: 55127 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700350D RID: 13581
		// (get) Token: 0x0600D758 RID: 55128 RVA: 0x002A8931 File Offset: 0x002A6B31
		// (set) Token: 0x0600D759 RID: 55129 RVA: 0x002A8939 File Offset: 0x002A6B39
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Table;
			}
		}

		// Token: 0x1700350E RID: 13582
		// (get) Token: 0x0600D75A RID: 55130 RVA: 0x002A8947 File Offset: 0x002A6B47
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Table;
			}
		}

		// Token: 0x1700350F RID: 13583
		// (get) Token: 0x0600D75B RID: 55131 RVA: 0x002A894F File Offset: 0x002A6B4F
		// (set) Token: 0x0600D75C RID: 55132 RVA: 0x002A3296 File Offset: 0x002A1496
		public Table Table
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Table>();
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

		// Token: 0x04006ADC RID: 27356
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/table";

		// Token: 0x04006ADD RID: 27357
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml";

		// Token: 0x04006ADE RID: 27358
		internal const string TargetPathConstant = "../tables";

		// Token: 0x04006ADF RID: 27359
		internal const string TargetNameConstant = "table";

		// Token: 0x04006AE0 RID: 27360
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AE1 RID: 27361
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AE2 RID: 27362
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Table _rootEle;
	}
}
