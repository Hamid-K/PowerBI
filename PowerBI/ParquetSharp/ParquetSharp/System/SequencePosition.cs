using System;
using System.ComponentModel;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C4 RID: 196
	[System.Memory.IsReadOnly]
	internal struct SequencePosition : IEquatable<SequencePosition>
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x000189C4 File Offset: 0x00016BC4
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000189D4 File Offset: 0x00016BD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetObject()
		{
			return this._object;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000189DC File Offset: 0x00016BDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetInteger()
		{
			return this._integer;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000189E4 File Offset: 0x00016BE4
		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00018A0C File Offset: 0x00016C0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is SequencePosition)
			{
				SequencePosition sequencePosition = (SequencePosition)obj;
				return this.Equals(sequencePosition);
			}
			return false;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00018A3C File Offset: 0x00016C3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			object @object = this._object;
			return System.Memory189091.HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		// Token: 0x040001D9 RID: 473
		private readonly object _object;

		// Token: 0x040001DA RID: 474
		private readonly int _integer;
	}
}
