using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000084 RID: 132
	[EditorBrowsable(1)]
	public sealed class PhysicalAddressDictionary : DictionaryProperty<PhysicalAddressKey, PhysicalAddressEntry>
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x000143CA File Offset: 0x000133CA
		internal override PhysicalAddressEntry CreateEntryInstance()
		{
			return new PhysicalAddressEntry();
		}

		// Token: 0x1700014B RID: 331
		public PhysicalAddressEntry this[PhysicalAddressKey key]
		{
			get
			{
				return base.Entries[key];
			}
			set
			{
				if (value == null)
				{
					base.InternalRemove(key);
					return;
				}
				value.Key = key;
				base.InternalAddOrReplace(value);
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000143FA File Offset: 0x000133FA
		public bool TryGetValue(PhysicalAddressKey key, out PhysicalAddressEntry physicalAddress)
		{
			return base.Entries.TryGetValue(key, ref physicalAddress);
		}
	}
}
