using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000054 RID: 84
	[EditorBrowsable(1)]
	public sealed class EmailAddressDictionary : DictionaryProperty<EmailAddressKey, EmailAddressEntry>
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000DECD File Offset: 0x0000CECD
		internal override string GetFieldURI()
		{
			return "contacts:EmailAddress";
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000DED4 File Offset: 0x0000CED4
		internal override EmailAddressEntry CreateEntryInstance()
		{
			return new EmailAddressEntry();
		}

		// Token: 0x170000D8 RID: 216
		public EmailAddress this[EmailAddressKey key]
		{
			get
			{
				return base.Entries[key].EmailAddress;
			}
			set
			{
				if (value == null)
				{
					base.InternalRemove(key);
					return;
				}
				EmailAddressEntry emailAddressEntry;
				if (base.Entries.TryGetValue(key, ref emailAddressEntry))
				{
					emailAddressEntry.EmailAddress = value;
					this.Changed();
					return;
				}
				emailAddressEntry = new EmailAddressEntry(key, value);
				base.InternalAdd(emailAddressEntry);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000DF38 File Offset: 0x0000CF38
		public bool TryGetValue(EmailAddressKey key, out EmailAddress emailAddress)
		{
			EmailAddressEntry emailAddressEntry = null;
			if (base.Entries.TryGetValue(key, ref emailAddressEntry))
			{
				emailAddress = emailAddressEntry.EmailAddress;
				return true;
			}
			emailAddress = null;
			return false;
		}
	}
}
