using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001FB RID: 507
	public static class LoadTransform
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
		public static IDataTransform Create(LoadTransform.Arguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("LoadTransform");
			Contracts.CheckValue<LoadTransform.Arguments>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Contracts.CheckUserArg(host, File.Exists(args.modelFile), "modelFile", "File does not exist");
			bool complement = args.complement || Utils.Size<string>(args.tag) == 0;
			HashSet<string> allTags = new HashSet<string>();
			for (int i = 0; i < Utils.Size<string>(args.tag); i++)
			{
				string text = args.tag[i];
				if (!string.IsNullOrWhiteSpace(text))
				{
					foreach (string text2 in text.Split(new char[] { ',' }))
					{
						if (!string.IsNullOrWhiteSpace(text2))
						{
							allTags.Add(text2.ToLower());
						}
					}
				}
			}
			Func<string, bool> func = delegate(string tag)
			{
				bool flag = allTags.Contains(tag.ToLower());
				return flag == !complement;
			};
			IDataView dataView;
			using (IFileHandle fileHandle = host.OpenInputFile(args.modelFile))
			{
				using (Stream stream = fileHandle.OpenReadStream())
				{
					using (RepositoryReader repositoryReader = RepositoryReader.Open(stream, true))
					{
						using (Repository.Entry entry = repositoryReader.OpenEntry("DataLoaderModel", "Model.key"))
						{
							using (ModelLoadContext modelLoadContext = new ModelLoadContext(repositoryReader, entry, "DataLoaderModel"))
							{
								dataView = CompositeDataLoader.LoadSelectedTransforms(modelLoadContext, input, host, func);
								if (dataView == input)
								{
									string text3 = string.Format(complement ? "transforms that don't have tags from the list: '{0}'" : "transforms that have tags from the list: '{0}'", string.Join(",", allTags));
									throw Contracts.ExceptUserArg(host, "tag", "No transforms were found that match the search criteria ({0})", new object[] { text3 });
								}
							}
						}
					}
				}
			}
			return (IDataTransform)dataView;
		}

		// Token: 0x04000616 RID: 1558
		internal const string Summary = "Loads specified transforms from the model file and applies them to current data.";

		// Token: 0x020001FC RID: 508
		public class Arguments
		{
			// Token: 0x04000617 RID: 1559
			[Argument(1, HelpText = "Model file to load the transforms from", ShortName = "in", SortOrder = 1, IsInputFileName = true)]
			public string modelFile;

			// Token: 0x04000618 RID: 1560
			[Argument(4, HelpText = "The tags (comma-separated) to be loaded (or omitted, if complement+)", SortOrder = 2)]
			public string[] tag;

			// Token: 0x04000619 RID: 1561
			[Argument(0, HelpText = "Whether to load all transforms except those marked by tags", ShortName = "comp", SortOrder = 3)]
			public bool complement;
		}
	}
}
