using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000141 RID: 321
	[SuppressMessage("Microsoft.Design", "CA1032", Justification = "We do not intend to support serialization of this exception yet, nor does it need the full suite of constructors.")]
	[SuppressMessage("Microsoft.Usage", "CA2237", Justification = "We do not intend to support serialization of this exception yet.")]
	[DebuggerDisplay("{Message}")]
	public class EdmParseException : Exception
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x000145A8 File Offset: 0x000127A8
		public EdmParseException(IEnumerable<EdmError> parseErrors)
			: this(Enumerable.ToList<EdmError>(parseErrors))
		{
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000145B6 File Offset: 0x000127B6
		private EdmParseException(List<EdmError> parseErrors)
			: base(EdmParseException.ConstructMessage(parseErrors))
		{
			this.Errors = new ReadOnlyCollection<EdmError>(parseErrors);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x000145D0 File Offset: 0x000127D0
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x000145D8 File Offset: 0x000127D8
		public ReadOnlyCollection<EdmError> Errors { get; private set; }

		// Token: 0x060007C1 RID: 1985 RVA: 0x000145E1 File Offset: 0x000127E1
		private static string ConstructMessage(IEnumerable<EdmError> parseErrors)
		{
			return Strings.EdmParseException_ErrorsEncounteredInEdmx(string.Join(Environment.NewLine, Enumerable.ToArray<string>(Enumerable.Select<EdmError, string>(parseErrors, (EdmError p) => p.ToString()))));
		}
	}
}
