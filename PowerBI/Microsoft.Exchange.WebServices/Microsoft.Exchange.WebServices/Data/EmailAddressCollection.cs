using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000053 RID: 83
	public sealed class EmailAddressCollection : ComplexPropertyCollection<EmailAddress>
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000DD6A File Offset: 0x0000CD6A
		internal EmailAddressCollection()
			: this("Mailbox")
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000DD77 File Offset: 0x0000CD77
		internal EmailAddressCollection(string collectionItemXmlElementName)
		{
			this.collectionItemXmlElementName = collectionItemXmlElementName;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000DD86 File Offset: 0x0000CD86
		public void Add(EmailAddress emailAddress)
		{
			base.InternalAdd(emailAddress);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DD90 File Offset: 0x0000CD90
		public void AddRange(IEnumerable<EmailAddress> emailAddresses)
		{
			foreach (EmailAddress emailAddress in emailAddresses)
			{
				this.Add(emailAddress);
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000DDD8 File Offset: 0x0000CDD8
		public EmailAddress Add(string smtpAddress)
		{
			EmailAddress emailAddress = new EmailAddress(smtpAddress);
			this.Add(emailAddress);
			return emailAddress;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000DDF4 File Offset: 0x0000CDF4
		public void AddRange(IEnumerable<string> smtpAddresses)
		{
			foreach (string text in smtpAddresses)
			{
				this.Add(text);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000DE40 File Offset: 0x0000CE40
		public EmailAddress Add(string name, string smtpAddress)
		{
			EmailAddress emailAddress = new EmailAddress(name, smtpAddress);
			this.Add(emailAddress);
			return emailAddress;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000DE5D File Offset: 0x0000CE5D
		public void Clear()
		{
			base.InternalClear();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000DE65 File Offset: 0x0000CE65
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= base.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			base.InternalRemoveAt(index);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000DE90 File Offset: 0x0000CE90
		public bool Remove(EmailAddress emailAddress)
		{
			EwsUtilities.ValidateParam(emailAddress, "emailAddress");
			return base.InternalRemove(emailAddress);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000DEA4 File Offset: 0x0000CEA4
		internal override EmailAddress CreateComplexProperty(string xmlElementName)
		{
			if (xmlElementName == this.collectionItemXmlElementName)
			{
				return new EmailAddress();
			}
			return null;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000DEBB File Offset: 0x0000CEBB
		internal override EmailAddress CreateDefaultComplexProperty()
		{
			return new EmailAddress();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000DEC2 File Offset: 0x0000CEC2
		internal override string GetCollectionItemXmlElementName(EmailAddress emailAddress)
		{
			return this.collectionItemXmlElementName;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000DECA File Offset: 0x0000CECA
		internal override bool ShouldWriteToRequest()
		{
			return true;
		}

		// Token: 0x0400017F RID: 383
		private string collectionItemXmlElementName;
	}
}
