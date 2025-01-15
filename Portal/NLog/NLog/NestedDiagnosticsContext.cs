using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000019 RID: 25
	public static class NestedDiagnosticsContext
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00008600 File Offset: 0x00006800
		public static string TopMessage
		{
			get
			{
				return FormatHelper.ConvertToString(NestedDiagnosticsContext.TopObject, null);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000860D File Offset: 0x0000680D
		public static object TopObject
		{
			get
			{
				return NestedDiagnosticsContext.PeekObject();
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00008614 File Offset: 0x00006814
		private static Stack<object> GetThreadStack(bool create = true)
		{
			return ThreadLocalStorageHelper.GetDataForSlot<Stack<object>>(NestedDiagnosticsContext.dataSlot, create);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00008621 File Offset: 0x00006821
		public static IDisposable Push(string text)
		{
			return NestedDiagnosticsContext.Push(text);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000862C File Offset: 0x0000682C
		public static IDisposable Push(object value)
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(true);
			int count = threadStack.Count;
			threadStack.Push(value);
			return new NestedDiagnosticsContext.StackPopper(threadStack, count);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00008653 File Offset: 0x00006853
		public static string Pop()
		{
			return NestedDiagnosticsContext.Pop(null);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000865B File Offset: 0x0000685B
		public static string Pop(IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(NestedDiagnosticsContext.PopObject() ?? string.Empty, formatProvider);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00008674 File Offset: 0x00006874
		public static object PopObject()
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(true);
			if (threadStack.Count <= 0)
			{
				return null;
			}
			return threadStack.Pop();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000869C File Offset: 0x0000689C
		public static object PeekObject()
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(false);
			if (threadStack == null || threadStack.Count <= 0)
			{
				return null;
			}
			return threadStack.Peek();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000086C4 File Offset: 0x000068C4
		public static void Clear()
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(false);
			if (threadStack == null)
			{
				return;
			}
			threadStack.Clear();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000086D6 File Offset: 0x000068D6
		public static string[] GetAllMessages()
		{
			return NestedDiagnosticsContext.GetAllMessages(null);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000086E0 File Offset: 0x000068E0
		public static string[] GetAllMessages(IFormatProvider formatProvider)
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(false);
			if (threadStack == null)
			{
				return ArrayHelper.Empty<string>();
			}
			return threadStack.Select((object o) => FormatHelper.ConvertToString(o, formatProvider)).ToArray<string>();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00008724 File Offset: 0x00006924
		public static object[] GetAllObjects()
		{
			Stack<object> threadStack = NestedDiagnosticsContext.GetThreadStack(false);
			if (threadStack == null)
			{
				return ArrayHelper.Empty<object>();
			}
			return threadStack.ToArray();
		}

		// Token: 0x04000047 RID: 71
		private static readonly object dataSlot = ThreadLocalStorageHelper.AllocateDataSlot();

		// Token: 0x0200021A RID: 538
		private class StackPopper : IDisposable
		{
			// Token: 0x060014D1 RID: 5329 RVA: 0x0003764F File Offset: 0x0003584F
			public StackPopper(Stack<object> stack, int previousCount)
			{
				this._stack = stack;
				this._previousCount = previousCount;
			}

			// Token: 0x060014D2 RID: 5330 RVA: 0x00037665 File Offset: 0x00035865
			void IDisposable.Dispose()
			{
				while (this._stack.Count > this._previousCount)
				{
					this._stack.Pop();
				}
			}

			// Token: 0x040005D1 RID: 1489
			private readonly Stack<object> _stack;

			// Token: 0x040005D2 RID: 1490
			private readonly int _previousCount;
		}
	}
}
