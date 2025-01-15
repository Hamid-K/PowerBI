using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000397 RID: 919
	public sealed class ScoreCommand : ICommand
	{
		// Token: 0x060013C1 RID: 5057 RVA: 0x00070363 File Offset: 0x0006E563
		public ScoreCommand(ScoreCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new ScoreCommand.Impl(args, env);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00070378 File Offset: 0x0006E578
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000B79 RID: 2937
		internal const string Summary = "Scores a data file.";

		// Token: 0x04000B7A RID: 2938
		private readonly ScoreCommand.Impl _impl;

		// Token: 0x02000398 RID: 920
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000B7B RID: 2939
			[Argument(4, HelpText = "Column to use for features when scorer is not defined", ShortName = "feat")]
			public string featureColumn = "Features";

			// Token: 0x04000B7C RID: 2940
			[Argument(0, HelpText = "Group column name", ShortName = "group")]
			public string groupColumn = "GroupId";

			// Token: 0x04000B7D RID: 2941
			[Argument(4, HelpText = "Input columns: Columns with custom kinds declared through key assignments, e.g., col[Kind]=Name to assign column named 'Name' kind 'Kind'", ShortName = "col", SortOrder = 10)]
			public KeyValuePair<string, string>[] customColumn;

			// Token: 0x04000B7E RID: 2942
			[Argument(4, HelpText = "Scorer to use")]
			public SubComponent<IDataScorerTransform, SignatureDataScorer> scorer;

			// Token: 0x04000B7F RID: 2943
			[Argument(4, HelpText = "The data saver to use")]
			public SubComponent<IDataSaver, SignatureDataSaver> saver;

			// Token: 0x04000B80 RID: 2944
			[Argument(4, HelpText = "File to save the data", ShortName = "dout")]
			public string outputDataFile;

			// Token: 0x04000B81 RID: 2945
			[Argument(0, HelpText = "Whether to include hidden columns", ShortName = "keep")]
			public bool keepHidden;

			// Token: 0x04000B82 RID: 2946
			[Argument(4, HelpText = "Post processing transform", ShortName = "pxf")]
			public KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] postTransform;

			// Token: 0x04000B83 RID: 2947
			[Argument(0, HelpText = "Whether to output all columns or just scores", ShortName = "all")]
			public bool? outputAllColumns;

			// Token: 0x04000B84 RID: 2948
			[Argument(4, HelpText = "What columns to output beyond score columns, if outputAllColumns=-.", ShortName = "outCol")]
			public string[] outputColumn;
		}

		// Token: 0x02000399 RID: 921
		private sealed class Impl : DataCommand.ImplBase<ScoreCommand.Arguments>
		{
			// Token: 0x060013C4 RID: 5060 RVA: 0x000703A4 File Offset: 0x0006E5A4
			public Impl(ScoreCommand.Arguments args, IHostEnvironment env)
				: base("ScoreCommand", args, env, null)
			{
				Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.inputModelFile), "inputModelFile", "The input model file is required.");
				Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.outputDataFile), "outputDataFile", "The output data file is required.");
				Utils.CheckOptionalUserDirectory(args.outputDataFile, "outputDataFile");
			}

			// Token: 0x060013C5 RID: 5061 RVA: 0x00070420 File Offset: 0x0006E620
			public override void Run()
			{
				using (IChannel channel = this._host.Start("Score"))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x060013C6 RID: 5062 RVA: 0x000704A4 File Offset: 0x0006E6A4
			private void RunCore(IChannel ch)
			{
				ch.Trace("Creating loader");
				IDataLoader dataLoader = base.CreateLoader("TextLoader");
				IPredictor predictor;
				using (IFileHandle fileHandle = this._host.OpenInputFile(this._args.inputModelFile))
				{
					using (Stream stream = fileHandle.OpenReadStream())
					{
						using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
						{
							ch.Trace("Loading predictor");
							ModelLoadContext.LoadModel<IPredictor, SignatureLoadModel>(out predictor, repositoryReader, "Predictor", new object[] { this._host });
						}
					}
				}
				ch.Trace("Creating pipeline");
				SubComponent<IDataScorerTransform, SignatureDataScorer> scorer = this._args.scorer;
				ISchemaBindableMapper schemaBindableMapper = ScoreUtils.GetSchemaBindableMapper(this._host, predictor, scorer);
				string text = TrainUtils.MatchNameOrDefaultOrNull(ch, dataLoader.Schema, "featureColumn", this._args.featureColumn, "Features");
				string text2 = TrainUtils.MatchNameOrDefaultOrNull(ch, dataLoader.Schema, "groupColumn", this._args.groupColumn, "GroupId");
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = TrainUtils.CheckAndGenerateCustomColumns(ch, this._args.customColumn);
				RoleMappedSchema roleMappedSchema = TrainUtils.CreateRoleMappedSchemaOpt(dataLoader.Schema, text, text2, enumerable);
				ISchemaBoundMapper mapper = schemaBindableMapper.Bind(this._host, roleMappedSchema);
				if (!SubComponentExtensions.IsGood(scorer))
				{
					scorer = ScoreUtils.GetScorerComponent(mapper);
				}
				dataLoader = CompositeDataLoader.ApplyTransform(this._host, dataLoader, "Scorer", scorer.ToString(), (IHostEnvironment env, IDataView view) => ComponentCatalog.CreateInstance<IDataScorerTransform, SignatureDataScorer>(scorer, new object[] { env, view, mapper }));
				dataLoader = CompositeDataLoader.Create(this._host, dataLoader, this._args.postTransform);
				if (!string.IsNullOrWhiteSpace(this._args.outputModelFile))
				{
					ch.Trace("Saving the data pipe");
					base.SaveLoader(dataLoader, this._args.outputModelFile);
				}
				ch.Trace("Creating saver");
				SubComponent<IDataSaver, SignatureDataSaver> subComponent = this._args.saver;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					string extension = Path.GetExtension(this._args.outputDataFile);
					subComponent = new SubComponent<IDataSaver, SignatureDataSaver>((extension == ".txt" || extension == ".tlc") ? "TextSaver" : "BinarySaver");
				}
				IDataSaver dataSaver = ComponentCatalog.CreateInstance<IDataSaver, SignatureDataSaver>(subComponent, new object[] { this._host });
				bool flag = dataSaver is BinaryWriter;
				bool flag2 = this._args.outputAllColumns == true || (this._args.outputAllColumns == null && Utils.Size<string>(this._args.outputColumn) == 0 && flag);
				bool flag3 = this._args.outputAllColumns == true || Utils.Size<string>(this._args.outputColumn) == 0;
				if (this._args.outputAllColumns == true && Utils.Size<string>(this._args.outputColumn) != 0)
				{
					ch.Warning("outputAllColumns=+ always writes all columns irrespective of outputColumn specified.");
				}
				if (!flag2 && Utils.Size<string>(this._args.outputColumn) != 0)
				{
					foreach (string text3 in this._args.outputColumn)
					{
						int num;
						if (!dataLoader.Schema.TryGetColumnIndex(text3, ref num))
						{
							throw Contracts.ExceptUserArg(ch, "outputColumn", "Column '{0}' not found.", new object[] { text3 });
						}
					}
				}
				uint num2 = 0U;
				if (!flag2)
				{
					int num3;
					num2 = MetadataUtils.GetMaxMetadataKind(dataLoader.Schema, ref num3, "ScoreColumnSetId", null);
				}
				List<int> list = new List<int>();
				for (int j = 0; j < dataLoader.Schema.ColumnCount; j++)
				{
					if ((this._args.keepHidden || !MetadataUtils.IsHidden(dataLoader.Schema, j)) && (flag2 || this.ShouldAddColumn(dataLoader.Schema, j, num2, flag3)))
					{
						ColumnType columnType = dataLoader.Schema.GetColumnType(j);
						if (dataSaver.IsColumnSavable(columnType))
						{
							list.Add(j);
						}
						else
						{
							ch.Warning("The column '{0}' will not be written as it has unsavable column type.", new object[] { dataLoader.Schema.GetColumnName(j) });
						}
					}
				}
				Contracts.Check(ch, list.Count > 0, "No valid columns to save");
				ch.Trace("Scoring and saving data");
				using (IFileHandle fileHandle2 = this._host.CreateOutputFile(this._args.outputDataFile))
				{
					using (Stream stream2 = fileHandle2.CreateWriteStream())
					{
						dataSaver.SaveData(stream2, dataLoader, list.ToArray());
					}
				}
			}

			// Token: 0x060013C7 RID: 5063 RVA: 0x000709CC File Offset: 0x0006EBCC
			private bool ShouldAddColumn(ISchema schema, int i, uint scoreSet, bool outputNamesAndLabels)
			{
				uint num = 0U;
				string columnName;
				return (MetadataUtils.TryGetMetadata<uint>(schema, MetadataUtils.ScoreColumnSetIdType.AsPrimitive, "ScoreColumnSetId", i, ref num) && num == scoreSet) || (outputNamesAndLabels && (columnName = schema.GetColumnName(i)) != null && (columnName == "Label" || columnName == "Name" || columnName == "Names")) || (this._args.outputColumn != null && Array.FindIndex<string>(this._args.outputColumn, new Predicate<string>(schema.GetColumnName(i).Equals)) >= 0);
			}
		}
	}
}
