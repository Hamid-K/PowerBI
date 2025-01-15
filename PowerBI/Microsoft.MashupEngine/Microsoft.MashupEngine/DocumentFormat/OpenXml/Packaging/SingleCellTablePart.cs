using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217C RID: 8572
	internal class SingleCellTablePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D734 RID: 55092 RVA: 0x002A8744 File Offset: 0x002A6944
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SingleCellTablePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SingleCellTablePart._partConstraint = dictionary;
			}
			return SingleCellTablePart._partConstraint;
		}

		// Token: 0x0600D735 RID: 55093 RVA: 0x002A876C File Offset: 0x002A696C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SingleCellTablePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SingleCellTablePart._dataPartReferenceConstraint = dictionary;
			}
			return SingleCellTablePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D736 RID: 55094 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SingleCellTablePart()
		{
		}

		// Token: 0x170034F7 RID: 13559
		// (get) Token: 0x0600D737 RID: 55095 RVA: 0x002A8791 File Offset: 0x002A6991
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells";
			}
		}

		// Token: 0x170034F8 RID: 13560
		// (get) Token: 0x0600D738 RID: 55096 RVA: 0x002A8798 File Offset: 0x002A6998
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.tableSingleCells+xml";
			}
		}

		// Token: 0x170034F9 RID: 13561
		// (get) Token: 0x0600D739 RID: 55097 RVA: 0x002A879F File Offset: 0x002A699F
		internal sealed override string TargetPath
		{
			get
			{
				return "../tables";
			}
		}

		// Token: 0x170034FA RID: 13562
		// (get) Token: 0x0600D73A RID: 55098 RVA: 0x002A87A6 File Offset: 0x002A69A6
		internal sealed override string TargetName
		{
			get
			{
				return "tableSingleCells";
			}
		}

		// Token: 0x170034FB RID: 13563
		// (get) Token: 0x0600D73B RID: 55099 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034FC RID: 13564
		// (get) Token: 0x0600D73C RID: 55100 RVA: 0x002A87AD File Offset: 0x002A69AD
		// (set) Token: 0x0600D73D RID: 55101 RVA: 0x002A87B5 File Offset: 0x002A69B5
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as SingleXmlCells;
			}
		}

		// Token: 0x170034FD RID: 13565
		// (get) Token: 0x0600D73E RID: 55102 RVA: 0x002A87C3 File Offset: 0x002A69C3
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.SingleXmlCells;
			}
		}

		// Token: 0x170034FE RID: 13566
		// (get) Token: 0x0600D73F RID: 55103 RVA: 0x002A87CB File Offset: 0x002A69CB
		// (set) Token: 0x0600D740 RID: 55104 RVA: 0x002A3296 File Offset: 0x002A1496
		public SingleXmlCells SingleXmlCells
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<SingleXmlCells>();
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

		// Token: 0x04006ACE RID: 27342
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableSingleCells";

		// Token: 0x04006ACF RID: 27343
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.tableSingleCells+xml";

		// Token: 0x04006AD0 RID: 27344
		internal const string TargetPathConstant = "../tables";

		// Token: 0x04006AD1 RID: 27345
		internal const string TargetNameConstant = "tableSingleCells";

		// Token: 0x04006AD2 RID: 27346
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AD3 RID: 27347
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AD4 RID: 27348
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SingleXmlCells _rootEle;
	}
}
