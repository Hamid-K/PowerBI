using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Diagnostics;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200007B RID: 123
	public sealed class DiagnosticsContext
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000AB91 File Offset: 0x00008D91
		public GrammarValidation ValidationFlags { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000AB99 File Offset: 0x00008D99
		public IReadOnlyList<string> Suppressions { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000ABA1 File Offset: 0x00008DA1
		public string FileName { get; }

		// Token: 0x060002B0 RID: 688 RVA: 0x0000ABA9 File Offset: 0x00008DA9
		public DiagnosticsContext(GrammarValidation validationFlags, IReadOnlyList<string> suppressions, string fileName)
		{
			this.ValidationFlags = validationFlags;
			this.Suppressions = suppressions;
			this._diagnostics = new HashSet<Diagnostic>();
			this.FileName = fileName;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public bool HasErrors
		{
			get
			{
				return this._diagnostics.Any((Diagnostic d) => d.Severity == Severity.Error);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000ABFD File Offset: 0x00008DFD
		public IEnumerable<Diagnostic> Diagnostics
		{
			get
			{
				return this._diagnostics;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AC08 File Offset: 0x00008E08
		public Result<TResult> ContinueWith<T, TResult>(Func<T, Result<TResult>> callback, T value)
		{
			if (this.HasErrors)
			{
				return Result<TResult>.Failure(this._diagnostics);
			}
			Result<TResult> result;
			try
			{
				result = callback(value);
			}
			catch (Exception ex)
			{
				result = Result<TResult>.Failure(this._diagnostics, ex);
			}
			return result;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AC58 File Offset: 0x00008E58
		public void AddDiagnostic(Diagnostic diagnostic)
		{
			if (!this.ValidationFlags.HasFlag(diagnostic.Category))
			{
				return;
			}
			if (diagnostic.Severity == Severity.Warning && this.Suppressions.Contains(diagnostic.ID))
			{
				return;
			}
			this._diagnostics.Add(diagnostic);
		}

		// Token: 0x04000154 RID: 340
		private readonly HashSet<Diagnostic> _diagnostics;
	}
}
