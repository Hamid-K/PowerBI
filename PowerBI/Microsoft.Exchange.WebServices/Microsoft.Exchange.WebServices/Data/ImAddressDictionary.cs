using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000069 RID: 105
	[EditorBrowsable(1)]
	public sealed class ImAddressDictionary : DictionaryProperty<ImAddressKey, ImAddressEntry>
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00011C9E File Offset: 0x00010C9E
		internal override string GetFieldURI()
		{
			return "contacts:ImAddress";
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00011CA5 File Offset: 0x00010CA5
		internal override ImAddressEntry CreateEntryInstance()
		{
			return new ImAddressEntry();
		}

		// Token: 0x17000109 RID: 265
		public string this[ImAddressKey key]
		{
			get
			{
				return base.Entries[key].ImAddress;
			}
			set
			{
				if (value == null)
				{
					base.InternalRemove(key);
					return;
				}
				ImAddressEntry imAddressEntry;
				if (base.Entries.TryGetValue(key, ref imAddressEntry))
				{
					imAddressEntry.ImAddress = value;
					this.Changed();
					return;
				}
				imAddressEntry = new ImAddressEntry(key, value);
				base.InternalAdd(imAddressEntry);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00011D08 File Offset: 0x00010D08
		public bool TryGetValue(ImAddressKey key, out string imAddress)
		{
			ImAddressEntry imAddressEntry = null;
			if (base.Entries.TryGetValue(key, ref imAddressEntry))
			{
				imAddress = imAddressEntry.ImAddress;
				return true;
			}
			imAddress = null;
			return false;
		}
	}
}
