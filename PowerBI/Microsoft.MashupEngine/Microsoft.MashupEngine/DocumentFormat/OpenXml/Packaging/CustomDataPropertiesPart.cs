using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B2 RID: 8626
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomDataPropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB30 RID: 56112 RVA: 0x002AEB34 File Offset: 0x002ACD34
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomDataPropertiesPart._partConstraint == null)
			{
				CustomDataPropertiesPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.microsoft.com/office/2007/relationships/customData",
					new PartConstraintRule("CustomDataPart", "application/binary", false, false, FileFormatVersions.Office2010)
				} };
			}
			return CustomDataPropertiesPart._partConstraint;
		}

		// Token: 0x0600DB31 RID: 56113 RVA: 0x002AEB78 File Offset: 0x002ACD78
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomDataPropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomDataPropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomDataPropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB32 RID: 56114 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomDataPropertiesPart()
		{
		}

		// Token: 0x0600DB33 RID: 56115 RVA: 0x002AEBA0 File Offset: 0x002ACDA0
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.microsoft.com/office/2007/relationships/customData")
			{
				return new CustomDataPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x17003718 RID: 14104
		// (get) Token: 0x0600DB34 RID: 56116 RVA: 0x002AEBE3 File Offset: 0x002ACDE3
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/customDataProps";
			}
		}

		// Token: 0x17003719 RID: 14105
		// (get) Token: 0x0600DB35 RID: 56117 RVA: 0x002AEBEA File Offset: 0x002ACDEA
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.customDataProperties+xml";
			}
		}

		// Token: 0x1700371A RID: 14106
		// (get) Token: 0x0600DB36 RID: 56118 RVA: 0x002AEBF1 File Offset: 0x002ACDF1
		internal sealed override string TargetPath
		{
			get
			{
				return "customData";
			}
		}

		// Token: 0x1700371B RID: 14107
		// (get) Token: 0x0600DB37 RID: 56119 RVA: 0x002AEBF8 File Offset: 0x002ACDF8
		internal sealed override string TargetName
		{
			get
			{
				return "customDataProps";
			}
		}

		// Token: 0x1700371C RID: 14108
		// (get) Token: 0x0600DB38 RID: 56120 RVA: 0x002AEBFF File Offset: 0x002ACDFF
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public CustomDataPart CustomDataPart
		{
			get
			{
				return base.GetSubPartOfType<CustomDataPart>();
			}
		}

		// Token: 0x0600DB39 RID: 56121 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x1700371D RID: 14109
		// (get) Token: 0x0600DB3A RID: 56122 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700371E RID: 14110
		// (get) Token: 0x0600DB3B RID: 56123 RVA: 0x002AEC07 File Offset: 0x002ACE07
		// (set) Token: 0x0600DB3C RID: 56124 RVA: 0x002AEC0F File Offset: 0x002ACE0F
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as DatastoreItem;
			}
		}

		// Token: 0x1700371F RID: 14111
		// (get) Token: 0x0600DB3D RID: 56125 RVA: 0x002AEC1D File Offset: 0x002ACE1D
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.DatastoreItem;
			}
		}

		// Token: 0x17003720 RID: 14112
		// (get) Token: 0x0600DB3E RID: 56126 RVA: 0x002AEC25 File Offset: 0x002ACE25
		// (set) Token: 0x0600DB3F RID: 56127 RVA: 0x002A3296 File Offset: 0x002A1496
		public DatastoreItem DatastoreItem
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<DatastoreItem>();
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

		// Token: 0x04006C34 RID: 27700
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/customDataProps";

		// Token: 0x04006C35 RID: 27701
		internal const string ContentTypeConstant = "application/vnd.ms-excel.customDataProperties+xml";

		// Token: 0x04006C36 RID: 27702
		internal const string TargetPathConstant = "customData";

		// Token: 0x04006C37 RID: 27703
		internal const string TargetNameConstant = "customDataProps";

		// Token: 0x04006C38 RID: 27704
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C39 RID: 27705
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C3A RID: 27706
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DatastoreItem _rootEle;
	}
}
