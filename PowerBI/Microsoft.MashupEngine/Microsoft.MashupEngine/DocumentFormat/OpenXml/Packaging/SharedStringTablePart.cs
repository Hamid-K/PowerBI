using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002178 RID: 8568
	internal class SharedStringTablePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6FE RID: 55038 RVA: 0x002A8474 File Offset: 0x002A6674
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SharedStringTablePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SharedStringTablePart._partConstraint = dictionary;
			}
			return SharedStringTablePart._partConstraint;
		}

		// Token: 0x0600D6FF RID: 55039 RVA: 0x002A849C File Offset: 0x002A669C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SharedStringTablePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SharedStringTablePart._dataPartReferenceConstraint = dictionary;
			}
			return SharedStringTablePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D700 RID: 55040 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SharedStringTablePart()
		{
		}

		// Token: 0x170034D6 RID: 13526
		// (get) Token: 0x0600D701 RID: 55041 RVA: 0x002A84C1 File Offset: 0x002A66C1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings";
			}
		}

		// Token: 0x170034D7 RID: 13527
		// (get) Token: 0x0600D702 RID: 55042 RVA: 0x002A84C8 File Offset: 0x002A66C8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml";
			}
		}

		// Token: 0x170034D8 RID: 13528
		// (get) Token: 0x0600D703 RID: 55043 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170034D9 RID: 13529
		// (get) Token: 0x0600D704 RID: 55044 RVA: 0x002A84CF File Offset: 0x002A66CF
		internal sealed override string TargetName
		{
			get
			{
				return "sharedStrings";
			}
		}

		// Token: 0x170034DA RID: 13530
		// (get) Token: 0x0600D705 RID: 55045 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034DB RID: 13531
		// (get) Token: 0x0600D706 RID: 55046 RVA: 0x002A84D6 File Offset: 0x002A66D6
		// (set) Token: 0x0600D707 RID: 55047 RVA: 0x002A84DE File Offset: 0x002A66DE
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as SharedStringTable;
			}
		}

		// Token: 0x170034DC RID: 13532
		// (get) Token: 0x0600D708 RID: 55048 RVA: 0x002A84EC File Offset: 0x002A66EC
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.SharedStringTable;
			}
		}

		// Token: 0x170034DD RID: 13533
		// (get) Token: 0x0600D709 RID: 55049 RVA: 0x002A84F4 File Offset: 0x002A66F4
		// (set) Token: 0x0600D70A RID: 55050 RVA: 0x002A3296 File Offset: 0x002A1496
		public SharedStringTable SharedStringTable
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<SharedStringTable>();
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

		// Token: 0x04006AB2 RID: 27314
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings";

		// Token: 0x04006AB3 RID: 27315
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml";

		// Token: 0x04006AB4 RID: 27316
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AB5 RID: 27317
		internal const string TargetNameConstant = "sharedStrings";

		// Token: 0x04006AB6 RID: 27318
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AB7 RID: 27319
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AB8 RID: 27320
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SharedStringTable _rootEle;
	}
}
