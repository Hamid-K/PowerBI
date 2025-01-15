using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019F RID: 415
	internal class ODataPathInfo
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x0002ED54 File Offset: 0x0002CF54
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

		// Token: 0x060010DB RID: 4315 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
		public ODataPathInfo(IEdmType targetEdmType, IEdmNavigationSource targetNavigationSource)
		{
			this.targetEdmType = targetEdmType;
			this.targetNavigationSource = targetNavigationSource;
			this.segments = new List<ODataPathSegment>();
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x0002EE15 File Offset: 0x0002D015
		public IEdmType TargetEdmType
		{
			get
			{
				return this.targetEdmType;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0002EE1D File Offset: 0x0002D01D
		public IEdmNavigationSource TargetNavigationSource
		{
			get
			{
				return this.targetNavigationSource;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0002EE25 File Offset: 0x0002D025
		public IEnumerable<ODataPathSegment> Segments
		{
			get
			{
				return this.segments;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0002EE2D File Offset: 0x0002D02D
		public IEdmStructuredType TargetStructuredType
		{
			get
			{
				return (IEdmStructuredType)this.targetEdmType;
			}
		}

		// Token: 0x040008B4 RID: 2228
		private readonly IEdmType targetEdmType;

		// Token: 0x040008B5 RID: 2229
		private readonly IEdmNavigationSource targetNavigationSource;

		// Token: 0x040008B6 RID: 2230
		private readonly IEnumerable<ODataPathSegment> segments;
	}
}
