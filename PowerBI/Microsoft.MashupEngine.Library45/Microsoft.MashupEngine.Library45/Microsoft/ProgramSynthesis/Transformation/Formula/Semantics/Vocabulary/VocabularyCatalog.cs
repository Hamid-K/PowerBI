using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Vocabulary
{
	// Token: 0x02001601 RID: 5633
	public class VocabularyCatalog
	{
		// Token: 0x0600BB4E RID: 47950 RVA: 0x00284D0F File Offset: 0x00282F0F
		public VocabularyCatalog(IEnumerable<CultureInfo> cultures, LearnDebugTrace debugTrace = null)
		{
			this._debugTrace = debugTrace;
			this._cultures = cultures.ToReadOnlyList<CultureInfo>();
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x0600BB4F RID: 47951 RVA: 0x00284D2C File Offset: 0x00282F2C
		protected HashSet<string> Catalog
		{
			get
			{
				HashSet<string> hashSet;
				if ((hashSet = this._catalog) == null)
				{
					hashSet = (this._catalog = this.LoadCatalog());
				}
				return hashSet;
			}
		}

		// Token: 0x0600BB50 RID: 47952 RVA: 0x00284D52 File Offset: 0x00282F52
		public bool Contains(string word)
		{
			return this.Catalog.Contains(word);
		}

		// Token: 0x0600BB51 RID: 47953 RVA: 0x00284D60 File Offset: 0x00282F60
		private HashSet<string> LoadCatalog()
		{
			string text = this._cultures.Select((CultureInfo c) => c.Name).ToJoinString(";");
			HashSet<string> hashSet;
			if (VocabularyCatalog._cache.TryGetValue(text, out hashSet))
			{
				return hashSet;
			}
			LearnDebugTrace debugTrace = this._debugTrace;
			HashSet<string> hashSet3;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("VocabularyCatalog", "LoadCatalog", false, true) : null))
			{
				HashSet<string> hashSet2 = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				foreach (CultureInfo cultureInfo in this._cultures)
				{
					hashSet2.UnionWith(this.LoadCatalog(cultureInfo));
				}
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				hashSet3 = (VocabularyCatalog._cache[text] = hashSet2);
				hashSet3 = hashSet3;
			}
			return hashSet3;
		}

		// Token: 0x0600BB52 RID: 47954 RVA: 0x00284E60 File Offset: 0x00283060
		private HashSet<string> LoadCatalog(CultureInfo culture)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			HashSet<string> hashSet2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("VocabularyCatalog", "LoadCatalog/" + culture.Name, false, true) : null))
			{
				string name = culture.Name;
				HashSet<string> hashSet;
				if (VocabularyCatalog._cache.TryGetValue(name, out hashSet))
				{
					hashSet2 = hashSet;
				}
				else
				{
					string text3;
					try
					{
						Type type = base.GetType();
						string @namespace = type.Namespace;
						string text = culture.Name.Replace("-", string.Empty);
						string text2 = @namespace + ".Catalogs.uncased." + text + ".txt";
						text3 = AssemblyResourceUtils.LoadResourceFromAssembly(type.Assembly, text2);
					}
					catch (MissingManifestResourceException)
					{
						throw new MissingManifestResourceException("Vocabulary not available.");
					}
					VocabularyCatalog._cache[name] = text3.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ConvertToHashSet(StringComparer.InvariantCultureIgnoreCase);
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					hashSet2 = VocabularyCatalog._cache[name];
				}
			}
			return hashSet2;
		}

		// Token: 0x040046D5 RID: 18133
		private static readonly ConcurrentDictionary<string, HashSet<string>> _cache = new ConcurrentDictionary<string, HashSet<string>>();

		// Token: 0x040046D6 RID: 18134
		private HashSet<string> _catalog;

		// Token: 0x040046D7 RID: 18135
		private readonly IReadOnlyList<CultureInfo> _cultures;

		// Token: 0x040046D8 RID: 18136
		private readonly LearnDebugTrace _debugTrace;
	}
}
