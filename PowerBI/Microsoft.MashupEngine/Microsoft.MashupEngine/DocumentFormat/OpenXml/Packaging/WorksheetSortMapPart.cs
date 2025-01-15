using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002182 RID: 8578
	internal class WorksheetSortMapPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D7A4 RID: 55204 RVA: 0x002A91CC File Offset: 0x002A73CC
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WorksheetSortMapPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorksheetSortMapPart._partConstraint = dictionary;
			}
			return WorksheetSortMapPart._partConstraint;
		}

		// Token: 0x0600D7A5 RID: 55205 RVA: 0x002A91F4 File Offset: 0x002A73F4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WorksheetSortMapPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WorksheetSortMapPart._dataPartReferenceConstraint = dictionary;
			}
			return WorksheetSortMapPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D7A6 RID: 55206 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WorksheetSortMapPart()
		{
		}

		// Token: 0x17003537 RID: 13623
		// (get) Token: 0x0600D7A7 RID: 55207 RVA: 0x002A9219 File Offset: 0x002A7419
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/wsSortMap";
			}
		}

		// Token: 0x17003538 RID: 13624
		// (get) Token: 0x0600D7A8 RID: 55208 RVA: 0x002A9220 File Offset: 0x002A7420
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.wsSortMap+xml";
			}
		}

		// Token: 0x17003539 RID: 13625
		// (get) Token: 0x0600D7A9 RID: 55209 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700353A RID: 13626
		// (get) Token: 0x0600D7AA RID: 55210 RVA: 0x002A9227 File Offset: 0x002A7427
		internal sealed override string TargetName
		{
			get
			{
				return "wsSortMap";
			}
		}

		// Token: 0x1700353B RID: 13627
		// (get) Token: 0x0600D7AB RID: 55211 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700353C RID: 13628
		// (get) Token: 0x0600D7AC RID: 55212 RVA: 0x002A922E File Offset: 0x002A742E
		// (set) Token: 0x0600D7AD RID: 55213 RVA: 0x002A9236 File Offset: 0x002A7436
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as WorksheetSortMap;
			}
		}

		// Token: 0x1700353D RID: 13629
		// (get) Token: 0x0600D7AE RID: 55214 RVA: 0x002A9244 File Offset: 0x002A7444
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.WorksheetSortMap;
			}
		}

		// Token: 0x1700353E RID: 13630
		// (get) Token: 0x0600D7AF RID: 55215 RVA: 0x002A924C File Offset: 0x002A744C
		// (set) Token: 0x0600D7B0 RID: 55216 RVA: 0x002A3296 File Offset: 0x002A1496
		public WorksheetSortMap WorksheetSortMap
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<WorksheetSortMap>();
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

		// Token: 0x04006AF8 RID: 27384
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/wsSortMap";

		// Token: 0x04006AF9 RID: 27385
		internal const string ContentTypeConstant = "application/vnd.ms-excel.wsSortMap+xml";

		// Token: 0x04006AFA RID: 27386
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AFB RID: 27387
		internal const string TargetNameConstant = "wsSortMap";

		// Token: 0x04006AFC RID: 27388
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AFD RID: 27389
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AFE RID: 27390
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private WorksheetSortMap _rootEle;
	}
}
