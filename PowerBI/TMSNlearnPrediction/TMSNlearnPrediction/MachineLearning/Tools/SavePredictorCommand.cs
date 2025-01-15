using System;
using System.IO;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Tools
{
	// Token: 0x02000482 RID: 1154
	public sealed class SavePredictorCommand : ICommand
	{
		// Token: 0x06001816 RID: 6166 RVA: 0x0008A088 File Offset: 0x00088288
		public SavePredictorCommand(SavePredictorCommand.Arguments args, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("SavePredictorCommand");
			Contracts.CheckValue<SavePredictorCommand.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.inputModelFile), "inputModelFile", "Must specify input model file");
			this._args = args;
			this.CheckOutputDirectories();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0008A0F8 File Offset: 0x000882F8
		private void CheckOutputDirectories()
		{
			Utils.CheckOptionalUserDirectory(this._args.binaryFile, "binaryFile");
			Utils.CheckOptionalUserDirectory(this._args.codeFile, "codeFile");
			Utils.CheckOptionalUserDirectory(this._args.iniFile, "iniFile");
			Utils.CheckOptionalUserDirectory(this._args.summaryFile, "summaryFile");
			Utils.CheckOptionalUserDirectory(this._args.textFile, "textFile");
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0008A170 File Offset: 0x00088370
		public void Run()
		{
			using (IFileHandle fileHandle = this._host.OpenInputFile(this._args.inputModelFile))
			{
				using (Stream stream = fileHandle.OpenReadStream())
				{
					using (IFileHandle fileHandle2 = this.CreateFile(this._args.binaryFile))
					{
						using (Stream stream2 = this.CreateStrm(fileHandle2))
						{
							using (IFileHandle fileHandle3 = this.CreateFile(this._args.summaryFile))
							{
								using (Stream stream3 = this.CreateStrm(fileHandle3))
								{
									using (IFileHandle fileHandle4 = this.CreateFile(this._args.textFile))
									{
										using (Stream stream4 = this.CreateStrm(fileHandle4))
										{
											using (IFileHandle fileHandle5 = this.CreateFile(this._args.iniFile))
											{
												using (Stream stream5 = this.CreateStrm(fileHandle5))
												{
													using (IFileHandle fileHandle6 = this.CreateFile(this._args.codeFile))
													{
														using (Stream stream6 = this.CreateStrm(fileHandle6))
														{
															SavePredictorUtils.SavePredictor(this._host, stream, stream2, stream3, stream4, stream5, stream6);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0008A404 File Offset: 0x00088604
		private IFileHandle CreateFile(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				return null;
			}
			return this._host.CreateOutputFile(path);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0008A41C File Offset: 0x0008861C
		private Stream CreateStrm(IFileHandle file)
		{
			if (file == null)
			{
				return null;
			}
			return file.CreateWriteStream();
		}

		// Token: 0x04000E7F RID: 3711
		internal const string Summary = "Given a TLC model file with a predictor, we can output this same predictor in multiple export formats.";

		// Token: 0x04000E80 RID: 3712
		private readonly SavePredictorCommand.Arguments _args;

		// Token: 0x04000E81 RID: 3713
		private readonly IHost _host;

		// Token: 0x02000483 RID: 1155
		public sealed class Arguments
		{
			// Token: 0x04000E82 RID: 3714
			[Argument(0, HelpText = "Model file containing the predictor", ShortName = "in")]
			public string inputModelFile;

			// Token: 0x04000E83 RID: 3715
			[Argument(0, HelpText = "File to save model summary", ShortName = "sum")]
			public string summaryFile;

			// Token: 0x04000E84 RID: 3716
			[Argument(0, HelpText = "File to save in text format", ShortName = "text")]
			public string textFile;

			// Token: 0x04000E85 RID: 3717
			[Argument(0, HelpText = "File to save in INI format", ShortName = "ini")]
			public string iniFile;

			// Token: 0x04000E86 RID: 3718
			[Argument(0, HelpText = "File to save in C++ code", ShortName = "code")]
			public string codeFile;

			// Token: 0x04000E87 RID: 3719
			[Argument(0, HelpText = "File to save in binary format", ShortName = "bin")]
			public string binaryFile;
		}
	}
}
