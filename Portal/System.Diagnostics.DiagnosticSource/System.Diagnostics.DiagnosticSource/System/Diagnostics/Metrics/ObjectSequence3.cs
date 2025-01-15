using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000051 RID: 81
	internal struct ObjectSequence3 : IEquatable<ObjectSequence3>, IObjectSequence
	{
		// Token: 0x06000280 RID: 640 RVA: 0x0000A936 File Offset: 0x00008B36
		public ObjectSequence3(object value1, object value2, object value3)
		{
			this.Value1 = value1;
			this.Value2 = value2;
			this.Value3 = value3;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A950 File Offset: 0x00008B50
		public bool Equals(ObjectSequence3 other)
		{
			if (!((this.Value1 == null) ? (other.Value1 == null) : this.Value1.Equals(other.Value1)) || !((this.Value2 == null) ? (other.Value2 == null) : this.Value2.Equals(other.Value2)))
			{
				return false;
			}
			if (this.Value3 != null)
			{
				return this.Value3.Equals(other.Value3);
			}
			return other.Value3 == null;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence3)
			{
				ObjectSequence3 objectSequence = (ObjectSequence3)obj;
				return this.Equals(objectSequence);
			}
			return false;
		}

		// Token: 0x1700008D RID: 141
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
				if (i == 2)
				{
					return this.Value3;
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
				if (i == 2)
				{
					this.Value3 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AA46 File Offset: 0x00008C46
		public override int GetHashCode()
		{
			object value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			object value2 = this.Value2;
			int num2 = num ^ ((value2 != null) ? value2.GetHashCode() : 0);
			object value3 = this.Value3;
			return num2 ^ ((value3 != null) ? value3.GetHashCode() : 0);
		}

		// Token: 0x04000119 RID: 281
		public object Value1;

		// Token: 0x0400011A RID: 282
		public object Value2;

		// Token: 0x0400011B RID: 283
		public object Value3;
	}
}
