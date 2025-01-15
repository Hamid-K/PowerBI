using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000148 RID: 328
	public sealed class TermTransform : OneToOneTransformBase, ITransformTemplate, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x000231AC File Offset: 0x000213AC
		private static void GetTextTerms<T>(ref VBuffer<T> src, ValueMapper<T, StringBuilder> stringMapper, ref VBuffer<DvText> dst)
		{
			StringBuilder stringBuilder = null;
			DvText[] array = dst.Values;
			if (Utils.Size<DvText>(array) < src.Length)
			{
				array = new DvText[src.Length];
			}
			for (int i = 0; i < src.Length; i++)
			{
				stringMapper.Invoke(ref src.Values[i], ref stringBuilder);
				array[i] = new DvText(stringBuilder.ToString());
			}
			dst = new VBuffer<DvText>(src.Length, array, dst.Indices);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00023230 File Offset: 0x00021430
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("TERMTRNF", 65539U, 65539U, 65537U, "TermTransform", null);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00023251 File Offset: 0x00021451
		private static VersionInfo GetTermManagerVersionInfo()
		{
			return new VersionInfo("TERM MAN", 65538U, 65538U, 65537U, "TermManager", null);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00023274 File Offset: 0x00021474
		private CodecFactory CodecFactory
		{
			get
			{
				if (this._codecFactory == null)
				{
					Interlocked.CompareExchange<MemoryStreamPool>(ref TermTransform._codecFactoryPool, new MemoryStreamPool(), null);
					Interlocked.CompareExchange<CodecFactory>(ref this._codecFactory, new CodecFactory(this._host, TermTransform._codecFactoryPool), null);
				}
				return this._codecFactory;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000232C3 File Offset: 0x000214C3
		public TermTransform(TermTransform.Arguments args, IHostEnvironment env, IDataView input)
			: this(args, Contracts.CheckRef<TermTransform.Arguments>(args, "args").column, env, input)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000232E0 File Offset: 0x000214E0
		private TermTransform(IHostEnvironment env, TermTransform transform, IDataView newSource)
			: base(env, "Term", transform, newSource, new Func<ColumnType, string>(TermTransform.TestIsKnownDataKind))
		{
			this._textMetadata = transform._textMetadata;
			this._termMap = new TermTransform.BoundTermMap[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				TermTransform.TermMap map = transform._termMap[i].Map;
				if (!map.ItemType.Equals(this.Infos[i].TypeSrc.ItemType))
				{
					throw Contracts.Except(this._host, "For column '{0}', term map was trained on items of type '{1}' but being applied to type '{2}'", new object[]
					{
						this.Infos[i].Name,
						map.ItemType,
						this.Infos[i].TypeSrc.ItemType
					});
				}
				this._termMap[i] = map.Bind(this, i);
			}
			this._types = this.ComputeTypesAndMetadata();
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000233CF File Offset: 0x000215CF
		public IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			return new TermTransform(env, this, newSource);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000233DC File Offset: 0x000215DC
		public TermTransform(TermTransform.ArgumentsBase args, TermTransform.ColumnBase[] column, IHostEnvironment env, IDataView input)
			: base(env, "Term", column, input, new Func<ColumnType, string>(TermTransform.TestIsKnownDataKind))
		{
			Contracts.CheckValue<TermTransform.ArgumentsBase>(this._host, args, "args");
			using (IChannel channel = this._host.Start("Training"))
			{
				TermTransform.TermMap[] array = TermTransform.Train(this._host, channel, this.Infos, args, column, this._input);
				this._textMetadata = new bool[array.Length];
				this._termMap = new TermTransform.BoundTermMap[array.Length];
				for (int i = 0; i < this.Infos.Length; i++)
				{
					this._textMetadata[i] = column[i].textKeyValues ?? args.textKeyValues;
					this._termMap[i] = array[i].Bind(this, i);
				}
				this._types = this.ComputeTypesAndMetadata();
				channel.Done();
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000234DC File Offset: 0x000216DC
		private static string TestIsKnownDataKind(ColumnType type)
		{
			if (type.ItemType.RawKind != null && (type.IsVector || type.IsPrimitive))
			{
				return null;
			}
			return "Expected standard type or a vector of standard type";
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00023548 File Offset: 0x00021748
		private static TermTransform.TermMap CreateFileTermMap(IHostEnvironment env, IChannel ch, TermTransform.ArgumentsBase args, TermTransform.Builder bldr)
		{
			TermTransform.<>c__DisplayClass3 CS$<>8__locals1 = new TermTransform.<>c__DisplayClass3();
			string dataFile = args.dataFile;
			string text = args.termsColumn;
			SubComponent<IDataLoader, SignatureDataLoader> subComponent = args.loader;
			bool flag = false;
			if (!SubComponentExtensions.IsGood(subComponent))
			{
				string extension = Path.GetExtension(dataFile);
				bool flag2 = string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase);
				bool flag3 = string.Equals(extension, ".tdv", StringComparison.OrdinalIgnoreCase);
				if (flag2 || flag3)
				{
					Contracts.CheckUserArg(ch, !string.IsNullOrWhiteSpace(text), "termsColumn", "termsColumn should be specified");
					if (flag2)
					{
						subComponent = new SubComponent<IDataLoader, SignatureDataLoader>("BinaryLoader");
					}
					else
					{
						subComponent = new SubComponent<IDataLoader, SignatureDataLoader>("TransposeLoader");
					}
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						ch.Warning("termsColumn should not be specified when default loader is TextLoader. Ignoring termsColumn={0}", new object[] { text });
					}
					subComponent = new SubComponent<IDataLoader, SignatureDataLoader>("TextLoader", new string[] { "sep=tab col=Term:TX:0" });
					text = "Term";
					flag = true;
				}
			}
			IDataLoader dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(subComponent, new object[]
			{
				env,
				new MultiFileSource(dataFile)
			});
			if (!dataLoader.Schema.TryGetColumnIndex(text, ref CS$<>8__locals1.colSrc))
			{
				throw Contracts.ExceptUserArg(ch, "termsColumn", "Unknown column '{0}'", new object[] { text });
			}
			ColumnType columnType = dataLoader.Schema.GetColumnType(CS$<>8__locals1.colSrc);
			if (!flag && !columnType.Equals(bldr.ItemType))
			{
				throw Contracts.ExceptUserArg(ch, "termsColumn", "Must be of type '{0}' but was '{1}'", new object[] { bldr.ItemType, columnType });
			}
			TermTransform.TermMap termMap;
			using (IRowCursor rowCursor = dataLoader.GetRowCursor((int col) => col == CS$<>8__locals1.colSrc, null))
			{
				using (IProgressChannel progressChannel = env.StartProgressChannel("Building term dictionary from file"))
				{
					TermTransform.<>c__DisplayClass5 CS$<>8__locals2 = new TermTransform.<>c__DisplayClass5();
					CS$<>8__locals2.CS$<>8__locals4 = CS$<>8__locals1;
					ProgressHeader progressHeader = new ProgressHeader(new string[] { "Total terms" }, new string[] { "examples" });
					CS$<>8__locals2.trainer = TermTransform.Trainer.Create(rowCursor, CS$<>8__locals1.colSrc, flag, int.MaxValue, bldr);
					TermTransform.<>c__DisplayClass5 CS$<>8__locals3 = CS$<>8__locals2;
					long? rowCount = dataLoader.GetRowCount(true);
					CS$<>8__locals3.rowCount = ((rowCount != null) ? ((double)rowCount.GetValueOrDefault()) : double.NaN);
					CS$<>8__locals2.rowCur = 0L;
					progressChannel.SetHeader(progressHeader, delegate(IProgressEntry e)
					{
						e.SetProgress(0, (double)CS$<>8__locals2.rowCur, CS$<>8__locals2.rowCount);
						e.SetMetric(0, (double)CS$<>8__locals2.trainer.Count);
					});
					while (rowCursor.MoveNext() && CS$<>8__locals2.trainer.ProcessRow())
					{
						CS$<>8__locals2.rowCur += 1L;
					}
					if (CS$<>8__locals2.trainer.Count == 0)
					{
						ch.Warning("Term map loaded from file resulted in an empty map.");
					}
					progressChannel.Checkpoint(new double?[]
					{
						new double?((double)CS$<>8__locals2.trainer.Count),
						new double?((double)CS$<>8__locals2.rowCur)
					});
					termMap = CS$<>8__locals2.trainer.Finish();
				}
			}
			return termMap;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002390C File Offset: 0x00021B0C
		private static TermTransform.TermMap[] Train(IHostEnvironment env, IChannel ch, OneToOneTransformBase.ColInfo[] infos, TermTransform.ArgumentsBase args, TermTransform.ColumnBase[] column, IDataView trainingData)
		{
			if (!string.IsNullOrEmpty(args.terms) && (!string.IsNullOrWhiteSpace(args.dataFile) || SubComponentExtensions.IsGood(args.loader) || !string.IsNullOrWhiteSpace(args.termsColumn)))
			{
				ch.Warning("Explicit term list specified. Data file arguments will be ignored");
			}
			if (!Enum.IsDefined(typeof(TermTransform.SortOrder), args.sort))
			{
				throw Contracts.ExceptUserArg(ch, "sort", "Undefined sorting criteria '{0}' detected", new object[] { args.sort });
			}
			TermTransform.TermMap termMap = null;
			TermTransform.TermMap[] array = new TermTransform.TermMap[infos.Length];
			int[] array2 = new int[infos.Length];
			int num = 0;
			HashSet<int> hashSet = null;
			for (int i = 0; i < infos.Length; i++)
			{
				DvText dvText = new DvText(column[i].terms);
				if (!dvText.HasChars)
				{
					dvText = new DvText(args.terms);
				}
				dvText = dvText.Trim();
				if (dvText.HasChars)
				{
					TermTransform.SortOrder sortOrder = column[i].sort ?? args.sort;
					if (!Enum.IsDefined(typeof(TermTransform.SortOrder), sortOrder))
					{
						throw Contracts.ExceptUserArg(ch, "sort", "Undefined sorting criteria '{0}' detected for column '{1}'", new object[]
						{
							sortOrder,
							infos[i].Name
						});
					}
					TermTransform.Builder builder = TermTransform.Builder.Create(infos[i].TypeSrc, sortOrder);
					builder.ParseAddTermArg(ref dvText, ch);
					array[i] = builder.Finish();
				}
				else if (!string.IsNullOrWhiteSpace(args.dataFile))
				{
					if (termMap == null)
					{
						TermTransform.Builder builder2 = TermTransform.Builder.Create(infos[i].TypeSrc, column[i].sort ?? args.sort);
						termMap = TermTransform.CreateFileTermMap(env, ch, args, builder2);
					}
					if (!termMap.ItemType.Equals(infos[i].TypeSrc.ItemType))
					{
						throw Contracts.ExceptUserArg(ch, "dataFile", "Data file terms loaded as type '{0}' but mismatches column '{1}' item type '{2}'", new object[]
						{
							termMap.ItemType,
							infos[i].Name,
							infos[i].TypeSrc.ItemType
						});
					}
					array[i] = termMap;
				}
				else
				{
					array2[i] = column[i].maxNumTerms ?? args.maxNumTerms;
					Contracts.CheckUserArg(ch, array2[i] > 0, "maxNumTerms", "must be positive");
					Utils.Add<int>(ref hashSet, infos[i].Source);
					num++;
				}
			}
			if (num > 0)
			{
				TermTransform.<>c__DisplayClassb CS$<>8__locals1 = new TermTransform.<>c__DisplayClassb();
				CS$<>8__locals1.trainer = new TermTransform.Trainer[num];
				int[] array3 = new int[num];
				using (IRowCursor rowCursor = trainingData.GetRowCursor(new Func<int, bool>(hashSet.Contains), null))
				{
					using (IProgressChannel progressChannel = env.StartProgressChannel("Building term dictionary"))
					{
						TermTransform.<>c__DisplayClassd CS$<>8__locals2 = new TermTransform.<>c__DisplayClassd();
						CS$<>8__locals2.CS$<>8__localsc = CS$<>8__locals1;
						CS$<>8__locals2.rowCur = 0L;
						TermTransform.<>c__DisplayClassd CS$<>8__locals3 = CS$<>8__locals2;
						long? rowCount = trainingData.GetRowCount(true);
						CS$<>8__locals3.rowCount = ((rowCount != null) ? ((double)rowCount.GetValueOrDefault()) : double.NaN);
						ProgressHeader progressHeader = new ProgressHeader(new string[] { "Total terms" }, new string[] { "examples" });
						int j = 0;
						for (int k = 0; k < infos.Length; k++)
						{
							if (array[k] == null)
							{
								TermTransform.Builder builder3 = TermTransform.Builder.Create(infos[k].TypeSrc, column[k].sort ?? args.sort);
								array3[j] = k;
								CS$<>8__locals1.trainer[j++] = TermTransform.Trainer.Create(rowCursor, infos[k].Source, false, array2[k], builder3);
							}
						}
						progressChannel.SetHeader(progressHeader, delegate(IProgressEntry e)
						{
							e.SetProgress(0, (double)CS$<>8__locals2.rowCur, CS$<>8__locals2.rowCount);
							e.SetMetric(0, (double)CS$<>8__locals2.CS$<>8__localsc.trainer.Sum((TermTransform.Trainer t) => t.Count));
						});
						int num2 = 0;
						while (num2 < CS$<>8__locals1.trainer.Length && rowCursor.MoveNext())
						{
							CS$<>8__locals2.rowCur += 1L;
							for (int l = num2; l < CS$<>8__locals1.trainer.Length; l++)
							{
								if (!CS$<>8__locals1.trainer[l].ProcessRow())
								{
									Utils.Swap<int>(ref array3[l], ref array3[num2]);
									Utils.Swap<TermTransform.Trainer>(ref CS$<>8__locals1.trainer[l], ref CS$<>8__locals1.trainer[num2++]);
								}
							}
						}
						IProgressChannel progressChannel2 = progressChannel;
						double?[] array4 = new double?[2];
						array4[0] = new double?((double)CS$<>8__locals1.trainer.Sum((TermTransform.Trainer t) => t.Count));
						array4[1] = new double?((double)CS$<>8__locals2.rowCur);
						progressChannel2.Checkpoint(array4);
					}
				}
				for (int j = 0; j < CS$<>8__locals1.trainer.Length; j++)
				{
					int num3 = array3[j];
					if (CS$<>8__locals1.trainer[j].Count == 0)
					{
						ch.Warning("Term map for output column '{0}' contains no entries.", new object[] { infos[num3].Name });
					}
					array[num3] = CS$<>8__locals1.trainer[j].Finish();
					CS$<>8__locals1.trainer[j] = null;
				}
			}
			return array;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00023EC8 File Offset: 0x000220C8
		private ColumnType[] ComputeTypesAndMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			ColumnType[] array = new ColumnType[this.Infos.Length];
			for (int i = 0; i < array.Length; i++)
			{
				OneToOneTransformBase.ColInfo colInfo = this.Infos[i];
				KeyType outputType = this._termMap[i].Map.OutputType;
				if (colInfo.TypeSrc.IsVector)
				{
					array[i] = new VectorType(outputType, colInfo.TypeSrc.AsVector);
				}
				else
				{
					array[i] = outputType;
				}
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, colInfo.Source, "SlotNames"))
				{
					this._termMap[i].AddMetadata(builder);
				}
			}
			metadata.Seal();
			return array;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00024078 File Offset: 0x00022278
		private TermTransform(ModelLoadContext ctx, IHost host, IDataView input)
		{
			TermTransform.<>c__DisplayClass11 CS$<>8__locals1 = new TermTransform.<>c__DisplayClass11();
			CS$<>8__locals1.host = host;
			base..ctor(ctx, CS$<>8__locals1.host, input, new Func<ColumnType, string>(TermTransform.TestIsKnownDataKind));
			CS$<>8__locals1.<>4__this = this;
			int cinfo = this.Infos.Length;
			if (ctx.Header.ModelVerWritten >= 65539U)
			{
				this._textMetadata = Utils.ReadBoolArray(ctx.Reader, cinfo);
			}
			else
			{
				this._textMetadata = new bool[cinfo];
			}
			TermTransform.TermMap[] termMap = new TermTransform.TermMap[cinfo];
			if (!ctx.TryProcessSubModel("Vocabulary", delegate(ModelLoadContext c)
			{
				Contracts.CheckValue<ModelLoadContext>(CS$<>8__locals1.<>4__this._host, c, "ctx");
				c.CheckAtModel(TermTransform.GetTermManagerVersionInfo());
				int num = c.Reader.ReadInt32();
				Contracts.CheckDecode(CS$<>8__locals1.<>4__this._host, num == cinfo);
				if (c.Header.ModelVerWritten >= 65538U)
				{
					for (int j = 0; j < cinfo; j++)
					{
						termMap[j] = TermTransform.TermMap.Load(c, CS$<>8__locals1.host, CS$<>8__locals1.<>4__this);
					}
					return;
				}
				for (int k = 0; k < cinfo; k++)
				{
					termMap[k] = TermTransform.TermMap.TextImpl.Create(c, CS$<>8__locals1.host);
				}
			}))
			{
				throw Contracts.ExceptDecode(this._host, "Missing {0} model", new object[] { "Vocabulary" });
			}
			this._termMap = new TermTransform.BoundTermMap[cinfo];
			for (int i = 0; i < cinfo; i++)
			{
				this._termMap[i] = termMap[i].Bind(this, i);
			}
			this._types = this.ComputeTypesAndMetadata();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000241C0 File Offset: 0x000223C0
		public static TermTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(TermTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckValue<IHostEnvironment>(env, env, "env");
			IHost h = env.Register("Term");
			return HostExtensions.Apply<TermTransform>(h, "Loading Model", (IChannel ch) => new TermTransform(ctx, h, input));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00024308 File Offset: 0x00022508
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(TermTransform.GetVersionInfo());
			base.SaveBase(ctx);
			Utils.WriteBoolBytesNoCount(ctx.Writer, this._textMetadata, this._textMetadata.Length);
			ctx.SaveSubModel("Vocabulary", delegate(ModelSaveContext c)
			{
				Contracts.CheckValue<ModelSaveContext>(this._host, c, "ctx");
				c.CheckAtModel();
				c.SetVersionInfo(TermTransform.GetTermManagerVersionInfo());
				c.Writer.Write(this._termMap.Length);
				foreach (TermTransform.BoundTermMap boundTermMap in this._termMap)
				{
					boundTermMap.Map.Save(c, this);
				}
				c.SaveTextStream("Terms.txt", delegate(TextWriter writer)
				{
					foreach (TermTransform.BoundTermMap boundTermMap2 in this._termMap)
					{
						boundTermMap2.WriteTextTerms(writer);
					}
				});
			});
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00024370 File Offset: 0x00022570
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00024387 File Offset: 0x00022587
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			return this._termMap[iinfo].GetMappingGetter(input);
		}

		// Token: 0x04000363 RID: 867
		internal const string Summary = "Converts input values (words, numbers, etc.) to index in a dictionary.";

		// Token: 0x04000364 RID: 868
		public const string LoaderSignature = "TermTransform";

		// Token: 0x04000365 RID: 869
		private const uint verNonTextTypesSupported = 65539U;

		// Token: 0x04000366 RID: 870
		private const uint verManagerNonTextTypesSupported = 65538U;

		// Token: 0x04000367 RID: 871
		public const string TermManagerLoaderSignature = "TermManager";

		// Token: 0x04000368 RID: 872
		private const string RegistrationName = "Term";

		// Token: 0x04000369 RID: 873
		private readonly ColumnType[] _types;

		// Token: 0x0400036A RID: 874
		private readonly TermTransform.BoundTermMap[] _termMap;

		// Token: 0x0400036B RID: 875
		private readonly bool[] _textMetadata;

		// Token: 0x0400036C RID: 876
		private static volatile MemoryStreamPool _codecFactoryPool;

		// Token: 0x0400036D RID: 877
		private volatile CodecFactory _codecFactory;

		// Token: 0x02000149 RID: 329
		private abstract class Builder
		{
			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060006AE RID: 1710
			public abstract int Count { get; }

			// Token: 0x060006AF RID: 1711 RVA: 0x0002439B File Offset: 0x0002259B
			protected Builder(PrimitiveType type)
			{
				this.ItemType = type;
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x000243AC File Offset: 0x000225AC
			public static TermTransform.Builder Create(ColumnType type, TermTransform.SortOrder sortOrder)
			{
				bool flag = sortOrder == TermTransform.SortOrder.Value;
				PrimitiveType asPrimitive = type.ItemType.AsPrimitive;
				if (asPrimitive.IsText)
				{
					return new TermTransform.Builder.TextImpl(flag);
				}
				return Utils.MarshalInvoke<PrimitiveType, bool, TermTransform.Builder>(new Func<PrimitiveType, bool, TermTransform.Builder>(TermTransform.Builder.CreateCore<int>), asPrimitive.RawType, asPrimitive, flag);
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x000243F8 File Offset: 0x000225F8
			private static TermTransform.Builder CreateCore<T>(PrimitiveType type, bool sorted) where T : IEquatable<T>, IComparable<T>
			{
				RefPredicate<T> refPredicate;
				if (!Conversions.Instance.TryGetIsNAPredicate<T>(type, out refPredicate))
				{
					refPredicate = delegate(ref T val)
					{
						return false;
					};
				}
				return new TermTransform.Builder.Impl<T>(type, refPredicate, sorted);
			}

			// Token: 0x060006B2 RID: 1714
			public abstract TermTransform.TermMap Finish();

			// Token: 0x060006B3 RID: 1715
			public abstract void ParseAddTermArg(ref DvText terms, IChannel ch);

			// Token: 0x0400036F RID: 879
			public readonly PrimitiveType ItemType;

			// Token: 0x0200014B RID: 331
			private sealed class TextImpl : TermTransform.Builder<DvText>
			{
				// Token: 0x17000088 RID: 136
				// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00024516 File Offset: 0x00022716
				public override int Count
				{
					get
					{
						return this._pool.Count;
					}
				}

				// Token: 0x060006B9 RID: 1721 RVA: 0x00024523 File Offset: 0x00022723
				public TextImpl(bool sorted)
					: base(TextType.Instance)
				{
					this._pool = new NormStr.Pool();
					this._sorted = sorted;
				}

				// Token: 0x060006BA RID: 1722 RVA: 0x00024544 File Offset: 0x00022744
				public override bool TryAdd(ref DvText val)
				{
					if (!val.HasChars)
					{
						return false;
					}
					int count = this._pool.Count;
					return val.AddToPool(this._pool).Id == count;
				}

				// Token: 0x060006BB RID: 1723 RVA: 0x000245A4 File Offset: 0x000227A4
				public override TermTransform.TermMap Finish()
				{
					if (!this._sorted || this._pool.Count <= 1)
					{
						return new TermTransform.TermMap.TextImpl(this._pool);
					}
					int[] identityPermutation = Utils.GetIdentityPermutation(this._pool.Count);
					int i;
					Comparison<int> comparison = (int i, int j) => this._pool.GetNormStrById(i).Value.CompareTo(this._pool.GetNormStrById(j).Value);
					Array.Sort<int>(identityPermutation, comparison);
					NormStr.Pool pool = new NormStr.Pool();
					for (i = 0; i < identityPermutation.Length; i++)
					{
						pool.Add(this._pool.GetNormStrById(identityPermutation[i]).Value);
					}
					return new TermTransform.TermMap.TextImpl(pool);
				}

				// Token: 0x04000370 RID: 880
				private readonly NormStr.Pool _pool;

				// Token: 0x04000371 RID: 881
				private readonly bool _sorted;
			}

			// Token: 0x0200014C RID: 332
			private sealed class Impl<T> : TermTransform.Builder<T> where T : IEquatable<T>, IComparable<T>
			{
				// Token: 0x17000089 RID: 137
				// (get) Token: 0x060006BD RID: 1725 RVA: 0x0002462C File Offset: 0x0002282C
				public override int Count
				{
					get
					{
						return this._values.Count;
					}
				}

				// Token: 0x060006BE RID: 1726 RVA: 0x00024639 File Offset: 0x00022839
				public Impl(PrimitiveType type, RefPredicate<T> mapsToMissing, bool sort)
					: base(type)
				{
					this._values = new HashArray<T>();
					this._mapsToMissing = mapsToMissing;
					this._sort = sort;
				}

				// Token: 0x060006BF RID: 1727 RVA: 0x0002465B File Offset: 0x0002285B
				public override bool TryAdd(ref T val)
				{
					return !this._mapsToMissing.Invoke(ref val) && this._values.TryAdd(val);
				}

				// Token: 0x060006C0 RID: 1728 RVA: 0x0002467E File Offset: 0x0002287E
				public override TermTransform.TermMap Finish()
				{
					if (this._sort)
					{
						this._values.Sort();
					}
					return new TermTransform.TermMap.HashArrayImpl<T>(this.ItemType, this._values);
				}

				// Token: 0x04000372 RID: 882
				private readonly HashArray<T> _values;

				// Token: 0x04000373 RID: 883
				private readonly RefPredicate<T> _mapsToMissing;

				// Token: 0x04000374 RID: 884
				private readonly bool _sort;
			}
		}

		// Token: 0x0200014A RID: 330
		private abstract class Builder<T> : TermTransform.Builder
		{
			// Token: 0x060006B5 RID: 1717 RVA: 0x00024429 File Offset: 0x00022629
			protected Builder(PrimitiveType type)
				: base(type)
			{
			}

			// Token: 0x060006B6 RID: 1718
			public abstract bool TryAdd(ref T val);

			// Token: 0x060006B7 RID: 1719 RVA: 0x00024434 File Offset: 0x00022634
			public override void ParseAddTermArg(ref DvText terms, IChannel ch)
			{
				TryParseMapper<T> parseConversion = Conversions.Instance.GetParseConversion<T>(this.ItemType);
				bool flag = true;
				while (flag)
				{
					DvText dvText;
					flag = terms.SplitOne(',', ref dvText, ref terms);
					dvText = dvText.Trim();
					T t;
					if (!dvText.HasChars)
					{
						ch.Warning("Empty strings ignored in 'terms' specification");
					}
					else if (!parseConversion(ref dvText, out t))
					{
						ch.Warning("Item '{0}' ignored in 'terms' specification since it could not be parsed as '{1}'", new object[] { dvText, this.ItemType });
					}
					else if (!this.TryAdd(ref t))
					{
						ch.Warning("Duplicate item '{0}' ignored in 'terms' specification", new object[] { dvText });
					}
				}
				if (this.Count == 0)
				{
					throw Contracts.ExceptUserArg(ch, "terms", "Nothing parsed as '{0}'", new object[] { this.ItemType });
				}
			}
		}

		// Token: 0x0200014D RID: 333
		private abstract class Trainer
		{
			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000246A4 File Offset: 0x000228A4
			public int Count
			{
				get
				{
					return this._bldr.Count;
				}
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x000246B1 File Offset: 0x000228B1
			private Trainer(TermTransform.Builder bldr, int max)
			{
				this._bldr = bldr;
				this._remaining = max;
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x000246C8 File Offset: 0x000228C8
			public static TermTransform.Trainer Create(IRow row, int col, bool autoConvert, int count, TermTransform.Builder bldr)
			{
				ISchema schema = row.Schema;
				ColumnType columnType = schema.GetColumnType(col);
				if (columnType.IsVector)
				{
					return Utils.MarshalInvoke<IRow, int, int, TermTransform.Builder, TermTransform.Trainer>(new Func<IRow, int, int, TermTransform.Builder, TermTransform.Trainer>(TermTransform.Trainer.CreateVec<int>), bldr.ItemType.RawType, row, col, count, bldr);
				}
				return Utils.MarshalInvoke<IRow, int, bool, int, TermTransform.Builder, TermTransform.Trainer>(new Func<IRow, int, bool, int, TermTransform.Builder, TermTransform.Trainer>(TermTransform.Trainer.CreateOne<int>), bldr.ItemType.RawType, row, col, autoConvert, count, bldr);
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x00024734 File Offset: 0x00022934
			private static TermTransform.Trainer CreateOne<T>(IRow row, int col, bool autoConvert, int count, TermTransform.Builder bldr)
			{
				TermTransform.Builder<T> builder = (TermTransform.Builder<T>)bldr;
				ValueGetter<T> valueGetter;
				if (autoConvert)
				{
					valueGetter = RowCursorUtils.GetGetterAs<T>(bldr.ItemType, row, col);
				}
				else
				{
					valueGetter = row.GetGetter<T>(col);
				}
				return new TermTransform.Trainer.ImplOne<T>(valueGetter, count, builder);
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x00024770 File Offset: 0x00022970
			private static TermTransform.Trainer CreateVec<T>(IRow row, int col, int count, TermTransform.Builder bldr)
			{
				TermTransform.Builder<T> builder = (TermTransform.Builder<T>)bldr;
				ValueGetter<VBuffer<T>> getter = row.GetGetter<VBuffer<T>>(col);
				return new TermTransform.Trainer.ImplVec<T>(getter, count, builder);
			}

			// Token: 0x060006C6 RID: 1734
			public abstract bool ProcessRow();

			// Token: 0x060006C7 RID: 1735 RVA: 0x00024794 File Offset: 0x00022994
			public TermTransform.TermMap Finish()
			{
				return this._bldr.Finish();
			}

			// Token: 0x04000375 RID: 885
			private readonly TermTransform.Builder _bldr;

			// Token: 0x04000376 RID: 886
			private int _remaining;

			// Token: 0x0200014E RID: 334
			private sealed class ImplOne<T> : TermTransform.Trainer
			{
				// Token: 0x060006C8 RID: 1736 RVA: 0x000247A1 File Offset: 0x000229A1
				public ImplOne(ValueGetter<T> getter, int max, TermTransform.Builder<T> bldr)
					: base(bldr, max)
				{
					this._getter = getter;
					this._bldr = bldr;
				}

				// Token: 0x060006C9 RID: 1737 RVA: 0x000247BC File Offset: 0x000229BC
				public sealed override bool ProcessRow()
				{
					if (this._remaining <= 0)
					{
						return false;
					}
					this._getter.Invoke(ref this._val);
					return !this._bldr.TryAdd(ref this._val) || --this._remaining > 0;
				}

				// Token: 0x04000377 RID: 887
				private readonly ValueGetter<T> _getter;

				// Token: 0x04000378 RID: 888
				private T _val;

				// Token: 0x04000379 RID: 889
				private readonly TermTransform.Builder<T> _bldr;
			}

			// Token: 0x0200014F RID: 335
			private sealed class ImplVec<T> : TermTransform.Trainer
			{
				// Token: 0x060006CA RID: 1738 RVA: 0x0002480E File Offset: 0x00022A0E
				public ImplVec(ValueGetter<VBuffer<T>> getter, int max, TermTransform.Builder<T> bldr)
					: base(bldr, max)
				{
					this._getter = getter;
					this._bldr = bldr;
				}

				// Token: 0x060006CB RID: 1739 RVA: 0x00024828 File Offset: 0x00022A28
				private bool AccumAndDecrement(ref T val)
				{
					return !this._bldr.TryAdd(ref val) || --this._remaining > 0;
				}

				// Token: 0x060006CC RID: 1740 RVA: 0x0002485C File Offset: 0x00022A5C
				public sealed override bool ProcessRow()
				{
					if (this._remaining <= 0)
					{
						return false;
					}
					this._getter.Invoke(ref this._val);
					if (this._val.IsDense || this._addedDefaultFromSparse)
					{
						for (int i = 0; i < this._val.Count; i++)
						{
							if (!this.AccumAndDecrement(ref this._val.Values[i]))
							{
								return false;
							}
						}
						return true;
					}
					T t = default(T);
					for (int j = 0; j < this._val.Count; j++)
					{
						if (!this._addedDefaultFromSparse && this._val.Indices[j] != j)
						{
							this._addedDefaultFromSparse = true;
							if (!this.AccumAndDecrement(ref t))
							{
								return false;
							}
						}
						if (!this.AccumAndDecrement(ref this._val.Values[j]))
						{
							return false;
						}
					}
					if (!this._addedDefaultFromSparse)
					{
						this._addedDefaultFromSparse = true;
						if (!this.AccumAndDecrement(ref t))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x0400037A RID: 890
				private readonly ValueGetter<VBuffer<T>> _getter;

				// Token: 0x0400037B RID: 891
				private VBuffer<T> _val;

				// Token: 0x0400037C RID: 892
				private readonly TermTransform.Builder<T> _bldr;

				// Token: 0x0400037D RID: 893
				private bool _addedDefaultFromSparse;
			}
		}

		// Token: 0x02000150 RID: 336
		private enum MapType : byte
		{
			// Token: 0x0400037F RID: 895
			Text,
			// Token: 0x04000380 RID: 896
			Codec
		}

		// Token: 0x02000151 RID: 337
		private abstract class TermMap
		{
			// Token: 0x060006CD RID: 1741 RVA: 0x0002494D File Offset: 0x00022B4D
			protected TermMap(PrimitiveType type, int count)
			{
				this.ItemType = type;
				this.Count = count;
				this.OutputType = new KeyType(6, 0UL, (this.Count == 0) ? 1 : this.Count, true);
			}

			// Token: 0x060006CE RID: 1742
			public abstract void Save(ModelSaveContext ctx, TermTransform trans);

			// Token: 0x060006CF RID: 1743 RVA: 0x00024984 File Offset: 0x00022B84
			public static TermTransform.TermMap Load(ModelLoadContext ctx, IExceptionContext ectx, TermTransform trans)
			{
				TermTransform.MapType mapType = (TermTransform.MapType)ctx.Reader.ReadByte();
				Contracts.CheckDecode(ectx, Enum.IsDefined(typeof(TermTransform.MapType), mapType));
				switch (mapType)
				{
				case TermTransform.MapType.Text:
					return TermTransform.TermMap.TextImpl.Create(ctx, ectx);
				case TermTransform.MapType.Codec:
				{
					IValueCodec valueCodec;
					if (!trans.CodecFactory.TryReadCodec(ctx.Reader.BaseStream, out valueCodec))
					{
						throw Contracts.ExceptDecode(ectx, "Unrecognized codec read");
					}
					Contracts.CheckDecode(ectx, valueCodec.Type.IsPrimitive);
					int num = ctx.Reader.ReadInt32();
					Contracts.CheckDecode(ectx, num >= 0);
					return Utils.MarshalInvoke<ModelLoadContext, IExceptionContext, IValueCodec, int, TermTransform.TermMap>(new Func<ModelLoadContext, IExceptionContext, IValueCodec, int, TermTransform.TermMap>(TermTransform.TermMap.LoadCodecCore<int>), valueCodec.Type.RawType, ctx, ectx, valueCodec, num);
				}
				default:
					throw Contracts.Except(ectx, "Unrecognized type '{0}'", new object[] { mapType });
				}
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x00024A64 File Offset: 0x00022C64
			private static TermTransform.TermMap LoadCodecCore<T>(ModelLoadContext ctx, IExceptionContext ectx, IValueCodec codec, int count) where T : IEquatable<T>, IComparable<T>
			{
				IValueCodec<T> valueCodec = (IValueCodec<T>)codec;
				HashArray<T> hashArray = new HashArray<T>();
				if (count > 0)
				{
					using (IValueReader<T> valueReader = valueCodec.OpenReader(ctx.Reader.BaseStream, count))
					{
						T t = default(T);
						for (int i = 0; i < count; i++)
						{
							valueReader.MoveNext();
							valueReader.Get(ref t);
							int num = hashArray.Add(t);
							if (num != i)
							{
								throw Contracts.ExceptDecode(ectx, "Duplicate items at positions {0} and {1}", new object[] { num, i });
							}
						}
					}
				}
				return new TermTransform.TermMap.HashArrayImpl<T>(codec.Type.AsPrimitive, hashArray);
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00024B28 File Offset: 0x00022D28
			public TermTransform.BoundTermMap Bind(TermTransform trans, int iinfo)
			{
				OneToOneTransformBase.ColInfo colInfo = trans.Infos[iinfo];
				ColumnType itemType = colInfo.TypeSrc.ItemType;
				if (!itemType.Equals(this.ItemType))
				{
					throw Contracts.Except(trans._host, "Could not apply a map over type '{0}' to column '{1}' since it has type '{2}'", new object[] { this.ItemType, colInfo.Name, itemType });
				}
				return TermTransform.BoundTermMap.Create(this, trans, iinfo);
			}

			// Token: 0x060006D2 RID: 1746
			public abstract void WriteTextTerms(TextWriter writer);

			// Token: 0x04000381 RID: 897
			public readonly PrimitiveType ItemType;

			// Token: 0x04000382 RID: 898
			public readonly KeyType OutputType;

			// Token: 0x04000383 RID: 899
			public readonly int Count;

			// Token: 0x02000153 RID: 339
			public sealed class TextImpl : TermTransform.TermMap<DvText>
			{
				// Token: 0x060006D6 RID: 1750 RVA: 0x00024B99 File Offset: 0x00022D99
				public TextImpl(NormStr.Pool pool)
					: base(TextType.Instance, pool.Count)
				{
					this._pool = pool;
				}

				// Token: 0x060006D7 RID: 1751 RVA: 0x00024BB4 File Offset: 0x00022DB4
				public static TermTransform.TermMap.TextImpl Create(ModelLoadContext ctx, IExceptionContext ectx)
				{
					NormStr.Pool pool = new NormStr.Pool();
					int num = ctx.Reader.ReadInt32();
					Contracts.CheckDecode(ectx, num >= 0);
					for (int i = 0; i < num; i++)
					{
						NormStr normStr = pool.Add(ctx.LoadNonEmptyString());
						Contracts.CheckDecode(ectx, normStr.Id == i);
					}
					Contracts.CheckDecode(ectx, pool.Get("", false) == null, "Why did the deserialized pool have the empty string");
					return new TermTransform.TermMap.TextImpl(pool);
				}

				// Token: 0x060006D8 RID: 1752 RVA: 0x00024C28 File Offset: 0x00022E28
				public override void Save(ModelSaveContext ctx, TermTransform trans)
				{
					ctx.Writer.Write(0);
					Contracts.CheckDecode(trans._host, this._pool.Get("", false) == null);
					ctx.Writer.Write(this._pool.Count);
					int num = 0;
					foreach (NormStr normStr in this._pool)
					{
						ctx.SaveNonEmptyString(normStr.Value);
						num++;
					}
				}

				// Token: 0x060006D9 RID: 1753 RVA: 0x00024CC4 File Offset: 0x00022EC4
				private void KeyMapper(ref DvText src, ref uint dst)
				{
					NormStr normStr = src.Trim().FindInPool(this._pool);
					if (normStr == null)
					{
						dst = 0U;
						return;
					}
					dst = (uint)(normStr.Id + 1);
				}

				// Token: 0x060006DA RID: 1754 RVA: 0x00024CF7 File Offset: 0x00022EF7
				public override ValueMapper<DvText, uint> GetKeyMapper()
				{
					return new ValueMapper<DvText, uint>(this.KeyMapper);
				}

				// Token: 0x060006DB RID: 1755 RVA: 0x00024D08 File Offset: 0x00022F08
				public override void GetTerms(ref VBuffer<DvText> dst)
				{
					DvText[] array = dst.Values;
					if (Utils.Size<DvText>(array) < this._pool.Count)
					{
						array = new DvText[this._pool.Count];
					}
					int num = 0;
					foreach (NormStr normStr in this._pool)
					{
						array[normStr.Id] = new DvText(normStr.Value);
						num++;
					}
					dst = new VBuffer<DvText>(this._pool.Count, array, dst.Indices);
				}

				// Token: 0x060006DC RID: 1756 RVA: 0x00024DB8 File Offset: 0x00022FB8
				public override void WriteTextTerms(TextWriter writer)
				{
					writer.WriteLine("# Number of terms = {0}", this.Count);
					foreach (NormStr normStr in this._pool)
					{
						writer.WriteLine("{0}\t{1}", normStr.Id, normStr.Value);
					}
				}

				// Token: 0x04000384 RID: 900
				private readonly NormStr.Pool _pool;
			}

			// Token: 0x02000154 RID: 340
			public sealed class HashArrayImpl<T> : TermTransform.TermMap<T> where T : IEquatable<T>, IComparable<T>
			{
				// Token: 0x060006DD RID: 1757 RVA: 0x00024E30 File Offset: 0x00023030
				public HashArrayImpl(PrimitiveType itemType, HashArray<T> values)
					: base(itemType, values.Count)
				{
					this._values = values;
				}

				// Token: 0x060006DE RID: 1758 RVA: 0x00024E48 File Offset: 0x00023048
				public override void Save(ModelSaveContext ctx, TermTransform trans)
				{
					IValueCodec valueCodec;
					if (!trans.CodecFactory.TryGetCodec(this.ItemType, out valueCodec))
					{
						throw Contracts.Except(trans._host, "We do not know how to serialize terms of type '{0}'", new object[] { this.ItemType });
					}
					ctx.Writer.Write(1);
					trans.CodecFactory.WriteCodec(ctx.Writer.BaseStream, valueCodec);
					IValueCodec<T> valueCodec2 = (IValueCodec<T>)valueCodec;
					ctx.Writer.Write(this._values.Count);
					using (IValueWriter<T> valueWriter = valueCodec2.OpenWriter(ctx.Writer.BaseStream))
					{
						for (int i = 0; i < this._values.Count; i++)
						{
							T item = this._values.GetItem(i);
							valueWriter.Write(ref item);
						}
						valueWriter.Commit();
					}
				}

				// Token: 0x060006DF RID: 1759 RVA: 0x00024F5B File Offset: 0x0002315B
				public override ValueMapper<T, uint> GetKeyMapper()
				{
					return delegate(ref T src, ref uint dst)
					{
						int num;
						if (this._values.TryGetIndex(src, ref num))
						{
							dst = (uint)(num + 1);
							return;
						}
						dst = 0U;
					};
				}

				// Token: 0x060006E0 RID: 1760 RVA: 0x00024F6C File Offset: 0x0002316C
				public override void GetTerms(ref VBuffer<T> dst)
				{
					if (this.Count == 0)
					{
						dst = new VBuffer<T>(0, dst.Values, dst.Indices);
						return;
					}
					T[] array = dst.Values;
					if (Utils.Size<T>(array) < this.Count)
					{
						array = new T[this.Count];
					}
					this._values.CopyTo(array);
					dst = new VBuffer<T>(this.Count, array, dst.Indices);
				}

				// Token: 0x060006E1 RID: 1761 RVA: 0x00024FE0 File Offset: 0x000231E0
				public override void WriteTextTerms(TextWriter writer)
				{
					writer.WriteLine("# Number of terms of type '{0}' = {1}", this.ItemType, this.Count);
					StringBuilder stringBuilder = null;
					ValueMapper<T, StringBuilder> stringConversion = Conversions.Instance.GetStringConversion<T>(this.ItemType);
					for (int i = 0; i < this._values.Count; i++)
					{
						T item = this._values.GetItem(i);
						stringConversion.Invoke(ref item, ref stringBuilder);
						writer.WriteLine("{0}\t{1}", i, stringBuilder.ToString());
					}
				}

				// Token: 0x04000385 RID: 901
				private readonly HashArray<T> _values;
			}
		}

		// Token: 0x02000152 RID: 338
		private abstract class TermMap<T> : TermTransform.TermMap
		{
			// Token: 0x060006D3 RID: 1747 RVA: 0x00024B8F File Offset: 0x00022D8F
			protected TermMap(PrimitiveType type, int count)
				: base(type, count)
			{
			}

			// Token: 0x060006D4 RID: 1748
			public abstract ValueMapper<T, uint> GetKeyMapper();

			// Token: 0x060006D5 RID: 1749
			public abstract void GetTerms(ref VBuffer<T> dst);
		}

		// Token: 0x02000155 RID: 341
		private abstract class BoundTermMap
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00025060 File Offset: 0x00023260
			private IHost Host
			{
				get
				{
					return this._parent._host;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0002506D File Offset: 0x0002326D
			private bool IsTextMetadata
			{
				get
				{
					return this._parent._textMetadata[this._iinfo];
				}
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x00025084 File Offset: 0x00023284
			private BoundTermMap(TermTransform.TermMap map, TermTransform trans, int iinfo)
			{
				this._parent = trans;
				OneToOneTransformBase.ColInfo colInfo = trans.Infos[iinfo];
				this.Map = map;
				this._iinfo = iinfo;
				this._inputIsVector = colInfo.TypeSrc.IsVector;
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x000250C6 File Offset: 0x000232C6
			public static TermTransform.BoundTermMap Create(TermTransform.TermMap map, TermTransform trans, int iinfo)
			{
				IHost host = trans._host;
				OneToOneTransformBase.ColInfo colInfo = trans.Infos[iinfo];
				return Utils.MarshalInvoke<TermTransform.TermMap, TermTransform, int, TermTransform.BoundTermMap>(new Func<TermTransform.TermMap, TermTransform, int, TermTransform.BoundTermMap>(TermTransform.BoundTermMap.CreateCore<int>), map.ItemType.RawType, map, trans, iinfo);
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x000250F8 File Offset: 0x000232F8
			public static TermTransform.BoundTermMap CreateCore<T>(TermTransform.TermMap map, TermTransform trans, int iinfo)
			{
				TermTransform.TermMap<T> termMap = (TermTransform.TermMap<T>)map;
				if (termMap.ItemType.IsKey)
				{
					return new TermTransform.BoundTermMap.KeyImpl<T>(termMap, trans, iinfo);
				}
				return new TermTransform.BoundTermMap.Impl<T>(termMap, trans, iinfo);
			}

			// Token: 0x060006E8 RID: 1768
			public abstract Delegate GetMappingGetter(IRow row);

			// Token: 0x060006E9 RID: 1769
			public abstract void AddMetadata(MetadataDispatcher.Builder bldr);

			// Token: 0x060006EA RID: 1770 RVA: 0x0002512A File Offset: 0x0002332A
			public virtual void WriteTextTerms(TextWriter writer)
			{
				this.Map.WriteTextTerms(writer);
			}

			// Token: 0x04000386 RID: 902
			public readonly TermTransform.TermMap Map;

			// Token: 0x04000387 RID: 903
			private readonly TermTransform _parent;

			// Token: 0x04000388 RID: 904
			private readonly int _iinfo;

			// Token: 0x04000389 RID: 905
			private readonly bool _inputIsVector;

			// Token: 0x02000156 RID: 342
			private abstract class Base<T> : TermTransform.BoundTermMap
			{
				// Token: 0x060006EB RID: 1771 RVA: 0x00025138 File Offset: 0x00023338
				public Base(TermTransform.TermMap<T> map, TermTransform trans, int iinfo)
					: base(map, trans, iinfo)
				{
					this._map = map;
				}

				// Token: 0x060006EC RID: 1772 RVA: 0x0002514C File Offset: 0x0002334C
				private static uint MapDefault(ValueMapper<T, uint> map)
				{
					T t = default(T);
					uint num = 0U;
					map.Invoke(ref t, ref num);
					return num;
				}

				// Token: 0x060006ED RID: 1773 RVA: 0x000254C4 File Offset: 0x000236C4
				public override Delegate GetMappingGetter(IRow input)
				{
					if (!this._inputIsVector)
					{
						ValueMapper<T, uint> map2 = this._map.GetKeyMapper();
						OneToOneTransformBase.ColInfo colInfo = this._parent.Infos[this._iinfo];
						T src2 = default(T);
						ValueGetter<T> getSrc2 = this._parent.GetSrcGetter<T>(input, this._iinfo);
						return new ValueGetter<uint>(delegate(ref uint dst)
						{
							getSrc2.Invoke(ref src2);
							map2.Invoke(ref src2, ref dst);
						});
					}
					ValueMapper<T, uint> map = this._map.GetKeyMapper();
					OneToOneTransformBase.ColInfo info = this._parent.Infos[this._iinfo];
					ValueGetter<VBuffer<T>> getSrc = this._parent.GetSrcGetter<VBuffer<T>>(input, this._iinfo);
					VBuffer<T> src = default(VBuffer<T>);
					VBufferBuilder<uint> bldr = new VBufferBuilder<uint>(U4Adder.Instance);
					int cv = info.TypeSrc.VectorSize;
					uint defaultMapValue = TermTransform.BoundTermMap.Base<T>.MapDefault(map);
					uint dstItem = 0U;
					ValueGetter<VBuffer<uint>> valueGetter;
					if (defaultMapValue == 0U)
					{
						valueGetter = delegate(ref VBuffer<uint> dst)
						{
							getSrc.Invoke(ref src);
							int length = src.Length;
							if (cv != 0 && length != cv)
							{
								throw Contracts.Except(this.Host, "Column '{0}': TermTransform expects {1} slots, but got {2}", new object[] { info.Name, cv, length });
							}
							if (length == 0)
							{
								dst = new VBuffer<uint>(length, dst.Values, dst.Indices);
								return;
							}
							bldr.Reset(length, false);
							T[] values = src.Values;
							int[] array = ((!src.IsDense) ? src.Indices : null);
							int count = src.Count;
							for (int i = 0; i < count; i++)
							{
								map.Invoke(ref values[i], ref dstItem);
								if (dstItem != 0U)
								{
									int num = ((array != null) ? array[i] : i);
									bldr.AddFeature(num, dstItem);
								}
							}
							bldr.GetResult(ref dst);
						};
					}
					else
					{
						valueGetter = delegate(ref VBuffer<uint> dst)
						{
							getSrc.Invoke(ref src);
							int length2 = src.Length;
							if (cv != 0 && length2 != cv)
							{
								throw Contracts.Except(this.Host, "Column '{0}': TermTransform expects {1} slots, but got {2}", new object[] { info.Name, cv, length2 });
							}
							if (length2 == 0)
							{
								dst = new VBuffer<uint>(length2, dst.Values, dst.Indices);
								return;
							}
							bldr.Reset(length2, false);
							T[] values2 = src.Values;
							if (src.IsDense)
							{
								for (int j = 0; j < src.Length; j++)
								{
									map.Invoke(ref values2[j], ref dstItem);
									if (dstItem != 0U)
									{
										bldr.AddFeature(j, dstItem);
									}
								}
							}
							else
							{
								int[] indices = src.Indices;
								int num2 = ((src.Count == 0) ? src.Length : indices[0]);
								int num3 = 0;
								for (int k = 0; k < src.Length; k++)
								{
									if (num2 == k)
									{
										map.Invoke(ref values2[num3], ref dstItem);
										if (dstItem != 0U)
										{
											bldr.AddFeature(k, dstItem);
										}
										num2 = ((++num3 == src.Count) ? src.Length : indices[num3]);
									}
									else
									{
										bldr.AddFeature(k, defaultMapValue);
									}
								}
							}
							bldr.GetResult(ref dst);
						};
					}
					return valueGetter;
				}

				// Token: 0x060006EE RID: 1774 RVA: 0x00025660 File Offset: 0x00023860
				public override void AddMetadata(MetadataDispatcher.Builder bldr)
				{
					if (this._map.Count == 0)
					{
						return;
					}
					if (base.IsTextMetadata && !this._map.ItemType.IsText)
					{
						Conversions instance = Conversions.Instance;
						ValueMapper<T, StringBuilder> stringMapper = instance.GetStringConversion<T>(this._map.ItemType);
						MetadataUtils.MetadataGetter<VBuffer<DvText>> metadataGetter = delegate(int iinfo, ref VBuffer<DvText> dst)
						{
							VBuffer<T> vbuffer = default(VBuffer<T>);
							this._map.GetTerms(ref vbuffer);
							TermTransform.GetTextTerms<T>(ref vbuffer, stringMapper, ref dst);
						};
						bldr.AddGetter<VBuffer<DvText>>("KeyValues", new VectorType(TextType.Instance, this._map.OutputType.KeyCount), metadataGetter);
						return;
					}
					MetadataUtils.MetadataGetter<VBuffer<T>> metadataGetter2 = delegate(int iinfo, ref VBuffer<T> dst)
					{
						this._map.GetTerms(ref dst);
					};
					bldr.AddGetter<VBuffer<T>>("KeyValues", new VectorType(this._map.ItemType, this._map.OutputType.KeyCount), metadataGetter2);
				}

				// Token: 0x0400038A RID: 906
				protected readonly TermTransform.TermMap<T> _map;
			}

			// Token: 0x02000157 RID: 343
			private sealed class KeyImpl<T> : TermTransform.BoundTermMap.Base<T>
			{
				// Token: 0x060006F0 RID: 1776 RVA: 0x00025737 File Offset: 0x00023937
				public KeyImpl(TermTransform.TermMap<T> map, TermTransform trans, int iinfo)
					: base(map, trans, iinfo)
				{
				}

				// Token: 0x060006F1 RID: 1777 RVA: 0x00025744 File Offset: 0x00023944
				public override void AddMetadata(MetadataDispatcher.Builder bldr)
				{
					if (this._map.Count == 0)
					{
						return;
					}
					int source = this._parent.Infos[this._iinfo].Source;
					ColumnType metadataTypeOrNull = this._parent._input.Schema.GetMetadataTypeOrNull("KeyValues", source);
					if (metadataTypeOrNull == null || metadataTypeOrNull.VectorSize != this._map.ItemType.KeyCount || this._map.ItemType.KeyCount == 0 || !Utils.MarshalInvoke<ColumnType, MetadataDispatcher.Builder, bool>(new Func<ColumnType, MetadataDispatcher.Builder, bool>(this.AddMetadataCore<int>), metadataTypeOrNull.ItemType.RawType, metadataTypeOrNull.ItemType, bldr))
					{
						base.AddMetadata(bldr);
					}
				}

				// Token: 0x060006F2 RID: 1778 RVA: 0x00025970 File Offset: 0x00023B70
				private bool AddMetadataCore<TMeta>(ColumnType srcMetaType, MetadataDispatcher.Builder bldr)
				{
					KeyType asKey = this._map.ItemType.AsKey;
					KeyType keyType = new KeyType(6, asKey.Min, asKey.Count, true);
					Conversions instance = Conversions.Instance;
					ValueMapper<T, uint> conv;
					bool flag;
					if (!instance.TryGetStandardConversion<T, uint>(asKey, keyType, out conv, out flag))
					{
						return false;
					}
					int srcCol = this._parent.Infos[this._iinfo].Source;
					ValueGetter<VBuffer<TMeta>> getter = delegate(ref VBuffer<TMeta> dst)
					{
						VBuffer<TMeta> vbuffer = default(VBuffer<TMeta>);
						this._parent._input.Schema.GetMetadata<VBuffer<TMeta>>("KeyValues", srcCol, ref vbuffer);
						VBuffer<T> vbuffer2 = default(VBuffer<T>);
						this._map.GetTerms(ref vbuffer2);
						TMeta[] array = dst.Values;
						if (Utils.Size<TMeta>(array) < this._map.OutputType.KeyCount)
						{
							array = new TMeta[this._map.OutputType.KeyCount];
						}
						uint num = 0U;
						foreach (KeyValuePair<int, T> keyValuePair in vbuffer2.Items(true))
						{
							T value = keyValuePair.Value;
							conv.Invoke(ref value, ref num);
							vbuffer.GetItemOrDefault((int)(num - 1U), ref array[keyValuePair.Key]);
						}
						dst = new VBuffer<TMeta>(this._map.OutputType.KeyCount, array, dst.Indices);
					};
					if (base.IsTextMetadata && !srcMetaType.IsText)
					{
						ValueMapper<TMeta, StringBuilder> stringMapper = instance.GetStringConversion<TMeta>(srcMetaType);
						MetadataUtils.MetadataGetter<VBuffer<DvText>> metadataGetter = delegate(int iinfo, ref VBuffer<DvText> dst)
						{
							VBuffer<TMeta> vbuffer3 = default(VBuffer<TMeta>);
							getter.Invoke(ref vbuffer3);
							TermTransform.GetTextTerms<TMeta>(ref vbuffer3, stringMapper, ref dst);
						};
						bldr.AddGetter<VBuffer<DvText>>("KeyValues", new VectorType(TextType.Instance, this._map.OutputType.KeyCount), metadataGetter);
					}
					else
					{
						MetadataUtils.MetadataGetter<VBuffer<TMeta>> metadataGetter2 = delegate(int iinfo, ref VBuffer<TMeta> dst)
						{
							getter.Invoke(ref dst);
						};
						bldr.AddGetter<VBuffer<TMeta>>("KeyValues", new VectorType(srcMetaType.ItemType.AsPrimitive, this._map.OutputType.KeyCount), metadataGetter2);
					}
					return true;
				}

				// Token: 0x060006F3 RID: 1779 RVA: 0x00025AAC File Offset: 0x00023CAC
				public override void WriteTextTerms(TextWriter writer)
				{
					if (this._map.Count == 0)
					{
						return;
					}
					int source = this._parent.Infos[this._iinfo].Source;
					ColumnType metadataTypeOrNull = this._parent._input.Schema.GetMetadataTypeOrNull("KeyValues", source);
					if (metadataTypeOrNull == null || metadataTypeOrNull.VectorSize != this._map.ItemType.KeyCount || this._map.ItemType.KeyCount == 0 || !Utils.MarshalInvoke<PrimitiveType, TextWriter, bool>(new Func<PrimitiveType, TextWriter, bool>(this.WriteTextTermsCore<int>), metadataTypeOrNull.ItemType.RawType, metadataTypeOrNull.AsVector.ItemType, writer))
					{
						base.WriteTextTerms(writer);
					}
				}

				// Token: 0x060006F4 RID: 1780 RVA: 0x00025B5C File Offset: 0x00023D5C
				private bool WriteTextTermsCore<TMeta>(PrimitiveType srcMetaType, TextWriter writer)
				{
					KeyType asKey = this._map.ItemType.AsKey;
					KeyType keyType = new KeyType(6, asKey.Min, asKey.Count, true);
					Conversions instance = Conversions.Instance;
					ValueMapper<T, uint> valueMapper;
					bool flag;
					if (!instance.TryGetStandardConversion<T, uint>(asKey, keyType, out valueMapper, out flag))
					{
						return false;
					}
					int source = this._parent.Infos[this._iinfo].Source;
					VBuffer<TMeta> vbuffer = default(VBuffer<TMeta>);
					this._parent._input.Schema.GetMetadata<VBuffer<TMeta>>("KeyValues", source, ref vbuffer);
					if (vbuffer.Length != asKey.Count)
					{
						return false;
					}
					VBuffer<T> vbuffer2 = default(VBuffer<T>);
					this._map.GetTerms(ref vbuffer2);
					TMeta tmeta = default(TMeta);
					uint num = 0U;
					StringBuilder stringBuilder = null;
					ValueMapper<T, StringBuilder> stringConversion = instance.GetStringConversion<T>(this._map.ItemType);
					ValueMapper<TMeta, StringBuilder> stringConversion2 = instance.GetStringConversion<TMeta>(srcMetaType);
					writer.WriteLine("# Number of terms of key '{0}' indexing '{1}' value = {2}", this._map.ItemType, srcMetaType, this._map.Count);
					foreach (KeyValuePair<int, T> keyValuePair in vbuffer2.Items(true))
					{
						T value = keyValuePair.Value;
						valueMapper.Invoke(ref value, ref num);
						vbuffer.GetItemOrDefault((int)(num - 1U), ref tmeta);
						stringConversion.Invoke(ref value, ref stringBuilder);
						writer.Write("{0}\t{1}", keyValuePair.Key, stringBuilder.ToString());
						stringConversion2.Invoke(ref tmeta, ref stringBuilder);
						writer.WriteLine("\t{0}", stringBuilder.ToString());
					}
					return true;
				}
			}

			// Token: 0x02000158 RID: 344
			private sealed class Impl<T> : TermTransform.BoundTermMap.Base<T>
			{
				// Token: 0x060006F5 RID: 1781 RVA: 0x00025D04 File Offset: 0x00023F04
				public Impl(TermTransform.TermMap<T> map, TermTransform trans, int iinfo)
					: base(map, trans, iinfo)
				{
				}
			}
		}

		// Token: 0x02000159 RID: 345
		public abstract class ColumnBase : OneToOneColumn
		{
			// Token: 0x060006F6 RID: 1782 RVA: 0x00025D0F File Offset: 0x00023F0F
			protected override bool TryUnparseCore(StringBuilder sb)
			{
				return this.maxNumTerms == null && string.IsNullOrEmpty(this.terms) && this.sort == null && this.textKeyValues == null && base.TryUnparseCore(sb);
			}

			// Token: 0x0400038B RID: 907
			[Argument(0, HelpText = "Maximum number of terms to keep when auto-training", ShortName = "max")]
			public int? maxNumTerms;

			// Token: 0x0400038C RID: 908
			[Argument(0, HelpText = "Comma separated list of terms")]
			public string terms;

			// Token: 0x0400038D RID: 909
			[Argument(0, HelpText = "How items should be ordered when vectorized. By default, they will be in the order encountered. If by value items are sorted according to their default comparison, e.g., text sorting will be case sensitive (e.g., 'A' then 'Z' then 'a').")]
			public TermTransform.SortOrder? sort;

			// Token: 0x0400038E RID: 910
			[Argument(0, HelpText = "Whether key value metadata should be text, regardless of the actual input type", ShortName = "textkv", Hide = true)]
			public bool? textKeyValues;
		}

		// Token: 0x0200015A RID: 346
		public sealed class Column : TermTransform.ColumnBase
		{
			// Token: 0x060006F8 RID: 1784 RVA: 0x00025D58 File Offset: 0x00023F58
			public static TermTransform.Column Parse(string str)
			{
				TermTransform.Column column = new TermTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00025D77 File Offset: 0x00023F77
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x0200015B RID: 347
		public enum SortOrder : byte
		{
			// Token: 0x04000390 RID: 912
			Occurrence,
			// Token: 0x04000391 RID: 913
			Value
		}

		// Token: 0x0200015C RID: 348
		public abstract class ArgumentsBase
		{
			// Token: 0x04000392 RID: 914
			[Argument(0, HelpText = "Maximum number of terms to keep per column when auto-training", ShortName = "max", SortOrder = 5)]
			public int maxNumTerms = 1000000;

			// Token: 0x04000393 RID: 915
			[Argument(0, HelpText = "Comma separated list of terms", SortOrder = 105)]
			public string terms;

			// Token: 0x04000394 RID: 916
			[Argument(0, IsInputFileName = true, HelpText = "Data file containing the terms", ShortName = "data", SortOrder = 110)]
			public string dataFile;

			// Token: 0x04000395 RID: 917
			[Argument(4, HelpText = "Data loader", NullName = "<Auto>", SortOrder = 111)]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x04000396 RID: 918
			[Argument(0, HelpText = "Name of the text column containing the terms", ShortName = "termCol", SortOrder = 112)]
			public string termsColumn;

			// Token: 0x04000397 RID: 919
			[Argument(0, HelpText = "How items should be ordered when vectorized. By default, they will be in the order encountered. If by value items are sorted according to their default comparison, e.g., text sorting will be case sensitive (e.g., 'A' then 'Z' then 'a').", SortOrder = 113)]
			public TermTransform.SortOrder sort;

			// Token: 0x04000398 RID: 920
			[Argument(0, HelpText = "Whether key value metadata should be text, regardless of the actual input type", ShortName = "textkv", SortOrder = 114, Hide = true)]
			public bool textKeyValues;
		}

		// Token: 0x0200015D RID: 349
		public sealed class Arguments : TermTransform.ArgumentsBase
		{
			// Token: 0x04000399 RID: 921
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public TermTransform.Column[] column;
		}
	}
}
