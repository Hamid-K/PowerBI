using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217A RID: 8570
	internal class WorkbookRevisionLogPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D71A RID: 55066 RVA: 0x002A8614 File Offset: 0x002A6814
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorkbookRevisionLogPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookRevisionLogPart._partConstraint = dictionary;
			}
			return WorkbookRevisionLogPart._partConstraint;
		}

		// Token: 0x0600D71B RID: 55067 RVA: 0x002A863C File Offset: 0x002A683C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorkbookRevisionLogPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookRevisionLogPart._dataPartReferenceConstraint = dictionary;
			}
			return WorkbookRevisionLogPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D71C RID: 55068 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorkbookRevisionLogPart()
		{
		}

		// Token: 0x170034E7 RID: 13543
		// (get) Token: 0x0600D71D RID: 55069 RVA: 0x002A8661 File Offset: 0x002A6861
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog";
			}
		}

		// Token: 0x170034E8 RID: 13544
		// (get) Token: 0x0600D71E RID: 55070 RVA: 0x002A8668 File Offset: 0x002A6868
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionLog+xml";
			}
		}

		// Token: 0x170034E9 RID: 13545
		// (get) Token: 0x0600D71F RID: 55071 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170034EA RID: 13546
		// (get) Token: 0x0600D720 RID: 55072 RVA: 0x002A866F File Offset: 0x002A686F
		internal sealed override string TargetName
		{
			get
			{
				return "revisionLog";
			}
		}

		// Token: 0x170034EB RID: 13547
		// (get) Token: 0x0600D721 RID: 55073 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034EC RID: 13548
		// (get) Token: 0x0600D722 RID: 55074 RVA: 0x002A8676 File Offset: 0x002A6876
		// (set) Token: 0x0600D723 RID: 55075 RVA: 0x002A867E File Offset: 0x002A687E
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Revisions;
			}
		}

		// Token: 0x170034ED RID: 13549
		// (get) Token: 0x0600D724 RID: 55076 RVA: 0x002A868C File Offset: 0x002A688C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Revisions;
			}
		}

		// Token: 0x170034EE RID: 13550
		// (get) Token: 0x0600D725 RID: 55077 RVA: 0x002A8694 File Offset: 0x002A6894
		// (set) Token: 0x0600D726 RID: 55078 RVA: 0x002A3296 File Offset: 0x002A1496
		public Revisions Revisions
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Revisions>();
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

		// Token: 0x04006AC0 RID: 27328
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/revisionLog";

		// Token: 0x04006AC1 RID: 27329
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.revisionLog+xml";

		// Token: 0x04006AC2 RID: 27330
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AC3 RID: 27331
		internal const string TargetNameConstant = "revisionLog";

		// Token: 0x04006AC4 RID: 27332
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AC5 RID: 27333
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AC6 RID: 27334
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Revisions _rootEle;
	}
}
