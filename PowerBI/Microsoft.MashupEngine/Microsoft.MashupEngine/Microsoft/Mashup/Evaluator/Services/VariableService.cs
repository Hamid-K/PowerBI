using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DC4 RID: 7620
	public sealed class VariableService : IVariableService
	{
		// Token: 0x0600BCD8 RID: 48344 RVA: 0x002651E0 File Offset: 0x002633E0
		public bool TryGetValue(string name, out object value)
		{
			Dictionary<string, object> dictionary = this.values;
			bool flag2;
			lock (dictionary)
			{
				flag2 = this.values.TryGetValue(name, out value);
			}
			return flag2;
		}

		// Token: 0x0600BCD9 RID: 48345 RVA: 0x0026522C File Offset: 0x0026342C
		public void Add(string name, object value)
		{
			Dictionary<string, object> dictionary = this.values;
			lock (dictionary)
			{
				this.values.Add(name, value);
			}
		}

		// Token: 0x0600BCDA RID: 48346 RVA: 0x00265274 File Offset: 0x00263474
		public void Remove(string name)
		{
			Dictionary<string, object> dictionary = this.values;
			lock (dictionary)
			{
				this.values.Remove(name);
			}
		}

		// Token: 0x04006056 RID: 24662
		private readonly Dictionary<string, object> values = new Dictionary<string, object>();
	}
}
