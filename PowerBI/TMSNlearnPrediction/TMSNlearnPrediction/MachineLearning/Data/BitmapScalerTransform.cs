using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000255 RID: 597
	public sealed class BitmapScalerTransform : OneToOneTransformBase
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x0004A128 File Offset: 0x00048328
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BMPSCALF", 65538U, 65538U, 65538U, "BitmapScalerTransform", "BitmapScalerFunction");
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0004A160 File Offset: 0x00048360
		public BitmapScalerTransform(BitmapScalerTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "BitmapScaler", Contracts.CheckRef<BitmapScalerTransform.Arguments>(env, args, "args").column, input, delegate(ColumnType t)
			{
				if (!(t is PictureType))
				{
					return "Expected Picture type";
				}
				return null;
			})
		{
			this._exes = new BitmapScalerTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				BitmapScalerTransform.Column column = args.column[i];
				this._exes[i] = new BitmapScalerTransform.ColInfoEx(column.imageWidth ?? args.imageWidth, column.imageHeight ?? args.imageHeight, column.scaling ?? args.scaling);
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0004A264 File Offset: 0x00048464
		private BitmapScalerTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, delegate(ColumnType t)
			{
				if (!(t is PictureType))
				{
					return "Expected Picture type";
				}
				return null;
			})
		{
			this._exes = new BitmapScalerTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num > 0);
				int num2 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num2 > 0);
				BitmapScalerTransform.ScalingKind scalingKind = (BitmapScalerTransform.ScalingKind)ctx.Reader.ReadByte();
				Contracts.CheckDecode(this._host, Enum.IsDefined(typeof(BitmapScalerTransform.ScalingKind), scalingKind));
				this._exes[i] = new BitmapScalerTransform.ColInfoEx(num, num2, scalingKind);
			}
			base.Metadata.Seal();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0004A388 File Offset: 0x00048588
		public static BitmapScalerTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("BitmapScaler");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(BitmapScalerTransform.GetVersionInfo());
			return HostExtensions.Apply<BitmapScalerTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new BitmapScalerTransform(ctx, h, input);
			});
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0004A420 File Offset: 0x00048620
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BitmapScalerTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				BitmapScalerTransform.ColInfoEx colInfoEx = this._exes[i];
				ctx.Writer.Write(colInfoEx.Width);
				ctx.Writer.Write(colInfoEx.Height);
				ctx.Writer.Write((byte)colInfoEx.Scale);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0004A4B1 File Offset: 0x000486B1
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, 0 <= iinfo && iinfo < this.Infos.Length);
			return this._exes[iinfo].Type;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0004A4DD File Offset: 0x000486DD
		[Conditional("LOGGING")]
		private static void Log(IChannel ch, string msg)
		{
			ch.Trace(msg);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0004A4E6 File Offset: 0x000486E6
		[Conditional("LOGGING")]
		private static void Log(IChannel ch, string msg, params object[] args)
		{
			ch.Trace(msg, args);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0004A7EC File Offset: 0x000489EC
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			Stack<Picture.Impl> cache = new Stack<Picture.Impl>();
			Picture src = null;
			ValueGetter<Picture> getSrc = base.GetSrcGetter<Picture>(input, iinfo);
			BitmapScalerTransform.ColInfoEx ex = this._exes[iinfo];
			Picture cur = null;
			long pos = -1L;
			disposer = delegate
			{
				if (src != null)
				{
					src.Dispose();
					src = null;
				}
				while (cache.Count > 0)
				{
					Picture.Impl impl = cache.Pop();
					impl.Free();
				}
			};
			return new ValueGetter<Picture>(delegate(ref Picture dst)
			{
				BitmapScalerTransform.Release(this._host, ref dst, ex, cache);
				if (pos == input.Position)
				{
					if (cur != null)
					{
						dst = new Picture(cur.Contents);
					}
					return;
				}
				BitmapScalerTransform.Release(this._host, ref cur, ex, cache);
				pos = input.Position;
				getSrc.Invoke(ref src);
				if (src == null || src.Contents == null)
				{
					src = null;
					return;
				}
				if (BitmapScalerTransform.IsUsable(this._host, ex, src.Contents))
				{
					cur = src;
					src = null;
				}
				else
				{
					Rectangle rectangle = new Rectangle(0, 0, ex.Width, ex.Height);
					if (ex.Scale == BitmapScalerTransform.ScalingKind.IsoPad || ex.Scale == BitmapScalerTransform.ScalingKind.IsoCrop)
					{
						bool flag = ex.Scale == BitmapScalerTransform.ScalingKind.IsoPad;
						long num = (long)src.Contents.Pixels.Width * (long)ex.Height;
						long num2 = (long)src.Contents.Pixels.Height * (long)ex.Width;
						if (flag == num > num2)
						{
							checked
							{
								rectangle.Height = (int)(num2 / unchecked((long)src.Contents.Pixels.Width));
							}
							rectangle.Y = (ex.Height - rectangle.Height) / 2;
						}
						else
						{
							checked
							{
								rectangle.Width = (int)(num / unchecked((long)src.Contents.Pixels.Height));
							}
							rectangle.X = (ex.Width - rectangle.Width) / 2;
						}
					}
					Picture.Impl impl2;
					if (cache.Count > 0)
					{
						impl2 = cache.Pop();
						impl2.Context.Clear(Color.Transparent);
					}
					else
					{
						impl2 = new Picture.Impl(ex.Width, ex.Height, PixelFormat.Format32bppArgb);
						impl2.Context.Clear(Color.Transparent);
					}
					impl2.Context.DrawImage(src.Contents.Pixels, rectangle);
					cur = new Picture(impl2);
				}
				dst = new Picture(cur.Contents);
			});
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0004A86C File Offset: 0x00048A6C
		private static void Release(IHostEnvironment env, ref Picture pic, BitmapScalerTransform.ColInfoEx ex, Stack<Picture.Impl> cache)
		{
			if (pic == null)
			{
				return;
			}
			Picture.Impl impl;
			pic.DisposeAndSurrender(out impl);
			using (IChannel channel = env.Start("Release"))
			{
				if (impl != null)
				{
					if (impl.Context != null && BitmapScalerTransform.IsUsable(env, ex, impl))
					{
						cache.Push(impl);
					}
					else
					{
						impl.Free();
					}
				}
				channel.Done();
			}
			pic = null;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0004A8DC File Offset: 0x00048ADC
		private static bool IsUsable(IExceptionContext ectx, BitmapScalerTransform.ColInfoEx ex, Picture.Impl impl)
		{
			if (impl == null)
			{
				return false;
			}
			if (impl.Pixels == null)
			{
				return false;
			}
			if (impl.Pixels.Width != ex.Width || impl.Pixels.Height != ex.Height)
			{
				return false;
			}
			PixelFormat pixelFormat = impl.Pixels.PixelFormat;
			PixelFormat pixelFormat2 = pixelFormat;
			return pixelFormat2 == PixelFormat.Format24bppRgb || pixelFormat2 == PixelFormat.Format32bppArgb;
		}

		// Token: 0x0400077E RID: 1918
		internal const string Summary = "Scales an image to specified dimensions using one of the three scale types: isotropic with padding, isotropic with cropping or anisotropic. In case of isotropic padding, transparent color is used to pad resulting image.";

		// Token: 0x0400077F RID: 1919
		public const string LoaderSignature = "BitmapScalerTransform";

		// Token: 0x04000780 RID: 1920
		internal const string LoaderSignatureOld = "BitmapScalerFunction";

		// Token: 0x04000781 RID: 1921
		private const string RegistrationName = "BitmapScaler";

		// Token: 0x04000782 RID: 1922
		private readonly BitmapScalerTransform.ColInfoEx[] _exes;

		// Token: 0x02000256 RID: 598
		public enum ScalingKind : byte
		{
			// Token: 0x04000786 RID: 1926
			[TGUI(Label = "Isotropic with Padding")]
			IsoPad,
			// Token: 0x04000787 RID: 1927
			[TGUI(Label = "Isotropic with Cropping")]
			IsoCrop,
			// Token: 0x04000788 RID: 1928
			[TGUI(Label = "Anisotropic")]
			Aniso
		}

		// Token: 0x02000257 RID: 599
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000D6B RID: 3435 RVA: 0x0004A948 File Offset: 0x00048B48
			public static BitmapScalerTransform.Column Parse(string str)
			{
				BitmapScalerTransform.Column column = new BitmapScalerTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x0004A967 File Offset: 0x00048B67
			public bool TryUnparse(StringBuilder sb)
			{
				return this.imageWidth == null && this.imageHeight == null && this.scaling == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000789 RID: 1929
			[Argument(0, HelpText = "Scaled width of the image", ShortName = "width")]
			public int? imageWidth;

			// Token: 0x0400078A RID: 1930
			[Argument(0, HelpText = "Height of the image to use", ShortName = "height")]
			public int? imageHeight;

			// Token: 0x0400078B RID: 1931
			[Argument(0, HelpText = "Scaling method", ShortName = "scale")]
			public BitmapScalerTransform.ScalingKind? scaling;
		}

		// Token: 0x02000258 RID: 600
		public class Arguments
		{
			// Token: 0x0400078C RID: 1932
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public BitmapScalerTransform.Column[] column;

			// Token: 0x0400078D RID: 1933
			[Argument(0, HelpText = "Scaled width of the image", ShortName = "width")]
			public int imageWidth = 256;

			// Token: 0x0400078E RID: 1934
			[Argument(0, HelpText = "Scaled height of the image", ShortName = "height")]
			public int imageHeight = 256;

			// Token: 0x0400078F RID: 1935
			[Argument(0, HelpText = "Scaling method", ShortName = "scale")]
			public BitmapScalerTransform.ScalingKind scaling = BitmapScalerTransform.ScalingKind.IsoCrop;
		}

		// Token: 0x02000259 RID: 601
		private sealed class ColInfoEx
		{
			// Token: 0x06000D6F RID: 3439 RVA: 0x0004A9C8 File Offset: 0x00048BC8
			public ColInfoEx(int width, int height, BitmapScalerTransform.ScalingKind scale)
			{
				Contracts.CheckUserArg(width > 0, "imageWidth");
				Contracts.CheckUserArg(height > 0, "imageHeight");
				Contracts.CheckUserArg(Enum.IsDefined(typeof(BitmapScalerTransform.ScalingKind), scale), "scaling");
				this.Width = width;
				this.Height = height;
				this.Scale = scale;
				this.Type = new PictureType(this.Height, this.Width);
			}

			// Token: 0x04000790 RID: 1936
			public readonly int Width;

			// Token: 0x04000791 RID: 1937
			public readonly int Height;

			// Token: 0x04000792 RID: 1938
			public readonly BitmapScalerTransform.ScalingKind Scale;

			// Token: 0x04000793 RID: 1939
			public readonly ColumnType Type;
		}
	}
}
