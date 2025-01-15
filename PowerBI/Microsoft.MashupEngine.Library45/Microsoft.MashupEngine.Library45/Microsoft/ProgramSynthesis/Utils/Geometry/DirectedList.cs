using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005CB RID: 1483
	public class DirectedList<T> : Directed<IReadOnlyList<T>>
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		public DirectedList(Func<Direction, IEnumerable<T>> generator)
			: base(delegate(Direction axis)
			{
				IEnumerable<T> enumerable = generator(axis);
				return (enumerable as IReadOnlyList<T>) ?? enumerable.ToList<T>();
			})
		{
		}
	}
}
