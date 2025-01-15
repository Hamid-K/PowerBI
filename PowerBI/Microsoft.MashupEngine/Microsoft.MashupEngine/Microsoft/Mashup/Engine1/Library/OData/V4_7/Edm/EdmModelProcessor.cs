using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x0200080D RID: 2061
	internal sealed class EdmModelProcessor : EdmModelProcessorBase<EdmModelProcessorOutput>
	{
		// Token: 0x06003B9A RID: 15258 RVA: 0x000C183B File Offset: 0x000BFA3B
		public EdmModelProcessor(IEngineHost engineHost, Uri serviceUri, Microsoft.OData.Edm.IEdmModel model, ODataUserSettings userSettings, IResource resource)
			: base(engineHost)
		{
			this.serviceUri = serviceUri;
			this.model = model;
			this.userSettings = userSettings;
			this.resource = resource;
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000C1864 File Offset: 0x000BFA64
		public EdmTypeToTypeValueVisitor EdmTypeToTypeValueVisitor
		{
			get
			{
				if (this.edmTypeToTypeValueVisitor == null || this.edmTypeToTypeValueVisitor.Output != this.output)
				{
					this.edmTypeToTypeValueVisitor = new EdmTypeToTypeValueVisitor(this.resource, this.model, this.userSettings, this.output);
				}
				return this.edmTypeToTypeValueVisitor;
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000C18B8 File Offset: 0x000BFAB8
		protected override void ProcessEntityContainers()
		{
			if (this.model != null && this.model.EntityContainer != null)
			{
				foreach (Microsoft.OData.Edm.IEdmEntityContainerElement edmEntityContainerElement in this.model.EntityContainer.Elements)
				{
					switch (edmEntityContainerElement.ContainerElementKind)
					{
					case Microsoft.OData.Edm.EdmContainerElementKind.EntitySet:
					{
						Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = (Microsoft.OData.Edm.IEdmEntitySet)edmEntityContainerElement;
						this.ProcessAndAddCapability(edmEntitySet.Name, edmEntitySet);
						this.EdmTypeToTypeValueVisitor.VisitType(edmEntitySet.Type);
						break;
					}
					case Microsoft.OData.Edm.EdmContainerElementKind.FunctionImport:
					{
						Microsoft.OData.Edm.IEdmFunctionImport edmFunctionImport = (Microsoft.OData.Edm.IEdmFunctionImport)edmEntityContainerElement;
						this.ProcessAndAddCapability(edmFunctionImport.Name, edmFunctionImport);
						List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> list;
						if (!this.output.UnboundFunctionOverloads.TryGetValue(edmFunctionImport.Name, out list))
						{
							list = new List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>();
							this.output.UnboundFunctionOverloads.Add(edmFunctionImport.Name, list);
						}
						list.Add(new KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>(edmFunctionImport, this.EdmTypeToTypeValueVisitor.GetFunctionImportType(edmFunctionImport)));
						break;
					}
					case Microsoft.OData.Edm.EdmContainerElementKind.Singleton:
					{
						Microsoft.OData.Edm.IEdmSingleton edmSingleton = (Microsoft.OData.Edm.IEdmSingleton)edmEntityContainerElement;
						this.ProcessAndAddCapability(edmSingleton.Name, edmSingleton);
						this.EdmTypeToTypeValueVisitor.VisitType(edmSingleton.Type);
						break;
					}
					}
				}
			}
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000C1A20 File Offset: 0x000BFC20
		protected override void ProcessTypes()
		{
			if (this.model != null)
			{
				if (this.model.EntityContainer != null)
				{
					Microsoft.OData.Edm.IEdmEntityContainer entityContainer = this.model.EntityContainer;
					this.output.TypeMetaValue = AnnotationProcessor.ProcessEntityContainer(this.model, entityContainer, this.output.Annotations, this.userSettings);
				}
				foreach (Microsoft.OData.Edm.IEdmSchemaElement edmSchemaElement in this.model.SchemaElements)
				{
					if (edmSchemaElement.SchemaElementKind == Microsoft.OData.Edm.EdmSchemaElementKind.TypeDefinition)
					{
						this.EdmTypeToTypeValueVisitor.VisitType((Microsoft.OData.Edm.IEdmType)edmSchemaElement);
					}
				}
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000C1AD4 File Offset: 0x000BFCD4
		private TypeValue AddDisplayAnnotationsMetadata(RecordValue annotations, TypeValue type)
		{
			if (annotations.Keys.Length > 0)
			{
				return BinaryOperator.AddMeta.Invoke(type, annotations).AsType;
			}
			return type;
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000C1AF8 File Offset: 0x000BFCF8
		private Capabilities ProcessAndAddCapability(string key, Microsoft.OData.Edm.Vocabularies.IEdmVocabularyAnnotatable annotatable)
		{
			Capabilities capabilities = AnnotationProcessor.ProcessCapabilities(key, this.model, annotatable, this.output.Annotations, this.userSettings);
			this.output.Annotations.TryAddCapability(annotatable, capabilities);
			return capabilities;
		}

		// Token: 0x04001F0B RID: 7947
		private readonly Microsoft.OData.Edm.IEdmModel model;

		// Token: 0x04001F0C RID: 7948
		private readonly IResource resource;

		// Token: 0x04001F0D RID: 7949
		private readonly Uri serviceUri;

		// Token: 0x04001F0E RID: 7950
		private readonly ODataUserSettings userSettings;

		// Token: 0x04001F0F RID: 7951
		private EdmTypeToTypeValueVisitor edmTypeToTypeValueVisitor;
	}
}
