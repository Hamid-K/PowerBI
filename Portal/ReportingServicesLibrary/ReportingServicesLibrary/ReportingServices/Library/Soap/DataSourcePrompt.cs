using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200032C RID: 812
	public class DataSourcePrompt
	{
		// Token: 0x06001B62 RID: 7010 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		internal static DataSourcePrompt[] CollectionToPromptArray(DataSourcePromptCollection promptRepresentatives)
		{
			if (promptRepresentatives == null || promptRepresentatives.Count == 0)
			{
				return new DataSourcePrompt[0];
			}
			DataSourcePrompt[] array = new DataSourcePrompt[promptRepresentatives.Count];
			int num = 0;
			foreach (object obj in promptRepresentatives)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				array[num] = new DataSourcePrompt
				{
					Name = dataSourceInfo.PromptIdentifier,
					DataSourceID = dataSourceInfo.PromptIdentifier,
					Prompt = dataSourceInfo.Prompt
				};
				num++;
			}
			return array;
		}

		// Token: 0x04000AF6 RID: 2806
		public string Name;

		// Token: 0x04000AF7 RID: 2807
		public string DataSourceID;

		// Token: 0x04000AF8 RID: 2808
		public string Prompt;
	}
}
