using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200218F RID: 8591
	internal class ViewPropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D94C RID: 55628 RVA: 0x002AC5C8 File Offset: 0x002AA7C8
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ViewPropertiesPart._partConstraint == null)
			{
				ViewPropertiesPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide",
					new PartConstraintRule("SlidePart", "application/vnd.openxmlformats-officedocument.presentationml.slide+xml", false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return ViewPropertiesPart._partConstraint;
		}

		// Token: 0x0600D94D RID: 55629 RVA: 0x002AC60C File Offset: 0x002AA80C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ViewPropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ViewPropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return ViewPropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D94E RID: 55630 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ViewPropertiesPart()
		{
		}

		// Token: 0x0600D94F RID: 55631 RVA: 0x002AC634 File Offset: 0x002AA834
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slide")
			{
				return new SlidePart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003611 RID: 13841
		// (get) Token: 0x0600D950 RID: 55632 RVA: 0x002AC677 File Offset: 0x002AA877
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps";
			}
		}

		// Token: 0x17003612 RID: 13842
		// (get) Token: 0x0600D951 RID: 55633 RVA: 0x002AC67E File Offset: 0x002AA87E
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.viewProps+xml";
			}
		}

		// Token: 0x17003613 RID: 13843
		// (get) Token: 0x0600D952 RID: 55634 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003614 RID: 13844
		// (get) Token: 0x0600D953 RID: 55635 RVA: 0x002AC685 File Offset: 0x002AA885
		internal sealed override string TargetName
		{
			get
			{
				return "viewProps";
			}
		}

		// Token: 0x17003615 RID: 13845
		// (get) Token: 0x0600D954 RID: 55636 RVA: 0x002A9797 File Offset: 0x002A7997
		public IEnumerable<SlidePart> SlideParts
		{
			get
			{
				return base.GetPartsOfType<SlidePart>();
			}
		}

		// Token: 0x17003616 RID: 13846
		// (get) Token: 0x0600D955 RID: 55637 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003617 RID: 13847
		// (get) Token: 0x0600D956 RID: 55638 RVA: 0x002AC68C File Offset: 0x002AA88C
		// (set) Token: 0x0600D957 RID: 55639 RVA: 0x002AC694 File Offset: 0x002AA894
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as ViewProperties;
			}
		}

		// Token: 0x17003618 RID: 13848
		// (get) Token: 0x0600D958 RID: 55640 RVA: 0x002AC6A2 File Offset: 0x002AA8A2
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.ViewProperties;
			}
		}

		// Token: 0x17003619 RID: 13849
		// (get) Token: 0x0600D959 RID: 55641 RVA: 0x002AC6AA File Offset: 0x002AA8AA
		// (set) Token: 0x0600D95A RID: 55642 RVA: 0x002A3296 File Offset: 0x002A1496
		public ViewProperties ViewProperties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<ViewProperties>();
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

		// Token: 0x04006B52 RID: 27474
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/viewProps";

		// Token: 0x04006B53 RID: 27475
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.viewProps+xml";

		// Token: 0x04006B54 RID: 27476
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B55 RID: 27477
		internal const string TargetNameConstant = "viewProps";

		// Token: 0x04006B56 RID: 27478
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B57 RID: 27479
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B58 RID: 27480
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ViewProperties _rootEle;
	}
}
