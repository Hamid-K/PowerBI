using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216C RID: 8556
	internal class WorksheetCommentsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D64C RID: 54860 RVA: 0x002A784C File Offset: 0x002A5A4C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorksheetCommentsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorksheetCommentsPart._partConstraint = dictionary;
			}
			return WorksheetCommentsPart._partConstraint;
		}

		// Token: 0x0600D64D RID: 54861 RVA: 0x002A7874 File Offset: 0x002A5A74
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorksheetCommentsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorksheetCommentsPart._dataPartReferenceConstraint = dictionary;
			}
			return WorksheetCommentsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D64E RID: 54862 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorksheetCommentsPart()
		{
		}

		// Token: 0x1700346B RID: 13419
		// (get) Token: 0x0600D64F RID: 54863 RVA: 0x002A4725 File Offset: 0x002A2925
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";
			}
		}

		// Token: 0x1700346C RID: 13420
		// (get) Token: 0x0600D650 RID: 54864 RVA: 0x002A7899 File Offset: 0x002A5A99
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.comments+xml";
			}
		}

		// Token: 0x1700346D RID: 13421
		// (get) Token: 0x0600D651 RID: 54865 RVA: 0x002A78A0 File Offset: 0x002A5AA0
		internal sealed override string TargetPath
		{
			get
			{
				return "..";
			}
		}

		// Token: 0x1700346E RID: 13422
		// (get) Token: 0x0600D652 RID: 54866 RVA: 0x002A4733 File Offset: 0x002A2933
		internal sealed override string TargetName
		{
			get
			{
				return "comments";
			}
		}

		// Token: 0x1700346F RID: 13423
		// (get) Token: 0x0600D653 RID: 54867 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003470 RID: 13424
		// (get) Token: 0x0600D654 RID: 54868 RVA: 0x002A78A7 File Offset: 0x002A5AA7
		// (set) Token: 0x0600D655 RID: 54869 RVA: 0x002A78AF File Offset: 0x002A5AAF
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Comments;
			}
		}

		// Token: 0x17003471 RID: 13425
		// (get) Token: 0x0600D656 RID: 54870 RVA: 0x002A78BD File Offset: 0x002A5ABD
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Comments;
			}
		}

		// Token: 0x17003472 RID: 13426
		// (get) Token: 0x0600D657 RID: 54871 RVA: 0x002A78C5 File Offset: 0x002A5AC5
		// (set) Token: 0x0600D658 RID: 54872 RVA: 0x002A3296 File Offset: 0x002A1496
		public Comments Comments
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Comments>();
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

		// Token: 0x04006A5F RID: 27231
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";

		// Token: 0x04006A60 RID: 27232
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.comments+xml";

		// Token: 0x04006A61 RID: 27233
		internal const string TargetPathConstant = "..";

		// Token: 0x04006A62 RID: 27234
		internal const string TargetNameConstant = "comments";

		// Token: 0x04006A63 RID: 27235
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A64 RID: 27236
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A65 RID: 27237
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Comments _rootEle;
	}
}
