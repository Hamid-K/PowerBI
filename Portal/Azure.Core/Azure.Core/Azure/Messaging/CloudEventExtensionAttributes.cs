using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Azure.Core;

namespace Azure.Messaging
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	internal class CloudEventExtensionAttributes<TKey, [Nullable(2)] TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004C98 File Offset: 0x00002E98
		public CloudEventExtensionAttributes()
		{
			this._backingDictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x17000056 RID: 86
		public TValue this[TKey key]
		{
			get
			{
				return this._backingDictionary[key];
			}
			set
			{
				CloudEventExtensionAttributes<TKey, TValue>.ValidateAttribute(key as string, value);
				this._backingDictionary[key] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004CDE File Offset: 0x00002EDE
		public ICollection<TKey> Keys
		{
			get
			{
				return this._backingDictionary.Keys;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004CEB File Offset: 0x00002EEB
		public ICollection<TValue> Values
		{
			get
			{
				return this._backingDictionary.Values;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public int Count
		{
			get
			{
				return this._backingDictionary.Count;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004D05 File Offset: 0x00002F05
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<TKey, TValue>>)this._backingDictionary).IsReadOnly;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004D12 File Offset: 0x00002F12
		public void Add(TKey key, TValue value)
		{
			CloudEventExtensionAttributes<TKey, TValue>.ValidateAttribute(key as string, value);
			this._backingDictionary.Add(key, value);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004D37 File Offset: 0x00002F37
		public void AddWithoutValidation(TKey key, TValue value)
		{
			this._backingDictionary.Add(key, value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D46 File Offset: 0x00002F46
		public void Add([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			CloudEventExtensionAttributes<TKey, TValue>.ValidateAttribute(item.Key as string, item.Value);
			((ICollection<KeyValuePair<TKey, TValue>>)this._backingDictionary).Add(item);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004D76 File Offset: 0x00002F76
		public void Clear()
		{
			this._backingDictionary.Clear();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004D83 File Offset: 0x00002F83
		public bool Contains([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this._backingDictionary).Contains(item);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004D91 File Offset: 0x00002F91
		public bool ContainsKey(TKey key)
		{
			return this._backingDictionary.ContainsKey(key);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004D9F File Offset: 0x00002F9F
		public void CopyTo([Nullable(new byte[] { 1, 0, 1, 1 })] KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)this._backingDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004DAE File Offset: 0x00002FAE
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this._backingDictionary.GetEnumerator();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public bool Remove(TKey key)
		{
			return this._backingDictionary.Remove(key);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004DCE File Offset: 0x00002FCE
		public bool Remove([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)this._backingDictionary).Remove(item);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004DDC File Offset: 0x00002FDC
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._backingDictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004DEB File Offset: 0x00002FEB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._backingDictionary.GetEnumerator();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E00 File Offset: 0x00003000
		[NullableContext(2)]
		private static void ValidateAttribute(string name, object value)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			Argument.AssertNotNull<object>(value, "value");
			if (CloudEventExtensionAttributes<TKey, TValue>.s_reservedAttributes.Contains(name))
			{
				throw new ArgumentException("Attribute name cannot use the reserved attribute: '" + name + "'", "name");
			}
			foreach (char c in name)
			{
				if ((c < '0' || c > '9') && (c < 'a' || c > 'z'))
				{
					throw new ArgumentException(string.Format("Invalid character in extension attribute name: '{0}'. ", c) + "CloudEvent attribute names must consist of lower-case letters ('a' to 'z') or digits ('0' to '9') from the ASCII character set.", "name");
				}
			}
			if (value is string || value is byte[] || value is ReadOnlyMemory<byte> || value is int || value is bool || value is Uri || value is DateTime || value is DateTimeOffset)
			{
				return;
			}
			throw new ArgumentException(string.Format("Values of type {0} are not supported. ", value.GetType()) + "Attribute values must be of type string, bool, byte, int, Uri, DateTime, or DateTimeOffset.");
		}

		// Token: 0x0400007F RID: 127
		private readonly Dictionary<TKey, TValue> _backingDictionary;

		// Token: 0x04000080 RID: 128
		private static readonly HashSet<string> s_reservedAttributes = new HashSet<string> { "specversion", "id", "source", "type", "datacontenttype", "dataschema", "subject", "time", "data", "data_base64" };
	}
}
