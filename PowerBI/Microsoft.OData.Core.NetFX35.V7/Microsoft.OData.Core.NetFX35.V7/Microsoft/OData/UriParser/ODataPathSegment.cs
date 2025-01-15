using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000146 RID: 326
	public abstract class ODataPathSegment
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x0002A0F2 File Offset: 0x000282F2
		internal ODataPathSegment(ODataPathSegment other)
		{
			this.CopyValuesFrom(other);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00002CFE File Offset: 0x00000EFE
		protected ODataPathSegment()
		{
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E7B RID: 3707
		public abstract IEdmType EdmType { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0002A101 File Offset: 0x00028301
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0002A109 File Offset: 0x00028309
		public string Identifier { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0002A112 File Offset: 0x00028312
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0002A11A File Offset: 0x0002831A
		internal bool SingleResult { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002A123 File Offset: 0x00028323
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0002A12B File Offset: 0x0002832B
		internal IEdmNavigationSource TargetEdmNavigationSource { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002A134 File Offset: 0x00028334
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0002A13C File Offset: 0x0002833C
		internal IEdmType TargetEdmType { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0002A145 File Offset: 0x00028345
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0002A14D File Offset: 0x0002834D
		internal RequestTargetKind TargetKind { get; set; }

		// Token: 0x06000E86 RID: 3718
		public abstract T TranslateWith<T>(PathSegmentTranslator<T> translator);

		// Token: 0x06000E87 RID: 3719
		public abstract void HandleWith(PathSegmentHandler handler);

		// Token: 0x06000E88 RID: 3720 RVA: 0x0002A156 File Offset: 0x00028356
		internal virtual bool Equals(ODataPathSegment other)
		{
			return this == other;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002A15C File Offset: 0x0002835C
		internal void CopyValuesFrom(ODataPathSegment other)
		{
			this.Identifier = other.Identifier;
			this.SingleResult = other.SingleResult;
			this.TargetEdmNavigationSource = other.TargetEdmNavigationSource;
			this.TargetKind = other.TargetKind;
			this.TargetEdmType = other.TargetEdmType;
		}
	}
}
