using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000172 RID: 370
	public class UnionSchemaElement<TSequenceProgram, TRegionProgram, TRegion> : SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00019682 File Offset: 0x00017882
		public List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Members { get; }

		// Token: 0x0600083A RID: 2106 RVA: 0x0001968C File Offset: 0x0001788C
		public UnionSchemaElement(string name, List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> members, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, null, nullable, learner)
		{
			this.Members = members;
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in this.Members)
			{
				schemaElement.Parent = this;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000196F0 File Offset: 0x000178F0
		public UnionSchemaElement(string name, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, parent, nullable, learner)
		{
			this.Members = new List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00019708 File Offset: 0x00017908
		public override void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element)
		{
			this.Members.Add(element);
			element.Parent = this;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001971D File Offset: 0x0001791D
		public override List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children
		{
			get
			{
				return this.Members;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00019725 File Offset: 0x00017925
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor)
		{
			return visitor.VisitUnion(this);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000170F6 File Offset: 0x000152F6
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override IEnumerable<TRegion> RunWithoutCache(TRegion s, int k)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000170F6 File Offset: 0x000152F6
		public override TreeElement<TRegion> RunTree(TRegion s)
		{
			throw new NotImplementedException();
		}
	}
}
