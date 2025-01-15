using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B36 RID: 2870
	public class Session : NonInteractiveSession<Program, string, ITable<string>>, ITextReaderInput
	{
		// Token: 0x06004798 RID: 18328 RVA: 0x000E0B15 File Offset: 0x000DED15
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Extraction.Json", logger, true)
		{
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x000E0B30 File Offset: 0x000DED30
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return Session.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x000E0B3C File Offset: 0x000DED3C
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return Session.JsonSerializerSettings;
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000E0B44 File Offset: 0x000DED44
		public void AddInput(TextReader reader, int ndJsonLinesToRead = 1000)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			string text;
			while (num < ndJsonLinesToRead && (text = reader.ReadLine()) != null)
			{
				stringBuilder.AppendLine(text);
				num++;
			}
			string text2 = stringBuilder.ToString();
			ParsedJson parsedJson;
			if (Utils.TryParse(text2, out parsedJson) && parsedJson.IsDelimitedJson && parsedJson.Regions.Count<JsonRegion>() > 1)
			{
				base.Inputs.Add(text2);
				return;
			}
			base.Inputs.Add(text2 + reader.ReadToEnd());
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000E0BC4 File Offset: 0x000DEDC4
		protected override IEnumerable<KeyValuePair<string, double>> TrackedLearningMetrics(LearnProgramRequest<Program, string, ITable<string>> request, Program topProgram)
		{
			IEnumerable<KeyValuePair<string, double>> enumerable = base.TrackedLearningMetrics(request, topProgram);
			KeyValuePair<string, double>[] array = new KeyValuePair<string, double>[1];
			int num = 0;
			string text = "NumExamples";
			int? num2 = ((request != null) ? new int?(request.Inputs.Count) : null);
			array[num] = KVP.Create<string, double>(text, ((num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null).GetValueOrDefault());
			return enumerable.Concat(array);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000E0C40 File Offset: 0x000DEE40
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<Program, string, ITable<string>> request, Program topProgram, bool includeConstraints = true)
		{
			string text = ((((request != null) ? request.Constraints : null) == null) ? null : JsonConvert.SerializeObject(request.Constraints, this.JsonSerializerSettingsInstance));
			string text2 = ((request != null) ? request.Inputs.FirstOrDefault<string>() : null);
			string text3 = null;
			if (text2 != null)
			{
				int num = 0;
				int i;
				for (i = 0; i < text2.Length; i++)
				{
					if (text2[i] == '\n')
					{
						num++;
						if (num == 20)
						{
							break;
						}
					}
				}
				text3 = text2.Substring(0, Math.Min(i, 20000));
			}
			return base.TrackedLearningUserProperties(request, topProgram, false).Concat(new KeyValuePair<string, string>[]
			{
				KVP.Create<string, string>("NoInputConstraints", text),
				KVP.Create<string, string>("InputSample", text3)
			}).ToList<KeyValuePair<string, string>>();
		}

		// Token: 0x040020C9 RID: 8393
		private const int LoggingInputLines = 20;

		// Token: 0x040020CA RID: 8394
		private const int LoggingInputSize = 20000;

		// Token: 0x040020CB RID: 8395
		private const int NdJsonLinesToRead = 1000;

		// Token: 0x040020CC RID: 8396
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings().Initialize());
	}
}
