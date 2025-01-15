using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;

namespace Microsoft.Data.Common
{
	// Token: 0x02000178 RID: 376
	internal static class ADP
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00071AC5 File Offset: 0x0006FCC5
		internal static Task<bool> TrueTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP.s_trueTask) == null)
				{
					task = (ADP.s_trueTask = Task.FromResult<bool>(true));
				}
				return task;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00071ADC File Offset: 0x0006FCDC
		internal static Task<bool> FalseTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP.s_falseTask) == null)
				{
					task = (ADP.s_falseTask = Task.FromResult<bool>(false));
				}
				return task;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00071AF4 File Offset: 0x0006FCF4
		internal static InvalidUdtException CreateInvalidUdtException(Type udtType, string resourceReasonName)
		{
			InvalidUdtException ex = (InvalidUdtException)ADP.s_method.Invoke(null, new object[] { udtType, resourceReasonName });
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00071B27 File Offset: 0x0006FD27
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				SqlClientEventSource.Log.TryTraceEvent<Exception>(trace, e);
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00071B38 File Offset: 0x0006FD38
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00071B45 File Offset: 0x0006FD45
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00071B52 File Offset: 0x0006FD52
		internal static bool IsEmptyArray(string[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00071B60 File Offset: 0x0006FD60
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00071B8C File Offset: 0x0006FD8C
		internal static Exception ExceptionWithStackTrace(Exception e)
		{
			Exception ex2;
			try
			{
				throw e;
			}
			catch (Exception ex)
			{
				ex2 = ex;
			}
			return ex2;
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00071BB4 File Offset: 0x0006FDB4
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00071BD0 File Offset: 0x0006FDD0
		internal static ArgumentException Argument(string error, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00071BEC File Offset: 0x0006FDEC
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00071C08 File Offset: 0x0006FE08
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00071C24 File Offset: 0x0006FE24
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00071C40 File Offset: 0x0006FE40
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00071C5C File Offset: 0x0006FE5C
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00071C78 File Offset: 0x0006FE78
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00071C94 File Offset: 0x0006FE94
		internal static IndexOutOfRangeException IndexOutOfRange(int value)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00071CBC File Offset: 0x0006FEBC
		internal static IndexOutOfRangeException IndexOutOfRange()
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00071CD8 File Offset: 0x0006FED8
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			InvalidOperationException ex = new InvalidOperationException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00071CF4 File Offset: 0x0006FEF4
		internal static OverflowException Overflow(string error)
		{
			return ADP.Overflow(error, null);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00071D00 File Offset: 0x0006FF00
		internal static OverflowException Overflow(string error, Exception inner)
		{
			OverflowException ex = new OverflowException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00071D1C File Offset: 0x0006FF1C
		internal static TimeoutException TimeoutException(string error, Exception inner = null)
		{
			TimeoutException ex = new TimeoutException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00071D38 File Offset: 0x0006FF38
		internal static TypeLoadException TypeLoad(string error)
		{
			TypeLoadException ex = new TypeLoadException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00071D54 File Offset: 0x0006FF54
		internal static InvalidCastException InvalidCast()
		{
			InvalidCastException ex = new InvalidCastException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00071D6E File Offset: 0x0006FF6E
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00071D78 File Offset: 0x0006FF78
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00071D94 File Offset: 0x0006FF94
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00071DB0 File Offset: 0x0006FFB0
		internal static IOException IO(string error)
		{
			IOException ex = new IOException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00071DCC File Offset: 0x0006FFCC
		internal static IOException IO(string error, Exception inner)
		{
			IOException ex = new IOException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00071DE8 File Offset: 0x0006FFE8
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00071E04 File Offset: 0x00070004
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00071E1F File Offset: 0x0007001F
		internal static InvalidOperationException DataAdapter(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00071E1F File Offset: 0x0007001F
		private static InvalidOperationException Provider(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00071E28 File Offset: 0x00070028
		internal static ArgumentException InvalidMultipartName(string property, string value)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_InvalidMultipartName, new object[]
			{
				StringsHelper.GetString(property, Array.Empty<object>()),
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00071E64 File Offset: 0x00070064
		internal static ArgumentException InvalidMultipartNameIncorrectUsageOfQuotes(string property, string value)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_InvalidMultipartNameQuoteUsage, new object[]
			{
				StringsHelper.GetString(property, Array.Empty<object>()),
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00071EA0 File Offset: 0x000700A0
		internal static ArgumentException InvalidMultipartNameToManyParts(string property, string value, int limit)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_InvalidMultipartNameToManyParts, new object[]
			{
				StringsHelper.GetString(property, Array.Empty<object>()),
				value,
				limit
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00071EE8 File Offset: 0x000700E8
		internal static ObjectDisposedException ObjectDisposed(object instance)
		{
			ObjectDisposedException ex = new ObjectDisposedException(instance.GetType().Name);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00071F10 File Offset: 0x00070110
		internal static InvalidOperationException MethodCalledTwice(string method)
		{
			InvalidOperationException ex = new InvalidOperationException(StringsHelper.GetString(Strings.ADP_CalledTwice, new object[] { method }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00071F40 File Offset: 0x00070140
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, value, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00071F5D File Offset: 0x0007015D
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, string value, string method)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_NotSupportedEnumerationValue, new object[] { type.Name, value, method }), type.Name);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00071F8B File Offset: 0x0007018B
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull(parameterName);
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00071F98 File Offset: 0x00070198
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != typeof(StackOverflowException) && type != typeof(OutOfMemoryException) && type != typeof(ThreadAbortException) && type != typeof(NullReferenceException) && type != typeof(AccessViolationException) && !typeof(SecurityException).IsAssignableFrom(type);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007201C File Offset: 0x0007021C
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != typeof(StackOverflowException) && type != typeof(OutOfMemoryException) && type != typeof(ThreadAbortException) && type != typeof(NullReferenceException) && type != typeof(AccessViolationException);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0007208A File Offset: 0x0007028A
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_InvalidEnumerationValue, new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000720BF File Offset: 0x000702BF
		internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000720D1 File Offset: 0x000702D1
		internal static void ValidateCommandBehavior(CommandBehavior value)
		{
			if (value < CommandBehavior.Default || (CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection) < value)
			{
				throw ADP.InvalidCommandBehavior(value);
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000720E3 File Offset: 0x000702E3
		internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Format value)
		{
			return ADP.InvalidEnumerationValue(typeof(Format), (int)value);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000720F5 File Offset: 0x000702F5
		internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Format value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(Format), value.ToString(), method);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00072114 File Offset: 0x00070314
		internal static ArgumentException InvalidArgumentLength(string argumentName, int limit)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidArgumentLength, new object[] { argumentName, limit }));
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00072138 File Offset: 0x00070338
		internal static ArgumentException MustBeReadOnly(string argumentName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_MustBeReadOnly, new object[] { argumentName }));
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00072154 File Offset: 0x00070354
		internal static Exception CreateSqlException(MsalException msalException, SqlConnectionString connectionOptions, SqlInternalConnectionTds sender, string username)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			sqlErrorCollection.Add(new SqlError(0, 0, 11, connectionOptions.DataSource, StringsHelper.GetString(Strings.SQL_MSALFailure, new object[]
			{
				username,
				connectionOptions.Authentication.ToString("G")
			}), "AcquireToken", 0, null));
			string @string = StringsHelper.GetString(Strings.SQL_MSALInnerException, new object[] { msalException.ErrorCode });
			sqlErrorCollection.Add(new SqlError(0, 0, 11, connectionOptions.DataSource, @string, "AcquireToken", 0, null));
			if (!string.IsNullOrEmpty(msalException.Message))
			{
				sqlErrorCollection.Add(new SqlError(0, 0, 11, connectionOptions.DataSource, msalException.Message, "AcquireToken", 0, null));
			}
			return SqlException.CreateException(sqlErrorCollection, "", sender, null);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00072224 File Offset: 0x00070424
		internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
		{
			int num = ((quotePrefix == null) ? 0 : quotePrefix.Length);
			int num2 = ((quoteSuffix == null) ? 0 : quoteSuffix.Length);
			if (num2 + num == 0)
			{
				unquotedString = quotedString;
				return true;
			}
			if (quotedString == null)
			{
				unquotedString = quotedString;
				return false;
			}
			int length = quotedString.Length;
			if (length < num + num2)
			{
				unquotedString = quotedString;
				return false;
			}
			if (num > 0 && !quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
			{
				unquotedString = quotedString;
				return false;
			}
			if (num2 > 0)
			{
				if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
				{
					unquotedString = quotedString;
					return false;
				}
				unquotedString = quotedString.Substring(num, length - (num + num2)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
			}
			else
			{
				unquotedString = quotedString.Substring(num, length - num);
			}
			return true;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000722C0 File Offset: 0x000704C0
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder(unQuotedString.Length + quoteSuffix.Length + quoteSuffix.Length);
			ADP.AppendQuotedString(stringBuilder, quotePrefix, quoteSuffix, unQuotedString);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000722F8 File Offset: 0x000704F8
		internal static string AppendQuotedString(StringBuilder buffer, string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			if (!string.IsNullOrEmpty(quotePrefix))
			{
				buffer.Append(quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				int length = buffer.Length;
				buffer.Append(unQuotedString);
				buffer.Replace(quoteSuffix, quoteSuffix + quoteSuffix, length, unQuotedString.Length);
				buffer.Append(quoteSuffix);
			}
			else
			{
				buffer.Append(unQuotedString);
			}
			return buffer.ToString();
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0007235C File Offset: 0x0007055C
		internal static string BuildMultiPartName(string[] strings)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < strings.Length; i++)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append('.');
				}
				if (strings[i] != null && strings[i].Length != 0)
				{
					stringBuilder.Append(ADP.BuildQuotedString("[", "]", strings[i]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000723C0 File Offset: 0x000705C0
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			foreach (Delegate @delegate in (mcd != null) ? mcd.GetInvocationList() : null)
			{
				if (@delegate.Target is DbCommandBuilder)
				{
					return @delegate;
				}
			}
			return null;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000723FC File Offset: 0x000705FC
		internal static long TimerCurrent()
		{
			return DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00072418 File Offset: 0x00070618
		internal static long TimerFromSeconds(int seconds)
		{
			checked
			{
				return unchecked((long)seconds) * 10000000L;
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00072430 File Offset: 0x00070630
		internal static long TimerFromMilliseconds(long milliseconds)
		{
			return checked(milliseconds * 10000L);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00072448 File Offset: 0x00070648
		internal static bool TimerHasExpired(long timerExpire)
		{
			return ADP.TimerCurrent() > timerExpire;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00072460 File Offset: 0x00070660
		internal static long TimerRemaining(long timerExpire)
		{
			long num = ADP.TimerCurrent();
			return checked(timerExpire - num);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00072478 File Offset: 0x00070678
		internal static long TimerRemainingMilliseconds(long timerExpire)
		{
			return ADP.TimerToMilliseconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00072494 File Offset: 0x00070694
		internal static long TimerRemainingSeconds(long timerExpire)
		{
			return ADP.TimerToSeconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000724B0 File Offset: 0x000706B0
		internal static long TimerToMilliseconds(long timerValue)
		{
			return timerValue / 10000L;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000724C8 File Offset: 0x000706C8
		private static long TimerToSeconds(long timerValue)
		{
			return timerValue / 10000000L;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000724DF File Offset: 0x000706DF
		[EnvironmentPermission(SecurityAction.Assert, Read = "COMPUTERNAME")]
		internal static string MachineName()
		{
			return Environment.MachineName;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000724E8 File Offset: 0x000706E8
		internal static Transaction GetCurrentTransaction()
		{
			return Transaction.Current;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000724FC File Offset: 0x000706FC
		internal static bool IsDirection(DbParameter value, ParameterDirection condition)
		{
			return condition == (condition & value.Direction);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007250C File Offset: 0x0007070C
		internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
		{
			if (value == null || value == DBNull.Value)
			{
				isNull = true;
				isSqlType = false;
				return;
			}
			INullable nullable = value as INullable;
			if (nullable != null)
			{
				isNull = nullable.IsNull;
				isSqlType = value is SqlBinary || value is SqlBoolean || value is SqlByte || value is SqlBytes || value is SqlChars || value is SqlDateTime || value is SqlDecimal || value is SqlDouble || value is SqlGuid || value is SqlInt16 || value is SqlInt32 || value is SqlInt64 || value is SqlMoney || value is SqlSingle || value is SqlString;
				return;
			}
			isNull = false;
			isSqlType = false;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000725C5 File Offset: 0x000707C5
		internal static Version GetAssemblyVersion()
		{
			if (ADP.s_systemDataVersion == null)
			{
				ADP.s_systemDataVersion = new Version("5.15.24027.2");
			}
			return ADP.s_systemDataVersion;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000725E2 File Offset: 0x000707E2
		internal static bool IsAzureSynapseOnDemandEndpoint(string dataSource)
		{
			return ADP.IsEndpoint(dataSource, "-ondemand") || dataSource.Contains("-ondemand.sql.azuresynapse.");
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000725FE File Offset: 0x000707FE
		internal static bool IsAzureSqlServerEndpoint(string dataSource)
		{
			return ADP.IsEndpoint(dataSource, null);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00072608 File Offset: 0x00070808
		private static bool IsEndpoint(string dataSource, string prefix)
		{
			int num = dataSource.Length;
			int num2 = dataSource.LastIndexOf(',');
			if (num2 >= 0)
			{
				num = num2;
			}
			num2 = dataSource.LastIndexOf('\\', num - 1, num - 1);
			if (num2 > 0)
			{
				num = num2;
			}
			while (num > 0 && char.IsWhiteSpace(dataSource[num - 1]))
			{
				num--;
			}
			for (int i = 0; i < ADP.s_azureSqlServerEndpoints.Length; i++)
			{
				string text = (string.IsNullOrEmpty(prefix) ? ADP.s_azureSqlServerEndpoints[i] : (prefix + ADP.s_azureSqlServerEndpoints[i]));
				if (num > text.Length && string.Compare(dataSource, num - text.Length, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000726B0 File Offset: 0x000708B0
		internal static ArgumentException SingleValuedProperty(string propertyName, string value)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_SingleValuedProperty, new object[] { propertyName, value }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000726E4 File Offset: 0x000708E4
		internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_DoubleValuedProperty, new object[] { propertyName, value1, value2 }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0007271C File Offset: 0x0007091C
		internal static ArgumentException InvalidPrefixSuffix()
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_InvalidPrefixSuffix, Array.Empty<object>()));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00072745 File Offset: 0x00070945
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_ConnectionStringSyntax, new object[] { index }));
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00072765 File Offset: 0x00070965
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_KeywordNotSupported, new object[] { keyword }));
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00072780 File Offset: 0x00070980
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return ADP.InvalidConnectionOptionValue(key, null);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00072789 File Offset: 0x00070989
		internal static Exception InvalidConnectionOptionValue(string key, Exception inner)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidConnectionOptionValue, new object[] { key }), inner);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000727A5 File Offset: 0x000709A5
		internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidConnectionOptionValueLength, new object[] { key, limit }));
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000727C9 File Offset: 0x000709C9
		internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_MissingConnectionOptionValue, new object[] { key, requiredAdditionalKey }));
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000727E8 File Offset: 0x000709E8
		internal static InvalidOperationException InvalidDataDirectory()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidDataDirectory, Array.Empty<object>()));
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000727FE File Offset: 0x000709FE
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_CollectionRemoveInvalidObject, new object[]
			{
				itemType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0007282C File Offset: 0x00070A2C
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, StringsHelper.GetString(Strings.ADP_CollectionNullValue, new object[] { collection.Name, itemType.Name }));
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00072856 File Offset: 0x00070A56
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(StringsHelper.GetString(Strings.ADP_CollectionIndexInt32, new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				collection.Name,
				count.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00072894 File Offset: 0x00070A94
		internal static IndexOutOfRangeException CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return ADP.IndexOutOfRange(StringsHelper.GetString(Strings.ADP_CollectionIndexString, new object[] { itemType.Name, propertyName, propertyValue, collection.Name }));
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000728C5 File Offset: 0x00070AC5
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.ADP_CollectionInvalidType, new object[]
			{
				collection.Name,
				itemType.FullName,
				invalidValue.GetType().FullName
			}));
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000728FC File Offset: 0x00070AFC
		internal static ArgumentException ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SqlConvert_ConvertFailed, new object[] { fromType.FullName, toType.FullName }), innerException);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00072926 File Offset: 0x00070B26
		internal static ArgumentException InvalidMinMaxPoolSizeValues()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMinMaxPoolSizeValues, Array.Empty<object>()));
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0007293C File Offset: 0x00070B3C
		private static string ConnectionStateMsg(ConnectionState state)
		{
			switch (state)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg_Open, Array.Empty<object>());
			case ConnectionState.Connecting:
				return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg_Connecting, Array.Empty<object>());
			case ConnectionState.Open | ConnectionState.Connecting:
			case ConnectionState.Executing:
				goto IL_0082;
			case ConnectionState.Open | ConnectionState.Executing:
				return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg_OpenExecuting, Array.Empty<object>());
			default:
				if (state == (ConnectionState.Open | ConnectionState.Fetching))
				{
					return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg_OpenFetching, Array.Empty<object>());
				}
				if (state != (ConnectionState.Connecting | ConnectionState.Broken))
				{
					goto IL_0082;
				}
				break;
			}
			return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg_Closed, Array.Empty<object>());
			IL_0082:
			return StringsHelper.GetString(Strings.ADP_ConnectionStateMsg, new object[] { state.ToString() });
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000729ED File Offset: 0x00070BED
		internal static InvalidOperationException NoConnectionString()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_NoConnectionString, Array.Empty<object>()));
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00072A04 File Offset: 0x00070C04
		internal static NotImplementedException MethodNotImplemented([CallerMemberName] string methodName = "")
		{
			NotImplementedException ex = new NotImplementedException(methodName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00072A1F File Offset: 0x00070C1F
		internal static Exception StreamClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_StreamClosed, new object[] { method }));
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00072A3A File Offset: 0x00070C3A
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_InvalidSeekOrigin, Array.Empty<object>()), parameterName);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00072A51 File Offset: 0x00070C51
		internal static IOException ErrorReadingFromStream(Exception internalException)
		{
			return ADP.IO(StringsHelper.GetString(Strings.SqlMisc_StreamErrorMessage, Array.Empty<object>()), internalException);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00072A68 File Offset: 0x00070C68
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_CollectionIsNotParent, new object[]
			{
				parameterType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00072A68 File Offset: 0x00070C68
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_CollectionIsNotParent, new object[]
			{
				parameterType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00072A96 File Offset: 0x00070C96
		internal static Exception InternalError(ADP.InternalErrorCode internalError)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InternalProviderError, new object[] { (int)internalError }));
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00072AB6 File Offset: 0x00070CB6
		internal static Exception ClosedConnectionError()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_ClosedConnectionError, Array.Empty<object>()));
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00072ACC File Offset: 0x00070CCC
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_ConnectionAlreadyOpen, new object[] { ADP.ConnectionStateMsg(state) }));
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00072AEC File Offset: 0x00070CEC
		internal static Exception TransactionPresent()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_TransactionPresent, Array.Empty<object>()));
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00072B02 File Offset: 0x00070D02
		internal static Exception LocalTransactionPresent()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_LocalTransactionPresent, Array.Empty<object>()));
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00072B18 File Offset: 0x00070D18
		internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenConnectionPropertySet, new object[]
			{
				property,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00072B3C File Offset: 0x00070D3C
		internal static Exception EmptyDatabaseName()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_EmptyDatabaseName, Array.Empty<object>()));
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00072B52 File Offset: 0x00070D52
		internal static Exception InternalConnectionError(ADP.ConnectionError internalError)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InternalConnectionError, new object[] { (int)internalError }));
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00072B72 File Offset: 0x00070D72
		internal static Exception InvalidConnectRetryCountValue()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLCR_InvalidConnectRetryCountValue, Array.Empty<object>()));
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00072B88 File Offset: 0x00070D88
		internal static Exception InvalidConnectRetryIntervalValue()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.SQLCR_InvalidConnectRetryIntervalValue, Array.Empty<object>()));
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00072B9E File Offset: 0x00070D9E
		internal static Exception DataReaderClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_DataReaderClosed, new object[] { method }));
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00072BB9 File Offset: 0x00070DB9
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_InvalidSourceBufferIndex, new object[]
			{
				maxLen.ToString(CultureInfo.InvariantCulture),
				srcOffset.ToString(CultureInfo.InvariantCulture)
			}), parameterName);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00072BEF File Offset: 0x00070DEF
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(StringsHelper.GetString(Strings.ADP_InvalidDestinationBufferIndex, new object[]
			{
				maxLen.ToString(CultureInfo.InvariantCulture),
				dstOffset.ToString(CultureInfo.InvariantCulture)
			}), parameterName);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00072C25 File Offset: 0x00070E25
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return ADP.IndexOutOfRange(StringsHelper.GetString(Strings.SQL_InvalidBufferSizeOrIndex, new object[]
			{
				numBytes.ToString(CultureInfo.InvariantCulture),
				bufferIndex.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00072C5A File Offset: 0x00070E5A
		internal static Exception InvalidDataLength(long length)
		{
			return ADP.IndexOutOfRange(StringsHelper.GetString(Strings.SQL_InvalidDataLength, new object[] { length.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00072C80 File Offset: 0x00070E80
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00072C97 File Offset: 0x00070E97
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00072CAC File Offset: 0x00070EAC
		internal static void SetCurrentTransaction(Transaction transaction)
		{
			Transaction.Current = transaction;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00072CB4 File Offset: 0x00070EB4
		internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_NonSeqByteAccess, new object[]
			{
				badIndex.ToString(CultureInfo.InvariantCulture),
				currIndex.ToString(CultureInfo.InvariantCulture),
				method
			}));
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00072CED File Offset: 0x00070EED
		internal static Exception NegativeParameter(string parameterName)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_NegativeParameter, new object[] { parameterName }));
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00072D08 File Offset: 0x00070F08
		internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_InvalidXmlMissingColumn, new object[] { collectionName, columnName }));
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00072D27 File Offset: 0x00070F27
		internal static InvalidOperationException AsyncOperationPending()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PendingAsyncOperation, Array.Empty<object>()));
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00072D3D File Offset: 0x00070F3D
		internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00072D4F File Offset: 0x00070F4F
		internal static Exception TooManyRestrictions(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_TooManyRestrictions, new object[] { collectionName }));
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00072D6A File Offset: 0x00070F6A
		internal static Exception CommandTextRequired(string method)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_CommandTextRequired, new object[] { method }));
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00072D85 File Offset: 0x00070F85
		internal static Exception UninitializedParameterSize(int index, Type dataType)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_UninitializedParameterSize, new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dataType.Name
			}));
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00072DB4 File Offset: 0x00070FB4
		internal static Exception PrepareParameterType(DbCommand cmd)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterType, new object[] { cmd.GetType().Name }));
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00072DD9 File Offset: 0x00070FD9
		internal static Exception PrepareParameterSize(DbCommand cmd)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterSize, new object[] { cmd.GetType().Name }));
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00072DFE File Offset: 0x00070FFE
		internal static Exception PrepareParameterScale(DbCommand cmd, string type)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PrepareParameterScale, new object[]
			{
				cmd.GetType().Name,
				type
			}));
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00072E27 File Offset: 0x00071027
		internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_MismatchedAsyncResult, new object[] { expectedMethod, gotMethod }));
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00072E46 File Offset: 0x00071046
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00072E58 File Offset: 0x00071058
		internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00072E78 File Offset: 0x00071078
		internal static ArgumentException BadParameterName(string parameterName)
		{
			ArgumentException ex = new ArgumentException(StringsHelper.GetString(Strings.ADP_BadParameterName, new object[] { parameterName }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00072EA8 File Offset: 0x000710A8
		internal static Exception DeriveParametersNotSupported(IDbCommand value)
		{
			return ADP.DataAdapter(StringsHelper.GetString(Strings.ADP_DeriveParametersNotSupported, new object[]
			{
				value.GetType().Name,
				value.CommandType.ToString()
			}));
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00072EEF File Offset: 0x000710EF
		internal static Exception NoStoredProcedureExists(string sproc)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_NoStoredProcedureExists, new object[] { sproc }));
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00072F0A File Offset: 0x0007110A
		internal static Exception DataTableDoesNotExist(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_DataTableDoesNotExist, new object[] { collectionName }));
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00072F25 File Offset: 0x00071125
		internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00072F37 File Offset: 0x00071137
		internal static Exception QueryFailed(string collectionName, Exception e)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.MDF_QueryFailed, new object[] { collectionName }), e);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00072F53 File Offset: 0x00071153
		internal static Exception NoColumns()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_NoColumns, Array.Empty<object>()));
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00072F69 File Offset: 0x00071169
		internal static InvalidOperationException ConnectionRequired(string method)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_ConnectionRequired, new object[] { method }));
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00072F84 File Offset: 0x00071184
		internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenConnectionRequired, new object[]
			{
				method,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00072FA8 File Offset: 0x000711A8
		internal static Exception OpenReaderExists(bool marsOn)
		{
			return ADP.OpenReaderExists(null, marsOn);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00072FB1 File Offset: 0x000711B1
		internal static Exception OpenReaderExists(Exception e, bool marsOn)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_OpenReaderExists, new object[] { marsOn ? "Command" : "Connection" }), e);
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00072FDB File Offset: 0x000711DB
		internal static Exception InvalidXml()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_InvalidXml, Array.Empty<object>()));
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00072FF1 File Offset: 0x000711F1
		internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_InvalidXmlInvalidValue, new object[] { collectionName, columnName }));
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00073010 File Offset: 0x00071210
		internal static Exception CollectionNameIsNotUnique(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_CollectionNameISNotUnique, new object[] { collectionName }));
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0007302B File Offset: 0x0007122B
		internal static Exception UnableToBuildCollection(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_UnableToBuildCollection, new object[] { collectionName }));
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00073046 File Offset: 0x00071246
		internal static Exception UndefinedCollection(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_UndefinedCollection, new object[] { collectionName }));
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00073061 File Offset: 0x00071261
		internal static Exception UnsupportedVersion(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_UnsupportedVersion, new object[] { collectionName }));
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0007307C File Offset: 0x0007127C
		internal static Exception AmbiguousCollectionName(string collectionName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_AmbiguousCollectionName, new object[] { collectionName }));
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00073097 File Offset: 0x00071297
		internal static Exception MissingDataSourceInformationColumn()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_MissingDataSourceInformationColumn, Array.Empty<object>()));
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000730AD File Offset: 0x000712AD
		internal static Exception IncorrectNumberOfDataSourceInformationRows()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_IncorrectNumberOfDataSourceInformationRows, Array.Empty<object>()));
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000730C3 File Offset: 0x000712C3
		internal static Exception MissingRestrictionColumn()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_MissingRestrictionColumn, Array.Empty<object>()));
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000730D9 File Offset: 0x000712D9
		internal static Exception MissingRestrictionRow()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_MissingRestrictionRow, Array.Empty<object>()));
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000730EF File Offset: 0x000712EF
		internal static Exception UndefinedPopulationMechanism(string populationMechanism)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.MDF_UndefinedPopulationMechanism, new object[] { populationMechanism }));
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007310A File Offset: 0x0007130A
		internal static Exception PooledOpenTimeout()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_PooledOpenTimeout, Array.Empty<object>()));
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00073120 File Offset: 0x00071320
		internal static Exception NonPooledOpenTimeout()
		{
			return ADP.TimeoutException(StringsHelper.GetString(Strings.ADP_NonPooledOpenTimeout, Array.Empty<object>()), null);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00073137 File Offset: 0x00071337
		internal static InvalidOperationException TransactionConnectionMismatch()
		{
			return ADP.Provider(StringsHelper.GetString(Strings.ADP_TransactionConnectionMismatch, Array.Empty<object>()));
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0007314D File Offset: 0x0007134D
		internal static InvalidOperationException TransactionRequired(string method)
		{
			return ADP.Provider(StringsHelper.GetString(Strings.ADP_TransactionRequired, new object[] { method }));
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00073168 File Offset: 0x00071368
		internal static InvalidOperationException TransactionCompletedButNotDisposed()
		{
			return ADP.Provider(StringsHelper.GetString(Strings.ADP_TransactionCompletedButNotDisposed, Array.Empty<object>()));
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0007317E File Offset: 0x0007137E
		internal static Exception InvalidMetaDataValue()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMetaDataValue, Array.Empty<object>()));
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00073194 File Offset: 0x00071394
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_NonSequentialColumnAccess, new object[]
			{
				badCol.ToString(CultureInfo.InvariantCulture),
				currCol.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000731C9 File Offset: 0x000713C9
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidDataType, new object[] { typecode.ToString() }));
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000731F0 File Offset: 0x000713F0
		internal static ArgumentException UnknownDataType(Type dataType)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_UnknownDataType, new object[] { dataType.FullName }));
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00073210 File Offset: 0x00071410
		internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_DbTypeNotSupported, new object[]
			{
				type.ToString(),
				enumtype.Name
			}));
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00073240 File Offset: 0x00071440
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string adp_UnknownDataTypeCode = Strings.ADP_UnknownDataTypeCode;
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)typeCode;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = dataType.FullName;
			return ADP.Argument(StringsHelper.GetString(adp_UnknownDataTypeCode, array));
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0007327C File Offset: 0x0007147C
		internal static ArgumentException InvalidOffsetValue(int value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidOffsetValue, new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000732A2 File Offset: 0x000714A2
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidSizeValue, new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000732C8 File Offset: 0x000714C8
		internal static ArgumentException ParameterValueOutOfRange(decimal value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, new object[] { value.ToString(null) }));
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000732EA File Offset: 0x000714EA
		internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, new object[] { value.ToString() }));
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00073311 File Offset: 0x00071511
		internal static ArgumentException ParameterValueOutOfRange(string value)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_ParameterValueOutOfRange, new object[] { value }));
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0007332C File Offset: 0x0007152C
		internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_VersionDoesNotSupportDataType, new object[] { typeName }));
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00073348 File Offset: 0x00071548
		internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
		{
			string @string = StringsHelper.GetString(Strings.ADP_ParameterConversionFailed, new object[]
			{
				value.GetType().Name,
				destType.Name
			});
			Exception ex;
			if (inner is ArgumentException)
			{
				ex = new ArgumentException(@string, inner);
			}
			else if (inner is FormatException)
			{
				ex = new FormatException(@string, inner);
			}
			else if (inner is InvalidCastException)
			{
				ex = new InvalidCastException(@string, inner);
			}
			else if (inner is OverflowException)
			{
				ex = new OverflowException(@string, inner);
			}
			else
			{
				ex = inner;
			}
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000733CE File Offset: 0x000715CE
		internal static Exception ParametersMappingIndex(int index, DbParameterCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000733E2 File Offset: 0x000715E2
		internal static Exception ParametersSourceIndex(string parameterName, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionIndexString(parameterType, "ParameterName", parameterName, collection.GetType());
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000733F6 File Offset: 0x000715F6
		internal static Exception ParameterNull(string parameter, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00073405 File Offset: 0x00071605
		internal static Exception InvalidParameterType(DbParameterCollection collection, Type parameterType, object invalidValue)
		{
			return ADP.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00073414 File Offset: 0x00071614
		internal static Exception ParallelTransactionsNotSupported(DbConnection obj)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_ParallelTransactionsNotSupported, new object[] { obj.GetType().Name }));
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00073439 File Offset: 0x00071639
		internal static Exception TransactionZombied(DbTransaction obj)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_TransactionZombied, new object[] { obj.GetType().Name }));
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0007345E File Offset: 0x0007165E
		internal static InvalidOperationException InvalidMixedUsageOfSecureAndClearCredential()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureAndClearCredential, Array.Empty<object>()));
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00073474 File Offset: 0x00071674
		internal static ArgumentException InvalidMixedArgumentOfSecureAndClearCredential()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureAndClearCredential, Array.Empty<object>()));
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0007348A File Offset: 0x0007168A
		internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity, Array.Empty<object>()));
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000734A0 File Offset: 0x000716A0
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity, Array.Empty<object>()));
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000734B6 File Offset: 0x000716B6
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity, Array.Empty<object>()));
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000734CC File Offset: 0x000716CC
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndUserIDPassword()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword, Array.Empty<object>()));
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000734E2 File Offset: 0x000716E2
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndAuthentication()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndAuthentication, Array.Empty<object>()));
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000734F8 File Offset: 0x000716F8
		internal static Exception InvalidMixedUsageOfCredentialAndAccessToken()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfCredentialAndAccessToken, Array.Empty<object>()));
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0007350E File Offset: 0x0007170E
		internal static bool IsEmpty(string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00073518 File Offset: 0x00071718
		internal static Task<T> CreatedTaskWithException<T>(Exception ex)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(ex);
			return taskCompletionSource.Task;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00073538 File Offset: 0x00071738
		internal static Task<T> CreatedTaskWithCancellation<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00071B45 File Offset: 0x0006FD45
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00073557 File Offset: 0x00071757
		internal static void CheckArgumentLength(string value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.ADP_EmptyString, new object[] { parameterName }));
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00073582 File Offset: 0x00071782
		internal static ArgumentOutOfRangeException InvalidIsolationLevel(global::System.Data.IsolationLevel value)
		{
			return ADP.InvalidEnumerationValue(typeof(global::System.Data.IsolationLevel), (int)value);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00073594 File Offset: 0x00071794
		internal static ArgumentOutOfRangeException InvalidKeyRestrictionBehavior(KeyRestrictionBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(KeyRestrictionBehavior), (int)value);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000735A6 File Offset: 0x000717A6
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return ADP.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000735B8 File Offset: 0x000717B8
		internal static ArgumentException InvalidKeyname(string parameterName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidKey, Array.Empty<object>()), parameterName);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000735CF File Offset: 0x000717CF
		internal static ArgumentException InvalidValue(string parameterName)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidValue, Array.Empty<object>()), parameterName);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000735E6 File Offset: 0x000717E6
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndContextConnection()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection, Array.Empty<object>()));
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000735FC File Offset: 0x000717FC
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndContextConnection()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndContextConnection, Array.Empty<object>()));
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00073612 File Offset: 0x00071812
		internal static Exception InvalidMixedUsageOfAccessTokenAndCredential()
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_InvalidMixedUsageOfAccessTokenAndCredential, Array.Empty<object>()));
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00073628 File Offset: 0x00071828
		internal static Exception InvalidXMLBadVersion()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidXMLBadVersion, Array.Empty<object>()));
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0007363E File Offset: 0x0007183E
		internal static Exception NotAPermissionElement()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_NotAPermissionElement, Array.Empty<object>()));
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00073654 File Offset: 0x00071854
		internal static Exception PermissionTypeMismatch()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_PermissionTypeMismatch, Array.Empty<object>()));
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0007366A File Offset: 0x0007186A
		internal static Exception NumericToDecimalOverflow()
		{
			return ADP.InvalidCast(StringsHelper.GetString(Strings.ADP_NumericToDecimalOverflow, Array.Empty<object>()));
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00073680 File Offset: 0x00071880
		internal static Exception InvalidCommandTimeout(int value, string name)
		{
			return ADP.Argument(StringsHelper.GetString(Strings.ADP_InvalidCommandTimeout, new object[] { value.ToString(CultureInfo.InvariantCulture) }), name);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000736A7 File Offset: 0x000718A7
		internal static InvalidOperationException ComputerNameEx(int lastError)
		{
			return ADP.InvalidOperation(StringsHelper.GetString(Strings.ADP_ComputerNameEx, new object[] { lastError }));
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000736C7 File Offset: 0x000718C7
		internal static PlatformNotSupportedException SNIPlatformNotSupported(string platform)
		{
			return new PlatformNotSupportedException(StringsHelper.GetString(Strings.SNI_PlatformNotSupportedNetFx, new object[] { platform }));
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000736E2 File Offset: 0x000718E2
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000736EC File Offset: 0x000718EC
		internal static string GetComputerNameDnsFullyQualified()
		{
			string text;
			if (ADP.s_isPlatformNT5)
			{
				int num = 0;
				int num2 = 0;
				if (SafeNativeMethods.GetComputerNameEx(3, null, ref num) == 0)
				{
					num2 = Marshal.GetLastWin32Error();
				}
				if ((num2 != 0 && num2 != 234) || num <= 0)
				{
					throw ADP.ComputerNameEx(num2);
				}
				StringBuilder stringBuilder = new StringBuilder(num);
				num = stringBuilder.Capacity;
				if (SafeNativeMethods.GetComputerNameEx(3, stringBuilder, ref num) == 0)
				{
					throw ADP.ComputerNameEx(Marshal.GetLastWin32Error());
				}
				text = stringBuilder.ToString();
			}
			else
			{
				text = ADP.MachineName();
			}
			return text;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0007375F File Offset: 0x0007195F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (4 == ADP.s_ptrSize)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00073788 File Offset: 0x00071988
		internal static object LocalMachineRegistryValue(string subkey, string queryvalue)
		{
			new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\" + subkey).Assert();
			object obj;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(subkey, false))
				{
					obj = ((registryKey != null) ? registryKey.GetValue(queryvalue) : null);
				}
			}
			catch (SecurityException ex)
			{
				ADP.TraceExceptionWithoutRethrow(ex);
				obj = null;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return obj;
		}

		// Token: 0x04000B5A RID: 2906
		private static Task<bool> s_trueTask;

		// Token: 0x04000B5B RID: 2907
		private static Task<bool> s_falseTask;

		// Token: 0x04000B5C RID: 2908
		internal const CompareOptions DefaultCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04000B5D RID: 2909
		internal const int DefaultConnectionTimeout = 15;

		// Token: 0x04000B5E RID: 2910
		internal const int InfiniteConnectionTimeout = 0;

		// Token: 0x04000B5F RID: 2911
		internal const int MaxBufferAccessTokenExpiry = 600;

		// Token: 0x04000B60 RID: 2912
		private static readonly MethodInfo s_method = typeof(InvalidUdtException).GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000B61 RID: 2913
		internal const string ColumnEncryptionSystemProviderNamePrefix = "MSSQL_";

		// Token: 0x04000B62 RID: 2914
		internal const string Command = "Command";

		// Token: 0x04000B63 RID: 2915
		internal const string Connection = "Connection";

		// Token: 0x04000B64 RID: 2916
		internal const string Parameter = "Parameter";

		// Token: 0x04000B65 RID: 2917
		internal const string ParameterName = "ParameterName";

		// Token: 0x04000B66 RID: 2918
		internal const string ParameterSetPosition = "set_Position";

		// Token: 0x04000B67 RID: 2919
		internal const int DefaultCommandTimeout = 30;

		// Token: 0x04000B68 RID: 2920
		internal const float FailoverTimeoutStep = 0.08f;

		// Token: 0x04000B69 RID: 2921
		internal const int CharSize = 2;

		// Token: 0x04000B6A RID: 2922
		private static Version s_systemDataVersion;

		// Token: 0x04000B6B RID: 2923
		private const string ONDEMAND_PREFIX = "-ondemand";

		// Token: 0x04000B6C RID: 2924
		private const string AZURE_SYNAPSE = "-ondemand.sql.azuresynapse.";

		// Token: 0x04000B6D RID: 2925
		internal static readonly string[] s_azureSqlServerEndpoints = new string[]
		{
			StringsHelper.GetString(Strings.AZURESQL_GenericEndpoint, Array.Empty<object>()),
			StringsHelper.GetString(Strings.AZURESQL_GermanEndpoint, Array.Empty<object>()),
			StringsHelper.GetString(Strings.AZURESQL_UsGovEndpoint, Array.Empty<object>()),
			StringsHelper.GetString(Strings.AZURESQL_ChinaEndpoint, Array.Empty<object>())
		};

		// Token: 0x04000B6E RID: 2926
		internal static readonly IntPtr s_ptrZero = IntPtr.Zero;

		// Token: 0x04000B6F RID: 2927
		internal const float FailoverTimeoutStepForTnir = 0.125f;

		// Token: 0x04000B70 RID: 2928
		internal const int MinimumTimeoutForTnirMs = 500;

		// Token: 0x04000B71 RID: 2929
		internal static readonly int s_ptrSize = IntPtr.Size;

		// Token: 0x04000B72 RID: 2930
		internal static readonly IntPtr s_invalidPtr = new IntPtr(-1);

		// Token: 0x04000B73 RID: 2931
		internal static readonly bool s_isWindowsNT = PlatformID.Win32NT == Environment.OSVersion.Platform;

		// Token: 0x04000B74 RID: 2932
		internal static readonly bool s_isPlatformNT5 = ADP.s_isWindowsNT && Environment.OSVersion.Version.Major >= 5;

		// Token: 0x02000278 RID: 632
		internal enum InternalErrorCode
		{
			// Token: 0x0400176E RID: 5998
			UnpooledObjectHasOwner,
			// Token: 0x0400176F RID: 5999
			UnpooledObjectHasWrongOwner,
			// Token: 0x04001770 RID: 6000
			PushingObjectSecondTime,
			// Token: 0x04001771 RID: 6001
			PooledObjectHasOwner,
			// Token: 0x04001772 RID: 6002
			PooledObjectInPoolMoreThanOnce,
			// Token: 0x04001773 RID: 6003
			CreateObjectReturnedNull,
			// Token: 0x04001774 RID: 6004
			NewObjectCannotBePooled,
			// Token: 0x04001775 RID: 6005
			NonPooledObjectUsedMoreThanOnce,
			// Token: 0x04001776 RID: 6006
			AttemptingToPoolOnRestrictedToken,
			// Token: 0x04001777 RID: 6007
			ConvertSidToStringSidWReturnedNull = 10,
			// Token: 0x04001778 RID: 6008
			AttemptingToConstructReferenceCollectionOnStaticObject = 12,
			// Token: 0x04001779 RID: 6009
			AttemptingToEnlistTwice,
			// Token: 0x0400177A RID: 6010
			CreateReferenceCollectionReturnedNull,
			// Token: 0x0400177B RID: 6011
			PooledObjectWithoutPool,
			// Token: 0x0400177C RID: 6012
			UnexpectedWaitAnyResult,
			// Token: 0x0400177D RID: 6013
			SynchronousConnectReturnedPending,
			// Token: 0x0400177E RID: 6014
			CompletedConnectReturnedPending,
			// Token: 0x0400177F RID: 6015
			NameValuePairNext = 20,
			// Token: 0x04001780 RID: 6016
			InvalidParserState1,
			// Token: 0x04001781 RID: 6017
			InvalidParserState2,
			// Token: 0x04001782 RID: 6018
			InvalidParserState3,
			// Token: 0x04001783 RID: 6019
			InvalidBuffer = 30,
			// Token: 0x04001784 RID: 6020
			UnimplementedSMIMethod = 40,
			// Token: 0x04001785 RID: 6021
			InvalidSmiCall,
			// Token: 0x04001786 RID: 6022
			SqlDependencyObtainProcessDispatcherFailureObjectHandle = 50,
			// Token: 0x04001787 RID: 6023
			SqlDependencyProcessDispatcherFailureCreateInstance,
			// Token: 0x04001788 RID: 6024
			SqlDependencyProcessDispatcherFailureAppDomain,
			// Token: 0x04001789 RID: 6025
			SqlDependencyCommandHashIsNotAssociatedWithNotification,
			// Token: 0x0400178A RID: 6026
			UnknownTransactionFailure = 60
		}

		// Token: 0x02000279 RID: 633
		internal enum ConnectionError
		{
			// Token: 0x0400178C RID: 6028
			BeginGetConnectionReturnsNull,
			// Token: 0x0400178D RID: 6029
			GetConnectionReturnsNull,
			// Token: 0x0400178E RID: 6030
			ConnectionOptionsMissing,
			// Token: 0x0400178F RID: 6031
			CouldNotSwitchToClosedPreviouslyOpenedState
		}
	}
}
