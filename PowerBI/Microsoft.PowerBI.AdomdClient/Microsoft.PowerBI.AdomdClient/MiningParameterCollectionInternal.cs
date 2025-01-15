using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C5 RID: 197
	internal sealed class MiningParameterCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x0002C330 File Offset: 0x0002A530
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

		// Token: 0x170003E4 RID: 996
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

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002C4CA File Offset: 0x0002A6CA
		public IEnumerator GetEnumerator()
		{
			return new MiningParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002C4D7 File Offset: 0x0002A6D7
		public void CopyTo(Array array, int index)
		{
			this.internalObjectCollection.CopyTo(array, index);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002C4E6 File Offset: 0x0002A6E6
		public int Count
		{
			get
			{
				return this.internalObjectCollection.Count;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002C4F3 File Offset: 0x0002A6F3
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002C4F6 File Offset: 0x0002A6F6
		public object SyncRoot
		{
			get
			{
				return this.internalObjectCollection.SyncRoot;
			}
		}

		// Token: 0x0400074C RID: 1868
		private ArrayList internalObjectCollection;
	}
}
