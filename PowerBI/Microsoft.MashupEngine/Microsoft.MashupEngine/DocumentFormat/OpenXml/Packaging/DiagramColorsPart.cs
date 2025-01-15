using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002195 RID: 8597
	internal class DiagramColorsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9A1 RID: 55713 RVA: 0x002ACC18 File Offset: 0x002AAE18
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DiagramColorsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramColorsPart._partConstraint = dictionary;
			}
			return DiagramColorsPart._partConstraint;
		}

		// Token: 0x0600D9A2 RID: 55714 RVA: 0x002ACC40 File Offset: 0x002AAE40
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DiagramColorsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramColorsPart._dataPartReferenceConstraint = dictionary;
			}
			return DiagramColorsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9A3 RID: 55715 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DiagramColorsPart()
		{
		}

		// Token: 0x17003642 RID: 13890
		// (get) Token: 0x0600D9A4 RID: 55716 RVA: 0x002ACC65 File Offset: 0x002AAE65
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors";
			}
		}

		// Token: 0x17003643 RID: 13891
		// (get) Token: 0x0600D9A5 RID: 55717 RVA: 0x002ACC6C File Offset: 0x002AAE6C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.diagramColors+xml";
			}
		}

		// Token: 0x17003644 RID: 13892
		// (get) Token: 0x0600D9A6 RID: 55718 RVA: 0x002ACC73 File Offset: 0x002AAE73
		internal sealed override string TargetPath
		{
			get
			{
				return "../graphics";
			}
		}

		// Token: 0x17003645 RID: 13893
		// (get) Token: 0x0600D9A7 RID: 55719 RVA: 0x002ACC7A File Offset: 0x002AAE7A
		internal sealed override string TargetName
		{
			get
			{
				return "colors";
			}
		}

		// Token: 0x17003646 RID: 13894
		// (get) Token: 0x0600D9A8 RID: 55720 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003647 RID: 13895
		// (get) Token: 0x0600D9A9 RID: 55721 RVA: 0x002ACC81 File Offset: 0x002AAE81
		// (set) Token: 0x0600D9AA RID: 55722 RVA: 0x002ACC89 File Offset: 0x002AAE89
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as ColorsDefinition;
			}
		}

		// Token: 0x17003648 RID: 13896
		// (get) Token: 0x0600D9AB RID: 55723 RVA: 0x002ACC97 File Offset: 0x002AAE97
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.ColorsDefinition;
			}
		}

		// Token: 0x17003649 RID: 13897
		// (get) Token: 0x0600D9AC RID: 55724 RVA: 0x002ACC9F File Offset: 0x002AAE9F
		// (set) Token: 0x0600D9AD RID: 55725 RVA: 0x002A3296 File Offset: 0x002A1496
		public ColorsDefinition ColorsDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<ColorsDefinition>();
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

		// Token: 0x04006B7C RID: 27516
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramColors";

		// Token: 0x04006B7D RID: 27517
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.diagramColors+xml";

		// Token: 0x04006B7E RID: 27518
		internal const string TargetPathConstant = "../graphics";

		// Token: 0x04006B7F RID: 27519
		internal const string TargetNameConstant = "colors";

		// Token: 0x04006B80 RID: 27520
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B81 RID: 27521
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B82 RID: 27522
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ColorsDefinition _rootEle;
	}
}
