using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000063 RID: 99
	internal class RequestInfo
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		internal RequestInfo(DataServiceContext context, bool isContinuation)
			: this(context)
		{
			this.IsContinuation = isContinuation;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		internal RequestInfo(DataServiceContext context)
		{
			this.Context = context;
			this.WriteHelper = new ODataMessageWritingHelper(this);
			this.typeResolver = new TypeResolver(context.Model, new Func<string, Type>(context.ResolveTypeFromName), new Func<Type, string>(context.ResolveNameFromTypeInternal), context.Format.ServiceModel);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D01E File Offset: 0x0000B21E
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000D026 File Offset: 0x0000B226
		internal ODataMessageWritingHelper WriteHelper { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D02F File Offset: 0x0000B22F
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000D037 File Offset: 0x0000B237
		internal DataServiceContext Context { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000D040 File Offset: 0x0000B240
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000D048 File Offset: 0x0000B248
		internal bool IsContinuation { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D051 File Offset: 0x0000B251
		internal DataServiceClientConfigurations Configurations
		{
			get
			{
				return this.Context.Configurations;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D05E File Offset: 0x0000B25E
		internal EntityTracker EntityTracker
		{
			get
			{
				return this.Context.EntityTracker;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000D06B File Offset: 0x0000B26B
		internal bool IgnoreResourceNotFoundException
		{
			get
			{
				return this.Context.IgnoreResourceNotFoundException;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D078 File Offset: 0x0000B278
		internal bool HasResolveName
		{
			get
			{
				return this.Context.ResolveName != null;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000D088 File Offset: 0x0000B288
		internal bool IsUserSuppliedResolver
		{
			get
			{
				MethodInfo method = this.Context.ResolveName.Method;
				GeneratedCodeAttribute generatedCodeAttribute = method.GetCustomAttributes(false).OfType<GeneratedCodeAttribute>().FirstOrDefault<GeneratedCodeAttribute>();
				return generatedCodeAttribute == null || generatedCodeAttribute.Tool != "Microsoft.OData.Service.Design";
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000D0CD File Offset: 0x0000B2CD
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.Context.BaseUriResolver;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000D0DA File Offset: 0x0000B2DA
		internal DataServiceResponsePreference AddAndUpdateResponsePreference
		{
			get
			{
				return this.Context.AddAndUpdateResponsePreference;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		internal Version MaxProtocolVersionAsVersion
		{
			get
			{
				return this.Context.MaxProtocolVersionAsVersion;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		internal bool HasSendingRequest2EventHandlers
		{
			get
			{
				return this.Context.HasSendingRequest2EventHandlers;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000D101 File Offset: 0x0000B301
		internal bool UserModifiedRequestInBuildingRequest
		{
			get
			{
				return this.Context.HasBuildingRequestEventHandlers;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000D10E File Offset: 0x0000B30E
		internal ICredentials Credentials
		{
			get
			{
				return this.Context.Credentials;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000D11B File Offset: 0x0000B31B
		internal int Timeout
		{
			get
			{
				return this.Context.Timeout;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000D128 File Offset: 0x0000B328
		internal bool UsePostTunneling
		{
			get
			{
				return this.Context.UsePostTunneling;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000D135 File Offset: 0x0000B335
		internal ClientEdmModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D142 File Offset: 0x0000B342
		internal DataServiceClientFormat Format
		{
			get
			{
				return this.Context.Format;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000D14F File Offset: 0x0000B34F
		internal TypeResolver TypeResolver
		{
			get
			{
				return this.typeResolver;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D157 File Offset: 0x0000B357
		internal HttpStack HttpStack
		{
			get
			{
				return this.Context.HttpStack;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000D164 File Offset: 0x0000B364
		internal bool UseDefaultCredentials
		{
			get
			{
				return this.Context.UseDefaultCredentials;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D171 File Offset: 0x0000B371
		internal IODataResponseMessage GetSyncronousResponse(ODataRequestMessageWrapper request, bool handleWebException)
		{
			return this.Context.GetSyncronousResponse(request, handleWebException);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D180 File Offset: 0x0000B380
		internal IODataResponseMessage EndGetResponse(ODataRequestMessageWrapper request, IAsyncResult asyncResult)
		{
			return this.Context.EndGetResponse(request, asyncResult);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D190 File Offset: 0x0000B390
		internal string GetServerTypeName(EntityDescriptor descriptor)
		{
			string text;
			if (this.HasResolveName)
			{
				Type type = descriptor.Entity.GetType();
				if (this.IsUserSuppliedResolver)
				{
					text = this.ResolveNameFromType(type) ?? descriptor.GetLatestServerTypeName();
				}
				else
				{
					text = descriptor.GetLatestServerTypeName() ?? this.ResolveNameFromType(type);
				}
			}
			else
			{
				text = descriptor.GetLatestServerTypeName();
			}
			return text;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		internal string GetServerTypeName(ClientTypeAnnotation clientTypeAnnotation)
		{
			return this.ResolveNameFromType(clientTypeAnnotation.ElementType);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D208 File Offset: 0x0000B408
		internal string InferServerTypeNameFromServerModel(EntityDescriptor descriptor)
		{
			if (descriptor.EntitySetName != null)
			{
				string text;
				if (this.TypeResolver.TryResolveEntitySetBaseTypeName(descriptor.EntitySetName, out text))
				{
					return text;
				}
			}
			else if (descriptor.IsDeepInsert)
			{
				string text2 = this.GetServerTypeName(descriptor.ParentForInsert);
				if (text2 == null)
				{
					text2 = this.InferServerTypeNameFromServerModel(descriptor.ParentForInsert);
				}
				string text3;
				if (this.TypeResolver.TryResolveNavigationTargetTypeName(text2, descriptor.ParentPropertyForInsert, out text3))
				{
					return text3;
				}
			}
			return null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000D272 File Offset: 0x0000B472
		internal string ResolveNameFromType(Type type)
		{
			return this.Context.ResolveNameFromTypeInternal(type);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000D280 File Offset: 0x0000B480
		internal ResponseInfo GetDeserializationInfo(MergeOption? mergeOption)
		{
			return new ResponseInfo(this, (mergeOption != null) ? mergeOption.Value : this.Context.MergeOption);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000D2A5 File Offset: 0x0000B4A5
		internal ResponseInfo GetDeserializationInfoForLoadProperty(MergeOption? mergeOption, EntityDescriptor entityDescriptor, ClientPropertyAnnotation property)
		{
			return new LoadPropertyResponseInfo(this, (mergeOption != null) ? mergeOption.Value : this.Context.MergeOption, entityDescriptor, property);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		internal InvalidOperationException ValidateResponseVersion(Version responseVersion)
		{
			if (responseVersion != null && responseVersion > this.Context.MaxProtocolVersionAsVersion)
			{
				string text = Strings.Context_ResponseVersionIsBiggerThanProtocolVersion(responseVersion.ToString(), this.Context.MaxProtocolVersion.ToString());
				return Error.InvalidOperation(text);
			}
			return null;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000D322 File Offset: 0x0000B522
		internal void FireSendingRequest(SendingRequestEventArgs eventArgs)
		{
			this.Context.FireSendingRequest(eventArgs);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D330 File Offset: 0x0000B530
		internal void FireSendingRequest2(SendingRequest2EventArgs eventArgs)
		{
			this.Context.FireSendingRequest2(eventArgs);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000D340 File Offset: 0x0000B540
		internal DataServiceClientRequestMessage CreateRequestMessage(BuildingRequestEventArgs requestMessageArgs)
		{
			IDictionary<string, string> underlyingDictionary = requestMessageArgs.HeaderCollection.UnderlyingDictionary;
			if (this.UsePostTunneling)
			{
				bool flag = false;
				if (string.CompareOrdinal("GET", requestMessageArgs.Method) != 0 && string.CompareOrdinal("POST", requestMessageArgs.Method) != 0)
				{
					flag = true;
				}
				if (flag)
				{
					underlyingDictionary["X-HTTP-Method"] = requestMessageArgs.Method;
				}
				if (string.CompareOrdinal("DELETE", requestMessageArgs.Method) == 0)
				{
					underlyingDictionary["Content-Length"] = "0";
				}
			}
			DataServiceClientRequestMessageArgs dataServiceClientRequestMessageArgs = new DataServiceClientRequestMessageArgs(requestMessageArgs.Method, requestMessageArgs.RequestUri, this.UseDefaultCredentials, this.UsePostTunneling, underlyingDictionary);
			DataServiceClientRequestMessage dataServiceClientRequestMessage;
			if (this.Configurations.RequestPipeline.OnMessageCreating != null)
			{
				dataServiceClientRequestMessage = this.Configurations.RequestPipeline.OnMessageCreating(dataServiceClientRequestMessageArgs);
				if (dataServiceClientRequestMessage == null)
				{
					throw Error.InvalidOperation(Strings.Context_OnMessageCreatingReturningNull);
				}
			}
			else
			{
				dataServiceClientRequestMessage = new HttpWebRequestMessage(dataServiceClientRequestMessageArgs);
			}
			return dataServiceClientRequestMessage;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000D41D File Offset: 0x0000B61D
		internal BuildingRequestEventArgs CreateRequestArgsAndFireBuildingRequest(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor)
		{
			return this.Context.CreateRequestArgsAndFireBuildingRequest(method, requestUri, headers, httpStack, descriptor);
		}

		// Token: 0x04000111 RID: 273
		private readonly TypeResolver typeResolver;
	}
}
