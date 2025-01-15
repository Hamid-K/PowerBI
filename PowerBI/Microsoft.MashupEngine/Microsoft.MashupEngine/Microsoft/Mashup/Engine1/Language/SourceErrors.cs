using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x020017A7 RID: 6055
	internal static class SourceErrors
	{
		// Token: 0x0600991B RID: 39195 RVA: 0x001F9D8D File Offset: 0x001F7F8D
		public static SourceError SyntaxError(SourceLocation location, string message)
		{
			return new SourceError(ErrorKind.SyntaxError, location, message);
		}

		// Token: 0x0600991C RID: 39196 RVA: 0x001F9D97 File Offset: 0x001F7F97
		public static SourceError DuplicateParameter(SourceLocation location, Identifier identifier)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateParameter, location, Strings.SourceError_DuplicateParameter(identifier.Name), identifier.Name);
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x001F9DB6 File Offset: 0x001F7FB6
		public static SourceError DuplicateLocal(SourceLocation location, Identifier identifier)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateLocal, location, Strings.SourceError_DuplicateLocal(identifier.Name), identifier.Name);
		}

		// Token: 0x0600991E RID: 39198 RVA: 0x001F9DD5 File Offset: 0x001F7FD5
		public static SourceError DuplicateField(SourceLocation location, Identifier identifier)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateField, location, Strings.SourceError_DuplicateFieldName(identifier.Name), identifier.Name);
		}

		// Token: 0x0600991F RID: 39199 RVA: 0x001F9DF4 File Offset: 0x001F7FF4
		public static SourceError DuplicateMember(SourceLocation location, Identifier identifier)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateMember, location, Strings.SourceError_DuplicateMember(identifier.Name), identifier.Name);
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x001F9E13 File Offset: 0x001F8013
		public static SourceError DuplicateExport(SourceLocation location, string name)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateExport, location, Strings.SourceError_DuplicateExport(name), name);
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x001F9E28 File Offset: 0x001F8028
		public static SourceError DuplicateSection(SourceLocation location, string section)
		{
			return new SourceErrors.DuplicateIdentifierError(ErrorKind.DuplicateSection, location, Strings.SourceError_DuplicateSection(section), section);
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x001F9E40 File Offset: 0x001F8040
		public static SourceError UnknownIdentifier(SourceLocation location, string section, string name)
		{
			if (section != null)
			{
				return new SourceErrors.UnknownIdentifierError(ErrorKind.UnknownIdentifier, location, Strings.SourceError_UnknownSectionIdentifier(section, name), section, name);
			}
			if (name == Identifier.Underscore)
			{
				return new SourceErrors.UnknownIdentifierError(ErrorKind.UnknownIdentifier, location, string.Format(CultureInfo.CurrentCulture, Strings.SourceError_UnderscoreOutsideEach, Array.Empty<object>()), null, name);
			}
			return new SourceErrors.UnknownIdentifierError(ErrorKind.UnknownIdentifier, location, Strings.SourceError_UnknownIdentifier(name), null, name);
		}

		// Token: 0x06009923 RID: 39203 RVA: 0x001F9EAF File Offset: 0x001F80AF
		public static SourceError UnknownSection(SourceLocation location, string section)
		{
			return new SourceErrors.UnknownIdentifierError(ErrorKind.UnknownSection, location, Strings.SourceError_UnknownSection(section), section, null);
		}

		// Token: 0x06009924 RID: 39204 RVA: 0x001F9EC5 File Offset: 0x001F80C5
		public static SourceError Generic(string text)
		{
			return new SourceError(ErrorKind.Generic, SourceLocation.None, text);
		}

		// Token: 0x020017A8 RID: 6056
		private class DuplicateIdentifierError : SourceError, IDuplicateIdentifierError, IError
		{
			// Token: 0x06009925 RID: 39205 RVA: 0x001F9ED4 File Offset: 0x001F80D4
			public DuplicateIdentifierError(ErrorKind kind, SourceLocation location, string message, string name)
				: base(kind, location, message)
			{
				this.name = name;
			}

			// Token: 0x170027AB RID: 10155
			// (get) Token: 0x06009926 RID: 39206 RVA: 0x001F9EE7 File Offset: 0x001F80E7
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04005130 RID: 20784
			private string name;
		}

		// Token: 0x020017A9 RID: 6057
		private class UnknownIdentifierError : SourceError, IUnknownIdentifierError, IError
		{
			// Token: 0x06009927 RID: 39207 RVA: 0x001F9EEF File Offset: 0x001F80EF
			public UnknownIdentifierError(ErrorKind kind, SourceLocation location, string message, string section, string name)
				: base(kind, location, message)
			{
				this.section = section;
				this.name = name;
			}

			// Token: 0x170027AC RID: 10156
			// (get) Token: 0x06009928 RID: 39208 RVA: 0x001F9F0A File Offset: 0x001F810A
			public string Section
			{
				get
				{
					return this.section;
				}
			}

			// Token: 0x170027AD RID: 10157
			// (get) Token: 0x06009929 RID: 39209 RVA: 0x001F9F12 File Offset: 0x001F8112
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x04005131 RID: 20785
			private string section;

			// Token: 0x04005132 RID: 20786
			private string name;
		}
	}
}
