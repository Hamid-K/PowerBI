using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200097F RID: 2431
	internal static class MdxCubeMetadata
	{
		// Token: 0x0600459F RID: 17823 RVA: 0x000EABE4 File Offset: 0x000E8DE4
		public static TableValue NewDimensionsTable(CubeContextProvider provider, MdxCube cube, IList<ParameterArguments> parameters = null)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
			foreach (MdxDimension mdxDimension in cube.Dimensions.Values)
			{
				HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>();
				KeysBuilder keysBuilder = default(KeysBuilder);
				foreach (MdxHierarchy mdxHierarchy in mdxDimension.VisibleHierarchies)
				{
					foreach (MdxLevel mdxLevel in mdxHierarchy.Levels)
					{
						string mdxIdentifier = mdxLevel.MdxIdentifier;
						if (hashSet.Add(new IdentifierCubeExpression(mdxIdentifier)))
						{
							keysBuilder.Add(mdxIdentifier);
						}
					}
				}
				if (keysBuilder.Count > 0)
				{
					CubeContext cubeContext;
					provider.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(cube.MdxIdentifier), hashSet.ToArray<IdentifierCubeExpression>(), EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All), parameters ?? EmptyArray<ParameterArguments>.Instance, out cubeContext);
					CubeValue cubeValue = CubeContextCubeValue.New(provider, cubeContext, keysBuilder.ToKeys());
					cubeObjectTableBuilder.AddDimension(mdxDimension.MdxIdentifier, mdxDimension.Caption, cubeValue);
				}
			}
			return cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000EAD78 File Offset: 0x000E8F78
		public static TableValue NewMeasuresTable(MdxCube cube)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
			foreach (MdxMeasure mdxMeasure in cube.Measures)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(mdxMeasure.MdxIdentifier);
				MeasureValue measureValue = new MeasureValue(identifierCubeExpression, mdxMeasure.Type.GetTypeValue().Nullable);
				cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, mdxMeasure.Caption, measureValue);
			}
			TableValue tableValue = cubeObjectTableBuilder.ToTable();
			string text;
			if (cube.TryGetDefaultMeasure(out text))
			{
				RecordValue recordValue = RecordValue.New(Keys.New("Cube.DefaultMemberId"), new Value[] { TextValue.New(text) });
				tableValue = tableValue.NewMeta(tableValue.MetaValue.Concatenate(recordValue).AsRecord).AsTable;
			}
			return tableValue;
		}
	}
}
