using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Tmdl.Converters;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000132 RID: 306
	internal abstract class MetadataObjectConfiguration : IMetadataObjectConfiguration
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x0008C157 File Offset: 0x0008A357
		protected MetadataObjectConfiguration(TmdlSchema schema, IReadOnlyDictionary<ObjectType, IMetadataObjectConverter> converters)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (converters == null)
			{
				throw new ArgumentNullException("converters");
			}
			this.Schema = schema;
			this.Converters = converters;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0008C189 File Offset: 0x0008A389
		public static IMetadataObjectConfiguration Default
		{
			get
			{
				return MetadataObjectConfiguration._default.Value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0008C195 File Offset: 0x0008A395
		public TmdlSchema Schema { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0008C19D File Offset: 0x0008A39D
		public IReadOnlyDictionary<ObjectType, IMetadataObjectConverter> Converters { get; }

		// Token: 0x060014AE RID: 5294
		public abstract TmdlObjectInfo GetSchema(ObjectType objectType);

		// Token: 0x060014AF RID: 5295 RVA: 0x0008C1A5 File Offset: 0x0008A3A5
		internal static TmdlSchema GetFullSchema()
		{
			return ((MetadataObjectConfiguration.DefaultMetadataObjectConfiguration)MetadataObjectConfiguration._default.Value).FullSchema;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0008C1BC File Offset: 0x0008A3BC
		private static MetadataObjectConfiguration BuildDefaultConfiguration()
		{
			IReadOnlyDictionary<ObjectType, TmdlObjectInfo> readOnlyDictionary = TmdlObjectInfoWriter.BuildFullMetadataSchema(TmdlSerializationHelper.DefaultFilter, new SerializationActivityContext(MetadataSerializationMode.Tmdl, CompatibilityMode.PowerBI, 1000000, false, false));
			Dictionary<ObjectType, IMetadataObjectConverter> dictionary = new Dictionary<ObjectType, IMetadataObjectConverter>();
			foreach (KeyValuePair<ObjectType, TmdlObjectInfo> keyValuePair in readOnlyDictionary.Where((KeyValuePair<ObjectType, TmdlObjectInfo> kvp) => kvp.Key != ObjectType.Database))
			{
				dictionary.Add(keyValuePair.Key, new MetadataObjectConverter(keyValuePair.Value));
			}
			return new MetadataObjectConfiguration.DefaultMetadataObjectConfiguration(TmdlSchema.CreateStandardReadOnlySchema(readOnlyDictionary, false), TmdlSchema.CreateStandardReadOnlySchema(readOnlyDictionary, true), dictionary);
		}

		// Token: 0x04000347 RID: 839
		private static readonly Lazy<MetadataObjectConfiguration> _default = new Lazy<MetadataObjectConfiguration>(new Func<MetadataObjectConfiguration>(MetadataObjectConfiguration.BuildDefaultConfiguration));

		// Token: 0x0200031C RID: 796
		private sealed class DefaultMetadataObjectConfiguration : MetadataObjectConfiguration
		{
			// Token: 0x060024DC RID: 9436 RVA: 0x000E55E3 File Offset: 0x000E37E3
			public DefaultMetadataObjectConfiguration(TmdlSchema schema, TmdlSchema fullSchema, IReadOnlyDictionary<ObjectType, IMetadataObjectConverter> converters)
				: base(schema, converters)
			{
				this.FullSchema = fullSchema;
			}

			// Token: 0x060024DD RID: 9437 RVA: 0x000E55F4 File Offset: 0x000E37F4
			public override TmdlObjectInfo GetSchema(ObjectType objectType)
			{
				IMetadataObjectConverter metadataObjectConverter;
				if (!base.Converters.TryGetValue(objectType, out metadataObjectConverter))
				{
					throw new ArgumentException(TomSR.Exception_MetadataConfigUnsupportedType(objectType.ToString("G")), "objectType");
				}
				return ((MetadataObjectConverter)metadataObjectConverter).Schema;
			}

			// Token: 0x17000788 RID: 1928
			// (get) Token: 0x060024DE RID: 9438 RVA: 0x000E563C File Offset: 0x000E383C
			internal TmdlSchema FullSchema { get; }
		}
	}
}
