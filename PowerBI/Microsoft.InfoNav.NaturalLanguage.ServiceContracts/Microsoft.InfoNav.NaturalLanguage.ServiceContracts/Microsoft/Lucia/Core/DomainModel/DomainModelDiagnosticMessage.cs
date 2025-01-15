using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Lucia.Diagnostics;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000187 RID: 391
	public sealed class DomainModelDiagnosticMessage : DiagnosticMessage
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		public DomainModelDiagnosticMessage(DiagnosticSeverity severity, DomainModelDiagnosticCode code, string message, DomainModelSchemaLocation location)
			: base(severity, message)
		{
			this.Code = code;
			this.Location = location;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		public DomainModelDiagnosticMessage(DiagnosticSeverity severity, DomainModelDiagnosticCode code, IFormattable message, DomainModelSchemaLocation location)
			: base(severity, message)
		{
			this.Code = code;
			this.Location = location;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0000E5FD File Offset: 0x0000C7FD
		public DomainModelDiagnosticCode Code { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000E605 File Offset: 0x0000C805
		public DomainModelSchemaLocation Location { get; }

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000E60D File Offset: 0x0000C80D
		public string ToString(bool includeCodes)
		{
			return base.ToString(includeCodes ? "f" : null, null);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000E624 File Offset: 0x0000C824
		protected override string ToStringCore(string format, DiagnosticFormatProvider formatProvider)
		{
			if (format == "f" || format == "F")
			{
				return FormattableStringUtil.Format(formatProvider, FormattableStringFactory.Create("{0}: '{1}'. {2}", new object[] { base.Severity, this.Code, base.Message }));
			}
			return base.ToStringCore(format, formatProvider);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000E68F File Offset: 0x0000C88F
		public static IReadOnlyList<DomainModelDiagnosticMessage> EmptyList()
		{
			return ImmutableList<DomainModelDiagnosticMessage>.Empty;
		}
	}
}
