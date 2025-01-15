using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217D RID: 8573
	internal class WorkbookStylesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D741 RID: 55105 RVA: 0x002A87E4 File Offset: 0x002A69E4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorkbookStylesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookStylesPart._partConstraint = dictionary;
			}
			return WorkbookStylesPart._partConstraint;
		}

		// Token: 0x0600D742 RID: 55106 RVA: 0x002A880C File Offset: 0x002A6A0C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorkbookStylesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorkbookStylesPart._dataPartReferenceConstraint = dictionary;
			}
			return WorkbookStylesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D743 RID: 55107 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorkbookStylesPart()
		{
		}

		// Token: 0x170034FF RID: 13567
		// (get) Token: 0x0600D744 RID: 55108 RVA: 0x002A6B99 File Offset: 0x002A4D99
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";
			}
		}

		// Token: 0x17003500 RID: 13568
		// (get) Token: 0x0600D745 RID: 55109 RVA: 0x002A8831 File Offset: 0x002A6A31
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml";
			}
		}

		// Token: 0x17003501 RID: 13569
		// (get) Token: 0x0600D746 RID: 55110 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003502 RID: 13570
		// (get) Token: 0x0600D747 RID: 55111 RVA: 0x002A6BA7 File Offset: 0x002A4DA7
		internal sealed override string TargetName
		{
			get
			{
				return "styles";
			}
		}

		// Token: 0x17003503 RID: 13571
		// (get) Token: 0x0600D748 RID: 55112 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003504 RID: 13572
		// (get) Token: 0x0600D749 RID: 55113 RVA: 0x002A8838 File Offset: 0x002A6A38
		// (set) Token: 0x0600D74A RID: 55114 RVA: 0x002A8840 File Offset: 0x002A6A40
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Stylesheet;
			}
		}

		// Token: 0x17003505 RID: 13573
		// (get) Token: 0x0600D74B RID: 55115 RVA: 0x002A884E File Offset: 0x002A6A4E
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Stylesheet;
			}
		}

		// Token: 0x17003506 RID: 13574
		// (get) Token: 0x0600D74C RID: 55116 RVA: 0x002A8856 File Offset: 0x002A6A56
		// (set) Token: 0x0600D74D RID: 55117 RVA: 0x002A3296 File Offset: 0x002A1496
		public Stylesheet Stylesheet
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Stylesheet>();
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

		// Token: 0x04006AD5 RID: 27349
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles";

		// Token: 0x04006AD6 RID: 27350
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml";

		// Token: 0x04006AD7 RID: 27351
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AD8 RID: 27352
		internal const string TargetNameConstant = "styles";

		// Token: 0x04006AD9 RID: 27353
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006ADA RID: 27354
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006ADB RID: 27355
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Stylesheet _rootEle;
	}
}
