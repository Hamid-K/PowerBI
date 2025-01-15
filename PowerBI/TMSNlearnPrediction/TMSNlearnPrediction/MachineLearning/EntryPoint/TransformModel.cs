using System;
using System.IO;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.EntryPoint
{
	// Token: 0x0200042E RID: 1070
	public sealed class TransformModel : ITransformModel
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000817A3 File Offset: 0x0007F9A3
		public ISchema InputSchema
		{
			get
			{
				return this._schemaRoot;
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000817AC File Offset: 0x0007F9AC
		public TransformModel(IHostEnvironment env, IDataView result, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, result, "result");
			Contracts.CheckValue<IDataView>(env, input, "input");
			EmptyDataView emptyDataView = new EmptyDataView(env, input.Schema);
			this._schemaRoot = emptyDataView.Schema;
			this._chain = ApplyTransformUtils.ApplyAllTransformsToData(env, result, emptyDataView, input);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0008180A File Offset: 0x0007FA0A
		private TransformModel(IHostEnvironment env, ISchema schemaRoot, IDataView chain)
		{
			this._schemaRoot = schemaRoot;
			this._chain = chain;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00081820 File Offset: 0x0007FA20
		public TransformModel(IHostEnvironment env, ISchema schemaRoot, IDataTransform[] xfs)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ISchema>(env, schemaRoot, "schemaRoot");
			IDataView dataView = new EmptyDataView(env, schemaRoot);
			this._schemaRoot = dataView.Schema;
			if (Utils.Size<IDataTransform>(xfs) > 0)
			{
				foreach (IDataTransform dataTransform in xfs)
				{
					dataView = ApplyTransformUtils.ApplyTransformToData(env, dataTransform, dataView);
				}
			}
			this._chain = dataView;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0008188C File Offset: 0x0007FA8C
		public TransformModel(IHostEnvironment env, Stream stream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, stream, "stream");
			using (IChannel channel = env.Start("Loading transform model"))
			{
				this._chain = ModelFileUtils.LoadPipeline(env, stream, new MultiFileSource(null), true);
				channel.Done();
			}
			IDataView dataView = this._chain;
			for (;;)
			{
				IDataTransform dataTransform = dataView as IDataTransform;
				if (dataTransform == null)
				{
					break;
				}
				dataView = dataTransform.Source;
			}
			this._schemaRoot = dataView.Schema;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00081920 File Offset: 0x0007FB20
		public IDataView Apply(IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, input, "input");
			return ApplyTransformUtils.ApplyAllTransformsToData(env, this._chain, input, null);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00081948 File Offset: 0x0007FB48
		public ITransformModel Apply(IHostEnvironment env, ITransformModel input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ITransformModel>(env, input, "input");
			ISchema inputSchema = input.InputSchema;
			TransformModel transformModel = input as TransformModel;
			IDataView dataView;
			if (transformModel != null)
			{
				dataView = ApplyTransformUtils.ApplyAllTransformsToData(env, this._chain, transformModel._chain, null);
			}
			else
			{
				dataView = new EmptyDataView(env, inputSchema);
				dataView = input.Apply(env, dataView);
				dataView = this.Apply(env, dataView);
			}
			return new TransformModel(env, inputSchema, dataView);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000819B8 File Offset: 0x0007FBB8
		public void Save(IHostEnvironment env, Stream stream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, stream, "stream");
			using (IChannel channel = env.Start("Saving transform model"))
			{
				using (RepositoryWriter repositoryWriter = RepositoryWriter.CreateNew(stream, true))
				{
					channel.Trace("Saving root schema and transformations");
					TrainUtils.SaveDataPipe(env, repositoryWriter, this._chain, true);
					repositoryWriter.Commit();
				}
				channel.Done();
			}
		}

		// Token: 0x04000DA4 RID: 3492
		private readonly ISchema _schemaRoot;

		// Token: 0x04000DA5 RID: 3493
		private readonly IDataView _chain;
	}
}
