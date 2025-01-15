using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002114 RID: 8468
	internal sealed class EmptyEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600D135 RID: 53557 RVA: 0x000020FD File Offset: 0x000002FD
		private EmptyEnumerator()
		{
		}

		// Token: 0x1700328D RID: 12941
		// (get) Token: 0x0600D136 RID: 53558 RVA: 0x0029A286 File Offset: 0x00298486
		internal static IEnumerator<T> EmptyEnumeratorSingleton
		{
			get
			{
				return EmptyEnumerator<T>._EmptyEnumerator;
			}
		}

		// Token: 0x1700328E RID: 12942
		// (get) Token: 0x0600D137 RID: 53559 RVA: 0x0029A28D File Offset: 0x0029848D
		public T Current
		{
			get
			{
				throw new InvalidOperationException(ExceptionMessages.EmptyCollection);
			}
		}

		// Token: 0x1700328F RID: 12943
		// (get) Token: 0x0600D138 RID: 53560 RVA: 0x0029A299 File Offset: 0x00298499
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600D139 RID: 53561 RVA: 0x00002105 File Offset: 0x00000305
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x0600D13A RID: 53562 RVA: 0x0000336E File Offset: 0x0000156E
		public void Reset()
		{
		}

		// Token: 0x0600D13B RID: 53563 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x0400693D RID: 26941
		private static readonly IEnumerator<T> _EmptyEnumerator = new EmptyEnumerator<T>();
	}
}
