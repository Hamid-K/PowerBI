using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000021 RID: 33
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Pending")]
	internal abstract class BaseSaveResult : BaseAsyncResult
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000514C File Offset: 0x0000334C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "IsBatch returns a constant value and hence safe to be invoked from the constructor")]
		internal BaseSaveResult(DataServiceContext context, string method, DataServiceRequest[] queries, SaveChangesOptions options, AsyncCallback callback, object state)
			: base(context, method, callback, state)
		{
			this.RequestInfo = new RequestInfo(context);
			this.Options = options;
			this.SerializerInstance = new Serializer(this.RequestInfo, options);
			if (queries == null)
			{
				this.ChangedEntries = (from o in context.EntityTracker.Entities.Cast<Descriptor>().Union(context.EntityTracker.Links.Cast<Descriptor>()).Union(context.EntityTracker.Entities.SelectMany((EntityDescriptor e) => e.StreamDescriptors).Cast<Descriptor>())
					where o.IsModified && o.ChangeOrder != uint.MaxValue
					orderby o.ChangeOrder
					select o).ToList<Descriptor>();
				using (List<Descriptor>.Enumerator enumerator = this.ChangedEntries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Descriptor descriptor = enumerator.Current;
						descriptor.ContentGeneratedForSave = false;
						descriptor.SaveResultWasProcessed = (EntityStates)0;
						descriptor.SaveError = null;
						if (descriptor.DescriptorKind == DescriptorKind.Link)
						{
							object target = ((LinkDescriptor)descriptor).Target;
							if (target != null)
							{
								Descriptor entityDescriptor = context.EntityTracker.GetEntityDescriptor(target);
								if (EntityStates.Unchanged == entityDescriptor.State)
								{
									entityDescriptor.ContentGeneratedForSave = false;
									entityDescriptor.SaveResultWasProcessed = (EntityStates)0;
									entityDescriptor.SaveError = null;
								}
							}
						}
					}
					return;
				}
			}
			this.ChangedEntries = new List<Descriptor>();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E1 RID: 225
		internal abstract bool IsBatchRequest { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E2 RID: 226
		protected abstract Stream ResponseStream { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E3 RID: 227
		protected abstract bool ProcessResponsePayload { get; }

		// Token: 0x060000E4 RID: 228 RVA: 0x000052EC File Offset: 0x000034EC
		internal static BaseSaveResult CreateSaveResult(DataServiceContext context, string method, DataServiceRequest[] queries, SaveChangesOptions options, AsyncCallback callback, object state)
		{
			if (!Util.IsBatch(options))
			{
				return new SaveResult(context, method, options, callback, state);
			}
			return new BatchSaveResult(context, method, queries, options, callback, state);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005310 File Offset: 0x00003510
		internal static InvalidOperationException HandleResponse(RequestInfo requestInfo, HttpStatusCode statusCode, string responseVersion, Func<Stream> getResponseStream, bool throwOnFailure, out Version parsedResponseVersion)
		{
			InvalidOperationException ex = null;
			if (!BaseSaveResult.CanHandleResponseVersion(responseVersion, out parsedResponseVersion))
			{
				string text = Strings.Context_VersionNotSupported(responseVersion, BaseSaveResult.SerializeSupportedVersions());
				ex = Error.InvalidOperation(text);
			}
			if (ex == null)
			{
				ex = requestInfo.ValidateResponseVersion(parsedResponseVersion);
			}
			if (ex == null && !WebUtil.SuccessStatusCode(statusCode))
			{
				ex = BaseSaveResult.GetResponseText(getResponseStream, statusCode);
			}
			if (ex != null && throwOnFailure)
			{
				throw ex;
			}
			return ex;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005368 File Offset: 0x00003568
		[SuppressMessage("Microsoft.Design", "CA1031", Justification = "Cache exception so user can examine it later")]
		internal static DataServiceClientException GetResponseText(Func<Stream> getResponseStream, HttpStatusCode statusCode)
		{
			string text = null;
			using (Stream stream = getResponseStream())
			{
				if (stream != null && stream.CanRead)
				{
					text = new StreamReader(stream).ReadToEnd();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = statusCode.ToString();
			}
			return new DataServiceClientException(text, (int)statusCode);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000053D0 File Offset: 0x000035D0
		internal DataServiceResponse EndRequest()
		{
			foreach (Descriptor descriptor in this.ChangedEntries)
			{
				descriptor.ClearChanges();
			}
			return this.HandleResponse();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005428 File Offset: 0x00003628
		protected static string GetLinkHttpMethod(LinkDescriptor link)
		{
			if (!link.IsSourcePropertyCollection)
			{
				if (link.Target == null)
				{
					return "DELETE";
				}
				return "PUT";
			}
			else
			{
				if (EntityStates.Deleted == link.State)
				{
					return "DELETE";
				}
				return "POST";
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000545C File Offset: 0x0000365C
		protected static void ApplyPreferences(HeaderCollection headers, string method, DataServiceResponsePreference responsePreference, ref Version requestVersion)
		{
			if (string.CompareOrdinal("POST", method) != 0 && string.CompareOrdinal("PUT", method) != 0 && string.CompareOrdinal("PATCH", method) != 0)
			{
				return;
			}
			string preferHeaderAndRequestVersion = WebUtil.GetPreferHeaderAndRequestVersion(responsePreference, ref requestVersion);
			if (preferHeaderAndRequestVersion != null)
			{
				headers.SetHeader("Prefer", preferHeaderAndRequestVersion);
			}
		}

		// Token: 0x060000EA RID: 234
		protected abstract DataServiceResponse HandleResponse();

		// Token: 0x060000EB RID: 235
		protected abstract ODataRequestMessageWrapper CreateRequestMessage(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor, string contentId);

		// Token: 0x060000EC RID: 236 RVA: 0x000054A8 File Offset: 0x000036A8
		protected string GetHttpMethod(EntityStates state, ref Version requestVersion)
		{
			if (state == EntityStates.Added)
			{
				return "POST";
			}
			if (state == EntityStates.Deleted)
			{
				return "DELETE";
			}
			if (state != EntityStates.Modified)
			{
				throw Error.InternalError(InternalError.UnvalidatedEntityState);
			}
			if (Util.IsFlagSet(this.Options, SaveChangesOptions.ReplaceOnUpdate))
			{
				return "PUT";
			}
			return "PATCH";
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000054E8 File Offset: 0x000036E8
		protected bool CreateChangeData(int index, ODataRequestMessageWrapper requestMessage)
		{
			Descriptor descriptor = this.ChangedEntries[index];
			if (descriptor.DescriptorKind == DescriptorKind.Entity)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				descriptor.ContentGeneratedForSave = true;
				return this.CreateRequestData(entityDescriptor, requestMessage);
			}
			descriptor.ContentGeneratedForSave = true;
			LinkDescriptor linkDescriptor = (LinkDescriptor)descriptor;
			if (EntityStates.Added == linkDescriptor.State || (EntityStates.Modified == linkDescriptor.State && linkDescriptor.Target != null))
			{
				this.CreateRequestData(linkDescriptor, requestMessage);
				return true;
			}
			return false;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005558 File Offset: 0x00003758
		protected override void HandleCompleted(BaseAsyncResult.PerRequest pereq)
		{
			if (pereq != null)
			{
				base.SetCompletedSynchronously(pereq.RequestCompletedSynchronously);
				if (pereq.RequestCompleted)
				{
					Interlocked.CompareExchange<BaseAsyncResult.PerRequest>(ref this.perRequest, null, pereq);
					if (this.IsBatchRequest)
					{
						Interlocked.CompareExchange<IODataResponseMessage>(ref this.batchResponseMessage, pereq.ResponseMessage, null);
						pereq.ResponseMessage = null;
					}
					pereq.Dispose();
				}
			}
			base.HandleCompleted();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000055B8 File Offset: 0x000037B8
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		protected override void AsyncEndGetResponse(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndGetResponseCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndGetResponse);
				ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndGetResponseRequest);
				IODataResponseMessage iodataResponseMessage = this.RequestInfo.EndGetResponse(odataRequestMessageWrapper, asyncResult);
				perRequest.ResponseMessage = Util.NullCheck<IODataResponseMessage>(iodataResponseMessage, InternalError.InvalidEndGetResponseResponse);
				if (!this.IsBatchRequest)
				{
					this.HandleOperationResponse(iodataResponseMessage);
					this.HandleOperationResponseHeaders((HttpStatusCode)iodataResponseMessage.StatusCode, new HeaderCollection(iodataResponseMessage));
				}
				Stream stream = iodataResponseMessage.GetStream();
				perRequest.ResponseStream = stream;
				if (stream != null && stream.CanRead)
				{
					if (this.buildBatchBuffer == null)
					{
						this.buildBatchBuffer = new byte[8000];
					}
					do
					{
						asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(stream.BeginRead), this.buildBatchBuffer, 0, this.buildBatchBuffer.Length, new AsyncCallback(this.AsyncEndRead), new BaseSaveResult.AsyncReadState(perRequest));
						perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
						if (!asyncResult.CompletedSynchronously || perRequest.RequestCompleted || base.IsCompletedInternally)
						{
							break;
						}
					}
					while (stream.CanRead);
				}
				else
				{
					perRequest.SetComplete();
					if (!base.IsCompletedInternally && !perRequest.RequestCompletedSynchronously)
					{
						this.FinishCurrentChange(perRequest);
					}
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x060000F0 RID: 240
		protected abstract void HandleOperationResponse(IODataResponseMessage responseMessage);

		// Token: 0x060000F1 RID: 241 RVA: 0x00005760 File Offset: 0x00003960
		protected void HandleOperationResponseHeaders(HttpStatusCode statusCode, HeaderCollection headers)
		{
			Descriptor descriptor = this.ChangedEntries[this.entryIndex];
			if (descriptor.DescriptorKind == DescriptorKind.Entity)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				if ((descriptor.State == EntityStates.Added || this.streamRequestKind == BaseSaveResult.StreamRequestKind.PostMediaResource || !Util.IsFlagSet(this.Options, SaveChangesOptions.ReplaceOnUpdate)) && WebUtil.SuccessStatusCode(statusCode))
				{
					Uri uri = null;
					string text;
					headers.TryGetHeader("Location", out text);
					string text2;
					headers.TryGetHeader("OData-EntityId", out text2);
					if (text != null)
					{
						uri = WebUtil.ValidateLocationHeader(text);
					}
					else if (descriptor.State == EntityStates.Added || this.streamRequestKind == BaseSaveResult.StreamRequestKind.PostMediaResource)
					{
						throw Error.NotSupported(Strings.Deserialize_NoLocationHeader);
					}
					Uri uri2;
					if (text2 != null)
					{
						uri2 = WebUtil.ValidateIdentityValue(text2);
						if (text == null)
						{
							throw Error.NotSupported(Strings.Context_BothLocationAndIdMustBeSpecified);
						}
					}
					else
					{
						uri2 = UriUtil.CreateUri(text, UriKind.Absolute);
					}
					if (null != uri)
					{
						this.RequestInfo.EntityTracker.AttachLocation(entityDescriptor.Entity, uri2, uri);
					}
				}
				if (this.streamRequestKind != BaseSaveResult.StreamRequestKind.None)
				{
					if (!WebUtil.SuccessStatusCode(statusCode))
					{
						if (this.streamRequestKind == BaseSaveResult.StreamRequestKind.PostMediaResource)
						{
							descriptor.State = EntityStates.Added;
						}
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.None;
						descriptor.ContentGeneratedForSave = true;
						return;
					}
					string text3;
					if (this.streamRequestKind == BaseSaveResult.StreamRequestKind.PostMediaResource && headers.TryGetHeader("ETag", out text3))
					{
						entityDescriptor.ETag = text3;
					}
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000058A0 File Offset: 0x00003AA0
		protected void HandleOperationResponse(Descriptor descriptor, HeaderCollection contentHeaders)
		{
			EntityStates entityStates = EntityStates.Unchanged;
			if (descriptor.DescriptorKind == DescriptorKind.Entity)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				entityStates = entityDescriptor.StreamState;
			}
			if (entityStates == EntityStates.Added || descriptor.State == EntityStates.Added)
			{
				this.HandleResponsePost(descriptor, contentHeaders);
				return;
			}
			if (entityStates == EntityStates.Modified || descriptor.State == EntityStates.Modified)
			{
				this.HandleResponsePut(descriptor, contentHeaders);
				return;
			}
			if (descriptor.State == EntityStates.Deleted)
			{
				this.HandleResponseDelete(descriptor);
			}
		}

		// Token: 0x060000F3 RID: 243
		protected abstract MaterializeAtom GetMaterializer(EntityDescriptor entityDescriptor, ResponseInfo responseInfo);

		// Token: 0x060000F4 RID: 244 RVA: 0x00005903 File Offset: 0x00003B03
		protected override void CompletedRequest()
		{
			this.buildBatchBuffer = null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000590C File Offset: 0x00003B0C
		protected ResponseInfo CreateResponseInfo(EntityDescriptor entityDescriptor)
		{
			MergeOption mergeOption = MergeOption.OverwriteChanges;
			if (entityDescriptor.StreamState == EntityStates.Added)
			{
				mergeOption = MergeOption.PreserveChanges;
			}
			return this.RequestInfo.GetDeserializationInfo(new MergeOption?(mergeOption));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005937 File Offset: 0x00003B37
		protected IEnumerable<LinkDescriptor> RelatedLinks(EntityDescriptor entityDescriptor)
		{
			foreach (LinkDescriptor linkDescriptor in this.RequestInfo.EntityTracker.Links)
			{
				if (linkDescriptor.Source == entityDescriptor.Entity && linkDescriptor.Target != null)
				{
					EntityDescriptor entityDescriptor2 = this.RequestInfo.EntityTracker.GetEntityDescriptor(linkDescriptor.Target);
					if (Util.IncludeLinkState(entityDescriptor2.SaveResultWasProcessed) || (entityDescriptor2.SaveResultWasProcessed == (EntityStates)0 && Util.IncludeLinkState(entityDescriptor2.State)) || (null != entityDescriptor2.Identity && entityDescriptor2.ChangeOrder < entityDescriptor.ChangeOrder && ((entityDescriptor2.SaveResultWasProcessed == (EntityStates)0 && EntityStates.Added == entityDescriptor2.State) || EntityStates.Added == entityDescriptor2.SaveResultWasProcessed)))
					{
						yield return linkDescriptor;
					}
				}
			}
			IEnumerator<LinkDescriptor> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005950 File Offset: 0x00003B50
		protected int SaveResultProcessed(Descriptor descriptor)
		{
			descriptor.SaveResultWasProcessed = descriptor.State;
			int num = 0;
			if (descriptor.DescriptorKind == DescriptorKind.Entity && EntityStates.Added == descriptor.State)
			{
				foreach (LinkDescriptor linkDescriptor in this.RelatedLinks((EntityDescriptor)descriptor))
				{
					if (linkDescriptor.ContentGeneratedForSave)
					{
						linkDescriptor.SaveResultWasProcessed = linkDescriptor.State;
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000059D4 File Offset: 0x00003BD4
		protected ODataRequestMessageWrapper CreateRequest(LinkDescriptor binding)
		{
			if (binding.ContentGeneratedForSave)
			{
				return null;
			}
			EntityDescriptor entityDescriptor = this.RequestInfo.EntityTracker.GetEntityDescriptor(binding.Source);
			EntityDescriptor entityDescriptor2 = ((binding.Target != null) ? this.RequestInfo.EntityTracker.GetEntityDescriptor(binding.Target) : null);
			if (!Util.IsBatchWithSingleChangeset(this.Options))
			{
				BaseSaveResult.ValidateLinkDescriptorSourceAndTargetHaveIdentities(binding, entityDescriptor, entityDescriptor2);
			}
			LinkInfo linkInfo = null;
			Uri uri;
			if (entityDescriptor.TryGetLinkInfo(binding.SourceProperty, out linkInfo) && linkInfo.AssociationLink != null)
			{
				uri = linkInfo.AssociationLink;
			}
			else
			{
				Uri uri2;
				if (null == entityDescriptor.GetLatestIdentity())
				{
					uri2 = UriUtil.CreateUri("$" + entityDescriptor.ChangeOrder.ToString(CultureInfo.InvariantCulture), UriKind.Relative);
				}
				else
				{
					uri2 = entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false);
				}
				string sourcePropertyUri = this.GetSourcePropertyUri(binding, entityDescriptor);
				Uri uri3 = UriUtil.CreateUri(sourcePropertyUri, UriKind.Relative);
				uri3 = UriUtil.CreateUri(UriUtil.UriToString(uri3) + "/$ref", UriKind.Relative);
				uri = UriUtil.CreateUri(uri2, uri3);
			}
			uri = BaseSaveResult.AppendTargetEntityKeyIfNeeded(uri, binding, entityDescriptor2);
			string linkHttpMethod = BaseSaveResult.GetLinkHttpMethod(binding);
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(Util.ODataVersion4, this.RequestInfo.MaxProtocolVersionAsVersion);
			this.RequestInfo.Format.SetRequestAcceptHeader(headerCollection);
			if (EntityStates.Added == binding.State || (EntityStates.Modified == binding.State && binding.Target != null))
			{
				this.RequestInfo.Format.SetRequestContentTypeForLinks(headerCollection);
			}
			return this.CreateRequestMessage(linkHttpMethod, uri, headerCollection, this.RequestInfo.HttpStack, binding, this.IsBatchRequest ? binding.ChangeOrder.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B8C File Offset: 0x00003D8C
		protected ODataRequestMessageWrapper CreateRequest(EntityDescriptor entityDescriptor)
		{
			EntityStates state = entityDescriptor.State;
			Uri resourceUri = entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false);
			ClientEdmModel model = this.RequestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
			Version version = BaseSaveResult.DetermineRequestVersion(clientTypeAnnotation);
			string httpMethod = this.GetHttpMethod(state, ref version);
			HeaderCollection headerCollection = new HeaderCollection();
			if (EntityStates.Deleted != entityDescriptor.State)
			{
				this.RequestInfo.Context.Format.SetRequestContentTypeForEntry(headerCollection);
			}
			if (EntityStates.Deleted == state || EntityStates.Modified == state)
			{
				string latestETag = entityDescriptor.GetLatestETag();
				if (latestETag != null)
				{
					headerCollection.SetHeader("If-Match", latestETag);
				}
			}
			BaseSaveResult.ApplyPreferences(headerCollection, httpMethod, this.RequestInfo.AddAndUpdateResponsePreference, ref version);
			headerCollection.SetRequestVersion(version, this.RequestInfo.MaxProtocolVersionAsVersion);
			this.RequestInfo.Format.SetRequestAcceptHeader(headerCollection);
			return this.CreateRequestMessage(httpMethod, resourceUri, headerCollection, this.RequestInfo.HttpStack, entityDescriptor, this.IsBatchRequest ? entityDescriptor.ChangeOrder.ToString(CultureInfo.InvariantCulture) : null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005CA8 File Offset: 0x00003EA8
		protected ODataRequestMessageWrapper CreateTopLevelRequest(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor)
		{
			BuildingRequestEventArgs buildingRequestEventArgs = this.RequestInfo.CreateRequestArgsAndFireBuildingRequest(method, requestUri, headers, httpStack, descriptor);
			return this.RequestInfo.WriteHelper.CreateRequestMessage(buildingRequestEventArgs);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005CDC File Offset: 0x00003EDC
		private static Version DetermineRequestVersion(ClientTypeAnnotation clientType)
		{
			Version odataVersion = Util.ODataVersion4;
			WebUtil.RaiseVersion(ref odataVersion, clientType.GetMetadataVersion());
			return odataVersion;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005D00 File Offset: 0x00003F00
		private static bool CanHandleResponseVersion(string responseVersion, out Version parsedResponseVersion)
		{
			parsedResponseVersion = null;
			if (!string.IsNullOrEmpty(responseVersion))
			{
				KeyValuePair<Version, string> keyValuePair;
				if (!CommonUtil.TryReadVersion(responseVersion, out keyValuePair))
				{
					return false;
				}
				if (!Util.SupportedResponseVersions.Contains(keyValuePair.Key))
				{
					return false;
				}
				parsedResponseVersion = keyValuePair.Key;
			}
			return true;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005D43 File Offset: 0x00003F43
		private static void HandleResponsePost(LinkDescriptor linkDescriptor)
		{
			if (EntityStates.Added != linkDescriptor.State && (EntityStates.Modified != linkDescriptor.State || linkDescriptor.Target == null))
			{
				Error.ThrowBatchUnexpectedContent(InternalError.LinkNotAddedState);
			}
			linkDescriptor.State = EntityStates.Unchanged;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005D70 File Offset: 0x00003F70
		private static void ValidateLinkDescriptorSourceAndTargetHaveIdentities(LinkDescriptor binding, EntityDescriptor sourceResource, EntityDescriptor targetResource)
		{
			if (null == sourceResource.GetLatestIdentity())
			{
				binding.ContentGeneratedForSave = true;
				throw Error.InvalidOperation(Strings.Context_LinkResourceInsertFailure, sourceResource.SaveError);
			}
			if (targetResource != null && null == targetResource.GetLatestIdentity())
			{
				binding.ContentGeneratedForSave = true;
				throw Error.InvalidOperation(Strings.Context_LinkResourceInsertFailure, targetResource.SaveError);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005DCC File Offset: 0x00003FCC
		private static string SerializeSupportedVersions()
		{
			StringBuilder stringBuilder = new StringBuilder("'").Append(Util.SupportedResponseVersions[0].ToString());
			for (int i = 1; i < Util.SupportedResponseVersions.Length; i++)
			{
				stringBuilder.Append("', '");
				stringBuilder.Append(Util.SupportedResponseVersions[i].ToString());
			}
			stringBuilder.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005E38 File Offset: 0x00004038
		private static Uri AppendTargetEntityKeyIfNeeded(Uri linkUri, LinkDescriptor binding, EntityDescriptor targetResource)
		{
			if (!binding.IsSourcePropertyCollection || EntityStates.Deleted != binding.State)
			{
				return linkUri;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(UriUtil.UriToString(linkUri));
			stringBuilder.Append("?$id=" + targetResource.Identity);
			return UriUtil.CreateUri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005E90 File Offset: 0x00004090
		private bool CreateRequestData(EntityDescriptor entityDescriptor, ODataRequestMessageWrapper requestMessage)
		{
			bool flag = false;
			EntityStates state = entityDescriptor.State;
			if (state != EntityStates.Added)
			{
				if (state == EntityStates.Deleted)
				{
					goto IL_0020;
				}
				if (state != EntityStates.Modified)
				{
					Error.ThrowInternalError(InternalError.UnvalidatedEntityState);
					goto IL_0020;
				}
			}
			flag = true;
			IL_0020:
			if (flag)
			{
				this.SerializerInstance.WriteEntry(entityDescriptor, this.RelatedLinks(entityDescriptor), requestMessage);
			}
			return flag;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005ED5 File Offset: 0x000040D5
		private void CreateRequestData(LinkDescriptor binding, ODataRequestMessageWrapper requestMessage)
		{
			this.SerializerInstance.WriteEntityReferenceLink(binding, requestMessage);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005EE4 File Offset: 0x000040E4
		private void HandleResponsePost(Descriptor descriptor, HeaderCollection contentHeaders)
		{
			if (descriptor.DescriptorKind == DescriptorKind.Entity)
			{
				string text;
				contentHeaders.TryGetHeader("ETag", out text);
				this.HandleResponsePost((EntityDescriptor)descriptor, text);
				return;
			}
			BaseSaveResult.HandleResponsePost((LinkDescriptor)descriptor);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005F20 File Offset: 0x00004120
		private void HandleResponsePost(EntityDescriptor entityDescriptor, string etag)
		{
			try
			{
				if (EntityStates.Added != entityDescriptor.State && EntityStates.Added != entityDescriptor.StreamState)
				{
					Error.ThrowBatchUnexpectedContent(InternalError.EntityNotAddedState);
				}
				if (this.ProcessResponsePayload)
				{
					this.MaterializeResponse(entityDescriptor, this.CreateResponseInfo(entityDescriptor), etag);
				}
				else
				{
					entityDescriptor.ETag = etag;
					entityDescriptor.State = EntityStates.Unchanged;
					entityDescriptor.PropertiesToSerialize.Clear();
				}
				if (entityDescriptor.StreamState != EntityStates.Added)
				{
					foreach (LinkDescriptor linkDescriptor in this.RelatedLinks(entityDescriptor))
					{
						if (Util.IncludeLinkState(linkDescriptor.SaveResultWasProcessed) || linkDescriptor.SaveResultWasProcessed == EntityStates.Added)
						{
							BaseSaveResult.HandleResponsePost(linkDescriptor);
						}
					}
				}
			}
			finally
			{
				if (entityDescriptor.StreamState == EntityStates.Added)
				{
					entityDescriptor.State = EntityStates.Modified;
					entityDescriptor.StreamState = EntityStates.Unchanged;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006000 File Offset: 0x00004200
		private void HandleResponsePut(Descriptor descriptor, HeaderCollection responseHeaders)
		{
			if (descriptor.DescriptorKind != DescriptorKind.Entity)
			{
				if (descriptor.DescriptorKind == DescriptorKind.Link)
				{
					if (EntityStates.Added == descriptor.State || EntityStates.Modified == descriptor.State)
					{
						descriptor.State = EntityStates.Unchanged;
						return;
					}
					if (EntityStates.Detached != descriptor.State)
					{
						Error.ThrowBatchUnexpectedContent(InternalError.LinkBadState);
						return;
					}
				}
				else
				{
					descriptor.State = EntityStates.Unchanged;
					StreamDescriptor streamDescriptor = (StreamDescriptor)descriptor;
					string text;
					responseHeaders.TryGetHeader("ETag", out text);
					streamDescriptor.ETag = text;
				}
				return;
			}
			string text2;
			responseHeaders.TryGetHeader("ETag", out text2);
			EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
			if (this.ProcessResponsePayload)
			{
				this.MaterializeResponse(entityDescriptor, this.CreateResponseInfo(entityDescriptor), text2);
				return;
			}
			if (EntityStates.Modified != entityDescriptor.State && EntityStates.Modified != entityDescriptor.StreamState)
			{
				Error.ThrowBatchUnexpectedContent(InternalError.EntryNotModified);
			}
			if (entityDescriptor.StreamState == EntityStates.Modified)
			{
				entityDescriptor.StreamETag = text2;
				entityDescriptor.StreamState = EntityStates.Unchanged;
				return;
			}
			entityDescriptor.ETag = text2;
			entityDescriptor.State = EntityStates.Unchanged;
			entityDescriptor.PropertiesToSerialize.Clear();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000060E8 File Offset: 0x000042E8
		private void HandleResponseDelete(Descriptor descriptor)
		{
			if (EntityStates.Deleted != descriptor.State)
			{
				Error.ThrowBatchUnexpectedContent(InternalError.EntityNotDeleted);
			}
			if (descriptor.DescriptorKind == DescriptorKind.Entity)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				this.RequestInfo.EntityTracker.DetachResource(entityDescriptor);
				return;
			}
			this.RequestInfo.EntityTracker.DetachExistingLink((LinkDescriptor)descriptor, false);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006140 File Offset: 0x00004340
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		private void AsyncEndRead(IAsyncResult asyncResult)
		{
			BaseSaveResult.AsyncReadState asyncReadState = (BaseSaveResult.AsyncReadState)asyncResult.AsyncState;
			BaseAsyncResult.PerRequest pereq = asyncReadState.Pereq;
			try
			{
				this.CompleteCheck(pereq, InternalError.InvalidEndReadCompleted);
				pereq.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, pereq, InternalError.InvalidEndRead);
				Stream stream = Util.NullCheck<Stream>(pereq.ResponseStream, InternalError.InvalidEndReadStream);
				int num = stream.EndRead(asyncResult);
				if (0 < num)
				{
					Stream stream2 = Util.NullCheck<Stream>(this.ResponseStream, InternalError.InvalidEndReadCopy);
					stream2.Write(this.buildBatchBuffer, 0, num);
					asyncReadState.TotalByteCopied += num;
					if (!asyncResult.CompletedSynchronously && stream.CanRead)
					{
						do
						{
							asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(stream.BeginRead), this.buildBatchBuffer, 0, this.buildBatchBuffer.Length, new AsyncCallback(this.AsyncEndRead), asyncReadState);
							pereq.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
							if (!asyncResult.CompletedSynchronously || pereq.RequestCompleted || base.IsCompletedInternally)
							{
								break;
							}
						}
						while (stream.CanRead);
					}
				}
				else
				{
					pereq.SetComplete();
					if (!base.IsCompletedInternally && !pereq.RequestCompletedSynchronously)
					{
						this.FinishCurrentChange(pereq);
					}
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(pereq, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(pereq);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000062B4 File Offset: 0x000044B4
		private void MaterializeResponse(EntityDescriptor entityDescriptor, ResponseInfo responseInfo, string etag)
		{
			using (MaterializeAtom materializer = this.GetMaterializer(entityDescriptor, responseInfo))
			{
				materializer.SetInsertingObject(entityDescriptor.Entity);
				object obj = null;
				foreach (object obj2 in materializer)
				{
					if (obj != null)
					{
						Error.ThrowInternalError(InternalError.MaterializerReturningMoreThanOneEntity);
					}
					obj = obj2;
				}
				if (entityDescriptor.GetLatestETag() == null)
				{
					entityDescriptor.ETag = etag;
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000634C File Offset: 0x0000454C
		private string GetSourcePropertyUri(LinkDescriptor binding, EntityDescriptor sourceEntityDescriptor)
		{
			if (string.IsNullOrEmpty(binding.SourceProperty))
			{
				return null;
			}
			string text = binding.SourceProperty;
			string text2 = this.RequestInfo.TypeResolver.ResolveServiceEntityTypeFullName(binding.Source.GetType());
			if (string.IsNullOrEmpty(text2))
			{
				return text;
			}
			string text3 = null;
			if (!string.IsNullOrEmpty(sourceEntityDescriptor.EntitySetName) && this.RequestInfo.TypeResolver.TryResolveEntitySetBaseTypeName(sourceEntityDescriptor.EntitySetName, out text3) && !string.IsNullOrEmpty(text3) && !string.Equals(text2, text3, StringComparison.OrdinalIgnoreCase))
			{
				text = text2 + "/" + text;
			}
			return text;
		}

		// Token: 0x0400004E RID: 78
		protected readonly RequestInfo RequestInfo;

		// Token: 0x0400004F RID: 79
		protected readonly Serializer SerializerInstance;

		// Token: 0x04000050 RID: 80
		protected readonly List<Descriptor> ChangedEntries;

		// Token: 0x04000051 RID: 81
		protected readonly SaveChangesOptions Options;

		// Token: 0x04000052 RID: 82
		protected IODataResponseMessage batchResponseMessage;

		// Token: 0x04000053 RID: 83
		protected int entryIndex = -1;

		// Token: 0x04000054 RID: 84
		protected BaseSaveResult.StreamRequestKind streamRequestKind;

		// Token: 0x04000055 RID: 85
		protected Stream mediaResourceRequestStream;

		// Token: 0x04000056 RID: 86
		protected byte[] buildBatchBuffer;

		// Token: 0x0200014F RID: 335
		protected enum StreamRequestKind
		{
			// Token: 0x040006CD RID: 1741
			None,
			// Token: 0x040006CE RID: 1742
			PostMediaResource,
			// Token: 0x040006CF RID: 1743
			PutMediaResource
		}

		// Token: 0x02000150 RID: 336
		private struct AsyncReadState
		{
			// Token: 0x06000D0A RID: 3338 RVA: 0x0002DAB7 File Offset: 0x0002BCB7
			internal AsyncReadState(BaseAsyncResult.PerRequest pereq)
			{
				this.Pereq = pereq;
				this.totalByteCopied = 0;
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0002DAC7 File Offset: 0x0002BCC7
			// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0002DACF File Offset: 0x0002BCCF
			internal int TotalByteCopied
			{
				get
				{
					return this.totalByteCopied;
				}
				set
				{
					this.totalByteCopied = value;
				}
			}

			// Token: 0x040006D0 RID: 1744
			internal readonly BaseAsyncResult.PerRequest Pereq;

			// Token: 0x040006D1 RID: 1745
			private int totalByteCopied;
		}
	}
}
