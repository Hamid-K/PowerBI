using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002177 RID: 8567
	internal class QueryTablePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6F1 RID: 55025 RVA: 0x002A83D4 File Offset: 0x002A65D4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (QueryTablePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				QueryTablePart._partConstraint = dictionary;
			}
			return QueryTablePart._partConstraint;
		}

		// Token: 0x0600D6F2 RID: 55026 RVA: 0x002A83FC File Offset: 0x002A65FC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (QueryTablePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				QueryTablePart._dataPartReferenceConstraint = dictionary;
			}
			return QueryTablePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6F3 RID: 55027 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal QueryTablePart()
		{
		}

		// Token: 0x170034CE RID: 13518
		// (get) Token: 0x0600D6F4 RID: 55028 RVA: 0x002A8421 File Offset: 0x002A6621
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable";
			}
		}

		// Token: 0x170034CF RID: 13519
		// (get) Token: 0x0600D6F5 RID: 55029 RVA: 0x002A8428 File Offset: 0x002A6628
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.queryTable+xml";
			}
		}

		// Token: 0x170034D0 RID: 13520
		// (get) Token: 0x0600D6F6 RID: 55030 RVA: 0x002A842F File Offset: 0x002A662F
		internal sealed override string TargetPath
		{
			get
			{
				return "../queryTables";
			}
		}

		// Token: 0x170034D1 RID: 13521
		// (get) Token: 0x0600D6F7 RID: 55031 RVA: 0x002A8436 File Offset: 0x002A6636
		internal sealed override string TargetName
		{
			get
			{
				return "queryTable";
			}
		}

		// Token: 0x170034D2 RID: 13522
		// (get) Token: 0x0600D6F8 RID: 55032 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034D3 RID: 13523
		// (get) Token: 0x0600D6F9 RID: 55033 RVA: 0x002A843D File Offset: 0x002A663D
		// (set) Token: 0x0600D6FA RID: 55034 RVA: 0x002A8445 File Offset: 0x002A6645
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as QueryTable;
			}
		}

		// Token: 0x170034D4 RID: 13524
		// (get) Token: 0x0600D6FB RID: 55035 RVA: 0x002A8453 File Offset: 0x002A6653
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.QueryTable;
			}
		}

		// Token: 0x170034D5 RID: 13525
		// (get) Token: 0x0600D6FC RID: 55036 RVA: 0x002A845B File Offset: 0x002A665B
		// (set) Token: 0x0600D6FD RID: 55037 RVA: 0x002A3296 File Offset: 0x002A1496
		public QueryTable QueryTable
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<QueryTable>();
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

		// Token: 0x04006AAB RID: 27307
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/queryTable";

		// Token: 0x04006AAC RID: 27308
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.queryTable+xml";

		// Token: 0x04006AAD RID: 27309
		internal const string TargetPathConstant = "../queryTables";

		// Token: 0x04006AAE RID: 27310
		internal const string TargetNameConstant = "queryTable";

		// Token: 0x04006AAF RID: 27311
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AB0 RID: 27312
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AB1 RID: 27313
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private QueryTable _rootEle;
	}
}
