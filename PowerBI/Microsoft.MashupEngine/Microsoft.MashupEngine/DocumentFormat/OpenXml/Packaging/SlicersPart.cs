using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021B5 RID: 8629
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicersPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DB57 RID: 56151 RVA: 0x002AED38 File Offset: 0x002ACF38
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SlicersPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlicersPart._partConstraint = dictionary;
			}
			return SlicersPart._partConstraint;
		}

		// Token: 0x0600DB58 RID: 56152 RVA: 0x002AED60 File Offset: 0x002ACF60
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SlicersPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SlicersPart._dataPartReferenceConstraint = dictionary;
			}
			return SlicersPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DB59 RID: 56153 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SlicersPart()
		{
		}

		// Token: 0x1700372E RID: 14126
		// (get) Token: 0x0600DB5A RID: 56154 RVA: 0x002AED85 File Offset: 0x002ACF85
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2007/relationships/slicer";
			}
		}

		// Token: 0x1700372F RID: 14127
		// (get) Token: 0x0600DB5B RID: 56155 RVA: 0x002AED8C File Offset: 0x002ACF8C
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-excel.slicer+xml";
			}
		}

		// Token: 0x17003730 RID: 14128
		// (get) Token: 0x0600DB5C RID: 56156 RVA: 0x002AED93 File Offset: 0x002ACF93
		internal sealed override string TargetPath
		{
			get
			{
				return "../slicers";
			}
		}

		// Token: 0x17003731 RID: 14129
		// (get) Token: 0x0600DB5D RID: 56157 RVA: 0x002AED9A File Offset: 0x002ACF9A
		internal sealed override string TargetName
		{
			get
			{
				return "slicer";
			}
		}

		// Token: 0x0600DB5E RID: 56158 RVA: 0x002AE8FE File Offset: 0x002ACAFE
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return version == FileFormatVersions.Office2010;
		}

		// Token: 0x17003732 RID: 14130
		// (get) Token: 0x0600DB5F RID: 56159 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003733 RID: 14131
		// (get) Token: 0x0600DB60 RID: 56160 RVA: 0x002AEDA1 File Offset: 0x002ACFA1
		// (set) Token: 0x0600DB61 RID: 56161 RVA: 0x002AEDA9 File Offset: 0x002ACFA9
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Slicers;
			}
		}

		// Token: 0x17003734 RID: 14132
		// (get) Token: 0x0600DB62 RID: 56162 RVA: 0x002AEDB7 File Offset: 0x002ACFB7
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Slicers;
			}
		}

		// Token: 0x17003735 RID: 14133
		// (get) Token: 0x0600DB63 RID: 56163 RVA: 0x002AEDBF File Offset: 0x002ACFBF
		// (set) Token: 0x0600DB64 RID: 56164 RVA: 0x002A3296 File Offset: 0x002A1496
		public Slicers Slicers
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Slicers>();
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

		// Token: 0x04006C48 RID: 27720
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2007/relationships/slicer";

		// Token: 0x04006C49 RID: 27721
		internal const string ContentTypeConstant = "application/vnd.ms-excel.slicer+xml";

		// Token: 0x04006C4A RID: 27722
		internal const string TargetPathConstant = "../slicers";

		// Token: 0x04006C4B RID: 27723
		internal const string TargetNameConstant = "slicer";

		// Token: 0x04006C4C RID: 27724
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C4D RID: 27725
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006C4E RID: 27726
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Slicers _rootEle;
	}
}
