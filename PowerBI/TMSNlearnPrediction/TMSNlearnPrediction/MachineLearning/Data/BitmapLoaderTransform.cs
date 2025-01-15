using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000252 RID: 594
	public sealed class BitmapLoaderTransform : OneToOneTransformBase
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00049E94 File Offset: 0x00048094
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("BMPLOADT", 65538U, 65538U, 65538U, "BitmapLoaderTransform", null);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00049EB8 File Offset: 0x000480B8
		public BitmapLoaderTransform(BitmapLoaderTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "BitmapLoader", Contracts.CheckRef<BitmapLoaderTransform.Arguments>(env, args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsText))
		{
			this._type = new PictureType();
			base.Metadata.Seal();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00049F05 File Offset: 0x00048105
		private BitmapLoaderTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsText))
		{
			this._type = new PictureType();
			base.Metadata.Seal();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00049F54 File Offset: 0x00048154
		public static BitmapLoaderTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("BitmapLoader");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(BitmapLoaderTransform.GetVersionInfo());
			return HostExtensions.Apply<BitmapLoaderTransform>(h, "Loading Model", (IChannel ch) => new BitmapLoaderTransform(ctx, h, input));
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00049FE9 File Offset: 0x000481E9
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(BitmapLoaderTransform.GetVersionInfo());
			base.SaveBase(ctx);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0004A014 File Offset: 0x00048214
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, 0 <= iinfo && iinfo < this.Infos.Length);
			return this._type;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0004A0B0 File Offset: 0x000482B0
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ValueGetter<DvText> getSrc = base.GetSrcGetter<DvText>(input, iinfo);
			DvText src = default(DvText);
			return new ValueGetter<Picture>(delegate(ref Picture dst)
			{
				if (dst != null)
				{
					dst.Dispose();
					dst = null;
				}
				getSrc.Invoke(ref src);
				if (src.Length > 0)
				{
					try
					{
						dst = new Picture(src.ToString());
					}
					catch
					{
						dst = null;
					}
				}
			});
		}

		// Token: 0x04000779 RID: 1913
		internal const string Summary = "Loads a Bitmap image from a file.";

		// Token: 0x0400077A RID: 1914
		public const string LoaderSignature = "BitmapLoaderTransform";

		// Token: 0x0400077B RID: 1915
		private const string RegistrationName = "BitmapLoader";

		// Token: 0x0400077C RID: 1916
		private readonly PictureType _type;

		// Token: 0x02000253 RID: 595
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000D5A RID: 3418 RVA: 0x0004A0F0 File Offset: 0x000482F0
			public static BitmapLoaderTransform.Column Parse(string str)
			{
				BitmapLoaderTransform.Column column = new BitmapLoaderTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x0004A10F File Offset: 0x0004830F
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x02000254 RID: 596
		public sealed class Arguments
		{
			// Token: 0x0400077D RID: 1917
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public BitmapLoaderTransform.Column[] column;
		}
	}
}
