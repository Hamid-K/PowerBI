using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C32 RID: 3122
	[NullableContext(1)]
	[Nullable(0)]
	public struct BoundedKeyValuePair<[Nullable(0)] TUnit, [Nullable(0)] TKey, [Nullable(2)] TValue> : IBounded<TUnit> where TUnit : BoundsUnit where TKey : IBounded<TUnit>
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005093 RID: 20627 RVA: 0x000FD1AB File Offset: 0x000FB3AB
		public readonly TKey Key { get; }

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06005094 RID: 20628 RVA: 0x000FD1B3 File Offset: 0x000FB3B3
		public readonly TValue Value { get; }

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06005095 RID: 20629 RVA: 0x000FD1BC File Offset: 0x000FB3BC
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<TUnit> Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				TKey key = this.Key;
				return key.Bounds;
			}
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x000FD1DD File Offset: 0x000FB3DD
		public BoundedKeyValuePair(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x000FD1ED File Offset: 0x000FB3ED
		public override string ToString()
		{
			return string.Format("{0}=>{1}", this.Key, this.Value);
		}
	}
}
