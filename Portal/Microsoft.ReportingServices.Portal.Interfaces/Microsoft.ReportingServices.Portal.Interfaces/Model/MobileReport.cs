using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000071 RID: 113
	public class MobileReport : CatalogItem
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00003F15 File Offset: 0x00002115
		public MobileReport()
			: base(CatalogItemType.MobileReport)
		{
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00003F1E File Offset: 0x0000211E
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00003F26 File Offset: 0x00002126
		public bool AllowCaching { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00003F2F File Offset: 0x0000212F
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00003F37 File Offset: 0x00002137
		public MobileReportManifest Manifest { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00003F40 File Offset: 0x00002140
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00003F48 File Offset: 0x00002148
		[ReadOnly(true)]
		public bool HasSharedDataSets { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00003F54 File Offset: 0x00002154
		public IList<DataSet> SharedDataSets
		{
			get
			{
				IList<DataSet> list;
				if ((list = this.sharedDataSets) == null)
				{
					list = (this.sharedDataSets = this.LoadSharedDataSets());
				}
				return list;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00003F7A File Offset: 0x0000217A
		protected virtual IList<DataSet> LoadSharedDataSets()
		{
			return new List<DataSet>();
		}

		// Token: 0x0400025B RID: 603
		public const string ContentsFolderName = "contents";

		// Token: 0x0400025C RID: 604
		public const string PackageIdPropertyName = "PackageId";

		// Token: 0x0400025D RID: 605
		public const string PackageNamePropertyName = "PackageName";

		// Token: 0x0400025E RID: 606
		private IList<DataSet> sharedDataSets;

		// Token: 0x0400025F RID: 607
		public static readonly IDictionary<Type, IEnumerable<string>> ExtendedPropertiesMap = new Dictionary<Type, IEnumerable<string>> { 
		{
			typeof(MobileReport),
			new string[] { "AllowCaching", "Manifest" }
		} };
	}
}
