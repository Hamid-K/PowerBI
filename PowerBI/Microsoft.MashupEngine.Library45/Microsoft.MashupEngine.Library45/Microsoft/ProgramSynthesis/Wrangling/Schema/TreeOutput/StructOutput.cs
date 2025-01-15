using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput
{
	// Token: 0x02000135 RID: 309
	public class StructOutput<TRegion> : ITreeOutput<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00016429 File Offset: 0x00014629
		public StructOutput(string name, TRegion value, IEnumerable<ITreeOutput<TRegion>> children = null)
		{
			this.Name = name;
			this.Value = value;
			this.Children = children;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00016446 File Offset: 0x00014646
		public TRegion Value { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001644E File Offset: 0x0001464E
		public IEnumerable<ITreeOutput<TRegion>> Children { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00016456 File Offset: 0x00014656
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001645E File Offset: 0x0001465E
		public string Name { get; set; }

		// Token: 0x060006EC RID: 1772 RVA: 0x00016468 File Offset: 0x00014668
		public IEnumerable<IReadOnlyList<TRegion>> ToTable(ISchemaElement<TRegion> schema, TreeToTableSemantics semantics)
		{
			IReadOnlyList<TRegion>[] array;
			if (!string.IsNullOrEmpty(this.Name))
			{
				(array = new IReadOnlyList<TRegion>[1])[0] = new TRegion[] { this.Value };
			}
			else
			{
				(array = new IReadOnlyList<TRegion>[1])[0] = new TRegion[0];
			}
			IEnumerable<IReadOnlyList<TRegion>> enumerable = array;
			if (this.Children == null)
			{
				return enumerable;
			}
			StructElement<TRegion> structElement = schema as StructElement<TRegion>;
			if (structElement == null)
			{
				throw new InvalidOperationException("Invalid schema");
			}
			foreach (Record<ITreeOutput<TRegion>, ISchemaElement<TRegion>> record in this.Children.ZipWith(structElement.Children))
			{
				List<IReadOnlyList<TRegion>> subtable = record.Item1.ToTable(record.Item2, semantics).ToList<IReadOnlyList<TRegion>>();
				enumerable = from existingRow in enumerable
					from newRow in subtable
					select existingRow.Concat(newRow).ToList<TRegion>();
			}
			return enumerable;
		}
	}
}
