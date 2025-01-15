using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000120 RID: 288
	public sealed class NopTransform : IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00020010 File Offset: 0x0001E210
		public static IDataTransform CreateIfNeeded(IHostEnvironment env, IDataView input)
		{
			IDataTransform dataTransform = input as IDataTransform;
			if (dataTransform != null)
			{
				return dataTransform;
			}
			return new NopTransform(env, input);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00020030 File Offset: 0x0001E230
		private NopTransform(IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, input, "input");
			this._input = input;
			this._host = env.Register(NopTransform.RegistrationName);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00020067 File Offset: 0x0001E267
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NOOPNOOP", 65537U, 65537U, 65537U, "NopTransform", null);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000200AC File Offset: 0x0001E2AC
		public static NopTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register(NopTransform.RegistrationName);
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NopTransform.GetVersionInfo());
			return HostExtensions.Apply<NopTransform>(h, "Loading Model", (IChannel ch) => new NopTransform(ctx, h, input));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00020141 File Offset: 0x0001E341
		private NopTransform(ModelLoadContext ctx, IHost host, IDataView input)
		{
			Contracts.CheckValue<IDataView>(host, input, "input");
			this._input = input;
			this._host = host;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00020163 File Offset: 0x0001E363
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NopTransform.GetVersionInfo());
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00020187 File Offset: 0x0001E387
		public bool CanShuffle
		{
			get
			{
				return this._input.CanShuffle;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00020194 File Offset: 0x0001E394
		public ISchema Schema
		{
			get
			{
				return this._input.Schema;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000201A1 File Offset: 0x0001E3A1
		public long? GetRowCount(bool lazy = true)
		{
			return this._input.GetRowCount(lazy);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000201AF File Offset: 0x0001E3AF
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			return this._input.GetRowCursor(predicate, rand);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000201BE File Offset: 0x0001E3BE
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			return this._input.GetRowCursorSet(ref consolidator, predicate, n, rand);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public IDataView Source
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x040002E6 RID: 742
		internal const string Summary = "Does nothing.";

		// Token: 0x040002E7 RID: 743
		public const string LoaderSignature = "NopTransform";

		// Token: 0x040002E8 RID: 744
		private readonly IDataView _input;

		// Token: 0x040002E9 RID: 745
		private readonly IHost _host;

		// Token: 0x040002EA RID: 746
		internal static string RegistrationName = "NopTransform";
	}
}
