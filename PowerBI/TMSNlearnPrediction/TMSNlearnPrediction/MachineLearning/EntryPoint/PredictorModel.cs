using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.EntryPoint
{
	// Token: 0x0200042D RID: 1069
	public sealed class PredictorModel : IPredictorModel
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x00081547 File Offset: 0x0007F747
		public ITransformModel TransformModel
		{
			get
			{
				return this._transformModel;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0008154F File Offset: 0x0007F74F
		public IPredictor Predictor
		{
			get
			{
				return this._predictor;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00081558 File Offset: 0x0007F758
		public PredictorModel(IHostEnvironment env, RoleMappedData trainingData, IDataView startingData, IPredictor predictor)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<RoleMappedData>(env, trainingData, "trainingData");
			Contracts.CheckValue<IPredictor>(env, predictor, "predictor");
			this._transformModel = new TransformModel(env, trainingData.Data, startingData);
			this._roleMappings = trainingData.Schema.GetColumnRoleNames().ToArray<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
			this._predictor = predictor;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000815C0 File Offset: 0x0007F7C0
		public PredictorModel(IHostEnvironment env, Stream stream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, stream, "stream");
			using (IChannel channel = env.Start("Loading predictor model"))
			{
				this._transformModel = new TransformModel(env, stream);
				IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable = ModelFileUtils.LoadRoleMappingsOrNull(env, stream);
				Contracts.CheckDecode(env, enumerable != null, "Predictor model must contain role mappings");
				this._roleMappings = enumerable.ToArray<KeyValuePair<RoleMappedSchema.ColumnRole, string>>();
				this._predictor = ModelFileUtils.LoadPredictorOrNull(env, stream);
				Contracts.CheckDecode(env, this._predictor != null, "Predictor model must contain a predictor");
				channel.Done();
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00081670 File Offset: 0x0007F870
		private PredictorModel(ITransformModel transformModel, IPredictor predictor, KeyValuePair<RoleMappedSchema.ColumnRole, string>[] roleMappings)
		{
			this._transformModel = transformModel;
			this._predictor = predictor;
			this._roleMappings = roleMappings;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00081690 File Offset: 0x0007F890
		public void Save(IHostEnvironment env, Stream stream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, stream, "stream");
			using (IChannel channel = env.Start("Saving predictor model"))
			{
				IDataView dataView = new EmptyDataView(env, this._transformModel.InputSchema);
				dataView = this._transformModel.Apply(env, dataView);
				RoleMappedData roleMappedData = RoleMappedData.CreateOpt(dataView, this._roleMappings);
				TrainUtils.SaveModel(env, channel, stream, this._predictor, roleMappedData, null);
				channel.Done();
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00081720 File Offset: 0x0007F920
		public IPredictorModel Apply(IHostEnvironment env, ITransformModel transformModel)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ITransformModel>(env, transformModel, "transformModel");
			ITransformModel transformModel2 = this._transformModel.Apply(env, transformModel);
			return new PredictorModel(transformModel2, this._predictor, this._roleMappings);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00081764 File Offset: 0x0007F964
		public void PrepareData(IHostEnvironment env, IDataView input, out RoleMappedData roleMappedData, out IPredictor predictor)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, input, "input");
			input = this._transformModel.Apply(env, input);
			roleMappedData = RoleMappedData.CreateOpt(input, this._roleMappings);
			predictor = this._predictor;
		}

		// Token: 0x04000DA1 RID: 3489
		private readonly IPredictor _predictor;

		// Token: 0x04000DA2 RID: 3490
		private readonly ITransformModel _transformModel;

		// Token: 0x04000DA3 RID: 3491
		private readonly KeyValuePair<RoleMappedSchema.ColumnRole, string>[] _roleMappings;
	}
}
