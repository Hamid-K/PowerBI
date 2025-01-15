using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000208 RID: 520
	public sealed class GenericScorer : RowToRowScorerBase
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0003EBC4 File Offset: 0x0003CDC4
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("GNRICSCR", 65537U, 65537U, 65537U, "GenericScoreTransform", null);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003EBE5 File Offset: 0x0003CDE5
		protected override RowToRowScorerBase.BindingsBase GetBindings()
		{
			return this._bindings;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
		public GenericScorer(ScorerArgumentsBase args, IHostEnvironment env, IDataView data, ISchemaBoundMapper mapper)
			: base(env, data, "GenericScore", Contracts.CheckRef<ISchemaBoundMapper>(mapper, "mapper").Bindable)
		{
			Contracts.CheckValue<ScorerArgumentsBase>(this._host, args, "args");
			ISchemaBoundRowMapper schemaBoundRowMapper = mapper as ISchemaBoundRowMapper;
			Contracts.CheckParam(this._host, schemaBoundRowMapper != null, "mapper", "mapper should implement ISchemaBoundRowMapper");
			this._bindings = GenericScorer.Bindings.Create(data.Schema, schemaBoundRowMapper, args.suffix, true);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003EC69 File Offset: 0x0003CE69
		private GenericScorer(IHostEnvironment env, GenericScorer transform, IDataView data)
			: base(env, data, "GenericScore", transform._bindable)
		{
			this._bindings = transform._bindings.ApplyToSchema(env, data.Schema);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003EC96 File Offset: 0x0003CE96
		private GenericScorer(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input)
		{
			this._bindings = GenericScorer.Bindings.Create(ctx, host, this._bindable, input.Schema);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003ECDC File Offset: 0x0003CEDC
		public static GenericScorer Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("GenericScore");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(GenericScorer.GetVersionInfo());
			return HostExtensions.Apply<GenericScorer>(h, "Loading Model", (IChannel ch) => new GenericScorer(ctx, h, input));
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003ED71 File Offset: 0x0003CF71
		protected override void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(GenericScorer.GetVersionInfo());
			this._bindings.Save(ctx);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003ED8A File Offset: 0x0003CF8A
		protected override bool WantParallelCursors(Func<int, bool> predicate)
		{
			return this._bindings.AnyNewColumnsActive(predicate);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003ED98 File Offset: 0x0003CF98
		public override IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(this._host, env, "env");
			Contracts.CheckValue<IDataView>(this._host, newSource, "newSource");
			return new GenericScorer(env, this, newSource);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0003EDC4 File Offset: 0x0003CFC4
		protected override Delegate[] GetGetters(IRow output, Func<int, bool> predicate)
		{
			return RowToRowScorerBase.GetGettersFromRow(output, predicate);
		}

		// Token: 0x0400064D RID: 1613
		public const string LoadName = "GenericScorer";

		// Token: 0x0400064E RID: 1614
		public const string LoaderSignature = "GenericScoreTransform";

		// Token: 0x0400064F RID: 1615
		private const string RegistrationName = "GenericScore";

		// Token: 0x04000650 RID: 1616
		private GenericScorer.Bindings _bindings;

		// Token: 0x02000209 RID: 521
		public sealed class Arguments : ScorerArgumentsBase
		{
		}

		// Token: 0x0200020A RID: 522
		private sealed class Bindings : RowToRowScorerBase.BindingsBase
		{
			// Token: 0x06000B97 RID: 2967 RVA: 0x0003EDD5 File Offset: 0x0003CFD5
			private Bindings(ISchema input, ISchemaBoundRowMapper mapper, string suffix, bool user)
				: base(input, mapper, suffix, user, new string[0])
			{
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
			public static GenericScorer.Bindings Create(ISchema input, ISchemaBoundRowMapper mapper, string suffix, bool user = true)
			{
				return new GenericScorer.Bindings(input, mapper, suffix, user);
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
			private static GenericScorer.Bindings Create(IHostEnvironment env, ISchemaBindableMapper bindable, ISchema input, IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> roles, string suffix, bool user = true)
			{
				ISchemaBoundMapper schemaBoundMapper = bindable.Bind(env, RoleMappedSchema.Create(input, roles));
				ISchemaBoundRowMapper schemaBoundRowMapper = schemaBoundMapper as ISchemaBoundRowMapper;
				Contracts.Check(schemaBoundRowMapper != null, "Predictor expected to be a RowMapper!");
				return GenericScorer.Bindings.Create(input, schemaBoundRowMapper, suffix, user);
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x0003EE34 File Offset: 0x0003D034
			public GenericScorer.Bindings ApplyToSchema(IHostEnvironment env, ISchema input)
			{
				ISchemaBindableMapper bindable = this.RowMapper.Bindable;
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> inputColumnRoles = this.RowMapper.GetInputColumnRoles();
				string suffix = this.Suffix;
				return GenericScorer.Bindings.Create(env, bindable, input, inputColumnRoles, suffix, true);
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x0003EE6C File Offset: 0x0003D06C
			public static GenericScorer.Bindings Create(ModelLoadContext ctx, IHostEnvironment env, ISchemaBindableMapper bindable, ISchema input)
			{
				string text;
				KeyValuePair<RoleMappedSchema.ColumnRole, string>[] array = ScorerBindingsBase.LoadBaseInfo(ctx, out text);
				return GenericScorer.Bindings.Create(env, bindable, input, array, text, false);
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x0003EE8D File Offset: 0x0003D08D
			public override void Save(ModelSaveContext ctx)
			{
				base.SaveBase(ctx);
			}
		}
	}
}
