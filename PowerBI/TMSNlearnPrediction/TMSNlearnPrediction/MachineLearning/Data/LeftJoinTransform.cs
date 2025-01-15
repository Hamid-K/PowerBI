using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200010A RID: 266
	public sealed class LeftJoinTransform : TransformBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001D28F File Offset: 0x0001B48F
		public override ISchema Schema
		{
			get
			{
				return this._leftJoinDv.Schema;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001D29C File Offset: 0x0001B49C
		public static IDataTransform Create(LeftJoinTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<LeftJoinTransform.Arguments>(env, args, "args");
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
			bool flag = args.type == LeftJoinTransform.JoinType.Inner;
			return new LeftJoinTransform(env, input, dataLoader, args.keyColumns, flag, args.addRelativeIndex, args.relativeIndexColumnName, args.rename, args.renameFormat);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001D380 File Offset: 0x0001B580
		public LeftJoinTransform(IHostEnvironment env, IDataView leftDv, IDataView rightDv, LeftJoinDataViewBase.JoinKeyColumn[] keyColumns, bool inner, bool? addRelativeIndex = null, string relativeIndexColumnName = null, bool rename = true, string renameFormat = "{0}_{1:000}")
			: base(env, "LeftJoinTransform", leftDv)
		{
			Contracts.CheckValue<IDataView>(this._host, rightDv, "rightDv");
			Contracts.CheckValue<LeftJoinDataViewBase.JoinKeyColumn[]>(this._host, keyColumns, "keyColumns");
			if (rename)
			{
				Contracts.CheckNonWhiteSpace(this._host, renameFormat, "renameFormat");
			}
			Contracts.Check(this._host, keyColumns.Length > 0, "At least a key column should be specified.");
			this._inner = inner;
			this._model = this.GenerateModel(env, this._input, rightDv, keyColumns, relativeIndexColumnName, rename, renameFormat);
			this._leftJoinDv = new LeftJoinDataView(env, this._input, this._model.TransformedRightDv, this._model.KeyColumns, this._inner, addRelativeIndex, relativeIndexColumnName);
			bool hasRelativeIndex = this._leftJoinDv.HasRelativeIndex;
			this._relativeIndexColumnName = this._leftJoinDv.RelativeIndexColumnName;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001D468 File Offset: 0x0001B668
		private LeftJoinSharedModel GenerateModel(IHostEnvironment env, IDataView leftDv, IDataView rightDv, LeftJoinDataViewBase.JoinKeyColumn[] keyColumns, string relativeIndexColumnName, bool rename, string renameFormat)
		{
			HashSet<string> hashSet = new HashSet<string>(keyColumns.Select((LeftJoinDataViewBase.JoinKeyColumn joinKeyCol) => joinKeyCol.Right));
			ISchema schema = leftDv.Schema;
			ISchema schema2 = rightDv.Schema;
			List<ChooseColumnsTransform.Column> list = null;
			bool flag = false;
			using (IChannel channel = this._host.Start("Checking right data view columns."))
			{
				for (int i = 0; i < schema2.ColumnCount; i++)
				{
					if (MetadataUtils.IsHidden(schema2, i))
					{
						flag = true;
					}
					else
					{
						ColumnType columnType = schema2.GetColumnType(i);
						if (!columnType.IsCachable())
						{
							flag = true;
							channel.Warning("Column #{0} in the right data view is dropped because it has a non-cachable type: {1}.", new object[] { i, columnType });
						}
						else
						{
							string columnName = schema2.GetColumnName(i);
							string text = columnName;
							int num;
							if (rename && !hashSet.Contains(columnName) && (schema.TryGetColumnIndex(columnName, ref num) || columnName == relativeIndexColumnName))
							{
								flag = true;
								int num2 = 1;
								for (;;)
								{
									text = string.Format(renameFormat, columnName, num2);
									Contracts.Check(channel, text != columnName, "The renaming format is invalid.");
									if (!schema.TryGetColumnIndex(text, ref num) && text != relativeIndexColumnName && (!schema2.TryGetColumnIndex(text, ref num) || !schema2.GetColumnType(num).IsCachable()))
									{
										break;
									}
									num2++;
								}
							}
							Utils.Add<ChooseColumnsTransform.Column>(ref list, new ChooseColumnsTransform.Column
							{
								source = columnName,
								name = text
							});
						}
					}
				}
				channel.Done();
			}
			if (flag)
			{
				ChooseColumnsTransform.Arguments arguments = new ChooseColumnsTransform.Arguments
				{
					column = Utils.ToArray<ChooseColumnsTransform.Column>(list)
				};
				rightDv = new ChooseColumnsTransform(arguments, this._host, rightDv);
			}
			return new LeftJoinSharedModel(this._host, keyColumns, rightDv);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001D660 File Offset: 0x0001B860
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("LJOIN XF", 65537U, 65537U, 65537U, "LeftJoinTransform", null);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001D684 File Offset: 0x0001B884
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(LeftJoinTransform.GetVersionInfo());
			Utils.WriteBoolByte(ctx.Writer, this._inner);
			ctx.SaveStringOrNull(this._relativeIndexColumnName);
			this._model.Save(ctx);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public override long? GetRowCount(bool lazy = true)
		{
			return this._leftJoinDv.GetRowCount(lazy);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001D6EA File Offset: 0x0001B8EA
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return new bool?(true);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001D6F2 File Offset: 0x0001B8F2
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			return this._leftJoinDv.GetRowCursor(predicate, rand);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001D701 File Offset: 0x0001B901
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			return this._leftJoinDv.GetRowCursorSet(out consolidator, predicate, n, rand);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001D738 File Offset: 0x0001B938
		public static LeftJoinTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(LeftJoinTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckValue<IHostEnvironment>(env, env, "env");
			IHost h = env.Register("LeftJoinTransform");
			return HostExtensions.Apply<LeftJoinTransform>(h, "Loading Model", (IChannel ch) => new LeftJoinTransform(ch, ctx, h, input));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		private LeftJoinTransform(IChannel ch, ModelLoadContext ctx, IHostEnvironment env, IDataView input)
			: base(env, "LeftJoinTransform", input)
		{
			Contracts.CheckValue<IChannel>(this._host, ch, "ch");
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			this._inner = Utils.ReadBoolByte(ctx.Reader);
			this._relativeIndexColumnName = ctx.LoadStringOrNull();
			Contracts.CheckDecode(this._host, this._relativeIndexColumnName == null || !string.IsNullOrWhiteSpace(this._relativeIndexColumnName));
			bool flag = this._relativeIndexColumnName != null;
			this._model = LeftJoinSharedModel.Create(ctx, this._host);
			this._leftJoinDv = new LeftJoinDataView(this._host, input, this._model.TransformedRightDv, this._model.KeyColumns, this._inner, new bool?(flag), this._relativeIndexColumnName);
		}

		// Token: 0x040002A4 RID: 676
		internal const string Summary = "Performs the logical inner join or left-outer join of two data views.";

		// Token: 0x040002A5 RID: 677
		internal const string RenameDefaultFormat = "{0}_{1:000}";

		// Token: 0x040002A6 RID: 678
		private const string RegistrationName = "LeftJoinTransform";

		// Token: 0x040002A7 RID: 679
		public const string LoaderSignature = "LeftJoinTransform";

		// Token: 0x040002A8 RID: 680
		private readonly bool _inner;

		// Token: 0x040002A9 RID: 681
		private readonly string _relativeIndexColumnName;

		// Token: 0x040002AA RID: 682
		private readonly LeftJoinSharedModel _model;

		// Token: 0x040002AB RID: 683
		private readonly LeftJoinDataView _leftJoinDv;

		// Token: 0x0200010B RID: 267
		public enum JoinType
		{
			// Token: 0x040002AE RID: 686
			Outer,
			// Token: 0x040002AF RID: 687
			Inner
		}

		// Token: 0x0200010C RID: 268
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000571 RID: 1393 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
			public static LeftJoinTransform.Column Parse(string str)
			{
				LeftJoinTransform.Column column = new LeftJoinTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x0001D8C7 File Offset: 0x0001BAC7
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x0200010D RID: 269
		public sealed class Arguments
		{
			// Token: 0x040002B0 RID: 688
			[Argument(0, IsInputFileName = true, HelpText = "The file containing the right data view", ShortName = "data", SortOrder = 2)]
			public string dataFile;

			// Token: 0x040002B1 RID: 689
			[Argument(4, HelpText = "The data loader", NullName = "<Auto>")]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x040002B2 RID: 690
			[Argument(4, HelpText = "Join key column names", ShortName = "keyCol", SortOrder = 1)]
			public LeftJoinDataViewBase.JoinKeyColumn[] keyColumns;

			// Token: 0x040002B3 RID: 691
			[Argument(0, HelpText = "Whether to add the relative index column", ShortName = "ri", SortOrder = 3)]
			public bool? addRelativeIndex;

			// Token: 0x040002B4 RID: 692
			[Argument(0, HelpText = "The column name for the generated relative index", ShortName = "riName", SortOrder = 4)]
			public string relativeIndexColumnName;

			// Token: 0x040002B5 RID: 693
			[Argument(0, HelpText = "The type of join", ShortName = "type", SortOrder = 1)]
			public LeftJoinTransform.JoinType type;

			// Token: 0x040002B6 RID: 694
			[Argument(0, HelpText = "Whether the right column is renamed if there is a name conflict", ShortName = "rename", SortOrder = 3)]
			public bool rename;

			// Token: 0x040002B7 RID: 695
			[Argument(0, Hide = true, HelpText = "The string format for renaming columns from right data view", ShortName = "format", SortOrder = 99)]
			public string renameFormat = "{0}_{1:000}";
		}
	}
}
