using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Diagnostics
{
	// Token: 0x020008C3 RID: 2243
	public struct Result<T>
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x0008E3F9 File Offset: 0x0008C5F9
		// (set) Token: 0x06003039 RID: 12345 RVA: 0x0008E401 File Offset: 0x0008C601
		public T Value { readonly get; private set; }

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x0008E40A File Offset: 0x0008C60A
		// (set) Token: 0x0600303B RID: 12347 RVA: 0x0008E412 File Offset: 0x0008C612
		public Exception Exception { readonly get; private set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0008E41B File Offset: 0x0008C61B
		// (set) Token: 0x0600303D RID: 12349 RVA: 0x0008E423 File Offset: 0x0008C623
		public HashSet<Diagnostic> Diagnostics { readonly get; private set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0008E42C File Offset: 0x0008C62C
		public bool HasErrors
		{
			get
			{
				if (!object.Equals(this.Value, default(T)))
				{
					return this.Diagnostics.Any((Diagnostic d) => d.Severity == Severity.Error);
				}
				return true;
			}
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0008E488 File Offset: 0x0008C688
		public static Result<T> Create(T value, IEnumerable<Diagnostic> diagnostics)
		{
			HashSet<Diagnostic> hashSet = (diagnostics as HashSet<Diagnostic>) ?? diagnostics.ConvertToHashSet<Diagnostic>();
			if (hashSet.Any((Diagnostic d) => d.Severity == Severity.Error))
			{
				return Result<T>.Failure(hashSet);
			}
			return new Result<T>
			{
				Value = value,
				Diagnostics = hashSet
			};
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0008E4F0 File Offset: 0x0008C6F0
		public static Result<T> Failure(IEnumerable<Diagnostic> diagnostics)
		{
			return new Result<T>
			{
				Diagnostics = ((diagnostics as HashSet<Diagnostic>) ?? diagnostics.ConvertToHashSet<Diagnostic>())
			};
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0008E520 File Offset: 0x0008C720
		public static Result<T> Failure(IEnumerable<Diagnostic> diagnostics, Exception exception)
		{
			return new Result<T>
			{
				Diagnostics = ((diagnostics as HashSet<Diagnostic>) ?? diagnostics.ConvertToHashSet<Diagnostic>()),
				Exception = exception
			};
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0008E555 File Offset: 0x0008C755
		public Result<TResult> Select<TResult>(Func<T, TResult> transform)
		{
			if (!this.HasErrors)
			{
				return Result<TResult>.Create(transform(this.Value), this.Diagnostics);
			}
			return Result<TResult>.Failure(this.Diagnostics);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x0008E582 File Offset: 0x0008C782
		public Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> transform)
		{
			if (!this.HasErrors)
			{
				return transform(this.Value);
			}
			return Result<TResult>.Failure(this.Diagnostics);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0008E5A4 File Offset: 0x0008C7A4
		public void TraceDiagnostics()
		{
			foreach (Diagnostic diagnostic in this.Diagnostics)
			{
				Trace.WriteLine(diagnostic);
			}
			if (this.Exception != null)
			{
				Trace.WriteLine(this.Exception.Message);
			}
		}
	}
}
