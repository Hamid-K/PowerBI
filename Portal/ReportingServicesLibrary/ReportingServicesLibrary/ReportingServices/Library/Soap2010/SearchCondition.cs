using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002F5 RID: 757
	public class SearchCondition
	{
		// Token: 0x06001AEA RID: 6890 RVA: 0x000025F4 File Offset: 0x000007F4
		public SearchCondition()
		{
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0006CE54 File Offset: 0x0006B054
		private SearchCondition(SearchCondition sc)
		{
			if (sc != null)
			{
				this.Condition = SearchCondition.ConvertCondition(sc.Condition);
				this.ConditionSpecified = sc.ConditionSpecified;
				this.Values = new List<string>(new string[] { sc.Value });
				this.Name = sc.Name;
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0006CEAD File Offset: 0x0006B0AD
		private static ConditionEnum ConvertCondition(ConditionEnum condition)
		{
			if (condition == ConditionEnum.Contains)
			{
				return ConditionEnum.Contains;
			}
			if (condition != ConditionEnum.Equals)
			{
				throw new InternalCatalogException("unknown condition: {0}", new object[] { condition });
			}
			return ConditionEnum.Equals;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0006CED8 File Offset: 0x0006B0D8
		public static SearchCondition[] From2005Array(SearchCondition[] items)
		{
			SearchCondition[] array = null;
			if (items != null)
			{
				array = Array.ConvertAll<SearchCondition, SearchCondition>(items, new Converter<SearchCondition, SearchCondition>(SearchCondition.From2005));
			}
			return array;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0006CEFE File Offset: 0x0006B0FE
		public static SearchCondition From2005(SearchCondition sc)
		{
			return new SearchCondition(sc);
		}

		// Token: 0x040009F0 RID: 2544
		public ConditionEnum Condition;

		// Token: 0x040009F1 RID: 2545
		[XmlIgnore]
		public bool ConditionSpecified;

		// Token: 0x040009F2 RID: 2546
		[XmlArrayItem("Value")]
		public List<string> Values;

		// Token: 0x040009F3 RID: 2547
		public string Name;
	}
}
