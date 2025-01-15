using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200001A RID: 26
	public static class NestedDiagnosticsLogicalContext
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00008753 File Offset: 0x00006953
		public static IDisposable Push<T>(T value)
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.NestedContext<T>.CreateNestedContext(NestedDiagnosticsLogicalContext.GetThreadLocal(), value);
			NestedDiagnosticsLogicalContext.SetThreadLocal(nestedContext);
			return nestedContext;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00008766 File Offset: 0x00006966
		public static IDisposable PushObject(object value)
		{
			return NestedDiagnosticsLogicalContext.Push<object>(value);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000876E File Offset: 0x0000696E
		public static object Pop()
		{
			return NestedDiagnosticsLogicalContext.PopObject();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00008775 File Offset: 0x00006975
		public static string Pop(IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(NestedDiagnosticsLogicalContext.PopObject() ?? string.Empty, formatProvider);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000878C File Offset: 0x0000698C
		public static object PopObject()
		{
			NestedDiagnosticsLogicalContext.INestedContext threadLocal = NestedDiagnosticsLogicalContext.GetThreadLocal();
			if (threadLocal != null)
			{
				NestedDiagnosticsLogicalContext.SetThreadLocal(threadLocal.Parent);
			}
			if (threadLocal == null)
			{
				return null;
			}
			return threadLocal.Value;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000087B8 File Offset: 0x000069B8
		public static object PeekObject()
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.PeekContext(false);
			if (nestedContext == null)
			{
				return null;
			}
			return nestedContext.Value;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000087CC File Offset: 0x000069CC
		internal static DateTime PeekTopScopeBeginTime()
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.PeekContext(false);
			return new DateTime((nestedContext != null) ? nestedContext.CreatedTimeUtcTicks : DateTime.MinValue.Ticks, DateTimeKind.Utc);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00008800 File Offset: 0x00006A00
		internal static DateTime PeekBottomScopeBeginTime()
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.PeekContext(true);
			return new DateTime((nestedContext != null) ? nestedContext.CreatedTimeUtcTicks : DateTime.MinValue.Ticks, DateTimeKind.Utc);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00008834 File Offset: 0x00006A34
		private static NestedDiagnosticsLogicalContext.INestedContext PeekContext(bool bottomScope)
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.GetThreadLocal();
			if (nestedContext != null)
			{
				if (bottomScope)
				{
					while (nestedContext.Parent != null)
					{
						nestedContext = nestedContext.Parent;
					}
				}
				return nestedContext;
			}
			return null;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00008861 File Offset: 0x00006A61
		public static void Clear()
		{
			NestedDiagnosticsLogicalContext.SetThreadLocal(null);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008869 File Offset: 0x00006A69
		public static string[] GetAllMessages()
		{
			return NestedDiagnosticsLogicalContext.GetAllMessages(null);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00008874 File Offset: 0x00006A74
		public static string[] GetAllMessages(IFormatProvider formatProvider)
		{
			return (from o in NestedDiagnosticsLogicalContext.GetAllObjects()
				select FormatHelper.ConvertToString(o, formatProvider)).ToArray<string>();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000088AC File Offset: 0x00006AAC
		public static object[] GetAllObjects()
		{
			NestedDiagnosticsLogicalContext.INestedContext nestedContext = NestedDiagnosticsLogicalContext.GetThreadLocal();
			if (nestedContext == null)
			{
				return ArrayHelper.Empty<object>();
			}
			int num = 0;
			object[] array = new object[nestedContext.FrameLevel];
			while (nestedContext != null)
			{
				array[num++] = nestedContext.Value;
				nestedContext = nestedContext.Parent;
			}
			return array;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000088F0 File Offset: 0x00006AF0
		private static void SetThreadLocal(NestedDiagnosticsLogicalContext.INestedContext newValue)
		{
			if (newValue == null)
			{
				CallContext.FreeNamedDataSlot("NLog.AsyncableNestedDiagnosticsContext");
				return;
			}
			CallContext.LogicalSetData("NLog.AsyncableNestedDiagnosticsContext", newValue);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000890B File Offset: 0x00006B0B
		private static NestedDiagnosticsLogicalContext.INestedContext GetThreadLocal()
		{
			return CallContext.LogicalGetData("NLog.AsyncableNestedDiagnosticsContext") as NestedDiagnosticsLogicalContext.INestedContext;
		}

		// Token: 0x04000048 RID: 72
		private const string NestedDiagnosticsContextKey = "NLog.AsyncableNestedDiagnosticsContext";

		// Token: 0x0200021C RID: 540
		private interface INestedContext : IDisposable
		{
			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x060014D5 RID: 5333
			NestedDiagnosticsLogicalContext.INestedContext Parent { get; }

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x060014D6 RID: 5334
			int FrameLevel { get; }

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x060014D7 RID: 5335
			object Value { get; }

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x060014D8 RID: 5336
			long CreatedTimeUtcTicks { get; }
		}

		// Token: 0x0200021D RID: 541
		[Serializable]
		private class NestedContext<T> : NestedDiagnosticsLogicalContext.INestedContext, IDisposable
		{
			// Token: 0x170003DD RID: 989
			// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0003769E File Offset: 0x0003589E
			public NestedDiagnosticsLogicalContext.INestedContext Parent { get; }

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x060014DA RID: 5338 RVA: 0x000376A6 File Offset: 0x000358A6
			public T Value { get; }

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x060014DB RID: 5339 RVA: 0x000376AE File Offset: 0x000358AE
			public long CreatedTimeUtcTicks { get; }

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x060014DC RID: 5340 RVA: 0x000376B6 File Offset: 0x000358B6
			public int FrameLevel { get; }

			// Token: 0x060014DD RID: 5341 RVA: 0x000376BE File Offset: 0x000358BE
			public static NestedDiagnosticsLogicalContext.INestedContext CreateNestedContext(NestedDiagnosticsLogicalContext.INestedContext parent, T value)
			{
				if (typeof(T).IsValueType || Convert.GetTypeCode(value) != TypeCode.Object)
				{
					return new NestedDiagnosticsLogicalContext.NestedContext<T>(parent, value);
				}
				return new NestedDiagnosticsLogicalContext.NestedContext<ObjectHandleSerializer>(parent, new ObjectHandleSerializer(value));
			}

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x060014DE RID: 5342 RVA: 0x000376F8 File Offset: 0x000358F8
			object NestedDiagnosticsLogicalContext.INestedContext.Value
			{
				get
				{
					object obj = this.Value;
					ObjectHandleSerializer objectHandleSerializer;
					if ((objectHandleSerializer = obj as ObjectHandleSerializer) != null)
					{
						return objectHandleSerializer.Unwrap();
					}
					return obj;
				}
			}

			// Token: 0x060014DF RID: 5343 RVA: 0x00037724 File Offset: 0x00035924
			public NestedContext(NestedDiagnosticsLogicalContext.INestedContext parent, T value)
			{
				this.Parent = parent;
				this.Value = value;
				this.CreatedTimeUtcTicks = DateTime.UtcNow.Ticks;
				this.FrameLevel = ((parent != null) ? (parent.FrameLevel + 1) : 1);
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0003776C File Offset: 0x0003596C
			void IDisposable.Dispose()
			{
				if (Interlocked.Exchange(ref this._disposed, 1) != 1)
				{
					NestedDiagnosticsLogicalContext.PopObject();
				}
			}

			// Token: 0x060014E1 RID: 5345 RVA: 0x00037783 File Offset: 0x00035983
			public override string ToString()
			{
				T t = this.Value;
				return ((t != null) ? t.ToString() : null) ?? "null";
			}

			// Token: 0x040005D8 RID: 1496
			private int _disposed;
		}
	}
}
