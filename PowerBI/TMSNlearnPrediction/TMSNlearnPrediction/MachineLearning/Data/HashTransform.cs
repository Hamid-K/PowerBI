using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002B9 RID: 697
	public sealed class HashTransform : OneToOneTransformBase, ITransformTemplate, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005834B File Offset: 0x0005654B
		private static string TestType(ColumnType type)
		{
			if (type.ItemType.IsText || type.ItemType.IsKey)
			{
				return null;
			}
			return "Expected Text or Key item type";
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005836E File Offset: 0x0005656E
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("HASHTRNS", 65538U, 65538U, 65538U, "HashTransform", null);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000583B0 File Offset: 0x000565B0
		public static HashTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Hash");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(HashTransform.GetVersionInfo());
			return HostExtensions.Apply<HashTransform>(h, "Loading Model", (IChannel ch) => new HashTransform(ctx, h, input));
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00058448 File Offset: 0x00056648
		private HashTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(HashTransform.TestType))
		{
			this._exes = new HashTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._exes[i] = new HashTransform.ColInfoEx(ctx);
			}
			this._types = this.InitColumnTypes();
			TextModelHelper.LoadAll(this._host, ctx, this.Infos.Length, out this._keyValues, out this._kvTypes);
			this.SetMetadata();
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000584D0 File Offset: 0x000566D0
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(HashTransform.GetVersionInfo());
			base.SaveBase(ctx);
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
			TextModelHelper.SaveAll(this._host, ctx, this.Infos.Length, this._keyValues);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00058550 File Offset: 0x00056750
		public HashTransform(HashTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(Contracts.CheckRef<IHostEnvironment>(env, "env"), "Hash", Contracts.CheckRef<HashTransform.Arguments>(env, args, "args").column, input, new Func<ColumnType, string>(HashTransform.TestType))
		{
			if (args.hashBits < 1 || args.hashBits >= 32)
			{
				throw Contracts.ExceptUserArg(this._host, "hashBits", "hashBits should be between {0} and {1} inclusive", new object[] { 1, 31 });
			}
			this._exes = new HashTransform.ColInfoEx[this.Infos.Length];
			List<int> list = null;
			List<int> list2 = null;
			for (int m = 0; m < this.Infos.Length; m++)
			{
				this._exes[m] = new HashTransform.ColInfoEx(args, args.column[m]);
				int andVerifyInvertHashMaxCount = HashTransform.GetAndVerifyInvertHashMaxCount(args, args.column[m], this._exes[m]);
				if (andVerifyInvertHashMaxCount > 0)
				{
					Utils.Add<int>(ref list, m);
					Utils.Add<int>(ref list2, andVerifyInvertHashMaxCount);
				}
			}
			this._types = this.InitColumnTypes();
			if (Utils.Size<int>(list) > 0)
			{
				HashSet<int> hashSet = new HashSet<int>(list.Select((int i) => this.Infos[i].Source));
				using (IRowCursor rowCursor = input.GetRowCursor(new Func<int, bool>(hashSet.Contains), null))
				{
					using (IChannel channel = this._host.Start("Invert hash building"))
					{
						HashTransform.InvertHashHelper[] array = new HashTransform.InvertHashHelper[list.Count];
						Action action = null;
						for (int j = 0; j < array.Length; j++)
						{
							int num = list[j];
							Delegate getterCore = this.GetGetterCore(channel, rowCursor, num, out action);
							HashTransform.ColInfoEx colInfoEx = this._exes[num];
							int num2 = list2[j];
							array[j] = HashTransform.InvertHashHelper.Create(rowCursor, this.Infos[num], colInfoEx, num2, getterCore);
						}
						while (rowCursor.MoveNext())
						{
							for (int k = 0; k < array.Length; k++)
							{
								array[k].Process();
							}
						}
						this._keyValues = new VBuffer<DvText>[this._exes.Length];
						this._kvTypes = new ColumnType[this._exes.Length];
						for (int l = 0; l < array.Length; l++)
						{
							this._keyValues[list[l]] = array[l].GetKeyValuesMetadata();
							this._kvTypes[list[l]] = new VectorType(TextType.Instance, this._keyValues[list[l]].Length);
						}
						channel.Done();
					}
				}
			}
			this.SetMetadata();
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005882C File Offset: 0x00056A2C
		private HashTransform(IHostEnvironment env, HashTransform transform, IDataView newSource)
			: base(env, "Hash", transform, newSource, new Func<ColumnType, string>(HashTransform.TestType))
		{
			this._exes = transform._exes;
			this._types = this.InitColumnTypes();
			this._keyValues = transform._keyValues;
			this._kvTypes = transform._kvTypes;
			this.SetMetadata();
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00058889 File Offset: 0x00056A89
		public IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			return new HashTransform(env, this, newSource);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00058894 File Offset: 0x00056A94
		private static int GetAndVerifyInvertHashMaxCount(HashTransform.Arguments args, HashTransform.Column col, HashTransform.ColInfoEx ex)
		{
			int num = col.invertHash ?? args.invertHash;
			if (num != 0)
			{
				if (num == -1)
				{
					num = int.MaxValue;
				}
				Contracts.CheckUserArg(num > 0, "invertHash", "Value too small, must be -1 or larger");
				if (ex.HashBits >= 31)
				{
					throw Contracts.ExceptUserArg("invertHash", "Cannot support invertHash for a {0} bit hash. 30 is the maximum possible.", new object[] { ex.HashBits });
				}
			}
			return num;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00058914 File Offset: 0x00056B14
		private ColumnType[] InitColumnTypes()
		{
			ColumnType[] array = new ColumnType[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				int num = ((this._exes[i].HashBits < 31) ? (1 << this._exes[i].HashBits) : 0);
				KeyType keyType = new KeyType(6, 0UL, num, num > 0);
				if (!this.Infos[i].TypeSrc.IsVector)
				{
					array[i] = keyType;
				}
				else
				{
					array[i] = new VectorType(keyType, this.Infos[i].TypeSrc.VectorSize);
				}
			}
			return array;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000589AD File Offset: 0x00056BAD
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, (0 <= iinfo) & (iinfo < this.Infos.Length));
			return this._types[iinfo];
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000589D8 File Offset: 0x00056BD8
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, "SlotNames"))
				{
					if (this._kvTypes != null && this._kvTypes[i] != null)
					{
						builder.AddGetter<VBuffer<DvText>>("KeyValues", this._kvTypes[i], new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetTerms));
					}
				}
			}
			metadata.Seal();
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00058A78 File Offset: 0x00056C78
		private void GetTerms(int iinfo, ref VBuffer<DvText> dst)
		{
			this._keyValues[iinfo].CopyTo(ref dst);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00058A8C File Offset: 0x00056C8C
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			if (!this.Infos[iinfo].TypeSrc.IsVector)
			{
				return this.ComposeGetterOne(input, iinfo);
			}
			return this.ComposeGetterVec(input, iinfo);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00058AB8 File Offset: 0x00056CB8
		private ValueGetter<uint> ComposeGetterOne(IRow input, int iinfo)
		{
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			uint num = (1U << this._exes[iinfo].HashBits) - 1U;
			uint num2 = this._exes[iinfo].HashSeed;
			if (this._exes[iinfo].Ordered)
			{
				num2 = Hashing.MurmurRound(num2, 0U);
			}
			DataKind rawKind = typeSrc.RawKind;
			switch (rawKind)
			{
			case 2:
				return this.ComposeGetterOneCore(base.GetSrcGetter<byte>(input, iinfo), num2, num);
			case 3:
			case 5:
				break;
			case 4:
				return this.ComposeGetterOneCore(base.GetSrcGetter<ushort>(input, iinfo), num2, num);
			case 6:
				return this.ComposeGetterOneCore(base.GetSrcGetter<uint>(input, iinfo), num2, num);
			default:
				if (rawKind == 11)
				{
					return this.ComposeGetterOneCore(base.GetSrcGetter<DvText>(input, iinfo), num2, num);
				}
				break;
			}
			return this.ComposeGetterOneCore(base.GetSrcGetter<ulong>(input, iinfo), num2, num);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00058BC0 File Offset: 0x00056DC0
		private ValueGetter<uint> ComposeGetterOneCore(ValueGetter<DvText> getSrc, uint seed, uint mask)
		{
			DvText src = default(DvText);
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = HashTransform.HashCore(seed, ref src, mask);
			};
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00058C34 File Offset: 0x00056E34
		private ValueGetter<uint> ComposeGetterOneCore(ValueGetter<byte> getSrc, uint seed, uint mask)
		{
			byte src = 0;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = HashTransform.HashCore(seed, (uint)src, mask);
			};
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00058CA4 File Offset: 0x00056EA4
		private ValueGetter<uint> ComposeGetterOneCore(ValueGetter<ushort> getSrc, uint seed, uint mask)
		{
			ushort src = 0;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = HashTransform.HashCore(seed, (uint)src, mask);
			};
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00058D14 File Offset: 0x00056F14
		private ValueGetter<uint> ComposeGetterOneCore(ValueGetter<uint> getSrc, uint seed, uint mask)
		{
			uint src = 0U;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = HashTransform.HashCore(seed, src, mask);
			};
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00058D84 File Offset: 0x00056F84
		private ValueGetter<uint> ComposeGetterOneCore(ValueGetter<ulong> getSrc, uint seed, uint mask)
		{
			ulong src = 0UL;
			return delegate(ref uint dst)
			{
				getSrc.Invoke(ref src);
				dst = HashTransform.HashCore(seed, src, mask);
			};
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00058DC0 File Offset: 0x00056FC0
		private ValueGetter<VBuffer<uint>> ComposeGetterVec(IRow input, int iinfo)
		{
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			DataKind rawKind = typeSrc.ItemType.RawKind;
			switch (rawKind)
			{
			case 2:
				return this.ComposeGetterVecCore<byte>(input, iinfo, new HashTransform.HashLoop<byte>(HashTransform.HashUnord), new HashTransform.HashLoop<byte>(HashTransform.HashDense), new HashTransform.HashLoop<byte>(HashTransform.HashSparse));
			case 3:
			case 5:
				break;
			case 4:
				return this.ComposeGetterVecCore<ushort>(input, iinfo, new HashTransform.HashLoop<ushort>(HashTransform.HashUnord), new HashTransform.HashLoop<ushort>(HashTransform.HashDense), new HashTransform.HashLoop<ushort>(HashTransform.HashSparse));
			case 6:
				return this.ComposeGetterVecCore<uint>(input, iinfo, new HashTransform.HashLoop<uint>(HashTransform.HashUnord), new HashTransform.HashLoop<uint>(HashTransform.HashDense), new HashTransform.HashLoop<uint>(HashTransform.HashSparse));
			default:
				if (rawKind == 11)
				{
					return this.ComposeGetterVecCore<DvText>(input, iinfo, new HashTransform.HashLoop<DvText>(HashTransform.HashUnord), new HashTransform.HashLoop<DvText>(HashTransform.HashDense), new HashTransform.HashLoop<DvText>(HashTransform.HashSparse));
				}
				break;
			}
			return this.ComposeGetterVecCore<ulong>(input, iinfo, new HashTransform.HashLoop<ulong>(HashTransform.HashUnord), new HashTransform.HashLoop<ulong>(HashTransform.HashDense), new HashTransform.HashLoop<ulong>(HashTransform.HashSparse));
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000590A4 File Offset: 0x000572A4
		private ValueGetter<VBuffer<uint>> ComposeGetterVecCore<T>(IRow input, int iinfo, HashTransform.HashLoop<T> hasherUnord, HashTransform.HashLoop<T> hasherDense, HashTransform.HashLoop<T> hasherSparse)
		{
			ValueGetter<VBuffer<T>> getSrc = base.GetSrcGetter<VBuffer<T>>(input, iinfo);
			HashTransform.ColInfoEx colInfoEx = this._exes[iinfo];
			uint mask = (1U << colInfoEx.HashBits) - 1U;
			uint seed = colInfoEx.HashSeed;
			int len = this.Infos[iinfo].TypeSrc.VectorSize;
			VBuffer<T> src = default(VBuffer<T>);
			if (!colInfoEx.Ordered)
			{
				hasherDense = hasherUnord;
				hasherSparse = hasherUnord;
			}
			return delegate(ref VBuffer<uint> dst)
			{
				getSrc.Invoke(ref src);
				if (len > 0 && src.Length != len)
				{
					throw Contracts.Except(this._host, "Hash transform expected {0} slots, but got {1}", new object[] { len, src.Length });
				}
				uint[] array = dst.Values;
				if (Utils.Size<uint>(array) < src.Count)
				{
					array = new uint[src.Count];
				}
				if (src.IsDense)
				{
					hasherDense(src.Count, null, src.Values, array, seed, mask);
					dst = new VBuffer<uint>(src.Length, array, dst.Indices);
					return;
				}
				hasherSparse(src.Count, src.Indices, src.Values, array, seed, mask);
				int[] array2 = dst.Indices;
				if (src.Count > 0)
				{
					if (Utils.Size<int>(array2) < src.Count)
					{
						array2 = new int[src.Count];
					}
					Array.Copy(src.Indices, array2, src.Count);
				}
				dst = new VBuffer<uint>(src.Length, src.Count, array, array2);
			};
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005914C File Offset: 0x0005734C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, ref DvText value, uint mask)
		{
			if (!value.HasChars)
			{
				return 0U;
			}
			return (value.Trim().Hash(seed) & mask) + 1U;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00059178 File Offset: 0x00057378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, ref DvText value, int i, uint mask)
		{
			if (!value.HasChars)
			{
				return 0U;
			}
			return (value.Trim().Hash(Hashing.MurmurRound(seed, (uint)i)) & mask) + 1U;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000591A8 File Offset: 0x000573A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, uint value, uint mask)
		{
			if (value == 0U)
			{
				return 0U;
			}
			return (Hashing.MixHash(Hashing.MurmurRound(seed, value)) & mask) + 1U;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000591BF File Offset: 0x000573BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, uint value, int i, uint mask)
		{
			if (value == 0U)
			{
				return 0U;
			}
			return (Hashing.MixHash(Hashing.MurmurRound(Hashing.MurmurRound(seed, (uint)i), value)) & mask) + 1U;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000591DC File Offset: 0x000573DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, ulong value, uint mask)
		{
			if (value == 0UL)
			{
				return 0U;
			}
			uint num = Hashing.MurmurRound(seed, Utils.GetLo(value));
			uint hi = Utils.GetHi(value);
			if (hi != 0U)
			{
				num = Hashing.MurmurRound(num, hi);
			}
			return (Hashing.MixHash(num) & mask) + 1U;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005921C File Offset: 0x0005741C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint HashCore(uint seed, ulong value, int i, uint mask)
		{
			uint lo = Utils.GetLo(value);
			uint hi = Utils.GetHi(value);
			if (lo == 0U && hi == 0U)
			{
				return 0U;
			}
			uint num = Hashing.MurmurRound(Hashing.MurmurRound(seed, (uint)i), lo);
			if (hi != 0U)
			{
				num = Hashing.MurmurRound(num, hi);
			}
			return (Hashing.MixHash(num) & mask) + 1U;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00059264 File Offset: 0x00057464
		private static void HashUnord(int count, int[] indices, DvText[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, ref src[i], mask);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00059290 File Offset: 0x00057490
		private static void HashUnord(int count, int[] indices, byte[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], mask);
			}
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000592B8 File Offset: 0x000574B8
		private static void HashUnord(int count, int[] indices, ushort[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], mask);
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000592E0 File Offset: 0x000574E0
		private static void HashUnord(int count, int[] indices, uint[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], mask);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00059308 File Offset: 0x00057508
		private static void HashUnord(int count, int[] indices, ulong[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], mask);
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00059330 File Offset: 0x00057530
		private static void HashDense(int count, int[] indices, DvText[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, ref src[i], i, mask);
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00059360 File Offset: 0x00057560
		private static void HashDense(int count, int[] indices, byte[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], i, mask);
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005938C File Offset: 0x0005758C
		private static void HashDense(int count, int[] indices, ushort[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], i, mask);
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000593B8 File Offset: 0x000575B8
		private static void HashDense(int count, int[] indices, uint[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], i, mask);
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x000593E4 File Offset: 0x000575E4
		private static void HashDense(int count, int[] indices, ulong[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], i, mask);
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00059410 File Offset: 0x00057610
		private static void HashSparse(int count, int[] indices, DvText[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, ref src[i], indices[i], mask);
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00059440 File Offset: 0x00057640
		private static void HashSparse(int count, int[] indices, byte[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], indices[i], mask);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005946C File Offset: 0x0005766C
		private static void HashSparse(int count, int[] indices, ushort[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, (uint)src[i], indices[i], mask);
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00059498 File Offset: 0x00057698
		private static void HashSparse(int count, int[] indices, uint[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], indices[i], mask);
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000594C4 File Offset: 0x000576C4
		private static void HashSparse(int count, int[] indices, ulong[] src, uint[] dst, uint seed, uint mask)
		{
			for (int i = 0; i < count; i++)
			{
				dst[i] = HashTransform.HashCore(seed, src[i], indices[i], mask);
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000594EF File Offset: 0x000576EF
		[Conditional("DEBUG")]
		private static void AssertValid<T>(int count, T[] src, uint[] dst)
		{
		}

		// Token: 0x040008E0 RID: 2272
		public const int NumBitsMin = 1;

		// Token: 0x040008E1 RID: 2273
		public const int NumBitsLim = 32;

		// Token: 0x040008E2 RID: 2274
		private const string RegistrationName = "Hash";

		// Token: 0x040008E3 RID: 2275
		internal const string Summary = "Converts column values into hashes. This transform accepts text and keys as inputs. It works on single- and vector-valued columns, and hashes each slot in a vector separately.";

		// Token: 0x040008E4 RID: 2276
		public const string LoaderSignature = "HashTransform";

		// Token: 0x040008E5 RID: 2277
		private readonly HashTransform.ColInfoEx[] _exes;

		// Token: 0x040008E6 RID: 2278
		private readonly ColumnType[] _types;

		// Token: 0x040008E7 RID: 2279
		private readonly VBuffer<DvText>[] _keyValues;

		// Token: 0x040008E8 RID: 2280
		private readonly ColumnType[] _kvTypes;

		// Token: 0x020002BA RID: 698
		public sealed class Arguments
		{
			// Token: 0x040008E9 RID: 2281
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public HashTransform.Column[] column;

			// Token: 0x040008EA RID: 2282
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 31, inclusive", ShortName = "bits", SortOrder = 2)]
			public int hashBits = 31;

			// Token: 0x040008EB RID: 2283
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x040008EC RID: 2284
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool ordered;

			// Token: 0x040008ED RID: 2285
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int invertHash;
		}

		// Token: 0x020002BB RID: 699
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06001024 RID: 4132 RVA: 0x0005950C File Offset: 0x0005770C
			public static HashTransform.Column Parse(string str)
			{
				HashTransform.Column column = new HashTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001025 RID: 4133 RVA: 0x0005952C File Offset: 0x0005772C
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
				int num;
				if (!int.TryParse(text, out num))
				{
					return false;
				}
				this.hashBits = new int?(num);
				return true;
			}

			// Token: 0x06001026 RID: 4134 RVA: 0x00059564 File Offset: 0x00057764
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.seed != null || this.ordered != null || this.invertHash != null)
				{
					return false;
				}
				if (this.hashBits == null)
				{
					return this.TryUnparseCore(sb);
				}
				string text = this.hashBits.Value.ToString();
				return this.TryUnparseCore(sb, text);
			}

			// Token: 0x040008EE RID: 2286
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 31, inclusive", ShortName = "bits")]
			public int? hashBits;

			// Token: 0x040008EF RID: 2287
			[Argument(0, HelpText = "Hashing seed")]
			public uint? seed;

			// Token: 0x040008F0 RID: 2288
			[Argument(0, HelpText = "Whether the position of each term should be included in the hash", ShortName = "ord")]
			public bool? ordered;

			// Token: 0x040008F1 RID: 2289
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int? invertHash;
		}

		// Token: 0x020002BC RID: 700
		private sealed class ColInfoEx
		{
			// Token: 0x06001028 RID: 4136 RVA: 0x000595D4 File Offset: 0x000577D4
			public ColInfoEx(HashTransform.Arguments args, HashTransform.Column col)
			{
				this.HashBits = col.hashBits ?? args.hashBits;
				if (this.HashBits < 1 || this.HashBits >= 32)
				{
					throw Contracts.ExceptUserArg("hashBits", "hashBits should be between {0} and {1} inclusive", new object[] { 1, 31 });
				}
				this.HashSeed = col.seed ?? args.seed;
				this.Ordered = col.ordered ?? args.ordered;
			}

			// Token: 0x06001029 RID: 4137 RVA: 0x00059694 File Offset: 0x00057894
			public ColInfoEx(ModelLoadContext ctx)
			{
				this.HashBits = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(1 <= this.HashBits && this.HashBits < 32);
				this.HashSeed = ctx.Reader.ReadUInt32();
				this.Ordered = Utils.ReadBoolByte(ctx.Reader);
			}

			// Token: 0x0600102A RID: 4138 RVA: 0x000596F5 File Offset: 0x000578F5
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this.HashBits);
				ctx.Writer.Write(this.HashSeed);
				Utils.WriteBoolByte(ctx.Writer, this.Ordered);
			}

			// Token: 0x040008F2 RID: 2290
			public readonly int HashBits;

			// Token: 0x040008F3 RID: 2291
			public readonly uint HashSeed;

			// Token: 0x040008F4 RID: 2292
			public readonly bool Ordered;
		}

		// Token: 0x020002BD RID: 701
		// (Invoke) Token: 0x0600102C RID: 4140
		private delegate void HashLoop<TSrc>(int count, int[] indices, TSrc[] src, uint[] dst, uint seed, uint mask);

		// Token: 0x020002BE RID: 702
		private abstract class InvertHashHelper
		{
			// Token: 0x0600102F RID: 4143 RVA: 0x0005972C File Offset: 0x0005792C
			private InvertHashHelper(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex)
			{
				this._row = row;
				this._info = info;
				this._ex = ex;
				this._includeSlot = this._info.TypeSrc.IsVector && this._ex.Ordered;
			}

			// Token: 0x06001030 RID: 4144 RVA: 0x0005977C File Offset: 0x0005797C
			public static HashTransform.InvertHashHelper Create(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex, int invertHashMaxCount, Delegate dstGetter)
			{
				ColumnType typeSrc = info.TypeSrc;
				Type type = (typeSrc.IsVector ? (ex.Ordered ? typeof(HashTransform.InvertHashHelper.ImplVecOrdered<>) : typeof(HashTransform.InvertHashHelper.ImplVec<>)) : typeof(HashTransform.InvertHashHelper.ImplOne<>));
				type = type.MakeGenericType(new Type[] { typeSrc.ItemType.RawType });
				Type[] array = new Type[]
				{
					typeof(IRow),
					typeof(OneToOneTransformBase.ColInfo),
					typeof(HashTransform.ColInfoEx),
					typeof(int),
					typeof(Delegate)
				};
				ConstructorInfo constructor = type.GetConstructor(array);
				return (HashTransform.InvertHashHelper)constructor.Invoke(new object[] { row, info, ex, invertHashMaxCount, dstGetter });
			}

			// Token: 0x06001031 RID: 4145
			public abstract void Process();

			// Token: 0x06001032 RID: 4146
			public abstract VBuffer<DvText> GetKeyValuesMetadata();

			// Token: 0x06001033 RID: 4147 RVA: 0x00059870 File Offset: 0x00057A70
			private IEqualityComparer<T> GetSimpleComparer<T>()
			{
				if (typeof(T) == typeof(DvText))
				{
					HashTransform.InvertHashHelper.TextEqualityComparer textEqualityComparer = new HashTransform.InvertHashHelper.TextEqualityComparer(~this._ex.HashSeed);
					return textEqualityComparer as IEqualityComparer<T>;
				}
				return EqualityComparer<T>.Default;
			}

			// Token: 0x040008F5 RID: 2293
			protected readonly IRow _row;

			// Token: 0x040008F6 RID: 2294
			private readonly bool _includeSlot;

			// Token: 0x040008F7 RID: 2295
			private readonly OneToOneTransformBase.ColInfo _info;

			// Token: 0x040008F8 RID: 2296
			private readonly HashTransform.ColInfoEx _ex;

			// Token: 0x020002BF RID: 703
			private sealed class TextEqualityComparer : IEqualityComparer<DvText>
			{
				// Token: 0x06001034 RID: 4148 RVA: 0x000598B6 File Offset: 0x00057AB6
				public TextEqualityComparer(uint seed)
				{
					this._seed = seed;
				}

				// Token: 0x06001035 RID: 4149 RVA: 0x000598C5 File Offset: 0x00057AC5
				public bool Equals(DvText x, DvText y)
				{
					return x.Equals(y);
				}

				// Token: 0x06001036 RID: 4150 RVA: 0x000598D0 File Offset: 0x00057AD0
				public int GetHashCode(DvText obj)
				{
					if (!obj.HasChars)
					{
						return 0;
					}
					return (int)(obj.Trim().Hash(this._seed) + 1U);
				}

				// Token: 0x040008F9 RID: 2297
				private readonly uint _seed;
			}

			// Token: 0x020002C0 RID: 704
			private sealed class KeyValueComparer<T> : IEqualityComparer<KeyValuePair<int, T>>
			{
				// Token: 0x06001037 RID: 4151 RVA: 0x000598FF File Offset: 0x00057AFF
				public KeyValueComparer(IEqualityComparer<T> tComparer)
				{
					this._tComparer = tComparer;
				}

				// Token: 0x06001038 RID: 4152 RVA: 0x0005990E File Offset: 0x00057B0E
				public bool Equals(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
				{
					return x.Key == y.Key && this._tComparer.Equals(x.Value, y.Value);
				}

				// Token: 0x06001039 RID: 4153 RVA: 0x0005993B File Offset: 0x00057B3B
				public int GetHashCode(KeyValuePair<int, T> obj)
				{
					return Hashing.CombineHash(obj.Key, this._tComparer.GetHashCode(obj.Value));
				}

				// Token: 0x040008FA RID: 2298
				private readonly IEqualityComparer<T> _tComparer;
			}

			// Token: 0x020002C1 RID: 705
			private abstract class Impl<T> : HashTransform.InvertHashHelper
			{
				// Token: 0x0600103A RID: 4154 RVA: 0x0005995B File Offset: 0x00057B5B
				protected Impl(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex, int invertHashMaxCount)
					: base(row, info, ex)
				{
					this._collector = new InvertHashCollector<T>(1 << ex.HashBits, invertHashMaxCount, this.GetTextMap(), this.GetComparer(), null);
				}

				// Token: 0x0600103B RID: 4155 RVA: 0x0005998B File Offset: 0x00057B8B
				protected virtual ValueMapper<T, StringBuilder> GetTextMap()
				{
					return InvertHashUtils.GetSimpleMapper<T>(this._row.Schema, this._info.Source);
				}

				// Token: 0x0600103C RID: 4156 RVA: 0x000599A8 File Offset: 0x00057BA8
				protected virtual IEqualityComparer<T> GetComparer()
				{
					return base.GetSimpleComparer<T>();
				}

				// Token: 0x0600103D RID: 4157 RVA: 0x000599B0 File Offset: 0x00057BB0
				public override VBuffer<DvText> GetKeyValuesMetadata()
				{
					return this._collector.GetMetadata();
				}

				// Token: 0x040008FB RID: 2299
				protected readonly InvertHashCollector<T> _collector;
			}

			// Token: 0x020002C2 RID: 706
			private sealed class ImplOne<T> : HashTransform.InvertHashHelper.Impl<T>
			{
				// Token: 0x0600103E RID: 4158 RVA: 0x000599BD File Offset: 0x00057BBD
				public ImplOne(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex, int invertHashMaxCount, Delegate dstGetter)
					: base(row, info, ex, invertHashMaxCount)
				{
					this._srcGetter = this._row.GetGetter<T>(this._info.Source);
					this._dstGetter = dstGetter as ValueGetter<uint>;
				}

				// Token: 0x0600103F RID: 4159 RVA: 0x000599F3 File Offset: 0x00057BF3
				public override void Process()
				{
					this._dstGetter.Invoke(ref this._hash);
					this._collector.Add(this._hash, this._srcGetter, ref this._value);
				}

				// Token: 0x040008FC RID: 2300
				private readonly ValueGetter<T> _srcGetter;

				// Token: 0x040008FD RID: 2301
				private readonly ValueGetter<uint> _dstGetter;

				// Token: 0x040008FE RID: 2302
				private T _value;

				// Token: 0x040008FF RID: 2303
				private uint _hash;
			}

			// Token: 0x020002C3 RID: 707
			private sealed class ImplVec<T> : HashTransform.InvertHashHelper.Impl<T>
			{
				// Token: 0x06001040 RID: 4160 RVA: 0x00059A23 File Offset: 0x00057C23
				public ImplVec(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex, int invertHashMaxCount, Delegate dstGetter)
					: base(row, info, ex, invertHashMaxCount)
				{
					this._srcGetter = this._row.GetGetter<VBuffer<T>>(this._info.Source);
					this._dstGetter = dstGetter as ValueGetter<VBuffer<uint>>;
				}

				// Token: 0x06001041 RID: 4161 RVA: 0x00059A5C File Offset: 0x00057C5C
				public override void Process()
				{
					this._srcGetter.Invoke(ref this._value);
					this._dstGetter.Invoke(ref this._hash);
					for (int i = 0; i < this._value.Count; i++)
					{
						this._collector.Add(this._hash.Values[i], this._value.Values[i]);
					}
				}

				// Token: 0x04000900 RID: 2304
				private readonly ValueGetter<VBuffer<T>> _srcGetter;

				// Token: 0x04000901 RID: 2305
				private readonly ValueGetter<VBuffer<uint>> _dstGetter;

				// Token: 0x04000902 RID: 2306
				private VBuffer<T> _value;

				// Token: 0x04000903 RID: 2307
				private VBuffer<uint> _hash;
			}

			// Token: 0x020002C4 RID: 708
			private sealed class ImplVecOrdered<T> : HashTransform.InvertHashHelper.Impl<KeyValuePair<int, T>>
			{
				// Token: 0x06001042 RID: 4162 RVA: 0x00059ACA File Offset: 0x00057CCA
				public ImplVecOrdered(IRow row, OneToOneTransformBase.ColInfo info, HashTransform.ColInfoEx ex, int invertHashMaxCount, Delegate dstGetter)
					: base(row, info, ex, invertHashMaxCount)
				{
					this._srcGetter = this._row.GetGetter<VBuffer<T>>(this._info.Source);
					this._dstGetter = dstGetter as ValueGetter<VBuffer<uint>>;
				}

				// Token: 0x06001043 RID: 4163 RVA: 0x00059B00 File Offset: 0x00057D00
				protected override ValueMapper<KeyValuePair<int, T>, StringBuilder> GetTextMap()
				{
					ValueMapper<T, StringBuilder> simpleMapper = InvertHashUtils.GetSimpleMapper<T>(this._row.Schema, this._info.Source);
					return InvertHashUtils.GetPairMapper<T>(simpleMapper);
				}

				// Token: 0x06001044 RID: 4164 RVA: 0x00059B2F File Offset: 0x00057D2F
				protected override IEqualityComparer<KeyValuePair<int, T>> GetComparer()
				{
					return new HashTransform.InvertHashHelper.KeyValueComparer<T>(base.GetSimpleComparer<T>());
				}

				// Token: 0x06001045 RID: 4165 RVA: 0x00059B3C File Offset: 0x00057D3C
				public override void Process()
				{
					this._srcGetter.Invoke(ref this._value);
					this._dstGetter.Invoke(ref this._hash);
					if (this._hash.IsDense)
					{
						for (int i = 0; i < this._value.Count; i++)
						{
							this._collector.Add(this._hash.Values[i], new KeyValuePair<int, T>(i, this._value.Values[i]));
						}
						return;
					}
					for (int j = 0; j < this._value.Count; j++)
					{
						this._collector.Add(this._hash.Values[j], new KeyValuePair<int, T>(this._hash.Indices[j], this._value.Values[j]));
					}
				}

				// Token: 0x04000904 RID: 2308
				private readonly ValueGetter<VBuffer<T>> _srcGetter;

				// Token: 0x04000905 RID: 2309
				private readonly ValueGetter<VBuffer<uint>> _dstGetter;

				// Token: 0x04000906 RID: 2310
				private VBuffer<T> _value;

				// Token: 0x04000907 RID: 2311
				private VBuffer<uint> _hash;
			}
		}
	}
}
