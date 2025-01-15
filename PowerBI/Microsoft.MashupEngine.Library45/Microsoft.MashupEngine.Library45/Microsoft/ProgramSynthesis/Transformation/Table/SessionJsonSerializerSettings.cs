using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Table
{
	// Token: 0x02001A85 RID: 6789
	public class SessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<Program, ITable<object>, ITable<object>>
	{
		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x0600DF5F RID: 57183 RVA: 0x002F630E File Offset: 0x002F450E
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(Session) });
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x0600DF60 RID: 57184 RVA: 0x002F632E File Offset: 0x002F452E
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[0]);
			}
		}

		// Token: 0x0600DF61 RID: 57185 RVA: 0x002F6344 File Offset: 0x002F4544
		public override JsonSerializerSettings Initialize()
		{
			JsonSerializerSettings jsonSerializerSettings = base.Initialize();
			jsonSerializerSettings.Converters.Add(new KnownTypesJsonConverter(new Type[] { typeof(ITable<object>) }));
			return jsonSerializerSettings;
		}
	}
}
