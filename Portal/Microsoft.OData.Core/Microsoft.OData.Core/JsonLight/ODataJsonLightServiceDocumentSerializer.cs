using System;
using System.Collections.Generic;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000253 RID: 595
	internal sealed class ODataJsonLightServiceDocumentSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06001ABE RID: 6846 RVA: 0x0004878E File Offset: 0x0004698E
		internal ODataJsonLightServiceDocumentSerializer(ODataJsonLightOutputContext jsonLightOutputContext)
			: base(jsonLightOutputContext, true)
		{
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000514B8 File Offset: 0x0004F6B8
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

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000514EC File Offset: 0x0004F6EC
		private void WriteServiceDocumentElement(ODataServiceDocumentElement serviceDocumentElement, string kind)
		{
			ValidationUtils.ValidateServiceDocumentElement(serviceDocumentElement, ODataFormat.Json);
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("name");
			base.JsonWriter.WriteValue(serviceDocumentElement.Name);
			if (!string.IsNullOrEmpty(serviceDocumentElement.Title) && !serviceDocumentElement.Title.Equals(serviceDocumentElement.Name, StringComparison.Ordinal))
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
