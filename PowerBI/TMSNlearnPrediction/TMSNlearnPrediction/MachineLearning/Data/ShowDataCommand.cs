using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002D9 RID: 729
	public sealed class ShowDataCommand : ICommand
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0005CE78 File Offset: 0x0005B078
		public ShowDataCommand(ShowDataCommand.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<ShowDataCommand.Arguments>(args, "args");
			this._impl = new ShowDataCommand.Impl(args, env);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005CE98 File Offset: 0x0005B098
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x04000961 RID: 2401
		internal const string Summary = "Given input data, a loader, and possibly transforms, display a sample of the data file.";

		// Token: 0x04000962 RID: 2402
		private readonly ShowDataCommand.Impl _impl;

		// Token: 0x020002DA RID: 730
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000963 RID: 2403
			[Argument(4, HelpText = "Comma separate list of columns to display", ShortName = "cols")]
			public string columns;

			// Token: 0x04000964 RID: 2404
			[Argument(0, HelpText = "Number of rows", ShortName = "rows")]
			public int rows = 20;

			// Token: 0x04000965 RID: 2405
			[Argument(0, HelpText = "Whether to include hidden columns", ShortName = "keep")]
			public bool keepHidden;

			// Token: 0x04000966 RID: 2406
			[Argument(0, HelpText = "Force dense format", ShortName = "dense")]
			public bool dense;
		}

		// Token: 0x020002DB RID: 731
		private sealed class Impl : DataCommand.ImplBase<ShowDataCommand.Arguments>
		{
			// Token: 0x060010AB RID: 4267 RVA: 0x0005CEB8 File Offset: 0x0005B0B8
			public Impl(ShowDataCommand.Arguments args, IHostEnvironment env)
				: base("ShowDataCommand", args, env, null)
			{
			}

			// Token: 0x060010AC RID: 4268 RVA: 0x0005CEDC File Offset: 0x0005B0DC
			public override void Run()
			{
				string text = "ShowDataCommand";
				using (IChannel channel = this._host.Start(text))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x060010AD RID: 4269 RVA: 0x0005CF44 File Offset: 0x0005B144
			private void RunCore(IChannel ch)
			{
				IDataView dataView = base.CreateAndSaveLoader("TextLoader");
				if (!string.IsNullOrWhiteSpace(this._args.columns))
				{
					ChooseColumnsTransform.Arguments arguments = new ChooseColumnsTransform.Arguments();
					arguments.column = (from s in this._args.columns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
						select new ChooseColumnsTransform.Column
						{
							name = s
						}).ToArray<ChooseColumnsTransform.Column>();
					if (Utils.Size<ChooseColumnsTransform.Column>(arguments.column) > 0)
					{
						dataView = new ChooseColumnsTransform(arguments, this._host, dataView);
					}
				}
				TextSaver textSaver = new TextSaver(new TextSaver.Arguments
				{
					dense = this._args.dense
				}, this._host);
				List<int> list = new List<int>();
				for (int i = 0; i < dataView.Schema.ColumnCount; i++)
				{
					if (this._args.keepHidden || !MetadataUtils.IsHidden(dataView.Schema, i))
					{
						ColumnType columnType = dataView.Schema.GetColumnType(i);
						if (textSaver.IsColumnSavable(columnType))
						{
							list.Add(i);
						}
						else
						{
							ch.Info("The column '{0}' will not be written as it has unsavable column type.", new object[] { dataView.Schema.GetColumnName(i) });
						}
					}
				}
				Contracts.Check(this._host, list.Count > 0, "No valid columns to save");
				if (this._args.rows > 0)
				{
					SkipTakeFilter.TakeArguments takeArguments = new SkipTakeFilter.TakeArguments
					{
						count = (long)this._args.rows
					};
					dataView = SkipTakeFilter.Create(takeArguments, this._host, dataView);
				}
				textSaver.WriteData(dataView, true, list.ToArray());
			}
		}
	}
}
