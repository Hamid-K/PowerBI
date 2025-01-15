using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002152 RID: 8530
	internal class CoreFilePropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D3F2 RID: 54258 RVA: 0x002A3194 File Offset: 0x002A1394
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CoreFilePropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CoreFilePropertiesPart._partConstraint = dictionary;
			}
			return CoreFilePropertiesPart._partConstraint;
		}

		// Token: 0x0600D3F3 RID: 54259 RVA: 0x002A31BC File Offset: 0x002A13BC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CoreFilePropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CoreFilePropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return CoreFilePropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D3F4 RID: 54260 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CoreFilePropertiesPart()
		{
		}

		// Token: 0x17003327 RID: 13095
		// (get) Token: 0x0600D3F5 RID: 54261 RVA: 0x002A31E1 File Offset: 0x002A13E1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";
			}
		}

		// Token: 0x17003328 RID: 13096
		// (get) Token: 0x0600D3F6 RID: 54262 RVA: 0x002A31E8 File Offset: 0x002A13E8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-package.core-properties+xml";
			}
		}

		// Token: 0x17003329 RID: 13097
		// (get) Token: 0x0600D3F7 RID: 54263 RVA: 0x002A31EF File Offset: 0x002A13EF
		internal sealed override string TargetPath
		{
			get
			{
				return "docProps";
			}
		}

		// Token: 0x1700332A RID: 13098
		// (get) Token: 0x0600D3F8 RID: 54264 RVA: 0x002A31F6 File Offset: 0x002A13F6
		internal sealed override string TargetName
		{
			get
			{
				return "core";
			}
		}

		// Token: 0x1700332B RID: 13099
		// (get) Token: 0x0600D3F9 RID: 54265 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040069B7 RID: 27063
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties";

		// Token: 0x040069B8 RID: 27064
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-package.core-properties+xml";

		// Token: 0x040069B9 RID: 27065
		internal const string TargetPathConstant = "docProps";

		// Token: 0x040069BA RID: 27066
		internal const string TargetNameConstant = "core";

		// Token: 0x040069BB RID: 27067
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x040069BC RID: 27068
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
