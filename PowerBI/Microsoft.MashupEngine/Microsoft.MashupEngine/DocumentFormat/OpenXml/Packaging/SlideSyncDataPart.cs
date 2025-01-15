using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200218D RID: 8589
	internal class SlideSyncDataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D932 RID: 55602 RVA: 0x002AC490 File Offset: 0x002AA690
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlideSyncDataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlideSyncDataPart._partConstraint = dictionary;
			}
			return SlideSyncDataPart._partConstraint;
		}

		// Token: 0x0600D933 RID: 55603 RVA: 0x002AC4B8 File Offset: 0x002AA6B8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlideSyncDataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlideSyncDataPart._dataPartReferenceConstraint = dictionary;
			}
			return SlideSyncDataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D934 RID: 55604 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlideSyncDataPart()
		{
		}

		// Token: 0x17003601 RID: 13825
		// (get) Token: 0x0600D935 RID: 55605 RVA: 0x002AC4DD File Offset: 0x002AA6DD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo";
			}
		}

		// Token: 0x17003602 RID: 13826
		// (get) Token: 0x0600D936 RID: 55606 RVA: 0x002AC4E4 File Offset: 0x002AA6E4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.slideUpdateInfo+xml";
			}
		}

		// Token: 0x17003603 RID: 13827
		// (get) Token: 0x0600D937 RID: 55607 RVA: 0x002AC4EB File Offset: 0x002AA6EB
		internal sealed override string TargetPath
		{
			get
			{
				return "slideUpdateInfo";
			}
		}

		// Token: 0x17003604 RID: 13828
		// (get) Token: 0x0600D938 RID: 55608 RVA: 0x002AC4EB File Offset: 0x002AA6EB
		internal sealed override string TargetName
		{
			get
			{
				return "slideUpdateInfo";
			}
		}

		// Token: 0x17003605 RID: 13829
		// (get) Token: 0x0600D939 RID: 55609 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003606 RID: 13830
		// (get) Token: 0x0600D93A RID: 55610 RVA: 0x002AC4F2 File Offset: 0x002AA6F2
		// (set) Token: 0x0600D93B RID: 55611 RVA: 0x002AC4FA File Offset: 0x002AA6FA
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as SlideSyncProperties;
			}
		}

		// Token: 0x17003607 RID: 13831
		// (get) Token: 0x0600D93C RID: 55612 RVA: 0x002AC508 File Offset: 0x002AA708
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.SlideSyncProperties;
			}
		}

		// Token: 0x17003608 RID: 13832
		// (get) Token: 0x0600D93D RID: 55613 RVA: 0x002AC510 File Offset: 0x002AA710
		// (set) Token: 0x0600D93E RID: 55614 RVA: 0x002A3296 File Offset: 0x002A1496
		public SlideSyncProperties SlideSyncProperties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<SlideSyncProperties>();
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

		// Token: 0x04006B44 RID: 27460
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/slideUpdateInfo";

		// Token: 0x04006B45 RID: 27461
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.slideUpdateInfo+xml";

		// Token: 0x04006B46 RID: 27462
		internal const string TargetPathConstant = "slideUpdateInfo";

		// Token: 0x04006B47 RID: 27463
		internal const string TargetNameConstant = "slideUpdateInfo";

		// Token: 0x04006B48 RID: 27464
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B49 RID: 27465
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B4A RID: 27466
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SlideSyncProperties _rootEle;
	}
}
