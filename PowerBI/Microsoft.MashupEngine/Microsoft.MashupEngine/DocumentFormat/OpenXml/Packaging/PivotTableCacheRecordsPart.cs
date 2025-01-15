using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002176 RID: 8566
	internal class PivotTableCacheRecordsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6E4 RID: 55012 RVA: 0x002A833C File Offset: 0x002A653C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (PivotTableCacheRecordsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PivotTableCacheRecordsPart._partConstraint = dictionary;
			}
			return PivotTableCacheRecordsPart._partConstraint;
		}

		// Token: 0x0600D6E5 RID: 55013 RVA: 0x002A8364 File Offset: 0x002A6564
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (PivotTableCacheRecordsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				PivotTableCacheRecordsPart._dataPartReferenceConstraint = dictionary;
			}
			return PivotTableCacheRecordsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6E6 RID: 55014 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal PivotTableCacheRecordsPart()
		{
		}

		// Token: 0x170034C6 RID: 13510
		// (get) Token: 0x0600D6E7 RID: 55015 RVA: 0x002A8389 File Offset: 0x002A6589
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords";
			}
		}

		// Token: 0x170034C7 RID: 13511
		// (get) Token: 0x0600D6E8 RID: 55016 RVA: 0x002A8390 File Offset: 0x002A6590
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheRecords+xml";
			}
		}

		// Token: 0x170034C8 RID: 13512
		// (get) Token: 0x0600D6E9 RID: 55017 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170034C9 RID: 13513
		// (get) Token: 0x0600D6EA RID: 55018 RVA: 0x002A8397 File Offset: 0x002A6597
		internal sealed override string TargetName
		{
			get
			{
				return "pivotCacheRecords";
			}
		}

		// Token: 0x170034CA RID: 13514
		// (get) Token: 0x0600D6EB RID: 55019 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034CB RID: 13515
		// (get) Token: 0x0600D6EC RID: 55020 RVA: 0x002A839E File Offset: 0x002A659E
		// (set) Token: 0x0600D6ED RID: 55021 RVA: 0x002A83A6 File Offset: 0x002A65A6
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as PivotCacheRecords;
			}
		}

		// Token: 0x170034CC RID: 13516
		// (get) Token: 0x0600D6EE RID: 55022 RVA: 0x002A83B4 File Offset: 0x002A65B4
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.PivotCacheRecords;
			}
		}

		// Token: 0x170034CD RID: 13517
		// (get) Token: 0x0600D6EF RID: 55023 RVA: 0x002A83BC File Offset: 0x002A65BC
		// (set) Token: 0x0600D6F0 RID: 55024 RVA: 0x002A3296 File Offset: 0x002A1496
		public PivotCacheRecords PivotCacheRecords
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<PivotCacheRecords>();
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

		// Token: 0x04006AA4 RID: 27300
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords";

		// Token: 0x04006AA5 RID: 27301
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheRecords+xml";

		// Token: 0x04006AA6 RID: 27302
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AA7 RID: 27303
		internal const string TargetNameConstant = "pivotCacheRecords";

		// Token: 0x04006AA8 RID: 27304
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AA9 RID: 27305
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AAA RID: 27306
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PivotCacheRecords _rootEle;
	}
}
