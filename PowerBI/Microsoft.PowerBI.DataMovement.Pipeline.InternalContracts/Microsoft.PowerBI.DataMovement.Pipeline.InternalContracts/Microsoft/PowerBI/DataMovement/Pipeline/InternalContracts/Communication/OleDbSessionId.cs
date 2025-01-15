using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000050 RID: 80
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal class OleDbSessionId
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00002CF1 File Offset: 0x00000EF1
		[IgnoreDataMember]
		public bool IsExistingStateful
		{
			get
			{
				return this.Id != OleDbSessionId.AdhocId && this.Id != OleDbSessionId.NewStatefulId;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00002D17 File Offset: 0x00000F17
		[IgnoreDataMember]
		public bool IsNewStateful
		{
			get
			{
				return this.Id == OleDbSessionId.NewStatefulId;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00002D29 File Offset: 0x00000F29
		[IgnoreDataMember]
		public bool IsAdhoc
		{
			get
			{
				return this.Id == OleDbSessionId.AdhocId;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00002D3B File Offset: 0x00000F3B
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00002D43 File Offset: 0x00000F43
		[DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
		public Guid Id { get; set; }

		// Token: 0x0600017C RID: 380 RVA: 0x00002D4C File Offset: 0x00000F4C
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			OleDbSessionId oleDbSessionId = other as OleDbSessionId;
			return oleDbSessionId != null && this.Id == oleDbSessionId.Id;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00002D7C File Offset: 0x00000F7C
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x040000CC RID: 204
		private static readonly Guid AdhocId = Guid.Empty;

		// Token: 0x040000CD RID: 205
		private static readonly Guid NewStatefulId = new Guid("{00000000-0000-0000-0000-000000000001}");

		// Token: 0x040000CE RID: 206
		internal static readonly OleDbSessionId Adhoc = new OleDbSessionId
		{
			Id = OleDbSessionId.AdhocId
		};

		// Token: 0x040000CF RID: 207
		internal static readonly OleDbSessionId NewStateful = new OleDbSessionId
		{
			Id = OleDbSessionId.NewStatefulId
		};
	}
}
