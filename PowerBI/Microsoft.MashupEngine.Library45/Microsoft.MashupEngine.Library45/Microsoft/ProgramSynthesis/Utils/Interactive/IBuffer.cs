using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x02000697 RID: 1687
	public interface IBuffer<out T> : IEnumerable<T>, IEnumerable, IDisposable
	{
	}
}
