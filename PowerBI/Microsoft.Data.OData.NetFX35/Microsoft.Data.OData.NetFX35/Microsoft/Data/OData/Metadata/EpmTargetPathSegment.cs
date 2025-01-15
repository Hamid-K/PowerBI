using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000213 RID: 531
	[DebuggerDisplay("EpmTargetPathSegment {SegmentName} HasContent={HasContent}")]
	internal sealed class EpmTargetPathSegment
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x000393F3 File Offset: 0x000375F3
		internal EpmTargetPathSegment()
		{
			this.subSegments = new List<EpmTargetPathSegment>();
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00039408 File Offset: 0x00037608
		internal EpmTargetPathSegment(string segmentName, string segmentNamespaceUri, string segmentNamespacePrefix, EpmTargetPathSegment parentSegment)
			: this()
		{
			this.segmentName = segmentName;
			this.segmentNamespaceUri = segmentNamespaceUri;
			this.segmentNamespacePrefix = segmentNamespacePrefix;
			this.parentSegment = parentSegment;
			if (!string.IsNullOrEmpty(segmentName) && segmentName.get_Chars(0) == '@')
			{
				this.segmentAttributeName = segmentName.Substring(1);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00039458 File Offset: 0x00037658
		internal string SegmentName
		{
			get
			{
				return this.segmentName;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00039460 File Offset: 0x00037660
		internal string AttributeName
		{
			get
			{
				return this.segmentAttributeName;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00039468 File Offset: 0x00037668
		internal string SegmentNamespaceUri
		{
			get
			{
				return this.segmentNamespaceUri;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00039470 File Offset: 0x00037670
		internal string SegmentNamespacePrefix
		{
			get
			{
				return this.segmentNamespacePrefix;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00039478 File Offset: 0x00037678
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x00039480 File Offset: 0x00037680
		internal EntityPropertyMappingInfo EpmInfo
		{
			get
			{
				return this.epmInfo;
			}
			set
			{
				this.epmInfo = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00039489 File Offset: 0x00037689
		internal bool HasContent
		{
			get
			{
				return this.EpmInfo != null;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00039497 File Offset: 0x00037697
		internal bool IsAttribute
		{
			get
			{
				return this.segmentAttributeName != null;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x000394A5 File Offset: 0x000376A5
		internal EpmTargetPathSegment ParentSegment
		{
			get
			{
				return this.parentSegment;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x000394AD File Offset: 0x000376AD
		internal List<EpmTargetPathSegment> SubSegments
		{
			get
			{
				return this.subSegments;
			}
		}

		// Token: 0x040005FB RID: 1531
		private readonly string segmentName;

		// Token: 0x040005FC RID: 1532
		private readonly string segmentAttributeName;

		// Token: 0x040005FD RID: 1533
		private readonly string segmentNamespaceUri;

		// Token: 0x040005FE RID: 1534
		private readonly string segmentNamespacePrefix;

		// Token: 0x040005FF RID: 1535
		private readonly List<EpmTargetPathSegment> subSegments;

		// Token: 0x04000600 RID: 1536
		private readonly EpmTargetPathSegment parentSegment;

		// Token: 0x04000601 RID: 1537
		private EntityPropertyMappingInfo epmInfo;
	}
}
