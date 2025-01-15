using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000216 RID: 534
	public sealed class HashJoinTransform : OneToOneTransformBase
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x00040AFA File Offset: 0x0003ECFA
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("HSHJOINF", 65541U, 65541U, 65541U, "HashJoinTransform", null);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00040B1C File Offset: 0x0003ED1C
		public HashJoinTransform(HashJoinTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "HashJoin", Contracts.CheckRef<HashJoinTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(HashJoinTransform.TestColumnType))
		{
			if (args.hashBits < 1 || args.hashBits >= 32)
			{
				throw Contracts.ExceptUserArg(this._host, "hashBits", "hashBits should be between {0} and {1} inclusive", new object[] { 1, 31 });
			}
			this._exes = new HashJoinTransform.ColumnInfoEx[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				int num = args.column[i].hashBits ?? args.hashBits;
				Contracts.CheckUserArg(this._host, 1 <= num && num < 32, "hashBits");
				this._exes[i] = this.CreateColumnInfoEx(args.column[i].join ?? args.join, args.column[i].customSlotMap, args.column[i].hashBits ?? args.hashBits, args.column[i].seed ?? args.seed, args.column[i].ordered ?? args.ordered, this.Infos[i]);
			}
			this.SetMetadata();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00040CFC File Offset: 0x0003EEFC
		private HashJoinTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(HashJoinTransform.TestColumnType))
		{
			this._exes = new HashJoinTransform.ColumnInfoEx[this.Infos.Length];
			int i;
			for (i = 0; i < this.Infos.Length; i++)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, 1 <= num && num < 32);
				uint num2 = ctx.Reader.ReadUInt32();
				bool flag = Utils.ReadBoolByte(ctx.Reader);
				int num3 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num3 >= 0);
				int[][] array = null;
				if (num3 > 0)
				{
					Contracts.CheckDecode(this._host, this.Infos[i].TypeSrc.IsVector);
					array = new int[num3][];
					for (int j = 0; j < num3; j++)
					{
						array[j] = Utils.ReadIntArray(ctx.Reader);
						Contracts.CheckDecode(this._host, Utils.Size<int>(array[j]) > 0);
						Contracts.CheckDecode(this._host, array[j].Distinct<int>().Count<int>() == array[j].Length);
						Contracts.CheckDecode(this._host, array[j].All((int slot) => 0 <= slot && slot < this.Infos[i].TypeSrc.ValueCount));
					}
				}
				this._exes[i] = new HashJoinTransform.ColumnInfoEx(array, num, num2, flag);
			}
			this.SetMetadata();
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00040EC8 File Offset: 0x0003F0C8
		public static HashJoinTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("HashJoin");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(HashJoinTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<HashJoinTransform>(h, "Loading Model", (IChannel ch) => new HashJoinTransform(ctx, h, input));
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00040F60 File Offset: 0x0003F160
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(HashJoinTransform.GetVersionInfo());
			base.SaveBase(ctx);
			for (int i = 0; i < this.Infos.Length; i++)
			{
				HashJoinTransform.ColumnInfoEx columnInfoEx = this._exes[i];
				ctx.Writer.Write(columnInfoEx.HashBits);
				ctx.Writer.Write(columnInfoEx.HashSeed);
				Utils.WriteBoolByte(ctx.Writer, columnInfoEx.Ordered);
				ctx.Writer.Write(Utils.Size<int[]>(columnInfoEx.SlotMap));
				if (columnInfoEx.SlotMap != null)
				{
					for (int j = 0; j < columnInfoEx.SlotMap.Length; j++)
					{
						Utils.WriteIntArray(ctx.Writer, columnInfoEx.SlotMap[j]);
					}
				}
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00041030 File Offset: 0x0003F230
		private HashJoinTransform.ColumnInfoEx CreateColumnInfoEx(bool join, string customSlotMap, int hashBits, uint hashSeed, bool ordered, OneToOneTransformBase.ColInfo colInfo)
		{
			int[][] array = null;
			if (colInfo.TypeSrc.IsVector)
			{
				if (!string.IsNullOrWhiteSpace(customSlotMap))
				{
					array = this.CompileSlotMap(customSlotMap, colInfo.TypeSrc.ValueCount);
				}
				else
				{
					array = HashJoinTransform.CreateDefaultSlotMap(join, colInfo.TypeSrc.ValueCount);
				}
			}
			return new HashJoinTransform.ColumnInfoEx(array, hashBits, hashSeed, ordered);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0004108C File Offset: 0x0003F28C
		private int[][] CompileSlotMap(string slotMapString, int srcSlotCount)
		{
			DvText[] array = new DvText(slotMapString).Split(new char[] { ';' }).ToArray<DvText>();
			int[][] array2 = new int[array.Length][];
			for (int i = 0; i < array2.Length; i++)
			{
				DvText[] array3 = array[i].Split(new char[] { ',' }).ToArray<DvText>();
				int[] array4 = new int[array3.Length];
				array2[i] = array4;
				for (int j = 0; j < array4.Length; j++)
				{
					int num;
					if (!int.TryParse(array3[j].ToString(), out num) || num < 0 || num >= srcSlotCount)
					{
						throw Contracts.Except(this._host, "Unexpected slot index '{1}' in group {0}. Expected 0 to {2}", new object[]
						{
							i,
							array3[j],
							srcSlotCount - 1
						});
					}
					array4[j] = num;
				}
				if (array4.Distinct<int>().Count<int>() < array4.Length)
				{
					throw Contracts.Except(this._host, "Group '{0}' has duplicate slot indices", new object[] { array[i] });
				}
			}
			return array2;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000411F8 File Offset: 0x0003F3F8
		private static int[][] CreateDefaultSlotMap(bool join, int srcSlotCount)
		{
			if (join)
			{
				return new int[][] { Utils.GetIdentityPermutation(srcSlotCount) };
			}
			return (from v in Enumerable.Range(0, srcSlotCount)
				select new int[] { v }).ToArray<int[]>();
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00041248 File Offset: 0x0003F448
		private static string TestColumnType(ColumnType type)
		{
			if (type.ValueCount > 0)
			{
				return null;
			}
			return "Unknown vector size";
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0004125C File Offset: 0x0003F45C
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this._exes.Length; i++)
			{
				HashJoinTransform.ColumnInfoEx columnInfoEx = this._exes[i];
				if (Utils.Size<int[]>(columnInfoEx.SlotMap) > 1)
				{
					using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i))
					{
						builder.AddGetter<VBuffer<DvText>>("SlotNames", new VectorType(TextType.Instance, columnInfoEx.SlotMap.Length), new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames));
					}
				}
			}
			metadata.Seal();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000412F0 File Offset: 0x0003F4F0
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			int outputValueCount = this._exes[iinfo].OutputValueCount;
			DvText[] array = dst.Values;
			if (Utils.Size<DvText>(array) < outputValueCount)
			{
				array = new DvText[outputValueCount];
			}
			string columnName = this._input.Schema.GetColumnName(this.Infos[iinfo].Source);
			bool flag = !MetadataUtils.HasSlotNames(this._input.Schema, this.Infos[iinfo].Source, this.Infos[iinfo].TypeSrc.VectorSize);
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			if (!flag)
			{
				this._input.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", this.Infos[iinfo].Source, ref vbuffer);
				flag = !vbuffer.IsDense || vbuffer.Length != this.Infos[iinfo].TypeSrc.ValueCount;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < outputValueCount; i++)
			{
				int[] array2 = this._exes[iinfo].SlotMap[i];
				stringBuilder.Clear();
				foreach (int num in array2)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("+");
					}
					if (flag)
					{
						stringBuilder.AppendFormat("{0}[{1}]", columnName, num);
					}
					else
					{
						stringBuilder.Append(vbuffer.Values[num]);
					}
				}
				array[i] = new DvText(stringBuilder.ToString());
			}
			dst = new VBuffer<DvText>(outputValueCount, array, dst.Indices);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000414A0 File Offset: 0x0003F6A0
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			if (HashJoinTransform._methGetterOneToOne == null)
			{
				Func<IRow, int, ValueGetter<uint>> func = new Func<IRow, int, ValueGetter<uint>>(this.ComposeGetterOneToOne<int>);
				Interlocked.CompareExchange<MethodInfo>(ref HashJoinTransform._methGetterOneToOne, func.GetMethodInfo().GetGenericMethodDefinition(), null);
			}
			if (HashJoinTransform._methGetterVecToVec == null)
			{
				Func<IRow, int, ValueGetter<VBuffer<uint>>> func2 = new Func<IRow, int, ValueGetter<VBuffer<uint>>>(this.ComposeGetterVecToVec<int>);
				Interlocked.CompareExchange<MethodInfo>(ref HashJoinTransform._methGetterVecToVec, func2.GetMethodInfo().GetGenericMethodDefinition(), null);
			}
			if (HashJoinTransform._methGetterVecToOne == null)
			{
				Func<IRow, int, ValueGetter<uint>> func3 = new Func<IRow, int, ValueGetter<uint>>(this.ComposeGetterVecToOne<int>);
				Interlocked.CompareExchange<MethodInfo>(ref HashJoinTransform._methGetterVecToOne, func3.GetMethodInfo().GetGenericMethodDefinition(), null);
			}
			MethodInfo methodInfo;
			if (!this.Infos[iinfo].TypeSrc.IsVector)
			{
				methodInfo = HashJoinTransform._methGetterOneToOne;
			}
			else if (this._exes[iinfo].OutputValueCount == 1)
			{
				methodInfo = HashJoinTransform._methGetterVecToOne;
			}
			else
			{
				methodInfo = HashJoinTransform._methGetterVecToVec;
			}
			methodInfo = methodInfo.MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.ItemType.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { input, iinfo });
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0004160C File Offset: 0x0003F80C
		private ValueGetter<uint> ComposeGetterOneToOne<TSrc>(IRow input, int iinfo)
		{
			ValueGetter<TSrc> getSrc = base.GetSrcGetter<TSrc>(input, iinfo);
			HashJoinTransform.HashDelegate<TSrc> hashFunction = this.ComposeHashDelegate<TSrc>();
			TSrc src = default(TSrc);
			uint mask = (1U << this._exes[iinfo].HashBits) - 1U;
			uint hashSeed = this._exes[iinfo].HashSeed;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = (hashFunction(ref src, hashSeed) & mask) + 1U;
			};
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000417C0 File Offset: 0x0003F9C0
		private ValueGetter<VBuffer<uint>> ComposeGetterVecToVec<TSrc>(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<TSrc>> getSrc = base.GetSrcGetter<VBuffer<TSrc>>(input, iinfo);
			HashJoinTransform.HashDelegate<TSrc> hashFunction = this.ComposeHashDelegate<TSrc>();
			VBuffer<TSrc> src = default(VBuffer<TSrc>);
			int n = this._exes[iinfo].OutputValueCount;
			int expectedSrcLength = this.Infos[iinfo].TypeSrc.VectorSize;
			int[][] slotMap = this._exes[iinfo].SlotMap;
			uint mask = (1U << this._exes[iinfo].HashBits) - 1U;
			uint hashSeed = this._exes[iinfo].HashSeed;
			bool ordered = this._exes[iinfo].Ordered;
			TSrc[] denseValues = null;
			return delegate(ref VBuffer<uint> dst)
			{
				getSrc.Invoke(ref src);
				Contracts.Check(this._host, src.Length == expectedSrcLength);
				TSrc[] array;
				if (src.IsDense)
				{
					array = src.Values;
				}
				else
				{
					if (denseValues == null)
					{
						denseValues = new TSrc[expectedSrcLength];
					}
					array = denseValues;
					src.CopyTo(array);
				}
				uint[] array2 = dst.Values;
				if (Utils.Size<uint>(array2) < n)
				{
					array2 = new uint[n];
				}
				for (int i = 0; i < n; i++)
				{
					uint num = hashSeed;
					foreach (int num2 in slotMap[i])
					{
						if (ordered)
						{
							num = Hashing.MurmurRound(num, (uint)num2);
						}
						num = hashFunction(ref array[num2], num);
					}
					array2[i] = (Hashing.MixHash(num) & mask) + 1U;
				}
				dst = new VBuffer<uint>(n, array2, dst.Indices);
			};
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00041980 File Offset: 0x0003FB80
		private ValueGetter<uint> ComposeGetterVecToOne<TSrc>(IRow input, int iinfo)
		{
			int[] slots = this._exes[iinfo].SlotMap[0];
			ValueGetter<VBuffer<TSrc>> getSrc = base.GetSrcGetter<VBuffer<TSrc>>(input, iinfo);
			HashJoinTransform.HashDelegate<TSrc> hashFunction = this.ComposeHashDelegate<TSrc>();
			VBuffer<TSrc> src = default(VBuffer<TSrc>);
			int expectedSrcLength = this.Infos[iinfo].TypeSrc.VectorSize;
			uint mask = (1U << this._exes[iinfo].HashBits) - 1U;
			uint hashSeed = this._exes[iinfo].HashSeed;
			bool ordered = this._exes[iinfo].Ordered;
			TSrc[] denseValues = null;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				Contracts.Check(this._host, src.Length == expectedSrcLength);
				TSrc[] array;
				if (src.IsDense)
				{
					array = src.Values;
				}
				else
				{
					if (denseValues == null)
					{
						denseValues = new TSrc[expectedSrcLength];
					}
					array = denseValues;
					src.CopyTo(array);
				}
				uint num = hashSeed;
				foreach (int num2 in slots)
				{
					if (ordered)
					{
						num = Hashing.MurmurRound(num, (uint)num2);
					}
					num = hashFunction(ref array[num2], num);
				}
				dst = (Hashing.MixHash(num) & mask) + 1U;
			};
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00041A74 File Offset: 0x0003FC74
		private HashJoinTransform.HashDelegate<TSrc> ComposeHashDelegate<TSrc>()
		{
			if (typeof(TSrc) == typeof(float))
			{
				return (HashJoinTransform.HashDelegate<TSrc>)this.ComposeFloatHashDelegate();
			}
			if (typeof(TSrc) == typeof(double))
			{
				return (HashJoinTransform.HashDelegate<TSrc>)this.ComposeDoubleHashDelegate();
			}
			StringBuilder sb = null;
			ValueMapper<TSrc, StringBuilder> conv = Conversions.Instance.GetStringConversion<TSrc>();
			return delegate(ref TSrc value, uint seed)
			{
				conv.Invoke(ref value, ref sb);
				return Hashing.MurmurHash(seed, sb, 0, sb.Length);
			};
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00041AF8 File Offset: 0x0003FCF8
		private HashJoinTransform.HashDelegate<float> ComposeFloatHashDelegate()
		{
			return new HashJoinTransform.HashDelegate<float>(this.Hash);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00041B06 File Offset: 0x0003FD06
		private HashJoinTransform.HashDelegate<double> ComposeDoubleHashDelegate()
		{
			return new HashJoinTransform.HashDelegate<double>(this.Hash);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00041B14 File Offset: 0x0003FD14
		private uint Hash(ref float value, uint seed)
		{
			return Hashing.MurmurRound(seed, FloatUtils.GetBits(value));
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00041B24 File Offset: 0x0003FD24
		private uint Hash(ref double value, uint seed)
		{
			ulong bits = FloatUtils.GetBits(value);
			uint num = Hashing.MurmurRound(seed, Utils.GetLo(bits));
			return Hashing.MurmurRound(num, Utils.GetHi(bits));
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00041B52 File Offset: 0x0003FD52
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._exes[iinfo].OutputColumnType;
		}

		// Token: 0x04000689 RID: 1673
		public const int NumBitsMin = 1;

		// Token: 0x0400068A RID: 1674
		public const int NumBitsLim = 32;

		// Token: 0x0400068B RID: 1675
		private const string RegistrationName = "HashJoin";

		// Token: 0x0400068C RID: 1676
		internal const string Summary = "Converts column values into hashes. This transform accepts both numeric and text inputs, both single and vector-valued columns. This is a part of the Dracula transform.";

		// Token: 0x0400068D RID: 1677
		public const string LoaderSignature = "HashJoinTransform";

		// Token: 0x0400068E RID: 1678
		private readonly HashJoinTransform.ColumnInfoEx[] _exes;

		// Token: 0x0400068F RID: 1679
		private static MethodInfo _methGetterOneToOne;

		// Token: 0x04000690 RID: 1680
		private static MethodInfo _methGetterVecToVec;

		// Token: 0x04000691 RID: 1681
		private static MethodInfo _methGetterVecToOne;

		// Token: 0x02000217 RID: 535
		public sealed class Arguments
		{
			// Token: 0x04000693 RID: 1683
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public HashJoinTransform.Column[] column;

			// Token: 0x04000694 RID: 1684
			[Argument(0, HelpText = "Whether the values need to be combined for a single hash")]
			public bool join = true;

			// Token: 0x04000695 RID: 1685
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 31, inclusive.", ShortName = "bits", SortOrder = 2)]
			public int hashBits = 31;

			// Token: 0x04000696 RID: 1686
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x04000697 RID: 1687
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool ordered = true;
		}

		// Token: 0x02000218 RID: 536
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000BF6 RID: 3062 RVA: 0x00041B8C File Offset: 0x0003FD8C
			public static HashJoinTransform.Column Parse(string str)
			{
				HashJoinTransform.Column column = new HashJoinTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000BF7 RID: 3063 RVA: 0x00041BAC File Offset: 0x0003FDAC
			public bool TryUnparse(StringBuilder sb)
			{
				return this.join == null && string.IsNullOrEmpty(this.customSlotMap) && this.hashBits == null && this.seed == null && this.ordered == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000698 RID: 1688
			[Argument(0, HelpText = "Whether the values need to be combined for a single hash")]
			public bool? join;

			// Token: 0x04000699 RID: 1689
			[Argument(0, HelpText = "Which slots should be combined together. Example: 0,3,5;0,1;3;2,1,0. Overrides 'join'.")]
			public string customSlotMap;

			// Token: 0x0400069A RID: 1690
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 31, inclusive.", ShortName = "bits")]
			public int? hashBits;

			// Token: 0x0400069B RID: 1691
			[Argument(0, HelpText = "Hashing seed")]
			public uint? seed;

			// Token: 0x0400069C RID: 1692
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool? ordered;
		}

		// Token: 0x02000219 RID: 537
		public sealed class ColumnInfoEx
		{
			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00041C0B File Offset: 0x0003FE0B
			public int OutputValueCount
			{
				get
				{
					return this.OutputColumnType.ValueCount;
				}
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00041C18 File Offset: 0x0003FE18
			public ColumnInfoEx(int[][] slotMap, int hashBits, uint hashSeed, bool ordered)
			{
				Contracts.Check(1 <= hashBits && hashBits < 32);
				this.SlotMap = slotMap;
				this.HashBits = hashBits;
				this.HashSeed = hashSeed;
				this.Ordered = ordered;
				KeyType itemType = HashJoinTransform.ColumnInfoEx.GetItemType(hashBits);
				if (Utils.Size<int[]>(this.SlotMap) <= 1)
				{
					this.OutputColumnType = itemType;
					return;
				}
				this.OutputColumnType = new VectorType(itemType, this.SlotMap.Length);
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x00041C8C File Offset: 0x0003FE8C
			private static KeyType GetItemType(int hashBits)
			{
				int num = ((hashBits < 31) ? (1 << hashBits) : 0);
				return new KeyType(6, 0UL, num, num > 0);
			}

			// Token: 0x0400069D RID: 1693
			public readonly ColumnType OutputColumnType;

			// Token: 0x0400069E RID: 1694
			public readonly int HashBits;

			// Token: 0x0400069F RID: 1695
			public readonly uint HashSeed;

			// Token: 0x040006A0 RID: 1696
			public readonly bool Ordered;

			// Token: 0x040006A1 RID: 1697
			public readonly int[][] SlotMap;
		}

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x06000BFD RID: 3069
		private delegate uint HashDelegate<TSrc>(ref TSrc value, uint seed);
	}
}
