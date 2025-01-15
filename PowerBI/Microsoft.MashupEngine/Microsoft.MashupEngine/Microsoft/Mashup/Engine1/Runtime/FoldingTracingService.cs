using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001307 RID: 4871
	internal class FoldingTracingService
	{
		// Token: 0x060080BA RID: 32954 RVA: 0x001B720C File Offset: 0x001B540C
		public FoldingTracingService(IEngineHost host, string foldingFailureEntryName)
		{
			if (string.IsNullOrEmpty(foldingFailureEntryName))
			{
				this.foldingFailureEntryName = "Query/FoldingWarning";
			}
			else
			{
				this.foldingFailureEntryName = foldingFailureEntryName;
			}
			this.scopeStack = new Stack<FoldingTracingService.Scope>();
			this.engineHost = host;
			ITracingService service = TracingService.GetService(this.engineHost);
			this.traceEnabled = host != null && service.Options.IsEnabled("SqlFoldingTracing") && service.WarningEnabled();
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x060080BB RID: 32955 RVA: 0x001B727D File Offset: 0x001B547D
		public bool TraceEnabled
		{
			get
			{
				return this.traceEnabled;
			}
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x060080BC RID: 32956 RVA: 0x001B7285 File Offset: 0x001B5485
		public IEngineHost Host
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x060080BD RID: 32957 RVA: 0x001B728D File Offset: 0x001B548D
		protected FoldingTracingService.Scope CurrentScope
		{
			get
			{
				return this.scopeStack.Peek();
			}
		}

		// Token: 0x060080BE RID: 32958 RVA: 0x001B729C File Offset: 0x001B549C
		public IDisposable NewScope(string name)
		{
			if (this.TraceEnabled)
			{
				FoldingTracingService.Scope scope = new FoldingTracingService.Scope(name, this);
				this.scopeStack.Push(scope);
				return scope;
			}
			return FoldingTracingService.DummyScope.Instance;
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x001B72CC File Offset: 0x001B54CC
		public void FlushTraces()
		{
			if (this.TraceEnabled)
			{
				foreach (FoldingTracingService.Scope scope in this.scopeStack.Reverse<FoldingTracingService.Scope>())
				{
					scope.FlushTraces(this.Host);
				}
			}
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x001B732C File Offset: 0x001B552C
		public ValueException NewValueException()
		{
			this.FlushTraces();
			return ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x001B7340 File Offset: 0x001B5540
		public NotSupportedException NewFoldingFailureException(ValueException innerException = null)
		{
			this.FlushTraces();
			return new FoldingFailureException(innerException);
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x001B734E File Offset: 0x001B554E
		public NotSupportedException NewFoldingFailureException<T>(T warning) where T : IFoldingWarning
		{
			this.Trace<T>(warning);
			return this.NewFoldingFailureException(null);
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x001B735E File Offset: 0x001B555E
		public NotSupportedException NewFoldingFailureException(string warning)
		{
			this.Trace(warning);
			return this.NewFoldingFailureException(null);
		}

		// Token: 0x060080C4 RID: 32964 RVA: 0x001B736E File Offset: 0x001B556E
		public void Trace<T>(T foldingWarning) where T : IFoldingWarning
		{
			if (this.TraceEnabled)
			{
				this.CurrentScope.Trace(foldingWarning);
			}
		}

		// Token: 0x060080C5 RID: 32965 RVA: 0x001B7389 File Offset: 0x001B5589
		public void Trace(string warning)
		{
			if (this.TraceEnabled)
			{
				this.CurrentScope.Trace(warning);
			}
		}

		// Token: 0x060080C6 RID: 32966 RVA: 0x001B739F File Offset: 0x001B559F
		public bool When<T>(bool condition, T foldingWarning) where T : IFoldingWarning
		{
			if (this.TraceEnabled && condition)
			{
				this.CurrentScope.Trace(foldingWarning);
			}
			return condition;
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x001B73BD File Offset: 0x001B55BD
		public bool When(bool condition, string foldingWarning)
		{
			if (this.TraceEnabled && condition)
			{
				this.CurrentScope.Trace(foldingWarning);
			}
			return condition;
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x001B73D6 File Offset: 0x001B55D6
		public bool WhenNot<T>(bool condition, T foldingWarning) where T : IFoldingWarning
		{
			return !this.When<T>(!condition, foldingWarning);
		}

		// Token: 0x060080C9 RID: 32969 RVA: 0x001B73E6 File Offset: 0x001B55E6
		public bool WhenNot(bool condition, string foldingWarning)
		{
			return !this.When(!condition, foldingWarning);
		}

		// Token: 0x060080CA RID: 32970 RVA: 0x001B73F6 File Offset: 0x001B55F6
		public void Pop()
		{
			this.scopeStack.Pop();
		}

		// Token: 0x0400461C RID: 17948
		private const string DefaultFoldingFailureEntryName = "Query/FoldingWarning";

		// Token: 0x0400461D RID: 17949
		private const string SqlFoldingTracing = "SqlFoldingTracing";

		// Token: 0x0400461E RID: 17950
		protected readonly Stack<FoldingTracingService.Scope> scopeStack;

		// Token: 0x0400461F RID: 17951
		private readonly bool traceEnabled;

		// Token: 0x04004620 RID: 17952
		private readonly string foldingFailureEntryName;

		// Token: 0x04004621 RID: 17953
		private readonly IEngineHost engineHost;

		// Token: 0x02001308 RID: 4872
		protected class Scope : IDisposable
		{
			// Token: 0x060080CB RID: 32971 RVA: 0x001B7404 File Offset: 0x001B5604
			public Scope(string name, FoldingTracingService service)
			{
				this.name = name;
				this.service = service;
				this.entries = new Queue<IFoldingWarning>();
			}

			// Token: 0x060080CC RID: 32972 RVA: 0x001B7425 File Offset: 0x001B5625
			public void Trace(IFoldingWarning foldingWarning)
			{
				this.entries.Enqueue(foldingWarning);
			}

			// Token: 0x060080CD RID: 32973 RVA: 0x001B7433 File Offset: 0x001B5633
			public void Trace(string foldingWarning)
			{
				this.entries.Enqueue(new FoldingTracingService.Scope.FoldingWarning(foldingWarning));
			}

			// Token: 0x060080CE RID: 32974 RVA: 0x001B7448 File Offset: 0x001B5648
			public void FlushTraces(IEngineHost host)
			{
				using (IHostTrace hostTrace = TracingService.CreatePerformanceTrace(host, this.service.foldingFailureEntryName, TraceEventType.Warning, null))
				{
					hostTrace.Add("Function Name", this.name, false);
					goto IL_0063;
				}
				IL_0032:
				using (IHostTrace hostTrace2 = TracingService.CreatePerformanceTrace(host, this.service.foldingFailureEntryName, TraceEventType.Warning, null))
				{
					this.entries.Dequeue().Trace(hostTrace2);
				}
				IL_0063:
				if (this.entries.Count <= 0)
				{
					return;
				}
				goto IL_0032;
			}

			// Token: 0x060080CF RID: 32975 RVA: 0x001B74E4 File Offset: 0x001B56E4
			public void Dispose()
			{
				this.entries.Clear();
				this.service.Pop();
			}

			// Token: 0x04004622 RID: 17954
			private readonly string name;

			// Token: 0x04004623 RID: 17955
			private readonly FoldingTracingService service;

			// Token: 0x04004624 RID: 17956
			private readonly Queue<IFoldingWarning> entries;

			// Token: 0x02001309 RID: 4873
			private class FoldingWarning : IFoldingWarning
			{
				// Token: 0x060080D0 RID: 32976 RVA: 0x001B74FC File Offset: 0x001B56FC
				public FoldingWarning(string errorMessage)
				{
					this.errorMessage = errorMessage;
				}

				// Token: 0x060080D1 RID: 32977 RVA: 0x001B750B File Offset: 0x001B570B
				public void Trace(IHostTrace trace)
				{
					trace.Add("ErrorMessage", this.errorMessage, true);
				}

				// Token: 0x04004625 RID: 17957
				private readonly string errorMessage;
			}
		}

		// Token: 0x0200130A RID: 4874
		private class DummyScope : IDisposable
		{
			// Token: 0x060080D2 RID: 32978 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04004626 RID: 17958
			public static readonly FoldingTracingService.DummyScope Instance = new FoldingTracingService.DummyScope();
		}
	}
}
