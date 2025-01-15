using System;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001FE RID: 510
	[DebuggerDisplay("{value}")]
	internal struct ObjectId : IEquatable<ObjectId>
	{
		// Token: 0x06001D25 RID: 7461 RVA: 0x000C788E File Offset: 0x000C5A8E
		public ObjectId(ulong value)
		{
			this.value = value;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x000C7897 File Offset: 0x000C5A97
		public bool IsNull
		{
			get
			{
				return this.value == 0UL;
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000C78A4 File Offset: 0x000C5AA4
		public static ObjectId FromString(string value)
		{
			ulong num;
			if (!ulong.TryParse(value, out num))
			{
				throw TomInternalException.Create("Can't convert value '{0}' to ObjectId", new object[] { value });
			}
			return new ObjectId(num);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000C78D6 File Offset: 0x000C5AD6
		public static ObjectId FromUInt64(ulong idValue)
		{
			return new ObjectId(idValue);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000C78DE File Offset: 0x000C5ADE
		public static ObjectId FromInt32(int idValue)
		{
			return new ObjectId((ulong)((long)idValue));
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000C78E7 File Offset: 0x000C5AE7
		public static implicit operator ObjectId(ulong value)
		{
			return new ObjectId(value);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000C78EF File Offset: 0x000C5AEF
		public static explicit operator ulong(ObjectId id)
		{
			return id.value;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000C78F7 File Offset: 0x000C5AF7
		public static bool operator ==(ObjectId lhs, ObjectId rhs)
		{
			return lhs.value == rhs.value;
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000C7907 File Offset: 0x000C5B07
		public static bool operator !=(ObjectId lhs, ObjectId rhs)
		{
			return lhs.value != rhs.value;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000C791A File Offset: 0x000C5B1A
		public static bool operator ==(ObjectId lhs, ulong rhs)
		{
			return lhs.value == rhs;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000C7925 File Offset: 0x000C5B25
		public static bool operator !=(ObjectId lhs, ulong rhs)
		{
			return lhs.value != rhs;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000C7934 File Offset: 0x000C5B34
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000C7950 File Offset: 0x000C5B50
		public override bool Equals(object other)
		{
			if (other is ObjectId)
			{
				ObjectId objectId = (ObjectId)other;
				return this.Equals(objectId);
			}
			return false;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000C7978 File Offset: 0x000C5B78
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000C7993 File Offset: 0x000C5B93
		public bool Equals(ObjectId other)
		{
			return this.value == other.value;
		}

		// Token: 0x040006A0 RID: 1696
		public static readonly ObjectId Null = new ObjectId(0UL);

		// Token: 0x040006A1 RID: 1697
		public static readonly ObjectId Model = new ObjectId(1UL);

		// Token: 0x040006A2 RID: 1698
		private readonly ulong value;
	}
}
