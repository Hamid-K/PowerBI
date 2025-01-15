using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219B RID: 8603
	internal class TableStylesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DA09 RID: 55817 RVA: 0x002AD418 File Offset: 0x002AB618
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (TableStylesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				TableStylesPart._partConstraint = dictionary;
			}
			return TableStylesPart._partConstraint;
		}

		// Token: 0x0600DA0A RID: 55818 RVA: 0x002AD440 File Offset: 0x002AB640
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (TableStylesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				TableStylesPart._dataPartReferenceConstraint = dictionary;
			}
			return TableStylesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA0B RID: 55819 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal TableStylesPart()
		{
		}

		// Token: 0x17003678 RID: 13944
		// (get) Token: 0x0600DA0C RID: 55820 RVA: 0x002AD465 File Offset: 0x002AB665
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles";
			}
		}

		// Token: 0x17003679 RID: 13945
		// (get) Token: 0x0600DA0D RID: 55821 RVA: 0x002AD46C File Offset: 0x002AB66C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.presentationml.tableStyles+xml";
			}
		}

		// Token: 0x1700367A RID: 13946
		// (get) Token: 0x0600DA0E RID: 55822 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x1700367B RID: 13947
		// (get) Token: 0x0600DA0F RID: 55823 RVA: 0x002AD473 File Offset: 0x002AB673
		internal sealed override string TargetName
		{
			get
			{
				return "tableStyles";
			}
		}

		// Token: 0x1700367C RID: 13948
		// (get) Token: 0x0600DA10 RID: 55824 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700367D RID: 13949
		// (get) Token: 0x0600DA11 RID: 55825 RVA: 0x002AD47A File Offset: 0x002AB67A
		// (set) Token: 0x0600DA12 RID: 55826 RVA: 0x002AD482 File Offset: 0x002AB682
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as TableStyleList;
			}
		}

		// Token: 0x1700367E RID: 13950
		// (get) Token: 0x0600DA13 RID: 55827 RVA: 0x002AD490 File Offset: 0x002AB690
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.TableStyleList;
			}
		}

		// Token: 0x1700367F RID: 13951
		// (get) Token: 0x0600DA14 RID: 55828 RVA: 0x002AD498 File Offset: 0x002AB698
		// (set) Token: 0x0600DA15 RID: 55829 RVA: 0x002A3296 File Offset: 0x002A1496
		public TableStyleList TableStyleList
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<TableStyleList>();
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

		// Token: 0x04006BA6 RID: 27558
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/tableStyles";

		// Token: 0x04006BA7 RID: 27559
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.presentationml.tableStyles+xml";

		// Token: 0x04006BA8 RID: 27560
		internal const string TargetPathConstant = ".";

		// Token: 0x04006BA9 RID: 27561
		internal const string TargetNameConstant = "tableStyles";

		// Token: 0x04006BAA RID: 27562
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BAB RID: 27563
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006BAC RID: 27564
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TableStyleList _rootEle;
	}
}
