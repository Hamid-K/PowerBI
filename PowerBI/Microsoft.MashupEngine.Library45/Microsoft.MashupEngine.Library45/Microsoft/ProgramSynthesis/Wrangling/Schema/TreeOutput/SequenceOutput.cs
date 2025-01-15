using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput
{
	// Token: 0x02000134 RID: 308
	public class SequenceOutput<TRegion> : ITreeOutput<TRegion>, IEnumerable<ITreeOutput<TRegion>>, IEnumerable where TRegion : IRegion<TRegion>
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001633E File Offset: 0x0001453E
		public SequenceOutput(string name, IEnumerable<ITreeOutput<TRegion>> elements)
		{
			this.Name = name;
			this._elements = elements;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00016354 File Offset: 0x00014554
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001635C File Offset: 0x0001455C
		public string Name { get; set; }

		// Token: 0x060006E4 RID: 1764 RVA: 0x00016368 File Offset: 0x00014568
		public IEnumerable<IReadOnlyList<TRegion>> ToTable(ISchemaElement<TRegion> schema, TreeToTableSemantics semantics)
		{
			List<IReadOnlyList<TRegion>> list = new List<IReadOnlyList<TRegion>>();
			SequenceElement<TRegion> sequenceElement = schema as SequenceElement<TRegion>;
			if (sequenceElement == null)
			{
				throw new InvalidOperationException("Invalid schema");
			}
			foreach (ITreeOutput<TRegion> treeOutput in this._elements)
			{
				list.AddRange(treeOutput.ToTable(sequenceElement.Child, semantics));
			}
			if (semantics != TreeToTableSemantics.OuterJoin || list.Count != 0)
			{
				return list;
			}
			List<TRegion> list2 = Enumerable.Repeat<TRegion>(default(TRegion), sequenceElement.DescendantOutputFields.Count<string>()).ToList<TRegion>();
			list.Add(list2);
			return list;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00016414 File Offset: 0x00014614
		public IEnumerator<ITreeOutput<TRegion>> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00016421 File Offset: 0x00014621
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000311 RID: 785
		private readonly IEnumerable<ITreeOutput<TRegion>> _elements;
	}
}
