using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000223 RID: 547
	public abstract class ODataPathSegment : ODataAnnotatable
	{
		// Token: 0x060013C5 RID: 5061 RVA: 0x00048B27 File Offset: 0x00046D27
		internal ODataPathSegment(ODataPathSegment other)
		{
			this.CopyValuesFrom(other);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00048B36 File Offset: 0x00046D36
		protected ODataPathSegment()
		{
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060013C7 RID: 5063
		public abstract IEdmType EdmType { get; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00048B3E File Offset: 0x00046D3E
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00048B46 File Offset: 0x00046D46
		internal string Identifier { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00048B4F File Offset: 0x00046D4F
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x00048B57 File Offset: 0x00046D57
		internal bool SingleResult { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00048B60 File Offset: 0x00046D60
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00048B68 File Offset: 0x00046D68
		internal IEdmNavigationSource TargetEdmNavigationSource { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00048B71 File Offset: 0x00046D71
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00048B79 File Offset: 0x00046D79
		internal IEdmType TargetEdmType { get; set; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00048B82 File Offset: 0x00046D82
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00048B8A File Offset: 0x00046D8A
		internal RequestTargetKind TargetKind { get; set; }

		// Token: 0x060013D2 RID: 5074
		public abstract T TranslateWith<T>(PathSegmentTranslator<T> translator);

		// Token: 0x060013D3 RID: 5075
		public abstract void HandleWith(PathSegmentHandler handler);

		// Token: 0x060013D4 RID: 5076 RVA: 0x00048B93 File Offset: 0x00046D93
		internal virtual bool Equals(ODataPathSegment other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00048B9C File Offset: 0x00046D9C
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
