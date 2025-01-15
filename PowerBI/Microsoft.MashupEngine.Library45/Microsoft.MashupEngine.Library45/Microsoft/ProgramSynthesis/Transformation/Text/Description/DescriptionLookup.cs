using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C87 RID: 7303
	public class DescriptionLookup
	{
		// Token: 0x0600F74D RID: 63309 RVA: 0x0034B764 File Offset: 0x00349964
		private DescriptionLookup(IEnumerable<TransformationDescription> descriptions)
		{
			this.Descriptions = descriptions.ToList<TransformationDescription>();
			for (int i = 0; i < this.Descriptions.Count; i++)
			{
				this.Descriptions[i].Index = new int?(i);
			}
			this._lookup = this.Descriptions.Distinct<TransformationDescription>().ToDictionary((TransformationDescription d) => d.Identity, (TransformationDescription d) => d);
		}

		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x0600F74E RID: 63310 RVA: 0x0034B804 File Offset: 0x00349A04
		public IReadOnlyList<TransformationDescription> Descriptions { get; }

		// Token: 0x0600F74F RID: 63311 RVA: 0x0034B80C File Offset: 0x00349A0C
		public static DescriptionLookup Create(ProgramNode program)
		{
			return new DescriptionLookup(program.AcceptVisitor<IEnumerable<TransformationDescription>>(DescriptionExtractionVisitor.Instance));
		}

		// Token: 0x0600F750 RID: 63312 RVA: 0x0034B81E File Offset: 0x00349A1E
		internal TransformationDescription Lookup(ProgramNode node, int inputDateTimeFormatIndex, int? inputDateTimeFormatPartIndex)
		{
			return this._Lookup(node, Record.Create<int, int?>(inputDateTimeFormatIndex, inputDateTimeFormatPartIndex));
		}

		// Token: 0x0600F751 RID: 63313 RVA: 0x0034B833 File Offset: 0x00349A33
		internal TransformationDescription Lookup(ProgramNode node, int outputDateTimeFormatPartIndex)
		{
			return this._Lookup(node, outputDateTimeFormatPartIndex);
		}

		// Token: 0x0600F752 RID: 63314 RVA: 0x0034B842 File Offset: 0x00349A42
		internal TransformationDescription Lookup(ProgramNode node, string columnName)
		{
			return this._Lookup(node, columnName);
		}

		// Token: 0x0600F753 RID: 63315 RVA: 0x0034B84C File Offset: 0x00349A4C
		internal TransformationDescription Lookup(ProgramNode node)
		{
			return this._Lookup(node, null);
		}

		// Token: 0x0600F754 RID: 63316 RVA: 0x0034B856 File Offset: 0x00349A56
		internal TransformationDescription _Lookup(ProgramNode node, object extraIdentity)
		{
			return this._lookup[Record.Create<ProgramNode, object>(node, extraIdentity)];
		}

		// Token: 0x04005B8B RID: 23435
		private readonly IReadOnlyDictionary<Record<ProgramNode, object>, TransformationDescription> _lookup;
	}
}
