using System;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000087 RID: 135
	public sealed class CopyColumnsTransform : OneToOneTransformBase
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000E834 File Offset: 0x0000CA34
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("COPYCOLT", 65537U, 65537U, 65537U, "CopyTransform", null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E855 File Offset: 0x0000CA55
		public CopyColumnsTransform(CopyColumnsTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "CopyColumns", Contracts.CheckRef<CopyColumnsTransform.Arguments>(env, args, "args").column, input, null)
		{
			this.SetMetadata();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E87C File Offset: 0x0000CA7C
		private CopyColumnsTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, null)
		{
			this.SetMetadata();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		public static CopyColumnsTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(CopyColumnsTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			IHost h = env.Register("CopyColumns");
			return HostExtensions.Apply<CopyColumnsTransform>(h, "Loading Model", (IChannel ch) => new CopyColumnsTransform(ctx, h, input));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E93B File Offset: 0x0000CB3B
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(CopyColumnsTransform.GetVersionInfo());
			base.SaveBase(ctx);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000E966 File Offset: 0x0000CB66
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this.Infos[iinfo].TypeSrc;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E978 File Offset: 0x0000CB78
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				using (metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source))
				{
				}
			}
			metadata.Seal();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		protected override bool WantParallelCursors(Func<int, bool> predicate)
		{
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			int source = this.Infos[iinfo].Source;
			ColumnType columnType = input.Schema.GetColumnType(source);
			Func<int, ValueGetter<int>> func = new Func<int, ValueGetter<int>>(input.GetGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType });
			return (Delegate)methodInfo.Invoke(input, new object[] { source });
		}

		// Token: 0x040000EE RID: 238
		internal const string Summary = "Copy a source column to a new column.";

		// Token: 0x040000EF RID: 239
		public const string LoaderSignature = "CopyTransform";

		// Token: 0x040000F0 RID: 240
		private const string RegistrationName = "CopyColumns";

		// Token: 0x02000088 RID: 136
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x0600027E RID: 638 RVA: 0x0000EA68 File Offset: 0x0000CC68
			public static CopyColumnsTransform.Column Parse(string str)
			{
				CopyColumnsTransform.Column column = new CopyColumnsTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600027F RID: 639 RVA: 0x0000EA87 File Offset: 0x0000CC87
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x02000089 RID: 137
		public sealed class Arguments
		{
			// Token: 0x040000F1 RID: 241
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public CopyColumnsTransform.Column[] column;
		}
	}
}
