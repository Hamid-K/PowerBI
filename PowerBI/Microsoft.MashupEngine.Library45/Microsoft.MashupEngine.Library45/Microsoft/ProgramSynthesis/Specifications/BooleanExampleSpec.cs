using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Specifications
{
	// Token: 0x02000332 RID: 818
	public abstract class BooleanExampleSpec : ExampleSpec
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x000355B4 File Offset: 0x000337B4
		public BooleanExampleSpec(IDictionary<State, object> examples)
			: base(examples)
		{
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000355C0 File Offset: 0x000337C0
		public BooleanExampleSpec(IDictionary<State, bool> examples)
			: base(examples.ToDictionary((KeyValuePair<State, bool> kvp) => kvp.Key, (KeyValuePair<State, bool> kvp) => kvp.Value))
		{
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00035618 File Offset: 0x00033818
		public ReadOnlyDictionary<State, bool> Selection
		{
			get
			{
				return new ReadOnlyDictionary<State, bool>(base.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => (bool)kvp.Value));
			}
		}
	}
}
