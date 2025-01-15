using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000261 RID: 609
	public static class CategoricalTransform
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0004B930 File Offset: 0x00049B30
		public static IDataTransform Create(CategoricalTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Categorical");
			Contracts.CheckValue<CategoricalTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, Utils.Size<CategoricalTransform.Column>(args.column) > 0, "column");
			TermTransform termTransform = new TermTransform(args, args.column, host, input);
			List<KeyToVectorTransform.Column> list = new List<KeyToVectorTransform.Column>();
			int i = 0;
			while (i < args.column.Length)
			{
				CategoricalTransform.Column column = args.column[i];
				if (!column.TrySanitize())
				{
					throw Contracts.ExceptUserArg(host, "name");
				}
				bool flag;
				switch (column.outputKind ?? args.outputKind)
				{
				case CategoricalTransform.OutputKind.Bag:
					flag = true;
					break;
				case CategoricalTransform.OutputKind.Ind:
					flag = false;
					break;
				case CategoricalTransform.OutputKind.Key:
					IL_010C:
					i++;
					continue;
				default:
					throw Contracts.ExceptUserArg(env, "outputKind");
				}
				list.Add(new KeyToVectorTransform.Column
				{
					name = column.name,
					source = column.name,
					bag = new bool?(flag)
				});
				goto IL_010C;
			}
			if (list.Count == 0)
			{
				return termTransform;
			}
			return new KeyToVectorTransform(new KeyToVectorTransform.Arguments
			{
				column = list.ToArray()
			}, host, termTransform);
		}

		// Token: 0x040007A9 RID: 1961
		internal const string Summary = "Converts the categorical value into an indicator array by building a dictionary of categories based on the data and using the id in the dictionary as the index in the array.";

		// Token: 0x02000262 RID: 610
		public enum OutputKind : byte
		{
			// Token: 0x040007AB RID: 1963
			[TGUI(Label = "Output is a bag (multi-set) vector")]
			Bag = 1,
			// Token: 0x040007AC RID: 1964
			[TGUI(Label = "Output is an indicator vector")]
			Ind,
			// Token: 0x040007AD RID: 1965
			[TGUI(Label = "Output is a key value")]
			Key
		}

		// Token: 0x02000263 RID: 611
		public sealed class Column : TermTransform.ColumnBase
		{
			// Token: 0x06000D8C RID: 3468 RVA: 0x0004BA84 File Offset: 0x00049C84
			public static CategoricalTransform.Column Parse(string str)
			{
				CategoricalTransform.Column column = new CategoricalTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000D8D RID: 3469 RVA: 0x0004BAA4 File Offset: 0x00049CA4
			protected override bool TryParse(string str)
			{
				string text;
				if (!base.TryParse(str, out text))
				{
					return false;
				}
				if (text == null)
				{
					return true;
				}
				CategoricalTransform.OutputKind outputKind;
				if (!Enum.TryParse<CategoricalTransform.OutputKind>(text, true, out outputKind))
				{
					return false;
				}
				this.outputKind = new CategoricalTransform.OutputKind?(outputKind);
				return true;
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x0004BAE0 File Offset: 0x00049CE0
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.outputKind == null)
				{
					return this.TryUnparseCore(sb);
				}
				CategoricalTransform.OutputKind value = this.outputKind.Value;
				if (!Enum.IsDefined(typeof(CategoricalTransform.OutputKind), value))
				{
					return false;
				}
				string text = this.outputKind.Value.ToString();
				return this.TryUnparseCore(sb, text);
			}

			// Token: 0x040007AE RID: 1966
			[Argument(0, HelpText = "Output kind: Bag (multi-set vector), Ind (indicator vector), or Key (index)", ShortName = "kind")]
			public CategoricalTransform.OutputKind? outputKind;
		}

		// Token: 0x02000264 RID: 612
		public sealed class Arguments : TermTransform.ArgumentsBase
		{
			// Token: 0x06000D90 RID: 3472 RVA: 0x0004BB4D File Offset: 0x00049D4D
			public Arguments()
			{
				this.textKeyValues = true;
			}

			// Token: 0x040007AF RID: 1967
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public CategoricalTransform.Column[] column;

			// Token: 0x040007B0 RID: 1968
			[Argument(0, HelpText = "Output kind: Bag (multi-set vector), Ind (indicator vector), or Key (index)", ShortName = "kind", SortOrder = 102)]
			public CategoricalTransform.OutputKind outputKind = CategoricalTransform.OutputKind.Ind;
		}
	}
}
