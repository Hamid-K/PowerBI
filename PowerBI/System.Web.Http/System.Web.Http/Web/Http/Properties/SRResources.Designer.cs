using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Properties
{
	// Token: 0x020000BD RID: 189
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class SRResources
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00003AA7 File Offset: 0x00001CA7
		internal SRResources()
		{
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000CD22 File Offset: 0x0000AF22
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SRResources.resourceMan == null)
				{
					SRResources.resourceMan = new ResourceManager("System.Web.Http.Properties.SRResources", typeof(SRResources).Assembly);
				}
				return SRResources.resourceMan;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000CD4E File Offset: 0x0000AF4E
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000CD55 File Offset: 0x0000AF55
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SRResources.resourceCulture;
			}
			set
			{
				SRResources.resourceCulture = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000CD5D File Offset: 0x0000AF5D
		internal static string ActionExecutor_UnexpectedTaskInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutor_UnexpectedTaskInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000CD73 File Offset: 0x0000AF73
		internal static string ActionExecutor_WrappedTaskInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionExecutor_WrappedTaskInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000CD89 File Offset: 0x0000AF89
		internal static string ActionFilterAttribute_MustSupplyResponseOrException
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionFilterAttribute_MustSupplyResponseOrException", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000CD9F File Offset: 0x0000AF9F
		internal static string ActionSelector_AmbiguousMatchType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ActionSelector_AmbiguousMatchType", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000CDB5 File Offset: 0x0000AFB5
		internal static string ApiController_RequestMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiController_RequestMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000CDCB File Offset: 0x0000AFCB
		internal static string ApiControllerActionInvoker_InvalidHttpActionResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionInvoker_InvalidHttpActionResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		internal static string ApiControllerActionInvoker_NullHttpActionResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionInvoker_NullHttpActionResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000CDF7 File Offset: 0x0000AFF7
		internal static string ApiControllerActionSelector_ActionNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_ActionNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000CE0D File Offset: 0x0000B00D
		internal static string ApiControllerActionSelector_ActionNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_ActionNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000CE23 File Offset: 0x0000B023
		internal static string ApiControllerActionSelector_AmbiguousMatch
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_AmbiguousMatch", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000CE39 File Offset: 0x0000B039
		internal static string ApiControllerActionSelector_HttpMethodNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("ApiControllerActionSelector_HttpMethodNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000CE4F File Offset: 0x0000B04F
		internal static string AttributeRoutes_InvalidPrefix
		{
			get
			{
				return SRResources.ResourceManager.GetString("AttributeRoutes_InvalidPrefix", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000CE65 File Offset: 0x0000B065
		internal static string AttributeRoutes_InvalidTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("AttributeRoutes_InvalidTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000CE7B File Offset: 0x0000B07B
		internal static string AuthenticationFilterDidNothing
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterDidNothing", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000CE91 File Offset: 0x0000B091
		internal static string AuthenticationFilterErrorResult
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterErrorResult", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000CEA7 File Offset: 0x0000B0A7
		internal static string AuthenticationFilterSetPrincipalToKnownIdentity
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterSetPrincipalToKnownIdentity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000CEBD File Offset: 0x0000B0BD
		internal static string AuthenticationFilterSetPrincipalToUnknownIdentity
		{
			get
			{
				return SRResources.ResourceManager.GetString("AuthenticationFilterSetPrincipalToUnknownIdentity", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000CED3 File Offset: 0x0000B0D3
		internal static string BadRequest
		{
			get
			{
				return SRResources.ResourceManager.GetString("BadRequest", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000CEE9 File Offset: 0x0000B0E9
		internal static string BatchContentTypeMissing
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchContentTypeMissing", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000CEFF File Offset: 0x0000B0FF
		internal static string BatchMediaTypeNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchMediaTypeNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000CF15 File Offset: 0x0000B115
		internal static string BatchRequestMissingContent
		{
			get
			{
				return SRResources.ResourceManager.GetString("BatchRequestMissingContent", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000CF2B File Offset: 0x0000B12B
		internal static string CannotSupportSingletonInstance
		{
			get
			{
				return SRResources.ResourceManager.GetString("CannotSupportSingletonInstance", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000CF41 File Offset: 0x0000B141
		internal static string CollectionParameterContainsNullElement
		{
			get
			{
				return SRResources.ResourceManager.GetString("CollectionParameterContainsNullElement", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000CF57 File Offset: 0x0000B157
		internal static string Common_PropertyNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("Common_PropertyNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000CF6D File Offset: 0x0000B16D
		internal static string Common_TypeMustDriveFromType
		{
			get
			{
				return SRResources.ResourceManager.GetString("Common_TypeMustDriveFromType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000CF83 File Offset: 0x0000B183
		internal static string ControllerNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ControllerNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000CF99 File Offset: 0x0000B199
		internal static string DataAnnotationsModelValidatorProvider_ConstructorRequirements
		{
			get
			{
				return SRResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ConstructorRequirements", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000CFAF File Offset: 0x0000B1AF
		internal static string DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements
		{
			get
			{
				return SRResources.ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000CFC5 File Offset: 0x0000B1C5
		internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		internal static string DefaultControllerFactory_ControllerNameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ControllerNameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000CFF1 File Offset: 0x0000B1F1
		internal static string DefaultControllerFactory_ErrorCreatingController
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultControllerFactory_ErrorCreatingController", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000D007 File Offset: 0x0000B207
		internal static string DefaultInlineConstraintResolver_AmbiguousCtors
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_AmbiguousCtors", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000D01D File Offset: 0x0000B21D
		internal static string DefaultInlineConstraintResolver_CouldNotFindCtor
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_CouldNotFindCtor", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000D033 File Offset: 0x0000B233
		internal static string DefaultInlineConstraintResolver_TypeNotConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultInlineConstraintResolver_TypeNotConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000D049 File Offset: 0x0000B249
		internal static string DefaultServices_InvalidServiceType
		{
			get
			{
				return SRResources.ResourceManager.GetString("DefaultServices_InvalidServiceType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000D05F File Offset: 0x0000B25F
		internal static string DependencyResolver_BeginScopeReturnsNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("DependencyResolver_BeginScopeReturnsNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000D075 File Offset: 0x0000B275
		internal static string DependencyResolverNoService
		{
			get
			{
				return SRResources.ResourceManager.GetString("DependencyResolverNoService", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000D08B File Offset: 0x0000B28B
		internal static string DirectRoute_AmbiguousController
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_AmbiguousController", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		internal static string DirectRoute_HandlerNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_HandlerNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		internal static string DirectRoute_InvalidParameter_Action
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Action", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000D0CD File Offset: 0x0000B2CD
		internal static string DirectRoute_InvalidParameter_Controller
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_InvalidParameter_Controller", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000D0E3 File Offset: 0x0000B2E3
		internal static string DirectRoute_MissingActionDescriptors
		{
			get
			{
				return SRResources.ResourceManager.GetString("DirectRoute_MissingActionDescriptors", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000D0F9 File Offset: 0x0000B2F9
		internal static string ErrorOccurred
		{
			get
			{
				return SRResources.ResourceManager.GetString("ErrorOccurred", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000D10F File Offset: 0x0000B30F
		internal static string HttpActionDescriptor_NoConverterForGenericParamterTypeExists
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpActionDescriptor_NoConverterForGenericParamterTypeExists", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000D125 File Offset: 0x0000B325
		internal static string HttpControllerContext_ConfigurationMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpControllerContext_ConfigurationMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000D13B File Offset: 0x0000B33B
		internal static string HttpRequestMessageExtensions_NoConfiguration
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoConfiguration", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000D151 File Offset: 0x0000B351
		internal static string HttpRequestMessageExtensions_NoContentNegotiator
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoContentNegotiator", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000D167 File Offset: 0x0000B367
		internal static string HttpRequestMessageExtensions_NoMatchingFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRequestMessageExtensions_NoMatchingFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000D17D File Offset: 0x0000B37D
		internal static string HttpResponseExceptionMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpResponseExceptionMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000D193 File Offset: 0x0000B393
		internal static string HttpRouteBuilder_CouldNotResolveConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpRouteBuilder_CouldNotResolveConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000D1A9 File Offset: 0x0000B3A9
		internal static string HttpServerDisposed
		{
			get
			{
				return SRResources.ResourceManager.GetString("HttpServerDisposed", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000D1BF File Offset: 0x0000B3BF
		internal static string JQuerySyntaxMissingClosingBracket
		{
			get
			{
				return SRResources.ResourceManager.GetString("JQuerySyntaxMissingClosingBracket", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		internal static string MaxHttpCollectionKeyLimitReached
		{
			get
			{
				return SRResources.ResourceManager.GetString("MaxHttpCollectionKeyLimitReached", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000D1EB File Offset: 0x0000B3EB
		internal static string MissingDataMemberIsRequired
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingDataMemberIsRequired", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000D201 File Offset: 0x0000B401
		internal static string MissingRequiredMember
		{
			get
			{
				return SRResources.ResourceManager.GetString("MissingRequiredMember", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000D217 File Offset: 0x0000B417
		internal static string ModelBinderConfig_ValueInvalid
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderConfig_ValueInvalid", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000D22D File Offset: 0x0000B42D
		internal static string ModelBinderConfig_ValueRequired
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderConfig_ValueRequired", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000D243 File Offset: 0x0000B443
		internal static string ModelBinderProviderCollection_InvalidBinderType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderProviderCollection_InvalidBinderType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000D259 File Offset: 0x0000B459
		internal static string ModelBinderUtil_ModelCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000D26F File Offset: 0x0000B46F
		internal static string ModelBinderUtil_ModelInstanceIsWrong
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelInstanceIsWrong", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000D285 File Offset: 0x0000B485
		internal static string ModelBinderUtil_ModelMetadataCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelMetadataCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000D29B File Offset: 0x0000B49B
		internal static string ModelBinderUtil_ModelTypeIsWrong
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBinderUtil_ModelTypeIsWrong", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000D2B1 File Offset: 0x0000B4B1
		internal static string ModelBindingContext_ModelMetadataMustBeSet
		{
			get
			{
				return SRResources.ResourceManager.GetString("ModelBindingContext_ModelMetadataMustBeSet", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		internal static string NoControllerCreated
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoControllerCreated", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000D2DD File Offset: 0x0000B4DD
		internal static string NoControllerSelected
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoControllerSelected", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		internal static string NoRouteData
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoRouteData", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000D309 File Offset: 0x0000B509
		internal static string Object_NotYetInitialized
		{
			get
			{
				return SRResources.ResourceManager.GetString("Object_NotYetInitialized", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000D31F File Offset: 0x0000B51F
		internal static string OptionalBodyParameterNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("OptionalBodyParameterNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000D335 File Offset: 0x0000B535
		internal static string ParameterBindingCantHaveMultipleBodyParameters
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingCantHaveMultipleBodyParameters", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000D34B File Offset: 0x0000B54B
		internal static string ParameterBindingConflictingAttributes
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingConflictingAttributes", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000D361 File Offset: 0x0000B561
		internal static string ParameterBindingIllegalType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ParameterBindingIllegalType", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000D377 File Offset: 0x0000B577
		internal static string ReflectedActionDescriptor_ParameterCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000D38D File Offset: 0x0000B58D
		internal static string ReflectedActionDescriptor_ParameterNotInDictionary
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterNotInDictionary", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000D3A3 File Offset: 0x0000B5A3
		internal static string ReflectedActionDescriptor_ParameterValueHasWrongType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedActionDescriptor_ParameterValueHasWrongType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000D3B9 File Offset: 0x0000B5B9
		internal static string ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods
		{
			get
			{
				return SRResources.ResourceManager.GetString("ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		internal static string Request_RequestContextMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("Request_RequestContextMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000D3E5 File Offset: 0x0000B5E5
		internal static string RequestContextConflict
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestContextConflict", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000D3FB File Offset: 0x0000B5FB
		internal static string RequestIsNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestIsNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000D411 File Offset: 0x0000B611
		internal static string RequestNotAuthorized
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestNotAuthorized", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000D427 File Offset: 0x0000B627
		internal static string ResourceNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("ResourceNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000D43D File Offset: 0x0000B63D
		internal static string ResponseMessageResultConverter_NullHttpResponseMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("ResponseMessageResultConverter_NullHttpResponseMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000D453 File Offset: 0x0000B653
		internal static string Route_AddRemoveWithNoKeyNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_AddRemoveWithNoKeyNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000D469 File Offset: 0x0000B669
		internal static string Route_CannotHaveCatchAllInMultiSegment
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveCatchAllInMultiSegment", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000D47F File Offset: 0x0000B67F
		internal static string Route_CannotHaveConsecutiveParameters
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveConsecutiveParameters", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000D495 File Offset: 0x0000B695
		internal static string Route_CannotHaveConsecutiveSeparators
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CannotHaveConsecutiveSeparators", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000D4AB File Offset: 0x0000B6AB
		internal static string Route_CatchAllMustBeLast
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_CatchAllMustBeLast", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000D4C1 File Offset: 0x0000B6C1
		internal static string Route_InvalidParameterName
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_InvalidParameterName", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000D4D7 File Offset: 0x0000B6D7
		internal static string Route_InvalidRouteTemplate
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_InvalidRouteTemplate", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000D4ED File Offset: 0x0000B6ED
		internal static string Route_MismatchedParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_MismatchedParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000D503 File Offset: 0x0000B703
		internal static string Route_RepeatedParameter
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_RepeatedParameter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000D519 File Offset: 0x0000B719
		internal static string Route_ValidationMustBeStringOrCustomConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_ValidationMustBeStringOrCustomConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000D52F File Offset: 0x0000B72F
		internal static string RouteCollection_NameNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("RouteCollection_NameNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000D545 File Offset: 0x0000B745
		internal static string RoutePrefix_CannotSupportMultiRoutePrefix
		{
			get
			{
				return SRResources.ResourceManager.GetString("RoutePrefix_CannotSupportMultiRoutePrefix", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000D55B File Offset: 0x0000B75B
		internal static string RoutePrefix_PrefixCannotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("RoutePrefix_PrefixCannotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000D571 File Offset: 0x0000B771
		internal static string SubRouteCollection_DuplicateRouteName
		{
			get
			{
				return SRResources.ResourceManager.GetString("SubRouteCollection_DuplicateRouteName", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000D587 File Offset: 0x0000B787
		internal static string TraceActionFilterMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionFilterMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000D59D File Offset: 0x0000B79D
		internal static string TraceActionInvokeMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionInvokeMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
		internal static string TraceActionReturnValue
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionReturnValue", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
		internal static string TraceActionSelectedMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceActionSelectedMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000D5DF File Offset: 0x0000B7DF
		internal static string TraceBeginParameterBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceBeginParameterBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000D5F5 File Offset: 0x0000B7F5
		internal static string TraceCancelledMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceCancelledMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000D60B File Offset: 0x0000B80B
		internal static string TraceEndParameterBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceEndParameterBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000D621 File Offset: 0x0000B821
		internal static string TraceEndParameterBindNoBind
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceEndParameterBindNoBind", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000D637 File Offset: 0x0000B837
		internal static string TraceGetPerRequestFormatterEndMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterEndMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000D64D File Offset: 0x0000B84D
		internal static string TraceGetPerRequestFormatterEndMessageNew
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterEndMessageNew", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000D663 File Offset: 0x0000B863
		internal static string TraceGetPerRequestFormatterMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestFormatterMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000D679 File Offset: 0x0000B879
		internal static string TraceGetPerRequestNullFormatterEndMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceGetPerRequestNullFormatterEndMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000D68F File Offset: 0x0000B88F
		internal static string TraceHttpControllerTypeResolverError
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceHttpControllerTypeResolverError", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000D6A5 File Offset: 0x0000B8A5
		internal static string TraceInvokingAction
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceInvokingAction", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000D6BB File Offset: 0x0000B8BB
		internal static string TraceModelStateErrorMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceModelStateErrorMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
		internal static string TraceModelStateInvalidMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceModelStateInvalidMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		internal static string TraceNegotiateFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceNegotiateFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		internal static string TraceNoneObjectMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceNoneObjectMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000D713 File Offset: 0x0000B913
		internal static string TraceReadFromStreamMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceReadFromStreamMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000D729 File Offset: 0x0000B929
		internal static string TraceReadFromStreamValueMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceReadFromStreamValueMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000D73F File Offset: 0x0000B93F
		internal static string TraceRequestCompleteMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceRequestCompleteMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000D755 File Offset: 0x0000B955
		internal static string TraceRouteMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceRouteMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000D76B File Offset: 0x0000B96B
		internal static string TraceSelectedFormatter
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceSelectedFormatter", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000D781 File Offset: 0x0000B981
		internal static string TraceUnknownMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceUnknownMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000D797 File Offset: 0x0000B997
		internal static string TraceValidModelState
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceValidModelState", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		internal static string TraceWriteToStreamMessage
		{
			get
			{
				return SRResources.ResourceManager.GetString("TraceWriteToStreamMessage", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
		internal static string TypeInstanceMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeInstanceMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000D7D9 File Offset: 0x0000B9D9
		internal static string TypeMethodMustNotReturnNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeMethodMustNotReturnNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		internal static string TypePropertyMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypePropertyMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000D805 File Offset: 0x0000BA05
		internal static string UnsupportedMediaType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedMediaType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000D81B File Offset: 0x0000BA1B
		internal static string UnsupportedMediaTypeNoContentType
		{
			get
			{
				return SRResources.ResourceManager.GetString("UnsupportedMediaTypeNoContentType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000D831 File Offset: 0x0000BA31
		internal static string UrlHelper_LinkMustNotReturnNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("UrlHelper_LinkMustNotReturnNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000D847 File Offset: 0x0000BA47
		internal static string ValidatableObjectAdapter_IncompatibleType
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidatableObjectAdapter_IncompatibleType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000D85D File Offset: 0x0000BA5D
		internal static string Validation_ValueNotFound
		{
			get
			{
				return SRResources.ResourceManager.GetString("Validation_ValueNotFound", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000D873 File Offset: 0x0000BA73
		internal static string ValidationAttributeOnField
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidationAttributeOnField", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000D889 File Offset: 0x0000BA89
		internal static string ValidationAttributeOnNonPublicProperty
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidationAttributeOnNonPublicProperty", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000D89F File Offset: 0x0000BA9F
		internal static string ValidModelState
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValidModelState", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000D8B5 File Offset: 0x0000BAB5
		internal static string ValueProviderFactory_Cannot_Create
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderFactory_Cannot_Create", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000D8CB File Offset: 0x0000BACB
		internal static string ValueProviderResult_ConversionThrew
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderResult_ConversionThrew", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000D8E1 File Offset: 0x0000BAE1
		internal static string ValueProviderResult_NoConverterExists
		{
			get
			{
				return SRResources.ResourceManager.GetString("ValueProviderResult_NoConverterExists", SRResources.resourceCulture);
			}
		}

		// Token: 0x04000129 RID: 297
		private static ResourceManager resourceMan;

		// Token: 0x0400012A RID: 298
		private static CultureInfo resourceCulture;
	}
}
