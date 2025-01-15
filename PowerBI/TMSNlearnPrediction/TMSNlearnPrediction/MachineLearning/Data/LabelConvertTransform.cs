using System;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000E8 RID: 232
	public sealed class LabelConvertTransform : OneToOneTransformBase
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0001A1FF File Offset: 0x000183FF
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("LABCONVT", 65537U, 65537U, 65537U, "LabelConvertTransform", null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001A220 File Offset: 0x00018420
		public LabelConvertTransform(LabelConvertTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "LabelConvert", Contracts.CheckRef<LabelConvertTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(RowCursorUtils.TestGetLabelGetter))
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001A24B File Offset: 0x0001844B
		private LabelConvertTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, null)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public static LabelConvertTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("LabelConvert");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(LabelConvertTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<LabelConvertTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(h, num == 4);
				return new LabelConvertTransform(ctx, h, input);
			});
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001A339 File Offset: 0x00018539
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(LabelConvertTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001A370 File Offset: 0x00018570
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return NumberType.Float;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A378 File Offset: 0x00018578
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				using (metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, new Func<string, int, bool>(this.PassThrough)))
				{
				}
			}
			metadata.Seal();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A3F0 File Offset: 0x000185F0
		private bool PassThrough(string kind, int iinfo)
		{
			return kind != "KeyValues";
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001A400 File Offset: 0x00018600
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			int source = this.Infos[iinfo].Source;
			input.Schema.GetColumnType(source);
			return RowCursorUtils.GetLabelGetter(input, source);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001A434 File Offset: 0x00018634
		protected override VectorType GetSlotTypeCore(int iinfo)
		{
			VectorType slotTypeSrc = this.Infos[iinfo].SlotTypeSrc;
			if (slotTypeSrc == null)
			{
				return null;
			}
			Interlocked.CompareExchange<VectorType>(ref this._slotType, new VectorType(NumberType.Float, slotTypeSrc), null);
			return this._slotType;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001A474 File Offset: 0x00018674
		protected override ISlotCursor GetSlotCursorCore(int iinfo)
		{
			ISlotCursor slotCursor = this.InputTranspose.GetSlotCursor(this.Infos[iinfo].Source);
			return new LabelConvertTransform.SlotCursor(this._host, slotCursor, this.GetSlotTypeCore(iinfo));
		}

		// Token: 0x04000243 RID: 579
		internal const string Summary = "Convert a label column into a standard floating point representation.";

		// Token: 0x04000244 RID: 580
		public const string LoaderSignature = "LabelConvertTransform";

		// Token: 0x04000245 RID: 581
		private const string RegistrationName = "LabelConvert";

		// Token: 0x04000246 RID: 582
		private VectorType _slotType;

		// Token: 0x020000E9 RID: 233
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x060004D0 RID: 1232 RVA: 0x0001A4B0 File Offset: 0x000186B0
			public static LabelConvertTransform.Column Parse(string str)
			{
				LabelConvertTransform.Column column = new LabelConvertTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x0001A4CF File Offset: 0x000186CF
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x020000EA RID: 234
		public sealed class Arguments
		{
			// Token: 0x04000247 RID: 583
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col")]
			public LabelConvertTransform.Column[] column;
		}

		// Token: 0x020000EB RID: 235
		private sealed class SlotCursor : SynchronizedCursorBase<ISlotCursor>, ISlotCursor, ICursor, ICounted, IDisposable
		{
			// Token: 0x060004D4 RID: 1236 RVA: 0x0001A4E8 File Offset: 0x000186E8
			public SlotCursor(IChannelProvider provider, ISlotCursor cursor, VectorType typeDst)
				: base(provider, cursor)
			{
				this._getter = RowCursorUtils.GetLabelGetter(base.Input);
				this._type = typeDst;
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x0001A50A File Offset: 0x0001870A
			public VectorType GetSlotType()
			{
				return this._type;
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0001A514 File Offset: 0x00018714
			public ValueGetter<VBuffer<TValue>> GetGetter<TValue>()
			{
				ValueGetter<VBuffer<TValue>> valueGetter = this._getter as ValueGetter<VBuffer<TValue>>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x04000248 RID: 584
			private readonly Delegate _getter;

			// Token: 0x04000249 RID: 585
			private readonly VectorType _type;
		}
	}
}
