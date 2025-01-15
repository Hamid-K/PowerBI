using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001E9 RID: 489
	public sealed class RankerPerInstanceTransform : IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003A393 File Offset: 0x00038593
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("RNK INST", 65537U, 65537U, 65537U, "RankerPerInstTransform", null);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0003A3B4 File Offset: 0x000385B4
		public IDataView Source
		{
			get
			{
				return this._transform.Source;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0003A3C1 File Offset: 0x000385C1
		public bool CanShuffle
		{
			get
			{
				return this._transform.CanShuffle;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0003A3CE File Offset: 0x000385CE
		public ISchema Schema
		{
			get
			{
				return this._transform.Schema;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003A3DB File Offset: 0x000385DB
		public RankerPerInstanceTransform(IHostEnvironment env, IDataView input, string labelCol, string scoreCol, string groupCol, int truncationLevel, double[] labelGains)
		{
			this._transform = new RankerPerInstanceTransform.Transform(env, input, labelCol, scoreCol, groupCol, truncationLevel, labelGains);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003A3F9 File Offset: 0x000385F9
		private RankerPerInstanceTransform(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			this._transform = new RankerPerInstanceTransform.Transform(ctx, env, input);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003A430 File Offset: 0x00038630
		public static RankerPerInstanceTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("RankerPerInstTransform");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(RankerPerInstanceTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<RankerPerInstanceTransform>(h, "Loading Model", (IChannel ch) => new RankerPerInstanceTransform(ctx, h, input));
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003A4C5 File Offset: 0x000386C5
		public void Save(ModelSaveContext ctx)
		{
			ctx.CheckAtModel();
			ctx.SetVersionInfo(RankerPerInstanceTransform.GetVersionInfo());
			this._transform.Save(ctx);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003A4E4 File Offset: 0x000386E4
		public long? GetRowCount(bool lazy = true)
		{
			return this._transform.GetRowCount(lazy);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003A4F2 File Offset: 0x000386F2
		public IRowCursor GetRowCursor(Func<int, bool> needCol, IRandom rand = null)
		{
			return this._transform.GetRowCursor(needCol, rand);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0003A501 File Offset: 0x00038701
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> needCol, int n, IRandom rand = null)
		{
			return this._transform.GetRowCursorSet(out consolidator, needCol, n, rand);
		}

		// Token: 0x040005B8 RID: 1464
		public const string LoaderSignature = "RankerPerInstTransform";

		// Token: 0x040005B9 RID: 1465
		private const string RegistrationName = "RankerPerInstTransform";

		// Token: 0x040005BA RID: 1466
		public const string Ndcg = "NDCG";

		// Token: 0x040005BB RID: 1467
		public const string Dcg = "DCG";

		// Token: 0x040005BC RID: 1468
		public const string MaxDcg = "MaxDCG";

		// Token: 0x040005BD RID: 1469
		private readonly RankerPerInstanceTransform.Transform _transform;

		// Token: 0x020001EA RID: 490
		private sealed class Transform : PerGroupTransformBase<short, float, RankerPerInstanceTransform.Transform.RowCursorState>
		{
			// Token: 0x06000AFC RID: 2812 RVA: 0x0003A514 File Offset: 0x00038714
			public Transform(IHostEnvironment env, IDataView input, string labelCol, string scoreCol, string groupCol, int truncationLevel, double[] labelGains)
				: base(env, input, labelCol, scoreCol, groupCol, "RankerPerInstTransform")
			{
				Contracts.CheckParam(this._host, 0 < truncationLevel && truncationLevel < 100, "truncationLevel", "Truncation level must be between 1 and 99");
				Contracts.CheckValue<double[]>(this._host, labelGains, "labelGains");
				this._truncationLevel = truncationLevel;
				this._labelGains = labelGains;
				this._bindings = new RankerPerInstanceTransform.Transform.Bindings(this._host, this._input.Schema, true, this._labelCol, this._scoreCol, this._groupCol, this._truncationLevel);
			}

			// Token: 0x06000AFD RID: 2813 RVA: 0x0003A5B0 File Offset: 0x000387B0
			public Transform(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
				: base(ctx, env, input, "RankerPerInstTransform")
			{
				this._truncationLevel = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, 0 < this._truncationLevel && this._truncationLevel < 100);
				this._labelGains = Utils.ReadDoubleArray(ctx.Reader);
				this._bindings = new RankerPerInstanceTransform.Transform.Bindings(this._host, input.Schema, false, this._labelCol, this._scoreCol, this._groupCol, this._truncationLevel);
			}

			// Token: 0x06000AFE RID: 2814 RVA: 0x0003A63E File Offset: 0x0003883E
			public override void Save(ModelSaveContext ctx)
			{
				base.Save(ctx);
				ctx.Writer.Write(this._truncationLevel);
				Utils.WriteDoubleArray(ctx.Writer, this._labelGains);
			}

			// Token: 0x06000AFF RID: 2815 RVA: 0x0003A669 File Offset: 0x00038869
			protected override PerGroupTransformBase<short, float, RankerPerInstanceTransform.Transform.RowCursorState>.BindingsBase GetBindings()
			{
				return this._bindings;
			}

			// Token: 0x06000B00 RID: 2816 RVA: 0x0003A6C8 File Offset: 0x000388C8
			protected override Delegate[] CreateGetters(RankerPerInstanceTransform.Transform.RowCursorState state, Func<int, bool> predicate)
			{
				Delegate[] array = new Delegate[this._bindings.InfoCount];
				if (predicate(0))
				{
					double[] ndcg = state.NdcgCur;
					ValueGetter<VBuffer<double>> valueGetter = delegate(ref VBuffer<double> dst)
					{
						this.Copy(ndcg, ref dst);
					};
					array[0] = valueGetter;
				}
				if (predicate(1))
				{
					double[] dcg = state.DcgCur;
					ValueGetter<VBuffer<double>> valueGetter2 = delegate(ref VBuffer<double> dst)
					{
						this.Copy(dcg, ref dst);
					};
					array[1] = valueGetter2;
				}
				if (predicate(2))
				{
					double[] maxDcg = state.MaxDcgCur;
					ValueGetter<VBuffer<double>> valueGetter3 = delegate(ref VBuffer<double> dst)
					{
						this.Copy(maxDcg, ref dst);
					};
					array[2] = valueGetter3;
				}
				return array;
			}

			// Token: 0x06000B01 RID: 2817 RVA: 0x0003A78C File Offset: 0x0003898C
			private void Copy(double[] src, ref VBuffer<double> dst)
			{
				double[] array = dst.Values;
				if (Utils.Size<double>(array) < src.Length)
				{
					array = new double[src.Length];
				}
				src.CopyTo(array, 0);
				dst = new VBuffer<double>(src.Length, array, null);
			}

			// Token: 0x06000B02 RID: 2818 RVA: 0x0003A7F8 File Offset: 0x000389F8
			protected override ValueGetter<short> GetLabelGetter(IRow row)
			{
				ValueGetter<float> lb = RowCursorUtils.GetLabelGetter(row, this._bindings.LabelIndex);
				return delegate(ref short dst)
				{
					float num = 0f;
					lb.Invoke(ref num);
					dst = (short)num;
				};
			}

			// Token: 0x06000B03 RID: 2819 RVA: 0x0003A82E File Offset: 0x00038A2E
			protected override ValueGetter<float> GetScoreGetter(IRow row)
			{
				return row.GetGetter<float>(this._bindings.ScoreIndex);
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x0003A841 File Offset: 0x00038A41
			protected override RankerPerInstanceTransform.Transform.RowCursorState InitializeState(IRow input)
			{
				return new RankerPerInstanceTransform.Transform.RowCursorState(this._truncationLevel);
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x0003A84E File Offset: 0x00038A4E
			protected override void ProcessExample(RankerPerInstanceTransform.Transform.RowCursorState state, short label, float score)
			{
				state.QueryLabels.Add(label);
				state.QueryOutputs.Add(score);
			}

			// Token: 0x06000B06 RID: 2822 RVA: 0x0003A868 File Offset: 0x00038A68
			protected override void UpdateState(RankerPerInstanceTransform.Transform.RowCursorState state)
			{
				RankerUtils.QueryMaxDCG(this._labelGains, this._truncationLevel, state.QueryLabels, state.QueryOutputs, state.MaxDcgCur);
				RankerUtils.QueryDCG(this._labelGains, this._truncationLevel, state.QueryLabels, state.QueryOutputs, state.DcgCur);
				for (int i = 0; i < this._truncationLevel; i++)
				{
					double num = ((state.MaxDcgCur[i] > 0.0) ? (state.DcgCur[i] / state.MaxDcgCur[i] * 100.0) : 0.0);
					state.NdcgCur[i] = num;
				}
				state.QueryLabels.Clear();
				state.QueryOutputs.Clear();
			}

			// Token: 0x040005BE RID: 1470
			private const int NdcgCol = 0;

			// Token: 0x040005BF RID: 1471
			private const int DcgCol = 1;

			// Token: 0x040005C0 RID: 1472
			private const int MaxDcgCol = 2;

			// Token: 0x040005C1 RID: 1473
			private readonly RankerPerInstanceTransform.Transform.Bindings _bindings;

			// Token: 0x040005C2 RID: 1474
			private readonly int _truncationLevel;

			// Token: 0x040005C3 RID: 1475
			private readonly double[] _labelGains;

			// Token: 0x020001EB RID: 491
			private sealed class Bindings : PerGroupTransformBase<short, float, RankerPerInstanceTransform.Transform.RowCursorState>.BindingsBase
			{
				// Token: 0x06000B07 RID: 2823 RVA: 0x0003A928 File Offset: 0x00038B28
				public Bindings(IExceptionContext ectx, ISchema input, bool user, string labelCol, string scoreCol, string groupCol, int truncationLevel)
					: base(ectx, input, labelCol, scoreCol, groupCol, user, new string[] { "NDCG", "DCG", "MaxDCG" })
				{
					this._truncationLevel = truncationLevel;
					this._outputType = new VectorType(NumberType.R8, this._truncationLevel);
					this._slotNamesType = new VectorType(TextType.Instance, this._truncationLevel);
					this._slotNamesGetter = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.SlotNamesGetter);
				}

				// Token: 0x06000B08 RID: 2824 RVA: 0x0003A9AA File Offset: 0x00038BAA
				protected override ColumnType GetColumnTypeCore(int iinfo)
				{
					return this._outputType;
				}

				// Token: 0x06000B09 RID: 2825 RVA: 0x0003A9B4 File Offset: 0x00038BB4
				protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
				{
					IEnumerable<KeyValuePair<string, ColumnType>> metadataTypesCore = base.GetMetadataTypesCore(iinfo);
					return MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(metadataTypesCore, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(this._slotNamesType, "SlotNames") });
				}

				// Token: 0x06000B0A RID: 2826 RVA: 0x0003A9F5 File Offset: 0x00038BF5
				protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
				{
					if (kind == "SlotNames")
					{
						return this._slotNamesType;
					}
					return base.GetMetadataTypeCore(kind, iinfo);
				}

				// Token: 0x06000B0B RID: 2827 RVA: 0x0003AA13 File Offset: 0x00038C13
				protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
				{
					if (kind == "SlotNames")
					{
						MetadataUtils.Marshal<VBuffer<DvText>, TValue>(this._slotNamesGetter, iinfo, ref value);
						return;
					}
					base.GetMetadataCore<TValue>(kind, iinfo, ref value);
				}

				// Token: 0x06000B0C RID: 2828 RVA: 0x0003AA3C File Offset: 0x00038C3C
				private void SlotNamesGetter(int iinfo, ref VBuffer<DvText> dst)
				{
					DvText[] array = dst.Values;
					if (Utils.Size<DvText>(array) < this._truncationLevel)
					{
						array = new DvText[this._truncationLevel];
					}
					for (int i = 0; i < this._truncationLevel; i++)
					{
						array[i] = new DvText(string.Format("{0}@{1}", (iinfo == 0) ? "NDCG" : ((iinfo == 1) ? "DCG" : "MaxDCG"), i + 1));
					}
					dst = new VBuffer<DvText>(this._truncationLevel, array, null);
				}

				// Token: 0x040005C4 RID: 1476
				private readonly ColumnType _outputType;

				// Token: 0x040005C5 RID: 1477
				private readonly ColumnType _slotNamesType;

				// Token: 0x040005C6 RID: 1478
				private readonly int _truncationLevel;

				// Token: 0x040005C7 RID: 1479
				private readonly MetadataUtils.MetadataGetter<VBuffer<DvText>> _slotNamesGetter;
			}

			// Token: 0x020001EC RID: 492
			public sealed class RowCursorState
			{
				// Token: 0x06000B0D RID: 2829 RVA: 0x0003AACC File Offset: 0x00038CCC
				public RowCursorState(int truncationLevel)
				{
					this.QueryLabels = new List<short>();
					this.QueryOutputs = new List<float>();
					this.NdcgCur = new double[truncationLevel];
					this.DcgCur = new double[truncationLevel];
					this.MaxDcgCur = new double[truncationLevel];
				}

				// Token: 0x040005C8 RID: 1480
				public readonly List<short> QueryLabels;

				// Token: 0x040005C9 RID: 1481
				public readonly List<float> QueryOutputs;

				// Token: 0x040005CA RID: 1482
				public readonly double[] NdcgCur;

				// Token: 0x040005CB RID: 1483
				public readonly double[] DcgCur;

				// Token: 0x040005CC RID: 1484
				public readonly double[] MaxDcgCur;
			}
		}
	}
}
