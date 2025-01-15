using System;
using System.Data.Entity;
using System.Globalization;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000071 RID: 113
	public sealed class EdmSchemaError : EdmError
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x00014B95 File Offset: 0x00012D95
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity)
			: this(message, errorCode, severity, null)
		{
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00014BA1 File Offset: 0x00012DA1
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			this.Initialize(errorCode, severity, null, -1, -1, exception);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00014BD0 File Offset: 0x00012DD0
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column)
			: this(message, errorCode, severity, schemaLocation, line, column, null)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00014BE4 File Offset: 0x00012DE4
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			if (severity < EdmSchemaErrorSeverity.Warning || severity > EdmSchemaErrorSeverity.Error)
			{
				throw new ArgumentOutOfRangeException("severity", Strings.ArgumentOutOfRange(severity));
			}
			if (line < 0)
			{
				throw new ArgumentOutOfRangeException("line", Strings.ArgumentOutOfRangeExpectedPostiveNumber(line));
			}
			if (column < 0)
			{
				throw new ArgumentOutOfRangeException("column", Strings.ArgumentOutOfRangeExpectedPostiveNumber(column));
			}
			this.Initialize(errorCode, severity, schemaLocation, line, column, exception);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00014C78 File Offset: 0x00012E78
		private void Initialize(int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			if (errorCode < 0)
			{
				throw new ArgumentOutOfRangeException("errorCode", Strings.ArgumentOutOfRangeExpectedPostiveNumber(errorCode));
			}
			this._errorCode = errorCode;
			this._severity = severity;
			this._schemaLocation = schemaLocation;
			this._line = line;
			this._column = column;
			if (exception != null)
			{
				this._stackTrace = exception.StackTrace;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00014CD8 File Offset: 0x00012ED8
		public override string ToString()
		{
			EdmSchemaErrorSeverity severity = this.Severity;
			string text;
			if (severity != EdmSchemaErrorSeverity.Warning)
			{
				if (severity == EdmSchemaErrorSeverity.Error)
				{
					text = Strings.GeneratorErrorSeverityError;
				}
				else
				{
					text = Strings.GeneratorErrorSeverityUnknown;
				}
			}
			else
			{
				text = Strings.GeneratorErrorSeverityWarning;
			}
			string text2;
			if (string.IsNullOrEmpty(this.SchemaName) && this.Line < 0 && this.Column < 0)
			{
				text2 = string.Format(CultureInfo.CurrentCulture, "{0} : {1}", text, base.Message);
			}
			else
			{
				text2 = string.Format(CultureInfo.CurrentCulture, "{0}({1},{2}) : {3} : {4}", new object[]
				{
					(this.SchemaName == null) ? Strings.SourceUriUnknown : this.SchemaName,
					this.Line,
					this.Column,
					text,
					base.Message
				});
			}
			return text2;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00014D99 File Offset: 0x00012F99
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00014DA1 File Offset: 0x00012FA1
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00014DA9 File Offset: 0x00012FA9
		public EdmSchemaErrorSeverity Severity
		{
			get
			{
				return this._severity;
			}
			set
			{
				this._severity = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00014DB2 File Offset: 0x00012FB2
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00014DBA File Offset: 0x00012FBA
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00014DC2 File Offset: 0x00012FC2
		public string SchemaLocation
		{
			get
			{
				return this._schemaLocation;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00014DCA File Offset: 0x00012FCA
		public string SchemaName
		{
			get
			{
				return EdmSchemaError.GetNameFromSchemaLocation(this.SchemaLocation);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00014DD7 File Offset: 0x00012FD7
		public string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00014DE0 File Offset: 0x00012FE0
		private static string GetNameFromSchemaLocation(string schemaLocation)
		{
			if (string.IsNullOrEmpty(schemaLocation))
			{
				return schemaLocation;
			}
			int num = Math.Max(schemaLocation.LastIndexOf('/'), schemaLocation.LastIndexOf('\\'));
			int num2 = num + 1;
			if (num < 0)
			{
				return schemaLocation;
			}
			if (num2 >= schemaLocation.Length)
			{
				return string.Empty;
			}
			return schemaLocation.Substring(num2);
		}

		// Token: 0x04000728 RID: 1832
		private int _errorCode;

		// Token: 0x04000729 RID: 1833
		private EdmSchemaErrorSeverity _severity;

		// Token: 0x0400072A RID: 1834
		private string _schemaLocation;

		// Token: 0x0400072B RID: 1835
		private int _line;

		// Token: 0x0400072C RID: 1836
		private int _column;

		// Token: 0x0400072D RID: 1837
		private string _stackTrace;
	}
}
