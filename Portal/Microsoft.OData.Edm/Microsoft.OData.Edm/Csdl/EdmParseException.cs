using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014D RID: 333
	[SuppressMessage("Microsoft.Design", "CA1032", Justification = "We do not intend to support serialization of this exception yet, nor does it need the full suite of constructors.")]
	[SuppressMessage("Microsoft.Usage", "CA2237", Justification = "We do not intend to support serialization of this exception yet.")]
	[DebuggerDisplay("{Message}")]
	public class EdmParseException : Exception
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x000161BE File Offset: 0x000143BE
		public EdmParseException(IEnumerable<EdmError> parseErrors)
			: this(parseErrors.ToList<EdmError>())
		{
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000161CC File Offset: 0x000143CC
		private EdmParseException(List<EdmError> parseErrors)
			: base(EdmParseException.ConstructMessage(parseErrors))
		{
			this.Errors = new ReadOnlyCollection<EdmError>(parseErrors);
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000161E6 File Offset: 0x000143E6
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000161EE File Offset: 0x000143EE
		public ReadOnlyCollection<EdmError> Errors { get; private set; }

		// Token: 0x0600085F RID: 2143 RVA: 0x000161F7 File Offset: 0x000143F7
		private static string ConstructMessage(IEnumerable<EdmError> parseErrors)
		{
			return Strings.EdmParseException_ErrorsEncounteredInEdmx(string.Join(Environment.NewLine, parseErrors.Select((EdmError p) => p.ToString()).ToArray<string>()));
		}
	}
}
