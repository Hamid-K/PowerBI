using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200013B RID: 315
	[Obsolete("The InstanceAnnotationCollection class is deprecated, use the InstanceAnnotations property on objects that support instance annotations instead.")]
	public sealed class InstanceAnnotationCollection : IEnumerable<KeyValuePair<string, ODataValue>>, IEnumerable
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0001AF3D File Offset: 0x0001913D
		public InstanceAnnotationCollection()
		{
			this.inner = new Dictionary<string, ODataValue>(StringComparer.Ordinal);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001AF55 File Offset: 0x00019155
		public int Count
		{
			get
			{
				return this.inner.Count;
			}
		}

		// Token: 0x1700020F RID: 527
		public ODataValue this[string key]
		{
			get
			{
				return this.inner[key];
			}
			set
			{
				this.inner[key] = value;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001AF7F File Offset: 0x0001917F
		public bool ContainsKey(string key)
		{
			return this.inner.ContainsKey(key);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001AF8D File Offset: 0x0001918D
		public IEnumerator<KeyValuePair<string, ODataValue>> GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001AF9F File Offset: 0x0001919F
		public void Clear()
		{
			this.inner.Clear();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001AFAC File Offset: 0x000191AC
		public void Add(string key, ODataValue value)
		{
			ODataInstanceAnnotation.ValidateName(key);
			ODataInstanceAnnotation.ValidateValue(value);
			this.inner.Add(key, value);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001AFC7 File Offset: 0x000191C7
		public bool Remove(string key)
		{
			return this.inner.Remove(key);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001AFD5 File Offset: 0x000191D5
		public bool TryGetValue(string key, out ODataValue value)
		{
			return this.inner.TryGetValue(key, ref value);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001AFE4 File Offset: 0x000191E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000332 RID: 818
		private readonly Dictionary<string, ODataValue> inner;
	}
}
