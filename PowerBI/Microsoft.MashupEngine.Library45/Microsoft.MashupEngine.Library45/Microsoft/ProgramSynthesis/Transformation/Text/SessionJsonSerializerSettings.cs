using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BB7 RID: 7095
	public class SessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<Program, IRow, object>
	{
		// Token: 0x0600E841 RID: 59457 RVA: 0x00313B1C File Offset: 0x00311D1C
		public override JsonSerializerSettings Initialize()
		{
			JsonSerializerSettings jsonSerializerSettings = base.Initialize();
			jsonSerializerSettings.Converters.Add(new KnownTypesJsonConverter(new Type[]
			{
				typeof(IRow),
				typeof(InputRow),
				typeof(ValueSubstringRow)
			}.Concat(EntityBasedTokenizer.KnownSubTypes)));
			jsonSerializerSettings.Converters.Add(new StringEnumConverter());
			return jsonSerializerSettings;
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x0600E842 RID: 59458 RVA: 0x00313B88 File Offset: 0x00311D88
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(Session) });
			}
		}

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x0600E843 RID: 59459 RVA: 0x00313BA8 File Offset: 0x00311DA8
		protected override IEnumerable<Type> ValueTypes
		{
			get
			{
				return base.ValueTypes.Concat(new Type[]
				{
					typeof(FormattedPartialDateTimeType),
					typeof(PartialDateTimeType),
					typeof(NumberType)
				});
			}
		}

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x0600E844 RID: 59460 RVA: 0x00313BE4 File Offset: 0x00311DE4
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[]
				{
					typeof(ColumnPriority),
					typeof(MergeColumns),
					typeof(SingleInputDateFormat),
					typeof(Example),
					typeof(ExternalEntityExtraction)
				});
			}
		}
	}
}
