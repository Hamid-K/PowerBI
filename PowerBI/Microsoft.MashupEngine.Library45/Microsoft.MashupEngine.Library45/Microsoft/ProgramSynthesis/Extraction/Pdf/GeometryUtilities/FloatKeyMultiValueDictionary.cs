using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C3E RID: 3134
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	public class FloatKeyMultiValueDictionary<[Nullable(2)] T> : FloatKeyDictionary<List<T>>
	{
		// Token: 0x060050D4 RID: 20692 RVA: 0x000FD945 File Offset: 0x000FBB45
		public void Add(float key, T item)
		{
			this.GetOrCreateValue(key).Add(item);
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x000FD954 File Offset: 0x000FBB54
		public void MaybeAdd(float key, [Nullable(new byte[] { 0, 1 })] Optional<T> optional)
		{
			optional.Select(delegate(T item)
			{
				this.Add(key, item);
			});
		}
	}
}
