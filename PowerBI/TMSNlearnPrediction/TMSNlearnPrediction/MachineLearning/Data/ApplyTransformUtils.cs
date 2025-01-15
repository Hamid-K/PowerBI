using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000204 RID: 516
	public static class ApplyTransformUtils
	{
		// Token: 0x06000B7A RID: 2938 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		public static IDataTransform ApplyTransformToData(IHostEnvironment env, IDataTransform transform, IDataView newSource)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataTransform>(env, transform, "transform");
			Contracts.CheckValue<IDataView>(env, newSource, "newSource");
			ITransformTemplate transformTemplate = transform as ITransformTemplate;
			if (transformTemplate != null)
			{
				return transformTemplate.ApplyToData(env, newSource);
			}
			IDataTransform dataTransform2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (RepositoryWriter repositoryWriter = RepositoryWriter.CreateNew(memoryStream, true))
				{
					ModelSaveContext.SaveModel<IDataTransform>(repositoryWriter, transform, "model");
					repositoryWriter.Commit();
				}
				memoryStream.Position = 0L;
				using (RepositoryReader repositoryReader = RepositoryReader.Open(memoryStream, true))
				{
					IDataTransform dataTransform;
					ModelLoadContext.LoadModel<IDataTransform, SignatureLoadDataTransform>(out dataTransform, repositoryReader, "model", new object[] { env, newSource });
					dataTransform2 = dataTransform;
				}
			}
			return dataTransform2;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003E89C File Offset: 0x0003CA9C
		public static IDataView ApplyAllTransformsToData(IHostEnvironment env, IDataView chain, IDataView newSource, IDataView oldSource = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, chain, "transform");
			Contracts.CheckValue<IDataView>(env, newSource, "newSource");
			CompositeDataLoader compositeDataLoader = chain as CompositeDataLoader;
			if (compositeDataLoader != null)
			{
				chain = compositeDataLoader.View;
			}
			List<IDataTransform> list = new List<IDataTransform>();
			IDataTransform dataTransform;
			while ((dataTransform = chain as IDataTransform) != null && chain != oldSource)
			{
				list.Add(dataTransform);
				chain = dataTransform.Source;
				compositeDataLoader = chain as CompositeDataLoader;
				if (compositeDataLoader != null)
				{
					chain = compositeDataLoader.View;
				}
			}
			list.Reverse();
			Contracts.Check(env, oldSource == null || chain == oldSource, "Source data not found in the chain");
			IDataView dataView = newSource;
			foreach (IDataTransform dataTransform2 in list)
			{
				dataView = ApplyTransformUtils.ApplyTransformToData(env, dataTransform2, dataView);
			}
			return dataView;
		}
	}
}
