using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010A1 RID: 4257
	public class FieldExtractionSpec : ExampleWithNegativesSpec
	{
		// Token: 0x06008051 RID: 32849 RVA: 0x001AD4A6 File Offset: 0x001AB6A6
		public FieldExtractionSpec(IEnumerable<KeyValuePair<State, Record<object, IEnumerable<object>>>> examples, Dictionary<State, int[]> nodeIndexes)
			: base(examples)
		{
			this.NodeIndexes = nodeIndexes;
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06008052 RID: 32850 RVA: 0x001AD4B6 File Offset: 0x001AB6B6
		public IReadOnlyDictionary<State, int[]> NodeIndexes { get; }

		// Token: 0x06008053 RID: 32851 RVA: 0x000373A6 File Offset: 0x000355A6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			base.ThrowSerializationUnsupportedException();
			return null;
		}
	}
}
