using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging
{
	// Token: 0x02001E14 RID: 7700
	[JsonObject(MemberSerialization.OptIn)]
	internal class AutoCompleterCreateEvent : LoggingEvent
	{
		// Token: 0x17002AB1 RID: 10929
		// (get) Token: 0x0601017B RID: 65915 RVA: 0x003743B8 File Offset: 0x003725B8
		public override string EventName
		{
			get
			{
				return "AutoCompleterCreateEvent";
			}
		}

		// Token: 0x17002AB2 RID: 10930
		// (get) Token: 0x0601017C RID: 65916 RVA: 0x003743BF File Offset: 0x003725BF
		public bool RowDataIsIncomplete { get; }

		// Token: 0x17002AB3 RID: 10931
		// (get) Token: 0x0601017D RID: 65917 RVA: 0x003743C7 File Offset: 0x003725C7
		// (set) Token: 0x0601017E RID: 65918 RVA: 0x003743CF File Offset: 0x003725CF
		public IReadOnlyList<string> RowData { get; internal set; }

		// Token: 0x17002AB4 RID: 10932
		// (get) Token: 0x0601017F RID: 65919 RVA: 0x003743D8 File Offset: 0x003725D8
		// (set) Token: 0x06010180 RID: 65920 RVA: 0x003743E0 File Offset: 0x003725E0
		public double EntityDetectionTime { get; internal set; }

		// Token: 0x17002AB5 RID: 10933
		// (get) Token: 0x06010181 RID: 65921 RVA: 0x003743E9 File Offset: 0x003725E9
		// (set) Token: 0x06010182 RID: 65922 RVA: 0x003743F1 File Offset: 0x003725F1
		public double SearchTreeConstructionTime { get; internal set; }

		// Token: 0x17002AB6 RID: 10934
		// (get) Token: 0x06010183 RID: 65923 RVA: 0x003743FA File Offset: 0x003725FA
		// (set) Token: 0x06010184 RID: 65924 RVA: 0x00374402 File Offset: 0x00372602
		public double TotalCreationTime { get; internal set; }

		// Token: 0x17002AB7 RID: 10935
		// (get) Token: 0x06010185 RID: 65925 RVA: 0x0037440B File Offset: 0x0037260B
		// (set) Token: 0x06010186 RID: 65926 RVA: 0x00374413 File Offset: 0x00372613
		public IReadOnlyDictionary<Type, int> EntityCounts { get; internal set; }

		// Token: 0x06010187 RID: 65927 RVA: 0x0037441C File Offset: 0x0037261C
		public AutoCompleterCreateEvent(IEnumerable<string> rowData, IEnumerable<EntityToken> extractedEntities, double entityDetectionTime, double searchTreeConstructionTime, double totalCreationTime)
		{
			this.AutoCompleteEventId = Guid.NewGuid();
			this.RowData = (rowData as IReadOnlyList<string>) ?? rowData.ToList<string>();
			bool flag;
			if (this.RowData.Count <= 4)
			{
				flag = this.RowData.Any((string s) => !string.IsNullOrEmpty(s) && s.Length > 512);
			}
			else
			{
				flag = true;
			}
			this.RowDataIsIncomplete = flag;
			if (this.RowDataIsIncomplete)
			{
				this.RowData = (from s in this.RowData.Take(4)
					select s.Substring(0, Math.Min(s.Length, 512))).ToList<string>();
			}
			this.EntityCounts = (from t in extractedEntities
				group t by t.GetType()).ToDictionary((IGrouping<Type, EntityToken> g) => g.Key, (IGrouping<Type, EntityToken> g) => g.Count<EntityToken>());
			this.EntityDetectionTime = entityDetectionTime;
			this.SearchTreeConstructionTime = searchTreeConstructionTime;
			this.TotalCreationTime = totalCreationTime;
		}

		// Token: 0x17002AB8 RID: 10936
		// (get) Token: 0x06010188 RID: 65928 RVA: 0x00374559 File Offset: 0x00372759
		public Guid AutoCompleteEventId { get; }

		// Token: 0x06010189 RID: 65929 RVA: 0x00374564 File Offset: 0x00372764
		protected override IReadOnlyCollection<KeyValuePair<string, double>> GetMetrics()
		{
			if (this._metrics == null)
			{
				this._metrics = new List<KeyValuePair<string, double>>
				{
					new KeyValuePair<string, double>("AutoCompleterTreeBuildTimeInMs", this.SearchTreeConstructionTime),
					new KeyValuePair<string, double>("AutoCompleterEntityExtractionTimeInMs", this.EntityDetectionTime),
					new KeyValuePair<string, double>("AutoCompleterTotalCreateTimeInMs", this.TotalCreationTime)
				};
				this._metrics.AddRange(base.GetMetrics());
			}
			return this._metrics;
		}

		// Token: 0x0601018A RID: 65930 RVA: 0x003745E0 File Offset: 0x003727E0
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetProperties()
		{
			if (this._properties == null)
			{
				this._properties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("AutoCompleterEventId", this.AutoCompleteEventId.ToString()),
					new KeyValuePair<string, string>("AutoCompleterEntityCounts", JsonConvertUtils.SerializeObject(this.EntityCounts))
				};
				this._properties.AddRange(base.GetProperties());
			}
			return this._properties;
		}

		// Token: 0x0601018B RID: 65931 RVA: 0x00374658 File Offset: 0x00372858
		protected override IReadOnlyCollection<KeyValuePair<string, string>> GetUserDataProperties()
		{
			if (this._userDataProperties == null)
			{
				this._userDataProperties = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>("AutoCompleterRowData", JsonConvertUtils.SerializeObject(this.RowData)),
					new KeyValuePair<string, string>("AutoCompleterRowDataIsIncomplete", this.RowDataIsIncomplete.AsLoggingValue())
				};
				this._userDataProperties.AddRange(base.GetUserDataProperties());
			}
			return this._userDataProperties;
		}

		// Token: 0x0601018C RID: 65932 RVA: 0x003746C5 File Offset: 0x003728C5
		public AutoCompleterSuggestEvent ChainSuggestEvent(string userInput, string suggestEventId, IReadOnlyList<ISuggestion> suggestions, double suggestionTime = double.NegativeInfinity)
		{
			return new AutoCompleterSuggestEvent(this, suggestEventId, userInput, suggestions, suggestionTime);
		}

		// Token: 0x04006102 RID: 24834
		private const int MaxStringsToLog = 4;

		// Token: 0x04006103 RID: 24835
		private const int MaxStringLengthToLog = 512;

		// Token: 0x0400610B RID: 24843
		private List<KeyValuePair<string, double>> _metrics;

		// Token: 0x0400610C RID: 24844
		private List<KeyValuePair<string, string>> _properties;

		// Token: 0x0400610D RID: 24845
		private List<KeyValuePair<string, string>> _userDataProperties;
	}
}
