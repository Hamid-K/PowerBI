using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200014E RID: 334
	public class FieldSchemaElement<TSequenceProgram, TRegionProgram, TRegion> : SchemaElement<TSequenceProgram, TRegionProgram, TRegion> where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000171BB File Offset: 0x000153BB
		private string Type { get; }

		// Token: 0x06000772 RID: 1906 RVA: 0x000171C3 File Offset: 0x000153C3
		public FieldSchemaElement(string name, string type, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, null, nullable, learner)
		{
			this.Type = type;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000171D7 File Offset: 0x000153D7
		public FieldSchemaElement(string name, SchemaElement<TSequenceProgram, TRegionProgram, TRegion> parent, bool nullable, ExtractionLearner<TSequenceProgram, TRegionProgram, TRegion> learner)
			: base(name, parent, nullable, learner)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000171E4 File Offset: 0x000153E4
		public override void AddChild(SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element)
		{
			throw new Exception("Cannot add a child to FieldSchemaElement");
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00016FBA File Offset: 0x000151BA
		public override List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> Children
		{
			get
			{
				return new List<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>>();
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000171F0 File Offset: 0x000153F0
		public override T AcceptVisitor<T>(SchemaVisitor<T, TSequenceProgram, TRegionProgram, TRegion> visitor)
		{
			return visitor.VisitField(this);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000171F9 File Offset: 0x000153F9
		public override void LearnElementAndChildren(IEnumerable<DocumentSpecInterface> specs, int k = 1, bool learnAll = false)
		{
			base.ComputeReferencedElement();
			base.LearnElementField(specs, k, learnAll);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001720C File Offset: 0x0001540C
		public override TreeElement<TRegion> RunTree(TRegion s)
		{
			IEnumerable<TRegion> enumerable = base.Run(s, 0);
			TRegion[] array;
			if ((array = enumerable as TRegion[]) == null)
			{
				array = ((enumerable != null) ? enumerable.ToArray<TRegion>() : null) ?? new TRegion[0];
			}
			TRegion[] array2 = array;
			if (array2.Length == 0)
			{
				return new NullTreeElement<TRegion>(base.Name);
			}
			List<TreeElement<TRegion>> list = new List<TreeElement<TRegion>>(array2.Length);
			foreach (TRegion tregion in array2)
			{
				if (tregion == null)
				{
					list.Add(new NullTreeElement<TRegion>(base.Name));
				}
				else
				{
					list.Add(new FieldTreeElement<TRegion>(base.Name, tregion));
				}
			}
			if (base.ExtractionKind == ExtractionKind.Sequence)
			{
				return new SequenceTreeElement<TRegion>(null, s, list);
			}
			return list[0];
		}
	}
}
