using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B4 RID: 8628
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerCachePart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB49 RID: 56137 RVA: 0x002AEC98 File Offset: 0x002ACE98
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlicerCachePart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlicerCachePart._partConstraint = dictionary;
			}
			return SlicerCachePart._partConstraint;
		}

		// Token: 0x0600DB4A RID: 56138 RVA: 0x002AECC0 File Offset: 0x002ACEC0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlicerCachePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlicerCachePart._dataPartReferenceConstraint = dictionary;
			}
			return SlicerCachePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB4B RID: 56139 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlicerCachePart()
		{
		}

		// Token: 0x17003726 RID: 14118
		// (get) Token: 0x0600DB4C RID: 56140 RVA: 0x002AECE5 File Offset: 0x002ACEE5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/slicerCache";
			}
		}

		// Token: 0x17003727 RID: 14119
		// (get) Token: 0x0600DB4D RID: 56141 RVA: 0x002AECEC File Offset: 0x002ACEEC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.slicerCache+xml";
			}
		}

		// Token: 0x17003728 RID: 14120
		// (get) Token: 0x0600DB4E RID: 56142 RVA: 0x002AECF3 File Offset: 0x002ACEF3
		internal sealed override string TargetPath
		{
			get
			{
				return "slicerCaches";
			}
		}

		// Token: 0x17003729 RID: 14121
		// (get) Token: 0x0600DB4F RID: 56143 RVA: 0x002AECFA File Offset: 0x002ACEFA
		internal sealed override string TargetName
		{
			get
			{
				return "slicerCache";
			}
		}

		// Token: 0x0600DB50 RID: 56144 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x1700372A RID: 14122
		// (get) Token: 0x0600DB51 RID: 56145 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700372B RID: 14123
		// (get) Token: 0x0600DB52 RID: 56146 RVA: 0x002AED01 File Offset: 0x002ACF01
		// (set) Token: 0x0600DB53 RID: 56147 RVA: 0x002AED09 File Offset: 0x002ACF09
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as SlicerCacheDefinition;
			}
		}

		// Token: 0x1700372C RID: 14124
		// (get) Token: 0x0600DB54 RID: 56148 RVA: 0x002AED17 File Offset: 0x002ACF17
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.SlicerCacheDefinition;
			}
		}

		// Token: 0x1700372D RID: 14125
		// (get) Token: 0x0600DB55 RID: 56149 RVA: 0x002AED1F File Offset: 0x002ACF1F
		// (set) Token: 0x0600DB56 RID: 56150 RVA: 0x002A3296 File Offset: 0x002A1496
		public SlicerCacheDefinition SlicerCacheDefinition
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<SlicerCacheDefinition>();
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

		// Token: 0x04006C41 RID: 27713
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/slicerCache";

		// Token: 0x04006C42 RID: 27714
		internal const string ContentTypeConstant = "application/vnd.ms-excel.slicerCache+xml";

		// Token: 0x04006C43 RID: 27715
		internal const string TargetPathConstant = "slicerCaches";

		// Token: 0x04006C44 RID: 27716
		internal const string TargetNameConstant = "slicerCache";

		// Token: 0x04006C45 RID: 27717
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C46 RID: 27718
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C47 RID: 27719
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SlicerCacheDefinition _rootEle;
	}
}
