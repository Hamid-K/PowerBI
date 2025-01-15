using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200040C RID: 1036
	public sealed class CMCountTable : CountTableBase
	{
		// Token: 0x060015B2 RID: 5554 RVA: 0x0007E59E File Offset: 0x0007C79E
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CM    CT", 65537U, 65537U, 65537U, "CMCountTable", null);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0007E5F4 File Offset: 0x0007C7F4
		public CMCountTable(IHostEnvironment env, float[][][] tables, float[] priorCounts)
			: base(env, "CMCountTable", Utils.Size<float[][]>(tables), priorCounts, 0f, null)
		{
			Contracts.CheckValue<float[][][]>(this._host, tables, "tables");
			this._depth = Utils.Size<float[]>(tables[0]);
			Contracts.Check(this._host, this._depth > 0, "depth must be positive");
			Contracts.Check(this._host, tables.All((float[][] x) => Utils.Size<float[]>(x) == this._depth), "Depth must be the same for all labels");
			this._width = Utils.Size<float>(tables[0][0]);
			Contracts.Check(this._host, this._width > 0, "width must be positive");
			Contracts.Check(this._host, tables.All((float[][] t) => t.All((float[] t2) => Utils.Size<float>(t2) == this._width)), "Width must be the same for all depths");
			this._tables = tables;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0007E6D4 File Offset: 0x0007C8D4
		public static CMCountTable Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(CMCountTable.GetVersionInfo());
			return new CMCountTable(ctx, env);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0007E700 File Offset: 0x0007C900
		private CMCountTable(ModelLoadContext ctx, IHostEnvironment env)
			: base(env, "CMCountTable", ctx)
		{
			this._depth = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._depth > 0);
			this._width = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._width > 0);
			this._tables = new float[this._labelCardinality][][];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				bool flag = Utils.ReadBoolByte(ctx.Reader);
				this._tables[i] = new float[this._depth][];
				for (int j = 0; j < this._depth; j++)
				{
					if (!flag)
					{
						this._tables[i][j] = Utils.ReadSingleArray(ctx.Reader, this._width);
					}
					else
					{
						float[] array = (this._tables[i][j] = new float[this._width]);
						int num = -1;
						for (;;)
						{
							int num2 = num;
							num = ctx.Reader.ReadInt32();
							Contracts.CheckDecode(this._host, num >= -1 && num < this._width);
							if (num < 0)
							{
								break;
							}
							Contracts.CheckDecode(this._host, num > num2);
							float num3 = ctx.Reader.ReadSingle();
							Contracts.CheckDecode(num3 >= 0f);
							array[num] = num3;
						}
					}
				}
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0007E86C File Offset: 0x0007CA6C
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.SetVersionInfo(CMCountTable.GetVersionInfo());
			base.Save(ctx);
			ctx.Writer.Write(this._depth);
			ctx.Writer.Write(this._width);
			for (int i = 0; i < this._labelCardinality; i++)
			{
				float[][] array = this._tables[i];
				bool flag = this.IsTableSparse(array);
				Utils.WriteBoolByte(ctx.Writer, flag);
				foreach (float[] array3 in array)
				{
					if (!flag)
					{
						Utils.WriteSinglesNoCount(ctx.Writer, array3, array3.Length);
					}
					else
					{
						for (int k = 0; k < this._width; k++)
						{
							if (array3[k] > 0f)
							{
								ctx.Writer.Write(k);
								ctx.Writer.Write(array3[k]);
							}
						}
						ctx.Writer.Write(-1);
					}
				}
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0007E96C File Offset: 0x0007CB6C
		private bool IsTableSparse(float[][] table)
		{
			int num = Math.Min(10000, this._width);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < this._depth; j++)
				{
					if (table[j][i] != 0f)
					{
						num2++;
					}
				}
			}
			return (double)num2 < 0.3 * (double)num * (double)this._depth;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0007E9D4 File Offset: 0x0007CBD4
		public override void GetCounts(long key, float[] counts)
		{
			uint num = Hashing.MurmurRound((uint)(key >> 32), (uint)key);
			for (int i = 0; i < this._labelCardinality; i++)
			{
				float num2 = -1f;
				float[][] array = this._tables[i];
				for (int j = 0; j < this._depth; j++)
				{
					int num3 = (int)((ulong)Hashing.MixHash(Hashing.MurmurRound(num, (uint)j)) % (ulong)((long)this._width));
					float num4 = array[j][num3];
					if (num2 > num4 || num2 < 0f)
					{
						num2 = num4;
					}
				}
				counts[i] = num2;
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0007EA5C File Offset: 0x0007CC5C
		public override void GetRawCounts(RawCountKey key, float[] counts)
		{
			int num = this._tables.Length;
			for (int i = 0; i < num; i++)
			{
				counts[i] = this._tables[i][key.HashId][(int)(checked((IntPtr)key.HashValue))];
			}
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0007EC40 File Offset: 0x0007CE40
		public override IEnumerable<RawCountKey> AllRawCountKeys()
		{
			for (int hashId = 0; hashId < this._depth; hashId++)
			{
				for (long hashValue = 0L; hashValue < (long)this._width; hashValue += 1L)
				{
					bool allZero = true;
					for (int i = 0; i < this._tables.Length; i++)
					{
						allZero = allZero && this._tables[i][hashId][(int)(checked((IntPtr)hashValue))] == 0f;
					}
					if (!allZero)
					{
						yield return new RawCountKey(hashId, hashValue);
					}
				}
			}
			yield break;
		}

		// Token: 0x04000D4B RID: 3403
		public const string LoaderSignature = "CMCountTable";

		// Token: 0x04000D4C RID: 3404
		private readonly int _depth;

		// Token: 0x04000D4D RID: 3405
		private readonly int _width;

		// Token: 0x04000D4E RID: 3406
		private readonly float[][][] _tables;
	}
}
