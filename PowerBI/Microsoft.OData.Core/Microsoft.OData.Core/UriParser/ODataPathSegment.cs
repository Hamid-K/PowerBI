using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000192 RID: 402
	public abstract class ODataPathSegment
	{
		// Token: 0x0600138A RID: 5002 RVA: 0x00039F06 File Offset: 0x00038106
		internal ODataPathSegment(ODataPathSegment other)
		{
			this.CopyValuesFrom(other);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000036A9 File Offset: 0x000018A9
		protected ODataPathSegment()
		{
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600138C RID: 5004
		public abstract IEdmType EdmType { get; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00039F15 File Offset: 0x00038115
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x00039F1D File Offset: 0x0003811D
		public string Identifier { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00039F26 File Offset: 0x00038126
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x00039F2E File Offset: 0x0003812E
		internal bool SingleResult { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00039F37 File Offset: 0x00038137
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x00039F3F File Offset: 0x0003813F
		internal IEdmNavigationSource TargetEdmNavigationSource { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00039F48 File Offset: 0x00038148
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x00039F50 File Offset: 0x00038150
		internal IEdmType TargetEdmType { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00039F59 File Offset: 0x00038159
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00039F61 File Offset: 0x00038161
		internal RequestTargetKind TargetKind { get; set; }

		// Token: 0x06001397 RID: 5015
		public abstract T TranslateWith<T>(PathSegmentTranslator<T> translator);

		// Token: 0x06001398 RID: 5016
		public abstract void HandleWith(PathSegmentHandler handler);

		// Token: 0x06001399 RID: 5017 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		internal virtual bool Equals(ODataPathSegment other)
		{
			return this == other;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00039F6A File Offset: 0x0003816A
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
