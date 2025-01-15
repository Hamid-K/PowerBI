using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004F RID: 79
	internal struct ObjectSequence1 : IEquatable<ObjectSequence1>, IObjectSequence
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000A7BC File Offset: 0x000089BC
		public ObjectSequence1(object value1)
		{
			this.Value1 = value1;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A7C5 File Offset: 0x000089C5
		public override int GetHashCode()
		{
			object value = this.Value1;
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public bool Equals(ObjectSequence1 other)
		{
			if (this.Value1 != null)
			{
				return this.Value1.Equals(other.Value1);
			}
			return other.Value1 == null;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A800 File Offset: 0x00008A00
		public override bool Equals(object obj)
		{
			if (obj is ObjectSequence1)
			{
				ObjectSequence1 objectSequence = (ObjectSequence1)obj;
				return this.Equals(objectSequence);
			}
			return false;
		}

		// Token: 0x1700008B RID: 139
		public object this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.Value1;
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
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x04000116 RID: 278
		public object Value1;
	}
}
