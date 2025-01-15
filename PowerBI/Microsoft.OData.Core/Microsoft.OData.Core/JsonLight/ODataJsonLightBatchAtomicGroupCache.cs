using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000225 RID: 549
	internal sealed class ODataJsonLightBatchAtomicGroupCache
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x000448D9 File Offset: 0x00042AD9
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x000448E1 File Offset: 0x00042AE1
		internal bool IsWithinAtomicGroup
		{
			get
			{
				return this.isWithinAtomicGroup;
			}
			set
			{
				this.isWithinAtomicGroup = value;
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000448EA File Offset: 0x00042AEA
		internal bool IsChangesetEnd(string groupId)
		{
			if (!this.isWithinAtomicGroup || (this.precedingMessageGroupId != null && this.precedingMessageGroupId.Equals(groupId)))
			{
				return false;
			}
			this.isWithinAtomicGroup = false;
			this.precedingMessageGroupId = null;
			return true;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0004491C File Offset: 0x00042B1C
		internal bool AddMessageIdAndGroupId(string messageId, string groupId)
		{
			bool flag = false;
			if (groupId.Equals(this.precedingMessageGroupId, StringComparison.Ordinal))
			{
				this.groupToMessageIds[groupId].Add(messageId);
			}
			else
			{
				if (this.groupToMessageIds.ContainsKey(groupId))
				{
					throw new ODataException(Strings.ODataBatchReader_MessageIdPositionedIncorrectly(messageId, groupId));
				}
				this.groupToMessageIds.Add(groupId, new List<string> { messageId });
				this.precedingMessageGroupId = groupId;
				this.isWithinAtomicGroup = true;
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00044994 File Offset: 0x00042B94
		internal string GetGroupId(string targetMessageId)
		{
			foreach (KeyValuePair<string, IList<string>> keyValuePair in this.groupToMessageIds)
			{
				IList<string> value = keyValuePair.Value;
				if (value != null && value.Contains(targetMessageId))
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00044A04 File Offset: 0x00042C04
		internal bool IsGroupId(string id)
		{
			return this.groupToMessageIds.Keys.Contains(id);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00044A18 File Offset: 0x00042C18
		internal IList<string> GetFlattenedMessageIds(IList<string> ids)
		{
			List<string> list = new List<string>();
			if (ids.Count == 0)
			{
				return list;
			}
			foreach (string text in ids)
			{
				IList<string> list2;
				if (this.groupToMessageIds.TryGetValue(text, out list2))
				{
					list.AddRange(list2);
				}
				else
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x04000AB8 RID: 2744
		private readonly Dictionary<string, IList<string>> groupToMessageIds = new Dictionary<string, IList<string>>();

		// Token: 0x04000AB9 RID: 2745
		private string precedingMessageGroupId;

		// Token: 0x04000ABA RID: 2746
		private bool isWithinAtomicGroup;
	}
}
