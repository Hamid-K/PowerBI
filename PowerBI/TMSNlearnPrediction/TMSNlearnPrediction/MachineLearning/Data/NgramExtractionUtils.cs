using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003FD RID: 1021
	public static class NgramExtractionUtils
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x0007CF14 File Offset: 0x0007B114
		public static IDataView ApplyConcatOnSources(IHostEnvironment env, ManyToOneColumn[] columns, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ManyToOneColumn[]>(env, columns, "columns");
			Contracts.CheckValue<IDataView>(env, input, "input");
			List<ConcatTransform.Column> list = new List<ConcatTransform.Column>();
			foreach (ManyToOneColumn manyToOneColumn in columns)
			{
				Contracts.CheckUserArg(env, manyToOneColumn != null, "col");
				Contracts.CheckUserArg(env, !string.IsNullOrWhiteSpace(manyToOneColumn.name), "name");
				Contracts.CheckUserArg(env, Utils.Size<string>(manyToOneColumn.source) > 0, "source");
				Contracts.CheckUserArg(env, manyToOneColumn.source.All((string src) => !string.IsNullOrWhiteSpace(src)), "source");
				if (manyToOneColumn.source.Length > 1)
				{
					list.Add(new ConcatTransform.Column
					{
						source = manyToOneColumn.source,
						name = manyToOneColumn.name
					});
				}
			}
			if (list.Count > 0)
			{
				ConcatTransform.Arguments arguments = new ConcatTransform.Arguments
				{
					column = list.ToArray()
				};
				return new ConcatTransform(arguments, env, input);
			}
			return input;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0007D04C File Offset: 0x0007B24C
		public static string[][] GenerateUniqueSourceNames(IHostEnvironment env, ManyToOneColumn[] columns, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ManyToOneColumn[]>(env, columns, "columns");
			Contracts.CheckValue<ISchema>(env, schema, "schema");
			string[][] array = new string[columns.Length][];
			int num = 0;
			for (int i = 0; i < columns.Length; i++)
			{
				ManyToOneColumn manyToOneColumn = columns[i];
				Contracts.CheckUserArg(env, manyToOneColumn != null, "col");
				Contracts.CheckUserArg(env, !string.IsNullOrWhiteSpace(manyToOneColumn.name), "name");
				bool flag;
				if (Utils.Size<string>(manyToOneColumn.source) > 0)
				{
					flag = manyToOneColumn.source.All((string src) => !string.IsNullOrWhiteSpace(src));
				}
				else
				{
					flag = false;
				}
				Contracts.CheckUserArg(env, flag, "source");
				int num2 = manyToOneColumn.source.Length;
				array[i] = new string[num2];
				for (int j = 0; j < num2; j++)
				{
					string text;
					int num3;
					do
					{
						text = string.Format("_tmp{0:000}", num++);
					}
					while (schema.TryGetColumnIndex(text, ref num3));
					array[i][j] = text;
				}
			}
			return array;
		}
	}
}
