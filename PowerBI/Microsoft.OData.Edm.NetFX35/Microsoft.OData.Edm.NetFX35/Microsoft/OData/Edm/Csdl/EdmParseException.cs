using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000015 RID: 21
	[SuppressMessage("Microsoft.Usage", "CA2237", Justification = "We do not intend to support serialization of this exception yet.")]
	[SuppressMessage("Microsoft.Design", "CA1032", Justification = "We do not intend to support serialization of this exception yet, nor does it need the full suite of constructors.")]
	[DebuggerDisplay("{Message}")]
	public class EdmParseException : Exception
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public EdmParseException(IEnumerable<EdmError> parseErrors)
			: this(Enumerable.ToList<EdmError>(parseErrors))
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC6 File Offset: 0x00000DC6
		private EdmParseException(List<EdmError> parseErrors)
			: base(EdmParseException.ConstructMessage(parseErrors))
		{
			this.Errors = new ReadOnlyCollection<EdmError>(parseErrors);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002BE0 File Offset: 0x00000DE0
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public ReadOnlyCollection<EdmError> Errors { get; private set; }

		// Token: 0x06000066 RID: 102 RVA: 0x00002BF9 File Offset: 0x00000DF9
		private static string ConstructMessage(IEnumerable<EdmError> parseErrors)
		{
			return Strings.EdmParseException_ErrorsEncounteredInEdmx(string.Join(Environment.NewLine, Enumerable.ToArray<string>(Enumerable.Select<EdmError, string>(parseErrors, (EdmError p) => p.ToString()))));
		}
	}
}
