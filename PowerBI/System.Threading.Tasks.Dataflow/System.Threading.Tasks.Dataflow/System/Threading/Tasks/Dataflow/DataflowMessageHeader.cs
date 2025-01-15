using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001F RID: 31
	[DebuggerDisplay("Id = {Id}")]
	public readonly struct DataflowMessageHeader : IEquatable<DataflowMessageHeader>
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000038A6 File Offset: 0x00001AA6
		public DataflowMessageHeader(long id)
		{
			if (id == 0L)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageId, "id");
			}
			this._id = id;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000038C2 File Offset: 0x00001AC2
		public bool IsValid
		{
			get
			{
				return this._id != 0L;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000038CE File Offset: 0x00001ACE
		public long Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038D6 File Offset: 0x00001AD6
		public bool Equals(DataflowMessageHeader other)
		{
			return this == other;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000038E4 File Offset: 0x00001AE4
		[NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			return obj is DataflowMessageHeader && this == (DataflowMessageHeader)obj;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003901 File Offset: 0x00001B01
		public override int GetHashCode()
		{
			return (int)this.Id;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000390A File Offset: 0x00001B0A
		public static bool operator ==(DataflowMessageHeader left, DataflowMessageHeader right)
		{
			return left.Id == right.Id;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000391C File Offset: 0x00001B1C
		public static bool operator !=(DataflowMessageHeader left, DataflowMessageHeader right)
		{
			return left.Id != right.Id;
		}

		// Token: 0x0400002E RID: 46
		private readonly long _id;
	}
}
