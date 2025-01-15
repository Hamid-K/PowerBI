using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C5 RID: 197
	internal sealed class MiningParameterCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x0002C660 File Offset: 0x0002A860
		internal MiningParameterCollectionInternal(string parameters)
		{
			this.internalObjectCollection = new ArrayList();
			ArrayList arrayList = new ArrayList();
			int i = 0;
			char c = ' ';
			char c2 = ' ';
			while (i < parameters.Length)
			{
				if (parameters[i] == '=' || parameters[i] == ',')
				{
					if (c == parameters[i])
					{
						if (parameters[i] == ',')
						{
							arrayList[arrayList.Count - 1] = i;
						}
					}
					else
					{
						if (arrayList.Count == 0)
						{
							c2 = parameters[i];
						}
						arrayList.Add(i);
						c = parameters[i];
					}
				}
				i++;
			}
			if (c2 == ',')
			{
				arrayList.RemoveAt(0);
			}
			if (arrayList.Count % 2 == 1)
			{
				arrayList.Add(parameters.Length);
			}
			i = 0;
			for (int j = 0; j < arrayList.Count; j += 2)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				int num = Convert.ToInt32(arrayList[j], CultureInfo.InvariantCulture);
				int num2 = Convert.ToInt32(arrayList[j + 1], CultureInfo.InvariantCulture);
				text = parameters.Substring(i, num - i);
				text2 = parameters.Substring(num + 1, num2 - num - 1);
				i = num2 + 1;
				if (!string.IsNullOrEmpty(text))
				{
					this.internalObjectCollection.Add(new MiningParameter(text.Trim(), text2.Trim()));
				}
			}
		}

		// Token: 0x170003EA RID: 1002
		public MiningParameter this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (MiningParameter)this.internalObjectCollection[index];
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002C7FA File Offset: 0x0002A9FA
		public IEnumerator GetEnumerator()
		{
			return new MiningParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002C807 File Offset: 0x0002AA07
		public void CopyTo(Array array, int index)
		{
			this.internalObjectCollection.CopyTo(array, index);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002C816 File Offset: 0x0002AA16
		public int Count
		{
			get
			{
				return this.internalObjectCollection.Count;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002C823 File Offset: 0x0002AA23
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002C826 File Offset: 0x0002AA26
		public object SyncRoot
		{
			get
			{
				return this.internalObjectCollection.SyncRoot;
			}
		}

		// Token: 0x04000759 RID: 1881
		private ArrayList internalObjectCollection;
	}
}
