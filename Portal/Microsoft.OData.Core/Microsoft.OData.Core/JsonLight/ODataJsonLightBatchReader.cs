using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000228 RID: 552
	internal sealed class ODataJsonLightBatchReader : ODataBatchReader
	{
		// Token: 0x06001814 RID: 6164 RVA: 0x000451C8 File Offset: 0x000433C8
		internal ODataJsonLightBatchReader(ODataJsonLightInputContext inputContext, bool synchronous)
			: base(inputContext, synchronous)
		{
			this.batchStream = new ODataJsonLightBatchReaderStream(inputContext);
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x000451E9 File Offset: 0x000433E9
		internal ODataJsonLightInputContext JsonLightInputContext
		{
			get
			{
				return base.InputContext as ODataJsonLightInputContext;
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000451F8 File Offset: 0x000433F8
		protected override string GetCurrentGroupIdImplementation()
		{
			string text = null;
			if (this.messagePropertiesCache != null)
			{
				text = (string)this.messagePropertiesCache.GetPropertyValue("ATOMICITYGROUP");
			}
			return text;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00045228 File Offset: 0x00043428
		protected override ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation()
		{
			string text = (string)this.messagePropertiesCache.GetPropertyValue("ID");
			string text2 = (string)this.messagePropertiesCache.GetPropertyValue("ATOMICITYGROUP");
			IList<string> list = null;
			List<string> list2 = (List<string>)this.messagePropertiesCache.GetPropertyValue("DEPENDSON");
			if (list2 != null && list2.Count != 0)
			{
				this.ValidateDependsOnId(list2, text2, text);
				list = this.atomicGroups.GetFlattenedMessageIds(list2);
			}
			ODataBatchOperationHeaders odataBatchOperationHeaders = (ODataBatchOperationHeaders)this.messagePropertiesCache.GetPropertyValue("HEADERS");
			if (text2 != null)
			{
				odataBatchOperationHeaders.Add("ATOMICITYGROUP", text2);
			}
			Stream bodyContentStream = ((Stream)this.messagePropertiesCache.GetPropertyValue("BODY")) ?? new ODataJsonLightBatchBodyContentReaderStream(this);
			string text3 = (string)this.messagePropertiesCache.GetPropertyValue("METHOD");
			ODataJsonLightBatchReader.ValidateRequiredProperty(text3, "METHOD");
			text3 = text3.ToUpperInvariant();
			string text4 = (string)this.messagePropertiesCache.GetPropertyValue("URL");
			ODataJsonLightBatchReader.ValidateRequiredProperty(text4, "URL");
			int num = text4.IndexOf('?');
			int num2 = text4.IndexOf(':');
			if (num > 0 && num2 > 0 && num < num2)
			{
				text4 = text4.Substring(0, num) + text4.Substring(num).Replace(":", "%3A");
			}
			Uri uri = new Uri(text4, UriKind.RelativeOrAbsolute);
			this.messagePropertiesCache = null;
			return base.BuildOperationRequestMessage(() => bodyContentStream, text3, uri, odataBatchOperationHeaders, text, text2, list, true);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000453C0 File Offset: 0x000435C0
		protected override ODataBatchReaderState ReadAtStartImplementation()
		{
			if (this.mode == ODataJsonLightBatchReader.ReaderMode.NotDetected)
			{
				this.DetectReaderMode();
				return ODataBatchReaderState.Initial;
			}
			this.StartReadingBatchArray();
			this.messagePropertiesCache = new ODataJsonLightBatchPayloadItemPropertiesCache(this);
			string text = (string)this.messagePropertiesCache.GetPropertyValue("ATOMICITYGROUP");
			if (text == null)
			{
				return ODataBatchReaderState.Operation;
			}
			this.HandleNewAtomicGroupStart((string)this.messagePropertiesCache.GetPropertyValue("ID"), text);
			return ODataBatchReaderState.ChangesetStart;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00002393 File Offset: 0x00000593
		protected override ODataBatchReaderState ReadAtChangesetStartImplementation()
		{
			return ODataBatchReaderState.Operation;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00045428 File Offset: 0x00043628
		protected override ODataBatchReaderState ReadAtChangesetEndImplementation()
		{
			if (this.messagePropertiesCache == null && this.JsonLightInputContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				return ODataBatchReaderState.Completed;
			}
			return this.DetectChangesetStates(this.messagePropertiesCache);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00045453 File Offset: 0x00043653
		protected override ODataBatchReaderState ReadAtOperationImplementation()
		{
			if (this.JsonLightInputContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				return this.HandleMessagesEnd();
			}
			if (this.messagePropertiesCache == null)
			{
				this.messagePropertiesCache = new ODataJsonLightBatchPayloadItemPropertiesCache(this);
			}
			return this.DetectChangesetStates(this.messagePropertiesCache);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00045490 File Offset: 0x00043690
		protected override ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			Stream bodyContentStream = ((Stream)this.messagePropertiesCache.GetPropertyValue("BODY")) ?? new ODataJsonLightBatchBodyContentReaderStream(this);
			int num = (int)this.messagePropertiesCache.GetPropertyValue("STATUS");
			string text = (string)this.messagePropertiesCache.GetPropertyValue("ID");
			string text2 = (string)this.messagePropertiesCache.GetPropertyValue("ATOMICITYGROUP");
			ODataBatchOperationHeaders odataBatchOperationHeaders = (ODataBatchOperationHeaders)this.messagePropertiesCache.GetPropertyValue("HEADERS");
			this.messagePropertiesCache = null;
			return base.BuildOperationResponseMessage(() => bodyContentStream, num, odataBatchOperationHeaders, text, text2);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00045543 File Offset: 0x00043743
		private static void ValidateRequiredProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataBatchReader_RequestPropertyMissing(propertyName));
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00045554 File Offset: 0x00043754
		private void ValidateDependsOnId(IEnumerable<string> dependsOnIds, string atomicityGroupId, string requestId)
		{
			foreach (string text in dependsOnIds)
			{
				if (text.Equals(atomicityGroupId))
				{
					throw new ODataException(Strings.ODataBatchReader_SameRequestIdAsAtomicityGroupIdNotAllowed(text, atomicityGroupId));
				}
				if (text.Equals(requestId))
				{
					throw new ODataException(Strings.ODataBatchReader_SelfReferenceDependsOnRequestIdNotAllowed(text, requestId));
				}
				string groupId = this.atomicGroups.GetGroupId(text);
				if (groupId != null && !groupId.Equals(this.atomicGroups.GetGroupId(requestId)))
				{
					throw new ODataException(Strings.ODataBatchReader_DependsOnRequestIdIsPartOfAtomicityGroupNotAllowed(text, groupId));
				}
			}
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000455F4 File Offset: 0x000437F4
		private void DetectReaderMode()
		{
			this.batchStream.JsonReader.ReadNext();
			this.batchStream.JsonReader.ReadStartObject();
			string text = this.batchStream.JsonReader.ReadPropertyName();
			if (ODataJsonLightBatchReader.PropertyNameRequests.Equals(text, StringComparison.OrdinalIgnoreCase))
			{
				this.mode = ODataJsonLightBatchReader.ReaderMode.Requests;
				return;
			}
			if (ODataJsonLightBatchReader.PropertyNameResponses.Equals(text, StringComparison.OrdinalIgnoreCase))
			{
				this.mode = ODataJsonLightBatchReader.ReaderMode.Responses;
				return;
			}
			throw new ODataException(Strings.ODataBatchReader_JsonBatchTopLevelPropertyMissing);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004566C File Offset: 0x0004386C
		private ODataBatchReaderState StartReadingBatchArray()
		{
			this.batchStream.JsonReader.ReadStartArray();
			return ODataBatchReaderState.Operation;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0004568C File Offset: 0x0004388C
		private void HandleNewAtomicGroupStart(string messageId, string groupId)
		{
			if (this.atomicGroups.IsGroupId(groupId))
			{
				throw new ODataException(Strings.ODataBatchReader_DuplicateAtomicityGroupIDsNotAllowed(groupId));
			}
			this.atomicGroups.AddMessageIdAndGroupId(messageId, groupId);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000456B8 File Offset: 0x000438B8
		private ODataBatchReaderState HandleMessagesEnd()
		{
			ODataBatchReaderState odataBatchReaderState;
			if (this.atomicGroups.IsWithinAtomicGroup)
			{
				this.atomicGroups.IsWithinAtomicGroup = false;
				odataBatchReaderState = ODataBatchReaderState.ChangesetEnd;
			}
			else
			{
				this.JsonLightInputContext.JsonReader.ReadEndArray();
				this.JsonLightInputContext.JsonReader.ReadEndObject();
				odataBatchReaderState = ODataBatchReaderState.Completed;
			}
			return odataBatchReaderState;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00045708 File Offset: 0x00043908
		private ODataBatchReaderState DetectChangesetStates(ODataJsonLightBatchPayloadItemPropertiesCache messagePropertiesCache)
		{
			string text = (string)messagePropertiesCache.GetPropertyValue("ID");
			string text2 = (string)messagePropertiesCache.GetPropertyValue("ATOMICITYGROUP");
			bool flag = this.atomicGroups.IsChangesetEnd(text2);
			bool flag2 = false;
			if (!flag && text2 != null)
			{
				flag2 = this.atomicGroups.AddMessageIdAndGroupId(text, text2);
			}
			ODataBatchReaderState odataBatchReaderState = ODataBatchReaderState.Operation;
			if (flag)
			{
				odataBatchReaderState = ODataBatchReaderState.ChangesetEnd;
			}
			else if (flag2)
			{
				odataBatchReaderState = ODataBatchReaderState.ChangesetStart;
			}
			return odataBatchReaderState;
		}

		// Token: 0x04000AC9 RID: 2761
		private readonly ODataJsonLightBatchReaderStream batchStream;

		// Token: 0x04000ACA RID: 2762
		private readonly ODataJsonLightBatchAtomicGroupCache atomicGroups = new ODataJsonLightBatchAtomicGroupCache();

		// Token: 0x04000ACB RID: 2763
		private static string PropertyNameRequests = "requests";

		// Token: 0x04000ACC RID: 2764
		private static string PropertyNameResponses = "responses";

		// Token: 0x04000ACD RID: 2765
		private ODataJsonLightBatchReader.ReaderMode mode;

		// Token: 0x04000ACE RID: 2766
		private ODataJsonLightBatchPayloadItemPropertiesCache messagePropertiesCache;

		// Token: 0x020003E2 RID: 994
		private enum ReaderMode
		{
			// Token: 0x04000F74 RID: 3956
			NotDetected,
			// Token: 0x04000F75 RID: 3957
			Requests,
			// Token: 0x04000F76 RID: 3958
			Responses
		}
	}
}
