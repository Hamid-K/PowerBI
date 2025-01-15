using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.CustomProperties;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002154 RID: 8532
	internal class CustomFilePropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D407 RID: 54279 RVA: 0x002A32B0 File Offset: 0x002A14B0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomFilePropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomFilePropertiesPart._partConstraint = dictionary;
			}
			return CustomFilePropertiesPart._partConstraint;
		}

		// Token: 0x0600D408 RID: 54280 RVA: 0x002A32D8 File Offset: 0x002A14D8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomFilePropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomFilePropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomFilePropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D409 RID: 54281 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomFilePropertiesPart()
		{
		}

		// Token: 0x17003334 RID: 13108
		// (get) Token: 0x0600D40A RID: 54282 RVA: 0x002A32FD File Offset: 0x002A14FD
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties";
			}
		}

		// Token: 0x17003335 RID: 13109
		// (get) Token: 0x0600D40B RID: 54283 RVA: 0x002A3304 File Offset: 0x002A1504
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.custom-properties+xml";
			}
		}

		// Token: 0x17003336 RID: 13110
		// (get) Token: 0x0600D40C RID: 54284 RVA: 0x002A31EF File Offset: 0x002A13EF
		internal sealed override string TargetPath
		{
			get
			{
				return "docProps";
			}
		}

		// Token: 0x17003337 RID: 13111
		// (get) Token: 0x0600D40D RID: 54285 RVA: 0x002A330B File Offset: 0x002A150B
		internal sealed override string TargetName
		{
			get
			{
				return "custom";
			}
		}

		// Token: 0x17003338 RID: 13112
		// (get) Token: 0x0600D40E RID: 54286 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003339 RID: 13113
		// (get) Token: 0x0600D40F RID: 54287 RVA: 0x002A3312 File Offset: 0x002A1512
		// (set) Token: 0x0600D410 RID: 54288 RVA: 0x002A331A File Offset: 0x002A151A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Properties;
			}
		}

		// Token: 0x1700333A RID: 13114
		// (get) Token: 0x0600D411 RID: 54289 RVA: 0x002A3328 File Offset: 0x002A1528
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x1700333B RID: 13115
		// (get) Token: 0x0600D412 RID: 54290 RVA: 0x002A3330 File Offset: 0x002A1530
		// (set) Token: 0x0600D413 RID: 54291 RVA: 0x002A3296 File Offset: 0x002A1496
		public Properties Properties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Properties>();
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

		// Token: 0x040069C4 RID: 27076
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties";

		// Token: 0x040069C5 RID: 27077
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.custom-properties+xml";

		// Token: 0x040069C6 RID: 27078
		internal const string TargetPathConstant = "docProps";

		// Token: 0x040069C7 RID: 27079
		internal const string TargetNameConstant = "custom";

		// Token: 0x040069C8 RID: 27080
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069C9 RID: 27081
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x040069CA RID: 27082
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Properties _rootEle;
	}
}
