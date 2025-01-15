using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Internal
{
	// Token: 0x02000197 RID: 407
	internal static class EngineContext
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static bool Enabled
		{
			get
			{
				return Interlocked.CompareExchange(ref EngineContext.enabledCount, 0, 0) > 0;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000EE2D File Offset: 0x0000D02D
		public static void Enable()
		{
			Interlocked.Increment(ref EngineContext.enabledCount);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000EE3A File Offset: 0x0000D03A
		public static void Disable()
		{
			if (Interlocked.Decrement(ref EngineContext.enabledCount) < 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000EE4F File Offset: 0x0000D04F
		public static EngineContext.LeaveOnDisposeScope Enter()
		{
			if (EngineContext.Enabled)
			{
				EngineContext.engineContextLock.WaitOne();
				return new EngineContext.LeaveOnDisposeScope(EngineContext.engineContextLock);
			}
			return new EngineContext.LeaveOnDisposeScope(null);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0000EE74 File Offset: 0x0000D074
		public static EngineContext.EnterOnDisposeScope Leave()
		{
			if (EngineContext.Enabled)
			{
				EngineContext.engineContextLock.Release();
				return new EngineContext.EnterOnDisposeScope(EngineContext.engineContextLock);
			}
			return new EngineContext.EnterOnDisposeScope(null);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0000EE9C File Offset: 0x0000D09C
		public static T LeaveEngineContext<T>(this T item)
		{
			if (!EngineContext.Enabled || item == null)
			{
				return item;
			}
			ILeaveEngineContext<T> leaveEngineContext = item as ILeaveEngineContext<T>;
			if (leaveEngineContext != null)
			{
				return leaveEngineContext.Leave();
			}
			EngineContext.ObjectMarshaller objectMarshaller;
			if (!EngineContext.contextAwareItemFactories.TryGetValue(typeof(T), out objectMarshaller))
			{
				throw EngineContext.NewFailToMarshalObjectException(typeof(T));
			}
			return (T)((object)objectMarshaller.MarshalOutOfEngineContext(item));
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public static T EnterEngineContext<T>(this T item)
		{
			if (!EngineContext.Enabled || item == null)
			{
				return item;
			}
			IEnterEngineContext<T> enterEngineContext = item as IEnterEngineContext<T>;
			if (enterEngineContext != null)
			{
				return enterEngineContext.Enter();
			}
			EngineContext.ObjectMarshaller objectMarshaller;
			if (!EngineContext.contextAwareItemFactories.TryGetValue(typeof(T), out objectMarshaller))
			{
				throw EngineContext.NewFailToMarshalObjectException(typeof(T));
			}
			return (T)((object)objectMarshaller.MarshalIntoEngineContext(item));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public static T Marshal<C, U, T>(this C context, T item) where C : struct, IContext<U> where U : struct, IDisposable
		{
			if (!EngineContext.Enabled)
			{
				return item;
			}
			if (typeof(U) == typeof(EngineContext.LeaveOnDisposeScope))
			{
				return item.LeaveEngineContext<T>();
			}
			if (typeof(U) == typeof(EngineContext.EnterOnDisposeScope))
			{
				return item.EnterEngineContext<T>();
			}
			throw EngineContext.NewFailToMarshalObjectException(typeof(T));
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public static T ReverseMarshal<C, U, T>(this C context, T item) where C : struct, IContext<U> where U : struct, IDisposable
		{
			if (!EngineContext.Enabled)
			{
				return item;
			}
			if (typeof(U) == typeof(EngineContext.LeaveOnDisposeScope))
			{
				return item.EnterEngineContext<T>();
			}
			if (typeof(U) == typeof(EngineContext.EnterOnDisposeScope))
			{
				return item.LeaveEngineContext<T>();
			}
			throw EngineContext.NewFailToMarshalObjectException(typeof(T));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0000F051 File Offset: 0x0000D251
		private static Exception NewFailToMarshalObjectException(Type type)
		{
			return new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Don't know how to transfer objects of type {0} into or outside of the engine context", type));
		}

		// Token: 0x040004A3 RID: 1187
		private static readonly Dictionary<Type, EngineContext.ObjectMarshaller> contextAwareItemFactories = new Dictionary<Type, EngineContext.ObjectMarshaller>
		{
			{
				typeof(Stream),
				new EngineContext.ContextFreeStreamFactory()
			},
			{
				typeof(TextReader),
				new EngineContext.ContextFreeTextReaderFactory()
			},
			{
				typeof(DbDataReader),
				new EngineContext.ContextFreeDbDataReaderFactory()
			},
			{
				typeof(IPageReader),
				new EngineContext.ContextFreePageReaderFactory()
			},
			{
				typeof(IPage),
				new EngineContext.ContextFreePageFactory()
			},
			{
				typeof(IColumn),
				new EngineContext.ContextFreeColumnFactory()
			},
			{
				typeof(IProgress),
				new EngineContext.ContextFreeProgressFactory()
			},
			{
				typeof(Func<object[], object>),
				new EngineContext.ContextFreeDelegateFactory()
			}
		};

		// Token: 0x040004A4 RID: 1188
		private static readonly Semaphore engineContextLock = new Semaphore(0, 1);

		// Token: 0x040004A5 RID: 1189
		private static int enabledCount;

		// Token: 0x02000198 RID: 408
		public struct LeaveOnDisposeScope : IDisposable
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x0000F133 File Offset: 0x0000D333
			public LeaveOnDisposeScope(Semaphore engineContextLock)
			{
				this.engineContextLock = engineContextLock;
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x0000F13C File Offset: 0x0000D33C
			public void Dispose()
			{
				if (this.engineContextLock != null)
				{
					this.engineContextLock.Release();
					this.engineContextLock = null;
				}
			}

			// Token: 0x040004A6 RID: 1190
			private Semaphore engineContextLock;
		}

		// Token: 0x02000199 RID: 409
		public struct EnterOnDisposeScope : IDisposable
		{
			// Token: 0x060007EE RID: 2030 RVA: 0x0000F159 File Offset: 0x0000D359
			public EnterOnDisposeScope(Semaphore engineContextLock)
			{
				this.engineContextLock = engineContextLock;
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0000F162 File Offset: 0x0000D362
			public void Dispose()
			{
				if (this.engineContextLock != null)
				{
					this.engineContextLock.WaitOne();
					this.engineContextLock = null;
				}
			}

			// Token: 0x040004A7 RID: 1191
			private Semaphore engineContextLock;
		}

		// Token: 0x0200019A RID: 410
		public struct EnterOnUseContext : IContext<EngineContext.LeaveOnDisposeScope>
		{
			// Token: 0x060007F0 RID: 2032 RVA: 0x0000F17F File Offset: 0x0000D37F
			public EngineContext.LeaveOnDisposeScope Enter()
			{
				return EngineContext.Enter();
			}
		}

		// Token: 0x0200019B RID: 411
		public struct LeaveOnUseContext : IContext<EngineContext.EnterOnDisposeScope>
		{
			// Token: 0x060007F1 RID: 2033 RVA: 0x0000F186 File Offset: 0x0000D386
			public EngineContext.EnterOnDisposeScope Enter()
			{
				return EngineContext.Leave();
			}
		}

		// Token: 0x0200019C RID: 412
		private abstract class ObjectMarshaller
		{
			// Token: 0x060007F2 RID: 2034
			public abstract object MarshalOutOfEngineContext(object obj);

			// Token: 0x060007F3 RID: 2035
			public abstract object MarshalIntoEngineContext(object obj);
		}

		// Token: 0x0200019D RID: 413
		private class ContextFreeStreamFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x060007F5 RID: 2037 RVA: 0x0000F190 File Offset: 0x0000D390
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new ContextAwareStream<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (Stream)obj);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
			public override object MarshalIntoEngineContext(object obj)
			{
				return new ContextAwareStream<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (Stream)obj);
			}
		}

		// Token: 0x0200019E RID: 414
		private class ContextFreeTextReaderFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x060007F8 RID: 2040 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new ContextAwareTextReader<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (TextReader)obj);
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0000F204 File Offset: 0x0000D404
			public override object MarshalIntoEngineContext(object obj)
			{
				return new ContextAwareTextReader<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (TextReader)obj);
			}
		}

		// Token: 0x0200019F RID: 415
		private class ContextFreeDbDataReaderFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x060007FB RID: 2043 RVA: 0x0000F225 File Offset: 0x0000D425
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new EngineContext.ExportedDbDataReader(((DbDataReader)obj).WithTableSchema());
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0000F237 File Offset: 0x0000D437
			public override object MarshalIntoEngineContext(object obj)
			{
				return new EngineContext.ImportedDbDataReader(((DbDataReader)obj).WithTableSchema());
			}
		}

		// Token: 0x020001A0 RID: 416
		private class ContextFreePageReaderFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x060007FE RID: 2046 RVA: 0x0000F24C File Offset: 0x0000D44C
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new ContextAwarePageReader<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (IPageReader)obj);
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x0000F270 File Offset: 0x0000D470
			public override object MarshalIntoEngineContext(object obj)
			{
				return new ContextAwarePageReader<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (IPageReader)obj);
			}
		}

		// Token: 0x020001A1 RID: 417
		private class ContextFreePageFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x06000801 RID: 2049 RVA: 0x0000F294 File Offset: 0x0000D494
			public override object MarshalOutOfEngineContext(object obj)
			{
				IConcurrentPage concurrentPage = obj as IConcurrentPage;
				if (concurrentPage != null && concurrentPage.IsBufferedForRead)
				{
					return obj;
				}
				ContextAwarePage<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope> contextAwarePage = obj as ContextAwarePage<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>;
				if (contextAwarePage != null)
				{
					return contextAwarePage.Page;
				}
				return new ContextAwarePage<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (IPage)obj);
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x0000F2DC File Offset: 0x0000D4DC
			public override object MarshalIntoEngineContext(object obj)
			{
				IConcurrentPage concurrentPage = obj as IConcurrentPage;
				if (concurrentPage != null && concurrentPage.IsBufferedForRead)
				{
					return obj;
				}
				ContextAwarePage<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope> contextAwarePage = obj as ContextAwarePage<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>;
				if (contextAwarePage != null)
				{
					return contextAwarePage.Page;
				}
				return new ContextAwarePage<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (IPage)obj);
			}
		}

		// Token: 0x020001A2 RID: 418
		private class ContextFreeColumnFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x06000804 RID: 2052 RVA: 0x0000F324 File Offset: 0x0000D524
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new ContextAwareColumn<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (IColumn)obj);
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x0000F348 File Offset: 0x0000D548
			public override object MarshalIntoEngineContext(object obj)
			{
				return new ContextAwareColumn<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (IColumn)obj);
			}
		}

		// Token: 0x020001A3 RID: 419
		private class ContextFreeProgressFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x06000807 RID: 2055 RVA: 0x0000F36C File Offset: 0x0000D56C
			public override object MarshalOutOfEngineContext(object obj)
			{
				return new ContextAwareProgress<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>(default(EngineContext.EnterOnUseContext), (IProgress)obj);
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x0000F390 File Offset: 0x0000D590
			public override object MarshalIntoEngineContext(object obj)
			{
				return new ContextAwareProgress<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>(default(EngineContext.LeaveOnUseContext), (IProgress)obj);
			}
		}

		// Token: 0x020001A4 RID: 420
		private class ContextFreeDelegateFactory : EngineContext.ObjectMarshaller
		{
			// Token: 0x0600080A RID: 2058 RVA: 0x0000F3B1 File Offset: 0x0000D5B1
			public override object MarshalOutOfEngineContext(object obj)
			{
				Func<object[], object> item = (Func<object[], object>)obj;
				return new Func<object[], object>((object[] args) => this.RunInEngineContext(item, args));
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x0000F3D6 File Offset: 0x0000D5D6
			public override object MarshalIntoEngineContext(object obj)
			{
				throw new InvalidOperationException(Strings.UnsupportedClrType(obj.GetType()));
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
			private object RunInEngineContext(Func<object[], object> function, object[] args)
			{
				object obj;
				using (EngineContext.Enter())
				{
					obj = function(args);
				}
				return obj;
			}
		}

		// Token: 0x020001A6 RID: 422
		private abstract class ContextAwareDbDataReader<T, U> : Microsoft.Internal.ContextAwareDbDataReader<T, U> where T : struct, IContext<U> where U : struct, IDisposable
		{
			// Token: 0x06000810 RID: 2064 RVA: 0x0000F440 File Offset: 0x0000D640
			public ContextAwareDbDataReader(T context, DbDataReaderWithTableSchema reader)
				: base(context, reader)
			{
				this.ownedObjects = new HashSet<IDisposable>(ObjectComparer<IDisposable>.Instance);
			}

			// Token: 0x06000811 RID: 2065
			protected abstract TValue Marshal<TValue>(TValue obj);

			// Token: 0x06000812 RID: 2066 RVA: 0x0000F45C File Offset: 0x0000D65C
			protected sealed override object MarshalIntoContext(object obj)
			{
				if (!EngineContext.Enabled || obj == null)
				{
					return obj;
				}
				Action action = null;
				if (obj is IDisposable)
				{
					action = delegate
					{
						this.RemoveOwnership((IDisposable)obj);
					};
					HashSet<IDisposable> hashSet = this.ownedObjects;
					lock (hashSet)
					{
						this.ownedObjects.Add((IDisposable)obj);
					}
				}
				if (obj is Stream)
				{
					return this.Marshal<Stream>((Stream)obj).OnDispose(action);
				}
				if (obj is DbDataReader)
				{
					return this.Marshal<DbDataReader>((DbDataReader)obj).WithTableSchema().OnDispose(action);
				}
				if (obj is TextReader)
				{
					return this.Marshal<TextReader>((TextReader)obj).OnDispose(action);
				}
				throw EngineContext.NewFailToMarshalObjectException(obj.GetType());
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x0000F578 File Offset: 0x0000D778
			protected override void Dispose(bool disposing)
			{
				HashSet<IDisposable> hashSet = this.ownedObjects;
				IDisposable[] array;
				lock (hashSet)
				{
					array = this.ownedObjects.ToArray<IDisposable>();
					this.ownedObjects.Clear();
				}
				T context = base.Context;
				U u = context.Enter();
				try
				{
					IDisposable[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].Dispose();
					}
				}
				finally
				{
					u.Dispose();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0000F624 File Offset: 0x0000D824
			private void RemoveOwnership(IDisposable obj)
			{
				HashSet<IDisposable> hashSet = this.ownedObjects;
				lock (hashSet)
				{
					this.ownedObjects.Remove(obj);
				}
			}

			// Token: 0x040004AA RID: 1194
			private readonly HashSet<IDisposable> ownedObjects;
		}

		// Token: 0x020001A8 RID: 424
		private sealed class ExportedDbDataReader : EngineContext.ContextAwareDbDataReader<EngineContext.EnterOnUseContext, EngineContext.LeaveOnDisposeScope>, ILeaveEngineContext<DbDataReader>
		{
			// Token: 0x06000817 RID: 2071 RVA: 0x0000F684 File Offset: 0x0000D884
			public ExportedDbDataReader(DbDataReaderWithTableSchema reader)
				: base(default(EngineContext.EnterOnUseContext), reader)
			{
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public DbDataReader Leave()
			{
				return this;
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
			protected override TValue Marshal<TValue>(TValue obj)
			{
				return obj.LeaveEngineContext<TValue>();
			}
		}

		// Token: 0x020001A9 RID: 425
		private sealed class ImportedDbDataReader : EngineContext.ContextAwareDbDataReader<EngineContext.LeaveOnUseContext, EngineContext.EnterOnDisposeScope>, IEnterEngineContext<DbDataReader>
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x0000F6AC File Offset: 0x0000D8AC
			public ImportedDbDataReader(DbDataReaderWithTableSchema reader)
				: base(default(EngineContext.LeaveOnUseContext), reader)
			{
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public DbDataReader Enter()
			{
				return this;
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0000F6C9 File Offset: 0x0000D8C9
			protected override TValue Marshal<TValue>(TValue obj)
			{
				return obj.EnterEngineContext<TValue>();
			}
		}
	}
}
