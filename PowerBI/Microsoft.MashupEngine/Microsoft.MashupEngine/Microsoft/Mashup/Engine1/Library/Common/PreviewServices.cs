using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001105 RID: 4357
	internal static class PreviewServices
	{
		// Token: 0x060071FC RID: 29180 RVA: 0x00187D70 File Offset: 0x00185F70
		public static TypeValue ConvertToDelayedValue(TypeValue type, string itemKind)
		{
			RecordValue recordValue = RecordValue.New(PreviewServices.PreviewDelay, new Value[] { TextValue.New(LinkName.getLinkNameFromLinkKind(itemKind)) });
			return BinaryOperator.AddMeta.Invoke(type, recordValue).AsType;
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x00187DB0 File Offset: 0x00185FB0
		public static bool IsDelayed(TypeValue type, out string typeName)
		{
			Value value;
			if (type.TryGetMetaField("Preview.Delay", out value) && value.IsText)
			{
				typeName = value.AsString;
				return true;
			}
			typeName = null;
			return false;
		}

		// Token: 0x04003EFA RID: 16122
		private static readonly Keys PreviewDelay = Keys.New("Preview.Delay");
	}
}
