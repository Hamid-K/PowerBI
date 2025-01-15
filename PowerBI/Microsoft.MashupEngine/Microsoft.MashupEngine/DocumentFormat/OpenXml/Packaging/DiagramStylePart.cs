using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002198 RID: 8600
	internal class DiagramStylePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D9D6 RID: 55766 RVA: 0x002AD048 File Offset: 0x002AB248
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (DiagramStylePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramStylePart._partConstraint = dictionary;
			}
			return DiagramStylePart._partConstraint;
		}

		// Token: 0x0600D9D7 RID: 55767 RVA: 0x002AD070 File Offset: 0x002AB270
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (DiagramStylePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				DiagramStylePart._dataPartReferenceConstraint = dictionary;
			}
			return DiagramStylePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D9D8 RID: 55768 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal DiagramStylePart()
		{
		}

		// Token: 0x1700365E RID: 13918
		// (get) Token: 0x0600D9D9 RID: 55769 RVA: 0x002AD095 File Offset: 0x002AB295
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle";
			}
		}

		// Token: 0x1700365F RID: 13919
		// (get) Token: 0x0600D9DA RID: 55770 RVA: 0x002AD09C File Offset: 0x002AB29C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.drawingml.diagramStyle+xml";
			}
		}

		// Token: 0x17003660 RID: 13920
		// (get) Token: 0x0600D9DB RID: 55771 RVA: 0x002ACC73 File Offset: 0x002AAE73
		internal sealed override string TargetPath
		{
			get
			{
				return "../graphics";
			}
		}

		// Token: 0x17003661 RID: 13921
		// (get) Token: 0x0600D9DC RID: 55772 RVA: 0x002AD0A3 File Offset: 0x002AB2A3
		internal sealed override string TargetName
		{
			get
			{
				return "quickStyle";
			}
		}

		// Token: 0x17003662 RID: 13922
		// (get) Token: 0x0600D9DD RID: 55773 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003663 RID: 13923
		// (get) Token: 0x0600D9DE RID: 55774 RVA: 0x002AD0AA File Offset: 0x002AB2AA
		// (set) Token: 0x0600D9DF RID: 55775 RVA: 0x002AD0B2 File Offset: 0x002AB2B2
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as StyleDefinition;
			}
		}

		// Token: 0x17003664 RID: 13924
		// (get) Token: 0x0600D9E0 RID: 55776 RVA: 0x002AD0C0 File Offset: 0x002AB2C0
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.StyleDefinition;
			}
		}

		// Token: 0x17003665 RID: 13925
		// (get) Token: 0x0600D9E1 RID: 55777 RVA: 0x002AD0C8 File Offset: 0x002AB2C8
		// (set) Token: 0x0600D9E2 RID: 55778 RVA: 0x002A3296 File Offset: 0x002A1496
		public StyleDefinition StyleDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<StyleDefinition>();
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

		// Token: 0x04006B91 RID: 27537
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/diagramQuickStyle";

		// Token: 0x04006B92 RID: 27538
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.drawingml.diagramStyle+xml";

		// Token: 0x04006B93 RID: 27539
		internal const string TargetPathConstant = "../graphics";

		// Token: 0x04006B94 RID: 27540
		internal const string TargetNameConstant = "quickStyle";

		// Token: 0x04006B95 RID: 27541
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B96 RID: 27542
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B97 RID: 27543
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private StyleDefinition _rootEle;
	}
}
