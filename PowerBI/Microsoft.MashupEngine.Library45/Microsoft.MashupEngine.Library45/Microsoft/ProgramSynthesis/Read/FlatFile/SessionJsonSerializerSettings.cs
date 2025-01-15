using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Read.FlatFile.Constraints;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001259 RID: 4697
	public class SessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<Program, string, ITable<string>>
	{
		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06008D3F RID: 36159 RVA: 0x001DAE3A File Offset: 0x001D903A
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(Session) });
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06008D40 RID: 36160 RVA: 0x001DAE5C File Offset: 0x001D905C
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[]
				{
					typeof(Csv),
					typeof(Delimiter),
					typeof(FixedWidth),
					typeof(LearnLineLimit),
					typeof(Skip),
					typeof(SkipFooter)
				});
			}
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x001DAEC8 File Offset: 0x001D90C8
		public override JsonSerializerSettings Initialize()
		{
			JsonSerializerSettings jsonSerializerSettings = base.Initialize();
			jsonSerializerSettings.Converters.Add(new KnownTypesJsonConverter(new Type[] { typeof(ITable<string>) }));
			return jsonSerializerSettings;
		}
	}
}
