using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000050 RID: 80
	internal struct ObjectSequence2 : IEquatable<ObjectSequence2>, IObjectSequence
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000A848 File Offset: 0x00008A48
		public ObjectSequence2(object value1, object value2)
		{
			this.Value1 = value1;
			this.Value2 = value2;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A858 File Offset: 0x00008A58
		public bool Equals(ObjectSequence2 other)
		{
			if (!((this.Value1 == null) ? (other.Value1 == null) : this.Value1.Equals(other.Value1)))
			{
				return false;
			}
			if (this.Value2 != null)
			{
				return this.Value2.Equals(other.Value2);
			}
			return other.Value2 == null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence2)
			{
				ObjectSequence2 objectSequence = (ObjectSequence2)obj;
				return this.Equals(objectSequence);
			}
			return false;
		}

		// Token: 0x1700008C RID: 140
		public object this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.Value1;
				}
				if (i == 1)
				{
					return this.Value2;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (i == 0)
				{
					this.Value1 = value;
					return;
				}
				if (i == 1)
				{
					this.Value2 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A90F File Offset: 0x00008B0F
		public override int GetHashCode()
		{
			object value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			object value2 = this.Value2;
			return num ^ ((value2 != null) ? value2.GetHashCode() : 0);
		}

		// Token: 0x04000117 RID: 279
		public object Value1;

		// Token: 0x04000118 RID: 280
		public object Value2;
	}
}
