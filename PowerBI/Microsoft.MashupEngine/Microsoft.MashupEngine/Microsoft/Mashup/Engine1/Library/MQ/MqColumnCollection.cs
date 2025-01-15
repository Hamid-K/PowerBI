using System;
using System.Collections.ObjectModel;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200091D RID: 2333
	internal sealed class MqColumnCollection : KeyedCollection<string, MqColumnInfo>
	{
		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x000E06E0 File Offset: 0x000DE8E0
		public Keys AllKeys
		{
			get
			{
				if (this.keys == null)
				{
					KeysBuilder keysBuilder = new KeysBuilder(base.Items.Count);
					foreach (MqColumnInfo mqColumnInfo in base.Items)
					{
						keysBuilder.Add(mqColumnInfo.Name);
					}
					this.keys = keysBuilder.ToKeys();
				}
				return this.keys;
			}
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000E0760 File Offset: 0x000DE960
		protected override string GetKeyForItem(MqColumnInfo columnInfo)
		{
			return columnInfo.Name;
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000E0768 File Offset: 0x000DE968
		public bool TryGetValue(string key, out MqColumnInfo columnInfo)
		{
			return base.Dictionary.TryGetValue(key, out columnInfo);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000E0777 File Offset: 0x000DE977
		public bool TryGetValue(int index, out MqColumnInfo columnInfo)
		{
			if (index >= 0 && index < base.Items.Count)
			{
				columnInfo = base[index];
				return true;
			}
			columnInfo = null;
			return false;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000E079A File Offset: 0x000DE99A
		public bool TryGetWritableColumn(string key, out MqColumnInfo columnInfo)
		{
			return base.Dictionary.TryGetValue(key, out columnInfo) && columnInfo.IsWritable;
		}

		// Token: 0x040022F6 RID: 8950
		private Keys keys;
	}
}
