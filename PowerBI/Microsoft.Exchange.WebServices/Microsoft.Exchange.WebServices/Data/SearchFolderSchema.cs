using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C9 RID: 457
	[Schema]
	public class SearchFolderSchema : FolderSchema
	{
		// Token: 0x06001512 RID: 5394 RVA: 0x0003B2F3 File Offset: 0x0003A2F3
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(SearchFolderSchema.SearchParameters);
		}

		// Token: 0x04000C9E RID: 3230
		public static readonly PropertyDefinition SearchParameters = new ComplexPropertyDefinition<SearchFolderParameters>("SearchParameters", "folder:SearchParameters", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new SearchFolderParameters());

		// Token: 0x04000C9F RID: 3231
		internal new static readonly SearchFolderSchema Instance = new SearchFolderSchema();

		// Token: 0x020001CA RID: 458
		private static class FieldUris
		{
			// Token: 0x04000CA1 RID: 3233
			public const string SearchParameters = "folder:SearchParameters";
		}
	}
}
