using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200000E RID: 14
	[CLSCompliant(true)]
	public class Logger : ILogger, ILoggerBase, ISuppress
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00003ECC File Offset: 0x000020CC
		[Conditional("DEBUG")]
		public void ConditionalDebug<T>(T value)
		{
			this.Debug<T>(value);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003ED5 File Offset: 0x000020D5
		[Conditional("DEBUG")]
		public void ConditionalDebug<T>(IFormatProvider formatProvider, T value)
		{
			this.Debug<T>(formatProvider, value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00003EDF File Offset: 0x000020DF
		[Conditional("DEBUG")]
		public void ConditionalDebug(LogMessageGenerator messageFunc)
		{
			this.Debug(messageFunc);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00003EE8 File Offset: 0x000020E8
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(Exception exception, string message, params object[] args)
		{
			this.Debug(exception, message, args);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00003EF3 File Offset: 0x000020F3
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Debug(exception, formatProvider, message, args);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00003F00 File Offset: 0x00002100
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Debug(formatProvider, message, args);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00003F0B File Offset: 0x0000210B
		[Conditional("DEBUG")]
		public void ConditionalDebug(string message)
		{
			this.Debug(message);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00003F14 File Offset: 0x00002114
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, params object[] args)
		{
			this.Debug(message, args);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00003F1E File Offset: 0x0000211E
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
		{
			this.Debug<TArgument>(formatProvider, message, argument);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00003F29 File Offset: 0x00002129
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument>(string message, TArgument argument)
		{
			this.Debug<TArgument>(message, argument);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00003F33 File Offset: 0x00002133
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Debug<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00003F40 File Offset: 0x00002140
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Debug<TArgument1, TArgument2>(message, argument1, argument2);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00003F4B File Offset: 0x0000214B
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Debug<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00003F5A File Offset: 0x0000215A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Debug<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00003F67 File Offset: 0x00002167
		[Conditional("DEBUG")]
		public void ConditionalDebug(object value)
		{
			this.Debug(value);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00003F70 File Offset: 0x00002170
		[Conditional("DEBUG")]
		public void ConditionalDebug(IFormatProvider formatProvider, object value)
		{
			this.Debug(formatProvider, value);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00003F7A File Offset: 0x0000217A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, object arg1, object arg2)
		{
			this.Debug(message, arg1, arg2);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00003F85 File Offset: 0x00002185
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, object arg1, object arg2, object arg3)
		{
			this.Debug(message, arg1, arg2, arg3);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00003F92 File Offset: 0x00002192
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, bool argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00003F9D File Offset: 0x0000219D
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, bool argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00003FA7 File Offset: 0x000021A7
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, char argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00003FB2 File Offset: 0x000021B2
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, char argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003FBC File Offset: 0x000021BC
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, byte argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00003FC7 File Offset: 0x000021C7
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, byte argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00003FD1 File Offset: 0x000021D1
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, string argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00003FDC File Offset: 0x000021DC
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, string argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00003FE6 File Offset: 0x000021E6
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, int argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00003FF1 File Offset: 0x000021F1
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, int argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00003FFB File Offset: 0x000021FB
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, long argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00004006 File Offset: 0x00002206
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, long argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00004010 File Offset: 0x00002210
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, float argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000401B File Offset: 0x0000221B
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, float argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00004025 File Offset: 0x00002225
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, double argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00004030 File Offset: 0x00002230
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, double argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000403A File Offset: 0x0000223A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, decimal argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00004045 File Offset: 0x00002245
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, decimal argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000404F File Offset: 0x0000224F
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(IFormatProvider formatProvider, string message, object argument)
		{
			this.Debug(formatProvider, message, argument);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000405A File Offset: 0x0000225A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalDebug(string message, object argument)
		{
			this.Debug(message, argument);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00004064 File Offset: 0x00002264
		[Conditional("DEBUG")]
		public void ConditionalTrace<T>(T value)
		{
			this.Trace<T>(value);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000406D File Offset: 0x0000226D
		[Conditional("DEBUG")]
		public void ConditionalTrace<T>(IFormatProvider formatProvider, T value)
		{
			this.Trace<T>(formatProvider, value);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00004077 File Offset: 0x00002277
		[Conditional("DEBUG")]
		public void ConditionalTrace(LogMessageGenerator messageFunc)
		{
			this.Trace(messageFunc);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00004080 File Offset: 0x00002280
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(Exception exception, string message, params object[] args)
		{
			this.Trace(exception, message, args);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000408B File Offset: 0x0000228B
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(Exception exception, IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Trace(exception, formatProvider, message, args);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00004098 File Offset: 0x00002298
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, params object[] args)
		{
			this.Trace(formatProvider, message, args);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000040A3 File Offset: 0x000022A3
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message)
		{
			this.Trace(message);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000040AC File Offset: 0x000022AC
		[Conditional("DEBUG")]
		public void ConditionalTrace(string message, params object[] args)
		{
			this.Trace(message, args);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000040B6 File Offset: 0x000022B6
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
		{
			this.Trace<TArgument>(formatProvider, message, argument);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000040C1 File Offset: 0x000022C1
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument>(string message, TArgument argument)
		{
			this.Trace<TArgument>(message, argument);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000040CB File Offset: 0x000022CB
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Trace<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000040D8 File Offset: 0x000022D8
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
		{
			this.Trace<TArgument1, TArgument2>(message, argument1, argument2);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000040E3 File Offset: 0x000022E3
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Trace<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000040F2 File Offset: 0x000022F2
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			this.Trace<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000040FF File Offset: 0x000022FF
		[Conditional("DEBUG")]
		public void ConditionalTrace(object value)
		{
			this.Trace(value);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00004108 File Offset: 0x00002308
		[Conditional("DEBUG")]
		public void ConditionalTrace(IFormatProvider formatProvider, object value)
		{
			this.Trace(formatProvider, value);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00004112 File Offset: 0x00002312
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, object arg1, object arg2)
		{
			this.Trace(message, arg1, arg2);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000411D File Offset: 0x0000231D
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, object arg1, object arg2, object arg3)
		{
			this.Trace(message, arg1, arg2, arg3);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000412A File Offset: 0x0000232A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, bool argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00004135 File Offset: 0x00002335
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, bool argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000413F File Offset: 0x0000233F
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, char argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000414A File Offset: 0x0000234A
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, char argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00004154 File Offset: 0x00002354
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, byte argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000415F File Offset: 0x0000235F
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, byte argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004169 File Offset: 0x00002369
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, string argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00004174 File Offset: 0x00002374
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, string argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000417E File Offset: 0x0000237E
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, int argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00004189 File Offset: 0x00002389
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, int argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00004193 File Offset: 0x00002393
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, long argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000419E File Offset: 0x0000239E
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, long argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000041A8 File Offset: 0x000023A8
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, float argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000041B3 File Offset: 0x000023B3
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, float argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000041BD File Offset: 0x000023BD
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, double argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000041C8 File Offset: 0x000023C8
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, double argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000041D2 File Offset: 0x000023D2
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, decimal argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000041DD File Offset: 0x000023DD
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, decimal argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000041E7 File Offset: 0x000023E7
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(IFormatProvider formatProvider, string message, object argument)
		{
			this.Trace(formatProvider, message, argument);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000041F2 File Offset: 0x000023F2
		[Conditional("DEBUG")]
		[MessageTemplateFormatMethod("message")]
		public void ConditionalTrace(string message, object argument)
		{
			this.Trace(message, argument);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000041FC File Offset: 0x000023FC
		public bool IsTraceEnabled
		{
			get
			{
				return this._contextLogger._isTraceEnabled;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000420B File Offset: 0x0000240B
		public bool IsDebugEnabled
		{
			get
			{
				return this._contextLogger._isDebugEnabled;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000421A File Offset: 0x0000241A
		public bool IsInfoEnabled
		{
			get
			{
				return this._contextLogger._isInfoEnabled;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00004229 File Offset: 0x00002429
		public bool IsWarnEnabled
		{
			get
			{
				return this._contextLogger._isWarnEnabled;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00004238 File Offset: 0x00002438
		public bool IsErrorEnabled
		{
			get
			{
				return this._contextLogger._isErrorEnabled;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00004247 File Offset: 0x00002447
		public bool IsFatalEnabled
		{
			get
			{
				return this._contextLogger._isFatalEnabled;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00004256 File Offset: 0x00002456
		public void Trace<T>(T value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Trace, null, value);
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000426D File Offset: 0x0000246D
		public void Trace<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Trace, formatProvider, value);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00004284 File Offset: 0x00002484
		public void Trace(LogMessageGenerator messageFunc)
		{
			if (this.IsTraceEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Trace, null, messageFunc());
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000042AE File Offset: 0x000024AE
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void TraceException([Localizable(false)] string message, Exception exception)
		{
			this.Trace(message, exception);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000042B8 File Offset: 0x000024B8
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, args);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000042D0 File Offset: 0x000024D0
		public void Trace([Localizable(false)] string message)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, null, message);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000042E7 File Offset: 0x000024E7
		[MessageTemplateFormatMethod("message")]
		public void Trace([Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, args);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000042FE File Offset: 0x000024FE
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Trace([Localizable(false)] string message, Exception exception)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, message, null);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00004316 File Offset: 0x00002516
		public void Trace(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, message, null);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000432E File Offset: 0x0000252E
		[MessageTemplateFormatMethod("message")]
		public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, message, args);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004346 File Offset: 0x00002546
		[MessageTemplateFormatMethod("message")]
		public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, exception, formatProvider, message, args);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00004360 File Offset: 0x00002560
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00004388 File Offset: 0x00002588
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsTraceEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Trace(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000043DD File Offset: 0x000025DD
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000440D File Offset: 0x0000260D
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000443B File Offset: 0x0000263B
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00004475 File Offset: 0x00002675
		[MessageTemplateFormatMethod("message")]
		public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000044AD File Offset: 0x000026AD
		public void Debug<T>(T value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Debug, null, value);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000044C4 File Offset: 0x000026C4
		public void Debug<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Debug, formatProvider, value);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000044DB File Offset: 0x000026DB
		public void Debug(LogMessageGenerator messageFunc)
		{
			if (this.IsDebugEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Debug, null, messageFunc());
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00004505 File Offset: 0x00002705
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void DebugException([Localizable(false)] string message, Exception exception)
		{
			this.Debug(message, exception);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000450F File Offset: 0x0000270F
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, args);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00004527 File Offset: 0x00002727
		public void Debug([Localizable(false)] string message)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, null, message);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000453E File Offset: 0x0000273E
		[MessageTemplateFormatMethod("message")]
		public void Debug([Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, args);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00004555 File Offset: 0x00002755
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Debug([Localizable(false)] string message, Exception exception)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, message, null);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000456D File Offset: 0x0000276D
		public void Debug(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, message, null);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00004585 File Offset: 0x00002785
		[MessageTemplateFormatMethod("message")]
		public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, message, args);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000459D File Offset: 0x0000279D
		[MessageTemplateFormatMethod("message")]
		public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, exception, formatProvider, message, args);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000045B7 File Offset: 0x000027B7
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000045E0 File Offset: 0x000027E0
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsDebugEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Debug(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00004635 File Offset: 0x00002835
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004665 File Offset: 0x00002865
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00004693 File Offset: 0x00002893
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000046CD File Offset: 0x000028CD
		[MessageTemplateFormatMethod("message")]
		public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00004705 File Offset: 0x00002905
		public void Info<T>(T value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Info, null, value);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000471C File Offset: 0x0000291C
		public void Info<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Info, formatProvider, value);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00004733 File Offset: 0x00002933
		public void Info(LogMessageGenerator messageFunc)
		{
			if (this.IsInfoEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Info, null, messageFunc());
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000475D File Offset: 0x0000295D
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void InfoException([Localizable(false)] string message, Exception exception)
		{
			this.Info(message, exception);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00004767 File Offset: 0x00002967
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, args);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000477F File Offset: 0x0000297F
		public void Info([Localizable(false)] string message)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, null, message);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00004796 File Offset: 0x00002996
		[MessageTemplateFormatMethod("message")]
		public void Info([Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, args);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000047AD File Offset: 0x000029AD
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Info([Localizable(false)] string message, Exception exception)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, message, null);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000047C5 File Offset: 0x000029C5
		public void Info(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, message, null);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000047DD File Offset: 0x000029DD
		[MessageTemplateFormatMethod("message")]
		public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, message, args);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000047F5 File Offset: 0x000029F5
		[MessageTemplateFormatMethod("message")]
		public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, exception, formatProvider, message, args);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000480F File Offset: 0x00002A0F
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00004838 File Offset: 0x00002A38
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsInfoEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Info(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000488D File Offset: 0x00002A8D
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000048BD File Offset: 0x00002ABD
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000048EB File Offset: 0x00002AEB
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00004925 File Offset: 0x00002B25
		[MessageTemplateFormatMethod("message")]
		public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000495D File Offset: 0x00002B5D
		public void Warn<T>(T value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Warn, null, value);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00004974 File Offset: 0x00002B74
		public void Warn<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Warn, formatProvider, value);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000498B File Offset: 0x00002B8B
		public void Warn(LogMessageGenerator messageFunc)
		{
			if (this.IsWarnEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Warn, null, messageFunc());
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000049B5 File Offset: 0x00002BB5
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void WarnException([Localizable(false)] string message, Exception exception)
		{
			this.Warn(message, exception);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000049BF File Offset: 0x00002BBF
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, args);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000049D7 File Offset: 0x00002BD7
		public void Warn([Localizable(false)] string message)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, null, message);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000049EE File Offset: 0x00002BEE
		[MessageTemplateFormatMethod("message")]
		public void Warn([Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, args);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00004A05 File Offset: 0x00002C05
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Warn([Localizable(false)] string message, Exception exception)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, message, null);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00004A1D File Offset: 0x00002C1D
		public void Warn(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, message, null);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00004A35 File Offset: 0x00002C35
		[MessageTemplateFormatMethod("message")]
		public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, message, args);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00004A4D File Offset: 0x00002C4D
		[MessageTemplateFormatMethod("message")]
		public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, exception, formatProvider, message, args);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00004A67 File Offset: 0x00002C67
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00004A90 File Offset: 0x00002C90
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsWarnEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Warn(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00004AE5 File Offset: 0x00002CE5
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00004B15 File Offset: 0x00002D15
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00004B43 File Offset: 0x00002D43
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00004B7D File Offset: 0x00002D7D
		[MessageTemplateFormatMethod("message")]
		public void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public void Error<T>(T value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Error, null, value);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00004BCC File Offset: 0x00002DCC
		public void Error<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Error, formatProvider, value);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00004BE3 File Offset: 0x00002DE3
		public void Error(LogMessageGenerator messageFunc)
		{
			if (this.IsErrorEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Error, null, messageFunc());
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00004C0D File Offset: 0x00002E0D
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void ErrorException([Localizable(false)] string message, Exception exception)
		{
			this.Error(message, exception);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00004C17 File Offset: 0x00002E17
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, args);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00004C2F File Offset: 0x00002E2F
		public void Error([Localizable(false)] string message)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, null, message);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004C46 File Offset: 0x00002E46
		[MessageTemplateFormatMethod("message")]
		public void Error([Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, args);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00004C5D File Offset: 0x00002E5D
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Error([Localizable(false)] string message, Exception exception)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, message, null);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00004C75 File Offset: 0x00002E75
		public void Error(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, message, null);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00004C8D File Offset: 0x00002E8D
		[MessageTemplateFormatMethod("message")]
		public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, message, args);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00004CA5 File Offset: 0x00002EA5
		[MessageTemplateFormatMethod("message")]
		public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00004CBF File Offset: 0x00002EBF
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00004CE8 File Offset: 0x00002EE8
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsErrorEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Error(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00004D3D File Offset: 0x00002F3D
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00004D6D File Offset: 0x00002F6D
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00004D9B File Offset: 0x00002F9B
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00004DD5 File Offset: 0x00002FD5
		[MessageTemplateFormatMethod("message")]
		public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00004E0D File Offset: 0x0000300D
		public void Fatal<T>(T value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Fatal, null, value);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00004E24 File Offset: 0x00003024
		public void Fatal<T>(IFormatProvider formatProvider, T value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<T>(LogLevel.Fatal, formatProvider, value);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00004E3B File Offset: 0x0000303B
		public void Fatal(LogMessageGenerator messageFunc)
		{
			if (this.IsFatalEnabled)
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(LogLevel.Fatal, null, messageFunc());
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00004E65 File Offset: 0x00003065
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void FatalException([Localizable(false)] string message, Exception exception)
		{
			this.Fatal(message, exception);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00004E6F File Offset: 0x0000306F
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, args);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00004E87 File Offset: 0x00003087
		public void Fatal([Localizable(false)] string message)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, null, message);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00004E9E File Offset: 0x0000309E
		[MessageTemplateFormatMethod("message")]
		public void Fatal([Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, args);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00004EB5 File Offset: 0x000030B5
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		public void Fatal([Localizable(false)] string message, Exception exception)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, message, null);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00004ECD File Offset: 0x000030CD
		public void Fatal(Exception exception, [Localizable(false)] string message)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, message, null);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00004EE5 File Offset: 0x000030E5
		[MessageTemplateFormatMethod("message")]
		public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, message, args);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00004EFD File Offset: 0x000030FD
		[MessageTemplateFormatMethod("message")]
		public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00004F17 File Offset: 0x00003117
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00004F40 File Offset: 0x00003140
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
		{
			if (this.IsFatalEnabled)
			{
				if (this._configuration.ExceptionLoggingOldStyle)
				{
					Exception ex = argument as Exception;
					if (ex != null)
					{
						this.Fatal(message, ex);
						return;
					}
				}
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00004F95 File Offset: 0x00003195
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00004FC5 File Offset: 0x000031C5
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00004FF3 File Offset: 0x000031F3
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000502D File Offset: 0x0000322D
		[MessageTemplateFormatMethod("message")]
		public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00005065 File Offset: 0x00003265
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, object value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<object>(level, null, value);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00005079 File Offset: 0x00003279
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, IFormatProvider formatProvider, object value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<object>(level, formatProvider, value);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000508D File Offset: 0x0000328D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, string message, object arg1, object arg2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000050AF File Offset: 0x000032AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000050D6 File Offset: 0x000032D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000050FA File Offset: 0x000032FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, bool argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000511C File Offset: 0x0000331C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00005140 File Offset: 0x00003340
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, char argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00005162 File Offset: 0x00003362
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00005186 File Offset: 0x00003386
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, byte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000051A8 File Offset: 0x000033A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000051C7 File Offset: 0x000033C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, string argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000051E4 File Offset: 0x000033E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00005208 File Offset: 0x00003408
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, int argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000522A File Offset: 0x0000342A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000524E File Offset: 0x0000344E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, long argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00005270 File Offset: 0x00003470
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00005294 File Offset: 0x00003494
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, float argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000052B6 File Offset: 0x000034B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000052DA File Offset: 0x000034DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, double argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000052FC File Offset: 0x000034FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00005320 File Offset: 0x00003520
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, decimal argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00005342 File Offset: 0x00003542
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00005361 File Offset: 0x00003561
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Log(LogLevel level, string message, object argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000537E File Offset: 0x0000357E
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000053A2 File Offset: 0x000035A2
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, string message, sbyte argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000053C4 File Offset: 0x000035C4
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000053E8 File Offset: 0x000035E8
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, string message, uint argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000540A File Offset: 0x0000360A
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000542E File Offset: 0x0000362E
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, string message, ulong argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00005450 File Offset: 0x00003650
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(object value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Trace, null, value);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00005467 File Offset: 0x00003667
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(IFormatProvider formatProvider, object value)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Trace, formatProvider, value);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000547E File Offset: 0x0000367E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(string message, object arg1, object arg2)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000054A2 File Offset: 0x000036A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000054CB File Offset: 0x000036CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000054F1 File Offset: 0x000036F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, bool argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00005516 File Offset: 0x00003716
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000553C File Offset: 0x0000373C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, char argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00005561 File Offset: 0x00003761
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00005587 File Offset: 0x00003787
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, byte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000055AC File Offset: 0x000037AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000055CD File Offset: 0x000037CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, string argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000055ED File Offset: 0x000037ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00005613 File Offset: 0x00003813
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, int argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005638 File Offset: 0x00003838
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000565E File Offset: 0x0000385E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, long argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00005683 File Offset: 0x00003883
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000056A9 File Offset: 0x000038A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, float argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000056CE File Offset: 0x000038CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000056F4 File Offset: 0x000038F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, double argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00005719 File Offset: 0x00003919
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000573F File Offset: 0x0000393F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, decimal argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00005764 File Offset: 0x00003964
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00005785 File Offset: 0x00003985
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Trace(string message, object argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000057A5 File Offset: 0x000039A5
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000057CB File Offset: 0x000039CB
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(string message, sbyte argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000057F0 File Offset: 0x000039F0
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00005816 File Offset: 0x00003A16
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(string message, uint argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000583B File Offset: 0x00003A3B
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00005861 File Offset: 0x00003A61
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Trace(string message, ulong argument)
		{
			if (this.IsTraceEnabled)
			{
				this.WriteToTargets(LogLevel.Trace, message, new object[] { argument });
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00005886 File Offset: 0x00003A86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(object value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Debug, null, value);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000589D File Offset: 0x00003A9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(IFormatProvider formatProvider, object value)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Debug, formatProvider, value);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000058B4 File Offset: 0x00003AB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(string message, object arg1, object arg2)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000058D8 File Offset: 0x00003AD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00005901 File Offset: 0x00003B01
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00005927 File Offset: 0x00003B27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, bool argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000594C File Offset: 0x00003B4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00005972 File Offset: 0x00003B72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, char argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00005997 File Offset: 0x00003B97
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000059BD File Offset: 0x00003BBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, byte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000059E2 File Offset: 0x00003BE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00005A03 File Offset: 0x00003C03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, string argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00005A23 File Offset: 0x00003C23
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00005A49 File Offset: 0x00003C49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, int argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00005A6E File Offset: 0x00003C6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00005A94 File Offset: 0x00003C94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, long argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00005AB9 File Offset: 0x00003CB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00005ADF File Offset: 0x00003CDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, float argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00005B04 File Offset: 0x00003D04
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00005B2A File Offset: 0x00003D2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, double argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00005B4F File Offset: 0x00003D4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00005B75 File Offset: 0x00003D75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, decimal argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00005B9A File Offset: 0x00003D9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00005BBB File Offset: 0x00003DBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Debug(string message, object argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00005BDB File Offset: 0x00003DDB
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00005C01 File Offset: 0x00003E01
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(string message, sbyte argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00005C26 File Offset: 0x00003E26
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00005C4C File Offset: 0x00003E4C
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(string message, uint argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00005C71 File Offset: 0x00003E71
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00005C97 File Offset: 0x00003E97
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Debug(string message, ulong argument)
		{
			if (this.IsDebugEnabled)
			{
				this.WriteToTargets(LogLevel.Debug, message, new object[] { argument });
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00005CBC File Offset: 0x00003EBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(object value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Info, null, value);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00005CD3 File Offset: 0x00003ED3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(IFormatProvider formatProvider, object value)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Info, formatProvider, value);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00005CEA File Offset: 0x00003EEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(string message, object arg1, object arg2)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00005D0E File Offset: 0x00003F0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00005D37 File Offset: 0x00003F37
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00005D5D File Offset: 0x00003F5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, bool argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00005D82 File Offset: 0x00003F82
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00005DA8 File Offset: 0x00003FA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, char argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00005DCD File Offset: 0x00003FCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00005DF3 File Offset: 0x00003FF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, byte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00005E18 File Offset: 0x00004018
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00005E39 File Offset: 0x00004039
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, string argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00005E59 File Offset: 0x00004059
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00005E7F File Offset: 0x0000407F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, int argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00005EA4 File Offset: 0x000040A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00005ECA File Offset: 0x000040CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, long argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00005EEF File Offset: 0x000040EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00005F15 File Offset: 0x00004115
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, float argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00005F3A File Offset: 0x0000413A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00005F60 File Offset: 0x00004160
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, double argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00005F85 File Offset: 0x00004185
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00005FAB File Offset: 0x000041AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, decimal argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00005FD0 File Offset: 0x000041D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00005FF1 File Offset: 0x000041F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Info(string message, object argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00006011 File Offset: 0x00004211
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00006037 File Offset: 0x00004237
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(string message, sbyte argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000605C File Offset: 0x0000425C
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00006082 File Offset: 0x00004282
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(string message, uint argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000060A7 File Offset: 0x000042A7
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000060CD File Offset: 0x000042CD
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Info(string message, ulong argument)
		{
			if (this.IsInfoEnabled)
			{
				this.WriteToTargets(LogLevel.Info, message, new object[] { argument });
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000060F2 File Offset: 0x000042F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(object value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Warn, null, value);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00006109 File Offset: 0x00004309
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(IFormatProvider formatProvider, object value)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Warn, formatProvider, value);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00006120 File Offset: 0x00004320
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(string message, object arg1, object arg2)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00006144 File Offset: 0x00004344
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000616D File Offset: 0x0000436D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00006193 File Offset: 0x00004393
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, bool argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000061B8 File Offset: 0x000043B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000061DE File Offset: 0x000043DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, char argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00006203 File Offset: 0x00004403
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00006229 File Offset: 0x00004429
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, byte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000624E File Offset: 0x0000444E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000626F File Offset: 0x0000446F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, string argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000628F File Offset: 0x0000448F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000062B5 File Offset: 0x000044B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, int argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000062DA File Offset: 0x000044DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00006300 File Offset: 0x00004500
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, long argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00006325 File Offset: 0x00004525
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000634B File Offset: 0x0000454B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, float argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00006370 File Offset: 0x00004570
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00006396 File Offset: 0x00004596
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, double argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000063BB File Offset: 0x000045BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000063E1 File Offset: 0x000045E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, decimal argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00006406 File Offset: 0x00004606
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00006427 File Offset: 0x00004627
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Warn(string message, object argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00006447 File Offset: 0x00004647
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000646D File Offset: 0x0000466D
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(string message, sbyte argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00006492 File Offset: 0x00004692
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000064B8 File Offset: 0x000046B8
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(string message, uint argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000064DD File Offset: 0x000046DD
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00006503 File Offset: 0x00004703
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Warn(string message, ulong argument)
		{
			if (this.IsWarnEnabled)
			{
				this.WriteToTargets(LogLevel.Warn, message, new object[] { argument });
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00006528 File Offset: 0x00004728
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(object value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Error, null, value);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000653F File Offset: 0x0000473F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(IFormatProvider formatProvider, object value)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Error, formatProvider, value);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00006556 File Offset: 0x00004756
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(string message, object arg1, object arg2)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000657A File Offset: 0x0000477A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000065A3 File Offset: 0x000047A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000065C9 File Offset: 0x000047C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, bool argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000065EE File Offset: 0x000047EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00006614 File Offset: 0x00004814
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, char argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00006639 File Offset: 0x00004839
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000665F File Offset: 0x0000485F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, byte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00006684 File Offset: 0x00004884
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000066A5 File Offset: 0x000048A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, string argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000066C5 File Offset: 0x000048C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000066EB File Offset: 0x000048EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, int argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00006710 File Offset: 0x00004910
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00006736 File Offset: 0x00004936
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, long argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000675B File Offset: 0x0000495B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00006781 File Offset: 0x00004981
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, float argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000067A6 File Offset: 0x000049A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000067CC File Offset: 0x000049CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, double argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000067F1 File Offset: 0x000049F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00006817 File Offset: 0x00004A17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, decimal argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000683C File Offset: 0x00004A3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000685D File Offset: 0x00004A5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Error(string message, object argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000687D File Offset: 0x00004A7D
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000068A3 File Offset: 0x00004AA3
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(string message, sbyte argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000068C8 File Offset: 0x00004AC8
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000068EE File Offset: 0x00004AEE
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(string message, uint argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00006913 File Offset: 0x00004B13
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00006939 File Offset: 0x00004B39
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Error(string message, ulong argument)
		{
			if (this.IsErrorEnabled)
			{
				this.WriteToTargets(LogLevel.Error, message, new object[] { argument });
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000695E File Offset: 0x00004B5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(object value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Fatal, null, value);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00006975 File Offset: 0x00004B75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(IFormatProvider formatProvider, object value)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets<object>(LogLevel.Fatal, formatProvider, value);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000698C File Offset: 0x00004B8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(string message, object arg1, object arg2)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { arg1, arg2 });
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000069B0 File Offset: 0x00004BB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(string message, object arg1, object arg2, object arg3)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { arg1, arg2, arg3 });
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000069D9 File Offset: 0x00004BD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, bool argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000069FF File Offset: 0x00004BFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, bool argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00006A24 File Offset: 0x00004C24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, char argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00006A4A File Offset: 0x00004C4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, char argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00006A6F File Offset: 0x00004C6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, byte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00006A95 File Offset: 0x00004C95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, byte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00006ABA File Offset: 0x00004CBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, string argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00006ADB File Offset: 0x00004CDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, string argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00006AFB File Offset: 0x00004CFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, int argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00006B21 File Offset: 0x00004D21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, int argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00006B46 File Offset: 0x00004D46
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, long argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00006B6C File Offset: 0x00004D6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, long argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00006B91 File Offset: 0x00004D91
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, float argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00006BB7 File Offset: 0x00004DB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, float argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00006BDC File Offset: 0x00004DDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, double argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00006C02 File Offset: 0x00004E02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, double argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00006C27 File Offset: 0x00004E27
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00006C4D File Offset: 0x00004E4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, decimal argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00006C72 File Offset: 0x00004E72
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, object argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006C93 File Offset: 0x00004E93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Fatal(string message, object argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00006CB3 File Offset: 0x00004EB3
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00006CD9 File Offset: 0x00004ED9
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(string message, sbyte argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00006CFE File Offset: 0x00004EFE
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, uint argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00006D24 File Offset: 0x00004F24
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(string message, uint argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00006D49 File Offset: 0x00004F49
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00006D6F File Offset: 0x00004F6F
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		public void Fatal(string message, ulong argument)
		{
			if (this.IsFatalEnabled)
			{
				this.WriteToTargets(LogLevel.Fatal, message, new object[] { argument });
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00006D94 File Offset: 0x00004F94
		protected internal Logger()
		{
			this._contextLogger = this;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000399 RID: 921 RVA: 0x00006DA4 File Offset: 0x00004FA4
		// (remove) Token: 0x0600039A RID: 922 RVA: 0x00006DDC File Offset: 0x00004FDC
		public event EventHandler<EventArgs> LoggerReconfigured;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00006E11 File Offset: 0x00005011
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00006E19 File Offset: 0x00005019
		public string Name { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00006E22 File Offset: 0x00005022
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00006E2A File Offset: 0x0000502A
		public LogFactory Factory { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00006E33 File Offset: 0x00005033
		public IDictionary<string, object> Properties
		{
			get
			{
				ThreadSafeDictionary<string, object> threadSafeDictionary;
				if ((threadSafeDictionary = this._contextProperties) == null)
				{
					threadSafeDictionary = Interlocked.CompareExchange<ThreadSafeDictionary<string, object>>(ref this._contextProperties, Logger.CreateContextPropertiesDictionary(null), null) ?? this._contextProperties;
				}
				return threadSafeDictionary;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00006E5B File Offset: 0x0000505B
		public bool IsEnabled(LogLevel level)
		{
			if (level == null)
			{
				throw new InvalidOperationException("Log level must be defined");
			}
			return this.GetTargetsForLevel(level) != null;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00006E7C File Offset: 0x0000507C
		public Logger WithProperty(string propertyKey, object propertyValue)
		{
			if (string.IsNullOrEmpty(propertyKey))
			{
				throw new ArgumentException("propertyKey");
			}
			Logger logger = this.Factory.CreateNewLogger(base.GetType()) ?? new Logger();
			logger.Initialize(this.Name, this._configuration, this.Factory);
			logger._contextProperties = Logger.CreateContextPropertiesDictionary(this._contextProperties);
			logger._contextProperties[propertyKey] = propertyValue;
			logger._contextLogger = this._contextLogger;
			return logger;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00006EF8 File Offset: 0x000050F8
		public void SetProperty(string propertyKey, object propertyValue)
		{
			if (string.IsNullOrEmpty(propertyKey))
			{
				throw new ArgumentException("propertyKey");
			}
			this.Properties[propertyKey] = propertyValue;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00006F1A File Offset: 0x0000511A
		private static ThreadSafeDictionary<string, object> CreateContextPropertiesDictionary(ThreadSafeDictionary<string, object> contextProperties)
		{
			contextProperties = ((contextProperties != null) ? new ThreadSafeDictionary<string, object>(contextProperties) : new ThreadSafeDictionary<string, object>());
			return contextProperties;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00006F30 File Offset: 0x00005130
		public void Log(LogEventInfo logEvent)
		{
			TargetWithFilterChain targetWithFilterChain = (this.IsEnabled(logEvent.Level) ? this.GetTargetsForLevel(logEvent.Level) : null);
			if (targetWithFilterChain != null)
			{
				if (logEvent.LoggerName == null)
				{
					logEvent.LoggerName = this.Name;
				}
				this.WriteToTargets(logEvent, targetWithFilterChain);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00006F7C File Offset: 0x0000517C
		public void Log(Type wrapperType, LogEventInfo logEvent)
		{
			TargetWithFilterChain targetWithFilterChain = (this.IsEnabled(logEvent.Level) ? this.GetTargetsForLevel(logEvent.Level) : null);
			if (targetWithFilterChain != null)
			{
				if (logEvent.LoggerName == null)
				{
					logEvent.LoggerName = this.Name;
				}
				this.WriteToTargets(wrapperType, logEvent, targetWithFilterChain);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00006FC7 File Offset: 0x000051C7
		public void Log<T>(LogLevel level, T value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<T>(level, null, value);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00006FDB File Offset: 0x000051DB
		public void Log<T>(LogLevel level, IFormatProvider formatProvider, T value)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets<T>(level, formatProvider, value);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00006FEF File Offset: 0x000051EF
		public void Log(LogLevel level, LogMessageGenerator messageFunc)
		{
			if (this.IsEnabled(level))
			{
				if (messageFunc == null)
				{
					throw new ArgumentNullException("messageFunc");
				}
				this.WriteToTargets(level, null, messageFunc());
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00007016 File Offset: 0x00005216
		[Obsolete("Use Log(LogLevel, String, Exception) method instead. Marked obsolete before v4.3.11")]
		public void LogException(LogLevel level, [Localizable(false)] string message, Exception exception)
		{
			this.Log(level, message, exception);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00007021 File Offset: 0x00005221
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, args);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00007037 File Offset: 0x00005237
		public void Log(LogLevel level, [Localizable(false)] string message)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, null, message);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000704B File Offset: 0x0000524B
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, args);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000705F File Offset: 0x0000525F
		[Obsolete("Use Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args) instead. Marked obsolete before v4.3.11")]
		public void Log(LogLevel level, [Localizable(false)] string message, Exception exception)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, exception, message, null);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00007074 File Offset: 0x00005274
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, exception, message, args);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000708A File Offset: 0x0000528A
		[MessageTemplateFormatMethod("message")]
		public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, exception, formatProvider, message, args);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000070A2 File Offset: 0x000052A2
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument });
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000070C6 File Offset: 0x000052C6
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument });
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000070E8 File Offset: 0x000052E8
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00007116 File Offset: 0x00005316
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument1, argument2 });
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00007142 File Offset: 0x00005342
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, formatProvider, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000717A File Offset: 0x0000537A
		[MessageTemplateFormatMethod("message")]
		public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
		{
			if (this.IsEnabled(level))
			{
				this.WriteToTargets(level, message, new object[] { argument1, argument2, argument3 });
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000071B0 File Offset: 0x000053B0
		private LogEventInfo PrepareLogEventInfo(LogEventInfo logEvent)
		{
			if (logEvent.FormatProvider == null)
			{
				logEvent.FormatProvider = this.Factory.DefaultCultureInfo;
			}
			if (this._contextProperties != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this._contextProperties)
				{
					if (!logEvent.Properties.ContainsKey(keyValuePair.Key))
					{
						logEvent.Properties[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			return logEvent;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000724C File Offset: 0x0000544C
		public void Swallow(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000727C File Offset: 0x0000547C
		public T Swallow<T>(Func<T> func)
		{
			return this.Swallow<T>(func, default(T));
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000729C File Offset: 0x0000549C
		public T Swallow<T>(Func<T> func, T fallback)
		{
			T t;
			try
			{
				t = func();
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
				t = fallback;
			}
			return t;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000072D0 File Offset: 0x000054D0
		public async void Swallow(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00007314 File Offset: 0x00005514
		public async Task SwallowAsync(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00007364 File Offset: 0x00005564
		public async Task SwallowAsync(Func<Task> asyncAction)
		{
			try
			{
				await asyncAction();
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000073B4 File Offset: 0x000055B4
		public async Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
		{
			return await this.SwallowAsync<TResult>(asyncFunc, default(TResult));
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00007404 File Offset: 0x00005604
		public async Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
		{
			TResult tresult;
			try
			{
				tresult = await asyncFunc();
			}
			catch (Exception ex)
			{
				this.Error<Exception>(ex);
				tresult = fallback;
			}
			return tresult;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00007459 File Offset: 0x00005659
		internal void Initialize(string name, LoggerConfiguration loggerConfiguration, LogFactory factory)
		{
			this.Name = name;
			this.Factory = factory;
			this.SetConfiguration(loggerConfiguration);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00007470 File Offset: 0x00005670
		private void WriteToTargets(LogLevel level, [Localizable(false)] string message, object[] args)
		{
			this.WriteToTargets(level, this.Factory.DefaultCultureInfo, message, args);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00007488 File Offset: 0x00005688
		private void WriteToTargets(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, object[] args)
		{
			TargetWithFilterChain targetsForLevel = this.GetTargetsForLevel(level);
			if (targetsForLevel != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, formatProvider, message, args);
				this.WriteToTargets(logEventInfo, targetsForLevel);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000074BC File Offset: 0x000056BC
		private void WriteToTargets(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message)
		{
			TargetWithFilterChain targetsForLevel = this.GetTargetsForLevel(level);
			if (targetsForLevel != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, formatProvider, message, null);
				this.WriteToTargets(logEventInfo, targetsForLevel);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000074EC File Offset: 0x000056EC
		private void WriteToTargets<T>(LogLevel level, IFormatProvider formatProvider, T value)
		{
			TargetWithFilterChain targetsForLevel = this.GetTargetsForLevel(level);
			if (targetsForLevel != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, formatProvider, value);
				this.WriteToTargets(logEventInfo, targetsForLevel);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00007520 File Offset: 0x00005720
		private void WriteToTargets(LogLevel level, Exception ex, [Localizable(false)] string message, object[] args)
		{
			TargetWithFilterChain targetsForLevel = this.GetTargetsForLevel(level);
			if (targetsForLevel != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, ex, this.Factory.DefaultCultureInfo, message, args);
				this.WriteToTargets(logEventInfo, targetsForLevel);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000755C File Offset: 0x0000575C
		private void WriteToTargets(LogLevel level, Exception ex, IFormatProvider formatProvider, [Localizable(false)] string message, object[] args)
		{
			TargetWithFilterChain targetsForLevel = this.GetTargetsForLevel(level);
			if (targetsForLevel != null)
			{
				LogEventInfo logEventInfo = LogEventInfo.Create(level, this.Name, ex, formatProvider, message, args);
				this.WriteToTargets(logEventInfo, targetsForLevel);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000758F File Offset: 0x0000578F
		private void WriteToTargets([NotNull] LogEventInfo logEvent, [NotNull] TargetWithFilterChain targetsForLevel)
		{
			LoggerImpl.Write(Logger.DefaultLoggerType, targetsForLevel, this.PrepareLogEventInfo(logEvent), this.Factory);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000075A9 File Offset: 0x000057A9
		private void WriteToTargets(Type wrapperType, [NotNull] LogEventInfo logEvent, [NotNull] TargetWithFilterChain targetsForLevel)
		{
			LoggerImpl.Write(wrapperType ?? Logger.DefaultLoggerType, targetsForLevel, this.PrepareLogEventInfo(logEvent), this.Factory);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000075C8 File Offset: 0x000057C8
		internal void SetConfiguration(LoggerConfiguration newConfiguration)
		{
			this._configuration = newConfiguration;
			this._isTraceEnabled = this.IsEnabled(LogLevel.Trace);
			this._isDebugEnabled = this.IsEnabled(LogLevel.Debug);
			this._isInfoEnabled = this.IsEnabled(LogLevel.Info);
			this._isWarnEnabled = this.IsEnabled(LogLevel.Warn);
			this._isErrorEnabled = this.IsEnabled(LogLevel.Error);
			this._isFatalEnabled = this.IsEnabled(LogLevel.Fatal);
			this.OnLoggerReconfigured(EventArgs.Empty);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00007659 File Offset: 0x00005859
		private TargetWithFilterChain GetTargetsForLevel(LogLevel level)
		{
			if (this._contextLogger == this)
			{
				return this._configuration.GetTargetsForLevel(level);
			}
			return this._contextLogger.GetTargetsForLevel(level);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000767D File Offset: 0x0000587D
		protected virtual void OnLoggerReconfigured(EventArgs e)
		{
			EventHandler<EventArgs> loggerReconfigured = this.LoggerReconfigured;
			if (loggerReconfigured == null)
			{
				return;
			}
			loggerReconfigured(this, e);
		}

		// Token: 0x04000026 RID: 38
		internal static readonly Type DefaultLoggerType = typeof(Logger);

		// Token: 0x04000027 RID: 39
		private Logger _contextLogger;

		// Token: 0x04000028 RID: 40
		private ThreadSafeDictionary<string, object> _contextProperties;

		// Token: 0x04000029 RID: 41
		private LoggerConfiguration _configuration;

		// Token: 0x0400002A RID: 42
		private volatile bool _isTraceEnabled;

		// Token: 0x0400002B RID: 43
		private volatile bool _isDebugEnabled;

		// Token: 0x0400002C RID: 44
		private volatile bool _isInfoEnabled;

		// Token: 0x0400002D RID: 45
		private volatile bool _isWarnEnabled;

		// Token: 0x0400002E RID: 46
		private volatile bool _isErrorEnabled;

		// Token: 0x0400002F RID: 47
		private volatile bool _isFatalEnabled;
	}
}
