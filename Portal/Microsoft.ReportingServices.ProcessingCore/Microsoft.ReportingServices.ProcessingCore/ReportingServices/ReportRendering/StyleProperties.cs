using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000024 RID: 36
	public sealed class StyleProperties : ICloneable
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000BD88 File Offset: 0x00009F88
		internal StyleProperties()
		{
			this.m_nameMap = new Hashtable();
			this.m_valueCollection = new ArrayList();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		internal StyleProperties(int capacity)
		{
			this.m_nameMap = new Hashtable(capacity);
			this.m_valueCollection = new ArrayList(capacity);
		}

		// Token: 0x17000364 RID: 868
		public object this[int index]
		{
			get
			{
				return this.m_valueCollection[index];
			}
		}

		// Token: 0x17000365 RID: 869
		public object this[string styleName]
		{
			get
			{
				object obj = this.m_nameMap[styleName];
				if (obj != null)
				{
					return this.m_valueCollection[(int)obj];
				}
				return null;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000BE04 File Offset: 0x0000A004
		public int Count
		{
			get
			{
				return this.m_valueCollection.Count;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000BE11 File Offset: 0x0000A011
		public ICollection Keys
		{
			get
			{
				return this.m_nameMap.Keys;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000BE1E File Offset: 0x0000A01E
		public bool ContainStyleProperty(string styleName)
		{
			return styleName != null && this.m_nameMap.Count != 0 && this.m_nameMap.ContainsKey(styleName);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000BE3E File Offset: 0x0000A03E
		internal void Add(string name, object value)
		{
			if (!this.m_nameMap.Contains(name))
			{
				this.m_nameMap.Add(name, this.m_valueCollection.Count);
				this.m_valueCollection.Add(value);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000BE78 File Offset: 0x0000A078
		internal void Set(string name, object value)
		{
			object obj = this.m_nameMap[name];
			if (obj != null)
			{
				this.m_valueCollection[(int)obj] = value;
				return;
			}
			this.m_nameMap.Add(name, this.m_valueCollection.Count);
			this.m_valueCollection.Add(value);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		internal DataValueInstanceList ExtractRenderStyles()
		{
			DataValueInstanceList dataValueInstanceList = null;
			if (this.m_nameMap != null)
			{
				Global.Tracer.Assert(this.m_valueCollection != null && this.m_nameMap.Count == this.m_valueCollection.Count);
				dataValueInstanceList = new DataValueInstanceList(this.m_nameMap.Count);
				IDictionaryEnumerator enumerator = this.m_nameMap.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataValueInstance dataValueInstance = new DataValueInstance();
					dataValueInstance.Name = enumerator.Key as string;
					object obj = this.m_valueCollection[(int)enumerator.Value];
					if (obj == null)
					{
						dataValueInstance.Value = null;
					}
					else if (obj is string)
					{
						dataValueInstance.Value = string.Copy(obj as string);
					}
					else if (obj is int)
					{
						dataValueInstance.Value = (int)obj;
					}
					else if (obj is ReportSize)
					{
						dataValueInstance.Value = ((ReportSize)obj).DeepClone();
					}
					dataValueInstanceList.Add(dataValueInstance);
				}
			}
			return dataValueInstanceList;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public object Clone()
		{
			StyleProperties styleProperties = (StyleProperties)base.MemberwiseClone();
			if (this.m_nameMap != null)
			{
				styleProperties.m_nameMap = new Hashtable(this.m_nameMap.Count);
				styleProperties.m_valueCollection = new ArrayList(this.m_valueCollection.Count);
				foreach (object obj in this.m_nameMap)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					styleProperties.m_nameMap.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
					object obj2 = this.m_valueCollection[(int)dictionaryEntry.Value];
					object obj3 = null;
					if (obj2 is string)
					{
						obj3 = string.Copy(obj2 as string);
					}
					else if (obj2 is int)
					{
						obj3 = (int)obj2;
					}
					else if (obj2 is ReportSize)
					{
						obj3 = ((ReportSize)obj2).DeepClone();
					}
					styleProperties.m_valueCollection[(int)dictionaryEntry.Value] = obj3;
				}
			}
			return styleProperties;
		}

		// Token: 0x040000BD RID: 189
		private Hashtable m_nameMap;

		// Token: 0x040000BE RID: 190
		private ArrayList m_valueCollection;
	}
}
