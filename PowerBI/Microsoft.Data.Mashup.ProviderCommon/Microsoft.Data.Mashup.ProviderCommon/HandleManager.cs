using System;
using System.Collections.Generic;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000C RID: 12
	internal sealed class HandleManager<T> where T : class, IDisposable
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002E44 File Offset: 0x00001044
		public void RegisterHandle(string handle, T value)
		{
			Dictionary<string, HandleManager<T>.HandleData> dictionary = this.handles;
			lock (dictionary)
			{
				HandleManager<T>.HandleData handleData;
				if (!this.handles.TryGetValue(handle, out handleData))
				{
					handleData = new HandleManager<T>.HandleData
					{
						Count = 0
					};
					this.handles[handle] = handleData;
				}
				handleData.Count++;
				handleData.AddValue(value);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002EC0 File Offset: 0x000010C0
		public bool UnregisterHandle(string handle, T value)
		{
			Dictionary<string, HandleManager<T>.HandleData> dictionary = this.handles;
			lock (dictionary)
			{
				HandleManager<T>.HandleData handleData;
				if (this.handles.TryGetValue(handle, out handleData) && handleData.Values != null && handleData.RemoveValue(value))
				{
					handleData.Count--;
					if (handleData.Count == 0)
					{
						this.handles.Remove(handle);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F48 File Offset: 0x00001148
		public bool TryGetValues(string handle, out List<T> values)
		{
			Dictionary<string, HandleManager<T>.HandleData> dictionary = this.handles;
			lock (dictionary)
			{
				HandleManager<T>.HandleData handleData;
				if (this.handles.TryGetValue(handle, out handleData) && handleData.Values != null && handleData.Values.Count > 0)
				{
					values = handleData.GetValues();
					return values.Count > 0;
				}
			}
			values = null;
			return false;
		}

		// Token: 0x0400001E RID: 30
		private readonly Dictionary<string, HandleManager<T>.HandleData> handles = new Dictionary<string, HandleManager<T>.HandleData>();

		// Token: 0x0200001F RID: 31
		private class HandleData
		{
			// Token: 0x060001A1 RID: 417 RVA: 0x00006844 File Offset: 0x00004A44
			public void AddValue(T value)
			{
				if (this.Values == null)
				{
					this.Values = new List<WeakReference>();
				}
				else
				{
					this.RemoveValue(default(T));
				}
				this.Values.Add(new WeakReference(value));
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x0000688C File Offset: 0x00004A8C
			public bool RemoveValue(T value)
			{
				bool flag = false;
				for (int i = this.Values.Count - 1; i >= 0; i--)
				{
					T t = this.Values[i].Target as T;
					if (t == null)
					{
						this.Values.RemoveAt(i);
					}
					else if (t == value)
					{
						this.Values.RemoveAt(i);
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00006904 File Offset: 0x00004B04
			public List<T> GetValues()
			{
				List<T> list = new List<T>(this.Values.Count);
				for (int i = this.Values.Count - 1; i >= 0; i--)
				{
					T t = this.Values[i].Target as T;
					if (t == null)
					{
						this.Values.RemoveAt(i);
					}
					else
					{
						list.Add(t);
					}
				}
				return list;
			}

			// Token: 0x040000A4 RID: 164
			public int Count;

			// Token: 0x040000A5 RID: 165
			public List<WeakReference> Values;
		}
	}
}
