using System;
using System.ComponentModel;

namespace NLog
{
	// Token: 0x02000007 RID: 7
	[CLSCompliant(false)]
	public interface ILoggerBase
	{
		// Token: 0x06000140 RID: 320
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, object value);

		// Token: 0x06000141 RID: 321
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Log(LogLevel level, IFormatProvider formatProvider, object value);

		// Token: 0x06000142 RID: 322
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, object arg1, object arg2);

		// Token: 0x06000143 RID: 323
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, object arg1, object arg2, object arg3);

		// Token: 0x06000144 RID: 324
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000145 RID: 325
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, bool argument);

		// Token: 0x06000146 RID: 326
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000147 RID: 327
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, char argument);

		// Token: 0x06000148 RID: 328
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000149 RID: 329
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, byte argument);

		// Token: 0x0600014A RID: 330
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument);

		// Token: 0x0600014B RID: 331
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, string argument);

		// Token: 0x0600014C RID: 332
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument);

		// Token: 0x0600014D RID: 333
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, int argument);

		// Token: 0x0600014E RID: 334
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600014F RID: 335
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, long argument);

		// Token: 0x06000150 RID: 336
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000151 RID: 337
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, float argument);

		// Token: 0x06000152 RID: 338
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000153 RID: 339
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, double argument);

		// Token: 0x06000154 RID: 340
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000155 RID: 341
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, decimal argument);

		// Token: 0x06000156 RID: 342
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000157 RID: 343
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, object argument);

		// Token: 0x06000158 RID: 344
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000159 RID: 345
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, sbyte argument);

		// Token: 0x0600015A RID: 346
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x0600015B RID: 347
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, uint argument);

		// Token: 0x0600015C RID: 348
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x0600015D RID: 349
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, string message, ulong argument);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600015E RID: 350
		// (remove) Token: 0x0600015F RID: 351
		event EventHandler<EventArgs> LoggerReconfigured;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000160 RID: 352
		string Name { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000161 RID: 353
		LogFactory Factory { get; }

		// Token: 0x06000162 RID: 354
		bool IsEnabled(LogLevel level);

		// Token: 0x06000163 RID: 355
		void Log(LogEventInfo logEvent);

		// Token: 0x06000164 RID: 356
		void Log(Type wrapperType, LogEventInfo logEvent);

		// Token: 0x06000165 RID: 357
		void Log<T>(LogLevel level, T value);

		// Token: 0x06000166 RID: 358
		void Log<T>(LogLevel level, IFormatProvider formatProvider, T value);

		// Token: 0x06000167 RID: 359
		void Log(LogLevel level, LogMessageGenerator messageFunc);

		// Token: 0x06000168 RID: 360
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args) instead. Marked obsolete before v4.3.11")]
		void LogException(LogLevel level, [Localizable(false)] string message, Exception exception);

		// Token: 0x06000169 RID: 361
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600016A RID: 362
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600016B RID: 363
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600016C RID: 364
		void Log(LogLevel level, [Localizable(false)] string message);

		// Token: 0x0600016D RID: 365
		[MessageTemplateFormatMethod("message")]
		void Log(LogLevel level, [Localizable(false)] string message, params object[] args);

		// Token: 0x0600016E RID: 366
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args) instead. Marked obsolete before v4.3.11")]
		void Log(LogLevel level, [Localizable(false)] string message, Exception exception);

		// Token: 0x0600016F RID: 367
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000170 RID: 368
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000171 RID: 369
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000172 RID: 370
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x06000173 RID: 371
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x06000174 RID: 372
		[MessageTemplateFormatMethod("message")]
		void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
	}
}
