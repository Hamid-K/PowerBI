using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Compound.Split.Constraints;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x02000917 RID: 2327
	public class SessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x00094B71 File Offset: 0x00092D71
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(Session) });
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x00094B94 File Offset: 0x00092D94
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[]
				{
					typeof(EnableTelemetry),
					typeof(FixedWidth),
					typeof(LineLengthLimit),
					typeof(SimpleDelimiter),
					typeof(SimpleDelimiterOrFixedWidth),
					typeof(TimeLimit)
				});
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00094C00 File Offset: 0x00092E00
		public override JsonSerializerSettings Initialize()
		{
			JsonSerializerSettings jsonSerializerSettings = base.Initialize();
			jsonSerializerSettings.Converters.Add(new KnownTypesJsonConverter(new Type[]
			{
				typeof(StringRegion),
				typeof(ITable<StringRegion>)
			}));
			return jsonSerializerSettings;
		}
	}
}
