using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200009E RID: 158
	public sealed class RuleOperationError : ComplexProperty, IEnumerable<RuleError>, IEnumerable
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00018E58 File Offset: 0x00017E58
		internal RuleOperationError()
		{
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00018E60 File Offset: 0x00017E60
		public RuleOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00018E68 File Offset: 0x00017E68
		public int Count
		{
			get
			{
				return this.ruleErrors.Count;
			}
		}

		// Token: 0x170001B4 RID: 436
		public RuleError this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.ruleErrors[index];
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00018E9C File Offset: 0x00017E9C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "OperationIndex")
				{
					this.operationIndex = reader.ReadElementValue<int>();
					return true;
				}
				if (localName == "ValidationErrors")
				{
					this.ruleErrors = new RuleErrorCollection();
					this.ruleErrors.LoadFromXml(reader, reader.LocalName);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00018F00 File Offset: 0x00017F00
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			if (jsonProperty.ContainsKey("OperationIndex"))
			{
				this.operationIndex = jsonProperty.ReadAsInt("OperationIndex");
			}
			if (jsonProperty.ContainsKey("ValidationErrors"))
			{
				this.ruleErrors = new RuleErrorCollection();
				((IJsonCollectionDeserializer)this.ruleErrors).CreateFromJsonCollection(jsonProperty.ReadAsArray("ValidationErrors"), service);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00018F5C File Offset: 0x00017F5C
		internal void SetOperationByIndex(IEnumerator<RuleOperation> operations)
		{
			operations.Reset();
			for (int i = 0; i <= this.operationIndex; i++)
			{
				operations.MoveNext();
			}
			this.operation = operations.Current;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00018F93 File Offset: 0x00017F93
		public IEnumerator<RuleError> GetEnumerator()
		{
			return this.ruleErrors.GetEnumerator();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00018FA0 File Offset: 0x00017FA0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ruleErrors.GetEnumerator();
		}

		// Token: 0x04000262 RID: 610
		private int operationIndex;

		// Token: 0x04000263 RID: 611
		private RuleOperation operation;

		// Token: 0x04000264 RID: 612
		private RuleErrorCollection ruleErrors;
	}
}
