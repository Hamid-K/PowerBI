using System;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B1 RID: 1201
	[Serializable]
	public sealed class EdmSchemaError : EdmError
	{
		// Token: 0x06003B14 RID: 15124 RVA: 0x000C3332 File Offset: 0x000C1532
		public EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity)
			: this(message, errorCode, severity, null)
		{
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000C333E File Offset: 0x000C153E
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			this.Initialize(errorCode, severity, null, -1, -1, exception);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000C336D File Offset: 0x000C156D
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column)
			: this(message, errorCode, severity, schemaLocation, line, column, null)
		{
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000C3380 File Offset: 0x000C1580
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			if (severity < EdmSchemaErrorSeverity.Warning || severity > EdmSchemaErrorSeverity.Error)
			{
				throw new ArgumentOutOfRangeException("severity", severity, Strings.ArgumentOutOfRange(severity));
			}
			this.Initialize(errorCode, severity, schemaLocation, line, column, exception);
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000C33E4 File Offset: 0x000C15E4
		private void Initialize(int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			if (errorCode < 0)
			{
				throw new ArgumentOutOfRangeException("errorCode", errorCode, Strings.ArgumentOutOfRangeExpectedPostiveNumber(errorCode));
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

		// Token: 0x06003B19 RID: 15129 RVA: 0x000C3448 File Offset: 0x000C1648
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
				text2 = string.Format(CultureInfo.CurrentCulture, "{0} {1:0000}: {2}", new object[] { text, this.ErrorCode, base.Message });
			}
			else
			{
				text2 = string.Format(CultureInfo.CurrentCulture, "{0}({1},{2}) : {3} {4:0000}: {5}", new object[]
				{
					(this.SchemaName == null) ? Strings.SourceUriUnknown : this.SchemaName,
					this.Line,
					this.Column,
					text,
					this.ErrorCode,
					base.Message
				});
			}
			return text2;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x000C3531 File Offset: 0x000C1731
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000C3539 File Offset: 0x000C1739
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000C3541 File Offset: 0x000C1741
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

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000C354A File Offset: 0x000C174A
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000C3552 File Offset: 0x000C1752
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000C355A File Offset: 0x000C175A
		public string SchemaLocation
		{
			get
			{
				return this._schemaLocation;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000C3562 File Offset: 0x000C1762
		public string SchemaName
		{
			get
			{
				return EdmSchemaError.GetNameFromSchemaLocation(this.SchemaLocation);
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000C356F File Offset: 0x000C176F
		public string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000C3578 File Offset: 0x000C1778
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

		// Token: 0x0400146F RID: 5231
		private int _errorCode;

		// Token: 0x04001470 RID: 5232
		private EdmSchemaErrorSeverity _severity;

		// Token: 0x04001471 RID: 5233
		private string _schemaLocation;

		// Token: 0x04001472 RID: 5234
		private int _line;

		// Token: 0x04001473 RID: 5235
		private int _column;

		// Token: 0x04001474 RID: 5236
		private string _stackTrace;
	}
}
