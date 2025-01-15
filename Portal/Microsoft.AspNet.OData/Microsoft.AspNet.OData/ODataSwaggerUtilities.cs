using System;
using System.Linq;
using Microsoft.OData.Edm;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000020 RID: 32
	internal static class ODataSwaggerUtilities
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000044A0 File Offset: 0x000026A0
		public static JObject CreateSwaggerPathForEntitySet(IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (edmEntitySet == null)
			{
				return new JObject();
			}
			JObject jobject = new JObject();
			jobject.Add("get", new JObject().Summary("Get EntitySet " + edmEntitySet.Name).OperationId(edmEntitySet.Name + "_Get").Description("Returns the EntitySet " + edmEntitySet.Name)
				.Tags(new string[] { edmEntitySet.Name })
				.Parameters(new JArray().Parameter("$expand", "query", "Expand navigation property", "string", null).Parameter("$select", "query", "select structural property", "string", null).Parameter("$orderby", "query", "order by some property", "string", null)
					.Parameter("$top", "query", "top elements", "integer", null)
					.Parameter("$skip", "query", "skip elements", "integer", null)
					.Parameter("$count", "query", "include count in response", "boolean", null))
				.Responses(new JObject().Response("200", "EntitySet " + edmEntitySet.Name, edmEntitySet.EntityType()).DefaultErrorResponse()));
			jobject.Add("post", new JObject().Summary("Post a new entity to EntitySet " + edmEntitySet.Name).OperationId(edmEntitySet.Name + "_Post").Description("Post a new entity to EntitySet " + edmEntitySet.Name)
				.Tags(new string[] { edmEntitySet.Name })
				.Parameters(new JArray().Parameter(edmEntitySet.EntityType().Name, "body", "The entity to post", edmEntitySet.EntityType()))
				.Responses(new JObject().Response("200", "EntitySet " + edmEntitySet.Name, edmEntitySet.EntityType()).DefaultErrorResponse()));
			return jobject;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000046B8 File Offset: 0x000028B8
		public static JObject CreateSwaggerPathForEntity(IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (edmEntitySet == null)
			{
				return new JObject();
			}
			JArray jarray = new JArray();
			foreach (IEdmStructuralProperty edmStructuralProperty in edmEntitySet.EntityType().Key())
			{
				string text;
				string primitiveTypeAndFormat = ODataSwaggerUtilities.GetPrimitiveTypeAndFormat(edmStructuralProperty.Type.Definition as IEdmPrimitiveType, out text);
				jarray.Parameter(edmStructuralProperty.Name, "path", "key: " + edmStructuralProperty.Name, primitiveTypeAndFormat, text);
			}
			JObject jobject = new JObject();
			jobject.Add("get", new JObject().Summary("Get entity from " + edmEntitySet.Name + " by key.").OperationId(edmEntitySet.Name + "_GetById").Description("Returns the entity with the key from " + edmEntitySet.Name)
				.Tags(new string[] { edmEntitySet.Name })
				.Parameters((jarray.DeepClone() as JArray).Parameter("$select", "query", "description", "string", null))
				.Responses(new JObject().Response("200", "EntitySet " + edmEntitySet.Name, edmEntitySet.EntityType()).DefaultErrorResponse()));
			jobject.Add("patch", new JObject().Summary("Update entity in EntitySet " + edmEntitySet.Name).OperationId(edmEntitySet.Name + "_PatchById").Description("Update entity in EntitySet " + edmEntitySet.Name)
				.Tags(new string[] { edmEntitySet.Name })
				.Parameters((jarray.DeepClone() as JArray).Parameter(edmEntitySet.EntityType().Name, "body", "The entity to patch", edmEntitySet.EntityType()))
				.Responses(new JObject().Response("204", "Empty response").DefaultErrorResponse()));
			jobject.Add("delete", new JObject().Summary("Delete entity in EntitySet " + edmEntitySet.Name).OperationId(edmEntitySet.Name + "_DeleteById").Description("Delete entity in EntitySet " + edmEntitySet.Name)
				.Tags(new string[] { edmEntitySet.Name })
				.Parameters((jarray.DeepClone() as JArray).Parameter("If-Match", "header", "If-Match header", "string", null))
				.Responses(new JObject().Response("204", "Empty response").DefaultErrorResponse()));
			return jobject;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004984 File Offset: 0x00002B84
		public static JObject CreateSwaggerPathForOperationImport(IEdmOperationImport operationImport)
		{
			if (operationImport == null)
			{
				return new JObject();
			}
			bool flag = operationImport is IEdmFunctionImport;
			JArray jarray = new JArray();
			foreach (IEdmOperationParameter edmOperationParameter in operationImport.Operation.Parameters)
			{
				jarray.Parameter(edmOperationParameter.Name, flag ? "path" : "body", "parameter: " + edmOperationParameter.Name, edmOperationParameter.Type.Definition);
			}
			JObject jobject = new JObject();
			if (operationImport.Operation.ReturnType == null)
			{
				jobject.Response("204", "Empty response");
			}
			else
			{
				jobject.Response("200", "Response from " + operationImport.Name, operationImport.Operation.ReturnType.Definition);
			}
			JObject jobject2 = new JObject().Summary("Call operation import  " + operationImport.Name).OperationId(operationImport.Name + (flag ? "_FunctionImportGet" : "_ActionImportPost")).Description("Call operation import  " + operationImport.Name)
				.Tags(new string[] { flag ? "Function Import" : "Action Import" });
			if (jarray.Count > 0)
			{
				jobject2.Parameters(jarray);
			}
			jobject2.Responses(jobject.DefaultErrorResponse());
			JObject jobject3 = new JObject();
			jobject3.Add(flag ? "get" : "post", jobject2);
			return jobject3;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004B20 File Offset: 0x00002D20
		public static JObject CreateSwaggerPathForOperationOfEntitySet(IEdmOperation operation, IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (operation == null || edmEntitySet == null)
			{
				return new JObject();
			}
			bool flag = operation is IEdmFunction;
			JArray jarray = new JArray();
			foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters.Skip(1))
			{
				jarray.Parameter(edmOperationParameter.Name, flag ? "path" : "body", "parameter: " + edmOperationParameter.Name, edmOperationParameter.Type.Definition);
			}
			JObject jobject = new JObject();
			if (operation.ReturnType == null)
			{
				jobject.Response("204", "Empty response");
			}
			else
			{
				jobject.Response("200", "Response from " + operation.Name, operation.ReturnType.Definition);
			}
			JObject jobject2 = new JObject().Summary("Call operation  " + operation.Name).OperationId(operation.Name + (flag ? "_FunctionGet" : "_ActionPost")).Description("Call operation  " + operation.Name)
				.Tags(new string[]
				{
					edmEntitySet.Name,
					flag ? "Function" : "Action"
				});
			if (jarray.Count > 0)
			{
				jobject2.Parameters(jarray);
			}
			jobject2.Responses(jobject.DefaultErrorResponse());
			JObject jobject3 = new JObject();
			jobject3.Add(flag ? "get" : "post", jobject2);
			return jobject3;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004CCC File Offset: 0x00002ECC
		public static JObject CreateSwaggerPathForOperationOfEntity(IEdmOperation operation, IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (operation == null || edmEntitySet == null)
			{
				return new JObject();
			}
			bool flag = operation is IEdmFunction;
			JArray jarray = new JArray();
			foreach (IEdmStructuralProperty edmStructuralProperty in edmEntitySet.EntityType().Key())
			{
				string text;
				string primitiveTypeAndFormat = ODataSwaggerUtilities.GetPrimitiveTypeAndFormat(edmStructuralProperty.Type.Definition as IEdmPrimitiveType, out text);
				jarray.Parameter(edmStructuralProperty.Name, "path", "key: " + edmStructuralProperty.Name, primitiveTypeAndFormat, text);
			}
			foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters.Skip(1))
			{
				jarray.Parameter(edmOperationParameter.Name, flag ? "path" : "body", "parameter: " + edmOperationParameter.Name, edmOperationParameter.Type.Definition);
			}
			JObject jobject = new JObject();
			if (operation.ReturnType == null)
			{
				jobject.Response("204", "Empty response");
			}
			else
			{
				jobject.Response("200", "Response from " + operation.Name, operation.ReturnType.Definition);
			}
			JObject jobject2 = new JObject().Summary("Call operation  " + operation.Name).OperationId(operation.Name + (flag ? "_FunctionGetById" : "_ActionPostById")).Description("Call operation  " + operation.Name)
				.Tags(new string[]
				{
					edmEntitySet.Name,
					flag ? "Function" : "Action"
				});
			if (jarray.Count > 0)
			{
				jobject2.Parameters(jarray);
			}
			jobject2.Responses(jobject.DefaultErrorResponse());
			JObject jobject3 = new JObject();
			jobject3.Add(flag ? "get" : "post", jobject2);
			return jobject3;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004EF8 File Offset: 0x000030F8
		public static string GetPathForEntity(IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (edmEntitySet == null)
			{
				return string.Empty;
			}
			string text = "/" + edmEntitySet.Name + "(";
			foreach (IEdmStructuralProperty edmStructuralProperty in edmEntitySet.EntityType().Key())
			{
				if (edmStructuralProperty.Type.Definition.TypeKind == EdmTypeKind.Primitive && ((IEdmPrimitiveType)edmStructuralProperty.Type.Definition).PrimitiveKind == EdmPrimitiveTypeKind.String)
				{
					text = text + "'{" + edmStructuralProperty.Name + "}', ";
				}
				else
				{
					text = text + "{" + edmStructuralProperty.Name + "}, ";
				}
			}
			text = text.Substring(0, text.Length - 2);
			text += ")";
			return text;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004FE4 File Offset: 0x000031E4
		public static string GetPathForOperationImport(IEdmOperationImport operationImport)
		{
			if (operationImport == null)
			{
				return string.Empty;
			}
			string text = "/" + operationImport.Name + "(";
			if (operationImport.IsFunctionImport())
			{
				foreach (IEdmOperationParameter edmOperationParameter in operationImport.Operation.Parameters)
				{
					text = string.Concat(new string[] { text, edmOperationParameter.Name, "={", edmOperationParameter.Name, "}," });
				}
			}
			if (text.EndsWith(",", StringComparison.Ordinal))
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += ")";
			return text;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000050B4 File Offset: 0x000032B4
		public static string GetPathForOperationOfEntitySet(IEdmOperation operation, IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (operation == null || edmEntitySet == null)
			{
				return string.Empty;
			}
			string text = string.Concat(new string[]
			{
				"/",
				edmEntitySet.Name,
				"/",
				operation.FullName(),
				"("
			});
			if (operation.IsFunction())
			{
				foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters.Skip(1))
				{
					if (edmOperationParameter.Type.Definition.TypeKind == EdmTypeKind.Primitive && ((IEdmPrimitiveType)edmOperationParameter.Type.Definition).PrimitiveKind == EdmPrimitiveTypeKind.String)
					{
						text = string.Concat(new string[] { text, edmOperationParameter.Name, "='{", edmOperationParameter.Name, "}'," });
					}
					else
					{
						text = string.Concat(new string[] { text, edmOperationParameter.Name, "={", edmOperationParameter.Name, "}," });
					}
				}
			}
			if (text.EndsWith(",", StringComparison.Ordinal))
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += ")";
			return text;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005218 File Offset: 0x00003418
		public static string GetPathForOperationOfEntity(IEdmOperation operation, IEdmNavigationSource navigationSource)
		{
			IEdmEntitySet edmEntitySet = navigationSource as IEdmEntitySet;
			if (operation == null || edmEntitySet == null)
			{
				return string.Empty;
			}
			string text = ODataSwaggerUtilities.GetPathForEntity(edmEntitySet) + "/" + operation.FullName() + "(";
			if (operation.IsFunction())
			{
				foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters.Skip(1))
				{
					if (edmOperationParameter.Type.Definition.TypeKind == EdmTypeKind.Primitive && ((IEdmPrimitiveType)edmOperationParameter.Type.Definition).PrimitiveKind == EdmPrimitiveTypeKind.String)
					{
						text = string.Concat(new string[] { text, edmOperationParameter.Name, "='{", edmOperationParameter.Name, "}'," });
					}
					else
					{
						text = string.Concat(new string[] { text, edmOperationParameter.Name, "={", edmOperationParameter.Name, "}," });
					}
				}
			}
			if (text.EndsWith(",", StringComparison.Ordinal))
			{
				text = text.Substring(0, text.Length - 1);
			}
			text += ")";
			return text;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005364 File Offset: 0x00003564
		public static JObject CreateSwaggerTypeDefinitionForStructuredType(IEdmStructuredType edmType)
		{
			if (edmType == null)
			{
				return new JObject();
			}
			JObject jobject = new JObject();
			foreach (IEdmStructuralProperty edmStructuralProperty in edmType.StructuralProperties())
			{
				JObject jobject2 = new JObject().Description(edmStructuralProperty.Name);
				ODataSwaggerUtilities.SetSwaggerType(jobject2, edmStructuralProperty.Type.Definition);
				jobject.Add(edmStructuralProperty.Name, jobject2);
			}
			JObject jobject3 = new JObject();
			jobject3.Add("properties", jobject);
			return jobject3;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000053FC File Offset: 0x000035FC
		private static void SetSwaggerType(JObject obj, IEdmType edmType)
		{
			if (edmType.TypeKind == EdmTypeKind.Complex || edmType.TypeKind == EdmTypeKind.Entity)
			{
				obj.Add("$ref", "#/definitions/" + edmType.FullTypeName());
				return;
			}
			if (edmType.TypeKind == EdmTypeKind.Primitive)
			{
				string text;
				string primitiveTypeAndFormat = ODataSwaggerUtilities.GetPrimitiveTypeAndFormat((IEdmPrimitiveType)edmType, out text);
				obj.Add("type", primitiveTypeAndFormat);
				if (text != null)
				{
					obj.Add("format", text);
					return;
				}
			}
			else
			{
				if (edmType.TypeKind == EdmTypeKind.Enum)
				{
					obj.Add("type", "string");
					return;
				}
				if (edmType.TypeKind == EdmTypeKind.Collection)
				{
					IEdmType definition = ((IEdmCollectionType)edmType).ElementType.Definition;
					JObject jobject = new JObject();
					ODataSwaggerUtilities.SetSwaggerType(jobject, definition);
					obj.Add("type", "array");
					obj.Add("items", jobject);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000054E0 File Offset: 0x000036E0
		private static string GetPrimitiveTypeAndFormat(IEdmPrimitiveType primitiveType, out string format)
		{
			format = null;
			EdmPrimitiveTypeKind primitiveKind = primitiveType.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Boolean:
				return "boolean";
			case EdmPrimitiveTypeKind.Byte:
				format = "byte";
				return "string";
			case EdmPrimitiveTypeKind.DateTimeOffset:
				format = "date-time";
				return "string";
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.SByte:
				break;
			case EdmPrimitiveTypeKind.Double:
				format = "double";
				return "number";
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
				format = "int32";
				return "integer";
			case EdmPrimitiveTypeKind.Int64:
				format = "int64";
				return "integer";
			case EdmPrimitiveTypeKind.Single:
				format = "float";
				return "number";
			case EdmPrimitiveTypeKind.String:
				return "string";
			default:
				if (primitiveKind == EdmPrimitiveTypeKind.Date)
				{
					format = "date";
					return "string";
				}
				break;
			}
			return "string";
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000055A2 File Offset: 0x000037A2
		private static JObject Responses(this JObject obj, JObject responses)
		{
			obj.Add("responses", responses);
			return obj;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000055B4 File Offset: 0x000037B4
		private static JObject ResponseRef(this JObject responses, string name, string description, string refType)
		{
			JObject jobject = new JObject();
			jobject.Add("description", description);
			string text = "schema";
			JObject jobject2 = new JObject();
			jobject2.Add("$ref", refType);
			jobject.Add(text, jobject2);
			responses.Add(name, jobject);
			return responses;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005600 File Offset: 0x00003800
		private static JObject Response(this JObject responses, string name, string description, IEdmType type)
		{
			JObject jobject = new JObject();
			ODataSwaggerUtilities.SetSwaggerType(jobject, type);
			JObject jobject2 = new JObject();
			jobject2.Add("description", description);
			jobject2.Add("schema", jobject);
			responses.Add(name, jobject2);
			return responses;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005644 File Offset: 0x00003844
		private static JObject DefaultErrorResponse(this JObject responses)
		{
			return responses.ResponseRef("default", "Unexpected error", "#/definitions/_Error");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000565B File Offset: 0x0000385B
		private static JObject Response(this JObject responses, string name, string description)
		{
			JObject jobject = new JObject();
			jobject.Add("description", description);
			responses.Add(name, jobject);
			return responses;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000567B File Offset: 0x0000387B
		private static JObject Parameters(this JObject obj, JArray parameters)
		{
			obj.Add("parameters", parameters);
			return obj;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000568C File Offset: 0x0000388C
		private static JArray Parameter(this JArray parameters, string name, string kind, string description, string type, string format = null)
		{
			JObject jobject = new JObject();
			jobject.Add("name", name);
			jobject.Add("in", kind);
			jobject.Add("description", description);
			jobject.Add("type", type);
			JObject jobject2 = jobject;
			if (!string.IsNullOrEmpty(format))
			{
				jobject2.Add("format", format);
			}
			parameters.Add(jobject2);
			return parameters;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005708 File Offset: 0x00003908
		private static JArray Parameter(this JArray parameters, string name, string kind, string description, IEdmType type)
		{
			JObject jobject = new JObject();
			jobject.Add("name", name);
			jobject.Add("in", kind);
			jobject.Add("description", description);
			JObject jobject2 = jobject;
			if (kind != "body")
			{
				ODataSwaggerUtilities.SetSwaggerType(jobject2, type);
			}
			else
			{
				JObject jobject3 = new JObject();
				ODataSwaggerUtilities.SetSwaggerType(jobject3, type);
				jobject2.Add("schema", jobject3);
			}
			parameters.Add(jobject2);
			return parameters;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005788 File Offset: 0x00003988
		private static JObject Tags(this JObject obj, params string[] tags)
		{
			obj.Add("tags", new JArray(tags));
			return obj;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000057A9 File Offset: 0x000039A9
		private static JObject Summary(this JObject obj, string summary)
		{
			obj.Add("summary", summary);
			return obj;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000057BD File Offset: 0x000039BD
		private static JObject Description(this JObject obj, string description)
		{
			obj.Add("description", description);
			return obj;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000057D1 File Offset: 0x000039D1
		private static JObject OperationId(this JObject obj, string operationId)
		{
			obj.Add("operationId", operationId);
			return obj;
		}
	}
}
