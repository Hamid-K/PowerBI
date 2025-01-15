using System;
using System.Collections.Generic;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000014 RID: 20
	public static class MappedDiagnosticsContext
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x000081D4 File Offset: 0x000063D4
		private static IDictionary<string, object> GetThreadDictionary(bool create = true)
		{
			Dictionary<string, object> dataForSlot = ThreadLocalStorageHelper.GetDataForSlot<Dictionary<string, object>>(MappedDiagnosticsContext.DataSlot, create);
			if (dataForSlot == null && !create)
			{
				return MappedDiagnosticsContext.EmptyDefaultDictionary;
			}
			return dataForSlot;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000081FA File Offset: 0x000063FA
		public static IDisposable SetScoped(string item, string value)
		{
			MappedDiagnosticsContext.Set(item, value);
			return new MappedDiagnosticsContext.ItemRemover(item);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00008209 File Offset: 0x00006409
		public static IDisposable SetScoped(string item, object value)
		{
			MappedDiagnosticsContext.Set(item, value);
			return new MappedDiagnosticsContext.ItemRemover(item);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00008218 File Offset: 0x00006418
		public static void Set(string item, string value)
		{
			MappedDiagnosticsContext.GetThreadDictionary(true)[item] = value;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00008227 File Offset: 0x00006427
		public static void Set(string item, object value)
		{
			MappedDiagnosticsContext.GetThreadDictionary(true)[item] = value;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00008236 File Offset: 0x00006436
		public static string Get(string item)
		{
			return MappedDiagnosticsContext.Get(item, null);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000823F File Offset: 0x0000643F
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(MappedDiagnosticsContext.GetObject(item), formatProvider);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00008250 File Offset: 0x00006450
		public static object GetObject(string item)
		{
			object obj;
			if (!MappedDiagnosticsContext.GetThreadDictionary(false).TryGetValue(item, out obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00008270 File Offset: 0x00006470
		public static ICollection<string> GetNames()
		{
			return MappedDiagnosticsContext.GetThreadDictionary(false).Keys;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000827D File Offset: 0x0000647D
		public static bool Contains(string item)
		{
			return MappedDiagnosticsContext.GetThreadDictionary(false).ContainsKey(item);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000828B File Offset: 0x0000648B
		public static void Remove(string item)
		{
			MappedDiagnosticsContext.GetThreadDictionary(true).Remove(item);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000829A File Offset: 0x0000649A
		public static void Clear()
		{
			MappedDiagnosticsContext.GetThreadDictionary(true).Clear();
		}

		// Token: 0x04000042 RID: 66
		private static readonly object DataSlot = ThreadLocalStorageHelper.AllocateDataSlot();

		// Token: 0x04000043 RID: 67
		private static readonly IDictionary<string, object> EmptyDefaultDictionary = default(SortHelpers.ReadOnlySingleBucketDictionary<string, object>);

		// Token: 0x02000218 RID: 536
		private sealed class ItemRemover : IDisposable
		{
			// Token: 0x060014CA RID: 5322 RVA: 0x0003735F File Offset: 0x0003555F
			public ItemRemover(string item)
			{
				this._item = item;
			}

			// Token: 0x060014CB RID: 5323 RVA: 0x0003736E File Offset: 0x0003556E
			public void Dispose()
			{
				if (!this._disposed)
				{
					this._disposed = true;
					MappedDiagnosticsContext.Remove(this._item);
				}
			}

			// Token: 0x040005C9 RID: 1481
			private readonly string _item;

			// Token: 0x040005CA RID: 1482
			private bool _disposed;
		}
	}
}
