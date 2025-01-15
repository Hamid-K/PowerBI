using System;
using System.Collections;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200001E RID: 30
	public sealed class ReadOnlyNameValueCollection : MarshalByRefObject
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000215E File Offset: 0x0000035E
		internal ReadOnlyNameValueCollection(NameValueCollection originalCollection)
		{
			if (originalCollection == null)
			{
				throw new ArgumentNullException("originalCollection");
			}
			this.m_originalCollection = originalCollection;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000217B File Offset: 0x0000037B
		public string[] AllKeys
		{
			get
			{
				return this.m_originalCollection.AllKeys;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002188 File Offset: 0x00000388
		public string[] AllValues
		{
			get
			{
				int count = this.m_originalCollection.Count;
				string[] array = new string[count];
				if (count > 0)
				{
					this.m_originalCollection.CopyTo(array, 0);
				}
				return array;
			}
		}

		// Token: 0x1700004D RID: 77
		public string this[int index]
		{
			get
			{
				return this.m_originalCollection[index];
			}
		}

		// Token: 0x1700004E RID: 78
		public string this[string name]
		{
			get
			{
				return this.m_originalCollection[name];
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000021D4 File Offset: 0x000003D4
		public int Count
		{
			get
			{
				return this.m_originalCollection.Count;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000021E1 File Offset: 0x000003E1
		public NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.m_originalCollection.Keys;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000021EE File Offset: 0x000003EE
		public void CopyTo(Array dest, int index)
		{
			this.m_originalCollection.CopyTo(dest, index);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000021FD File Offset: 0x000003FD
		public string Get(int index)
		{
			return this.m_originalCollection.Get(index);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000220B File Offset: 0x0000040B
		public string Get(string name)
		{
			return this.m_originalCollection.Get(name);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002219 File Offset: 0x00000419
		public string GetKey(int index)
		{
			return this.m_originalCollection.GetKey(index);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002227 File Offset: 0x00000427
		public string[] GetValues(int index)
		{
			return this.m_originalCollection.GetValues(index);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002235 File Offset: 0x00000435
		public string[] GetValues(string name)
		{
			return this.m_originalCollection.GetValues(name);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002243 File Offset: 0x00000443
		public bool HasKeys()
		{
			return this.m_originalCollection.HasKeys();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002250 File Offset: 0x00000450
		public IEnumerator GetEnumerator()
		{
			return this.m_originalCollection.GetEnumerator();
		}

		// Token: 0x04000002 RID: 2
		private NameValueCollection m_originalCollection;
	}
}
