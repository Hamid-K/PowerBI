using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000094 RID: 148
	public sealed class RuleCollection : ComplexProperty, IEnumerable<Rule>, IEnumerable, IJsonCollectionDeserializer
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x00016DF7 File Offset: 0x00015DF7
		internal RuleCollection()
		{
			this.rules = new List<Rule>();
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00016E0A File Offset: 0x00015E0A
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00016E12 File Offset: 0x00015E12
		public bool OutlookRuleBlobExists
		{
			get
			{
				return this.outlookRuleBlobExists;
			}
			internal set
			{
				this.outlookRuleBlobExists = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00016E1B File Offset: 0x00015E1B
		public int Count
		{
			get
			{
				return this.rules.Count;
			}
		}

		// Token: 0x17000177 RID: 375
		public Rule this[int index]
		{
			get
			{
				if (index < 0 || index >= this.rules.Count)
				{
					throw new ArgumentOutOfRangeException("Index");
				}
				return this.rules[index];
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00016E54 File Offset: 0x00015E54
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (reader.IsStartElement(XmlNamespace.Types, "Rule"))
			{
				Rule rule = new Rule();
				rule.LoadFromXml(reader, "Rule");
				this.rules.Add(rule);
				return true;
			}
			return false;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00016E90 File Offset: 0x00015E90
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject != null)
				{
					Rule rule = new Rule();
					rule.LoadFromJson(jsonObject, service);
					this.rules.Add(rule);
				}
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00016EDA File Offset: 0x00015EDA
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00016EE1 File Offset: 0x00015EE1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00016EE9 File Offset: 0x00015EE9
		public IEnumerator<Rule> GetEnumerator()
		{
			return this.rules.GetEnumerator();
		}

		// Token: 0x04000226 RID: 550
		private bool outlookRuleBlobExists;

		// Token: 0x04000227 RID: 551
		private List<Rule> rules;
	}
}
