using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200217F RID: 8575
	internal class VolatileDependenciesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D75D RID: 55133 RVA: 0x002A8968 File Offset: 0x002A6B68
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (VolatileDependenciesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VolatileDependenciesPart._partConstraint = dictionary;
			}
			return VolatileDependenciesPart._partConstraint;
		}

		// Token: 0x0600D75E RID: 55134 RVA: 0x002A8990 File Offset: 0x002A6B90
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (VolatileDependenciesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VolatileDependenciesPart._dataPartReferenceConstraint = dictionary;
			}
			return VolatileDependenciesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D75F RID: 55135 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal VolatileDependenciesPart()
		{
		}

		// Token: 0x17003510 RID: 13584
		// (get) Token: 0x0600D760 RID: 55136 RVA: 0x002A89B5 File Offset: 0x002A6BB5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies";
			}
		}

		// Token: 0x17003511 RID: 13585
		// (get) Token: 0x0600D761 RID: 55137 RVA: 0x002A89BC File Offset: 0x002A6BBC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.volatileDependencies+xml";
			}
		}

		// Token: 0x17003512 RID: 13586
		// (get) Token: 0x0600D762 RID: 55138 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003513 RID: 13587
		// (get) Token: 0x0600D763 RID: 55139 RVA: 0x002A89C3 File Offset: 0x002A6BC3
		internal sealed override string TargetName
		{
			get
			{
				return "volatileDependencies";
			}
		}

		// Token: 0x17003514 RID: 13588
		// (get) Token: 0x0600D764 RID: 55140 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003515 RID: 13589
		// (get) Token: 0x0600D765 RID: 55141 RVA: 0x002A89CA File Offset: 0x002A6BCA
		// (set) Token: 0x0600D766 RID: 55142 RVA: 0x002A89D2 File Offset: 0x002A6BD2
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as VolatileTypes;
			}
		}

		// Token: 0x17003516 RID: 13590
		// (get) Token: 0x0600D767 RID: 55143 RVA: 0x002A89E0 File Offset: 0x002A6BE0
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.VolatileTypes;
			}
		}

		// Token: 0x17003517 RID: 13591
		// (get) Token: 0x0600D768 RID: 55144 RVA: 0x002A89E8 File Offset: 0x002A6BE8
		// (set) Token: 0x0600D769 RID: 55145 RVA: 0x002A3296 File Offset: 0x002A1496
		public VolatileTypes VolatileTypes
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<VolatileTypes>();
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

		// Token: 0x04006AE3 RID: 27363
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/volatileDependencies";

		// Token: 0x04006AE4 RID: 27364
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.volatileDependencies+xml";

		// Token: 0x04006AE5 RID: 27365
		internal const string TargetPathConstant = ".";

		// Token: 0x04006AE6 RID: 27366
		internal const string TargetNameConstant = "volatileDependencies";

		// Token: 0x04006AE7 RID: 27367
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006AE8 RID: 27368
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006AE9 RID: 27369
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VolatileTypes _rootEle;
	}
}
