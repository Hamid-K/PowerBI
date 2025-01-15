using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x020016FC RID: 5884
	internal abstract class HostRelinkingFunctionValue : NativeFunctionValueN
	{
		// Token: 0x06009589 RID: 38281 RVA: 0x001EEBF8 File Offset: 0x001ECDF8
		protected HostRelinkingFunctionValue(IEngineHost engineHost, ExtensionModule extensionModule, string publicFunctionName, string privateFunctionName, string handlerFunctionName, FunctionValue handlerFunction, FunctionTypeValue dataSourceType)
			: base(dataSourceType)
		{
			this.engineHost = engineHost;
			this.engine = this.engineHost.QueryService<IEngine>();
			this.extensionModule = extensionModule;
			this.publicFunctionName = publicFunctionName;
			this.privateFunctionName = privateFunctionName;
			this.handlerFunctionName = handlerFunctionName;
			this.handlerFunction = handlerFunction;
		}

		// Token: 0x0600958A RID: 38282 RVA: 0x001EEC4C File Offset: 0x001ECE4C
		public static FunctionValue New(IEngineHost engineHost, string invocationKind, ResourceKindInfo resourceKindInfo, ExtensionModule extensionModule, string publicFunctionName, string privateFunctionName, string handlerFunctionName, FunctionValue handlerFunction, FunctionTypeValue dataSourceType, Value userExperience)
		{
			if (invocationKind == "Extension.DataSource")
			{
				ExtensionResourceKindInfo info = ((IExtensionResourceKind)resourceKindInfo).Info;
				return new HostRelinkingFunctionValue.DataSourceFunctionValue(engineHost, resourceKindInfo, info, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType, userExperience);
			}
			if (invocationKind == "Extension.InvokeWithCredentials")
			{
				return new HostRelinkingFunctionValue.InvokeWithCredentialsFunctionValue(engineHost, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType);
			}
			if (!(invocationKind == "Extension.InvokeWithPermissions"))
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			return new HostRelinkingFunctionValue.InvokeWithPermissionsFunctionValue(engineHost, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType);
		}

		// Token: 0x0600958B RID: 38283 RVA: 0x001EECD7 File Offset: 0x001ECED7
		protected virtual void SetHandler(InvocationEngineHost invocationHost, FunctionValue handler)
		{
			invocationHost.SetCredentialHandler(handler);
		}

		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x0600958C RID: 38284 RVA: 0x001EECE0 File Offset: 0x001ECEE0
		public override IExpression Expression
		{
			get
			{
				return new LibraryIdentifierExpression(this.publicFunctionName);
			}
		}

		// Token: 0x0600958D RID: 38285 RVA: 0x001EECF0 File Offset: 0x001ECEF0
		protected FunctionValue RelinkFunction(ResourceKindInfo resourceKindInfo, IResource resource, ResourceCredentialCollection credentials, Value[] args, bool allowChaining)
		{
			InvocationEngineHost invocationEngineHost = InvocationEngineHost.New(this.engineHost, this.engine, resourceKindInfo, resource, credentials, allowChaining);
			return this.RelinkFunction(invocationEngineHost, args);
		}

		// Token: 0x0600958E RID: 38286 RVA: 0x001EED20 File Offset: 0x001ECF20
		private FunctionValue RelinkNonDataSourceFunction(Value[] args)
		{
			InvocationEngineHost invocationEngineHost = InvocationEngineHost.New(this.engineHost, this.engine);
			invocationEngineHost.SetPermissionHandler(HostRelinkingFunctionValue.AllowNativeQueryFunctionValue.Instance);
			return this.RelinkFunction(invocationEngineHost, args);
		}

		// Token: 0x0600958F RID: 38287 RVA: 0x001EED54 File Offset: 0x001ECF54
		private FunctionValue RelinkFunction(InvocationEngineHost invocationHost, Value[] args)
		{
			IAssembly assembly = this.extensionModule.RelinkDataSourceFunctions(invocationHost, new string[] { this.privateFunctionName, this.handlerFunctionName });
			invocationHost.SetEnvironment(assembly.Exports);
			ListValue listValue = (ListValue)this.engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsList;
			FunctionValue asFunction = this.handlerFunction;
			if (asFunction == null && listValue[1].IsFunction)
			{
				asFunction = listValue[1].AsFunction;
			}
			if (asFunction != null)
			{
				this.SetHandler(invocationHost, asFunction.Invoke(args).AsFunction);
			}
			return listValue[0].AsFunction;
		}

		// Token: 0x04004F79 RID: 20345
		protected readonly IEngineHost engineHost;

		// Token: 0x04004F7A RID: 20346
		protected readonly IEngine engine;

		// Token: 0x04004F7B RID: 20347
		protected readonly ExtensionModule extensionModule;

		// Token: 0x04004F7C RID: 20348
		protected readonly string publicFunctionName;

		// Token: 0x04004F7D RID: 20349
		protected readonly string privateFunctionName;

		// Token: 0x04004F7E RID: 20350
		protected readonly string handlerFunctionName;

		// Token: 0x04004F7F RID: 20351
		protected readonly FunctionValue handlerFunction;

		// Token: 0x020016FD RID: 5885
		private sealed class AllowNativeQueryFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>
		{
			// Token: 0x06009590 RID: 38288 RVA: 0x001EEDFA File Offset: 0x001ECFFA
			private AllowNativeQueryFunctionValue()
				: base(TypeValue.Logical, "permission", TypeValue.Record)
			{
			}

			// Token: 0x06009591 RID: 38289 RVA: 0x001EEE11 File Offset: 0x001ED011
			public override LogicalValue TypedInvoke(RecordValue permission)
			{
				return LogicalValue.New(permission["PermissionKind"].AsText.String == "NativeQuery");
			}

			// Token: 0x04004F80 RID: 20352
			public static readonly FunctionValue Instance = new HostRelinkingFunctionValue.AllowNativeQueryFunctionValue();
		}

		// Token: 0x020016FE RID: 5886
		private sealed class InvokeWithCredentialsFunctionValue : HostRelinkingFunctionValue
		{
			// Token: 0x06009593 RID: 38291 RVA: 0x001EEE43 File Offset: 0x001ED043
			public InvokeWithCredentialsFunctionValue(IEngineHost engineHost, ExtensionModule extensionModule, string publicFunctionName, string privateFunctionName, string handlerFunctionName, FunctionValue handlerFunction, FunctionTypeValue dataSourceType)
				: base(engineHost, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType)
			{
			}

			// Token: 0x06009594 RID: 38292 RVA: 0x001EEE58 File Offset: 0x001ED058
			protected override Value InvokeN(Value[] args)
			{
				IExtensibilityService extensibilityService = this.engineHost as IExtensibilityService;
				if (extensibilityService == null)
				{
					ILibraryService libraryService = this.engineHost.QueryService<ILibraryService>();
					string text;
					if (ExtensionModule.TryGetModuleName(this.engineHost, out text))
					{
						ModuleTrustLevel trustLevel = libraryService.GetTrustLevel(text);
						if (trustLevel == ModuleTrustLevel.FirstParty || trustLevel == ModuleTrustLevel.Certified)
						{
							return base.RelinkNonDataSourceFunction(args).Invoke(args);
						}
					}
				}
				ResourceKindInfo resourceKindInfo;
				if (extensibilityService == null || !this.engine.TryLookupResourceKind(extensibilityService.CurrentResource.Kind, out resourceKindInfo))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				return base.RelinkFunction(resourceKindInfo, extensibilityService.CurrentResource, extensibilityService.CurrentCredentials, args, true).Invoke(args);
			}
		}

		// Token: 0x020016FF RID: 5887
		private sealed class InvokeWithPermissionsFunctionValue : HostRelinkingFunctionValue
		{
			// Token: 0x06009595 RID: 38293 RVA: 0x001EEE43 File Offset: 0x001ED043
			public InvokeWithPermissionsFunctionValue(IEngineHost engineHost, ExtensionModule extensionModule, string publicFunctionName, string privateFunctionName, string handlerFunctionName, FunctionValue handlerFunction, FunctionTypeValue dataSourceType)
				: base(engineHost, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType)
			{
			}

			// Token: 0x06009596 RID: 38294 RVA: 0x001EEEF8 File Offset: 0x001ED0F8
			protected override Value InvokeN(Value[] args)
			{
				IExtensibilityService extensibilityService = this.engineHost as IExtensibilityService;
				ResourceKindInfo resourceKindInfo;
				if (extensibilityService == null || !this.engine.TryLookupResourceKind(extensibilityService.CurrentResource.Kind, out resourceKindInfo))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				return base.RelinkFunction(resourceKindInfo, extensibilityService.CurrentResource, extensibilityService.CurrentCredentials, args, true).Invoke(args);
			}

			// Token: 0x06009597 RID: 38295 RVA: 0x001EEF56 File Offset: 0x001ED156
			protected override void SetHandler(InvocationEngineHost invocationHost, FunctionValue handler)
			{
				invocationHost.SetPermissionHandler(handler);
			}
		}

		// Token: 0x02001700 RID: 5888
		private sealed class DataSourceFunctionValue : HostRelinkingFunctionValue
		{
			// Token: 0x06009598 RID: 38296 RVA: 0x001EEF60 File Offset: 0x001ED160
			public DataSourceFunctionValue(IEngineHost engineHost, ResourceKindInfo resourceKindInfo, ExtensionResourceKindInfo extensionResourceKindInfo, ExtensionModule extensionModule, string publicFunctionName, string privateFunctionName, string handlerFunctionName, FunctionValue handlerFunction, FunctionTypeValue dataSourceType, Value userExperience)
				: base(engineHost, extensionModule, publicFunctionName, privateFunctionName, handlerFunctionName, handlerFunction, dataSourceType)
			{
				this.resourceKindInfo = resourceKindInfo;
				this.extensionResourceKindInfo = extensionResourceKindInfo;
				this.userExperience = userExperience;
				this.cache = new LruCache<HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey, FunctionValue>(16, null);
				Keys keys = dataSourceType.Parameters.Keys;
				this.makeResourceMap = HostRelinkingFunctionValue.DataSourceFunctionValue.MakeInvocationMap(keys, this.extensionResourceKindInfo.MakeResourcePathParameters);
				if (this.extensionResourceKindInfo.HasNativeQuery)
				{
					this.nativeQueryMap = HostRelinkingFunctionValue.DataSourceFunctionValue.MakeInvocationMap(keys, this.extensionResourceKindInfo.NativeQueryParameters);
				}
			}

			// Token: 0x1700271C RID: 10012
			// (get) Token: 0x06009599 RID: 38297 RVA: 0x001EEFEC File Offset: 0x001ED1EC
			public ResourceKindInfo ResourceKindInfo
			{
				get
				{
					return this.resourceKindInfo;
				}
			}

			// Token: 0x1700271D RID: 10013
			// (get) Token: 0x0600959A RID: 38298 RVA: 0x001EEFF4 File Offset: 0x001ED1F4
			public override RecordValue MetaValue
			{
				get
				{
					RecordBuilder recordBuilder = new RecordBuilder(2);
					recordBuilder.Add("DataSource.Kind", TextValue.New(this.resourceKindInfo.Kind), TypeValue.Text);
					if (this.userExperience != null)
					{
						recordBuilder.Add("Publish", this.userExperience, this.userExperience.Type);
					}
					return base.MetaValue.Concatenate(recordBuilder.ToRecord()).AsRecord;
				}
			}

			// Token: 0x0600959B RID: 38299 RVA: 0x001EF068 File Offset: 0x001ED268
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				ListValue listValue;
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression, out listValue);
				if (argumentValues != null)
				{
					Value value;
					int num;
					string[] array;
					ValueException ex;
					IResource resource = this.GetResource(argumentValues, out value, out num, out array, out ex);
					if (resource != null)
					{
						using (IEnumerator<ExtensionDataSourceLocationFactory> enumerator = this.extensionResourceKindInfo.DsrHandlers.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.TryGetLocation(expression, listValue, out location, out foundOptions, out unknownOptions))
								{
									return true;
								}
							}
						}
						if (listValue.IsEmpty)
						{
							location = new InternalDataSourceDataSourceLocation(resource.Kind, resource.NonNormalizedPath);
							foundOptions = RecordValue.Empty;
							unknownOptions = Keys.Empty;
							if (value == null)
							{
								unknownOptions = null;
							}
							else if (value.IsText)
							{
								location.Query = value.AsString;
							}
							return true;
						}
					}
				}
				IL_00B7:
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0600959C RID: 38300 RVA: 0x001EF14C File Offset: 0x001ED34C
			private static int[] MakeInvocationMap(Keys callSiteParameters, Keys inspectorParameters)
			{
				int[] array = new int[inspectorParameters.Length];
				int num = 0;
				for (int i = 0; i < inspectorParameters.Length; i++)
				{
					string text = inspectorParameters[i];
					array[num] = -1;
					for (int j = 0; j < callSiteParameters.Length; j++)
					{
						if (text == callSiteParameters[j])
						{
							array[num] = j;
							break;
						}
					}
					if (array[i] == -1)
					{
						Array.Resize<int>(ref array, array.Length - 1);
					}
					else
					{
						num++;
					}
				}
				return array;
			}

			// Token: 0x0600959D RID: 38301 RVA: 0x001EF1CC File Offset: 0x001ED3CC
			private static Value[] RemapArguments(int[] map, Value[] originalArgs)
			{
				Value[] array = new Value[map.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num = map[i];
					array[i] = ((num < originalArgs.Length) ? originalArgs[num] : Value.Null);
				}
				return array;
			}

			// Token: 0x0600959E RID: 38302 RVA: 0x001EF208 File Offset: 0x001ED408
			private IResource GetResource(Value[] values, out Value nativeQuery, out int parameterCount, out string[] parameterNames, out ValueException error)
			{
				error = null;
				parameterCount = 0;
				parameterNames = null;
				if (this.extensionResourceKindInfo.HasNativeQuery)
				{
					try
					{
						nativeQuery = this.extensionResourceKindInfo.NativeQuery(HostRelinkingFunctionValue.DataSourceFunctionValue.RemapArguments(this.nativeQueryMap, values));
						string text;
						if (NativeQueryRecord.TryGetNativeQuery(nativeQuery, out text, out parameterCount, out parameterNames))
						{
							nativeQuery = TextValue.New(text);
						}
						goto IL_005A;
					}
					catch (ValueException ex)
					{
						error = ex;
						nativeQuery = null;
						goto IL_005A;
					}
				}
				nativeQuery = Value.Null;
				IL_005A:
				string asString = this.extensionResourceKindInfo.MakeResourcePath(HostRelinkingFunctionValue.DataSourceFunctionValue.RemapArguments(this.makeResourceMap, values)).AsText.AsString;
				IResource resource;
				if (this.resourceKindInfo.IsUri)
				{
					string text2;
					string text3;
					if (!Resource.TryNormalizeWebUri(asString, out text2, out text3))
					{
						error = ValueException.NewExpressionError<Message0>(Strings.Resource_WebUrl_Invalid, null, null);
						return null;
					}
					resource = new Resource(this.resourceKindInfo.Kind, text2, text3);
				}
				else
				{
					resource = new Resource(this.resourceKindInfo.Kind, asString, asString);
				}
				return resource;
			}

			// Token: 0x1700271E RID: 10014
			// (get) Token: 0x0600959F RID: 38303 RVA: 0x001EF2F8 File Offset: 0x001ED4F8
			public override string PrimaryResourceKind
			{
				get
				{
					return this.resourceKindInfo.Kind;
				}
			}

			// Token: 0x060095A0 RID: 38304 RVA: 0x001EF308 File Offset: 0x001ED508
			protected override Value InvokeN(Value[] args)
			{
				Value value;
				int num;
				string[] array;
				ValueException ex;
				IResource resource = this.GetResource(args, out value, out num, out array, out ex);
				if (ex != null)
				{
					throw ex;
				}
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.engineHost, resource, null);
				if (value.IsText)
				{
					HostResourceQueryPermissionService.VerifyQueryPermission(this.engineHost, resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, value.AsString, num, array);
				}
				HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey cacheKey = new HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey(resource, resourceCredentialCollection, Extension.Modules);
				FunctionValue functionValue;
				if (!this.cache.TryGetValue(cacheKey, out functionValue))
				{
					functionValue = base.RelinkFunction(this.resourceKindInfo, resource, resourceCredentialCollection, args, false);
					if (this.handlerFunctionName == null)
					{
						this.cache.Add(cacheKey, functionValue);
					}
				}
				return functionValue.Invoke(args);
			}

			// Token: 0x04004F81 RID: 20353
			private readonly ResourceKindInfo resourceKindInfo;

			// Token: 0x04004F82 RID: 20354
			private readonly ExtensionResourceKindInfo extensionResourceKindInfo;

			// Token: 0x04004F83 RID: 20355
			private readonly int[] makeResourceMap;

			// Token: 0x04004F84 RID: 20356
			private readonly int[] nativeQueryMap;

			// Token: 0x04004F85 RID: 20357
			private readonly Value userExperience;

			// Token: 0x04004F86 RID: 20358
			private readonly LruCache<HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey, FunctionValue> cache;

			// Token: 0x02001701 RID: 5889
			private class CacheKey : IEquatable<HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey>
			{
				// Token: 0x060095A1 RID: 38305 RVA: 0x001EF3AD File Offset: 0x001ED5AD
				public CacheKey(IResource resource, ResourceCredentialCollection credentials, string[] modules)
				{
					this.resource = resource;
					this.credentials = credentials;
					this.modules = modules;
				}

				// Token: 0x060095A2 RID: 38306 RVA: 0x001EF3CA File Offset: 0x001ED5CA
				public override int GetHashCode()
				{
					return this.resource.GetHashCode();
				}

				// Token: 0x060095A3 RID: 38307 RVA: 0x001EF3D7 File Offset: 0x001ED5D7
				public override bool Equals(object other)
				{
					return this.Equals(other as HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey);
				}

				// Token: 0x060095A4 RID: 38308 RVA: 0x001EF3E5 File Offset: 0x001ED5E5
				public bool Equals(HostRelinkingFunctionValue.DataSourceFunctionValue.CacheKey other)
				{
					return other != null && this.resource.Equals(other.resource) && this.credentials.SequenceEqual(other.credentials) && this.modules.SequenceEqual(other.modules);
				}

				// Token: 0x04004F87 RID: 20359
				private readonly IResource resource;

				// Token: 0x04004F88 RID: 20360
				private readonly ResourceCredentialCollection credentials;

				// Token: 0x04004F89 RID: 20361
				private readonly string[] modules;
			}
		}
	}
}
