using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002189 RID: 8585
	internal class PresentationPropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D87D RID: 55421 RVA: 0x002AACA0 File Offset: 0x002A8EA0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PresentationPropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PresentationPropertiesPart._partConstraint = dictionary;
			}
			return PresentationPropertiesPart._partConstraint;
		}

		// Token: 0x0600D87E RID: 55422 RVA: 0x002AACC8 File Offset: 0x002A8EC8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PresentationPropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PresentationPropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return PresentationPropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D87F RID: 55423 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal PresentationPropertiesPart()
		{
		}

		// Token: 0x170035AB RID: 13739
		// (get) Token: 0x0600D880 RID: 55424 RVA: 0x002AACED File Offset: 0x002A8EED
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps";
			}
		}

		// Token: 0x170035AC RID: 13740
		// (get) Token: 0x0600D881 RID: 55425 RVA: 0x002AACF4 File Offset: 0x002A8EF4
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.presProps+xml";
			}
		}

		// Token: 0x170035AD RID: 13741
		// (get) Token: 0x0600D882 RID: 55426 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170035AE RID: 13742
		// (get) Token: 0x0600D883 RID: 55427 RVA: 0x002AACFB File Offset: 0x002A8EFB
		internal sealed override string TargetName
		{
			get
			{
				return "presProps";
			}
		}

		// Token: 0x170035AF RID: 13743
		// (get) Token: 0x0600D884 RID: 55428 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170035B0 RID: 13744
		// (get) Token: 0x0600D885 RID: 55429 RVA: 0x002AAD02 File Offset: 0x002A8F02
		// (set) Token: 0x0600D886 RID: 55430 RVA: 0x002AAD0A File Offset: 0x002A8F0A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as PresentationProperties;
			}
		}

		// Token: 0x170035B1 RID: 13745
		// (get) Token: 0x0600D887 RID: 55431 RVA: 0x002AAD18 File Offset: 0x002A8F18
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.PresentationProperties;
			}
		}

		// Token: 0x170035B2 RID: 13746
		// (get) Token: 0x0600D888 RID: 55432 RVA: 0x002AAD20 File Offset: 0x002A8F20
		// (set) Token: 0x0600D889 RID: 55433 RVA: 0x002A3296 File Offset: 0x002A1496
		public PresentationProperties PresentationProperties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<PresentationProperties>();
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

		// Token: 0x04006B28 RID: 27432
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/presProps";

		// Token: 0x04006B29 RID: 27433
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.presProps+xml";

		// Token: 0x04006B2A RID: 27434
		internal const string TargetPathConstant = ".";

		// Token: 0x04006B2B RID: 27435
		internal const string TargetNameConstant = "presProps";

		// Token: 0x04006B2C RID: 27436
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006B2D RID: 27437
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006B2E RID: 27438
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PresentationProperties _rootEle;
	}
}
