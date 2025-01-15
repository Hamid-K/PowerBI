using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000052 RID: 82
	internal struct ObjectSequenceMany : IEquatable<ObjectSequenceMany>, IObjectSequence
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000AA80 File Offset: 0x00008C80
		public ObjectSequenceMany(object[] values)
		{
			this._values = values;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public bool Equals(ObjectSequenceMany other)
		{
			if (this._values.Length != other._values.Length)
			{
				return false;
			}
			for (int i = 0; i < this._values.Length; i++)
			{
				object obj = this._values[i];
				object obj2 = other._values[i];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public override bool Equals(object obj)
		{
			if (obj is ObjectSequenceMany)
			{
				ObjectSequenceMany objectSequenceMany = (ObjectSequenceMany)obj;
				return this.Equals(objectSequenceMany);
			}
			return false;
		}

		// Token: 0x1700008E RID: 142
		public object this[int i]
		{
			get
			{
				return this._values[i];
			}
			set
			{
				this._values[i] = value;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AB24 File Offset: 0x00008D24
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this._values.Length; i++)
			{
				num <<= 3;
				object obj = this._values[i];
				if (obj != null)
				{
					num ^= obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0400011C RID: 284
		private readonly object[] _values;
	}
}
