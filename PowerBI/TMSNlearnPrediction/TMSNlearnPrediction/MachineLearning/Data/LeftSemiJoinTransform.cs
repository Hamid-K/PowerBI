using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200010E RID: 270
	public sealed class LeftSemiJoinTransform : FilterBase
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		public static IDataTransform Create(LeftSemiJoinTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<LeftSemiJoinTransform.Arguments>(env, args, "args");
			Contracts.CheckNonWhiteSpace(env, args.dataFile, "dataFile");
			Contracts.CheckValue<LeftJoinDataViewBase.JoinKeyColumn[]>(env, args.keyColumns, "keyColumns");
			SubComponent<IDataLoader, SignatureDataLoader> subComponent = args.loader;
			if (!SubComponentExtensions.IsGood(subComponent))
			{
				string extension = Path.GetExtension(args.dataFile);
				if (!string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase))
				{
					throw Contracts.ExceptUserArg(env, "loader", "The loader arguments must be specified.");
				}
				subComponent = new SubComponent<IDataLoader, SignatureDataLoader>("BinaryLoader");
			}
			IDataLoader dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(subComponent, new object[]
			{
				env,
				new MultiFileSource(args.dataFile)
			});
			return new LeftSemiJoinTransform(env, input, dataLoader, args.keyColumns);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public LeftSemiJoinTransform(IHostEnvironment env, IDataView leftDv, IDataView rightDv, LeftJoinDataViewBase.JoinKeyColumn[] keyColumns)
			: base(env, "LeftSemiJoinTransform", leftDv)
		{
			this._model = this.CreateModel(this._host, rightDv, keyColumns);
			this._leftSemiJoinDv = new LeftSemiJoinDataView(this._host, this._input, this._model.TransformedRightDv, keyColumns);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001DA20 File Offset: 0x0001BC20
		public static LeftSemiJoinTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(LeftSemiJoinTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckValue<IHostEnvironment>(env, env, "env");
			IHost h = env.Register("LeftSemiJoinTransform");
			return HostExtensions.Apply<LeftSemiJoinTransform>(h, "Loading Model", (IChannel ch) => new LeftSemiJoinTransform(ch, ctx, h, input));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		private LeftSemiJoinTransform(IChannel ch, ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			Contracts.CheckValue<IChannel>(this._host, ch, "ch");
			this._model = LeftJoinSharedModel.Create(ctx, this._host);
			this._leftSemiJoinDv = new LeftSemiJoinDataView(this._host, input, this._model.TransformedRightDv, this._model.KeyColumns);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001DB1A File Offset: 0x0001BD1A
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(LeftSemiJoinTransform.GetVersionInfo());
			this._model.Save(ctx);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001DB44 File Offset: 0x0001BD44
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("LSEMIJ F", 65537U, 65537U, 65537U, "LeftSemiJoinTransform", null);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001DB65 File Offset: 0x0001BD65
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(true);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001DB6D File Offset: 0x0001BD6D
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			return this._leftSemiJoinDv.GetRowCursor(predicate, rand);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			return this._leftSemiJoinDv.GetRowCursorSet(out consolidator, predicate, n, rand);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001DB90 File Offset: 0x0001BD90
		private LeftJoinSharedModel CreateModel(IHostEnvironment env, IDataView rightDv, LeftJoinDataViewBase.JoinKeyColumn[] keyColumns)
		{
			List<ChooseColumnsTransform.Column> list = new List<ChooseColumnsTransform.Column>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (LeftJoinDataViewBase.JoinKeyColumn joinKeyColumn in keyColumns)
			{
				Contracts.CheckNonWhiteSpace(env, joinKeyColumn.Left, "Left", "Missing left key column name.");
				Contracts.CheckNonWhiteSpace(env, joinKeyColumn.Right, "Right", "Missing right key column name.");
				if (hashSet.Add(joinKeyColumn.Right))
				{
					list.Add(new ChooseColumnsTransform.Column
					{
						source = joinKeyColumn.Right,
						name = joinKeyColumn.Right
					});
				}
			}
			ChooseColumnsTransform.Arguments arguments = new ChooseColumnsTransform.Arguments
			{
				column = list.ToArray()
			};
			return new LeftJoinSharedModel(this._host, keyColumns, new ChooseColumnsTransform(arguments, this._host, rightDv));
		}

		// Token: 0x040002B8 RID: 696
		internal const string Summary = "Performs the logical left semi-join of two data views.";

		// Token: 0x040002B9 RID: 697
		private const string RegistrationName = "LeftSemiJoinTransform";

		// Token: 0x040002BA RID: 698
		public const string LoaderSignature = "LeftSemiJoinTransform";

		// Token: 0x040002BB RID: 699
		private readonly LeftSemiJoinDataView _leftSemiJoinDv;

		// Token: 0x040002BC RID: 700
		private readonly LeftJoinSharedModel _model;

		// Token: 0x0200010F RID: 271
		public sealed class Arguments
		{
			// Token: 0x040002BD RID: 701
			[Argument(0, IsInputFileName = true, HelpText = "The data file containing the terms", ShortName = "data", SortOrder = 2)]
			public string dataFile;

			// Token: 0x040002BE RID: 702
			[Argument(4, HelpText = "The data loader", NullName = "<Auto>")]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x040002BF RID: 703
			[Argument(4, HelpText = "Join key column names", ShortName = "keyCol", SortOrder = 1)]
			public LeftJoinDataViewBase.JoinKeyColumn[] keyColumns;
		}
	}
}
