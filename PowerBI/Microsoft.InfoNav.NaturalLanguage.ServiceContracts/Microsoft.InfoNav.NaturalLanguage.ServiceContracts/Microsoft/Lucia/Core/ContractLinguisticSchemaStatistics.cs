using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200004E RID: 78
	public sealed class ContractLinguisticSchemaStatistics
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000426E File Offset: 0x0000246E
		private ContractLinguisticSchemaStatistics()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004278 File Offset: 0x00002478
		public ContractLinguisticSchemaStatistics(string dbName, int lsdlHashCode, string linguisticSchemaSource, int exampleCount, Dictionary<string, int> phrasingCountByType, Dictionary<int, int> synonymCountToPropertyCount, Dictionary<int, int> synonymCountToTableOrPodCount, Dictionary<int, int> synonymCountToTextualEntityCount, Dictionary<string, int> entityCountByVisibility, Dictionary<string, int> termCountByType, Dictionary<string, int> entityCountByTermType, Dictionary<string, int> phrasingCountByState)
		{
			this.DatabaseName = dbName;
			this.LinguisticSchemaUniqueIdentifier = lsdlHashCode;
			this.LinguisticSchemaSource = linguisticSchemaSource;
			this.ExampleCount = exampleCount;
			this.PhrasingCountByType = phrasingCountByType;
			this.SynonymCountToPropertyCount = synonymCountToPropertyCount;
			this.SynonymCountToTableOrPodCount = synonymCountToTableOrPodCount;
			this.SynonymCountToTextualEntityCount = synonymCountToTextualEntityCount;
			this.EntityCountByVisibility = entityCountByVisibility;
			this.TermCountByType = termCountByType;
			this.EntityCountByTermType = entityCountByTermType;
			this.PhrasingCountByState = phrasingCountByState;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000042E8 File Offset: 0x000024E8
		public string DatabaseName { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000042F0 File Offset: 0x000024F0
		public string LinguisticSchemaSource { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000042F8 File Offset: 0x000024F8
		public int LinguisticSchemaUniqueIdentifier { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004300 File Offset: 0x00002500
		public int ExampleCount { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004308 File Offset: 0x00002508
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, int> PhrasingCountByType { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004310 File Offset: 0x00002510
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<int, int> SynonymCountToPropertyCount { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00004318 File Offset: 0x00002518
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<int, int> SynonymCountToTableOrPodCount { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004320 File Offset: 0x00002520
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<int, int> SynonymCountToTextualEntityCount { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004328 File Offset: 0x00002528
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, int> EntityCountByVisibility { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004330 File Offset: 0x00002530
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, int> TermCountByType { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004338 File Offset: 0x00002538
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, int> EntityCountByTermType { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004340 File Offset: 0x00002540
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Dictionary<string, int> PhrasingCountByState { get; }

		// Token: 0x06000168 RID: 360 RVA: 0x00004348 File Offset: 0x00002548
		public string ToJsonString(Formatting formatting = Formatting.None)
		{
			return JsonConvert.SerializeObject(this, formatting);
		}
	}
}
