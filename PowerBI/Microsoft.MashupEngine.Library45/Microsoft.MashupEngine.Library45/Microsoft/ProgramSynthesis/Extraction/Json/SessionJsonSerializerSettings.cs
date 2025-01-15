using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Json.Constraints;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B38 RID: 2872
	public class SessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<Program, string, ITable<string>>
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x000E0D36 File Offset: 0x000DEF36
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(Session) });
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x000E0D58 File Offset: 0x000DEF58
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[]
				{
					typeof(AutoFlatten),
					typeof(JoinSingleTopArray),
					typeof(NamePrefix),
					typeof(JoinAllArrays),
					typeof(SplitTopArrays)
				});
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x000E0DB8 File Offset: 0x000DEFB8
		public override JsonSerializerSettings Initialize()
		{
			JsonSerializerSettings jsonSerializerSettings = base.Initialize();
			jsonSerializerSettings.Converters.Add(new KnownTypesJsonConverter(new Type[]
			{
				typeof(JsonRegion),
				typeof(ITreeOutput<JsonRegion>)
			}));
			return jsonSerializerSettings;
		}
	}
}
