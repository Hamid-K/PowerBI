using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x02000036 RID: 54
	public class DiagnosticMessage : IFormattable
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003B31 File Offset: 0x00001D31
		public DiagnosticMessage(DiagnosticSeverity severity, string message)
		{
			this.Severity = severity;
			this.Message = message;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003B47 File Offset: 0x00001D47
		public DiagnosticMessage(DiagnosticSeverity severity, IFormattable message)
		{
			this.Severity = severity;
			this.Message = message;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003B5D File Offset: 0x00001D5D
		public DiagnosticSeverity Severity { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003B65 File Offset: 0x00001D65
		protected object Message { get; }

		// Token: 0x060000DE RID: 222 RVA: 0x00003B6D File Offset: 0x00001D6D
		public sealed override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003B77 File Offset: 0x00001D77
		public string ToString([Nullable] IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003B81 File Offset: 0x00001D81
		public string ToString([Nullable] string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003B8B File Offset: 0x00001D8B
		public string ToString([Nullable] string format, [Nullable] IFormatProvider formatProvider)
		{
			return this.ToStringCore(format ?? string.Empty, DiagnosticFormatProvider.GetInstanceOrDefault(formatProvider ?? CultureInfo.CurrentCulture));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003BAC File Offset: 0x00001DAC
		protected virtual string ToStringCore(string format, DiagnosticFormatProvider formatProvider)
		{
			if ((format != null && format.Length == 0) || format == "g" || format == "G")
			{
				return FormattableStringUtil.Format(formatProvider, FormattableStringFactory.Create("{0}: {1}", new object[] { this.Severity, this.Message }));
			}
			if (!(format == "m") && !(format == "M"))
			{
				throw new FormatException("Invalid format '" + format + "' specified for DiagnosticMessage.");
			}
			IFormattable formattable = this.Message as IFormattable;
			if (formattable == null)
			{
				return this.Message.ToString();
			}
			return formattable.ToString(null, formatProvider);
		}
	}
}
