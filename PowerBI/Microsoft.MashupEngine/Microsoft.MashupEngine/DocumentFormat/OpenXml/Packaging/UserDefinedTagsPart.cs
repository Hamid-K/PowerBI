using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200218E RID: 8590
	internal class UserDefinedTagsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D93F RID: 55615 RVA: 0x002AC528 File Offset: 0x002AA728
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (UserDefinedTagsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				UserDefinedTagsPart._partConstraint = dictionary;
			}
			return UserDefinedTagsPart._partConstraint;
		}

		// Token: 0x0600D940 RID: 55616 RVA: 0x002AC550 File Offset: 0x002AA750
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (UserDefinedTagsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				UserDefinedTagsPart._dataPartReferenceConstraint = dictionary;
			}
			return UserDefinedTagsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D941 RID: 55617 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal UserDefinedTagsPart()
		{
		}

		// Token: 0x17003609 RID: 13833
		// (get) Token: 0x0600D942 RID: 55618 RVA: 0x002AC575 File Offset: 0x002AA775
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags";
			}
		}

		// Token: 0x1700360A RID: 13834
		// (get) Token: 0x0600D943 RID: 55619 RVA: 0x002AC57C File Offset: 0x002AA77C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.tags+xml";
			}
		}

		// Token: 0x1700360B RID: 13835
		// (get) Token: 0x0600D944 RID: 55620 RVA: 0x002AC583 File Offset: 0x002AA783
		internal sealed override string TargetPath
		{
			get
			{
				return "tags";
			}
		}

		// Token: 0x1700360C RID: 13836
		// (get) Token: 0x0600D945 RID: 55621 RVA: 0x002AC58A File Offset: 0x002AA78A
		internal sealed override string TargetName
		{
			get
			{
				return "tag";
			}
		}

		// Token: 0x1700360D RID: 13837
		// (get) Token: 0x0600D946 RID: 55622 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700360E RID: 13838
		// (get) Token: 0x0600D947 RID: 55623 RVA: 0x002AC591 File Offset: 0x002AA791
		// (set) Token: 0x0600D948 RID: 55624 RVA: 0x002AC599 File Offset: 0x002AA799
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as TagList;
			}
		}

		// Token: 0x1700360F RID: 13839
		// (get) Token: 0x0600D949 RID: 55625 RVA: 0x002AC5A7 File Offset: 0x002AA7A7
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.TagList;
			}
		}

		// Token: 0x17003610 RID: 13840
		// (get) Token: 0x0600D94A RID: 55626 RVA: 0x002AC5AF File Offset: 0x002AA7AF
		// (set) Token: 0x0600D94B RID: 55627 RVA: 0x002A3296 File Offset: 0x002A1496
		public TagList TagList
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<TagList>();
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

		// Token: 0x04006B4B RID: 27467
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tags";

		// Token: 0x04006B4C RID: 27468
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.tags+xml";

		// Token: 0x04006B4D RID: 27469
		internal const string TargetPathConstant = "tags";

		// Token: 0x04006B4E RID: 27470
		internal const string TargetNameConstant = "tag";

		// Token: 0x04006B4F RID: 27471
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B50 RID: 27472
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B51 RID: 27473
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TagList _rootEle;
	}
}
