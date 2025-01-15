using System;
using System.IO;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002D6 RID: 726
	public sealed class SaveDataCommand : ICommand
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x0005CCBC File Offset: 0x0005AEBC
		public SaveDataCommand(SaveDataCommand.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<SaveDataCommand.Arguments>(args, "args");
			Contracts.CheckUserArg(!string.IsNullOrEmpty(args.outputDataFile), "outputDataFile");
			Utils.CheckOptionalUserDirectory(args.outputDataFile, "outputDataFile");
			this._impl = new SaveDataCommand.Impl(args, env);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005CD0F File Offset: 0x0005AF0F
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x0400095C RID: 2396
		internal const string Summary = "Given input data, a loader, and possibly transforms, save the data to a new file as parameterized by a saver.";

		// Token: 0x0400095D RID: 2397
		private readonly SaveDataCommand.Impl _impl;

		// Token: 0x020002D7 RID: 727
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x0400095E RID: 2398
			[Argument(4, HelpText = "The data saver to use", NullName = "<Auto>")]
			public SubComponent<IDataSaver, SignatureDataSaver> saver;

			// Token: 0x0400095F RID: 2399
			[Argument(0, HelpText = "File to save the data", ShortName = "dout")]
			public string outputDataFile;

			// Token: 0x04000960 RID: 2400
			[Argument(0, HelpText = "Whether to include hidden columns", ShortName = "keep")]
			public bool keepHidden;
		}

		// Token: 0x020002D8 RID: 728
		private sealed class Impl : DataCommand.ImplBase<SaveDataCommand.Arguments>
		{
			// Token: 0x060010A5 RID: 4261 RVA: 0x0005CD24 File Offset: 0x0005AF24
			public Impl(SaveDataCommand.Arguments args, IHostEnvironment env)
				: base("SaveDataCommand", args, env, null)
			{
			}

			// Token: 0x060010A6 RID: 4262 RVA: 0x0005CD48 File Offset: 0x0005AF48
			public override void Run()
			{
				string text = "SaveDataCommand";
				using (IChannel channel = this._host.Start(text))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x060010A7 RID: 4263 RVA: 0x0005CD94 File Offset: 0x0005AF94
			private void RunCore(IChannel ch)
			{
				SubComponent<IDataSaver, SignatureDataSaver> subComponent = this._args.saver;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					string extension = Path.GetExtension(this._args.outputDataFile);
					bool flag = string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase);
					bool flag2 = string.Equals(extension, ".tdv", StringComparison.OrdinalIgnoreCase);
					subComponent = new SubComponent<IDataSaver, SignatureDataSaver>(flag ? "BinarySaver" : (flag2 ? "TransposeSaver" : "TextSaver"));
				}
				IDataSaver dataSaver = ComponentCatalog.CreateInstance<IDataSaver, SignatureDataSaver>(subComponent, new object[] { this._host });
				IDataLoader dataLoader = base.CreateAndSaveLoader("TextLoader");
				using (IFileHandle fileHandle = this._host.CreateOutputFile(this._args.outputDataFile))
				{
					DataSaverUtils.SaveDataView(ch, dataSaver, dataLoader, fileHandle, this._args.keepHidden);
				}
			}
		}
	}
}
