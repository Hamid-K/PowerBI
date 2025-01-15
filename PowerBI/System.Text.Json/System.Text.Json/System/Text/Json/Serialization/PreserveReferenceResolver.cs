using System;
using System.Collections.Generic;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000095 RID: 149
	internal sealed class PreserveReferenceResolver : ReferenceResolver
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00026FAC File Offset: 0x000251AC
		public PreserveReferenceResolver(bool writing)
		{
			if (writing)
			{
				this._objectToReferenceIdMap = new Dictionary<object, string>(ReferenceEqualityComparer.Instance);
				return;
			}
			this._referenceIdToObjectMap = new Dictionary<string, object>();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00026FD3 File Offset: 0x000251D3
		public override void AddReference(string referenceId, object value)
		{
			if (!this._referenceIdToObjectMap.TryAdd(referenceId, value))
			{
				ThrowHelper.ThrowJsonException_MetadataDuplicateIdFound(referenceId);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00026FEC File Offset: 0x000251EC
		public override string GetReference(object value, out bool alreadyExists)
		{
			string text;
			if (this._objectToReferenceIdMap.TryGetValue(value, out text))
			{
				alreadyExists = true;
			}
			else
			{
				this._referenceCount += 1U;
				text = this._referenceCount.ToString();
				this._objectToReferenceIdMap.Add(value, text);
				alreadyExists = false;
			}
			return text;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002703C File Offset: 0x0002523C
		public override object ResolveReference(string referenceId)
		{
			object obj;
			if (!this._referenceIdToObjectMap.TryGetValue(referenceId, out obj))
			{
				ThrowHelper.ThrowJsonException_MetadataReferenceNotFound(referenceId);
			}
			return obj;
		}

		// Token: 0x04000300 RID: 768
		private uint _referenceCount;

		// Token: 0x04000301 RID: 769
		private readonly Dictionary<string, object> _referenceIdToObjectMap;

		// Token: 0x04000302 RID: 770
		private readonly Dictionary<object, string> _objectToReferenceIdMap;
	}
}
