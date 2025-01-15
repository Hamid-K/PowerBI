using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbCreateRowsetRequest : OleDbRequestBase
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00002A8C File Offset: 0x00000C8C
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00002A94 File Offset: 0x00000C94
		[DataMember(Name = "commandProperties", IsRequired = true, EmitDefaultValue = false)]
		public OleDbProperty[] CommandProperties { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002A9D File Offset: 0x00000C9D
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00002AA5 File Offset: 0x00000CA5
		[DataMember(Name = "commandText", IsRequired = true, EmitDefaultValue = false)]
		public string CommandText { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002AAE File Offset: 0x00000CAE
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00002AB6 File Offset: 0x00000CB6
		[DataMember(Name = "parameters", IsRequired = false, EmitDefaultValue = false)]
		public OleDbParamCollection Parameters { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002ABF File Offset: 0x00000CBF
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002AC7 File Offset: 0x00000CC7
		[DataMember(Name = "createMultipleResults", IsRequired = false, EmitDefaultValue = false)]
		public bool CreateMultipleResults { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00002AD8 File Offset: 0x00000CD8
		[DataMember(Name = "requestId", IsRequired = false, EmitDefaultValue = false)]
		public Guid RequestId { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002AE1 File Offset: 0x00000CE1
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002B01 File Offset: 0x00000D01
		[DataMember(Name = "dialect", IsRequired = false, EmitDefaultValue = false)]
		internal Guid Dialect
		{
			get
			{
				if (!(this.m_dialect != Guid.Empty))
				{
					return DBGUID.Default;
				}
				return this.m_dialect;
			}
			set
			{
				this.m_dialect = value;
			}
		}

		// Token: 0x040000AF RID: 175
		private Guid m_dialect;
	}
}
