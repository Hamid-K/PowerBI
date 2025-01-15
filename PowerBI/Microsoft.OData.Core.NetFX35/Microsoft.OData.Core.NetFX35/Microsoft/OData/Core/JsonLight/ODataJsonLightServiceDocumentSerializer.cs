using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000FC RID: 252
	internal sealed class ODataJsonLightServiceDocumentSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00022E88 File Offset: 0x00021088
		internal ODataJsonLightServiceDocumentSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00023058 File Offset: 0x00021258
		internal void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				this.WriteContextUriProperty(ODataPayloadKind.ServiceDocument, null, null, null);
				this.JsonWriter.WriteValuePropertyName();
				this.JsonWriter.StartArrayScope();
				if (serviceDocument.EntitySets != null)
				{
					foreach (ODataEntitySetInfo odataEntitySetInfo in serviceDocument.EntitySets)
					{
						this.WriteServiceDocumentElement(odataEntitySetInfo, "EntitySet");
					}
				}
				if (serviceDocument.Singletons != null)
				{
					foreach (ODataSingletonInfo odataSingletonInfo in serviceDocument.Singletons)
					{
						this.WriteServiceDocumentElement(odataSingletonInfo, "Singleton");
					}
				}
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				if (serviceDocument.FunctionImports != null)
				{
					foreach (ODataFunctionImportInfo odataFunctionImportInfo in serviceDocument.FunctionImports)
					{
						if (odataFunctionImportInfo == null)
						{
							throw new ODataException(Strings.ValidationUtils_WorkspaceResourceMustNotContainNullItem);
						}
						if (!hashSet.Contains(odataFunctionImportInfo.Name))
						{
							hashSet.Add(odataFunctionImportInfo.Name);
							this.WriteServiceDocumentElement(odataFunctionImportInfo, "FunctionImport");
						}
					}
				}
				this.JsonWriter.EndArrayScope();
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002308C File Offset: 0x0002128C
		private void WriteServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, string kind)
		{
			this.WriterValidator.ValidateServiceDocumentElement(serviceDocumentElement, ODataFormat.Json);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("name");
			base.JsonWriter.WriteValue(serviceDocumentElement.Name);
			if (!string.IsNullOrEmpty(serviceDocumentElement.Title) && !serviceDocumentElement.Title.Equals(serviceDocumentElement.Name, 4))
			{
				base.JsonWriter.WriteName("title");
				base.JsonWriter.WriteValue(serviceDocumentElement.Title);
			}
			if (kind != null)
			{
				base.JsonWriter.WriteName("kind");
				base.JsonWriter.WriteValue(kind);
			}
			base.JsonWriter.WriteName("url");
			base.JsonWriter.WriteValue(base.UriToString(serviceDocumentElement.Url));
			base.JsonWriter.EndObjectScope();
		}
	}
}
