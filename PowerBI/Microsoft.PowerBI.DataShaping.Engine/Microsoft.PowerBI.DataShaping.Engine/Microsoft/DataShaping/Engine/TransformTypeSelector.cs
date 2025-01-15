using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.Analytics.Contracts.DaxDataTransform;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000020 RID: 32
	internal sealed class TransformTypeSelector : DataShapeVisitor
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000034BD File Offset: 0x000016BD
		private TransformTypeSelector(IDaxDataTransformMetadataFactory daxDataTransformMetadataFactory, IDataTransformPluginFactory dataTransformPluginFactory)
		{
			this._daxDataTransformMetadataFactory = daxDataTransformMetadataFactory;
			this._dataTransformPluginFactory = dataTransformPluginFactory;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000034D3 File Offset: 0x000016D3
		public static TransformType Select(DataShape dataShape, IDaxDataTransformMetadataFactory daxDataTransformMetadataFactory, IDataTransformPluginFactory dataTransformPluginFactory)
		{
			TransformTypeSelector transformTypeSelector = new TransformTypeSelector(daxDataTransformMetadataFactory, dataTransformPluginFactory);
			transformTypeSelector.Visit(dataShape);
			return transformTypeSelector.DetermineTransformType();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000034E8 File Offset: 0x000016E8
		protected override void Visit(DataTransform dataTransform)
		{
			if (dataTransform.Algorithm.IsValid)
			{
				object obj = this._daxDataTransformMetadataFactory != null && this._daxDataTransformMetadataFactory.HasTransform(dataTransform.Algorithm.Value);
				bool flag = this._dataTransformPluginFactory != null && this._dataTransformPluginFactory.HasTransform(dataTransform.Algorithm.Value);
				object obj2 = obj;
				if (obj2 != null)
				{
					this._algorithmsFoundInDaxTransformMetadata = true;
				}
				if (flag)
				{
					this._algorithmsFoundInProcessingTransformMetadata = true;
				}
				if ((obj2 & flag) != null)
				{
					throw new EngineException(EngineMessages.AmbiguousTransformConfiguration(EngineMessageSeverity.Error, dataTransform.Algorithm.Value));
				}
				if (obj2 == null && !flag)
				{
					throw new EngineException(EngineMessages.MissingTransformConfiguration(EngineMessageSeverity.Error, dataTransform.Algorithm.Value));
				}
				if (this._algorithmsFoundInDaxTransformMetadata && this._algorithmsFoundInProcessingTransformMetadata)
				{
					throw new EngineException(EngineMessages.UnsupportedTransformConfiguration(EngineMessageSeverity.Error));
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000035B0 File Offset: 0x000017B0
		private TransformType DetermineTransformType()
		{
			if (!this._algorithmsFoundInDaxTransformMetadata && !this._algorithmsFoundInProcessingTransformMetadata)
			{
				return TransformType.None;
			}
			if (this._algorithmsFoundInDaxTransformMetadata)
			{
				return TransformType.Query;
			}
			return TransformType.Processing;
		}

		// Token: 0x04000079 RID: 121
		private readonly IDaxDataTransformMetadataFactory _daxDataTransformMetadataFactory;

		// Token: 0x0400007A RID: 122
		private readonly IDataTransformPluginFactory _dataTransformPluginFactory;

		// Token: 0x0400007B RID: 123
		private bool _algorithmsFoundInDaxTransformMetadata;

		// Token: 0x0400007C RID: 124
		private bool _algorithmsFoundInProcessingTransformMetadata;
	}
}
