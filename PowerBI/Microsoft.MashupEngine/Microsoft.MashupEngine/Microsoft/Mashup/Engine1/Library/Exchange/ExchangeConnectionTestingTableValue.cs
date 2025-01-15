using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BDE RID: 3038
	internal class ExchangeConnectionTestingTableValue : WrappingTableValue
	{
		// Token: 0x060052DB RID: 21211 RVA: 0x00117ED0 File Offset: 0x001160D0
		public ExchangeConnectionTestingTableValue(TableValue table)
			: base(table)
		{
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00117ED9 File Offset: 0x001160D9
		protected override TableValue New(TableValue table)
		{
			return new ExchangeConnectionTestingTableValue(table);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00117EE4 File Offset: 0x001160E4
		public override void TestConnection()
		{
			foreach (IValueReference valueReference in this)
			{
				bool flag = true;
				foreach (NamedValue namedValue in (valueReference.Value as RecordValue).GetFields())
				{
					if (!flag)
					{
						namedValue.Value.TestConnection();
						return;
					}
					flag = false;
				}
			}
		}
	}
}
