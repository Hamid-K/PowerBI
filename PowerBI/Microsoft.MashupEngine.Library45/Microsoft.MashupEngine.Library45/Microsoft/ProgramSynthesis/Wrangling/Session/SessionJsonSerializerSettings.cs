using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000130 RID: 304
	public class SessionJsonSerializerSettings<TProgram, TInput, TOutput> : JsonSerializerSettings where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001604C File Offset: 0x0001424C
		public virtual JsonSerializerSettings Initialize()
		{
			base.TypeNameHandling = TypeNameHandling.None;
			base.DateParseHandling = DateParseHandling.None;
			base.Converters = new List<JsonConverter>
			{
				new KnownTypesJsonConverter(this.SessionTypes),
				new KnownTypesJsonConverter(this.ValueTypes),
				new KnownTypesJsonConverter(this.ConstraintTypes)
			};
			return this;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000160A6 File Offset: 0x000142A6
		protected virtual IEnumerable<Type> SessionTypes
		{
			get
			{
				return new Type[] { typeof(Session<TProgram, TInput, TOutput>) };
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000160BB File Offset: 0x000142BB
		protected virtual IEnumerable<Type> ValueTypes
		{
			get
			{
				return new Type[]
				{
					typeof(IType),
					typeof(UnknownType)
				};
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x000160E0 File Offset: 0x000142E0
		protected virtual IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Constraint<TInput, TOutput>),
					typeof(CorrespondingMemberDoesNotEqual<TInput, TOutput>),
					typeof(CorrespondingMemberEquals<TInput, TOutput>),
					typeof(DoesNotEqual<TInput, TOutput>),
					typeof(Example<TInput, TOutput>),
					typeof(MemberPrefix<TInput, TOutput>),
					typeof(MemberSubset<TInput, TOutput>),
					typeof(NegativeMemberSubset<TInput, TOutput>),
					typeof(NegativeSubset<TInput, TOutput>),
					typeof(OutputIs<TInput, TOutput>),
					typeof(Prefix<TInput, TOutput>),
					typeof(Subset<TInput, TOutput>)
				};
			}
		}
	}
}
