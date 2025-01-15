using System;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.AST.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x0200015B RID: 347
	public static class SchemaElementExtensions
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00018568 File Offset: 0x00016768
		public static SchemaElement<TSequenceProgram, TRegionProgram, TRegion> PreTraverse<TSequenceProgram, TRegionProgram, TRegion>(this SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element, Action<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> action) where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			action(element);
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in element.Children)
			{
				schemaElement.PreTraverse(action);
			}
			return element;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000185C4 File Offset: 0x000167C4
		public static void PreTraverseFirstProgram<TSequenceProgram, TRegionProgram, TRegion>(this SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element, Action<ProgramNode> action) where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			if (element.Programs != null && element.Programs.IsAny<IExtractionProgram<TRegion>>())
			{
				element.Programs.ElementAt(0).ProgramNode.PreTraverse(action);
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000185F4 File Offset: 0x000167F4
		public static void ForEach<TSequenceProgram, TRegionProgram, TRegion>(this SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element, Action<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>> callback) where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			callback(element);
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in element.Children)
			{
				schemaElement.ForEach(callback);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001864C File Offset: 0x0001684C
		public static bool ForEachWhile<TSequenceProgram, TRegionProgram, TRegion>(this SchemaElement<TSequenceProgram, TRegionProgram, TRegion> element, Func<SchemaElement<TSequenceProgram, TRegionProgram, TRegion>, bool> callback) where TSequenceProgram : SequenceExtractionProgram<TSequenceProgram, TRegion> where TRegionProgram : RegionExtractionProgram<TRegionProgram, TRegion> where TRegion : IRegion<TRegion>
		{
			bool flag = callback(element);
			foreach (SchemaElement<TSequenceProgram, TRegionProgram, TRegion> schemaElement in element.Children)
			{
				if (flag)
				{
					flag = schemaElement.ForEachWhile(callback);
				}
			}
			return flag;
		}
	}
}
