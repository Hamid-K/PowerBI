using System;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000198 RID: 408
	public abstract class ManyToOneColumnBindingsBase : ColumnBindingsBase
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00030174 File Offset: 0x0002E374
		protected ManyToOneColumnBindingsBase(ManyToOneColumn[] column, ISchema input, Func<ColumnType[], string> testTypes)
			: base(input, true, ManyToOneColumnBindingsBase.GetNamesAndSanitize(column))
		{
			this.Infos = new ManyToOneColumnBindingsBase.ColInfo[base.InfoCount];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ManyToOneColumn manyToOneColumn = column[i];
				string[] source = manyToOneColumn.source;
				int[] array = new int[source.Length];
				ColumnType[] array2 = new ColumnType[source.Length];
				int? num = new int?(0);
				for (int j = 0; j < source.Length; j++)
				{
					Contracts.CheckUserArg(!string.IsNullOrWhiteSpace(source[j]), "source");
					if (!input.TryGetColumnIndex(source[j], ref array[j]))
					{
						throw Contracts.ExceptUserArg("column", "Source column '{0}' not found", new object[] { source[j] });
					}
					array2[j] = input.GetColumnType(array[j]);
					int valueCount = array2[j].ValueCount;
					num = ((valueCount == 0) ? null : checked(num + valueCount));
				}
				if (testTypes != null)
				{
					string text = testTypes(array2);
					if (text != null)
					{
						string text2 = "column";
						string text3 = "Column '{0}' has invalid source types: {1}. Source types: '{2}'.";
						object[] array3 = new object[3];
						array3[0] = manyToOneColumn.name;
						array3[1] = text;
						array3[2] = string.Join(", ", array2.Select((ColumnType type) => type.ToString()));
						throw Contracts.ExceptUserArg(text2, text3, array3);
					}
				}
				this.Infos[i] = new ManyToOneColumnBindingsBase.ColInfo(num.GetValueOrDefault(), array, array2);
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00030320 File Offset: 0x0002E520
		private static string[] GetNamesAndSanitize(ManyToOneColumn[] column)
		{
			Contracts.CheckUserArg(Utils.Size<ManyToOneColumn>(column) > 0, "column");
			string[] array = new string[column.Length];
			for (int i = 0; i < column.Length; i++)
			{
				ManyToOneColumn manyToOneColumn = column[i];
				if (string.IsNullOrWhiteSpace(manyToOneColumn.name))
				{
					Contracts.CheckUserArg(Utils.Size<string>(manyToOneColumn.source) > 0, "name", "Must specify name");
					Contracts.CheckUserArg(manyToOneColumn.source.Length == 1, "column", "New name is required when multiple source columns are specified");
					manyToOneColumn.name = manyToOneColumn.source[0];
				}
				else if (Utils.Size<string>(manyToOneColumn.source) == 0)
				{
					manyToOneColumn.source = new string[] { manyToOneColumn.name };
				}
				array[i] = manyToOneColumn.name;
			}
			return array;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000303E3 File Offset: 0x0002E5E3
		protected ManyToOneColumnBindingsBase(ModelLoadContext ctx, ISchema input, Func<ColumnType[], string> testTypes)
			: this(new ManyToOneColumnBindingsBase.Contents(ctx, input, testTypes))
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000303F3 File Offset: 0x0002E5F3
		private ManyToOneColumnBindingsBase(ManyToOneColumnBindingsBase.Contents contents)
			: base(contents.Input, false, contents.Names)
		{
			this.Infos = contents.Infos;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00030414 File Offset: 0x0002E614
		public virtual void Save(ModelSaveContext ctx)
		{
			ctx.Writer.Write(this.Infos.Length);
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this.Infos[i];
				ctx.SaveNonEmptyString(base.GetColumnNameCore(i));
				ctx.Writer.Write(colInfo.SrcIndices.Length);
				foreach (int num in colInfo.SrcIndices)
				{
					ctx.SaveNonEmptyString(this.Input.GetColumnName(num));
				}
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000304C4 File Offset: 0x0002E6C4
		public Func<int, bool> GetDependencies(Func<int, bool> predicate)
		{
			bool[] active = new bool[this.Input.ColumnCount];
			for (int i = 0; i < base.ColumnCount; i++)
			{
				if (predicate(i))
				{
					bool flag;
					int num = base.MapColumnIndex(out flag, i);
					if (flag)
					{
						active[num] = true;
					}
					else
					{
						foreach (int num2 in this.Infos[num].SrcIndices)
						{
							active[num2] = true;
						}
					}
				}
			}
			return (int col) => 0 <= col && col < active.Length && active[col];
		}

		// Token: 0x04000480 RID: 1152
		public readonly ManyToOneColumnBindingsBase.ColInfo[] Infos;

		// Token: 0x02000199 RID: 409
		public sealed class ColInfo
		{
			// Token: 0x060008BD RID: 2237 RVA: 0x00030561 File Offset: 0x0002E761
			public ColInfo(int srcSize, int[] srcIndices, ColumnType[] srcTypes)
			{
				this.SrcSize = srcSize;
				this.SrcIndices = srcIndices;
				this.SrcTypes = srcTypes;
			}

			// Token: 0x04000482 RID: 1154
			public readonly int SrcSize;

			// Token: 0x04000483 RID: 1155
			public readonly int[] SrcIndices;

			// Token: 0x04000484 RID: 1156
			public readonly ColumnType[] SrcTypes;
		}

		// Token: 0x0200019A RID: 410
		private sealed class Contents
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x0003059C File Offset: 0x0002E79C
			public Contents(ModelLoadContext ctx, ISchema input, Func<ColumnType[], string> testTypes)
			{
				Contracts.CheckValue<ModelLoadContext>(ctx, "ctx");
				Contracts.CheckValue<ISchema>(input, "input");
				this.Input = input;
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num > 0);
				this.Infos = new ManyToOneColumnBindingsBase.ColInfo[num];
				this.Names = new string[num];
				for (int i = 0; i < num; i++)
				{
					this.Names[i] = ctx.LoadNonEmptyString();
					int num2 = ctx.Reader.ReadInt32();
					Contracts.CheckDecode(num2 > 0);
					int[] array = new int[num2];
					ColumnType[] array2 = new ColumnType[num2];
					int? num3 = new int?(0);
					for (int j = 0; j < num2; j++)
					{
						string text = ctx.LoadNonEmptyString();
						if (!input.TryGetColumnIndex(text, ref array[j]))
						{
							throw Contracts.ExceptDecode("Source column '{0}' is required but not found", new object[] { text });
						}
						array2[j] = input.GetColumnType(array[j]);
						int valueCount = array2[j].ValueCount;
						num3 = ((valueCount == 0) ? null : checked(num3 + valueCount));
					}
					if (testTypes != null)
					{
						string text2 = testTypes(array2);
						if (text2 != null)
						{
							string text3 = "Source columns '{0}' have invalid types: {1}. Source types: '{2}'.";
							object[] array3 = new object[3];
							array3[0] = string.Join(", ", array.Select((int k) => input.GetColumnName(k)));
							array3[1] = text2;
							array3[2] = string.Join(", ", array2.Select((ColumnType type) => type.ToString()));
							throw Contracts.ExceptDecode(text3, array3);
						}
					}
					this.Infos[i] = new ManyToOneColumnBindingsBase.ColInfo(num3.GetValueOrDefault(), array, array2);
				}
			}

			// Token: 0x04000485 RID: 1157
			public ISchema Input;

			// Token: 0x04000486 RID: 1158
			public ManyToOneColumnBindingsBase.ColInfo[] Infos;

			// Token: 0x04000487 RID: 1159
			public string[] Names;
		}
	}
}
