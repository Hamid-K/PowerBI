using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002172 RID: 8562
	internal class ExternalWorkbookPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6AC RID: 54956 RVA: 0x002A7FF4 File Offset: 0x002A61F4
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ExternalWorkbookPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExternalWorkbookPart._partConstraint = dictionary;
			}
			return ExternalWorkbookPart._partConstraint;
		}

		// Token: 0x0600D6AD RID: 54957 RVA: 0x002A801C File Offset: 0x002A621C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ExternalWorkbookPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ExternalWorkbookPart._dataPartReferenceConstraint = dictionary;
			}
			return ExternalWorkbookPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6AE RID: 54958 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ExternalWorkbookPart()
		{
		}

		// Token: 0x170034A4 RID: 13476
		// (get) Token: 0x0600D6AF RID: 54959 RVA: 0x002A8041 File Offset: 0x002A6241
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink";
			}
		}

		// Token: 0x170034A5 RID: 13477
		// (get) Token: 0x0600D6B0 RID: 54960 RVA: 0x002A8048 File Offset: 0x002A6248
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.externalLink+xml";
			}
		}

		// Token: 0x170034A6 RID: 13478
		// (get) Token: 0x0600D6B1 RID: 54961 RVA: 0x002A804F File Offset: 0x002A624F
		internal sealed override string TargetPath
		{
			get
			{
				return "externalReferences";
			}
		}

		// Token: 0x170034A7 RID: 13479
		// (get) Token: 0x0600D6B2 RID: 54962 RVA: 0x002A8056 File Offset: 0x002A6256
		internal sealed override string TargetName
		{
			get
			{
				return "externalReference";
			}
		}

		// Token: 0x170034A8 RID: 13480
		// (get) Token: 0x0600D6B3 RID: 54963 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034A9 RID: 13481
		// (get) Token: 0x0600D6B4 RID: 54964 RVA: 0x002A805D File Offset: 0x002A625D
		// (set) Token: 0x0600D6B5 RID: 54965 RVA: 0x002A8065 File Offset: 0x002A6265
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as ExternalLink;
			}
		}

		// Token: 0x170034AA RID: 13482
		// (get) Token: 0x0600D6B6 RID: 54966 RVA: 0x002A8073 File Offset: 0x002A6273
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.ExternalLink;
			}
		}

		// Token: 0x170034AB RID: 13483
		// (get) Token: 0x0600D6B7 RID: 54967 RVA: 0x002A807B File Offset: 0x002A627B
		// (set) Token: 0x0600D6B8 RID: 54968 RVA: 0x002A3296 File Offset: 0x002A1496
		public ExternalLink ExternalLink
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<ExternalLink>();
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

		// Token: 0x04006A88 RID: 27272
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/externalLink";

		// Token: 0x04006A89 RID: 27273
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.externalLink+xml";

		// Token: 0x04006A8A RID: 27274
		internal const string TargetPathConstant = "externalReferences";

		// Token: 0x04006A8B RID: 27275
		internal const string TargetNameConstant = "externalReference";

		// Token: 0x04006A8C RID: 27276
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A8D RID: 27277
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A8E RID: 27278
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ExternalLink _rootEle;
	}
}
