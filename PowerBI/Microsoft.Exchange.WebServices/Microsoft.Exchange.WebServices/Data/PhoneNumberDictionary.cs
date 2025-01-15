using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000082 RID: 130
	[EditorBrowsable(1)]
	public sealed class PhoneNumberDictionary : DictionaryProperty<PhoneNumberKey, PhoneNumberEntry>
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001427B File Offset: 0x0001327B
		internal override string GetFieldURI()
		{
			return "contacts:PhoneNumber";
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00014282 File Offset: 0x00013282
		internal override PhoneNumberEntry CreateEntryInstance()
		{
			return new PhoneNumberEntry();
		}

		// Token: 0x17000149 RID: 329
		public string this[PhoneNumberKey key]
		{
			get
			{
				return base.Entries[key].PhoneNumber;
			}
			set
			{
				if (value == null)
				{
					base.InternalRemove(key);
					return;
				}
				PhoneNumberEntry phoneNumberEntry;
				if (base.Entries.TryGetValue(key, ref phoneNumberEntry))
				{
					phoneNumberEntry.PhoneNumber = value;
					this.Changed();
					return;
				}
				phoneNumberEntry = new PhoneNumberEntry(key, value);
				base.InternalAdd(phoneNumberEntry);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000142E4 File Offset: 0x000132E4
		public bool TryGetValue(PhoneNumberKey key, out string phoneNumber)
		{
			PhoneNumberEntry phoneNumberEntry = null;
			if (base.Entries.TryGetValue(key, ref phoneNumberEntry))
			{
				phoneNumber = phoneNumberEntry.PhoneNumber;
				return true;
			}
			phoneNumber = null;
			return false;
		}
	}
}
