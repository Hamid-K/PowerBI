using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002179 RID: 8569
	internal class WorkbookRevisionHeaderPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D70B RID: 55051 RVA: 0x002A850C File Offset: 0x002A670C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorkbookRevisionHeaderPart._partConstraint == null)
			{
				WorkbookRevisionHeaderPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog",
					new PartConstraintRule("WorkbookRevisionLogPart", "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionLog+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return WorkbookRevisionHeaderPart._partConstraint;
		}

		// Token: 0x0600D70C RID: 55052 RVA: 0x002A8550 File Offset: 0x002A6750
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorkbookRevisionHeaderPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookRevisionHeaderPart._dataPartReferenceConstraint = dictionary;
			}
			return WorkbookRevisionHeaderPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D70D RID: 55053 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorkbookRevisionHeaderPart()
		{
		}

		// Token: 0x0600D70E RID: 55054 RVA: 0x002A8578 File Offset: 0x002A6778
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog")
			{
				return new WorkbookRevisionLogPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x170034DE RID: 13534
		// (get) Token: 0x0600D70F RID: 55055 RVA: 0x002A85BB File Offset: 0x002A67BB
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders";
			}
		}

		// Token: 0x170034DF RID: 13535
		// (get) Token: 0x0600D710 RID: 55056 RVA: 0x002A85C2 File Offset: 0x002A67C2
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionHeaders+xml";
			}
		}

		// Token: 0x170034E0 RID: 13536
		// (get) Token: 0x0600D711 RID: 55057 RVA: 0x002A85C9 File Offset: 0x002A67C9
		internal sealed override string TargetPath
		{
			get
			{
				return "revisions";
			}
		}

		// Token: 0x170034E1 RID: 13537
		// (get) Token: 0x0600D712 RID: 55058 RVA: 0x002A85D0 File Offset: 0x002A67D0
		internal sealed override string TargetName
		{
			get
			{
				return "revisionHeaders";
			}
		}

		// Token: 0x170034E2 RID: 13538
		// (get) Token: 0x0600D713 RID: 55059 RVA: 0x002A85D7 File Offset: 0x002A67D7
		public IEnumerable<WorkbookRevisionLogPart> WorkbookRevisionLogParts
		{
			get
			{
				return base.GetPartsOfType<WorkbookRevisionLogPart>();
			}
		}

		// Token: 0x170034E3 RID: 13539
		// (get) Token: 0x0600D714 RID: 55060 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034E4 RID: 13540
		// (get) Token: 0x0600D715 RID: 55061 RVA: 0x002A85DF File Offset: 0x002A67DF
		// (set) Token: 0x0600D716 RID: 55062 RVA: 0x002A85E7 File Offset: 0x002A67E7
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Headers;
			}
		}

		// Token: 0x170034E5 RID: 13541
		// (get) Token: 0x0600D717 RID: 55063 RVA: 0x002A85F5 File Offset: 0x002A67F5
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Headers;
			}
		}

		// Token: 0x170034E6 RID: 13542
		// (get) Token: 0x0600D718 RID: 55064 RVA: 0x002A85FD File Offset: 0x002A67FD
		// (set) Token: 0x0600D719 RID: 55065 RVA: 0x002A3296 File Offset: 0x002A1496
		public Headers Headers
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Headers>();
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

		// Token: 0x04006AB9 RID: 27321
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionHeaders";

		// Token: 0x04006ABA RID: 27322
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionHeaders+xml";

		// Token: 0x04006ABB RID: 27323
		internal const string TargetPathConstant = "revisions";

		// Token: 0x04006ABC RID: 27324
		internal const string TargetNameConstant = "revisionHeaders";

		// Token: 0x04006ABD RID: 27325
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006ABE RID: 27326
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006ABF RID: 27327
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Headers _rootEle;
	}
}
