using System;
using System.Collections;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000512 RID: 1298
	public sealed class StyleProperties
	{
		// Token: 0x0600451C RID: 17692 RVA: 0x00120680 File Offset: 0x0011E880
		internal StyleProperties()
		{
			this.m_nameMap = new Hashtable();
			this.m_valueCollection = new ArrayList();
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x0012069E File Offset: 0x0011E89E
		internal StyleProperties(int capacity)
		{
			this.m_nameMap = new Hashtable(capacity);
			this.m_valueCollection = new ArrayList(capacity);
		}

		// Token: 0x17001CFE RID: 7422
		public object this[int index]
		{
			get
			{
				return this.m_valueCollection[index];
			}
		}

		// Token: 0x17001CFF RID: 7423
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

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x001206FC File Offset: 0x0011E8FC
		public int Count
		{
			get
			{
				return this.m_valueCollection.Count;
			}
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x00120709 File Offset: 0x0011E909
		public ICollection Keys
		{
			get
			{
				return this.m_nameMap.Keys;
			}
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x00120716 File Offset: 0x0011E916
		public bool ContainStyleProperty(string styleName)
		{
			return styleName != null && this.m_nameMap.Count != 0 && this.m_nameMap.ContainsKey(styleName);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00120736 File Offset: 0x0011E936
		internal void Add(string name, object value)
		{
			if (!this.m_nameMap.Contains(name))
			{
				this.m_nameMap.Add(name, this.m_valueCollection.Count);
				this.m_valueCollection.Add(value);
			}
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x00120770 File Offset: 0x0011E970
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

		// Token: 0x06004525 RID: 17701 RVA: 0x001207CC File Offset: 0x0011E9CC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			StyleProperties styleProperties = (StyleProperties)base.MemberwiseClone();
			if (this.m_nameMap != null)
			{
				styleProperties.m_nameMap = new Hashtable(this.m_nameMap.Count);
				styleProperties.m_valueCollection = new ArrayList(this.m_valueCollection);
				foreach (object obj in this.m_nameMap)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					styleProperties.m_nameMap.Add(((string)dictionaryEntry.Key).Clone(), dictionaryEntry.Value);
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

		// Token: 0x04001F34 RID: 7988
		private Hashtable m_nameMap;

		// Token: 0x04001F35 RID: 7989
		private ArrayList m_valueCollection;
	}
}
