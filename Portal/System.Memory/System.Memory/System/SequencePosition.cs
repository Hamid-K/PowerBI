using System;
using System.ComponentModel;
using System.Numerics.Hashing;

namespace System
{
	// Token: 0x02000006 RID: 6
	public readonly struct SequencePosition : IEquatable<SequencePosition>
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002058 File Offset: 0x00000258
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002068 File Offset: 0x00000268
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetObject()
		{
			return this._object;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002070 File Offset: 0x00000270
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetInteger()
		{
			return this._integer;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002078 File Offset: 0x00000278
		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000209C File Offset: 0x0000029C
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

		// Token: 0x06000009 RID: 9 RVA: 0x000020C3 File Offset: 0x000002C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			object @object = this._object;
			return HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		// Token: 0x04000001 RID: 1
		private readonly object _object;

		// Token: 0x04000002 RID: 2
		private readonly int _integer;
	}
}
