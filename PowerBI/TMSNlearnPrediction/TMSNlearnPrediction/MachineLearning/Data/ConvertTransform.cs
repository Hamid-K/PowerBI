using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200027F RID: 639
	public sealed class ConvertTransform : OneToOneTransformBase
	{
		// Token: 0x06000E11 RID: 3601 RVA: 0x0004E537 File Offset: 0x0004C737
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CONVERTF", 65538U, 65538U, 65537U, "ConvertTransform", "ConvertFunction");
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0004E55C File Offset: 0x0004C75C
		public ConvertTransform(ConvertTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Convert", Contracts.CheckRef<ConvertTransform.Arguments>(env, args, "args").column, input, null)
		{
			this._exes = new ConvertTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				ConvertTransform.Column column = args.column[i];
				DataKind dataKind;
				KeyRange keyRange;
				if (column.resultType != null)
				{
					dataKind = column.resultType.Value;
					keyRange = column.keyRange;
				}
				else if (column.keyRange != null)
				{
					dataKind = (this.Infos[i].TypeSrc.IsKey ? this.Infos[i].TypeSrc.RawKind : 6);
					keyRange = column.keyRange;
				}
				else if (args.resultType != null)
				{
					dataKind = args.resultType.Value;
					keyRange = args.keyRange;
				}
				else if (args.keyRange != null)
				{
					dataKind = (this.Infos[i].TypeSrc.IsKey ? this.Infos[i].TypeSrc.RawKind : 6);
					keyRange = args.keyRange;
				}
				else
				{
					dataKind = 9;
					keyRange = null;
				}
				Contracts.CheckUserArg(this._host, Enum.IsDefined(typeof(DataKind), dataKind), "resultType");
				PrimitiveType primitiveType;
				if (!ConvertTransform.TryCreateEx(this._host, this.Infos[i], dataKind, keyRange, out primitiveType, out this._exes[i]))
				{
					throw Contracts.ExceptUserArg(this._host, "source", "source column '{0}' with item type '{1}' is not compatible with destination type '{2}'", new object[]
					{
						input.Schema.GetColumnName(this.Infos[i].Source),
						this.Infos[i].TypeSrc.ItemType,
						primitiveType
					});
				}
			}
			this.SetMetadata();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0004E728 File Offset: 0x0004C928
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				OneToOneTransformBase.ColInfo colInfo = this.Infos[i];
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, colInfo.Source, new Func<string, int, bool>(this.PassThrough)))
				{
					if (colInfo.TypeSrc.IsBool && this._exes[i].TypeDst.ItemType.IsNumber)
					{
						builder.AddPrimitive<DvBool>("IsNormalized", BoolType.Instance, DvBool.True);
					}
				}
			}
			metadata.Seal();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0004E7E0 File Offset: 0x0004C9E0
		private bool PassThrough(string kind, int iinfo)
		{
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			ColumnType typeDst = this._exes[iinfo].TypeDst;
			if (kind != null)
			{
				if (kind == "SlotNames")
				{
					return typeDst.IsKnownSizeVector;
				}
				if (kind == "KeyValues")
				{
					return typeSrc.ItemType.IsKey && typeDst.ItemType.IsKey && typeSrc.ItemType.KeyCount > 0 && typeSrc.ItemType.KeyCount == typeDst.ItemType.KeyCount;
				}
				if (kind == "IsNormalized")
				{
					return typeSrc.ItemType.IsNumber && typeDst.ItemType.IsNumber;
				}
			}
			return false;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0004E8A0 File Offset: 0x0004CAA0
		private ConvertTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, null)
		{
			this._exes = new ConvertTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				byte b = ctx.Reader.ReadByte();
				DataKind dataKind = b & 127;
				Contracts.CheckDecode(this._host, Enum.IsDefined(typeof(DataKind), dataKind));
				KeyRange keyRange = null;
				if ((b & 128) != 0)
				{
					keyRange = new KeyRange();
					keyRange.min = ctx.Reader.ReadUInt64();
					int num = ctx.Reader.ReadInt32();
					if (num != 0)
					{
						if (num < 0 || (long)(num - 1) > (long)(18446744073709551615UL - keyRange.min))
						{
							throw Contracts.ExceptDecode(this._host, "KeyType count too large");
						}
						keyRange.max = new ulong?(keyRange.min + (ulong)((long)(num - 1)));
					}
					keyRange.contiguous = Utils.ReadBoolByte(ctx.Reader);
				}
				PrimitiveType primitiveType;
				if (!ConvertTransform.TryCreateEx(this._host, this.Infos[i], dataKind, keyRange, out primitiveType, out this._exes[i]))
				{
					throw Contracts.ExceptDecode(this._host, "source is not of compatible type");
				}
			}
			this.SetMetadata();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0004EA1C File Offset: 0x0004CC1C
		public static ConvertTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Convert");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(ConvertTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<ConvertTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new ConvertTransform(ctx, h, input);
			});
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0004EAB4 File Offset: 0x0004CCB4
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ConvertTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				ConvertTransform.ColInfoEx colInfoEx = this._exes[i];
				if (!colInfoEx.HasKeyRange)
				{
					ctx.Writer.Write(colInfoEx.Kind);
				}
				else
				{
					KeyType asKey = colInfoEx.TypeDst.ItemType.AsKey;
					byte b = colInfoEx.Kind;
					b |= 128;
					ctx.Writer.Write(b);
					ctx.Writer.Write(asKey.Min);
					ctx.Writer.Write(asKey.Count);
					Utils.WriteBoolByte(ctx.Writer, asKey.Contiguous);
				}
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0004EB94 File Offset: 0x0004CD94
		private static bool TryCreateEx(IExceptionContext ectx, OneToOneTransformBase.ColInfo info, DataKind kind, KeyRange range, out PrimitiveType itemType, out ConvertTransform.ColInfoEx ex)
		{
			ex = null;
			ColumnType typeSrc = info.TypeSrc;
			if (range != null)
			{
				itemType = TypeParsingUtils.ConstructKeyType(new DataKind?(kind), range);
				if (!typeSrc.ItemType.IsKey && !typeSrc.ItemType.IsText)
				{
					return false;
				}
			}
			else if (!typeSrc.ItemType.IsKey)
			{
				itemType = PrimitiveType.FromKind(kind);
			}
			else
			{
				if (!KeyType.IsValidDataKind(kind))
				{
					itemType = PrimitiveType.FromKind(kind);
					return false;
				}
				KeyType asKey = typeSrc.ItemType.AsKey;
				int num = asKey.Count;
				ulong num2 = DataKindExtensions.ToMaxInt(kind);
				if ((long)num > (long)num2)
				{
					num = (int)num2;
				}
				itemType = new KeyType(kind, asKey.Min, num, asKey.Contiguous);
			}
			Delegate @delegate;
			bool flag;
			if (!Conversions.Instance.TryGetStandardConversion(typeSrc.ItemType, itemType, out @delegate, out flag))
			{
				return false;
			}
			ColumnType columnType = itemType;
			if (typeSrc.IsVector)
			{
				columnType = new VectorType(itemType, typeSrc.AsVector);
			}
			VectorType vectorType = null;
			if (info.SlotTypeSrc != null)
			{
				vectorType = new VectorType(itemType, info.SlotTypeSrc);
			}
			ex = new ConvertTransform.ColInfoEx(kind, range != null, columnType, vectorType);
			return true;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0004ECA5 File Offset: 0x0004CEA5
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._exes[iinfo].TypeDst;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0004ECB4 File Offset: 0x0004CEB4
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			ColumnType typeDst = this._exes[iinfo].TypeDst;
			if (!typeDst.IsVector)
			{
				return RowCursorUtils.GetGetterAs(typeDst, input, this.Infos[iinfo].Source);
			}
			return RowCursorUtils.GetVecGetterAs(typeDst.AsVector.ItemType, input, this.Infos[iinfo].Source);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0004ED1C File Offset: 0x0004CF1C
		protected override VectorType GetSlotTypeCore(int iinfo)
		{
			return this._exes[iinfo].SlotTypeDst;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0004ED2C File Offset: 0x0004CF2C
		protected override ISlotCursor GetSlotCursorCore(int iinfo)
		{
			ISlotCursor slotCursor = this.InputTranspose.GetSlotCursor(this.Infos[iinfo].Source);
			return new ConvertTransform.SlotCursor(this._host, slotCursor, this._exes[iinfo].SlotTypeDst);
		}

		// Token: 0x040007F8 RID: 2040
		internal const string Summary = "Converts a column to a different type, using standard conversions.";

		// Token: 0x040007F9 RID: 2041
		public const string LoaderSignature = "ConvertTransform";

		// Token: 0x040007FA RID: 2042
		internal const string LoaderSignatureOld = "ConvertFunction";

		// Token: 0x040007FB RID: 2043
		private const string RegistrationName = "Convert";

		// Token: 0x040007FC RID: 2044
		private readonly ConvertTransform.ColInfoEx[] _exes;

		// Token: 0x02000280 RID: 640
		public class Column : OneToOneColumn
		{
			// Token: 0x06000E1D RID: 3613 RVA: 0x0004ED6C File Offset: 0x0004CF6C
			public static ConvertTransform.Column Parse(string str)
			{
				ConvertTransform.Column column = new ConvertTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000E1E RID: 3614 RVA: 0x0004ED8C File Offset: 0x0004CF8C
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
				DataKind dataKind;
				if (!TypeParsingUtils.TryParseDataKind(text, out dataKind, out this.keyRange))
				{
					return false;
				}
				this.resultType = ((dataKind == null) ? null : new DataKind?(dataKind));
				return true;
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x0004EDD8 File Offset: 0x0004CFD8
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.resultType == null && this.keyRange == null)
				{
					return this.TryUnparseCore(sb);
				}
				if (!base.TrySanitize())
				{
					return false;
				}
				if (CmdQuoter.NeedsQuoting(this.name) || CmdQuoter.NeedsQuoting(this.source))
				{
					return false;
				}
				int length = sb.Length;
				sb.Append(this.name);
				sb.Append(':');
				if (this.resultType != null)
				{
					sb.Append(DataKindExtensions.GetString(this.resultType.Value));
				}
				if (this.keyRange != null)
				{
					sb.Append('[');
					if (!this.keyRange.TryUnparse(sb))
					{
						sb.Length = length;
						return false;
					}
					sb.Append(']');
				}
				sb.Append(':');
				sb.Append(this.source);
				return true;
			}

			// Token: 0x040007FD RID: 2045
			[Argument(0, HelpText = "The result type", ShortName = "type")]
			public DataKind? resultType;

			// Token: 0x040007FE RID: 2046
			[Argument(4, HelpText = "For a key column, this defines the range of values", ShortName = "key")]
			public KeyRange keyRange;
		}

		// Token: 0x02000281 RID: 641
		public class Arguments
		{
			// Token: 0x040007FF RID: 2047
			[Argument(4, HelpText = "New column definition(s) (optional form: name:type:src)", ShortName = "col", SortOrder = 1)]
			public ConvertTransform.Column[] column;

			// Token: 0x04000800 RID: 2048
			[Argument(0, HelpText = "The result type", ShortName = "type", SortOrder = 2)]
			public DataKind? resultType;

			// Token: 0x04000801 RID: 2049
			[Argument(4, HelpText = "For a key column, this defines the range of values", ShortName = "key")]
			public KeyRange keyRange;
		}

		// Token: 0x02000282 RID: 642
		private sealed class ColInfoEx
		{
			// Token: 0x06000E22 RID: 3618 RVA: 0x0004EEC1 File Offset: 0x0004D0C1
			public ColInfoEx(DataKind kind, bool hasKeyRange, ColumnType type, VectorType slotType)
			{
				this.Kind = kind;
				this.HasKeyRange = hasKeyRange;
				this.TypeDst = type;
				this.SlotTypeDst = slotType;
			}

			// Token: 0x04000802 RID: 2050
			public readonly DataKind Kind;

			// Token: 0x04000803 RID: 2051
			public readonly bool HasKeyRange;

			// Token: 0x04000804 RID: 2052
			public readonly ColumnType TypeDst;

			// Token: 0x04000805 RID: 2053
			public readonly VectorType SlotTypeDst;
		}

		// Token: 0x02000283 RID: 643
		private sealed class SlotCursor : SynchronizedCursorBase<IRowCursor>, ISlotCursor, ICursor, ICounted, IDisposable
		{
			// Token: 0x06000E23 RID: 3619 RVA: 0x0004EEE6 File Offset: 0x0004D0E6
			public SlotCursor(IChannelProvider provider, ISlotCursor cursor, VectorType typeDst)
				: base(provider, TransposerUtils.GetRowCursorShim(provider, cursor))
			{
				this._getter = RowCursorUtils.GetVecGetterAs(typeDst.ItemType, base.Input, 0);
				this._type = typeDst;
			}

			// Token: 0x06000E24 RID: 3620 RVA: 0x0004EF15 File Offset: 0x0004D115
			public VectorType GetSlotType()
			{
				return this._type;
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x0004EF20 File Offset: 0x0004D120
			public ValueGetter<VBuffer<TValue>> GetGetter<TValue>()
			{
				ValueGetter<VBuffer<TValue>> valueGetter = this._getter as ValueGetter<VBuffer<TValue>>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x04000806 RID: 2054
			private readonly Delegate _getter;

			// Token: 0x04000807 RID: 2055
			private readonly VectorType _type;
		}
	}
}
