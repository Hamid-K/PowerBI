using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000490 RID: 1168
	public class RDEventDictionary : Dictionary<string, object>
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x0007A0CC File Offset: 0x000782CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in base.Keys)
			{
				stringBuilder.AppendFormat("{0} : {1} ", text, base[text]);
			}
			return stringBuilder.ToString();
		}
	}
}
