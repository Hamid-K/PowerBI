using System;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public sealed class EntitySqlException : EntityException
	{
		// Token: 0x060022F9 RID: 8953 RVA: 0x00063201 File Offset: 0x00061401
		public EntitySqlException()
			: this(Strings.GeneralQueryError)
		{
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0006320E File Offset: 0x0006140E
		public EntitySqlException(string message)
			: base(message)
		{
			base.HResult = -2146232006;
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00063222 File Offset: 0x00061422
		public EntitySqlException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232006;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00063238 File Offset: 0x00061438
		private EntitySqlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorDescription = info.GetString("ErrorDescription");
			this._errorContext = info.GetString("ErrorContext");
			this.Line = info.GetInt32("Line");
			this.Column = info.GetInt32("Column");
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00063291 File Offset: 0x00061491
		internal static EntitySqlException Create(ErrorContext errCtx, string errorMessage, Exception innerException)
		{
			return EntitySqlException.Create(errCtx.CommandText, errorMessage, errCtx.InputPosition, errCtx.ErrorContextInfo, errCtx.UseContextInfoAsResourceIdentifier, innerException);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000632B4 File Offset: 0x000614B4
		internal static EntitySqlException Create(string commandText, string errorDescription, int errorPosition, string errorContextInfo, bool loadErrorContextInfoFromResource, Exception innerException)
		{
			int num;
			int num2;
			string text = EntitySqlException.FormatErrorContext(commandText, errorPosition, errorContextInfo, loadErrorContextInfoFromResource, out num, out num2);
			return new EntitySqlException(EntitySqlException.FormatQueryError(errorDescription, text), errorDescription, text, num, num2, innerException);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000632E2 File Offset: 0x000614E2
		private EntitySqlException(string message, string errorDescription, string errorContext, int line, int column, Exception innerException)
			: base(message, innerException)
		{
			this._errorDescription = errorDescription;
			this._errorContext = errorContext;
			this.Line = line;
			this.Column = column;
			base.HResult = -2146232006;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x00063316 File Offset: 0x00061516
		public string ErrorDescription
		{
			get
			{
				return this._errorDescription ?? string.Empty;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00063327 File Offset: 0x00061527
		public string ErrorContext
		{
			get
			{
				return this._errorContext ?? string.Empty;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x00063338 File Offset: 0x00061538
		public int Line { get; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00063340 File Offset: 0x00061540
		public int Column { get; }

		// Token: 0x06002304 RID: 8964 RVA: 0x00063348 File Offset: 0x00061548
		internal static string GetGenericErrorMessage(string commandText, int position)
		{
			int num = 0;
			int num2 = 0;
			return EntitySqlException.FormatErrorContext(commandText, position, "GenericSyntaxError", true, out num, out num2);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0006336C File Offset: 0x0006156C
		internal static string FormatErrorContext(string commandText, int errorPosition, string errorContextInfo, bool loadErrorContextInfoFromResource, out int lineNumber, out int columnNumber)
		{
			if (loadErrorContextInfoFromResource)
			{
				errorContextInfo = ((!string.IsNullOrEmpty(errorContextInfo)) ? EntityRes.GetString(errorContextInfo) : string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder(commandText.Length);
			foreach (char c in commandText)
			{
				if (CqlLexer.IsNewLine(c))
				{
					c = '\n';
				}
				else if ((char.IsControl(c) || char.IsWhiteSpace(c)) && '\r' != c)
				{
					c = ' ';
				}
				stringBuilder.Append(c);
			}
			commandText = stringBuilder.ToString().TrimEnd(new char[] { '\n' });
			string[] array = commandText.Split(new char[] { '\n' }, StringSplitOptions.None);
			lineNumber = 0;
			columnNumber = errorPosition;
			while (lineNumber < array.Length && columnNumber > array[lineNumber].Length)
			{
				columnNumber -= array[lineNumber].Length + 1;
				lineNumber++;
			}
			lineNumber++;
			columnNumber++;
			stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(errorContextInfo))
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}, ", new object[] { errorContextInfo });
			}
			if (errorPosition >= 0)
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0} {1}, {2} {3}", new object[]
				{
					Strings.LocalizedLine,
					lineNumber,
					Strings.LocalizedColumn,
					columnNumber
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000634CC File Offset: 0x000616CC
		private static string FormatQueryError(string errorMessage, string errorContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(errorMessage);
			if (!string.IsNullOrEmpty(errorContext))
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, " {0} {1}", new object[]
				{
					Strings.LocalizedNear,
					errorContext
				});
			}
			return stringBuilder.Append(".").ToString();
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00063524 File Offset: 0x00061724
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorDescription", this._errorDescription);
			info.AddValue("ErrorContext", this._errorContext);
			info.AddValue("Line", this.Line);
			info.AddValue("Column", this.Column);
		}

		// Token: 0x04000C06 RID: 3078
		private const int HResultInvalidQuery = -2146232006;

		// Token: 0x04000C07 RID: 3079
		private readonly string _errorDescription;

		// Token: 0x04000C08 RID: 3080
		private readonly string _errorContext;
	}
}
