using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000021 RID: 33
	public class ODataSwaggerConverter
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000057E5 File Offset: 0x000039E5
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000057ED File Offset: 0x000039ED
		public Uri MetadataUri { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000057F6 File Offset: 0x000039F6
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000057FE File Offset: 0x000039FE
		public string Host { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005807 File Offset: 0x00003A07
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000580F File Offset: 0x00003A0F
		public string BasePath { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005818 File Offset: 0x00003A18
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005820 File Offset: 0x00003A20
		public IEdmModel EdmModel { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005829 File Offset: 0x00003A29
		public virtual Version SwaggerVersion
		{
			get
			{
				return new Version(2, 0);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005832 File Offset: 0x00003A32
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000583A File Offset: 0x00003A3A
		protected virtual JObject SwaggerDocument { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005843 File Offset: 0x00003A43
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000584B File Offset: 0x00003A4B
		protected virtual JObject SwaggerPaths { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005854 File Offset: 0x00003A54
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000585C File Offset: 0x00003A5C
		protected virtual JObject SwaggerTypeDefinitions { get; set; }

		// Token: 0x060000EB RID: 235 RVA: 0x00005865 File Offset: 0x00003A65
		public ODataSwaggerConverter(IEdmModel model)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			this.EdmModel = model;
			this.MetadataUri = ODataSwaggerConverter.DefaultMetadataUri;
			this.Host = "default";
			this.BasePath = "/odata";
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000058A3 File Offset: 0x00003AA3
		public virtual JObject GetSwaggerModel()
		{
			if (this.SwaggerDocument != null)
			{
				return this.SwaggerDocument;
			}
			this.InitializeStart();
			this.InitializeDocument();
			this.InitializeContainer();
			this.InitializeTypeDefinitions();
			this.InitializeOperations();
			this.InitializeEnd();
			return this.SwaggerDocument;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000058DE File Offset: 0x00003ADE
		protected virtual void InitializeStart()
		{
			this.SwaggerDocument = null;
			this.SwaggerPaths = null;
			this.SwaggerTypeDefinitions = null;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000058F8 File Offset: 0x00003AF8
		protected virtual void InitializeDocument()
		{
			JObject jobject = new JObject();
			jobject.Add("swagger", this.SwaggerVersion.ToString());
			string text = "info";
			JObject jobject2 = new JObject();
			jobject2.Add("title", "OData Service");
			string text2 = "description";
			string text3 = "The OData Service at ";
			Uri metadataUri = this.MetadataUri;
			jobject2.Add(text2, text3 + ((metadataUri != null) ? metadataUri.ToString() : null));
			jobject2.Add("version", "0.1.0");
			jobject2.Add("x-odata-version", "4.0");
			jobject.Add(text, jobject2);
			jobject.Add("host", this.Host);
			jobject.Add("schemes", new JArray("http"));
			jobject.Add("basePath", this.BasePath);
			jobject.Add("consumes", new JArray("application/json"));
			jobject.Add("produces", new JArray("application/json"));
			this.SwaggerDocument = jobject;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005A14 File Offset: 0x00003C14
		protected virtual void InitializeContainer()
		{
			this.SwaggerPaths = new JObject();
			this.SwaggerDocument.Add("paths", this.SwaggerPaths);
			if (this.EdmModel.EntityContainer == null)
			{
				return;
			}
			foreach (IEdmEntitySet edmEntitySet in this.EdmModel.EntityContainer.EntitySets())
			{
				this.SwaggerPaths.Add("/" + edmEntitySet.Name, ODataSwaggerUtilities.CreateSwaggerPathForEntitySet(edmEntitySet));
				this.SwaggerPaths.Add(ODataSwaggerUtilities.GetPathForEntity(edmEntitySet), ODataSwaggerUtilities.CreateSwaggerPathForEntity(edmEntitySet));
			}
			foreach (IEdmOperationImport edmOperationImport in this.EdmModel.EntityContainer.OperationImports())
			{
				this.SwaggerPaths.Add(ODataSwaggerUtilities.GetPathForOperationImport(edmOperationImport), ODataSwaggerUtilities.CreateSwaggerPathForOperationImport(edmOperationImport));
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005B24 File Offset: 0x00003D24
		protected virtual void InitializeTypeDefinitions()
		{
			this.SwaggerTypeDefinitions = new JObject();
			this.SwaggerDocument.Add("definitions", this.SwaggerTypeDefinitions);
			foreach (IEdmStructuredType edmStructuredType in this.EdmModel.SchemaElements.OfType<IEdmStructuredType>())
			{
				this.SwaggerTypeDefinitions.Add(edmStructuredType.FullTypeName(), ODataSwaggerUtilities.CreateSwaggerTypeDefinitionForStructuredType(edmStructuredType));
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005BAC File Offset: 0x00003DAC
		protected virtual void InitializeOperations()
		{
			if (this.EdmModel.EntityContainer == null)
			{
				return;
			}
			foreach (IEdmOperation edmOperation in this.EdmModel.SchemaElements.OfType<IEdmOperation>())
			{
				if (edmOperation.IsBound)
				{
					IEdmType definition = edmOperation.Parameters.First<IEdmOperationParameter>().Type.Definition;
					if (definition.TypeKind == EdmTypeKind.Entity)
					{
						IEdmEntityType entityType2 = (IEdmEntityType)definition;
						IEnumerable<IEdmEntitySet> enumerable = this.EdmModel.EntityContainer.EntitySets();
						Func<IEdmEntitySet, bool> func;
						Func<IEdmEntitySet, bool> <>9__0;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (IEdmEntitySet es) => es.EntityType().Equals(entityType2));
						}
						using (IEnumerator<IEdmEntitySet> enumerator2 = enumerable.Where(func).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								IEdmEntitySet edmEntitySet = enumerator2.Current;
								this.SwaggerPaths.Add(ODataSwaggerUtilities.GetPathForOperationOfEntity(edmOperation, edmEntitySet), ODataSwaggerUtilities.CreateSwaggerPathForOperationOfEntity(edmOperation, edmEntitySet));
							}
							continue;
						}
					}
					if (definition.TypeKind == EdmTypeKind.Collection)
					{
						IEdmCollectionType edmCollectionType = definition as IEdmCollectionType;
						if (edmCollectionType != null && edmCollectionType.ElementType.Definition.TypeKind == EdmTypeKind.Entity)
						{
							IEdmEntityType entityType = (IEdmEntityType)edmCollectionType.ElementType.Definition;
							IEnumerable<IEdmEntitySet> enumerable2 = this.EdmModel.EntityContainer.EntitySets();
							Func<IEdmEntitySet, bool> func2;
							Func<IEdmEntitySet, bool> <>9__1;
							if ((func2 = <>9__1) == null)
							{
								func2 = (<>9__1 = (IEdmEntitySet es) => es.EntityType().Equals(entityType));
							}
							foreach (IEdmEntitySet edmEntitySet2 in enumerable2.Where(func2))
							{
								this.SwaggerPaths.Add(ODataSwaggerUtilities.GetPathForOperationOfEntitySet(edmOperation, edmEntitySet2), ODataSwaggerUtilities.CreateSwaggerPathForOperationOfEntitySet(edmOperation, edmEntitySet2));
							}
						}
					}
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005DD8 File Offset: 0x00003FD8
		protected virtual void InitializeEnd()
		{
			JObject swaggerTypeDefinitions = this.SwaggerTypeDefinitions;
			string text = "_Error";
			JObject jobject = new JObject();
			string text2 = "properties";
			JObject jobject2 = new JObject();
			string text3 = "error";
			JObject jobject3 = new JObject();
			jobject3.Add("$ref", "#/definitions/_InError");
			jobject2.Add(text3, jobject3);
			jobject.Add(text2, jobject2);
			swaggerTypeDefinitions.Add(text, jobject);
			JObject swaggerTypeDefinitions2 = this.SwaggerTypeDefinitions;
			string text4 = "_InError";
			JObject jobject4 = new JObject();
			string text5 = "properties";
			JObject jobject5 = new JObject();
			string text6 = "code";
			JObject jobject6 = new JObject();
			jobject6.Add("type", "string");
			jobject5.Add(text6, jobject6);
			string text7 = "message";
			JObject jobject7 = new JObject();
			jobject7.Add("type", "string");
			jobject5.Add(text7, jobject7);
			jobject4.Add(text5, jobject5);
			swaggerTypeDefinitions2.Add(text4, jobject4);
		}

		// Token: 0x0400002C RID: 44
		private static readonly Uri DefaultMetadataUri = new Uri("http://localhost");

		// Token: 0x0400002D RID: 45
		private const string DefaultHost = "default";

		// Token: 0x0400002E RID: 46
		private const string DefaultbasePath = "/odata";
	}
}
