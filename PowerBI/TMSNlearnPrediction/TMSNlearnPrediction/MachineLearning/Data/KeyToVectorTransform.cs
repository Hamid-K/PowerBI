using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200025B RID: 603
	public sealed class KeyToVectorTransform : OneToOneTransformBase
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x0004AC5D File Offset: 0x00048E5D
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("KEY2VECT", 65537U, 65537U, 65537U, "KeyToVectorTransform", null);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0004AC7E File Offset: 0x00048E7E
		public static string TestColumnType(ColumnType type)
		{
			if (type.ItemType.KeyCount > 0)
			{
				return null;
			}
			return "Expected Key type of known cardinality";
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0004AC98 File Offset: 0x00048E98
		public KeyToVectorTransform(KeyToVectorTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "KeyToVector", Contracts.CheckRef<KeyToVectorTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(KeyToVectorTransform.TestColumnType))
		{
			this._bag = new bool[this.Infos.Length];
			this._concat = new bool[this.Infos.Length];
			this._types = new VectorType[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				KeyToVectorTransform.Column column = args.column[i];
				this._bag[i] = column.bag ?? args.bag;
				KeyToVectorTransform.ComputeType(this, this._input.Schema, i, this.Infos[i], this._bag[i], base.Metadata, out this._types[i], out this._concat[i]);
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0004AD98 File Offset: 0x00048F98
		private KeyToVectorTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(KeyToVectorTransform.TestColumnType))
		{
			int num = this.Infos.Length;
			this._bag = new bool[num];
			this._concat = new bool[this.Infos.Length];
			this._types = new VectorType[num];
			for (int i = 0; i < num; i++)
			{
				this._bag[i] = Utils.ReadBoolByte(ctx.Reader);
				KeyToVectorTransform.ComputeType(this, this._input.Schema, i, this.Infos[i], this._bag[i], base.Metadata, out this._types[i], out this._concat[i]);
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0004AEA0 File Offset: 0x000490A0
		public static KeyToVectorTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("KeyToVector");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<KeyToVectorTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new KeyToVectorTransform(ctx, h, input);
			});
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0004AF28 File Offset: 0x00049128
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(KeyToVectorTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._bag.Length; i++)
			{
				Utils.WriteBoolByte(ctx.Writer, this._bag[i]);
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0004AF90 File Offset: 0x00049190
		private static void ComputeType(KeyToVectorTransform trans, ISchema input, int iinfo, OneToOneTransformBase.ColInfo info, bool bag, MetadataDispatcher md, out VectorType type, out bool concat)
		{
			int keyCount = info.TypeSrc.ItemType.KeyCount;
			ColumnType columnType = input.GetMetadataTypeOrNull("KeyValues", info.Source);
			if (columnType == null || !columnType.IsKnownSizeVector || !columnType.ItemType.IsText || columnType.VectorSize != keyCount)
			{
				columnType = null;
			}
			using (MetadataDispatcher.Builder builder = md.BuildMetadata(iinfo))
			{
				if (bag || info.TypeSrc.ValueCount == 1)
				{
					concat = false;
					type = new VectorType(NumberType.Float, keyCount);
					if (columnType != null)
					{
						builder.AddGetter<VBuffer<DvText>>("SlotNames", columnType, new MetadataUtils.MetadataGetter<VBuffer<DvText>>(trans.GetKeyNames));
					}
				}
				else
				{
					concat = true;
					type = new VectorType(NumberType.Float, new int[]
					{
						info.TypeSrc.ValueCount,
						keyCount
					});
					if (columnType != null && type.VectorSize > 0)
					{
						builder.AddGetter<VBuffer<DvText>>("SlotNames", new VectorType(TextType.Instance, type), new MetadataUtils.MetadataGetter<VBuffer<DvText>>(trans.GetSlotNames));
					}
				}
				if (!bag || info.TypeSrc.ValueCount == 1)
				{
					builder.AddPrimitive<DvBool>("IsNormalized", BoolType.Instance, DvBool.True);
				}
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0004B0D0 File Offset: 0x000492D0
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0004B0DA File Offset: 0x000492DA
		private void GetKeyNames(int iinfo, ref VBuffer<DvText> dst)
		{
			this._input.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", this.Infos[iinfo].Source, ref dst);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0004B100 File Offset: 0x00049300
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			ColumnType metadataTypeOrNull = this._input.Schema.GetMetadataTypeOrNull("SlotNames", this.Infos[iinfo].Source);
			if (metadataTypeOrNull != null && metadataTypeOrNull.VectorSize == typeSrc.VectorSize && metadataTypeOrNull.ItemType.IsText)
			{
				this._input.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", this.Infos[iinfo].Source, ref vbuffer);
				Contracts.Check(this._host, vbuffer.Length == typeSrc.VectorSize);
			}
			else
			{
				vbuffer = VBufferUtils.CreateEmpty<DvText>(typeSrc.VectorSize);
			}
			int keyCount = typeSrc.ItemType.KeyCount;
			int vectorSize = this._types[iinfo].VectorSize;
			VBuffer<DvText> vbuffer2 = default(VBuffer<DvText>);
			this._input.Schema.GetMetadata<VBuffer<DvText>>("KeyValues", this.Infos[iinfo].Source, ref vbuffer2);
			Contracts.Check(this._host, vbuffer2.Length == keyCount);
			DvText[] array = new DvText[keyCount];
			vbuffer2.CopyTo(array);
			DvText[] array2 = dst.Values;
			if (Utils.Size<DvText>(array2) < vectorSize)
			{
				array2 = new DvText[vectorSize];
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(true))
			{
				stringBuilder.Clear();
				if (keyValuePair.Value.HasChars)
				{
					keyValuePair.Value.AddToStringBuilder(stringBuilder);
				}
				else
				{
					stringBuilder.Append('[').Append(keyValuePair.Key).Append(']');
				}
				stringBuilder.Append('.');
				int length = stringBuilder.Length;
				foreach (DvText dvText in array)
				{
					stringBuilder.Length = length;
					dvText.AddToStringBuilder(stringBuilder);
					array2[num++] = new DvText(stringBuilder.ToString());
				}
			}
			dst = new VBuffer<DvText>(vectorSize, array2, dst.Indices);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0004B358 File Offset: 0x00049558
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			if (!colInfo.TypeSrc.IsVector)
			{
				return this.MakeGetterOne(input, iinfo);
			}
			if (this._bag[iinfo])
			{
				return this.MakeGetterBag(input, iinfo);
			}
			return this.MakeGetterInd(input, iinfo);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0004B458 File Offset: 0x00049658
		private ValueGetter<VBuffer<float>> MakeGetterOne(IRow input, int iinfo)
		{
			int size = this.Infos[iinfo].TypeSrc.KeyCount;
			ValueGetter<uint> getSrc = RowCursorUtils.GetGetterAs<uint>(NumberType.U4, input, this.Infos[iinfo].Source);
			uint src = 0U;
			return delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				if (src == 0U || (ulong)src > (ulong)((long)size))
				{
					dst = new VBuffer<float>(size, 0, dst.Values, dst.Indices);
					return;
				}
				float[] array = dst.Values;
				int[] array2 = dst.Indices;
				if (Utils.Size<float>(array) < 1)
				{
					array = new float[1];
				}
				if (Utils.Size<int>(array2) < 1)
				{
					array2 = new int[1];
				}
				array[0] = 1f;
				array2[0] = (int)(src - 1U);
				dst = new VBuffer<float>(size, 1, array, array2);
			};
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0004B56C File Offset: 0x0004976C
		private ValueGetter<VBuffer<float>> MakeGetterBag(IRow input, int iinfo)
		{
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			int size = colInfo.TypeSrc.ItemType.KeyCount;
			int cv = colInfo.TypeSrc.VectorSize;
			ValueGetter<VBuffer<uint>> getSrc = RowCursorUtils.GetVecGetterAs<uint>(NumberType.U4, input, colInfo.Source);
			VBuffer<uint> src = default(VBuffer<uint>);
			FloatBufferBuilder bldr = new FloatBufferBuilder();
			return delegate(ref VBuffer<float> dst)
			{
				bldr.Reset(size, false);
				getSrc.Invoke(ref src);
				Contracts.Check(this._host, cv == 0 || src.Length == cv);
				uint[] values = src.Values;
				int count = src.Count;
				for (int i = 0; i < count; i++)
				{
					uint num = values[i] - 1U;
					if ((ulong)num < (ulong)((long)size))
					{
						bldr.AddFeature((int)num, 1f);
					}
				}
				bldr.GetResult(ref dst);
			};
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0004B754 File Offset: 0x00049954
		private ValueGetter<VBuffer<float>> MakeGetterInd(IRow input, int iinfo)
		{
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			int size = colInfo.TypeSrc.ItemType.KeyCount;
			int cv = colInfo.TypeSrc.VectorSize;
			ValueGetter<VBuffer<uint>> getSrc = RowCursorUtils.GetVecGetterAs<uint>(NumberType.U4, input, colInfo.Source);
			VBuffer<uint> src = default(VBuffer<uint>);
			return delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				int length = src.Length;
				Contracts.Check(this._host, length == cv || cv == 0);
				float[] array = dst.Values;
				int[] array2 = dst.Indices;
				int num = checked(size * length);
				int count = src.Count;
				if (Utils.Size<float>(array) < count)
				{
					array = new float[count];
				}
				if (Utils.Size<int>(array2) < count)
				{
					array2 = new int[count];
				}
				uint[] values = src.Values;
				int num2 = 0;
				if (src.IsDense)
				{
					for (int i = 0; i < count; i++)
					{
						uint num3 = values[i] - 1U;
						if (num3 < (uint)size)
						{
							array[num2] = 1f;
							array2[num2++] = i * size + (int)num3;
						}
					}
				}
				else
				{
					int[] indices = src.Indices;
					for (int j = 0; j < count; j++)
					{
						uint num4 = values[j] - 1U;
						if (num4 < (uint)size)
						{
							array[num2] = 1f;
							array2[num2++] = indices[j] * size + (int)num4;
						}
					}
				}
				dst = new VBuffer<float>(num, num2, array, array2);
			};
		}

		// Token: 0x04000796 RID: 1942
		internal const string Summary = "Converts a key column to an indicator vector.";

		// Token: 0x04000797 RID: 1943
		public const string LoaderSignature = "KeyToVectorTransform";

		// Token: 0x04000798 RID: 1944
		private const string RegistrationName = "KeyToVector";

		// Token: 0x04000799 RID: 1945
		private readonly bool[] _bag;

		// Token: 0x0400079A RID: 1946
		private readonly bool[] _concat;

		// Token: 0x0400079B RID: 1947
		private readonly VectorType[] _types;

		// Token: 0x0200025C RID: 604
		public abstract class ColumnBase : OneToOneColumn
		{
			// Token: 0x06000D7F RID: 3455 RVA: 0x0004B7CD File Offset: 0x000499CD
			protected override bool TryUnparseCore(StringBuilder sb)
			{
				return this.bag == null && base.TryUnparseCore(sb);
			}

			// Token: 0x06000D80 RID: 3456 RVA: 0x0004B7E5 File Offset: 0x000499E5
			protected override bool TryUnparseCore(StringBuilder sb, string extra)
			{
				return this.bag == null && base.TryUnparseCore(sb, extra);
			}

			// Token: 0x0400079C RID: 1948
			[Argument(0, HelpText = "Whether to combine multiple indicator vectors into a single bag vector instead of concatenating them. This is only relevant when the input is a vector.")]
			public bool? bag;
		}

		// Token: 0x0200025D RID: 605
		public sealed class Column : KeyToVectorTransform.ColumnBase
		{
			// Token: 0x06000D82 RID: 3458 RVA: 0x0004B808 File Offset: 0x00049A08
			public static KeyToVectorTransform.Column Parse(string str)
			{
				KeyToVectorTransform.Column column = new KeyToVectorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000D83 RID: 3459 RVA: 0x0004B827 File Offset: 0x00049A27
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x0200025E RID: 606
		public sealed class Arguments
		{
			// Token: 0x0400079D RID: 1949
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public KeyToVectorTransform.Column[] column;

			// Token: 0x0400079E RID: 1950
			[Argument(0, HelpText = "Whether to combine multiple indicator vectors into a single bag vector instead of concatenating them. This is only relevant when the input is a vector.")]
			public bool bag;
		}
	}
}
