using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B50 RID: 2896
	public class MessagePropertyCollection : IEnumerable<PropertyValueDefinition>, IEnumerable
	{
		// Token: 0x06005B8B RID: 23435 RVA: 0x00178A7E File Offset: 0x00176C7E
		public MessagePropertyCollection(Message parentMessage)
		{
			this.message = parentMessage;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x00178A8D File Offset: 0x00176C8D
		public IEnumerator<PropertyValueDefinition> GetEnumerator()
		{
			return new MessagePropertyCollectionEnumerator(this);
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x00178A95 File Offset: 0x00176C95
		private IEnumerator GetEnumerator1()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x00178A9D File Offset: 0x00176C9D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator1();
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06005B8F RID: 23439 RVA: 0x00178AA8 File Offset: 0x00176CA8
		public int Count
		{
			get
			{
				RulesAndFormattingVersion2Header rf2Header = this.GetRf2Header(false);
				if (rf2Header != null)
				{
					return rf2Header.Properties.Count;
				}
				return 0;
			}
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x00178ACD File Offset: 0x00176CCD
		public void Add(PropertyValueDefinition newPropertyValue)
		{
			if (newPropertyValue == null)
			{
				throw new ArgumentNullException("newPropertyValue");
			}
			this.GetRf2Header(true).Properties.Add(newPropertyValue);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x00178AF0 File Offset: 0x00176CF0
		internal RulesAndFormattingVersion2Header GetRf2Header(bool createIfNeeded)
		{
			foreach (MqHeader mqHeader in this.message.Headers)
			{
				if (mqHeader.HeaderType == MqHeaderType.RulesAndFormattingVersion2)
				{
					return mqHeader as RulesAndFormattingVersion2Header;
				}
			}
			if (!createIfNeeded)
			{
				return null;
			}
			RulesAndFormattingVersion2Header rulesAndFormattingVersion2Header = new RulesAndFormattingVersion2Header();
			this.message.Headers.Add(rulesAndFormattingVersion2Header);
			return rulesAndFormattingVersion2Header;
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00178B74 File Offset: 0x00176D74
		public bool Remove(PropertyValueDefinition oldPropertyValue)
		{
			if (oldPropertyValue == null)
			{
				throw new ArgumentNullException("oldPropertyValue");
			}
			RulesAndFormattingVersion2Header rf2Header = this.GetRf2Header(false);
			return rf2Header != null && rf2Header.Properties.Remove(oldPropertyValue);
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00178BA8 File Offset: 0x00176DA8
		public void Clear()
		{
			RulesAndFormattingVersion2Header rf2Header = this.GetRf2Header(false);
			if (rf2Header == null)
			{
				return;
			}
			rf2Header.Properties.Clear();
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00178BCC File Offset: 0x00176DCC
		public bool TryGetValue(string propertyName, out PropertyValueDefinition propertyValue)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			propertyValue = null;
			RulesAndFormattingVersion2Header rf2Header = this.GetRf2Header(false);
			return rf2Header != null && rf2Header.Properties.TryGetValue(propertyName, out propertyValue);
		}

		// Token: 0x1700161E RID: 5662
		public PropertyValueDefinition this[string propertyName]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new ArgumentNullException("propertyName");
				}
				RulesAndFormattingVersion2Header rf2Header = this.GetRf2Header(false);
				if (rf2Header == null)
				{
					throw new CustomMqClientException(SR.PropertyNotFoundInMessage);
				}
				return rf2Header.Properties[propertyName];
			}
			set
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new ArgumentNullException("propertyName");
				}
				this.GetRf2Header(true).Properties[propertyName] = value;
			}
		}

		// Token: 0x040047FB RID: 18427
		internal Message message;
	}
}
