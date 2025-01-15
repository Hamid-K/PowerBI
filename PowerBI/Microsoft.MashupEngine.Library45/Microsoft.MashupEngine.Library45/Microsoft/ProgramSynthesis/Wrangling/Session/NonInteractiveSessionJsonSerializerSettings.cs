using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000123 RID: 291
	public class NonInteractiveSessionJsonSerializerSettings<TProgram, TInput, TOutput> : SessionJsonSerializerSettings<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00015206 File Offset: 0x00013406
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(NonInteractiveSession<TProgram, TInput, TOutput>) });
			}
		}
	}
}
