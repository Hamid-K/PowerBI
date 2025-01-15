using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000033 RID: 51
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000B591 File Offset: 0x00009791
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			Contract.Assert(payloadNames.Count == payloadValues.Count);
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B5BA File Offset: 0x000097BA
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B5C2 File Offset: 0x000097C2
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17000050 RID: 80
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == key)
						{
							return this.m_values[num];
						}
						num++;
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000B653 File Offset: 0x00009853
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000B65A File Offset: 0x0000985A
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000B661 File Offset: 0x00009861
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B668 File Offset: 0x00009868
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000B678 File Offset: 0x00009878
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == key)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000B6ED File Offset: 0x000098ED
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000B6F7 File Offset: 0x000098F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000B6FF File Offset: 0x000098FF
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B706 File Offset: 0x00009906
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B70D File Offset: 0x0000990D
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B714 File Offset: 0x00009914
		public bool TryGetValue(string key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			using (List<string>.Enumerator enumerator = this.m_names.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == key)
					{
						value = this.m_values[num];
						return true;
					}
					num++;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x040000D2 RID: 210
		private List<string> m_names;

		// Token: 0x040000D3 RID: 211
		private List<object> m_values;
	}
}
