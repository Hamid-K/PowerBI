using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000161 RID: 353
	public abstract class ODataDeltaLinkBase : ODataItem
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00030F7D File Offset: 0x0002F17D
		protected ODataDeltaLinkBase(Uri source, Uri target, string relationship)
		{
			this.Source = source;
			this.Target = target;
			this.Relationship = relationship;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00030F9A File Offset: 0x0002F19A
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00030FA2 File Offset: 0x0002F1A2
		public Uri Source { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00030FAB File Offset: 0x0002F1AB
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00030FB3 File Offset: 0x0002F1B3
		public Uri Target { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00030FBC File Offset: 0x0002F1BC
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00030FC4 File Offset: 0x0002F1C4
		public string Relationship { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00030FCD File Offset: 0x0002F1CD
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00030FD5 File Offset: 0x0002F1D5
		internal ODataDeltaSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataDeltaSerializationInfo.Validate(value);
			}
		}

		// Token: 0x040005AE RID: 1454
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
