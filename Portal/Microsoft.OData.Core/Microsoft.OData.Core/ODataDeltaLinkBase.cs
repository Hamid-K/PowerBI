using System;

namespace Microsoft.OData
{
	// Token: 0x02000076 RID: 118
	public abstract class ODataDeltaLinkBase : ODataItem
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000BA3F File Offset: 0x00009C3F
		protected ODataDeltaLinkBase(Uri source, Uri target, string relationship)
		{
			this.Source = source;
			this.Target = target;
			this.Relationship = relationship;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000BA5C File Offset: 0x00009C5C
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000BA64 File Offset: 0x00009C64
		public Uri Source { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000BA6D File Offset: 0x00009C6D
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000BA75 File Offset: 0x00009C75
		public Uri Target { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000BA7E File Offset: 0x00009C7E
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000BA86 File Offset: 0x00009C86
		public string Relationship { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000BA8F File Offset: 0x00009C8F
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000BA97 File Offset: 0x00009C97
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

		// Token: 0x040001E2 RID: 482
		private ODataDeltaSerializationInfo serializationInfo;
	}
}
