using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002167 RID: 8551
	internal class WebSettingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D5E1 RID: 54753 RVA: 0x002A6BB0 File Offset: 0x002A4DB0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WebSettingsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WebSettingsPart._partConstraint = dictionary;
			}
			return WebSettingsPart._partConstraint;
		}

		// Token: 0x0600D5E2 RID: 54754 RVA: 0x002A6BD8 File Offset: 0x002A4DD8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WebSettingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WebSettingsPart._dataPartReferenceConstraint = dictionary;
			}
			return WebSettingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D5E3 RID: 54755 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WebSettingsPart()
		{
		}

		// Token: 0x17003429 RID: 13353
		// (get) Token: 0x0600D5E4 RID: 54756 RVA: 0x002A6BFD File Offset: 0x002A4DFD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings";
			}
		}

		// Token: 0x1700342A RID: 13354
		// (get) Token: 0x0600D5E5 RID: 54757 RVA: 0x002A6C04 File Offset: 0x002A4E04
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml";
			}
		}

		// Token: 0x1700342B RID: 13355
		// (get) Token: 0x0600D5E6 RID: 54758 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700342C RID: 13356
		// (get) Token: 0x0600D5E7 RID: 54759 RVA: 0x002A6C0B File Offset: 0x002A4E0B
		internal sealed override string TargetName
		{
			get
			{
				return "webSettings";
			}
		}

		// Token: 0x1700342D RID: 13357
		// (get) Token: 0x0600D5E8 RID: 54760 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700342E RID: 13358
		// (get) Token: 0x0600D5E9 RID: 54761 RVA: 0x002A6C12 File Offset: 0x002A4E12
		// (set) Token: 0x0600D5EA RID: 54762 RVA: 0x002A6C1A File Offset: 0x002A4E1A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as WebSettings;
			}
		}

		// Token: 0x1700342F RID: 13359
		// (get) Token: 0x0600D5EB RID: 54763 RVA: 0x002A6C28 File Offset: 0x002A4E28
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.WebSettings;
			}
		}

		// Token: 0x17003430 RID: 13360
		// (get) Token: 0x0600D5EC RID: 54764 RVA: 0x002A6C30 File Offset: 0x002A4E30
		// (set) Token: 0x0600D5ED RID: 54765 RVA: 0x002A3296 File Offset: 0x002A1496
		public WebSettings WebSettings
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<WebSettings>();
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

		// Token: 0x04006A3E RID: 27198
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings";

		// Token: 0x04006A3F RID: 27199
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml";

		// Token: 0x04006A40 RID: 27200
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A41 RID: 27201
		internal const string TargetNameConstant = "webSettings";

		// Token: 0x04006A42 RID: 27202
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A43 RID: 27203
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A44 RID: 27204
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WebSettings _rootEle;
	}
}
