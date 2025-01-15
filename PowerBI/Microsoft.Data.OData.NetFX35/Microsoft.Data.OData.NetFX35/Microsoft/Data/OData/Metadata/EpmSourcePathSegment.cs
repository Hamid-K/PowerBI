using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000211 RID: 529
	internal sealed class EpmSourcePathSegment
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x000390A5 File Offset: 0x000372A5
		internal EpmSourcePathSegment()
			: this(null)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000390AE File Offset: 0x000372AE
		internal EpmSourcePathSegment(string propertyName)
		{
			this.propertyName = propertyName;
			this.subProperties = new List<EpmSourcePathSegment>();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x000390C8 File Offset: 0x000372C8
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000390D0 File Offset: 0x000372D0
		internal List<EpmSourcePathSegment> SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x000390D8 File Offset: 0x000372D8
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x000390E0 File Offset: 0x000372E0
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

		// Token: 0x040005F6 RID: 1526
		private readonly string propertyName;

		// Token: 0x040005F7 RID: 1527
		private readonly List<EpmSourcePathSegment> subProperties;

		// Token: 0x040005F8 RID: 1528
		private EntityPropertyMappingInfo epmInfo;
	}
}
