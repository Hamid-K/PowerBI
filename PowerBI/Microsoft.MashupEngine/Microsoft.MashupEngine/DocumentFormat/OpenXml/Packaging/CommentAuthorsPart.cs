using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002184 RID: 8580
	internal class CommentAuthorsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D7D5 RID: 55253 RVA: 0x002A9804 File Offset: 0x002A7A04
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CommentAuthorsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CommentAuthorsPart._partConstraint = dictionary;
			}
			return CommentAuthorsPart._partConstraint;
		}

		// Token: 0x0600D7D6 RID: 55254 RVA: 0x002A982C File Offset: 0x002A7A2C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CommentAuthorsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CommentAuthorsPart._dataPartReferenceConstraint = dictionary;
			}
			return CommentAuthorsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D7D7 RID: 55255 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CommentAuthorsPart()
		{
		}

		// Token: 0x17003555 RID: 13653
		// (get) Token: 0x0600D7D8 RID: 55256 RVA: 0x002A9851 File Offset: 0x002A7A51
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors";
			}
		}

		// Token: 0x17003556 RID: 13654
		// (get) Token: 0x0600D7D9 RID: 55257 RVA: 0x002A9858 File Offset: 0x002A7A58
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.commentAuthors+xml";
			}
		}

		// Token: 0x17003557 RID: 13655
		// (get) Token: 0x0600D7DA RID: 55258 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003558 RID: 13656
		// (get) Token: 0x0600D7DB RID: 55259 RVA: 0x002A985F File Offset: 0x002A7A5F
		internal sealed override string TargetName
		{
			get
			{
				return "commentAuthors";
			}
		}

		// Token: 0x17003559 RID: 13657
		// (get) Token: 0x0600D7DC RID: 55260 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700355A RID: 13658
		// (get) Token: 0x0600D7DD RID: 55261 RVA: 0x002A9866 File Offset: 0x002A7A66
		// (set) Token: 0x0600D7DE RID: 55262 RVA: 0x002A986E File Offset: 0x002A7A6E
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as CommentAuthorList;
			}
		}

		// Token: 0x1700355B RID: 13659
		// (get) Token: 0x0600D7DF RID: 55263 RVA: 0x002A987C File Offset: 0x002A7A7C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.CommentAuthorList;
			}
		}

		// Token: 0x1700355C RID: 13660
		// (get) Token: 0x0600D7E0 RID: 55264 RVA: 0x002A9884 File Offset: 0x002A7A84
		// (set) Token: 0x0600D7E1 RID: 55265 RVA: 0x002A3296 File Offset: 0x002A1496
		public CommentAuthorList CommentAuthorList
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<CommentAuthorList>();
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

		// Token: 0x04006B05 RID: 27397
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/commentAuthors";

		// Token: 0x04006B06 RID: 27398
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.commentAuthors+xml";

		// Token: 0x04006B07 RID: 27399
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B08 RID: 27400
		internal const string TargetNameConstant = "commentAuthors";

		// Token: 0x04006B09 RID: 27401
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B0A RID: 27402
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B0B RID: 27403
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CommentAuthorList _rootEle;
	}
}
