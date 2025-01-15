using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200020B RID: 523
	public sealed class NgramHashTransform : RowToRowTransformBase
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0003EE96 File Offset: 0x0003D096
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("HASHGRAM", 65538U, 65538U, 65538U, "NgramHashTransform", null);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		public NgramHashTransform(NgramHashTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "NgramHash", input)
		{
			Contracts.CheckValue<NgramHashTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<NgramHashTransform.Column>(args.column) > 0, "column");
			this._bindings = new NgramHashTransform.Bindings(args, this._input.Schema, this);
			this._exes = new NgramHashTransform.ColInfoEx[args.column.Length];
			List<int> list = null;
			int[] array = new int[args.column.Length];
			for (int j = 0; j < this._exes.Length; j++)
			{
				this._exes[j] = new NgramHashTransform.ColInfoEx(args.column[j], args);
				int andVerifyInvertHashMaxCount = NgramHashTransform.GetAndVerifyInvertHashMaxCount(args, args.column[j], this._exes[j]);
				if (andVerifyInvertHashMaxCount > 0)
				{
					Utils.Add<int>(ref list, j);
					array[j] = andVerifyInvertHashMaxCount;
				}
			}
			this.InitColumnTypes();
			if (Utils.Size<int>(list) > 0)
			{
				HashSet<int> hashSet = new HashSet<int>(list.Select((int i) => this._bindings.MapIinfoToCol(i)));
				Func<int, bool> dependencies = this._bindings.GetDependencies(new Func<int, bool>(hashSet.Contains));
				bool[] active = this._bindings.GetActive(new Func<int, bool>(hashSet.Contains));
				string[][] array2 = args.column.Select((NgramHashTransform.Column c) => c.friendlyNames).ToArray<string[]>();
				NgramHashTransform.InvertHashHelper invertHashHelper = new NgramHashTransform.InvertHashHelper(this, array2, dependencies, array);
				using (IRowCursor rowCursor = input.GetRowCursor(dependencies, null))
				{
					using (NgramHashTransform.RowCursor rowCursor2 = new NgramHashTransform.RowCursor(this, rowCursor, active, new NgramHashTransform.FinderDecorator(invertHashHelper.Decorate)))
					{
						Action action = NgramHashTransform.InvertHashHelper.CallAllGetters(rowCursor2);
						while (rowCursor2.MoveNext())
						{
							action();
						}
					}
				}
				this._slotNames = invertHashHelper.SlotNamesMetadata(out this._slotNamesTypes);
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0003F0D4 File Offset: 0x0003D2D4
		private NgramHashTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = new NgramHashTransform.Bindings(ctx, this._input.Schema, this);
			this._exes = new NgramHashTransform.ColInfoEx[this._bindings.Infos.Length];
			for (int i = 0; i < this._bindings.Infos.Length; i++)
			{
				this._exes[i] = new NgramHashTransform.ColInfoEx(ctx);
			}
			this.InitColumnTypes();
			TextModelHelper.LoadAll(this._host, ctx, this._exes.Length, out this._slotNames, out this._slotNamesTypes);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		public static NgramHashTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("NgramHash");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NgramHashTransform.GetVersionInfo());
			return HostExtensions.Apply<NgramHashTransform>(h, "Loading Model", (IChannel ch) => new NgramHashTransform(ctx, h, input));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003F23C File Offset: 0x0003D43C
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NgramHashTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
			TextModelHelper.SaveAll(this._host, ctx, this._exes.Length, this._slotNames);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0003F2C0 File Offset: 0x0003D4C0
		private void InitColumnTypes()
		{
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._bindings.Types[i] = new VectorType(NumberType.Float, 1 << this._exes[i].HashBits);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003F30C File Offset: 0x0003D50C
		private static int GetAndVerifyInvertHashMaxCount(NgramHashTransform.Arguments args, NgramHashTransform.Column col, NgramHashTransform.ColInfoEx ex)
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

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0003F38A File Offset: 0x0003D58A
		private void GetTerms(int iinfo, ref VBuffer<DvText> dst)
		{
			this._slotNames[iinfo].CopyTo(ref dst);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		private NgramIdFinder GetNgramIdFinder(int iinfo)
		{
			NgramIdFinder ngramIdFinder = null;
			NgramIdFinder ngramIdFinder2 = null;
			NgramIdFinder ngramIdFinder3 = null;
			uint mask = (1U << this._exes[iinfo].HashBits) - 1U;
			int ngramLength = this._exes[iinfo].NgramLength;
			bool rehash = this._exes[iinfo].Rehash;
			bool ordered = this._exes[iinfo].Ordered;
			bool allLengths = this._exes[iinfo].AllLengths;
			uint seed = this._exes[iinfo].Seed;
			if (!allLengths && ngramLength > 1)
			{
				if (ordered)
				{
					return delegate(uint[] ngram, int lim, int icol, ref bool more)
					{
						if (lim < ngramLength)
						{
							return -1;
						}
						uint num = Hashing.MurmurHash(seed, ngram, 0, lim);
						if (icol > 0)
						{
							num = Hashing.MurmurRound(num, (uint)icol);
						}
						return (int)(Hashing.MixHash(num) & mask);
					};
				}
				return delegate(uint[] ngram, int lim, int icol, ref bool more)
				{
					if (lim < ngramLength)
					{
						return -1;
					}
					return (int)(Hashing.MurmurHash(seed, ngram, 0, lim) & mask);
				};
			}
			else if (rehash)
			{
				if (ordered)
				{
					if (ngramIdFinder == null)
					{
						ngramIdFinder = delegate(uint[] ngram, int lim, int icol, ref bool more)
						{
							uint num2 = Hashing.MurmurHash(seed, ngram, 0, lim);
							if (icol > 0)
							{
								num2 = Hashing.MurmurRound(num2, (uint)icol);
							}
							return (int)(Hashing.MixHash(num2) & mask);
						};
					}
					return ngramIdFinder;
				}
				return delegate(uint[] ngram, int lim, int icol, ref bool more)
				{
					return (int)(Hashing.MurmurHash(seed, ngram, 0, lim) & mask);
				};
			}
			else if (ngramLength > 1)
			{
				if (ordered)
				{
					if (ngramIdFinder2 == null)
					{
						ngramIdFinder2 = delegate(uint[] ngram, int lim, int icol, ref bool more)
						{
							uint num3;
							if (lim == 1)
							{
								num3 = ngram[0];
							}
							else
							{
								num3 = Hashing.MurmurHash(seed, ngram, 0, lim);
							}
							if (icol > 0)
							{
								num3 = Hashing.MurmurRound(num3, (uint)icol);
							}
							return (int)(Hashing.MixHash(num3) & mask);
						};
					}
					return ngramIdFinder2;
				}
				return delegate(uint[] ngram, int lim, int icol, ref bool more)
				{
					if (lim == 1)
					{
						return (int)(ngram[0] & mask);
					}
					return (int)(Hashing.MurmurHash(seed, ngram, 0, lim) & mask);
				};
			}
			else
			{
				if (ordered)
				{
					if (ngramIdFinder3 == null)
					{
						ngramIdFinder3 = delegate(uint[] ngram, int lim, int icol, ref bool more)
						{
							uint num4 = ngram[0];
							if (icol > 0)
							{
								num4 = Hashing.MurmurRound(num4, (uint)icol);
							}
							return (int)(Hashing.MixHash(num4) & mask);
						};
					}
					return ngramIdFinder3;
				}
				return delegate(uint[] ngram, int lim, int icol, ref bool more)
				{
					return (int)(ngram[0] & mask);
				};
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003F662 File Offset: 0x0003D862
		[Conditional("DEBUG")]
		private void AssertValid(uint[] ngram, int ngramLength, int lim, int icol)
		{
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0003F664 File Offset: 0x0003D864
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003F66C File Offset: 0x0003D86C
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this._bindings.AnyNewColumnsActive(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0003F698 File Offset: 0x0003D898
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new NgramHashTransform.RowCursor(this, rowCursor, active, null);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			if (array.Length == 1 && n > 1 && this._bindings.AnyNewColumnsActive(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new NgramHashTransform.RowCursor(this, array[i], active, null);
			}
			return array2;
		}

		// Token: 0x04000651 RID: 1617
		internal const string Summary = "Produces a bag of counts of ngrams (sequences of consecutive values of length 1-n) in a given vector of keys. It does so by hashing each ngram and using the hash value as the index in the bag.";

		// Token: 0x04000652 RID: 1618
		public const string LoaderSignature = "NgramHashTransform";

		// Token: 0x04000653 RID: 1619
		private const string RegistrationName = "NgramHash";

		// Token: 0x04000654 RID: 1620
		private readonly NgramHashTransform.Bindings _bindings;

		// Token: 0x04000655 RID: 1621
		private readonly NgramHashTransform.ColInfoEx[] _exes;

		// Token: 0x04000656 RID: 1622
		private readonly VBuffer<DvText>[] _slotNames;

		// Token: 0x04000657 RID: 1623
		private readonly ColumnType[] _slotNamesTypes;

		// Token: 0x0200020C RID: 524
		public sealed class Column : ManyToOneColumn
		{
			// Token: 0x06000BAD RID: 2989 RVA: 0x0003F778 File Offset: 0x0003D978
			public static NgramHashTransform.Column Parse(string str)
			{
				NgramHashTransform.Column column = new NgramHashTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000BAE RID: 2990 RVA: 0x0003F798 File Offset: 0x0003D998
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

			// Token: 0x06000BAF RID: 2991 RVA: 0x0003F7D0 File Offset: 0x0003D9D0
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.ngramLength != null || this.allLengths != null || this.skipLength != null || this.seed != null || this.rehashUnigrams != null || this.ordered != null || this.invertHash != null)
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

			// Token: 0x04000659 RID: 1625
			[Argument(0, HelpText = "Maximum ngram length", ShortName = "ngram")]
			public int? ngramLength;

			// Token: 0x0400065A RID: 1626
			[Argument(0, HelpText = "Whether to include all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all")]
			public bool? allLengths;

			// Token: 0x0400065B RID: 1627
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips")]
			public int? skipLength;

			// Token: 0x0400065C RID: 1628
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits")]
			public int? hashBits;

			// Token: 0x0400065D RID: 1629
			[Argument(0, HelpText = "Hashing seed")]
			public uint? seed;

			// Token: 0x0400065E RID: 1630
			[Argument(0, HelpText = "Whether to rehash unigrams", ShortName = "rehash")]
			public bool? rehashUnigrams;

			// Token: 0x0400065F RID: 1631
			[Argument(0, HelpText = "Whether the position of each source column should be included in the hash (when there are multiple source columns).", ShortName = "ord")]
			public bool? ordered;

			// Token: 0x04000660 RID: 1632
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int? invertHash;

			// Token: 0x04000661 RID: 1633
			internal string[] friendlyNames;
		}

		// Token: 0x0200020D RID: 525
		public sealed class Arguments
		{
			// Token: 0x04000662 RID: 1634
			[Argument(4, HelpText = "New column definition(s) (optional form: name:hashBits:src)", ShortName = "col", SortOrder = 1)]
			public NgramHashTransform.Column[] column;

			// Token: 0x04000663 RID: 1635
			[Argument(0, HelpText = "Maximum ngram length", ShortName = "ngram", SortOrder = 3)]
			public int ngramLength = 2;

			// Token: 0x04000664 RID: 1636
			[Argument(0, HelpText = "Whether to include all ngram lengths up to ngramLength, or only ngramLength", ShortName = "all", SortOrder = 4)]
			public bool allLengths = true;

			// Token: 0x04000665 RID: 1637
			[Argument(0, HelpText = "Maximum number of tokens to skip when constructing an ngram", ShortName = "skips", SortOrder = 3)]
			public int skipLength;

			// Token: 0x04000666 RID: 1638
			[Argument(0, HelpText = "Number of bits to hash into. Must be between 1 and 30, inclusive.", ShortName = "bits", SortOrder = 2)]
			public int hashBits = 16;

			// Token: 0x04000667 RID: 1639
			[Argument(0, HelpText = "Hashing seed")]
			public uint seed = 314489979U;

			// Token: 0x04000668 RID: 1640
			[Argument(0, HelpText = "Whether to rehash unigrams", ShortName = "rehash")]
			public bool rehashUnigrams;

			// Token: 0x04000669 RID: 1641
			[Argument(0, HelpText = "Whether the position of each source column should be included in the hash (when there are multiple source columns).", ShortName = "ord", SortOrder = 6)]
			public bool ordered = true;

			// Token: 0x0400066A RID: 1642
			[Argument(0, HelpText = "Limit the number of keys used to generate the slot name to this many. 0 means no invert hashing, -1 means no limit.", ShortName = "ih")]
			public int invertHash;
		}

		// Token: 0x0200020E RID: 526
		private sealed class Bindings : ManyToOneColumnBindingsBase
		{
			// Token: 0x06000BB2 RID: 2994 RVA: 0x0003F8A3 File Offset: 0x0003DAA3
			public Bindings(NgramHashTransform.Arguments args, ISchema schemaInput, NgramHashTransform parent)
				: base(args.column, schemaInput, new Func<ColumnType[], string>(NgramHashTransform.Bindings.TestTypes))
			{
				this.Types = new VectorType[args.column.Length];
				this._parent = parent;
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x0003F8D8 File Offset: 0x0003DAD8
			public Bindings(ModelLoadContext ctx, ISchema schemaInput, NgramHashTransform parent)
				: base(ctx, schemaInput, new Func<ColumnType[], string>(NgramHashTransform.Bindings.TestTypes))
			{
				this.Types = new VectorType[this.Infos.Length];
				this._parent = parent;
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x0003F908 File Offset: 0x0003DB08
			private static string TestTypes(ColumnType[] types)
			{
				foreach (ColumnType columnType in types)
				{
					if (!columnType.IsVector)
					{
						return "Expected vector of Key type, and Key is convertable to U4";
					}
					if (!columnType.ItemType.IsKey)
					{
						return "Expected vector of Key type, and Key is convertable to U4";
					}
					if (columnType.ItemType.KeyCount == 0 && columnType.ItemType.RawKind > 6)
					{
						return "Expected vector of Key type, and Key is convertable to U4";
					}
				}
				return null;
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x0003F96A File Offset: 0x0003DB6A
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this.Types[iinfo];
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0003F974 File Offset: 0x0003DB74
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				if (kind == "SlotNames" && this._parent._slotNamesTypes != null)
				{
					return this._parent._slotNamesTypes[iinfo];
				}
				return base.GetMetadataTypeCore(kind, iinfo);
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x0003F9A8 File Offset: 0x0003DBA8
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				if (this._parent._slotNamesTypes != null && this._parent._slotNamesTypes[iinfo] != null)
				{
					return MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(base.GetMetadataTypesCore(iinfo), new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(this._parent._slotNamesTypes[iinfo], "SlotNames") });
				}
				return base.GetMetadataTypesCore(iinfo);
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x0003FA10 File Offset: 0x0003DC10
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				if (kind == "SlotNames" && this._parent._slotNames != null && this._parent._slotNames[iinfo].Length > 0)
				{
					MetadataUtils.MetadataGetter<VBuffer<DvText>> metadataGetter = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this._parent.GetTerms);
					MetadataUtils.Marshal<VBuffer<DvText>, TValue>(metadataGetter, iinfo, ref value);
					return;
				}
				base.GetMetadataCore<TValue>(kind, iinfo, ref value);
			}

			// Token: 0x0400066B RID: 1643
			public readonly VectorType[] Types;

			// Token: 0x0400066C RID: 1644
			private readonly NgramHashTransform _parent;
		}

		// Token: 0x0200020F RID: 527
		private sealed class ColInfoEx
		{
			// Token: 0x06000BB9 RID: 3001 RVA: 0x0003FA7C File Offset: 0x0003DC7C
			public ColInfoEx(NgramHashTransform.Column item, NgramHashTransform.Arguments args)
			{
				this.NgramLength = item.ngramLength ?? args.ngramLength;
				Contracts.CheckUserArg(0 < this.NgramLength && this.NgramLength <= 10, "ngram");
				this.SkipLength = item.skipLength ?? args.skipLength;
				Contracts.CheckUserArg(0 <= this.SkipLength && this.SkipLength <= 10, "skips");
				if (this.NgramLength + this.SkipLength > 10)
				{
					throw Contracts.ExceptUserArg("skips", "The sum of skipLength and ngramLength must be less than or equal to {0}", new object[] { 10 });
				}
				this.HashBits = item.hashBits ?? args.hashBits;
				Contracts.CheckUserArg(1 <= this.HashBits && this.HashBits <= 30, "hashBits");
				this.Seed = item.seed ?? args.seed;
				this.Rehash = item.rehashUnigrams ?? args.rehashUnigrams;
				this.Ordered = item.ordered ?? args.ordered;
				this.AllLengths = item.allLengths ?? args.allLengths;
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0003FC34 File Offset: 0x0003DE34
			public ColInfoEx(ModelLoadContext ctx)
			{
				this.NgramLength = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(0 < this.NgramLength && this.NgramLength <= 10);
				this.SkipLength = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(0 <= this.SkipLength && this.SkipLength <= 10);
				if (this.NgramLength + this.SkipLength > 10)
				{
					throw Contracts.ExceptDecode("The sum of skipLength and ngramLength must be less than or equal to {0}", new object[] { 10 });
				}
				this.HashBits = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(1 <= this.HashBits && this.HashBits <= 30);
				this.Seed = ctx.Reader.ReadUInt32();
				this.Rehash = Utils.ReadBoolByte(ctx.Reader);
				this.Ordered = Utils.ReadBoolByte(ctx.Reader);
				this.AllLengths = Utils.ReadBoolByte(ctx.Reader);
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x0003FD48 File Offset: 0x0003DF48
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write(this.NgramLength);
				ctx.Writer.Write(this.SkipLength);
				ctx.Writer.Write(this.HashBits);
				ctx.Writer.Write(this.Seed);
				Utils.WriteBoolByte(ctx.Writer, this.Rehash);
				Utils.WriteBoolByte(ctx.Writer, this.Ordered);
				Utils.WriteBoolByte(ctx.Writer, this.AllLengths);
			}

			// Token: 0x0400066D RID: 1645
			public readonly int NgramLength;

			// Token: 0x0400066E RID: 1646
			public readonly int SkipLength;

			// Token: 0x0400066F RID: 1647
			public readonly int HashBits;

			// Token: 0x04000670 RID: 1648
			public readonly uint Seed;

			// Token: 0x04000671 RID: 1649
			public readonly bool Rehash;

			// Token: 0x04000672 RID: 1650
			public readonly bool Ordered;

			// Token: 0x04000673 RID: 1651
			public readonly bool AllLengths;
		}

		// Token: 0x02000210 RID: 528
		// (Invoke) Token: 0x06000BBD RID: 3005
		private delegate NgramIdFinder FinderDecorator(int iinfo, NgramIdFinder finder);

		// Token: 0x02000211 RID: 529
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0003FDCC File Offset: 0x0003DFCC
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
			public RowCursor(NgramHashTransform parent, IRowCursor input, bool[] active, NgramHashTransform.FinderDecorator decorator = null)
				: base(parent._host, input)
			{
				this._bindings = parent._bindings;
				this._active = active;
				this._getters = new Delegate[this._bindings.Infos.Length];
				for (int i = 0; i < this._bindings.Infos.Length; i++)
				{
					if (this.IsIndexActive(i))
					{
						this._getters[i] = this.MakeGetter(i, parent, decorator);
					}
				}
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x0003FED8 File Offset: 0x0003E0D8
			private Delegate MakeGetter(int iinfo, NgramHashTransform parent, NgramHashTransform.FinderDecorator decorator)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				int srcCount = colInfo.SrcIndices.Length;
				ValueGetter<VBuffer<uint>>[] getSrc = new ValueGetter<VBuffer<uint>>[srcCount];
				for (int i = 0; i < srcCount; i++)
				{
					getSrc[i] = RowCursorUtils.GetVecGetterAs<uint>(NumberType.U4, base.Input, colInfo.SrcIndices[i]);
				}
				VBuffer<uint> src = default(VBuffer<uint>);
				NgramIdFinder ngramIdFinder = parent.GetNgramIdFinder(iinfo);
				if (decorator != null)
				{
					ngramIdFinder = decorator(iinfo, ngramIdFinder);
				}
				NgramBufferBuilder bldr = new NgramBufferBuilder(parent._exes[iinfo].NgramLength, parent._exes[iinfo].SkipLength, this._bindings.Types[iinfo].ValueCount, ngramIdFinder);
				uint[] keyCounts = parent._bindings.Infos[iinfo].SrcTypes.Select(delegate(ColumnType t)
				{
					if (t.ItemType.KeyCount <= 0)
					{
						return uint.MaxValue;
					}
					return (uint)t.ItemType.KeyCount;
				}).ToArray<uint>();
				return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
				{
					bldr.Reset();
					for (int j = 0; j < srcCount; j++)
					{
						getSrc[j].Invoke(ref src);
						bldr.AddNgrams(ref src, j, keyCounts[j]);
					}
					bldr.GetResult(ref dst);
				});
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x0003FFF8 File Offset: 0x0003E1F8
			private bool IsIndexActive(int iinfo)
			{
				return this._active == null || this._active[this._bindings.MapIinfoToCol(iinfo)];
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x00040017 File Offset: 0x0003E217
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x0004004C File Offset: 0x0003E24C
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				ValueGetter<TValue> valueGetter = this._getters[num] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x000400C2 File Offset: 0x0003E2C2
			private ValueGetter<T> GetSrcGetter<T>(int iinfo, int isrc)
			{
				return base.Input.GetGetter<T>(this._bindings.Infos[iinfo].SrcIndices[isrc]);
			}

			// Token: 0x04000674 RID: 1652
			private readonly NgramHashTransform.Bindings _bindings;

			// Token: 0x04000675 RID: 1653
			private readonly bool[] _active;

			// Token: 0x04000676 RID: 1654
			private readonly Delegate[] _getters;
		}

		// Token: 0x02000212 RID: 530
		private sealed class InvertHashHelper
		{
			// Token: 0x06000BC8 RID: 3016 RVA: 0x000400E4 File Offset: 0x0003E2E4
			public InvertHashHelper(NgramHashTransform parent, string[][] friendlyNames, Func<int, bool> inputPred, int[] invertHashMaxCounts)
			{
				this._parent = parent;
				this._iinfoToCollector = new InvertHashCollector<NgramHashTransform.InvertHashHelper.NGram>[this._parent._bindings.InfoCount];
				this._srcTextGetters = new ValueMapper<uint, StringBuilder>[this._parent.Source.Schema.ColumnCount];
				this._invertHashMaxCounts = invertHashMaxCounts;
				for (int i = 0; i < this._srcTextGetters.Length; i++)
				{
					if (inputPred(i))
					{
						this._srcTextGetters[i] = InvertHashUtils.GetSimpleMapper<uint>(this._parent.Source.Schema, i);
					}
				}
				this._friendlyNames = friendlyNames;
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x000401BC File Offset: 0x0003E3BC
			public static Action CallAllGetters(IRow row)
			{
				int columnCount = row.Schema.ColumnCount;
				List<Action> list = new List<Action>();
				for (int i = 0; i < columnCount; i++)
				{
					if (row.IsColumnActive(i))
					{
						list.Add(NgramHashTransform.InvertHashHelper.GetNoOpGetter(row, i));
					}
				}
				Action[] gettersArray = list.ToArray();
				return delegate
				{
					for (int j = 0; j < gettersArray.Length; j++)
					{
						gettersArray[j]();
					}
				};
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x0004021C File Offset: 0x0003E41C
			private static Action GetNoOpGetter(IRow row, int col)
			{
				Func<IRow, int, Action> func = new Func<IRow, int, Action>(NgramHashTransform.InvertHashHelper.GetNoOpGetter<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { row.Schema.GetColumnType(col).RawType });
				return (Action)methodInfo.Invoke(null, new object[] { row, col });
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x000402A0 File Offset: 0x0003E4A0
			private static Action GetNoOpGetter<T>(IRow row, int col)
			{
				T value = default(T);
				ValueGetter<T> getter = row.GetGetter<T>(col);
				return delegate
				{
					getter.Invoke(ref value);
				};
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x000402D8 File Offset: 0x0003E4D8
			private static void ClearDst(ref StringBuilder dst)
			{
				if (dst == null)
				{
					dst = new StringBuilder();
					return;
				}
				dst.Clear();
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x000404D8 File Offset: 0x0003E6D8
			public NgramIdFinder Decorate(int iinfo, NgramIdFinder finder)
			{
				int[] srcIndices = this._parent._bindings.Infos[iinfo].SrcIndices;
				StringBuilder temp = null;
				char[] buffer = null;
				ValueMapper<NgramHashTransform.InvertHashHelper.NGram, StringBuilder> valueMapper;
				if (srcIndices.Length == 1)
				{
					ValueMapper<uint, StringBuilder> srcMap = this._srcTextGetters[srcIndices[0]];
					valueMapper = delegate(ref NgramHashTransform.InvertHashHelper.NGram src, ref StringBuilder dst)
					{
						if (src.Lim == 1)
						{
							srcMap.Invoke(ref src.Grams[0], ref dst);
							return;
						}
						NgramHashTransform.InvertHashHelper.ClearDst(ref dst);
						dst.Append('(');
						for (int j = 0; j < src.Lim; j++)
						{
							if (j > 0)
							{
								dst.Append(',');
							}
							srcMap.Invoke(ref src.Grams[j], ref temp);
							InvertHashUtils.AppendToEnd(temp, dst, ref buffer);
						}
						dst.Append(')');
					};
				}
				else
				{
					string[] srcNames = this._friendlyNames[iinfo];
					if (srcNames == null)
					{
						srcNames = new string[srcIndices.Length];
						for (int i = 0; i < srcIndices.Length; i++)
						{
							srcNames[i] = this._parent.Source.Schema.GetColumnName(srcIndices[i]);
						}
					}
					if (this._friendlyNames != null)
					{
						string[] array = this._friendlyNames[iinfo];
					}
					valueMapper = delegate(ref NgramHashTransform.InvertHashHelper.NGram src, ref StringBuilder dst)
					{
						ValueMapper<uint, StringBuilder> valueMapper2 = this._srcTextGetters[srcIndices[src.ISrcCol]];
						NgramHashTransform.InvertHashHelper.ClearDst(ref dst);
						dst.Append(srcNames[src.ISrcCol]);
						dst.Append(':');
						if (src.Lim > 1)
						{
							dst.Append('(');
						}
						for (int k = 0; k < src.Lim; k++)
						{
							if (k > 0)
							{
								dst.Append(',');
							}
							valueMapper2.Invoke(ref src.Grams[k], ref temp);
							InvertHashUtils.AppendToEnd(temp, dst, ref buffer);
						}
						if (src.Lim > 1)
						{
							dst.Append(')');
						}
					};
				}
				InvertHashCollector<NgramHashTransform.InvertHashHelper.NGram> collector = (this._iinfoToCollector[iinfo] = new InvertHashCollector<NgramHashTransform.InvertHashHelper.NGram>(this._parent._bindings.Types[iinfo].VectorSize, this._invertHashMaxCounts[iinfo], valueMapper, EqualityComparer<NgramHashTransform.InvertHashHelper.NGram>.Default, delegate(ref NgramHashTransform.InvertHashHelper.NGram src, ref NgramHashTransform.InvertHashHelper.NGram dst)
				{
					dst = src.Clone();
				}));
				return delegate(uint[] ngram, int lim, int icol, ref bool more)
				{
					int num = finder(ngram, lim, icol, ref more);
					if (num != -1)
					{
						NgramHashTransform.InvertHashHelper.NGram ngram2 = new NgramHashTransform.InvertHashHelper.NGram(ngram, lim, icol);
						collector.Add(num, ngram2);
					}
					return num;
				};
			}

			// Token: 0x06000BCE RID: 3022 RVA: 0x00040664 File Offset: 0x0003E864
			public VBuffer<DvText>[] SlotNamesMetadata(out ColumnType[] types)
			{
				VBuffer<DvText>[] array = new VBuffer<DvText>[this._iinfoToCollector.Length];
				types = new ColumnType[this._iinfoToCollector.Length];
				for (int i = 0; i < this._iinfoToCollector.Length; i++)
				{
					if (this._iinfoToCollector[i] != null)
					{
						VBuffer<DvText> vbuffer = (array[i] = this._iinfoToCollector[i].GetMetadata());
						types[i] = new VectorType(TextType.Instance, vbuffer.Length);
					}
				}
				return array;
			}

			// Token: 0x04000678 RID: 1656
			private readonly NgramHashTransform _parent;

			// Token: 0x04000679 RID: 1657
			private readonly InvertHashCollector<NgramHashTransform.InvertHashHelper.NGram>[] _iinfoToCollector;

			// Token: 0x0400067A RID: 1658
			private readonly ValueMapper<uint, StringBuilder>[] _srcTextGetters;

			// Token: 0x0400067B RID: 1659
			private readonly string[][] _friendlyNames;

			// Token: 0x0400067C RID: 1660
			private readonly int[] _invertHashMaxCounts;

			// Token: 0x02000213 RID: 531
			private sealed class NGram : IEquatable<NgramHashTransform.InvertHashHelper.NGram>
			{
				// Token: 0x06000BD0 RID: 3024 RVA: 0x000406DE File Offset: 0x0003E8DE
				public NGram(uint[] ngram, int lim, int srcCol)
				{
					this.Grams = ngram;
					this.Lim = lim;
					this.ISrcCol = srcCol;
				}

				// Token: 0x06000BD1 RID: 3025 RVA: 0x000406FC File Offset: 0x0003E8FC
				public NgramHashTransform.InvertHashHelper.NGram Clone()
				{
					uint[] array = new uint[this.Lim];
					Array.Copy(this.Grams, array, this.Lim);
					return new NgramHashTransform.InvertHashHelper.NGram(array, this.Lim, this.ISrcCol);
				}

				// Token: 0x06000BD2 RID: 3026 RVA: 0x0004073C File Offset: 0x0003E93C
				public bool Equals(NgramHashTransform.InvertHashHelper.NGram other)
				{
					if (other != null && other.Lim == this.Lim && other.ISrcCol == this.ISrcCol)
					{
						for (int i = 0; i < this.Lim; i++)
						{
							if (other.Grams[i] != this.Grams[i])
							{
								return false;
							}
						}
						return true;
					}
					return false;
				}

				// Token: 0x06000BD3 RID: 3027 RVA: 0x00040790 File Offset: 0x0003E990
				public override int GetHashCode()
				{
					int num = this.Lim;
					num = Hashing.CombineHash(num, this.ISrcCol);
					for (int i = 0; i < this.Lim; i++)
					{
						num = Hashing.CombineHash(num, (int)this.Grams[i]);
					}
					return num;
				}

				// Token: 0x0400067E RID: 1662
				public readonly uint[] Grams;

				// Token: 0x0400067F RID: 1663
				public readonly int Lim;

				// Token: 0x04000680 RID: 1664
				public readonly int ISrcCol;
			}
		}
	}
}
