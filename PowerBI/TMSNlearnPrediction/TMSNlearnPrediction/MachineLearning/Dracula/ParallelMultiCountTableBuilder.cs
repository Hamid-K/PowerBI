using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000428 RID: 1064
	public sealed class ParallelMultiCountTableBuilder : IMultiCountTableBuilder
	{
		// Token: 0x06001619 RID: 5657 RVA: 0x00080C78 File Offset: 0x0007EE78
		public ParallelMultiCountTableBuilder(IHostEnvironment env, OneToOneTransformBase.ColInfo[] columnBinding, SubComponent<ICountTableBuilder, SignatureCountTableBuilder>[] builderArgs, SubComponent<ICountTableBuilder, SignatureCountTableBuilder> defaultBuilderArgs, int labelCardinality)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("ParallelMultiCountTableBuilder");
			int num = columnBinding.Length;
			this._countTableBuilders = new ICountTableBuilder[num][];
			for (int i = 0; i < num; i++)
			{
				int valueCount = columnBinding[i].TypeSrc.ValueCount;
				Contracts.CheckUserArg(this._host, valueCount > 0, "column", "vectors of unknown length are not supported");
				this._countTableBuilders[i] = new ICountTableBuilder[valueCount];
				for (int j = 0; j < valueCount; j++)
				{
					SubComponent<ICountTableBuilder, SignatureCountTableBuilder> subComponent = builderArgs[i] ?? defaultBuilderArgs;
					this._countTableBuilders[i][j] = ComponentCatalog.CreateInstance<ICountTableBuilder, SignatureCountTableBuilder>(subComponent, new object[] { env, labelCardinality });
				}
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00080D3A File Offset: 0x0007EF3A
		public void IncrementOne(int iCol, uint key, uint labelKey, double value)
		{
			this.IncrementSlot(iCol, 0, key, labelKey, value);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00080D48 File Offset: 0x0007EF48
		public void IncrementSlot(int iCol, int iSlot, uint key, uint labelKey, double value)
		{
			this._countTableBuilders[iCol][iSlot].Increment((long)((ulong)key), (long)((ulong)labelKey), value);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00080D64 File Offset: 0x0007EF64
		public IMultiCountTable CreateMultiCountTable()
		{
			int num = this._countTableBuilders.Length;
			ICountTable[][] array = new ICountTable[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = this._countTableBuilders[i].Length;
				array[i] = new ICountTable[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = this._countTableBuilders[i][j].CreateCountTable();
				}
			}
			return new ParallelMultiCountTable(this._host, array);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00080DDC File Offset: 0x0007EFDC
		public void LoadExternalCounts(string externalCountsFile, string externalCountsSchema, int labelCardinality)
		{
			Contracts.CheckUserArg(this._host, File.Exists(externalCountsFile), "externalCountsFile", "File does not exist");
			List<ICountTableBuilder> list = new List<ICountTableBuilder>();
			for (int i = 0; i < this._countTableBuilders.Length; i++)
			{
				for (int j = 0; j < this._countTableBuilders[i].Length; j++)
				{
					list.Add(this._countTableBuilders[i][j]);
				}
			}
			string[] array = (from x in externalCountsSchema.Split(new char[] { ',' })
				select x.Trim()).ToArray<string>();
			if (array.Length != list.Count)
			{
				throw Contracts.Except(this._host, "External count schema doesn't match columns: expected {0} columns in schema, got {1}", new object[] { list.Count, array.Length });
			}
			Contracts.CheckParam(this._host, array.Distinct<string>().Count<string>() == list.Count, "Duplicate column IDs encountered in the schema");
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int k = 0; k < list.Count; k++)
			{
				dictionary[array[k]] = k;
			}
			float[] array2 = new float[labelCardinality];
			string text = string.Format("Expected number of columns: {0}", labelCardinality + 3);
			foreach (string text2 in File.ReadLines(externalCountsFile))
			{
				DvText dvText = new DvText(text2);
				DvText dvText2;
				bool flag = dvText.SplitOne('\t', ref dvText2, ref dvText);
				Contracts.CheckDecode(this._host, flag, text);
				int num;
				if (dictionary.TryGetValue(dvText2.ToString(), out num))
				{
					flag = dvText.SplitOne('\t', ref dvText2, ref dvText);
					Contracts.CheckDecode(this._host, flag, text);
					DvInt4 dvInt = 0;
					Conversions.Instance.Convert(ref dvText2, ref dvInt);
					Contracts.CheckDecode(this._host, (dvInt >= 0).IsTrue, "Fail to parse hash id.");
					flag = dvText.SplitOne('\t', ref dvText2, ref dvText);
					Contracts.CheckDecode(this._host, flag, text);
					DvInt8 dvInt2 = 0L;
					Conversions.Instance.Convert(ref dvText2, ref dvInt2);
					Contracts.CheckDecode(this._host, (dvInt2 >= 0L).IsTrue, "Fail to parse hash value.");
					for (int l = 0; l < labelCardinality; l++)
					{
						flag = dvText.SplitOne('\t', ref dvText2, ref dvText);
						Contracts.CheckDecode(this._host, flag == l < labelCardinality - 1, text);
						int num2;
						int num3;
						string rawUnderlyingBufferInfo = dvText2.GetRawUnderlyingBufferInfo(ref num2, ref num3);
						float num4;
						DoubleParser.Result result = DoubleParser.Parse(ref num4, rawUnderlyingBufferInfo, num2, num3);
						Contracts.Check(this._host, result == 0);
						array2[l] = num4;
					}
					list[num].InsertOrUpdateRawCounts(dvInt.RawValue, dvInt2.RawValue + 1L, array2);
				}
			}
		}

		// Token: 0x04000D92 RID: 3474
		public const string RegistrationName = "ParallelMultiCountTableBuilder";

		// Token: 0x04000D93 RID: 3475
		private readonly IHost _host;

		// Token: 0x04000D94 RID: 3476
		private readonly ICountTableBuilder[][] _countTableBuilders;
	}
}
