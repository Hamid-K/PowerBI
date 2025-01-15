using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;

namespace System.Data.Entity.ModelConfiguration.Design.PluralizationServices
{
	// Token: 0x0200021A RID: 538
	internal class StringBidirectionalDictionary : BidirectionalDictionary<string, string>
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x000512C6 File Offset: 0x0004F4C6
		internal StringBidirectionalDictionary()
		{
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000512CE File Offset: 0x0004F4CE
		internal StringBidirectionalDictionary(Dictionary<string, string> firstToSecondDictionary)
			: base(firstToSecondDictionary)
		{
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000512D7 File Offset: 0x0004F4D7
		internal override bool ExistsInFirst(string value)
		{
			return base.ExistsInFirst(value.ToLowerInvariant());
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000512E5 File Offset: 0x0004F4E5
		internal override bool ExistsInSecond(string value)
		{
			return base.ExistsInSecond(value.ToLowerInvariant());
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000512F3 File Offset: 0x0004F4F3
		internal override string GetFirstValue(string value)
		{
			return base.GetFirstValue(value.ToLowerInvariant());
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00051301 File Offset: 0x0004F501
		internal override string GetSecondValue(string value)
		{
			return base.GetSecondValue(value.ToLowerInvariant());
		}
	}
}
