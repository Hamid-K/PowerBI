using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000061 RID: 97
	[DataContract]
	public sealed class MashupDataSourceDetails
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00004434 File Offset: 0x00002634
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000443C File Offset: 0x0000263C
		[Required]
		[DataMember(Name = "kind", Order = 0)]
		public string Kind
		{
			get
			{
				return this.m_kind;
			}
			set
			{
				this.m_kind = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00004445 File Offset: 0x00002645
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000444D File Offset: 0x0000264D
		[Required]
		[DataMember(Name = "path", Order = 10)]
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00004456 File Offset: 0x00002656
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000445E File Offset: 0x0000265E
		[DataMember(Name = "authentication", Order = 20)]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Authentication
		{
			get
			{
				return this.m_authentication;
			}
			set
			{
				this.m_authentication = value;
			}
		}

		// Token: 0x04000238 RID: 568
		private string m_kind;

		// Token: 0x04000239 RID: 569
		private string m_path;

		// Token: 0x0400023A RID: 570
		private string m_authentication;
	}
}
