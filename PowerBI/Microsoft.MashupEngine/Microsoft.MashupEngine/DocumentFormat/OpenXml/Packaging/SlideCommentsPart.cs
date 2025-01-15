using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002185 RID: 8581
	internal class SlideCommentsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D7E2 RID: 55266 RVA: 0x002A989C File Offset: 0x002A7A9C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlideCommentsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlideCommentsPart._partConstraint = dictionary;
			}
			return SlideCommentsPart._partConstraint;
		}

		// Token: 0x0600D7E3 RID: 55267 RVA: 0x002A98C4 File Offset: 0x002A7AC4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlideCommentsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlideCommentsPart._dataPartReferenceConstraint = dictionary;
			}
			return SlideCommentsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D7E4 RID: 55268 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlideCommentsPart()
		{
		}

		// Token: 0x1700355D RID: 13661
		// (get) Token: 0x0600D7E5 RID: 55269 RVA: 0x002A4725 File Offset: 0x002A2925
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";
			}
		}

		// Token: 0x1700355E RID: 13662
		// (get) Token: 0x0600D7E6 RID: 55270 RVA: 0x002A98E9 File Offset: 0x002A7AE9
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.comments+xml";
			}
		}

		// Token: 0x1700355F RID: 13663
		// (get) Token: 0x0600D7E7 RID: 55271 RVA: 0x002A98F0 File Offset: 0x002A7AF0
		internal sealed override string TargetPath
		{
			get
			{
				return "../comments";
			}
		}

		// Token: 0x17003560 RID: 13664
		// (get) Token: 0x0600D7E8 RID: 55272 RVA: 0x002A98F7 File Offset: 0x002A7AF7
		internal sealed override string TargetName
		{
			get
			{
				return "comment";
			}
		}

		// Token: 0x17003561 RID: 13665
		// (get) Token: 0x0600D7E9 RID: 55273 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003562 RID: 13666
		// (get) Token: 0x0600D7EA RID: 55274 RVA: 0x002A98FE File Offset: 0x002A7AFE
		// (set) Token: 0x0600D7EB RID: 55275 RVA: 0x002A9906 File Offset: 0x002A7B06
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as CommentList;
			}
		}

		// Token: 0x17003563 RID: 13667
		// (get) Token: 0x0600D7EC RID: 55276 RVA: 0x002A9914 File Offset: 0x002A7B14
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.CommentList;
			}
		}

		// Token: 0x17003564 RID: 13668
		// (get) Token: 0x0600D7ED RID: 55277 RVA: 0x002A991C File Offset: 0x002A7B1C
		// (set) Token: 0x0600D7EE RID: 55278 RVA: 0x002A3296 File Offset: 0x002A1496
		public CommentList CommentList
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<CommentList>();
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

		// Token: 0x04006B0C RID: 27404
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/comments";

		// Token: 0x04006B0D RID: 27405
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.comments+xml";

		// Token: 0x04006B0E RID: 27406
		internal const string TargetPathConstant = "../comments";

		// Token: 0x04006B0F RID: 27407
		internal const string TargetNameConstant = "comment";

		// Token: 0x04006B10 RID: 27408
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B11 RID: 27409
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B12 RID: 27410
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private CommentList _rootEle;
	}
}
