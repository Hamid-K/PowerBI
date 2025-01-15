using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B1 RID: 8625
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ControlPropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB22 RID: 56098 RVA: 0x002AEA94 File Offset: 0x002ACC94
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (ControlPropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ControlPropertiesPart._partConstraint = dictionary;
			}
			return ControlPropertiesPart._partConstraint;
		}

		// Token: 0x0600DB23 RID: 56099 RVA: 0x002AEABC File Offset: 0x002ACCBC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (ControlPropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				ControlPropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return ControlPropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB24 RID: 56100 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal ControlPropertiesPart()
		{
		}

		// Token: 0x17003710 RID: 14096
		// (get) Token: 0x0600DB25 RID: 56101 RVA: 0x002AEAE1 File Offset: 0x002ACCE1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp";
			}
		}

		// Token: 0x17003711 RID: 14097
		// (get) Token: 0x0600DB26 RID: 56102 RVA: 0x002AEAE8 File Offset: 0x002ACCE8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.controlproperties+xml";
			}
		}

		// Token: 0x17003712 RID: 14098
		// (get) Token: 0x0600DB27 RID: 56103 RVA: 0x002AEAEF File Offset: 0x002ACCEF
		internal sealed override string TargetPath
		{
			get
			{
				return "../ctrlProps";
			}
		}

		// Token: 0x17003713 RID: 14099
		// (get) Token: 0x0600DB28 RID: 56104 RVA: 0x002AEAF6 File Offset: 0x002ACCF6
		internal sealed override string TargetName
		{
			get
			{
				return "ctrlProp";
			}
		}

		// Token: 0x0600DB29 RID: 56105 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x17003714 RID: 14100
		// (get) Token: 0x0600DB2A RID: 56106 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003715 RID: 14101
		// (get) Token: 0x0600DB2B RID: 56107 RVA: 0x002AEAFD File Offset: 0x002ACCFD
		// (set) Token: 0x0600DB2C RID: 56108 RVA: 0x002AEB05 File Offset: 0x002ACD05
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as FormControlProperties;
			}
		}

		// Token: 0x17003716 RID: 14102
		// (get) Token: 0x0600DB2D RID: 56109 RVA: 0x002AEB13 File Offset: 0x002ACD13
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.FormControlProperties;
			}
		}

		// Token: 0x17003717 RID: 14103
		// (get) Token: 0x0600DB2E RID: 56110 RVA: 0x002AEB1B File Offset: 0x002ACD1B
		// (set) Token: 0x0600DB2F RID: 56111 RVA: 0x002A3296 File Offset: 0x002A1496
		public FormControlProperties FormControlProperties
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<FormControlProperties>();
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

		// Token: 0x04006C2D RID: 27693
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/ctrlProp";

		// Token: 0x04006C2E RID: 27694
		internal const string ContentTypeConstant = "application/vnd.ms-excel.controlproperties+xml";

		// Token: 0x04006C2F RID: 27695
		internal const string TargetPathConstant = "../ctrlProps";

		// Token: 0x04006C30 RID: 27696
		internal const string TargetNameConstant = "ctrlProp";

		// Token: 0x04006C31 RID: 27697
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C32 RID: 27698
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C33 RID: 27699
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private FormControlProperties _rootEle;
	}
}
