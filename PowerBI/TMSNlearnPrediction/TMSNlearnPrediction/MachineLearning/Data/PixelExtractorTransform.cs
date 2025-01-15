using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000387 RID: 903
	public sealed class PixelExtractorTransform : OneToOneTransformBase
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x0006DEF8 File Offset: 0x0006C0F8
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PIXLEXTF", 65538U, 65538U, 65538U, "PixelExtractorTransform", "PixelExtractorFunction");
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0006DF30 File Offset: 0x0006C130
		public PixelExtractorTransform(PixelExtractorTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "PixelExtractor", Contracts.CheckRef<PixelExtractorTransform.Arguments>(args, "args").column, input, delegate(ColumnType t)
			{
				if (!(t is PictureType))
				{
					return "Expected Picture type";
				}
				return null;
			})
		{
			this._exes = new PixelExtractorTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				PixelExtractorTransform.Column column = args.column[i];
				this._exes[i] = new PixelExtractorTransform.ColInfoEx(column, args);
			}
			this._types = this.ConstructTypes(true);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0006DFD4 File Offset: 0x0006C1D4
		private PixelExtractorTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, delegate(ColumnType t)
			{
				if (!(t is PictureType))
				{
					return "Expected Picture type";
				}
				return null;
			})
		{
			this._exes = new PixelExtractorTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new PixelExtractorTransform.ColInfoEx(ctx);
			}
			this._types = this.ConstructTypes(false);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0006E090 File Offset: 0x0006C290
		public static PixelExtractorTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("PixelExtractor");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(PixelExtractorTransform.GetVersionInfo());
			return HostExtensions.Apply<PixelExtractorTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new PixelExtractorTransform(ctx, h, input);
			});
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0006E128 File Offset: 0x0006C328
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(PixelExtractorTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0006E18C File Offset: 0x0006C38C
		private VectorType[] ConstructTypes(bool user)
		{
			VectorType[] array = new VectorType[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				OneToOneTransformBase.ColInfo colInfo = this.Infos[i];
				PixelExtractorTransform.ColInfoEx colInfoEx = this._exes[i];
				PictureType pictureType = this._input.Schema.GetColumnType(colInfo.Source) as PictureType;
				if (pictureType.Height <= 0 || pictureType.Width <= 0)
				{
					string columnName = this._input.Schema.GetColumnName(colInfo.Source);
					throw user ? Contracts.ExceptUserArg(this._host, "column", "Column '{0}' does not have known size", new object[] { columnName }) : Contracts.Except(this._host, "Column '{0}' does not have known size", new object[] { columnName });
				}
				int height = pictureType.Height;
				int width = pictureType.Width;
				array[i] = new VectorType(colInfoEx.Convert ? NumberType.Float : NumberType.U1, new int[]
				{
					(int)colInfoEx.Planes,
					height,
					width
				});
			}
			base.Metadata.Seal();
			return array;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0006E2C0 File Offset: 0x0006C4C0
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0006E2CA File Offset: 0x0006C4CA
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			if (this._exes[iinfo].Convert)
			{
				return this.GetGetterCore<float>(input, iinfo, out disposer);
			}
			return this.GetGetterCore<byte>(input, iinfo, out disposer);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		private unsafe ValueGetter<VBuffer<TValue>> GetGetterCore<TValue>(IRow input, int iinfo, out Action disposer)
		{
			VectorType vectorType = this._types[iinfo];
			vectorType.GetDim(0);
			int height = vectorType.GetDim(1);
			int width = vectorType.GetDim(2);
			int size = vectorType.ValueCount;
			int cpix = height * width;
			PixelExtractorTransform.ColInfoEx ex = this._exes[iinfo];
			ValueGetter<Picture> getSrc = base.GetSrcGetter<Picture>(input, iinfo);
			Picture src = null;
			disposer = delegate
			{
				if (src != null)
				{
					src.Dispose();
					src = null;
				}
			};
			return delegate(ref VBuffer<TValue> dst)
			{
				getSrc.Invoke(ref src);
				if (src == null || src.Contents == null || src.Contents.Pixels == null)
				{
					dst = new VBuffer<TValue>(size, 0, dst.Values, dst.Indices);
					return;
				}
				Contracts.Check(this._host, src.Contents.Pixels.PixelFormat == PixelFormat.Format32bppArgb || src.Contents.Pixels.PixelFormat == PixelFormat.Format24bppRgb);
				Contracts.Check(this._host, src.Contents.Pixels.Height == height && src.Contents.Pixels.Width == width);
				TValue[] array = dst.Values;
				if (Utils.Size<TValue>(array) < size)
				{
					array = new TValue[size];
				}
				BitmapData bitmapData = src.Contents.Pixels.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, src.Contents.Pixels.PixelFormat);
				try
				{
					float offset = ex.Offset;
					float scale = ex.Scale;
					float[] array2 = array as float[];
					byte[] array3 = array as byte[];
					bool flag = offset != 0f || scale != 1f;
					bool flag2 = bitmapData.PixelFormat == PixelFormat.Format32bppArgb;
					int num = (flag2 ? 4 : 3);
					bool flag3 = ex.Alpha && flag2;
					bool red = ex.Red;
					bool green = ex.Green;
					bool blue = ex.Blue;
					int num2 = 0;
					if (ex.Alpha && !flag2)
					{
						if (array2 != null)
						{
							float num3 = (255f - offset) * scale;
							for (int i = 0; i < cpix; i++)
							{
								array2[i] = num3;
							}
						}
						else
						{
							for (int j = 0; j < cpix; j++)
							{
								array2[j] = 255f;
							}
						}
						num2 = cpix;
					}
					int height2 = height;
					int width2 = width;
					byte* ptr = (byte*)bitmapData.Scan0.ToPointer();
					for (int k = 0; k < height2; k++)
					{
						byte* ptr2 = ptr;
						int num4 = num2 + k * width2;
						if (array3 != null)
						{
							int l = 0;
							while (l < width2)
							{
								int num5 = num4;
								if (flag3)
								{
									array3[num5] = ptr2[3];
									num5 += cpix;
								}
								if (red)
								{
									array3[num5] = ptr2[2];
									num5 += cpix;
								}
								if (green)
								{
									array3[num5] = ptr2[1];
									num5 += cpix;
								}
								if (blue)
								{
									array3[num5] = *ptr2;
									num5 += cpix;
								}
								l++;
								num4++;
								ptr2 += num;
							}
						}
						else if (!flag)
						{
							int m = 0;
							while (m < width2)
							{
								int num6 = num4;
								if (flag3)
								{
									array2[num6] = (float)ptr2[3];
									num6 += cpix;
								}
								if (red)
								{
									array2[num6] = (float)ptr2[2];
									num6 += cpix;
								}
								if (green)
								{
									array2[num6] = (float)ptr2[1];
									num6 += cpix;
								}
								if (blue)
								{
									array2[num6] = (float)(*ptr2);
									num6 += cpix;
								}
								m++;
								num4++;
								ptr2 += num;
							}
						}
						else
						{
							int n = 0;
							while (n < width2)
							{
								int num7 = num4;
								if (flag3)
								{
									array2[num7] = ((float)ptr2[3] - offset) * scale;
									num7 += cpix;
								}
								if (red)
								{
									array2[num7] = ((float)ptr2[2] - offset) * scale;
									num7 += cpix;
								}
								if (green)
								{
									array2[num7] = ((float)ptr2[1] - offset) * scale;
									num7 += cpix;
								}
								if (blue)
								{
									array2[num7] = ((float)(*ptr2) - offset) * scale;
									num7 += cpix;
								}
								n++;
								num4++;
								ptr2 += num;
							}
						}
						ptr += bitmapData.Stride;
					}
				}
				finally
				{
					src.Contents.Pixels.UnlockBits(bitmapData);
				}
				dst = new VBuffer<TValue>(size, array, dst.Indices);
			};
		}

		// Token: 0x04000B38 RID: 2872
		internal const string Summary = "Extract color plane(s) from an image. Options include scaling, offset and conversion to floating point.";

		// Token: 0x04000B39 RID: 2873
		public const string LoaderSignature = "PixelExtractorTransform";

		// Token: 0x04000B3A RID: 2874
		internal const string LoaderSignatureOld = "PixelExtractorFunction";

		// Token: 0x04000B3B RID: 2875
		private const string RegistrationName = "PixelExtractor";

		// Token: 0x04000B3C RID: 2876
		private readonly PixelExtractorTransform.ColInfoEx[] _exes;

		// Token: 0x04000B3D RID: 2877
		private readonly VectorType[] _types;

		// Token: 0x02000388 RID: 904
		public class Column : OneToOneColumn
		{
			// Token: 0x06001390 RID: 5008 RVA: 0x0006E884 File Offset: 0x0006CA84
			public static PixelExtractorTransform.Column Parse(string str)
			{
				PixelExtractorTransform.Column column = new PixelExtractorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001391 RID: 5009 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
			public bool TryUnparse(StringBuilder sb)
			{
				return this.useAlpha == null && this.useRed == null && this.useGreen == null && this.useBlue == null && this.convert == null && this.offset == null && this.scale == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000B40 RID: 2880
			[Argument(0, HelpText = "Whether to use alpha channel", ShortName = "alpha")]
			public bool? useAlpha;

			// Token: 0x04000B41 RID: 2881
			[Argument(0, HelpText = "Whether to use red channel", ShortName = "red")]
			public bool? useRed;

			// Token: 0x04000B42 RID: 2882
			[Argument(0, HelpText = "Whether to use green channel", ShortName = "green")]
			public bool? useGreen;

			// Token: 0x04000B43 RID: 2883
			[Argument(0, HelpText = "Whether to use blue channel", ShortName = "blue")]
			public bool? useBlue;

			// Token: 0x04000B44 RID: 2884
			[Argument(0, HelpText = "Whether to convert to floating point", ShortName = "conv")]
			public bool? convert;

			// Token: 0x04000B45 RID: 2885
			[Argument(0, HelpText = "Offset (pre-scale)")]
			public float? offset;

			// Token: 0x04000B46 RID: 2886
			[Argument(0, HelpText = "Scale factor")]
			public float? scale;
		}

		// Token: 0x02000389 RID: 905
		public class Arguments
		{
			// Token: 0x04000B47 RID: 2887
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public PixelExtractorTransform.Column[] column;

			// Token: 0x04000B48 RID: 2888
			[Argument(0, HelpText = "Whether to use alpha channel", ShortName = "alpha")]
			public bool useAlpha;

			// Token: 0x04000B49 RID: 2889
			[Argument(0, HelpText = "Whether to use red channel", ShortName = "red")]
			public bool useRed = true;

			// Token: 0x04000B4A RID: 2890
			[Argument(0, HelpText = "Whether to use green channel", ShortName = "green")]
			public bool useGreen = true;

			// Token: 0x04000B4B RID: 2891
			[Argument(0, HelpText = "Whether to use blue channel", ShortName = "blue")]
			public bool useBlue = true;

			// Token: 0x04000B4C RID: 2892
			[Argument(0, HelpText = "Whether to convert to floating point", ShortName = "conv")]
			public bool convert = true;

			// Token: 0x04000B4D RID: 2893
			[Argument(0, HelpText = "Offset (pre-scale)")]
			public float? offset;

			// Token: 0x04000B4E RID: 2894
			[Argument(0, HelpText = "Scale factor")]
			public float? scale;
		}

		// Token: 0x0200038A RID: 906
		[Flags]
		private enum ColorBits : byte
		{
			// Token: 0x04000B50 RID: 2896
			Alpha = 1,
			// Token: 0x04000B51 RID: 2897
			Red = 2,
			// Token: 0x04000B52 RID: 2898
			Green = 4,
			// Token: 0x04000B53 RID: 2899
			Blue = 8,
			// Token: 0x04000B54 RID: 2900
			All = 15
		}

		// Token: 0x0200038B RID: 907
		private sealed class ColInfoEx
		{
			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x0006E941 File Offset: 0x0006CB41
			public bool Alpha
			{
				get
				{
					return (byte)(this.Colors & PixelExtractorTransform.ColorBits.Alpha) != 0;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06001395 RID: 5013 RVA: 0x0006E952 File Offset: 0x0006CB52
			public bool Red
			{
				get
				{
					return (byte)(this.Colors & PixelExtractorTransform.ColorBits.Red) != 0;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06001396 RID: 5014 RVA: 0x0006E963 File Offset: 0x0006CB63
			public bool Green
			{
				get
				{
					return (byte)(this.Colors & PixelExtractorTransform.ColorBits.Green) != 0;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06001397 RID: 5015 RVA: 0x0006E974 File Offset: 0x0006CB74
			public bool Blue
			{
				get
				{
					return (byte)(this.Colors & PixelExtractorTransform.ColorBits.Blue) != 0;
				}
			}

			// Token: 0x06001398 RID: 5016 RVA: 0x0006E988 File Offset: 0x0006CB88
			public ColInfoEx(PixelExtractorTransform.Column item, PixelExtractorTransform.Arguments args)
			{
				if (item.useAlpha ?? args.useAlpha)
				{
					this.Colors |= PixelExtractorTransform.ColorBits.Alpha;
					this.Planes += 1;
				}
				if (item.useRed ?? args.useRed)
				{
					this.Colors |= PixelExtractorTransform.ColorBits.Red;
					this.Planes += 1;
				}
				if (item.useGreen ?? args.useGreen)
				{
					this.Colors |= PixelExtractorTransform.ColorBits.Green;
					this.Planes += 1;
				}
				if (item.useBlue ?? args.useBlue)
				{
					this.Colors |= PixelExtractorTransform.ColorBits.Blue;
					this.Planes += 1;
				}
				Contracts.CheckUserArg(this.Planes > 0, "useRed", "Need to use at least one color plane");
				this.Convert = item.convert ?? args.convert;
				if (!this.Convert)
				{
					this.Offset = 0f;
					this.Scale = 1f;
					return;
				}
				float? offset = item.offset;
				float num;
				if (offset == null)
				{
					float? offset2 = args.offset;
					num = ((offset2 != null) ? offset2.GetValueOrDefault() : 0f);
				}
				else
				{
					num = offset.GetValueOrDefault();
				}
				this.Offset = num;
				float? scale = item.scale;
				float num2;
				if (scale == null)
				{
					float? scale2 = args.scale;
					num2 = ((scale2 != null) ? scale2.GetValueOrDefault() : 1f);
				}
				else
				{
					num2 = scale.GetValueOrDefault();
				}
				this.Scale = num2;
				Contracts.CheckUserArg(FloatUtils.IsFinite(this.Offset), "offset");
				Contracts.CheckUserArg(FloatUtils.IsFiniteNonZero(this.Scale), "scale");
			}

			// Token: 0x06001399 RID: 5017 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
			public ColInfoEx(ModelLoadContext ctx)
			{
				this.Colors = (PixelExtractorTransform.ColorBits)ctx.Reader.ReadByte();
				Contracts.CheckDecode(this.Colors != (PixelExtractorTransform.ColorBits)0);
				Contracts.CheckDecode((this.Colors & PixelExtractorTransform.ColorBits.All) == this.Colors);
				int num = (int)this.Colors;
				num = (num & 5) + ((num >> 1) & 5);
				num = (num & 3) + ((num >> 2) & 3);
				this.Planes = (byte)num;
				this.Convert = Utils.ReadBoolByte(ctx.Reader);
				this.Offset = Utils.ReadFloat(ctx.Reader);
				Contracts.CheckDecode(FloatUtils.IsFinite(this.Offset));
				this.Scale = Utils.ReadFloat(ctx.Reader);
				Contracts.CheckDecode(FloatUtils.IsFiniteNonZero(this.Scale));
				Contracts.CheckDecode(this.Convert || (this.Offset == 0f && this.Scale == 1f));
			}

			// Token: 0x0600139A RID: 5018 RVA: 0x0006EC94 File Offset: 0x0006CE94
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write((byte)this.Colors);
				Utils.WriteBoolByte(ctx.Writer, this.Convert);
				ctx.Writer.Write(this.Offset);
				ctx.Writer.Write(this.Scale);
			}

			// Token: 0x04000B55 RID: 2901
			public readonly PixelExtractorTransform.ColorBits Colors;

			// Token: 0x04000B56 RID: 2902
			public readonly byte Planes;

			// Token: 0x04000B57 RID: 2903
			public readonly bool Convert;

			// Token: 0x04000B58 RID: 2904
			public readonly float Offset;

			// Token: 0x04000B59 RID: 2905
			public readonly float Scale;
		}
	}
}
