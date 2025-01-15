using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000A3 RID: 163
	public static class DataCommand
	{
		// Token: 0x020000A4 RID: 164
		public abstract class ArgumentsBase
		{
			// Token: 0x04000166 RID: 358
			[Argument(4, HelpText = "The data loader", ShortName = "loader", SortOrder = 1, NullName = "<Auto>")]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x04000167 RID: 359
			[Argument(0, IsInputFileName = true, HelpText = "The data file", ShortName = "data", SortOrder = 0)]
			public string dataFile;

			// Token: 0x04000168 RID: 360
			[Argument(0, HelpText = "Model file to save", ShortName = "out")]
			public string outputModelFile;

			// Token: 0x04000169 RID: 361
			[Argument(0, IsInputFileName = true, HelpText = "Model file to load", ShortName = "in", SortOrder = 90)]
			public string inputModelFile;

			// Token: 0x0400016A RID: 362
			[Argument(4, HelpText = "Load transforms from model file?", ShortName = "loadTrans", SortOrder = 91)]
			public bool? loadTransforms;

			// Token: 0x0400016B RID: 363
			[Argument(0, HelpText = "Random seed", ShortName = "seed", SortOrder = 101)]
			public int? randomSeed;

			// Token: 0x0400016C RID: 364
			[Argument(0, HelpText = "Verbose?", ShortName = "v", Hide = true)]
			public bool? verbose;

			// Token: 0x0400016D RID: 365
			[Argument(4, HelpText = "Desired degree of parallelism in the data pipeline", ShortName = "n")]
			public int? parallel;

			// Token: 0x0400016E RID: 366
			[Argument(4, HelpText = "Transform", ShortName = "xf")]
			public KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] transform;
		}

		// Token: 0x020000A5 RID: 165
		public abstract class ImplBase<TArgs> where TArgs : DataCommand.ArgumentsBase
		{
			// Token: 0x060002F2 RID: 754 RVA: 0x000126FC File Offset: 0x000108FC
			protected ImplBase(string name, TArgs args, IHostEnvironment env, int? conc = null)
			{
				Contracts.CheckValue<IHostEnvironment>(env, "env");
				Contracts.CheckValue<TArgs>(env, args, "args");
				Contracts.CheckParam(env, conc == null || conc >= 0, "conc", "Degree of concurrency must be non-negative (or null)");
				int? num = conc;
				conc = ((num != null) ? new int?(num.GetValueOrDefault()) : args.parallel);
				Contracts.CheckUserArg(env, !(conc < 0), "parallel", "Degree of parallelism must be non-negative (or null)");
				env = env.Fork(args.randomSeed, args.verbose, conc);
				Contracts.CheckNonWhiteSpace(env, name, "name");
				this._host = env.Register(name);
				this._args = args;
				Utils.CheckOptionalUserDirectory(args.outputModelFile, "outputModelFile");
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x00012800 File Offset: 0x00010A00
			protected ImplBase(DataCommand.ImplBase<TArgs> impl)
			{
				Contracts.CheckValue<DataCommand.ImplBase<TArgs>>(impl, "impl");
				this._args = impl._args;
				this._host = impl._host.Fork();
			}

			// Token: 0x060002F4 RID: 756
			public abstract void Run();

			// Token: 0x060002F5 RID: 757 RVA: 0x00012830 File Offset: 0x00010A30
			protected virtual void SendTelemetry(IChannelProvider prov)
			{
				using (IPipe<TelemetryMessage> pipe = prov.StartPipe<TelemetryMessage>("TelemetryPipe"))
				{
					this.SendTelemetryCore(pipe);
					pipe.Done();
				}
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x00012874 File Offset: 0x00010A74
			protected void SendTelemetryComponent(IPipe<TelemetryMessage> pipe, SubComponent sub)
			{
				if (SubComponentExtensions.IsGood(sub))
				{
					pipe.Send(TelemetryMessage.CreateTrainer(sub.Kind, sub.SubComponentSettings));
				}
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x00012898 File Offset: 0x00010A98
			protected virtual void SendTelemetryCore(IPipe<TelemetryMessage> pipe)
			{
				if (this._args.transform != null)
				{
					foreach (KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>> keyValuePair in this._args.transform)
					{
						this.SendTelemetryComponent(pipe, keyValuePair.Value);
					}
				}
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x000128F1 File Offset: 0x00010AF1
			protected IDataLoader LoadLoader(RepositoryReader rep, string path, bool loadTransforms)
			{
				return ModelFileUtils.LoadLoader(this._host, rep, new MultiFileSource(path), loadTransforms);
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x00012908 File Offset: 0x00010B08
			protected void SaveLoader(IDataLoader loader, string path)
			{
				using (IFileHandle fileHandle = this._host.CreateOutputFile(path))
				{
					LoaderUtils.SaveLoader(loader, fileHandle);
				}
			}

			// Token: 0x060002FA RID: 762 RVA: 0x00012948 File Offset: 0x00010B48
			protected IDataLoader CreateAndSaveLoader(string defaultLoader = "TextLoader")
			{
				IDataLoader dataLoader = this.CreateLoader(defaultLoader);
				if (!string.IsNullOrWhiteSpace(this._args.outputModelFile))
				{
					using (IFileHandle fileHandle = this._host.CreateOutputFile(this._args.outputModelFile))
					{
						LoaderUtils.SaveLoader(dataLoader, fileHandle);
					}
				}
				return dataLoader;
			}

			// Token: 0x060002FB RID: 763 RVA: 0x000129B4 File Offset: 0x00010BB4
			protected IDataLoader CreateLoader(string defaultLoader = "TextLoader")
			{
				IDataLoader dataLoader = this.CreateRawLoader(defaultLoader, null);
				return this.CreateTransformChain(dataLoader);
			}

			// Token: 0x060002FC RID: 764 RVA: 0x000129D3 File Offset: 0x00010BD3
			private IDataLoader CreateTransformChain(IDataLoader loader)
			{
				return CompositeDataLoader.Create(this._host, loader, this._args.transform);
			}

			// Token: 0x060002FD RID: 765 RVA: 0x000129F4 File Offset: 0x00010BF4
			protected IDataLoader CreateRawLoader(string defaultLoader = "TextLoader", string dataFile = null)
			{
				if (string.IsNullOrWhiteSpace(dataFile))
				{
					dataFile = this._args.dataFile;
				}
				IDataLoader dataLoader;
				if (!string.IsNullOrWhiteSpace(this._args.inputModelFile) && !SubComponentExtensions.IsGood(this._args.loader))
				{
					using (IFileHandle fileHandle = this._host.OpenInputFile(this._args.inputModelFile))
					{
						using (Stream stream = fileHandle.OpenReadStream())
						{
							using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
							{
								dataLoader = this.LoadLoader(repositoryReader, dataFile, this._args.loadTransforms ?? true);
							}
						}
						return dataLoader;
					}
				}
				SubComponent<IDataLoader, SignatureDataLoader> subComponent = this._args.loader;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					string extension = Path.GetExtension(dataFile);
					bool flag = string.Equals(extension, ".txt", StringComparison.OrdinalIgnoreCase) || string.Equals(extension, ".tlc", StringComparison.OrdinalIgnoreCase);
					bool flag2 = string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase);
					bool flag3 = string.Equals(extension, ".tdv", StringComparison.OrdinalIgnoreCase);
					subComponent = new SubComponent<IDataLoader, SignatureDataLoader>(flag ? "TextLoader" : (flag2 ? "BinaryLoader" : (flag3 ? "TransposeLoader" : defaultLoader)));
				}
				dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(subComponent, new object[]
				{
					this._host,
					new MultiFileSource(dataFile)
				});
				if (this._args.loadTransforms == true)
				{
					Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(this._args.inputModelFile), "inputModelFile");
					dataLoader = this.LoadTransformChain(dataLoader);
				}
				return dataLoader;
			}

			// Token: 0x060002FE RID: 766 RVA: 0x00012C04 File Offset: 0x00010E04
			private IDataLoader LoadTransformChain(IDataLoader srcData)
			{
				IDataLoader dataLoader;
				using (IFileHandle fileHandle = this._host.OpenInputFile(this._args.inputModelFile))
				{
					using (Stream stream = fileHandle.OpenReadStream())
					{
						using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
						{
							using (Repository.Entry entry = repositoryReader.OpenEntry("DataLoaderModel", "Model.key"))
							{
								using (ModelLoadContext modelLoadContext = new ModelLoadContext(repositoryReader, entry, "DataLoaderModel"))
								{
									dataLoader = CompositeDataLoader.Create(modelLoadContext, this._host, srcData, (string x) => true);
								}
							}
						}
					}
				}
				return dataLoader;
			}

			// Token: 0x0400016F RID: 367
			protected readonly IHost _host;

			// Token: 0x04000170 RID: 368
			protected readonly TArgs _args;
		}
	}
}
