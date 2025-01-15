using System;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000A7 RID: 167
	public sealed class DropSlotsTransform : OneToOneTransformBase
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00012D84 File Offset: 0x00010F84
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("DROPSLOT", 65537U, 65537U, 65537U, "DropSlotsTransform", null);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00012DA8 File Offset: 0x00010FA8
		public DropSlotsTransform(DropSlotsTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(Contracts.CheckRef<IHostEnvironment>(env, "env"), "DropSlots", Contracts.CheckRef<DropSlotsTransform.Arguments>(env, args, "args").column, input, null)
		{
			Contracts.CheckNonEmpty<DropSlotsTransform.Column>(this._host, args.column, "column");
			int num = this.Infos.Length;
			this._exes = new DropSlotsTransform.ColInfoEx[num];
			for (int i = 0; i < num; i++)
			{
				DropSlotsTransform.Column column = args.column[i];
				int[] array;
				int[] array2;
				this.GetSlotsMinMax(column, out array, out array2);
				int[] array3;
				bool flag;
				ColumnType columnType;
				this.ComputeType(this._input.Schema, array, array2, i, out array3, out flag, out columnType);
				this._exes[i] = new DropSlotsTransform.ColInfoEx(array, array2, array3, flag, columnType);
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00012E88 File Offset: 0x00011088
		public static DropSlotsTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("DropSlots");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(DropSlotsTransform.GetVersionInfo());
			return HostExtensions.Apply<DropSlotsTransform>(h, "Loading Model", (IChannel ch) => new DropSlotsTransform(ctx, h, input));
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00012F20 File Offset: 0x00011120
		private DropSlotsTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, null)
		{
			int num = this.Infos.Length;
			this._exes = new DropSlotsTransform.ColInfoEx[num];
			for (int i = 0; i < num; i++)
			{
				int[] array = Utils.ReadIntArray(ctx.Reader);
				Contracts.CheckDecode(this._host, Utils.Size<int>(array) > 0);
				int[] array2 = Utils.ReadIntArray(ctx.Reader, array.Length);
				int[] array3;
				bool flag;
				ColumnType columnType;
				this.ComputeType(input.Schema, array, array2, i, out array3, out flag, out columnType);
				this._exes[i] = new DropSlotsTransform.ColInfoEx(array, array2, array3, flag, columnType);
				Contracts.CheckDecode(this._host, this.AreRangesValid(i));
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00012FD0 File Offset: 0x000111D0
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(DropSlotsTransform.GetVersionInfo());
			base.SaveBase(ctx);
			for (int i = 0; i < this.Infos.Length; i++)
			{
				DropSlotsTransform.ColInfoEx colInfoEx = this._exes[i];
				Utils.WriteIntArray(ctx.Writer, colInfoEx.SlotsMin);
				Utils.WriteIntsNoCount(ctx.Writer, colInfoEx.SlotsMax, colInfoEx.SlotsMax.Length);
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001304C File Offset: 0x0001124C
		private void GetSlotsMinMax(DropSlotsTransform.Column col, out int[] slotsMin, out int[] slotsMax)
		{
			slotsMin = new int[col.slots.Length];
			slotsMax = new int[col.slots.Length];
			for (int i = 0; i < col.slots.Length; i++)
			{
				DropSlotsTransform.Range range = col.slots[i];
				Contracts.CheckUserArg(this._host, range.IsValid(), "range", "The range min and max must be non-negative and min must be less than or equal to max.");
				slotsMin[i] = range.min;
				slotsMax[i] = range.max ?? 2147483646;
			}
			Array.Sort<int, int>(slotsMin, slotsMax);
			int num = 0;
			for (int j = 1; j < col.slots.Length; j++)
			{
				if (slotsMin[j] <= slotsMax[num] + 1)
				{
					slotsMax[num] = Math.Max(slotsMax[num], slotsMax[j]);
				}
				else
				{
					num++;
					slotsMin[num] = slotsMin[j];
					slotsMax[num] = slotsMax[j];
				}
			}
			num++;
			Array.Resize<int>(ref slotsMin, num);
			Array.Resize<int>(ref slotsMax, num);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00013140 File Offset: 0x00011340
		private void ComputeType(ISchema input, int[] slotsMin, int[] slotsMax, int iinfo, out int[] lengthReduction, out bool suppressed, out ColumnType type)
		{
			lengthReduction = new int[slotsMax.Length];
			int num = 0;
			for (int i = 0; i < slotsMax.Length; i++)
			{
				int num2 = slotsMax[i] + 1;
				num += num2 - slotsMin[i];
				lengthReduction[i] = num;
			}
			using (MetadataDispatcher.Builder builder = base.Metadata.BuildMetadata(iinfo, input, this.Infos[iinfo].Source, "IsNormalized"))
			{
				ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
				if (!typeSrc.IsVector)
				{
					type = typeSrc;
					suppressed = slotsMin.Length > 0 && slotsMin[0] == 0;
				}
				else if (!typeSrc.IsKnownSizeVector)
				{
					type = typeSrc;
					suppressed = false;
				}
				else
				{
					int num3 = this.ComputeLength(typeSrc.ValueCount, slotsMin, slotsMax, lengthReduction);
					bool flag = MetadataUtils.HasSlotNames(input, this.Infos[iinfo].Source, this.Infos[iinfo].TypeSrc.VectorSize);
					type = new VectorType(typeSrc.ItemType.AsPrimitive, Math.Max(num3, 1));
					suppressed = num3 == 0;
					if (flag && num3 > 0)
					{
						builder.AddGetter<VBuffer<DvText>>("SlotNames", new VectorType(TextType.Instance, num3), new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames));
					}
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00013298 File Offset: 0x00011498
		private int ComputeLength(int srcLength, int[] slotsMin, int[] slotsMax, int[] lengthReduction)
		{
			int num = Utils.FindIndexSorted(slotsMin, srcLength);
			if (num == 0)
			{
				return srcLength;
			}
			num--;
			return srcLength - lengthReduction[num] + Math.Max(slotsMax[num] - srcLength + 1, 0);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000132CC File Offset: 0x000114CC
		private bool AreRangesValid(int iinfo)
		{
			DropSlotsTransform.ColInfoEx colInfoEx = this._exes[iinfo];
			int num = -2;
			for (int i = 0; i < colInfoEx.SlotsMin.Length; i++)
			{
				if (0 > colInfoEx.SlotsMin[i] || colInfoEx.SlotsMin[i] >= 2147483647)
				{
					return false;
				}
				if (0 > colInfoEx.SlotsMax[i] || colInfoEx.SlotsMax[i] >= 2147483647)
				{
					return false;
				}
				if (colInfoEx.SlotsMin[i] > colInfoEx.SlotsMax[i])
				{
					return false;
				}
				if (colInfoEx.SlotsMin[i] - 1 <= num)
				{
					return false;
				}
				num = colInfoEx.SlotsMax[i];
			}
			return true;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001335D File Offset: 0x0001155D
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._exes[iinfo].TypeDst;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001336C File Offset: 0x0001156C
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
			this._input.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", this.Infos[iinfo].Source, ref vbuffer);
			this.DropSlots<DvText>(ref vbuffer, ref dst, iinfo);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000133AF File Offset: 0x000115AF
		protected override void ActivateSourceColumns(int iinfo, bool[] active)
		{
			if (!this._exes[iinfo].Suppressed)
			{
				active[this.Infos[iinfo].Source] = true;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000133D0 File Offset: 0x000115D0
		protected override bool WantParallelCursors(Func<int, bool> predicate)
		{
			for (int i = 0; i < this.Infos.Length; i++)
			{
				int num = base.ColumnIndex(i);
				if (predicate(num))
				{
					DropSlotsTransform.ColInfoEx colInfoEx = this._exes[i];
					OneToOneTransformBase.ColInfo colInfo = this.Infos[i];
					if (!colInfoEx.Suppressed && (!colInfoEx.TypeDst.IsKnownSizeVector || colInfoEx.TypeDst.ValueCount != colInfo.TypeSrc.ValueCount))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00013444 File Offset: 0x00011644
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			if (!typeSrc.IsVector)
			{
				if (this._exes[iinfo].Suppressed)
				{
					return this.MakeOneTrivialGetter(input, iinfo);
				}
				return base.GetSrcGetter(typeSrc, input, this.Infos[iinfo].Source);
			}
			else
			{
				if (this._exes[iinfo].Suppressed)
				{
					return this.MakeVecTrivialGetter(input, iinfo);
				}
				return this.MakeVecGetter(input, iinfo);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000134BC File Offset: 0x000116BC
		private Delegate MakeOneTrivialGetter(IRow input, int iinfo)
		{
			Func<ValueGetter<int>> func = new Func<ValueGetter<int>>(this.MakeOneTrivialGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[0]);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00013516 File Offset: 0x00011716
		private ValueGetter<TDst> MakeOneTrivialGetter<TDst>()
		{
			return new ValueGetter<TDst>(this.OneTrivialGetter<TDst>);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00013524 File Offset: 0x00011724
		private void OneTrivialGetter<TDst>(ref TDst value)
		{
			value = default(TDst);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00013530 File Offset: 0x00011730
		private Delegate MakeVecTrivialGetter(IRow input, int iinfo)
		{
			Func<ValueGetter<VBuffer<int>>> func = new Func<ValueGetter<VBuffer<int>>>(this.MakeVecTrivialGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.ItemType.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[0]);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001358F File Offset: 0x0001178F
		private ValueGetter<VBuffer<TDst>> MakeVecTrivialGetter<TDst>()
		{
			return new ValueGetter<VBuffer<TDst>>(this.VecTrivialGetter<TDst>);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001359D File Offset: 0x0001179D
		private void VecTrivialGetter<TDst>(ref VBuffer<TDst> value)
		{
			value = new VBuffer<TDst>(1, 0, value.Values, value.Indices);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000135B8 File Offset: 0x000117B8
		private Delegate MakeVecGetter(IRow input, int iinfo)
		{
			Func<IRow, int, ValueGetter<VBuffer<int>>> func = new Func<IRow, int, ValueGetter<VBuffer<int>>>(this.MakeVecGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.ItemType.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { input, iinfo });
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001365C File Offset: 0x0001185C
		private ValueGetter<VBuffer<TDst>> MakeVecGetter<TDst>(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<TDst>> srcGetter = base.GetSrcGetter<VBuffer<TDst>>(input, iinfo);
			ColumnType typeDst = this._exes[iinfo].TypeDst;
			if (typeDst.IsKnownSizeVector && typeDst.ValueCount == this.Infos[iinfo].TypeSrc.ValueCount)
			{
				return srcGetter;
			}
			VBuffer<TDst> buffer = default(VBuffer<TDst>);
			return delegate(ref VBuffer<TDst> value)
			{
				srcGetter.Invoke(ref buffer);
				this.DropSlots<TDst>(ref buffer, ref value, iinfo);
			};
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000136EC File Offset: 0x000118EC
		private void DropSlots<TDst>(ref VBuffer<TDst> src, ref VBuffer<TDst> dst, int iinfo)
		{
			DropSlotsTransform.ColInfoEx colInfoEx = this._exes[iinfo];
			if (src.Length <= colInfoEx.SlotsMin[0])
			{
				Utils.Swap<VBuffer<TDst>>(ref src, ref dst);
				return;
			}
			int num;
			if (colInfoEx.TypeDst.IsKnownSizeVector)
			{
				num = colInfoEx.TypeDst.ValueCount;
			}
			else
			{
				num = this.ComputeLength(src.Length, colInfoEx.SlotsMin, colInfoEx.SlotsMax, colInfoEx.LengthReduction);
			}
			TDst[] array = dst.Values;
			if (num == 0)
			{
				dst = new VBuffer<TDst>(1, 0, dst.Values, dst.Indices);
				return;
			}
			if (src.IsDense)
			{
				if (Utils.Size<TDst>(array) < num)
				{
					array = new TDst[num];
				}
				int num2 = 0;
				int i = 0;
				for (int j = 0; j < colInfoEx.SlotsMax.Length; j++)
				{
					if (i >= src.Length)
					{
						break;
					}
					int num3 = Math.Min(colInfoEx.SlotsMin[j], src.Length);
					while (i < num3)
					{
						array[num2++] = src.Values[i++];
					}
					i = colInfoEx.SlotsMax[j] + 1;
				}
				while (i < src.Length)
				{
					array[num2++] = src.Values[i++];
				}
				dst = new VBuffer<TDst>(num, array, dst.Indices);
				return;
			}
			int num4 = Math.Min(src.Count, num);
			int[] array2 = dst.Indices;
			if (Utils.Size<int>(array2) < num4)
			{
				array2 = new int[num4];
			}
			if (Utils.Size<TDst>(array) < num4)
			{
				array = new TDst[num4];
			}
			int num5 = 0;
			int k = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = colInfoEx.SlotsMin[num7];
			int num9 = colInfoEx.SlotsMax[num7];
			while (k < src.Count)
			{
				int num10 = src.Indices[k];
				if (num10 < num8)
				{
					array2[num5] = num10 - num6;
					array[num5++] = src.Values[k++];
				}
				else if (num10 <= num9)
				{
					k++;
				}
				else
				{
					while (++num7 < colInfoEx.SlotsMax.Length && colInfoEx.SlotsMax[num7] < num10)
					{
						if (colInfoEx.SlotsMax.Length - num7 >= 20 && colInfoEx.SlotsMax[num7 + 10] < num10)
						{
							num7 = Utils.FindIndexSorted(colInfoEx.SlotsMax, num7 + 10, colInfoEx.SlotsMax.Length, num10);
							break;
						}
					}
					if (num7 < colInfoEx.SlotsMax.Length)
					{
						num8 = colInfoEx.SlotsMin[num7];
						num9 = colInfoEx.SlotsMax[num7];
					}
					else
					{
						num9 = (num8 = src.Length);
					}
					if (num7 > 0)
					{
						num6 = colInfoEx.LengthReduction[num7 - 1];
					}
				}
			}
			dst = new VBuffer<TDst>(num, num5, array, array2);
		}

		// Token: 0x04000172 RID: 370
		internal const string Summary = "Removes the selected slots from the column.";

		// Token: 0x04000173 RID: 371
		public const string LoaderSignature = "DropSlotsTransform";

		// Token: 0x04000174 RID: 372
		private const string RegistrationName = "DropSlots";

		// Token: 0x04000175 RID: 373
		private readonly DropSlotsTransform.ColInfoEx[] _exes;

		// Token: 0x020000A8 RID: 168
		public sealed class Arguments
		{
			// Token: 0x04000176 RID: 374
			[Argument(4, HelpText = "Columns to drop the slots for", ShortName = "col", SortOrder = 1)]
			public DropSlotsTransform.Column[] column;
		}

		// Token: 0x020000A9 RID: 169
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000319 RID: 793 RVA: 0x000139B4 File Offset: 0x00011BB4
			public static DropSlotsTransform.Column Parse(string str)
			{
				Contracts.CheckNonWhiteSpace(str, "str");
				DropSlotsTransform.Column column = new DropSlotsTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600031A RID: 794 RVA: 0x000139E0 File Offset: 0x00011BE0
			protected override bool TryParse(string str)
			{
				int num = str.LastIndexOf(':');
				return num > 0 && num < str.Length - 1 && base.TryParse(str.Substring(0, num)) && this.TryParseSlots(str.Substring(num + 1));
			}

			// Token: 0x0600031B RID: 795 RVA: 0x00013A28 File Offset: 0x00011C28
			private bool TryParseSlots(string str)
			{
				string[] array = str.Split(new char[] { ',' });
				if (str.Length == 0)
				{
					return false;
				}
				this.slots = new DropSlotsTransform.Range[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if ((this.slots[i] = DropSlotsTransform.Range.Parse(array[i])) == null)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600031C RID: 796 RVA: 0x00013A88 File Offset: 0x00011C88
			public bool TryUnparse(StringBuilder sb)
			{
				Contracts.CheckValue<StringBuilder>(sb, "sb");
				int length = sb.Length;
				if (!this.TryUnparseCore(sb))
				{
					return false;
				}
				sb.Append(':');
				string text = "";
				foreach (DropSlotsTransform.Range range in this.slots)
				{
					sb.Append(text);
					if (!range.TryUnparse(sb))
					{
						sb.Length = length;
						return false;
					}
					text = ",";
				}
				return true;
			}

			// Token: 0x04000177 RID: 375
			[Argument(4, HelpText = "Source slot index range(s) of the column to drop")]
			public DropSlotsTransform.Range[] slots;
		}

		// Token: 0x020000AA RID: 170
		public sealed class Range
		{
			// Token: 0x0600031E RID: 798 RVA: 0x00013B10 File Offset: 0x00011D10
			public static DropSlotsTransform.Range Parse(string str)
			{
				Contracts.CheckNonWhiteSpace(str, "str");
				DropSlotsTransform.Range range = new DropSlotsTransform.Range();
				if (range.TryParse(str))
				{
					return range;
				}
				return null;
			}

			// Token: 0x0600031F RID: 799 RVA: 0x00013B3C File Offset: 0x00011D3C
			private bool TryParse(string str)
			{
				int num = str.IndexOf('-');
				if (num < 0)
				{
					if (!int.TryParse(str, out this.min))
					{
						return false;
					}
					this.max = new int?(this.min);
					return true;
				}
				else
				{
					if (num == 0 || num >= str.Length - 1)
					{
						return false;
					}
					if (!int.TryParse(str.Substring(0, num), out this.min))
					{
						return false;
					}
					string text = str.Substring(num + 1);
					if (text == "*")
					{
						return true;
					}
					int num2;
					if (!int.TryParse(text, out num2))
					{
						return false;
					}
					this.max = new int?(num2);
					return true;
				}
			}

			// Token: 0x06000320 RID: 800 RVA: 0x00013BD4 File Offset: 0x00011DD4
			public bool TryUnparse(StringBuilder sb)
			{
				Contracts.CheckValue<StringBuilder>(sb, "sb");
				sb.Append(this.min);
				if (this.max != null)
				{
					if (this.max != this.min)
					{
						sb.Append("-").Append(this.max);
					}
				}
				else
				{
					sb.Append("-*");
				}
				return true;
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00013C58 File Offset: 0x00011E58
			public bool IsValid()
			{
				return this.min >= 0 && (this.max == null || this.min <= this.max);
			}

			// Token: 0x04000178 RID: 376
			[Argument(1, HelpText = "First index in the range")]
			public int min;

			// Token: 0x04000179 RID: 377
			[Argument(0, HelpText = "Last index in the range")]
			public int? max;
		}

		// Token: 0x020000AB RID: 171
		private sealed class ColInfoEx
		{
			// Token: 0x06000323 RID: 803 RVA: 0x00013CA8 File Offset: 0x00011EA8
			public ColInfoEx(int[] slotsMin, int[] slotsMax, int[] lengthReduction, bool suppressed, ColumnType typeDst)
			{
				this.SlotsMin = slotsMin;
				this.SlotsMax = slotsMax;
				this.LengthReduction = lengthReduction;
				this.Suppressed = suppressed;
				this.TypeDst = typeDst;
			}

			// Token: 0x0400017A RID: 378
			public readonly int[] SlotsMin;

			// Token: 0x0400017B RID: 379
			public readonly int[] SlotsMax;

			// Token: 0x0400017C RID: 380
			public readonly int[] LengthReduction;

			// Token: 0x0400017D RID: 381
			public readonly bool Suppressed;

			// Token: 0x0400017E RID: 382
			public readonly ColumnType TypeDst;
		}
	}
}
