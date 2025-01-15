using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000015 RID: 21
	public static class MappedDiagnosticsLogicalContext
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000082D4 File Offset: 0x000064D4
		private static IDictionary<string, object> GetLogicalThreadDictionary(bool clone = false, int initialCapacity = 0)
		{
			Dictionary<string, object> dictionary = MappedDiagnosticsLogicalContext.GetThreadLocal();
			if (dictionary == null)
			{
				if (!clone)
				{
					return MappedDiagnosticsLogicalContext.EmptyDefaultDictionary;
				}
				dictionary = new Dictionary<string, object>(initialCapacity);
				MappedDiagnosticsLogicalContext.SetThreadLocal(dictionary);
			}
			else if (clone)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>(dictionary.Count + initialCapacity);
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					dictionary2[keyValuePair.Key] = keyValuePair.Value;
				}
				MappedDiagnosticsLogicalContext.SetThreadLocal(dictionary2);
				return dictionary2;
			}
			return dictionary;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000836C File Offset: 0x0000656C
		public static string Get(string item)
		{
			return MappedDiagnosticsLogicalContext.Get(item, null);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00008375 File Offset: 0x00006575
		public static string Get(string item, IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(MappedDiagnosticsLogicalContext.GetObject(item), formatProvider);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00008384 File Offset: 0x00006584
		public static object GetObject(string item)
		{
			object obj;
			if (!MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(false, 0).TryGetValue(item, out obj))
			{
				return null;
			}
			ObjectHandleSerializer objectHandleSerializer;
			if ((objectHandleSerializer = obj as ObjectHandleSerializer) != null)
			{
				return objectHandleSerializer.Unwrap();
			}
			return obj;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000083B6 File Offset: 0x000065B6
		public static IDisposable SetScoped(string item, string value)
		{
			return MappedDiagnosticsLogicalContext.SetScoped<string>(item, value);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000083BF File Offset: 0x000065BF
		public static IDisposable SetScoped(string item, object value)
		{
			return MappedDiagnosticsLogicalContext.SetScoped<object>(item, value);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000083C8 File Offset: 0x000065C8
		public static IDisposable SetScoped<T>(string item, T value)
		{
			IDictionary<string, object> logicalThreadDictionary = MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, 1);
			bool flag = logicalThreadDictionary.Count == 0;
			MappedDiagnosticsLogicalContext.SetItemValue<T>(item, value, logicalThreadDictionary);
			return new MappedDiagnosticsLogicalContext.ItemRemover(item, flag);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000083F8 File Offset: 0x000065F8
		public static IDisposable SetScoped(IReadOnlyList<KeyValuePair<string, object>> items)
		{
			if (items != null && items.Count > 0)
			{
				IDictionary<string, object> logicalThreadDictionary = MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, items.Count);
				bool flag = logicalThreadDictionary.Count == 0;
				for (int i = 0; i < items.Count; i++)
				{
					MappedDiagnosticsLogicalContext.SetItemValue<object>(items[i].Key, items[i].Value, logicalThreadDictionary);
				}
				return new MappedDiagnosticsLogicalContext.ItemRemover(items, flag);
			}
			return null;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00008466 File Offset: 0x00006666
		public static void Set(string item, string value)
		{
			MappedDiagnosticsLogicalContext.Set<string>(item, value);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000846F File Offset: 0x0000666F
		public static void Set(string item, object value)
		{
			MappedDiagnosticsLogicalContext.Set<object>(item, value);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00008478 File Offset: 0x00006678
		public static void Set<T>(string item, T value)
		{
			IDictionary<string, object> logicalThreadDictionary = MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, 1);
			MappedDiagnosticsLogicalContext.SetItemValue<T>(item, value, logicalThreadDictionary);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00008498 File Offset: 0x00006698
		private static void SetItemValue<T>(string item, T value, IDictionary<string, object> logicalContext)
		{
			if (typeof(T).IsValueType || Convert.GetTypeCode(value) != TypeCode.Object)
			{
				logicalContext[item] = value;
				return;
			}
			logicalContext[item] = new ObjectHandleSerializer(value);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000084E4 File Offset: 0x000066E4
		public static ICollection<string> GetNames()
		{
			return MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(false, 0).Keys;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000084F2 File Offset: 0x000066F2
		public static bool Contains(string item)
		{
			return MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(false, 0).ContainsKey(item);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00008501 File Offset: 0x00006701
		public static void Remove(string item)
		{
			MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, 0).Remove(item);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00008511 File Offset: 0x00006711
		public static void Clear()
		{
			MappedDiagnosticsLogicalContext.Clear(true);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00008519 File Offset: 0x00006719
		public static void Clear(bool free)
		{
			if (free)
			{
				MappedDiagnosticsLogicalContext.SetThreadLocal(null);
				return;
			}
			MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, 0).Clear();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00008531 File Offset: 0x00006731
		private static void SetThreadLocal(Dictionary<string, object> newValue)
		{
			if (newValue == null)
			{
				CallContext.FreeNamedDataSlot("NLog.AsyncableMappedDiagnosticsContext");
				return;
			}
			CallContext.LogicalSetData("NLog.AsyncableMappedDiagnosticsContext", newValue);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000854C File Offset: 0x0000674C
		private static Dictionary<string, object> GetThreadLocal()
		{
			return CallContext.LogicalGetData("NLog.AsyncableMappedDiagnosticsContext") as Dictionary<string, object>;
		}

		// Token: 0x04000044 RID: 68
		private const string LogicalThreadDictionaryKey = "NLog.AsyncableMappedDiagnosticsContext";

		// Token: 0x04000045 RID: 69
		private static readonly IDictionary<string, object> EmptyDefaultDictionary = default(SortHelpers.ReadOnlySingleBucketDictionary<string, object>);

		// Token: 0x02000219 RID: 537
		private sealed class ItemRemover : IDisposable
		{
			// Token: 0x060014CC RID: 5324 RVA: 0x0003738A File Offset: 0x0003558A
			public ItemRemover(string item, bool wasEmpty)
			{
				this._item1 = item;
				this._wasEmpty = wasEmpty;
			}

			// Token: 0x060014CD RID: 5325 RVA: 0x000373A0 File Offset: 0x000355A0
			public ItemRemover(IReadOnlyList<KeyValuePair<string, object>> items, bool wasEmpty)
			{
				int count = items.Count;
				if (count > 2)
				{
					this._item1 = items[0].Key;
					this._item2 = items[1].Key;
					this._item3 = items[2].Key;
					for (int i = 3; i < count; i++)
					{
						this._itemArray = this._itemArray ?? new string[count - 3];
						this._itemArray[i - 3] = items[i].Key;
					}
				}
				else if (count > 1)
				{
					this._item1 = items[0].Key;
					this._item2 = items[1].Key;
				}
				else
				{
					this._item1 = items[0].Key;
				}
				this._wasEmpty = wasEmpty;
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x00037488 File Offset: 0x00035688
			public void Dispose()
			{
				if (Interlocked.Exchange(ref this._disposed, 1) == 0)
				{
					if (this._wasEmpty && this.RemoveScopeWillClearContext())
					{
						MappedDiagnosticsLogicalContext.Clear(true);
						return;
					}
					IDictionary<string, object> logicalThreadDictionary = MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(true, 0);
					logicalThreadDictionary.Remove(this._item1);
					if (this._item2 != null)
					{
						logicalThreadDictionary.Remove(this._item2);
						if (this._item3 != null)
						{
							logicalThreadDictionary.Remove(this._item3);
						}
						if (this._itemArray != null)
						{
							for (int i = 0; i < this._itemArray.Length; i++)
							{
								if (this._itemArray[i] != null)
								{
									logicalThreadDictionary.Remove(this._itemArray[i]);
								}
							}
						}
					}
				}
			}

			// Token: 0x060014CF RID: 5327 RVA: 0x00037530 File Offset: 0x00035730
			private bool RemoveScopeWillClearContext()
			{
				if (this._itemArray == null)
				{
					IDictionary<string, object> logicalThreadDictionary = MappedDiagnosticsLogicalContext.GetLogicalThreadDictionary(false, 0);
					if ((logicalThreadDictionary.Count == 1 && this._item2 == null && logicalThreadDictionary.ContainsKey(this._item1)) || (logicalThreadDictionary.Count == 2 && this._item2 != null && this._item3 == null && logicalThreadDictionary.ContainsKey(this._item1) && logicalThreadDictionary.ContainsKey(this._item2) && !this._item1.Equals(this._item2)) || (logicalThreadDictionary.Count == 3 && this._item3 != null && logicalThreadDictionary.ContainsKey(this._item1) && logicalThreadDictionary.ContainsKey(this._item2) && logicalThreadDictionary.ContainsKey(this._item3) && !this._item1.Equals(this._item2) && !this._item1.Equals(this._item3) && !this._item2.Equals(this._item3)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060014D0 RID: 5328 RVA: 0x00037631 File Offset: 0x00035831
			public override string ToString()
			{
				string item = this._item1;
				return ((item != null) ? item.ToString() : null) ?? base.ToString();
			}

			// Token: 0x040005CB RID: 1483
			private readonly string _item1;

			// Token: 0x040005CC RID: 1484
			private readonly string _item2;

			// Token: 0x040005CD RID: 1485
			private readonly string _item3;

			// Token: 0x040005CE RID: 1486
			private readonly string[] _itemArray;

			// Token: 0x040005CF RID: 1487
			private int _disposed;

			// Token: 0x040005D0 RID: 1488
			private readonly bool _wasEmpty;
		}
	}
}
