using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000103 RID: 259
	internal class ODataPathInfo
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x00025B48 File Offset: 0x00023D48
		public ODataPathInfo(ODataPath odataPath)
		{
			ODataPathSegment odataPathSegment = odataPath.LastSegment;
			IEnumerator<ODataPathSegment> enumerator = odataPath.GetEnumerator();
			int num = 0;
			while (++num < odataPath.Count && enumerator.MoveNext())
			{
			}
			ODataPathSegment odataPathSegment2 = enumerator.Current;
			if (odataPathSegment != null)
			{
				if (odataPathSegment is KeySegment || odataPathSegment is CountSegment)
				{
					odataPathSegment = odataPathSegment2;
				}
				this.targetNavigationSource = odataPathSegment.TargetEdmNavigationSource;
				this.targetEdmType = odataPathSegment.TargetEdmType;
				if (this.targetEdmType != null)
				{
					IEdmCollectionType edmCollectionType = this.targetEdmType as IEdmCollectionType;
					if (edmCollectionType != null)
					{
						this.targetEdmType = edmCollectionType.ElementType.Definition;
					}
				}
			}
			this.segments = odataPath;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00025BE8 File Offset: 0x00023DE8
		public ODataPathInfo(IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource)
		{
			this.targetEdmType = targetEdmType;
			this.targetNavigationSource = targetNavigationSource;
			this.segments = new List<ODataPathSegment>();
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00025C09 File Offset: 0x00023E09
		public IEdmType TargetEdmType
		{
			get
			{
				return this.targetEdmType;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00025C11 File Offset: 0x00023E11
		public IEdmNavigationSource TargetNavigationSource
		{
			get
			{
				return this.targetNavigationSource;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00025C19 File Offset: 0x00023E19
		public IEnumerable<ODataPathSegment> Segments
		{
			get
			{
				return this.segments;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00025C21 File Offset: 0x00023E21
		public IEdmStructuredType TargetStructuredType
		{
			get
			{
				return (IEdmStructuredType)this.targetEdmType;
			}
		}

		// Token: 0x04000766 RID: 1894
		private readonly IEdmType targetEdmType;

		// Token: 0x04000767 RID: 1895
		private readonly IEdmNavigationSource targetNavigationSource;

		// Token: 0x04000768 RID: 1896
		private readonly IEnumerable<ODataPathSegment> segments;
	}
}
