using System;
using System.Collections.Generic;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001DC RID: 476
	public sealed class LsdlDocumentTypeConverter : IYamlTypeConverter
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0001334B File Offset: 0x0001154B
		public LsdlDocumentTypeConverter()
		{
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00013353 File Offset: 0x00011553
		public LsdlDocumentTypeConverter(bool omitVersion = false, bool skipValidation = false)
		{
			this._omitVersion = omitVersion;
			this._skipValidation = skipValidation;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00013369 File Offset: 0x00011569
		public bool Accepts(Type type)
		{
			return type == typeof(LsdlDocument);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001337C File Offset: 0x0001157C
		public object ReadYaml(IParser parser, Type type)
		{
			JsonReader jsonReader = JsonReaderFactory.CreateFromYaml(parser);
			if (this._omitVersion)
			{
				JObject jobject = JObject.Load(jsonReader);
				jobject["Version"] = LsdlVersion.Latest.ToString();
				jsonReader = jobject.CreateReader();
			}
			if (this._skipValidation)
			{
				return LsdlJsonSerializer.ReadJson(LsdlDocument.UpgradeJsonReader(jsonReader));
			}
			LsdlDocument lsdlDocument;
			IReadOnlyList<DomainModelDiagnosticMessage> readOnlyList;
			if (!LsdlDocument.TryReadJson(jsonReader, out lsdlDocument, out readOnlyList))
			{
				throw new InvalidOperationException("Cannot read linguistic schema." + Environment.NewLine + readOnlyList.ToFormattedString(false, false));
			}
			return lsdlDocument;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000133FC File Offset: 0x000115FC
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			LsdlDocument lsdlDocument = (LsdlDocument)value;
			object obj;
			if (!this._omitVersion)
			{
				obj = null;
			}
			else
			{
				(obj = new LsdlSerializerSettings()).OmitVersion = true;
			}
			lsdlDocument.WriteNestedYaml(emitter, obj);
		}

		// Token: 0x040007F0 RID: 2032
		private readonly bool _omitVersion;

		// Token: 0x040007F1 RID: 2033
		private readonly bool _skipValidation;
	}
}
